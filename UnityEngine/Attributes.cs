using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RuntimeInitializeOnLoadMethodAttribute : Attribute {
        public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType) {

        }
    }

    public enum RuntimeInitializeLoadType {
        BeforeSceneLoad
    };

}
