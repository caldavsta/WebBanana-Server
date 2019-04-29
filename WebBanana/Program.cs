using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebBanana
{
    public class Program
    {
        // Application arguments in order
        public static string VoiceMeeterDLL = "C:\\Program Files(x86)\\VB\\Voicemeeter\\VoicemeeterRemote64.dll";
        public static string ipAndPort = "http://192.168.1.43:5090/";

        static volatile Stopwatch StayAliveTimer;
        static readonly long StayAliveLimit = 2000;

        public static void Main(string[] args)
        {
            Thread StdInOut = new Thread(StdInOutThread);
            StdInOut.Start();

            StayAliveTimer = new Stopwatch();
            StayAliveTimer.Start();

            Thread StayAlive = new Thread(StayAliveThread);
            StayAlive.Start();

            if (args != null)
            {
                BuildWebHost().Run();
            } else
            {
                VoiceMeeterDLL = args[0];
                ipAndPort = args[1];
                BuildWebHost().Run();
            }
        }

        public static void StayAliveThread()
        {
            while (true)
            {
                if (StayAliveTimer.ElapsedMilliseconds > StayAliveLimit)
                {
                    QuitApplication();
                }
            }
        }

        public static void StdInOutThread()
        {

            using (StreamReader stdIn = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            using (StreamWriter stdOut = new StreamWriter(Console.OpenStandardOutput(), Console.InputEncoding))
            {
                string line;

                while ((line = stdIn.ReadLine()).Length > 0)
                {
                    Console.WriteLine(line);
                    if (line.Equals("Quit"))
                    {
                        stdOut.Write("Exiting Application");
                        QuitApplication();
                    }
                    if (line.Equals("StayAlive"))
                    {
                        stdOut.Write("Staying Alive");
                        StayAliveTimer.Restart();
                    }
                    stdOut.Write(line);
                }
            }
        }

        public static void QuitApplication()
        {
            Environment.Exit(-1);
        }

        public static IWebHost BuildWebHost() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
            .UseUrls(ipAndPort)
                .Build();
    }
}
