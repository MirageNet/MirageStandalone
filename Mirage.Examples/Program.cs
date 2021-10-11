using System;
using Mirage;
using Mirage.Examples;

namespace MirageHostExample
{
    [NetworkMessage]
    public struct HelloMessage { }

    internal static class Program
    {
        static void Main(string[] args)
        {
            var perf = new HeadlessPerformance(args);

            Console.ReadLine();
        }
    }
}
