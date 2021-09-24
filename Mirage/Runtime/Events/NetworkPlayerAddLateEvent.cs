using System;

namespace Mirage.Events
{
    [Serializable] 
    public class NetworkPlayerEvent : Action<INetworkPlayer> { }

    /// <summary>
    /// Event fires from a <see cref="NetworkClient">NetworkClient</see> or <see cref="NetworkServer">NetworkServer</see> during a new connection, a new authentication, or a disconnection.
    /// </summary>
    [Serializable] public class NetworkPlayerAddLateEvent : Action<INetworkPlayer, NetworkPlayerEvent> { }
}
