using UnityEngine;

namespace Mirage.Examples.RpcExample
{
    public class RpcBehaviour : NetworkBehaviour
    {
        [ClientRpc]
        public void HelloWorld()
        {
            Debug.Log("Hello world");
        }
    }
}
