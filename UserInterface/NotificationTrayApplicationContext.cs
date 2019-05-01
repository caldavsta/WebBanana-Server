using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserInterface.Properties;

namespace UserInterface
{
    public class NotificationTrayApplicationContext : ApplicationContext
    {

        private NotifyIcon trayIcon;
        private ServerInterfaceForm interfaceForm;
        private Thread stayAliveThread;
        private readonly long stayAliveWait = 1000;
        private Process serverProcess;
        string serverProcessOutput = string.Empty;
        bool serverRunning = false;
        public NotificationTrayApplicationContext()
        {
            ShowTrayIcon();
            StartServerProcess();
        }

        void ShowTrayIcon()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.SystemTrayIcon,
                ContextMenu = new ContextMenu(GetContextMenu()),
                Visible = true
            };
        }
         
        void HideTrayIcon()
        {
            trayIcon.Visible = false;
            trayIcon = null;
        }

        void ContextExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            if (serverRunning)
            {
                StopServerProcess();
            }
            HideTrayIcon();
            Application.Exit();
        }

        public void StartServerProcess()
        {
            //build the correct relative path to the .dll
            String pathToDLL = Process.GetCurrentProcess().MainModule.FileName.Replace(@"UserInterface\bin\Debug\UserInterface.exe", @"\WebBanana\bin\Debug\netcoreapp2.0\WebBanana.dll");

            serverProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = pathToDLL,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }

            };

            serverProcess.OutputDataReceived += ServerProcessOutputRecieved;
            serverProcess.ErrorDataReceived += ServerProcessOutputRecieved;

            serverProcess.Start();
            serverProcess.BeginOutputReadLine();
            serverProcess.BeginErrorReadLine();

            stayAliveThread = new Thread(StayAlive);
            stayAliveThread.Start();

            serverRunning = true;
            trayIcon.Icon = Resources.SystemTrayIconRunning;
            serverProcessOutput = string.Empty;

            trayIcon.ContextMenu = new ContextMenu(GetContextMenu());
            SetInterfaceButtonStatus();
        }

        public void StopServerProcess()
        {
            if (serverProcess != null)
            {
                serverProcess.StandardInput.WriteLine("Quitting Process");
                serverRunning = false;

                stayAliveThread.Abort();
                stayAliveThread = null;

                serverProcess.OutputDataReceived -= ServerProcessOutputRecieved;
                serverProcess.ErrorDataReceived -= ServerProcessOutputRecieved;

                serverProcess = null;

                trayIcon.ContextMenu = new ContextMenu(GetContextMenu());
                trayIcon.Icon = Resources.SystemTrayIcon;
                SetInterfaceButtonStatus();
            }

        }

        private void StayAlive()
        {
            Stopwatch aliveStopwatch;
            aliveStopwatch = new Stopwatch();
            aliveStopwatch.Start();
            while (true)
            {
                if (aliveStopwatch.ElapsedMilliseconds > stayAliveWait)
                {
                    sendStayAliveMessage();
                    aliveStopwatch.Restart();
                }
            }
        }

        private void sendStayAliveMessage()
        {
            if (serverProcess != null)
            {
                serverProcess.StandardInput.WriteLine("StayAlive");
            }
        }

        void ServerProcessOutputRecieved(object sender, DataReceivedEventArgs args)
        {
            if (args.Data != null && interfaceForm != null)
            {
                try
                {
                    //run code on ui thread
                    interfaceForm.Invoke((MethodInvoker)delegate {
                        interfaceForm.ConsoleOutputTextBox.AppendText(args.Data);
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }


            }
        }

        MenuItem[] GetContextMenu()
        {
            MenuItem[] result = new MenuItem[3];
            result[0] = new MenuItem("Show Interface", Context_ShowInterface);

            if (serverRunning)
            {
                result[1] = new MenuItem("Stop Server", Context_ToggleServer);
            } else
            {
                result[1] = new MenuItem("Start Server", Context_ToggleServer);
            }

                result[2] = new MenuItem("Exit", ContextExit);
            return result;
        }

        void Context_ShowInterface(object sender, EventArgs e)
        {
            if (interfaceForm == null)
            {
                interfaceForm = new ServerInterfaceForm(this);
                interfaceForm.Closed += interfaceForm_Closed;
                interfaceForm.Show();
            }
            else { interfaceForm.Activate(); }
            //run code on ui thread
            interfaceForm.Invoke((MethodInvoker)delegate {
                interfaceForm.ConsoleOutputTextBox.AppendText(serverProcessOutput);
                interfaceForm.StartServerButton.Enabled = !serverRunning;
                interfaceForm.StopServerButton.Enabled = serverRunning;
            });
        }

        

        void Context_ToggleServer(object sender, EventArgs args)
        {
            if (serverRunning)
            {
                StopServerProcess();
            } else
            {
                StartServerProcess();
            }

            SetInterfaceButtonStatus();
        }

        void SetInterfaceButtonStatus()
        {
            if (interfaceForm != null)
            {
                //set buttons
                interfaceForm.Invoke((MethodInvoker)delegate
                {
                    interfaceForm.StartServerButton.Enabled = !serverRunning;
                    interfaceForm.StopServerButton.Enabled = serverRunning;
                });
            }
        }

        void interfaceForm_Closed(object sender, EventArgs e)
        {
            interfaceForm = null;
        }

    }

}
