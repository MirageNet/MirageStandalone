using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Mirage.Logging;
using Mirage.SocketLayer;
using Mirage.Sockets.Udp;
using UnityEngine;

namespace Mirage.ListServer.MasterServer
{
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
                if (logger.LogEnabled())
                {
                    string log = $"{typeof(T).Name} from {p}";
                    System.Reflection.FieldInfo[] fields = typeof(T).GetFields();
                    if (fields.Length > 0)
                    {
                        log += "\n  {\n";
                        foreach (System.Reflection.FieldInfo fieldInfo in fields)
                        {
                            string name = fieldInfo.Name;
                            object value = fieldInfo.GetValue(msg);
                            log += $"    \"{name}\": \"{value}\",\n";
                        }
                        log += "  }";
                    }
                    logger.Log(log);
                }

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
                DateTime time = kvp.Value.LastUpdated;
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
                    item.Port = msg.Port;
                    item.DisplayName = msg.DisplayName;
                    item.PlayerCount = msg.PlayerCount;
                    item.MaxPlayerCount = msg.MaxPlayerCount;
                    item.LastUpdated = _now;
                    item.MergeData(msg.ServerData);
                }
                else
                {
                    _servers.Add(endPoint, new Server(address.ToString(), msg.ServerData)
                    {
                        Port = msg.Port,
                        DisplayName = msg.DisplayName,
                        PlayerCount = msg.PlayerCount,
                        MaxPlayerCount = msg.MaxPlayerCount,
                        LastUpdated = _now
                    });
                }
            }
            else
            {
                logger.LogError("Could not get address for server");
                sender.Send(new Failed
                {
                    MessageName = nameof(AddServer),
                    Reason = "Could not get address for server",
                });
            }
        }

        void UpdateServerHandler(INetworkPlayer sender, UpdateServer msg)
        {
            if (_servers.TryGetValue(sender.Connection.EndPoint, out Server item))
            {
                Helper.UpdateIfNotNull(ref item.DisplayName, msg.DisplayName);
                Helper.UpdateIfNotNull(ref item.PlayerCount, msg.PlayerCount);
                Helper.UpdateIfNotNull(ref item.MaxPlayerCount, msg.MaxPlayerCount);
                item.LastUpdated = _now;
                item.MergeData(msg.ServerData);
            }
            else
            {
                sender.Send(new Failed
                {
                    MessageName = nameof(UpdateServer),
                    Reason = "Server not in list",
                });
            }
        }

        void KeepAliveHandler(INetworkPlayer sender, KeepAlive _)
        {
            if (_servers.TryGetValue(sender.Connection.EndPoint, out Server item))
            {
                item.LastUpdated = _now;
            }
            else
            {
                sender.Send(new Failed
                {
                    MessageName = nameof(KeepAlive),
                    Reason = "Server not in list",
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
                    address = x.Address,
                    port = x.Port,
                    DisplayName = x.DisplayName,
                    PlayerCount = x.PlayerCount,
                    MaxPlayerCount = x.MaxPlayerCount,
                    ServerData = x.ServerData,
                }).ToArray()
            });
        }

        public class Server
        {
            public readonly string Address;
            public int Port;

            public string DisplayName;
            public int PlayerCount;
            public int MaxPlayerCount;

            public DateTime LastUpdated;

            private Dictionary<string, string> _serverData;

            public IReadOnlyDictionary<string, string> ServerData;

            public Server(string address, Dictionary<string, string> serverData = null)
            {
                Address = address;
                _serverData = serverData;
            }

            public void MergeData(Dictionary<string, string> newData)
            {
                // todo validata data size (limit to 5-10 data per server)

                if (newData == null || newData.Count == 0)
                    return;

                // if current is null, we can just use reference of newData
                if (_serverData == null)
                {
                    _serverData = newData;
                    return;
                }

                foreach (KeyValuePair<string, string> data in newData)
                {
                    // use indexer here instead of add
                    // we want to overwrite any old values if they exist
                    _serverData[data.Key] = data.Value;
                }
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

        public static bool GetIPEndPoint(IEndPoint endPoint, out IPEndPoint ipEndPoint)
        {
            if (endPoint is EndPointWrapper wrapper)
            {
                if (wrapper.inner is IPEndPoint ip)
                {
                    ipEndPoint = ip;
                    return true;
                }
            }

            ipEndPoint = null;
            return false;
        }
        public static bool GetIPAddress(IEndPoint endPoint, out IPAddress address)
        {
            if (GetIPEndPoint(endPoint, out IPEndPoint ipEndPoint))
            {
                address = ipEndPoint.Address;
                return true;
            }

            address = null;
            return false;
        }
    }
}
