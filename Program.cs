using Mirage;
using Mirage.Weaver;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.IO;
using System.Threading;

namespace MirageStandalone
{
    class Program
    {
        static NetworkServer server = new NetworkServer();
        static NetworkClient client = new NetworkClient();

        static void Main(string[] args)
        {
            Weave(args);

            server.StartServer();
            client.Connect("localhost");

            while (true)
            {
                server.Update();
                client.Update();

                Thread.Sleep(5);
            }
        }

        private static void Weave(string[] args)
        {
            string dllPath = args[0];
            string pdbPath = $"{Path.GetDirectoryName(dllPath)}/{Path.GetFileNameWithoutExtension(dllPath)}.pdb";
            var compiledAssembly = new CompiledAssembly()
            {
                Name = Path.GetFileNameWithoutExtension(dllPath),
                PeData = File.ReadAllBytes(dllPath),
                PdbData = File.ReadAllBytes(pdbPath),
                Defines = new string[0],
                References = new string[0],
            };

            var weaver = new Weaver(new WeaverLogger());
            AssemblyDefinition assemblyDefinition = weaver.Weave(compiledAssembly);

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
}
