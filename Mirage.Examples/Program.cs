using System;
using System.Linq;
using System.Reflection;
using Mirage;
using Mirage.Examples;
using UnityEngine;

namespace MirageHostExample
{
    [NetworkMessage]
    public struct HelloMessage {}

    internal class Program
    {
        static void Main(string[] args)
        {
            var asm = Assembly.GetExecutingAssembly();

            MethodInfo[] methods = asm.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false).Length > 0)
                .ToArray();

            foreach (MethodInfo method in methods)
            {
                method.Invoke(null, null);
            }

            while (true)
            {

                Console.WriteLine("..........Mirage Examples.........\r");
                Console.WriteLine("Please choose one...\n");

                Console.WriteLine("Option 1: headless Server.");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:

                        Console.WriteLine("Please type in command to fire headless example. type -help for info");

                        string command = Console.ReadLine();

                        if (!string.IsNullOrEmpty(command) && command.Contains("-help"))
                        {
                            Console.WriteLine("--==Mirage HeadlessClients Benchmark==--");
                            Console.WriteLine(
                                "Please start your standalone application with the -nographics and -batchmode options");
                            Console.WriteLine("Also provide these arguments to control the autostart process:");
                            Console.WriteLine("-server (will run in server only mode)");
                            Console.WriteLine("-client 1234 (will run the specified number of clients)");
                            Console.WriteLine(
                                "-transport tcp (transport to be used in test. add more by editing HeadlessBenchmark.cs)");
                            Console.WriteLine("-address example.com (will run the specified number of clients)");
                            Console.WriteLine("-port 1234 (port used by transport)");
                            Console.WriteLine("-monster 100 (number of monsters to spawn on the server)");

                            goto case 1;
                        }

                        var headless = new HeadlessPerformance(command);

                        break;
                    default:
                        Console.WriteLine("Incorrect option selected.");
                        break;
                }
            }
        }
    }
}
