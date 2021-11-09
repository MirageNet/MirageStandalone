using System.Threading;
using Mirage;
using Mirage.Serialization;
using Mirage.Sockets.Udp;

namespace MirageListServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var listServer = new ListServer();
            listServer.Run();
        }
    }
    class ListServer
    {
        NetworkServer _server;
        public void Run()
        {
            StartServer();
            RegisterMessages();
            Loop();
        }

        private void StartServer()
        {
            _server = new NetworkServer();
            _server.SocketFactory = new UdpSocketFactory();
            _server.PeerConfig = new Mirage.SocketLayer.Config
            {
                MaxConnections = 1000,
            };

            _server.StartServer();
        }

        private void RegisterMessages()
        {
            _server.MessageHandler.RegisterHandler()
        }

        private void Loop()
        {
            while (true)
            {
                _server.Update();
                Thread.Sleep(10);
            }
        }
    }

    /// <summary>
    /// Request to be added to list
    /// </summary>
    [NetworkMessage]
    struct AddServer
    {
        public string displayName;
        public string customAddress;
        [BitCount(16)] public int port;
        [VarIntBlocks(7)] public int PlayerCount;
        [VarIntBlocks(7)] public int MaxPlayerCount;
    }

    /// <summary>
    /// Request to be added to list
    /// </summary>
    [NetworkMessage]
    struct UpdateServer
    {
        public ulong id;
        public string displayName;
        public string customAddress;
        [BitCount(16)] public int port;
        [VarIntBlocks(7)] public int PlayerCount;
        [VarIntBlocks(7)] public int MaxPlayerCount;
    }

    /// <summary>
    /// Reply from AddServer with unique id
    /// </summary>
    [NetworkMessage]
    struct AddServerReply
    {
        /// <summary>
        /// Id used to update server in future requests
        /// </summary>
        public ulong id;
    }

    /// <summary>
    /// 
    /// </summary>
    [NetworkMessage]
    struct RemoveServer
    {
        ulong id;
    }
}
