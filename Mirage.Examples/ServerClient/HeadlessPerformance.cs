using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Mirage.Standalone;

namespace Mirage.Examples
{
    public class HeadlessPerformance
    {
        #region Fields

        private readonly List<string> arguments;
        private ushort port;

        private NetworkServer server;
        private readonly StandaloneRunner runner;

        #endregion

        public HeadlessPerformance(string[] args)
        {
            arguments = new List<string>(args);
            runner = new StandaloneRunner(5);

            ParsePort();
            ParseForServerMode();
            StartClients();

        }

        public void Stop()
        {
            runner.Stop();
        }

        private void ParsePort()
        {
            var portString = GetArgValue("-port");
            if (string.IsNullOrEmpty(portString))
            {
                port = 7777;
                return;
            }

            if (!ushort.TryParse(portString, out port))
            {
                port = 7777;
            }
        }


        private void ParseForServerMode()
        {
            if (!HasArg("-server") && !Debugger.IsAttached) return;

            server = runner.AddServer(port);
            server.MaxConnections = 9999;

            //TODO fix this for listener
            //_server.Started.AddListener(OnServerStarted);

            // TODO fix this for listener
            //_server.Authenticated.AddListener(conn => _serverObjectManager.SpawnVisibleObjects(conn, true));
            server.StartServer();

            Console.WriteLine("Starting Server Only Mode");
        }

        private async Task StartClients()
        {
            if (!HasArg("-client") && !Debugger.IsAttached) return;

            var address = GetArgValue("-address");

            if (string.IsNullOrEmpty(address))
            {
                address = "localhost";
            }

            //nested clients
            var clonesCount = 1;
            var clonesString = GetArgValue("-client");
            if (!string.IsNullOrEmpty(clonesString))
            {
                clonesCount = int.Parse(clonesString);
            }

            Console.WriteLine("Starting {0} clients", clonesCount);

            // connect from a bunch of clients
            for (var i = 0; i < clonesCount; i++)
            {
                StartClient(i, address);

                await Task.Delay(5);

                Console.WriteLine("Started {0} clients", i + 1);
            }
        }

        void StartClient(int i, string networkAddress)
        {
            var client = runner.AddClient();

            /* TODO Fix this.
            objectManager.RegisterPrefab(MonsterPrefab.GetComponent<NetworkIdentity>());
            objectManager.RegisterPrefab(PlayerPrefab.GetComponent<NetworkIdentity>());


            spawner.Client = client;
            //TODO Fix this spawner.PlayerPrefab = PlayerPrefab.GetComponent<NetworkIdentity>();
            spawner.ClientObjectManager = objectManager;
            spawner.SceneManager = networkSceneManager;
            */
            client.Connect(networkAddress, port);
        }

        private string GetArgValue(string name)
        {
            var index = arguments.IndexOf(name);
            if (index == -1 || index + 1 == arguments.Count) return null;

            return arguments[index + 1];
        }

        private bool HasArg(string name)
        {
            return arguments.Contains(name);
        }
    }
}
