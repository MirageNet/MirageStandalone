using System;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    public static class SceneManager
    {
        public static int sceneCount => throw new NotSupportedException();

        public static Scene GetActiveScene() => throw new NotSupportedException();
        public static Scene GetSceneAt(int sceneIndex) => throw new NotSupportedException();

        public static AsyncOperation LoadSceneAsync(string scenePath, LoadSceneParameters value) => throw new NotSupportedException();
        public static AsyncOperation LoadSceneAsync(string scenePath) => throw new NotSupportedException();
        public static AsyncOperation LoadSceneAsync(string scenePath, LoadSceneMode mode) => throw new NotSupportedException();

        public static Scene GetSceneByName(string sceneName) => throw new NotSupportedException();
        public static Scene GetSceneByPath(string scenePath) => throw new NotSupportedException();

        public static AsyncOperation UnloadSceneAsync(Scene scene) => UnloadSceneAsync(scene, UnloadSceneOptions.None);
        public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options) => throw new NotSupportedException();

        public static AsyncOperation UnloadSceneAsync(string scenePath) => UnloadSceneAsync(scenePath, UnloadSceneOptions.None);
        public static AsyncOperation UnloadSceneAsync(string scenePath, UnloadSceneOptions options) => throw new NotSupportedException();
    }
}
