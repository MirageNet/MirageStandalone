using System;

namespace UnityEngine
{
    public class Attributes
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class RuntimeInitializeOnLoadMethodAttribute : Attribute
        {
            public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
            {

            }
        }
    }
}
