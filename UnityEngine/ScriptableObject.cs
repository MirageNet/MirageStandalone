using System;

namespace UnityEngine
{
    public class ScriptableObject : Object
    {
        protected ScriptableObject() => throw new NotSupportedException();

        public static object CreateInstance<T>() => throw new NotSupportedException();
    }
}
