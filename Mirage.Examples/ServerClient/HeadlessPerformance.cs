using System;
using System.Collections.Generic;
using System.Threading;
using Mirage.Standalone;

namespace Mirage.Examples
{
    public class HeadlessPerformance
    {
        #region Fields

        private List<string> arguments;
        private ushort port;

        private NetworkServer server;
        private ServerObjectManager serverObjectManager;
        private NetworkSceneManager networkSceneManager;
        private CharacterSpawner characterSpawner;
        private readonly StandaloneRunner runner;

        #endregion

        public HeadlessPerformance(string[] args)
        {
            arguments = new List<string>(args);
            runner = new StandaloneRunner();

            ParsePort();
            ParseForServerMode();
            StartClients();
        }

        private void ParsePort()
        {
            string portString = GetArgValue("-port");
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
            if (!HasArg("-server")) return;

            server = runner.AddServer(port);
            server.MaxConnections = 9999;
            networkSceneManager = new NetworkSceneManager { Server = server };
            serverObjectManager = new ServerObjectManager { Server = server, NetworkSceneManager = networkSceneManager };
            serverObjectManager.Start();
            characterSpawner = new CharacterSpawner { ServerObjectManager = serverObjectManager, Server = server };

            //TODO fix this for listener
            //_server.Started.AddListener(OnServerStarted);

            // TODO fix this for listener
            //_server.Authenticated.AddListener(conn => _serverObjectManager.SpawnVisibleObjects(conn, true));
            server.StartServer();

            Console.WriteLine("Starting Server Only Mode");
        }

        private void StartClients()
        {
            if (!HasArg("-client")) return;

            string address = GetArgValue("-address");

            if (string.IsNullOrEmpty(address))
            {
                address = "localhost";
            }

            //nested clients
            int clonesCount = 1;
            string clonesString = GetArgValue("-client");
            if (!string.IsNullOrEmpty(clonesString))
            {
                clonesCount = int.Parse(clonesString);
            }

            Console.WriteLine("Starting {0} clients", clonesCount);

            // connect from a bunch of clients
            for (int i = 0; i < clonesCount; i++)
            {
                StartClient(i, address);

                Thread.Sleep(5);

                Console.WriteLine("Started {0} clients", i + 1);
            }
        }

        void StartClient(int i, string networkAddress)
        {
            NetworkClient client = runner.AddClient();

            var objectManager = new ClientObjectManager();
            var spawner = new CharacterSpawner();
            var networkSceneManager = new NetworkSceneManager { Client = client };

            objectManager.Client = client;
            objectManager.NetworkSceneManager = networkSceneManager;
            objectManager.Start();

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
            int index = arguments.IndexOf(name);
            if (index == -1 || index + 1 == arguments.Count) return null;

            return arguments[index + 1];
        }

        private bool HasArg(string name)
        {
            return arguments.Contains(name);
        }
    }
}
