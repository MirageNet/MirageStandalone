﻿using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mirage.Weaver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mirage Weaver start");
            string dllPath = args[0];
            byte[] data = File.ReadAllBytes(dllPath);
            Assembly asm = Assembly.Load(data);

            // TODO: use proper Assembly paths 
            string[] references = asm.GetReferencedAssemblies().Select(a => Path.Combine(Path.GetDirectoryName(dllPath), a.Name)).ToArray();
            var compiledAssembly = new CompiledAssembly2(dllPath, references, new string[0]);
            var weaverLogger = new WeaverLogger();
            var weaver = new Weaver(weaverLogger);
            AssemblyDefinition assemblyDefinition = weaver.Weave(compiledAssembly);
            Write(assemblyDefinition, dllPath, compiledAssembly.PdbPath);
        }

        private static void Write(AssemblyDefinition assemblyDefinition, string dllPath, string pdbPath)
        {
            var pe = new MemoryStream();
            var pdb = new MemoryStream();

            var writerParameters = new WriterParameters
            {
                SymbolWriterProvider = new PortablePdbWriterProvider(),
                SymbolStream = pdb,
                WriteSymbols = true
            };

            assemblyDefinition?.Write(pe, writerParameters);

            File.WriteAllBytes(dllPath, pe.ToArray());
            File.WriteAllBytes(pdbPath, pdb.ToArray());
        }
    }


    public class CompiledAssembly2 : ICompiledAssembly
    {
        public CompiledAssembly2(string dllPath, string[] references, string[] defines)
        {
            this.Name = Path.GetFileName(dllPath);
            this.PdbPath = $"{Path.GetDirectoryName(dllPath)}/{Path.GetFileNameWithoutExtension(dllPath)}.pdb";
            byte[] peData = File.ReadAllBytes(dllPath);
            byte[] pdbData = File.ReadAllBytes(this.PdbPath);
            this.InMemoryAssembly = new InMemoryAssembly(peData, pdbData);
            this.References = references;
            this.Defines = defines;
        }

        public InMemoryAssembly InMemoryAssembly { get; }

        public string Name { get; }
        public string PdbPath { get; }
        public string[] References { get; }
        public string[] Defines { get; }
    }

    public interface ICompiledAssembly
    {
        InMemoryAssembly InMemoryAssembly { get; }
        string Name { get; }
        string[] References { get; }
        string[] Defines { get; }
    }
    public class InMemoryAssembly
    {
        public InMemoryAssembly(byte[] peData, byte[] pdbData)
        {
            this.PeData = peData;
            this.PdbData = pdbData;
        }

        public byte[] PeData { get; }
        public byte[] PdbData { get; }
    }
}
