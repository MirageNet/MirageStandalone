using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Mirage.Logging;
using Mirage.Serialization;
using Mirage.SocketLayer;
using Mirage.Sockets.Udp;
using UnityEngine;

namespace Mirage.ListServer.MasterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeReadWrite.RunMethods();
            ConfigureLog();
            var listServer = new Runner();
            listServer.Run();
        }

        static void ConfigureLog()
        {
            LogFactory.GetLogger<Mirage.SocketLayer.Peer>().filterLogType = LogType.Warning;
        }
    }

    class Runner
    {
        NetworkServer _server;
        ListServer _listServer;

        public int sleepMilliseconds = 10;
        public int timeoutSeconds;

        public void Run()
        {
            StartServer();
            _listServer = new ListServer(_server.MessageHandler, 60);
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
    class ListServer
    {
        static readonly ILogger logger = LogFactory.GetLogger<ListServer>();

        readonly Dictionary<IEndPoint, Server> _servers = new Dictionary<IEndPoint, Server>();
        readonly HashSet<IEndPoint> toRemove = new HashSet<IEndPoint>();
        readonly int _timeoutSeconds;
        DateTime _now;


        public ListServer(MessageHandler messageHandler, int timeoutSeconds)
        {
            _timeoutSeconds = timeoutSeconds;
            messageHandler.RegisterHandler<AddServer>(LogWrapper<AddServer>(AddServerHandler));
            messageHandler.RegisterHandler<UpdateServer>(LogWrapper<UpdateServer>(UpdateServerHandler));
            messageHandler.RegisterHandler<KeepAlive>(LogWrapper<KeepAlive>(KeepAliveHandler));
            messageHandler.RegisterHandler<RemoveServer>(LogWrapper<RemoveServer>(RemoveServerHandler));
            messageHandler.RegisterHandler<GetServers>(LogWrapper<GetServers>(GetServersHandler));
        }
        static MessageDelegateWithPlayer<T> LogWrapper<T>(MessageDelegateWithPlayer<T> inner)
        {
            return (INetworkPlayer p, T msg) =>
            {
                if (logger.LogEnabled()) logger.Log($"{typeof(T).Name} from {p}");
                inner.Invoke(p, msg);
            };
        }

        public void Update()
        {
            _now = DateTime.Now;
            CheckTimeout();

        }

        private void CheckTimeout()
        {
            DateTime timeOut = _now - TimeSpan.FromSeconds(_timeoutSeconds);
            toRemove.Clear();
            foreach (KeyValuePair<IEndPoint, Server> kvp in _servers)
            {
                DateTime time = kvp.Value.lastUpdated;
                if (time < timeOut)
                {
                    toRemove.Add(kvp.Key);
                }
            }

            foreach (IEndPoint remove in toRemove)
            {
                _servers.Remove(remove);
            }
        }

        void AddServerHandler(INetworkPlayer sender, AddServer msg)
        {
            IEndPoint endPoint = sender.Connection.EndPoint;
            if (Helper.GetIPAddress(endPoint, out IPAddress address))
            {
                if (_servers.TryGetValue(sender.Connection.EndPoint, out Server item))
                {
                    item.port = msg.port;
                    Helper.UpdateIfNotNull(ref item.displayName, msg.displayName);
                    Helper.UpdateIfNotNull(ref item.playerCount, msg.PlayerCount);
                    Helper.UpdateIfNotNull(ref item.maxPlayerCount, msg.MaxPlayerCount);
                    item.lastUpdated = _now;
                }
                else
                {
                    _servers.Add(endPoint, new Server(address.ToString())
                    {
                        port = msg.port,
                        displayName = msg.displayName,
                        playerCount = msg.PlayerCount,
                        maxPlayerCount = msg.MaxPlayerCount,
                        lastUpdated = _now
                    });
                }
            }
            else
            {
                logger.LogError("Could not get address for server");
                sender.Send(new Failed
                {
                    messageName = nameof(AddServer),
                    reason = "Could not get address for server",
                });
            }
        }

        void UpdateServerHandler(INetworkPlayer sender, UpdateServer msg)
        {
            if (_servers.TryGetValue(sender.Connection.EndPoint, out Server item))
            {
                Helper.UpdateIfNotNull(ref item.displayName, msg.displayName);
                Helper.UpdateIfNotNull(ref item.playerCount, msg.PlayerCount);
                Helper.UpdateIfNotNull(ref item.maxPlayerCount, msg.MaxPlayerCount);
                item.lastUpdated = _now;
            }
            else
            {
                sender.Send(new Failed
                {
                    messageName = nameof(UpdateServer),
                    reason = "Server not in list",
                });
            }
        }

        void KeepAliveHandler(INetworkPlayer sender, KeepAlive _)
        {
            if (_servers.TryGetValue(sender.Connection.EndPoint, out Server item))
            {
                item.lastUpdated = _now;
            }
            else
            {
                sender.Send(new Failed
                {
                    messageName = nameof(KeepAlive),
                    reason = "Server not in list",
                });
            }
        }

        void RemoveServerHandler(INetworkPlayer sender, RemoveServer _)
        {
            _servers.Remove(sender.Connection.EndPoint);
        }

        void GetServersHandler(INetworkPlayer sender, GetServers _)
        {
            sender.Send(new GetServersReply
            {
                // todo optimzie
                // todo add max
                servers = _servers.Values.Select(x => new GetServersReply.Server
                {
                    address = x.address,
                    port = x.port,
                    displayName = x.displayName,
                    playerCount = x.playerCount,
                    maxPlayerCount = x.maxPlayerCount
                }).ToArray()
            });
        }

        public class Server
        {
            public readonly string address;
            public int port;

            public string displayName;
            public int playerCount;
            public int maxPlayerCount;

            public DateTime lastUpdated;

            public Server(string address)
            {
                this.address = address;
            }
        }
    }
    public static class Helper
    {
        public static void UpdateIfNotNull(ref string field, string value)
        {
            if (!string.IsNullOrEmpty(value))
                field = value;
        }
        public static void UpdateIfNotNull<T>(ref T field, T value) where T : class
        {
            if (value != null)
                field = value;
        }
        public static void UpdateIfNotNull<T>(ref T field, T? value) where T : struct
        {
            if (value.HasValue)
                field = value.Value;
        }


        public static bool GetIPAddress(IEndPoint endPoint, out IPAddress address)
        {
            if (endPoint is EndPointWrapper wrapper)
            {
                if (wrapper.inner is IPEndPoint ipEndPoint)
                {
                    address = ipEndPoint.Address;
                    return true;
                }
            }

            address = null;
            return false;
        }
    }
}

namespace Mirage.ListServer
{
    /// <summary>
    /// Request to be added to list
    /// </summary>
    [NetworkMessage]
    struct AddServer
    {
        public string displayName;
        [BitCount(16)] public int port;
        public int PlayerCount;
        public int MaxPlayerCount;
    }

    /// <summary>
    /// Request to be added to list
    /// </summary>
    [NetworkMessage]
    struct UpdateServer
    {
        public string displayName;
        public int? PlayerCount;
        public int? MaxPlayerCount;
    }

    [NetworkMessage]
    struct KeepAlive
    {

    }

    [NetworkMessage]
    struct RemoveServer
    {
    }

    [NetworkMessage]
    struct GetServers
    {
    }

    [NetworkMessage]
    public struct GetServersReply
    {
        public Server[] servers;

        public struct Server
        {
            public string address;
            [BitCount(16)] public int port;

            public string displayName;
            public int playerCount;
            public int maxPlayerCount;
        }
    }

    [NetworkMessage]
    struct Failed
    {
        public string messageName;
        public string reason;
    }
}

namespace Mirage.ListServer.Server
{
    public class ListServer
    {
        static readonly ILogger logger = LogFactory.GetLogger<ListServer>();

        readonly INetworkPlayer _masterServer;
        readonly int _timeoutSeconds;
        DateTime _nextUpdated;

        bool _added;

        public ListServer(MessageHandler messageHandler, INetworkPlayer masterServer, int timeoutSeconds)
        {
            _masterServer = masterServer;
            _timeoutSeconds = timeoutSeconds;
            messageHandler.RegisterHandler<Failed>(FailedHandler);
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            if (now > _nextUpdated)
            {
                _nextUpdated = now + TimeSpan.FromSeconds(_timeoutSeconds / 5);
                _masterServer.Send(new KeepAlive(), Channel.Unreliable);
            }
        }

        void FailedHandler(INetworkPlayer sender, Failed msg)
        {
            logger.LogError($"{msg.messageName} failed: {msg.reason}");

            if (msg.messageName == nameof(Mirage.ListServer.AddServer))
            {
                _added = false;
            }
        }


        public void AddServer()
        {
            _added = true;
            _masterServer.Send(new AddServer()
            {

            });
            throw new NotImplementedException();
        }

        public void UpdateServer()
        {
            _masterServer.Send(new UpdateServer()
            {

            });
            throw new NotImplementedException();
        }

        public void RemoveServer()
        {
            _added = false;
            _masterServer.Send(new RemoveServer());
        }
    }
}

namespace Mirage.ListServer.Client
{
    public class ListServer
    {
        static readonly ILogger logger = LogFactory.GetLogger<ListServer>();

        INetworkPlayer _masterServer;

        public GetServersReply.Server[] Servers { get; private set; }
        public event Action OnListUpdated;

        public ListServer(MessageHandler messageHandler, INetworkPlayer masterServer)
        {
            _masterServer = masterServer;
            messageHandler.RegisterHandler<GetServersReply>(GetServersReplyHandler);
        }

        public void GetServerList()
        {
            _masterServer.Send(new GetServers());
        }

        void GetServersReplyHandler(INetworkPlayer sender, GetServersReply msg)
        {
            Servers = msg.servers;
            OnListUpdated.Invoke();
        }
    }
}
