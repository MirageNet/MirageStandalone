using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
    public class AsyncOperation
    {
        public bool allowSceneActivation { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public TaskAwaiter GetAwaiter() => throw new NotSupportedException();
    }
}
