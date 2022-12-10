using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Unity.CompilationPipeline.Common.Diagnostics;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Mirage.Weaver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Mirage Weaver start");
                var dllPath = args[0];
                var data = File.ReadAllBytes(dllPath);
                var asm = Assembly.Load(data);

                // TODO: use proper Assembly paths 
                var references = asm.GetReferencedAssemblies().Select(a => Path.Combine(Path.GetDirectoryName(dllPath), a.Name)).ToArray();
                var compiledAssembly = new CompiledAssembly(dllPath, references, new string[0]);
                var weaverLogger = new WeaverLogger(false);
                var weaver = new Weaver(weaverLogger);
                var assemblyDefinition = weaver.Weave(compiledAssembly);
                Write(assemblyDefinition, dllPath, compiledAssembly.PdbPath);


                var exitCode = CheckDiagnostics(weaverLogger);
                Environment.ExitCode = 0;
            }
            catch (Exception e)
            {
                Environment.ExitCode = 1;
                Console.Error.WriteLine(e);
                return;
            }
        }

        private static int CheckDiagnostics(WeaverLogger weaverLogger)
        {
            var diagnostics = weaverLogger.Diagnostics;
            var exitCode = 0;
            foreach (var message in diagnostics)
            {
                var data = message.MessageData;
                var type = message.DiagnosticType;
                Console.WriteLine($"[{type}]: {data}");

                if (type == DiagnosticType.Error)
                    exitCode = 1;
            }
            return exitCode;
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

    public class CompiledAssembly : ICompiledAssembly
    {
        public CompiledAssembly(string dllPath, string[] references, string[] defines)
        {
            Name = Path.GetFileName(dllPath);
            PdbPath = $"{Path.GetDirectoryName(dllPath)}/{Path.GetFileNameWithoutExtension(dllPath)}.pdb";
            var peData = File.ReadAllBytes(dllPath);
            var pdbData = File.ReadAllBytes(PdbPath);
            InMemoryAssembly = new InMemoryAssembly(peData, pdbData);
            References = references;
            Defines = defines;
        }

        public InMemoryAssembly InMemoryAssembly { get; }

        public string Name { get; }
        public string PdbPath { get; }
        public string[] References { get; }
        public string[] Defines { get; }
    }
}
