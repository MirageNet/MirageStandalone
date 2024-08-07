using System;
using System.Threading.Tasks;
using Mirage.Logging;
using Mirage.Sockets.Udp;
using UnityEngine;

namespace Mirage.Standalone
{
    /// <summary>
    /// Takes care of running the necessary Mirage environment.
    /// </summary>
    public class StandaloneRunner
    {
        private Action updated;
        private bool _running;
        private readonly int _sleepDelay;

        public StandaloneRunner(int sleepDelay)
        {
            Debug.unityLogger = new StandaloneLogger();
            InitializeReadWrite.RunMethods();
            _sleepDelay = sleepDelay;
            _running = true;
            Update();

            Console.WriteLine("Mirage Standalone Runner initialized");
        }
        public void Stop()
        {
            _running = false;
            Application.InvokeQuitting();
        }

        public NetworkServer AddServer(ushort port = 7777)
        {
            var socketFactory = new UdpSocketFactory { Port = port };
            var server = new NetworkServer { SocketFactory = socketFactory };

            server.Started.AddListener(() =>
            {
                updated += server.Update;
            });

            server.Stopped.AddListener(() =>
            {
                updated -= server.Update;
            });

            return server;
        }

        public NetworkClient AddClient()
        {
            var socketFactory = new UdpSocketFactory();
            var client = new NetworkClient { SocketFactory = socketFactory };

            client.Started.AddListener(() =>
            {
                updated += client.Update;
            });

            client.Disconnected.AddListener((_) =>
            {
                updated -= client.Update;
            });

            return client;
        }

        async void Update()
        {
            while (_running)
            {
                try
                {
                    updated?.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await Task.Delay(_sleepDelay);
            }
        }
    }
}
