using System;

namespace Mirage.Examples.RpcExample
{
    public class SyncVarBehaviour : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnHealthChanged), invokeHookOnServer = true)]
        public int Health;

        public event Action<int, int> OnHealthChanged;

        [ServerRpc]
        public void SetHealth(int health)
        {
            Health = health;
        }
    }
}
