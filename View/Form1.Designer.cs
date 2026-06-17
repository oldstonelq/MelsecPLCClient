namespace PLCClient.View
{
    partial class Form1
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
            this.btn_PLCInformationMemoryView = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.gb_ServiceInformation = new System.Windows.Forms.GroupBox();
            this.btn_CloseServer = new System.Windows.Forms.Button();
            this.btn_OpenServer = new System.Windows.Forms.Button();
            this.cmb_ServiceInformationCount = new System.Windows.Forms.ComboBox();
            this.tb_ServiceInformationPort = new System.Windows.Forms.TextBox();
            this.tb_ServiceInformationIP = new System.Windows.Forms.TextBox();
            this.lab_ServiceInformationCount = new System.Windows.Forms.Label();
            this.lab_ServiceInformationPort = new System.Windows.Forms.Label();
            this.lab_ServiceInformationIP = new System.Windows.Forms.Label();
            this.tb_PLCInformationPort = new System.Windows.Forms.TextBox();
            this.tb_PLCInformationIP = new System.Windows.Forms.TextBox();
            this.lab_PLCInformationPort = new System.Windows.Forms.Label();
            this.lab_PLCInformationIP = new System.Windows.Forms.Label();
            this.btn_Trigger = new System.Windows.Forms.Button();
            this.btn_ServiceInformationMemoryView = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.gb_ServiceInformationFunction = new System.Windows.Forms.GroupBox();
            this.gb_PLCInformation = new System.Windows.Forms.GroupBox();
            this.gb_ConnectStatus = new System.Windows.Forms.GroupBox();
            this.gb_PLCInformationFunction = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gb_ConnectionStatus = new System.Windows.Forms.GroupBox();
            this.lab_ConnectStatus = new System.Windows.Forms.Label();
            this.lab_ConnectPort = new System.Windows.Forms.Label();
            this.gb_ServiceInformation.SuspendLayout();
            this.gb_ServiceInformationFunction.SuspendLayout();
            this.gb_PLCInformation.SuspendLayout();
            this.gb_ConnectStatus.SuspendLayout();
            this.gb_PLCInformationFunction.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_PLCInformationMemoryView
            // 
            this.btn_PLCInformationMemoryView.Location = new System.Drawing.Point(2, 20);
            this.btn_PLCInformationMemoryView.Name = "btn_PLCInformationMemoryView";
            this.btn_PLCInformationMemoryView.Size = new System.Drawing.Size(95, 71);
            this.btn_PLCInformationMemoryView.TabIndex = 1;
            this.btn_PLCInformationMemoryView.Text = "MemoryView";
            this.btn_PLCInformationMemoryView.UseVisualStyleBackColor = true;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(6, 60);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(107, 33);
            this.btn_Connect.TabIndex = 7;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            // 
            // gb_ServiceInformation
            // 
            this.gb_ServiceInformation.Controls.Add(this.btn_CloseServer);
            this.gb_ServiceInformation.Controls.Add(this.btn_OpenServer);
            this.gb_ServiceInformation.Controls.Add(this.cmb_ServiceInformationCount);
            this.gb_ServiceInformation.Controls.Add(this.tb_ServiceInformationPort);
            this.gb_ServiceInformation.Controls.Add(this.tb_ServiceInformationIP);
            this.gb_ServiceInformation.Controls.Add(this.lab_ServiceInformationCount);
            this.gb_ServiceInformation.Controls.Add(this.lab_ServiceInformationPort);
            this.gb_ServiceInformation.Controls.Add(this.lab_ServiceInformationIP);
            this.gb_ServiceInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ServiceInformation.Location = new System.Drawing.Point(3, 3);
            this.gb_ServiceInformation.Name = "gb_ServiceInformation";
            this.gb_ServiceInformation.Size = new System.Drawing.Size(275, 99);
            this.gb_ServiceInformation.TabIndex = 0;
            this.gb_ServiceInformation.TabStop = false;
            this.gb_ServiceInformation.Text = "Service Information";
            // 
            // btn_CloseServer
            // 
            this.btn_CloseServer.Location = new System.Drawing.Point(162, 60);
            this.btn_CloseServer.Name = "btn_CloseServer";
            this.btn_CloseServer.Size = new System.Drawing.Size(102, 33);
            this.btn_CloseServer.TabIndex = 7;
            this.btn_CloseServer.Text = "Close Server";
            this.btn_CloseServer.UseVisualStyleBackColor = true;
            // 
            // btn_OpenServer
            // 
            this.btn_OpenServer.Location = new System.Drawing.Point(10, 60);
            this.btn_OpenServer.Name = "btn_OpenServer";
            this.btn_OpenServer.Size = new System.Drawing.Size(107, 33);
            this.btn_OpenServer.TabIndex = 6;
            this.btn_OpenServer.Text = "Open Server";
            this.btn_OpenServer.UseVisualStyleBackColor = true;
            // 
            // cmb_ServiceInformationCount
            // 
            this.cmb_ServiceInformationCount.FormattingEnabled = true;
            this.cmb_ServiceInformationCount.Location = new System.Drawing.Point(206, 33);
            this.cmb_ServiceInformationCount.Name = "cmb_ServiceInformationCount";
            this.cmb_ServiceInformationCount.Size = new System.Drawing.Size(58, 20);
            this.cmb_ServiceInformationCount.TabIndex = 5;
            // 
            // tb_ServiceInformationPort
            // 
            this.tb_ServiceInformationPort.Location = new System.Drawing.Point(129, 31);
            this.tb_ServiceInformationPort.Name = "tb_ServiceInformationPort";
            this.tb_ServiceInformationPort.Size = new System.Drawing.Size(62, 21);
            this.tb_ServiceInformationPort.TabIndex = 4;
            // 
            // tb_ServiceInformationIP
            // 
            this.tb_ServiceInformationIP.Location = new System.Drawing.Point(6, 32);
            this.tb_ServiceInformationIP.Name = "tb_ServiceInformationIP";
            this.tb_ServiceInformationIP.Size = new System.Drawing.Size(100, 21);
            this.tb_ServiceInformationIP.TabIndex = 3;
            // 
            // lab_ServiceInformationCount
            // 
            this.lab_ServiceInformationCount.AutoSize = true;
            this.lab_ServiceInformationCount.Location = new System.Drawing.Point(204, 16);
            this.lab_ServiceInformationCount.Name = "lab_ServiceInformationCount";
            this.lab_ServiceInformationCount.Size = new System.Drawing.Size(35, 12);
            this.lab_ServiceInformationCount.TabIndex = 2;
            this.lab_ServiceInformationCount.Text = "Count";
            // 
            // lab_ServiceInformationPort
            // 
            this.lab_ServiceInformationPort.AutoSize = true;
            this.lab_ServiceInformationPort.Location = new System.Drawing.Point(129, 16);
            this.lab_ServiceInformationPort.Name = "lab_ServiceInformationPort";
            this.lab_ServiceInformationPort.Size = new System.Drawing.Size(29, 12);
            this.lab_ServiceInformationPort.TabIndex = 1;
            this.lab_ServiceInformationPort.Text = "Port";
            // 
            // lab_ServiceInformationIP
            // 
            this.lab_ServiceInformationIP.AutoSize = true;
            this.lab_ServiceInformationIP.Location = new System.Drawing.Point(6, 16);
            this.lab_ServiceInformationIP.Name = "lab_ServiceInformationIP";
            this.lab_ServiceInformationIP.Size = new System.Drawing.Size(17, 12);
            this.lab_ServiceInformationIP.TabIndex = 0;
            this.lab_ServiceInformationIP.Text = "IP";
            // 
            // tb_PLCInformationPort
            // 
            this.tb_PLCInformationPort.Location = new System.Drawing.Point(127, 32);
            this.tb_PLCInformationPort.Name = "tb_PLCInformationPort";
            this.tb_PLCInformationPort.Size = new System.Drawing.Size(62, 21);
            this.tb_PLCInformationPort.TabIndex = 5;
            // 
            // tb_PLCInformationIP
            // 
            this.tb_PLCInformationIP.Location = new System.Drawing.Point(6, 32);
            this.tb_PLCInformationIP.Name = "tb_PLCInformationIP";
            this.tb_PLCInformationIP.Size = new System.Drawing.Size(100, 21);
            this.tb_PLCInformationIP.TabIndex = 4;
            // 
            // lab_PLCInformationPort
            // 
            this.lab_PLCInformationPort.AutoSize = true;
            this.lab_PLCInformationPort.Location = new System.Drawing.Point(125, 16);
            this.lab_PLCInformationPort.Name = "lab_PLCInformationPort";
            this.lab_PLCInformationPort.Size = new System.Drawing.Size(29, 12);
            this.lab_PLCInformationPort.TabIndex = 2;
            this.lab_PLCInformationPort.Text = "Port";
            // 
            // lab_PLCInformationIP
            // 
            this.lab_PLCInformationIP.AutoSize = true;
            this.lab_PLCInformationIP.Location = new System.Drawing.Point(15, 16);
            this.lab_PLCInformationIP.Name = "lab_PLCInformationIP";
            this.lab_PLCInformationIP.Size = new System.Drawing.Size(17, 12);
            this.lab_PLCInformationIP.TabIndex = 1;
            this.lab_PLCInformationIP.Text = "IP";
            // 
            // btn_Trigger
            // 
            this.btn_Trigger.Location = new System.Drawing.Point(16, 60);
            this.btn_Trigger.Name = "btn_Trigger";
            this.btn_Trigger.Size = new System.Drawing.Size(95, 33);
            this.btn_Trigger.TabIndex = 1;
            this.btn_Trigger.Text = "Trigger";
            this.btn_Trigger.UseVisualStyleBackColor = true;
            // 
            // btn_ServiceInformationMemoryView
            // 
            this.btn_ServiceInformationMemoryView.Location = new System.Drawing.Point(16, 21);
            this.btn_ServiceInformationMemoryView.Name = "btn_ServiceInformationMemoryView";
            this.btn_ServiceInformationMemoryView.Size = new System.Drawing.Size(95, 32);
            this.btn_ServiceInformationMemoryView.TabIndex = 0;
            this.btn_ServiceInformationMemoryView.Text = "MemoryView";
            this.btn_ServiceInformationMemoryView.UseVisualStyleBackColor = true;
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(119, 59);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(89, 33);
            this.btn_Disconnect.TabIndex = 8;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            // 
            // gb_ServiceInformationFunction
            // 
            this.gb_ServiceInformationFunction.Controls.Add(this.btn_Trigger);
            this.gb_ServiceInformationFunction.Controls.Add(this.btn_ServiceInformationMemoryView);
            this.gb_ServiceInformationFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ServiceInformationFunction.Location = new System.Drawing.Point(284, 3);
            this.gb_ServiceInformationFunction.Name = "gb_ServiceInformationFunction";
            this.gb_ServiceInformationFunction.Size = new System.Drawing.Size(130, 99);
            this.gb_ServiceInformationFunction.TabIndex = 1;
            this.gb_ServiceInformationFunction.TabStop = false;
            this.gb_ServiceInformationFunction.Text = "Function";
            // 
            // gb_PLCInformation
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gb_PLCInformation, 2);
            this.gb_PLCInformation.Controls.Add(this.btn_Disconnect);
            this.gb_PLCInformation.Controls.Add(this.btn_Connect);
            this.gb_PLCInformation.Controls.Add(this.tb_PLCInformationPort);
            this.gb_PLCInformation.Controls.Add(this.tb_PLCInformationIP);
            this.gb_PLCInformation.Controls.Add(this.lab_PLCInformationPort);
            this.gb_PLCInformation.Controls.Add(this.lab_PLCInformationIP);
            this.gb_PLCInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_PLCInformation.Location = new System.Drawing.Point(420, 3);
            this.gb_PLCInformation.Name = "gb_PLCInformation";
            this.gb_PLCInformation.Size = new System.Drawing.Size(211, 99);
            this.gb_PLCInformation.TabIndex = 2;
            this.gb_PLCInformation.TabStop = false;
            this.gb_PLCInformation.Text = "PLC Information";
            // 
            // gb_ConnectStatus
            // 
            this.gb_ConnectStatus.Controls.Add(this.lab_ConnectPort);
            this.gb_ConnectStatus.Controls.Add(this.lab_ConnectStatus);
            this.gb_ConnectStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ConnectStatus.Location = new System.Drawing.Point(420, 108);
            this.gb_ConnectStatus.Name = "gb_ConnectStatus";
            this.gb_ConnectStatus.Size = new System.Drawing.Size(106, 100);
            this.gb_ConnectStatus.TabIndex = 4;
            this.gb_ConnectStatus.TabStop = false;
            this.gb_ConnectStatus.Text = "Conn";
            // 
            // gb_PLCInformationFunction
            // 
            this.gb_PLCInformationFunction.Controls.Add(this.btn_PLCInformationMemoryView);
            this.gb_PLCInformationFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_PLCInformationFunction.Location = new System.Drawing.Point(532, 108);
            this.gb_PLCInformationFunction.Name = "gb_PLCInformationFunction";
            this.gb_PLCInformationFunction.Size = new System.Drawing.Size(99, 100);
            this.gb_PLCInformationFunction.TabIndex = 5;
            this.gb_PLCInformationFunction.TabStop = false;
            this.gb_PLCInformationFunction.Text = "Function";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.39659F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.6034F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.Controls.Add(this.gb_ServiceInformation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gb_ServiceInformationFunction, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gb_PLCInformation, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gb_ConnectionStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gb_ConnectStatus, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.gb_PLCInformationFunction, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 211);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gb_ConnectionStatus
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gb_ConnectionStatus, 2);
            this.gb_ConnectionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ConnectionStatus.Location = new System.Drawing.Point(3, 108);
            this.gb_ConnectionStatus.Name = "gb_ConnectionStatus";
            this.gb_ConnectionStatus.Size = new System.Drawing.Size(411, 100);
            this.gb_ConnectionStatus.TabIndex = 3;
            this.gb_ConnectionStatus.TabStop = false;
            this.gb_ConnectionStatus.Text = "Connection Status";
            // 
            // lab_ConnectStatus
            // 
            this.lab_ConnectStatus.BackColor = System.Drawing.Color.LightGray;
            this.lab_ConnectStatus.ForeColor = System.Drawing.Color.Black;
            this.lab_ConnectStatus.Location = new System.Drawing.Point(15, 55);
            this.lab_ConnectStatus.Name = "lab_ConnectStatus";
            this.lab_ConnectStatus.Size = new System.Drawing.Size(76, 36);
            this.lab_ConnectStatus.TabIndex = 12;
            this.lab_ConnectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_ConnectPort
            // 
            this.lab_ConnectPort.Location = new System.Drawing.Point(34, 30);
            this.lab_ConnectPort.Name = "lab_ConnectPort";
            this.lab_ConnectPort.Size = new System.Drawing.Size(40, 12);
            this.lab_ConnectPort.TabIndex = 13;
            this.lab_ConnectPort.Text = " Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 211);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            this.gb_ServiceInformation.ResumeLayout(false);
            this.gb_ServiceInformation.PerformLayout();
            this.gb_ServiceInformationFunction.ResumeLayout(false);
            this.gb_PLCInformation.ResumeLayout(false);
            this.gb_PLCInformation.PerformLayout();
            this.gb_ConnectStatus.ResumeLayout(false);
            this.gb_PLCInformationFunction.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_PLCInformationMemoryView;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.GroupBox gb_ServiceInformation;
        private System.Windows.Forms.Button btn_CloseServer;
        private System.Windows.Forms.Button btn_OpenServer;
        private System.Windows.Forms.ComboBox cmb_ServiceInformationCount;
        private System.Windows.Forms.TextBox tb_ServiceInformationPort;
        private System.Windows.Forms.TextBox tb_ServiceInformationIP;
        private System.Windows.Forms.Label lab_ServiceInformationCount;
        private System.Windows.Forms.Label lab_ServiceInformationPort;
        private System.Windows.Forms.Label lab_ServiceInformationIP;
        private System.Windows.Forms.TextBox tb_PLCInformationPort;
        private System.Windows.Forms.TextBox tb_PLCInformationIP;
        private System.Windows.Forms.Label lab_PLCInformationPort;
        private System.Windows.Forms.Label lab_PLCInformationIP;
        private System.Windows.Forms.Button btn_Trigger;
        private System.Windows.Forms.Button btn_ServiceInformationMemoryView;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.GroupBox gb_ServiceInformationFunction;
        private System.Windows.Forms.GroupBox gb_PLCInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gb_ConnectionStatus;
        private System.Windows.Forms.GroupBox gb_ConnectStatus;
        private System.Windows.Forms.GroupBox gb_PLCInformationFunction;
        private System.Windows.Forms.Label lab_ConnectPort;
        private System.Windows.Forms.Label lab_ConnectStatus;
    }
}