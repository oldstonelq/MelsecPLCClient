using PLCTest.Model;
using PLCTest.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTest.View
{
    public partial class PLCSeverForm : Form
    {
        MelsecMcPLCSever melsecMcPLCSever = null;
        public PLCSeverForm(MelsecMcPLCSever melsecMcPLCSever)
        {
            InitializeComponent();
            this.melsecMcPLCSever = melsecMcPLCSever;
        }

        private void btn_StartRead_Click(object sender, EventArgs e)
        {

        }


        private void PLCSeverForm_Load(object sender, EventArgs e)
        {
            InitCombox();
        }
        private void InitCombox()
        {
            cmb_Area.Items.Clear();
            cmb_Area.Items.AddRange(Enum.GetNames(typeof(McRegisterType)));

            cmb_WriteBitArea.Items.Clear();
            cmb_WriteBitArea.Items.AddRange(Enum.GetNames(typeof(McRegisterType)));

            cmb_WriteWordArea.Items.Clear();
            cmb_WriteWordArea.Items.AddRange(Enum.GetNames(typeof(McRegisterType)));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshBtn();
            RefreshDgv();
        }


        private void RefreshBtn()
        {
            if (melsecMcPLCSever != null && melsecMcPLCSever.IsWorking)
            {
                btn_WriteBitData.Enabled = true;
                btn_WriteWordData.Enabled = true;
                btn_ClearLength.Enabled = true;
                btn_ClearAll.Enabled = true;
            }
            else
            {
                btn_WriteBitData.Enabled = false;
                btn_WriteWordData.Enabled = false;
                btn_ClearLength.Enabled = false;
                btn_ClearAll.Enabled = false;
            }
        }

        private void RefreshDgv()
        {


        }
    }
}
