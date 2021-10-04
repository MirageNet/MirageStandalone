using System;
using Mirage;
using Mirage.Standalone;

namespace MirageHostExample
{
    [NetworkMessage]
    public struct HelloMessage { }

    internal static class Program
    {
        static void Main(string[] args)
        {
            var runner = new StandaloneRunner();
            NetworkServer server = runner.AddServer();
            NetworkClient client = runner.AddClient();

            server.StartServer();
            client.Connect("localhost");

            Console.ReadLine();
        }
    }
}
