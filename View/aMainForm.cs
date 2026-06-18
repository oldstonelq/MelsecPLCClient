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
        MelsecMcPLCControl pLCControl = null;

        PLCClientForm pLCClientForm=null;
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
            pLCControl= new MelsecMcPLCControl(tb_PLCInformationIP.Text, tb_PLCInformationPort.Text);
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            if (pLCControl != null)
            {
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
            if ((pLCControl != null && pLCControl.GetConnected()))
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
       
        }
        private void btn_CloseServer_Click(object sender, EventArgs e)
        {
         
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pLCControl != null && pLCControl.GetConnected())
            {
                lab_ConnectStatus.Text = "Connected";
                lab_ConnectStatus.BackColor = Color.Green;

                tb_PLCInformationIP.ReadOnly = true;
                tb_PLCInformationPort.ReadOnly = true;

                btn_Connect.Enabled = false;
                btn_Disconnect .Enabled = true;
                btn_PLCInformationMemoryView.Enabled = true;
            }
            else if(pLCControl != null && pLCControl.GetConnected()==false)
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
        }

       
    }
}
