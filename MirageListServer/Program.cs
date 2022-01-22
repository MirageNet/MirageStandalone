using Mirage.Logging;
using UnityEngine;

namespace Mirage.ListServer.MasterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeReadWrite.RunMethods();
            ConfigureLog();
            var listServer = new Runner();
            listServer.Run();
        }

        static void ConfigureLog()
        {
            Debug.unityLogger = new StandaloneLogger();
            LogFactory.GetLogger<SocketLayer.Peer>().filterLogType = LogType.Warning;
            LogFactory.GetLogger<NetworkTime>().filterLogType = LogType.Warning;
        }
    }
}
