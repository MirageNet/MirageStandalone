using System;
using System.Collections.Generic;
using Mirage.Logging;
using UnityEngine;


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
            logger.LogError($"{msg.MessageName} failed: {msg.Reason}");

            if (msg.MessageName == nameof(Mirage.ListServer.AddServer))
            {
                _added = false;
            }
        }

        public void AddServer(string displayName, int playerCount, int maxPlayers, int port, Dictionary<string, string> serverData = null)
        {
            _added = true;
            _masterServer.Send(new AddServer
            {
                DisplayName = displayName,
                PlayerCount = playerCount,
                MaxPlayerCount = maxPlayers,
                Port = port,
                ServerData = serverData,
            });
        }

        public void UpdateServer(string name = null, int? playerCount = null, Dictionary<string, string> changedData = null)
        {
            _masterServer.Send(new UpdateServer()
            {
                DisplayName = name,
                PlayerCount = playerCount,
                ServerData = changedData,
            });
        }

        public void RemoveServer()
        {
            _added = false;
            _masterServer.Send(new RemoveServer());
        }
    }
}
