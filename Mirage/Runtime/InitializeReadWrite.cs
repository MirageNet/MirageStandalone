using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Mirage
{
    public static class InitializeReadWrite
    {
        public static void RunMethods()
        {
            var asm = Assembly.GetEntryAssembly();

            MethodInfo[] methods = asm.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false).Length > 0)
                .ToArray();

            foreach (MethodInfo method in methods)
            {
                method.Invoke(null, null);
            }
        }
    }
}
