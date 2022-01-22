using System;
using Mirage.Logging;
using UnityEngine;

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
