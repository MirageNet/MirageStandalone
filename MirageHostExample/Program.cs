using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Mirage;
using Mirage.Sockets.Udp;
using UnityEngine;

namespace MirageHostExample
{
    [NetworkMessage]
    public struct HelloMessage {}
    
    internal class Program
    {
        static NetworkServer Server;
        static NetworkClient Client;

        static void Main(string[] args)
        {
            /// Start our pooling loop for data.
            //_ = Task.Run(Run);
            Assembly asm = Assembly.GetExecutingAssembly();
            var methods = asm.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false).Length > 0)
                .ToArray();

            foreach (var method in methods) {
                method.Invoke(null, null);
            }

            Server = new NetworkServer
            {
                SocketFactory = new UdpSocketFactory()
            };

            Client = new NetworkClient()
            {
                SocketFactory = new UdpSocketFactory()
            };

            // Boot up server.
            Server.StartServer();

            // Boot up a client.
            Client.Connect("localhost");

            while (true)
            {
                try
                {
                    if (Server is { Active: true })
                        Server.Update();
                    if (Client is { Active: true })
                        Client.Update();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Thread.Sleep(5);
            }
        }
    }
}
