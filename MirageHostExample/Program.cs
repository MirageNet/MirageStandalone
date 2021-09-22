using System.Threading;
using System.Threading.Tasks;
using Mirage;

namespace MirageHostExample
{
    internal class Program
    {
        static readonly NetworkServer Server = new();
        static readonly NetworkClient Client = new();
        static Task TaskRunner;

        static void Main(string[] args)
        {
            Server.StartServer();
            Client.Connect("localhost");

            TaskRunner = Task.Run(Run);
        }

        private static async void Run()
        {
            while (true)
            {
                Server.Update();
                Client.Update();

                await Task.Delay(5);
            }
        }
    }
}
