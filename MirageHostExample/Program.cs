using System;
using System.Threading;
using System.Threading.Tasks;
using Mirage;
using Mirage.Sockets.Udp;

namespace MirageHostExample
{
    internal class Program
    {
        static NetworkServer Server;
        static NetworkClient Client;

        static void Main(string[] args)
        {
            /// Start our pooling loop for data.
            //_ = Task.Run(Run);

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

        private static Task Run()
        {
            while (true)
            {
                try
                {
                    if (Server is { Active: true })
                        Server.Update();
                    if (Client is { Active: true })
                        Client.Update();

                    Console.WriteLine("Looping Task");
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
