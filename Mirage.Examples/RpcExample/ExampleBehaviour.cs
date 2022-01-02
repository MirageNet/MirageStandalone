using UnityEngine;

namespace Mirage.Examples.RpcExample
{
    public class ExampleBehaviour : NetworkBehaviour
    {
        [ClientRpc]
        public void HelloWorld()
        {
            Debug.Log("Hello world");
        }
    }
}
