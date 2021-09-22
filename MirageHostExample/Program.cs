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
            TaskRunner = Task.Run(Run);
        }

        private static async void Run()
        {
            Server.StartServer();
            Client.Connect("localhost");

            while (true)
            {
                Server.Update();
                Client.Update();

                await Task.Delay(5);
            }
        }
    }
}
