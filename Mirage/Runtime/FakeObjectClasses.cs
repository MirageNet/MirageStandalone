using System;
using UnityEngine;

namespace Mirage
{
    public class NetworkIdentity : MonoBehaviour
    {
        private NetworkIdentity() => throw new NotSupportedException();

        public ServerObjectManager ServerObjectManager => throw new NotSupportedException();
        public uint NetId => throw new NotSupportedException();

        public void RemoveObserverInternal(NetworkPlayer _) => throw new NotSupportedException();
    }
    public class ServerObjectManager
    {
        private ServerObjectManager() => throw new NotSupportedException();

        internal void Destroy(NetworkIdentity _) => throw new NotSupportedException();
        internal void Destroy(GameObject _) => throw new NotSupportedException();
    }
    public class SyncVarSender
    {
        internal SyncVarSender() { }

        internal void Update() { }
    }
}
