using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Mirage.Sockets.Udp;
using UnityEngine;

namespace Mirage.Standalone
{
    /// <summary>
    /// Takes care of running the necessary Mirage environment.
    /// </summary>
    public class StandaloneRunner
    {
        private readonly List<NetworkServer> servers = new List<NetworkServer>();
        private readonly List<NetworkClient> clients = new List<NetworkClient>();
        private readonly List<UdpSocketFactory> socketFactories = new List<UdpSocketFactory>();

        private Action updated;

        public StandaloneRunner()
        {
            RunInitializeMethods();
            Update();

            Console.WriteLine("Mirage Standalone Runner initialized");
        }

        public NetworkServer AddServer(ushort port = 7777)
        {
            var socketFactory = new UdpSocketFactory { Port = port };
            var server = new NetworkServer { SocketFactory = socketFactory };

            servers.Add(server);
            socketFactories.Add(socketFactory);

            server.Started += () =>
            {
                updated += server.Update;
            };

            server.Stopped += () =>
            {
                updated -= server.Update;
            };

            return server;
        }

        public NetworkClient AddClient()
        {
            var socketFactory = new UdpSocketFactory();
            var client = new NetworkClient { SocketFactory = socketFactory };

            clients.Add(client);
            socketFactories.Add(socketFactory);

            client.Started += () =>
            {
                updated += client.Update;
            };

            client.Disconnected += (_) =>
            {
                updated -= client.Update;
            };

            return client;
        }

        async void Update()
        {
            while (true)
            {
                try
                {
                    updated?.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await Task.Delay(5);
            }
        }

        static void RunInitializeMethods()
        {
            var asm = Assembly.GetEntryAssembly();

            MethodInfo[] methods = asm.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false).Length > 0)
                .ToArray();

            foreach (MethodInfo method in methods)
            {
                method.Invoke(null, null);
            }
        }
    }
}
