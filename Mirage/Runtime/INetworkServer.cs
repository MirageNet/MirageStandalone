using System;
using System.Collections.Generic;

namespace Mirage
{
    public interface INetworkServer
    {
        /// <summary>
        /// This is invoked when a server is started - including when a host is started.
        /// </summary>
        Action Started { get; }

        /// <summary>
        /// Event fires once a new Client has connect to the Server.
        /// </summary>
        Action<INetworkPlayer> Connected { get; }

        /// <summary>
        /// Event fires once a new Client has passed Authentication to the Server.
        /// </summary>
        Action<INetworkPlayer> Authenticated { get; }

        /// <summary>
        /// Event fires once a Client has Disconnected from the Server.
        /// </summary>
        Action<INetworkPlayer> Disconnected { get; }

        Action Stopped { get; }

        /// <summary>
        /// This is invoked when a host is started.
        /// <para>StartHost has multiple signatures, but they all cause this hook to be called.</para>
        /// </summary>
        Action OnStartHost { get; }

        /// <summary>
        /// This is called when a host is stopped.
        /// </summary>
        Action OnStopHost { get; }

        /// <summary>
        /// The connection to the host mode client (if any).
        /// </summary>
        INetworkPlayer LocalPlayer { get; }

        /// <summary>
        /// The host client for this server 
        /// </summary> 
        INetworkClient LocalClient { get; }

        /// <summary>
        /// True if there is a local client connected to this server (host mode)
        /// </summary>
        bool LocalClientActive { get; }

        /// <summary>
        /// <para>Checks if the server has been started.</para>
        /// <para>This will be true after NetworkServer.Listen() has been called.</para>
        /// </summary>
        bool Active { get; }

        NetworkWorld World { get; }

        SyncVarSender SyncVarSender { get; }

        IReadOnlyCollection<INetworkPlayer> Players { get; }

        void Stop();

        void AddConnection(INetworkPlayer player);

        void RemoveConnection(INetworkPlayer player);

        void SendToAll<T>(T msg, int channelId = Channel.Reliable);
    }
}
