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

        // needs to do nothing for standalone
        public static bool runInBackground { get; set; }

        public static event Action quitting;

        public static void InvokeQuitting()
        {
            quitting?.Invoke();
        }
    }
}
