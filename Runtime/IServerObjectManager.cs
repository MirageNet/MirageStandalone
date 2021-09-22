//using System;

//namespace Mirage
//{
//    public interface IServerObjectManager
//    {
//        void AddCharacter(INetworkPlayer player, Unity character);
//        void AddCharacter(INetworkPlayer player, Unity character, Guid assetId);
//        void AddCharacter(INetworkPlayer player, NetworkIdentity identity);

//        void ReplaceCharacter(INetworkPlayer player, Unity character, bool keepAuthority = false);
//        void ReplaceCharacter(INetworkPlayer player, Unity character, Guid assetId, bool keepAuthority = false);
//        void ReplaceCharacter(INetworkPlayer player, NetworkIdentity identity, bool keepAuthority = false);

//        void Spawn(Unity obj, INetworkPlayer owner = null);
//        void Spawn(Unity obj, Unity ownerObject);
//        void Spawn(Unity obj, Guid assetId, INetworkPlayer owner = null);
//        void Spawn(NetworkIdentity identity);
//        void Spawn(NetworkIdentity identity, INetworkPlayer owner);

//        void Destroy(Unity obj, bool destroyServerObject = true);

//        void SpawnObjects();
//    }
//}
