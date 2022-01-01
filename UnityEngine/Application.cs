using System;

namespace UnityEngine
{
    public enum RuntimePlatform
    {
        None,
        WindowsEditor,
        WindowsPlayer,
        WebGLPlayer,
        LinuxPlayer,
        OSXPlayer,
    }
    public static class Application
    {
        // todo set this for windows or linux
        public static RuntimePlatform platform { get; } = RuntimePlatform.WindowsPlayer;
        public static bool isEditor => platform == RuntimePlatform.WindowsEditor;

        public static event Action quitting;
    }
}
