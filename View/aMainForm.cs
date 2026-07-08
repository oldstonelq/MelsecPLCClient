using PLCTest.Communication;
using PLCTest.Device;
using PLCTest.Interface;
using PLCTest.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTest.View
{
    public partial class aMainForm : Form
    {
        IPLCDevice pLCControl = null;

        PLCClientForm pLCClientForm=null;

        MelsecMcPLCSever PLCSever = null;

        PLCSeverForm pLCSeverForm = null;
        public aMainForm()
        {
            InitializeComponent();
        }
        #region Button
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(tb_PLCInformationIP.Text, out var iPAddress))
            {
               return;
            }
            if (!int.TryParse(tb_PLCInformationPort.Text, out var port))
            {
                return;
            }
            pLCControl= new MelsecMc3EPLCClient(new TcpCommunication(tb_PLCInformationIP.Text, port));
            pLCControl.Connect();
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            if (pLCControl != null)
            {
                pLCControl.Disconnect();
                pLCControl.Dispose();
                pLCControl = null;
            }
            else
            {
                pLCControl = null;
            }
        }
        private void btn_PLCInformationMemoryView_Click(object sender, EventArgs e)
        {
            if ((pLCControl != null && pLCControl.IsConnected))
            {
                if (pLCClientForm==null|| pLCClientForm.IsDisposed)
                {
                    pLCClientForm = new PLCClientForm(pLCControl);
                    pLCClientForm.Show();
                }
                else
                {
                    pLCClientForm.Show();
                }
            }
            else
            {

            }
        }
        private void btn_OpenServer_Click(object sender, EventArgs e)
        {
            if (!int .TryParse(tb_ServiceInformationPort.Text,out var port))
            {
                return;
            }
            PLCSever = new MelsecMcPLCSever(tb_ServiceInformationIP.Text , port);
            PLCSever.StartListen();
        }
        private void btn_CloseServer_Click(object sender, EventArgs e)
        {
            if (PLCSever != null)
            {
                PLCSever.StopListen();
                PLCSever = null;
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pLCControl != null && pLCControl.IsConnected)
            {
                lab_ConnectStatus.Text = "Connected";
                lab_ConnectStatus.BackColor = Color.Green;

                tb_PLCInformationIP.ReadOnly = true;
                tb_PLCInformationPort.ReadOnly = true;

                btn_Connect.Enabled = false;
                btn_Disconnect .Enabled = true;
                btn_PLCInformationMemoryView.Enabled = true;
            }
            else if(pLCControl != null && pLCControl.IsConnected==false)
            {
                lab_ConnectStatus.Text = "waiting";
                lab_ConnectStatus.BackColor = Color.Orange;
                btn_Connect.Enabled = false;
                btn_Disconnect.Enabled = true;
                btn_PLCInformationMemoryView.Enabled = false;
            }
            else
            {
                lab_ConnectStatus.Text = "Disconnected";
                lab_ConnectStatus.BackColor = Color.Red;

                tb_PLCInformationIP.ReadOnly = false;
                tb_PLCInformationPort.ReadOnly = false;

                btn_Connect.Enabled = true;
                btn_Disconnect.Enabled = false;
                btn_PLCInformationMemoryView.Enabled = false;

                if ((pLCClientForm != null && pLCClientForm.IsDisposed==false))
                {
                    pLCClientForm.Dispose();
                }
            }

            if (PLCSever != null && PLCSever.IsWorking)
            {
                tb_ServiceInformationIP.ReadOnly = true;
                tb_ServiceInformationPort.ReadOnly= true;
                btn_OpenServer.Enabled = false;
                btn_CloseServer.Enabled = true;
                btn_ServiceInformationMemoryView.Enabled = true;
                btn_Trigger.Enabled = true;
            }
            else
            {
                tb_ServiceInformationIP.ReadOnly = false;
                tb_ServiceInformationPort.ReadOnly = false;
                btn_OpenServer.Enabled = true;
                btn_CloseServer.Enabled = false;
                btn_ServiceInformationMemoryView.Enabled = false;
                btn_Trigger.Enabled = false;
                if ((pLCSeverForm != null && pLCSeverForm.IsDisposed == false))
                {
                    pLCSeverForm.Dispose();
                }
            }

        }

        private void btn_ServiceInformationMemoryView_Click(object sender, EventArgs e)
        {
            if ((PLCSever != null && PLCSever.IsWorking))
            {
                if (pLCSeverForm == null || pLCSeverForm.IsDisposed)
                {
                    pLCSeverForm = new PLCSeverForm(PLCSever);
                    pLCSeverForm.Show();
                }
                else
                {
                    pLCSeverForm.Show();
                }
            }
            else
            {

            }
        }
    }
}
