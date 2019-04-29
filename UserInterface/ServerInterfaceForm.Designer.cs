namespace UserInterface
{
    partial class ServerInterfaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonStartServer = new System.Windows.Forms.Button();
            this.consoleOutputTextBox = new System.Windows.Forms.TextBox();
            this.ButtonStopServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonStartServer
            // 
            this.ButtonStartServer.Location = new System.Drawing.Point(12, 12);
            this.ButtonStartServer.Name = "ButtonStartServer";
            this.ButtonStartServer.Size = new System.Drawing.Size(106, 23);
            this.ButtonStartServer.TabIndex = 0;
            this.ButtonStartServer.Text = "Start Server";
            this.ButtonStartServer.UseVisualStyleBackColor = true;
            this.ButtonStartServer.Click += new System.EventHandler(this.StartServerButtonPressed);
            // 
            // consoleOutputTextBox
            // 
            this.consoleOutputTextBox.Location = new System.Drawing.Point(124, 12);
            this.consoleOutputTextBox.Multiline = true;
            this.consoleOutputTextBox.Name = "consoleOutputTextBox";
            this.consoleOutputTextBox.Size = new System.Drawing.Size(664, 426);
            this.consoleOutputTextBox.TabIndex = 2;
            // 
            // StopServerButton
            // 
            this.ButtonStopServer.Location = new System.Drawing.Point(12, 41);
            this.ButtonStopServer.Name = "StopServerButton";
            this.ButtonStopServer.Size = new System.Drawing.Size(106, 23);
            this.ButtonStopServer.TabIndex = 3;
            this.ButtonStopServer.Text = "Stop Server";
            this.ButtonStopServer.UseVisualStyleBackColor = true;
            this.ButtonStopServer.Click += new System.EventHandler(this.StopServerButtonPressed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ButtonStopServer);
            this.Controls.Add(this.consoleOutputTextBox);
            this.Controls.Add(this.ButtonStartServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStartServer;
        private System.Windows.Forms.TextBox consoleOutputTextBox;
        private System.Windows.Forms.Button ButtonStopServer;
    }
}

