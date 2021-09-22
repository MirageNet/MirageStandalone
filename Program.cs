using Mirage;
using System.Threading;

namespace MirageStandalone
{
    class Program
    {
        static NetworkServer server = new NetworkServer();
        static NetworkClient client = new NetworkClient();

        static void Main(string[] args)
        {
            server.StartServer();
            client.Connect("localhost");

            while (true)
            {
                server.Update();
                client.Update();


                if (client.IsConnected)
                {
                    //client.Send(new ArraySegment<byte>(new byte[] { 1, 2, 3, 4, 5 }));
                }
                Thread.Sleep(5);
            }
        }
    }
}
