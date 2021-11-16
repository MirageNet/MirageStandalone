using System;
using System.Collections.Generic;
using System.Threading;
using Mirage.ListServer.MasterServer;
using Mirage.SocketLayer;
using Mirage.Sockets.Udp;

namespace Mirage.NatPunchthrough.MasterServer
{
    class Runner
    {
        NetworkServer _server;
        Puncher _puncher;

        public int sleepMilliseconds = 10;
        public int timeoutSeconds;

        public void Run()
        {
            StartServer();
            _puncher = new Puncher(_server.MessageHandler);
            Loop();
        }

        private void StartServer()
        {
            _server = new NetworkServer();
            _server.SocketFactory = new UdpSocketFactory()
            {
                Port = 8001
            };
            _server.PeerConfig = new Config
            {
                MaxConnections = 1000,
            };

            _server.StartServer();
        }

        private void Loop()
        {
            while (true)
            {
                _server.Update();
                Thread.Sleep(sleepMilliseconds);
            }
        }
    }
    class Puncher
    {
        readonly Dictionary<string, Room> keyPlayerPair = new Dictionary<string, Room>();
        // todo, do we need secure random?
        readonly Random random = new Random();

        public Puncher(MessageHandler messageHandler)
        {
            messageHandler.RegisterHandler<RequestRoomKey>(HandleRequestRoomKey);
            messageHandler.RegisterHandler<RequestPunch>(HandleRequestPunch);
        }

        string CreateKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] key = new char[6];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = chars[random.Next(0, chars.Length)];
            }
            return key.ToString();
        }
        private void HandleRequestRoomKey(INetworkPlayer player, RequestRoomKey message)
        {
            string key = CreateKey();
            keyPlayerPair.Add(key, new Room
            {
                owner = player,
                created = DateTime.Now,
            });
            player.Send(new RequestRoomKeyReply { key = key });
        }
        private void HandleRequestPunch(INetworkPlayer player, RequestPunch message)
        {
            var msg = new RequestPunchReply();

            if (keyPlayerPair.TryGetValue(message.key, out Room room) && Helper.GetIPEndPoint(room.owner.Connection.EndPoint, out System.Net.IPEndPoint ipEndPoint))
            {
                msg.success = true;
                msg.address = ipEndPoint.Address.ToString();
                msg.port = ipEndPoint.Port;
            }

            player.Send(msg);
        }
    }
    class Room
    {
        public INetworkPlayer owner;
        public DateTime created;
    }
}
namespace Mirage.NatPunchthrough
{
    [NetworkMessage]
    struct RequestRoomKey
    {
    }
    [NetworkMessage]
    struct RequestRoomKeyReply
    {
        public string key;
    }

    [NetworkMessage]
    struct RequestPunch
    {
        public string key;
    }
    [NetworkMessage]
    struct RequestPunchReply
    {
        public bool success;
        public string address;
        public int port;
    }
}

namespace Mirage.NatPunchthrough.Client
{
    public class PuncherClient
    {
        private readonly INetworkPlayer _masterServer;

        public PuncherClient(MessageHandler messageHandler, INetworkPlayer masterServer)
        {
            _masterServer = masterServer;
            messageHandler.RegisterHandler<RequestRoomKeyReply>(HandleRoomKeyReply);
            messageHandler.RegisterHandler<RequestPunchReply>(HandlePunchReply);
        }

        private void HandleRoomKeyReply(INetworkPlayer player, RequestRoomKeyReply message)
        {
            throw new NotImplementedException();
        }

        private void HandlePunchReply(INetworkPlayer player, RequestPunchReply message)
        {
            throw new NotImplementedException();
        }
    }
}
