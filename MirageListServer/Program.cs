using System;
using Mirage.Logging;
using UnityEngine;

namespace Mirage.ListServer.MasterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Mirage List Server using Mirage Standalone {Version.Current}.");

            InitializeReadWrite.RunMethods();
            ConfigureLog();
            var listServer = new Runner();
            listServer.Run();
        }

        static void ConfigureLog()
        {
            Debug.unityLogger = new StandaloneLogger();
            LogFactory.GetLogger<SocketLayer.Peer>().filterLogType = LogType.Log;
            LogFactory.GetLogger<NetworkTime>().filterLogType = LogType.Log;
        }
    }
}
