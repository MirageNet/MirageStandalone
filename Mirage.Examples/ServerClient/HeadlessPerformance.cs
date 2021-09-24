using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mirage.SocketLayer;
using Mirage.Sockets.Udp;

namespace Mirage.Examples
{
    public class HeadlessPerformance
    {
        #region Fields

        private string[] cachedArgs;
        private string port;

        private NetworkServer _server;
        private ServerObjectManager _serverObjectManager;
        private SocketFactory socketFactory;
        private NetworkSceneManager _networkSceneManager;
        private CharacterSpawner _characterSpawner;
        private readonly List<NetworkClient> _clients;

        #endregion

        public HeadlessPerformance(string args)
        {
            _clients = new List<NetworkClient>();

            cachedArgs = args.Split(' ');

            //Try to find port
            port = GetArgValue("-port");

            //Try to find Socket
            ParseForSocket();

            Task.Factory.StartNew(Update, TaskCreationOptions.LongRunning);

            //Server mode?
            ParseForServerMode();

            //Or client mode?
            Task.Run(StartClients);
        }

        private async void Update()
        {
            while (true)
            {
                try
                {
                    if (_server is { Active: true })
                        _server.Update();

                    for (int i = _clients.Count - 1; i >= 0; i--)
                    {
                        if (_clients[i] != null && _clients[i].Active)
                            _clients[i].Update();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await Task.Delay(5);
            }
        }

        private async void StartClients()
        {
            string clientArg = GetArg("-client");
            if (!string.IsNullOrEmpty(clientArg))
            {
                //network address provided?
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

                    await Task.Delay(1);

                    Console.WriteLine("Started {0} clients", i + 1);
                }
            }
        }

        void StartClient(int i, string networkAddress)
        {
            var client = new NetworkClient { SocketFactory = socketFactory };

            _clients.Add(client);

            var objectManager = new ClientObjectManager();

            var spawner = new CharacterSpawner();

            var networkSceneManager = new NetworkSceneManager { Client = client };

            objectManager.Client = client;
            objectManager.NetworkSceneManager = networkSceneManager;
            objectManager.Start();

            /* TODO Fix this.
            objectManager.RegisterPrefab(MonsterPrefab.GetComponent<NetworkIdentity>());
            objectManager.RegisterPrefab(PlayerPrefab.GetComponent<NetworkIdentity>());
            */

            spawner.Client = client;
            //TODO Fix this spawner.PlayerPrefab = PlayerPrefab.GetComponent<NetworkIdentity>();
            spawner.ClientObjectManager = objectManager;
            spawner.SceneManager = networkSceneManager;

            try
            {
                client.Connect(networkAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ParseForServerMode()
        {
            if (string.IsNullOrEmpty(GetArg("-server"))) return;

            _server = new NetworkServer { MaxConnections = 9999, SocketFactory = socketFactory };

            _networkSceneManager = new NetworkSceneManager { Server = _server };

            _serverObjectManager = new ServerObjectManager { Server = _server, NetworkSceneManager = _networkSceneManager };

            _serverObjectManager.Start();

            _characterSpawner = new CharacterSpawner { ServerObjectManager = _serverObjectManager, Server = _server };

            //TODO fix this for listener
            //_server.Started.AddListener(OnServerStarted);

            // TODO fix this for listener
            //_server.Authenticated.AddListener(conn => _serverObjectManager.SpawnVisibleObjects(conn, true));
            _server.StartServer();

            Console.WriteLine("Starting Server Only Mode");
        }

        private void ParseForSocket()
        {
            string socket = GetArgValue("-socket");

            if (string.IsNullOrEmpty(socket) || socket.Equals("udp"))
            {

                socketFactory = new UdpSocketFactory();

                //Try to apply port if exists and needed by transport.

                //TODO: Uncomment this after the port is made public
                /*if (!string.IsNullOrEmpty(port))
                {
                    newSocket.port = ushort.Parse(port);
                    newSocket.
                }*/
            }
        }

        private string GetArgValue(string name)
        {
            for (int i = 0; i < cachedArgs.Length; i++)
            {
                if (cachedArgs[i] == name && cachedArgs.Length > i + 1)
                {
                    return cachedArgs[i + 1];
                }
            }
            return null;
        }

        private string GetArg(string name)
        {
            for (int i = 0; i < cachedArgs.Length; i++)
            {
                if (cachedArgs[i] == name)
                {
                    return cachedArgs[i];
                }
            }
            return null;
        }
    }
}
