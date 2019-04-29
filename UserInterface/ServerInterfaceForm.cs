using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class ServerInterfaceForm : Form
    {
        NotificationTrayApplicationContext context;
        public ServerInterfaceForm(NotificationTrayApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
        }
        
        public TextBox ConsoleOutputTextBox
        {
            get { return consoleOutputTextBox; }
        }

        public Button StartServerButton
        {
            get { return ButtonStartServer; }
        }

        public Button StopServerButton
        {
            get { return ButtonStopServer; }
        }

        private void StartServerButtonPressed(object sender, EventArgs e)
        {
            context.StartServerProcess();
        }

        private void StopServerButtonPressed(object sender, EventArgs e)
        {
            context.StopServerProcess();
        }
    }
}
