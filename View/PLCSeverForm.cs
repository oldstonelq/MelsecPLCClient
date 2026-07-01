using PLCTest.Model;
using PLCTest.Tool;
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
        /// <summary>
        /// 
        /// </summary>
        McRegisterType CurrentReadArea = McRegisterType.D;
        /// <summary>
        /// 
        /// </summary>
        int CurrentReadAddress = 0;
        /// <summary>
        /// 
        /// </summary>
        int CurrentWriteAddress = 0;
        /// <summary>
        /// 
        /// </summary>
        int CurrentReadLength = 0;
        public PLCSeverForm(MelsecMcPLCSever melsecMcPLCSever)
        {
            InitializeComponent();
            this.melsecMcPLCSever = melsecMcPLCSever;
        }

        private void btn_StartRead_Click(object sender, EventArgs e)
        {
            if (CheckCanStartRead())
            {
                melsecMcPLCSever.dRegisters.Clear();
                for (int i = CurrentReadAddress; i < CurrentReadLength; i++)
                {
                    melsecMcPLCSever.dRegisters.TryAdd(i,0);
                }
            }
            else
            {
                MessageBox.Show("Please Check Address Information ");
            }
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

        private bool CheckCanStartRead()
        {
            if (string.IsNullOrEmpty(cmb_Area.Text))
            {
                return false;
            }
            if (!Enum.TryParse(cmb_Area.Text, out McRegisterType Area))
            {
                return false;
            }
            if (!int.TryParse(tb_StartAddress.Text, out var StartAddress))
            {
                return false;
            }
            if (!int.TryParse(tb_ReadLength.Text, out var ReadLength))
            {
                return false;
            }
            CurrentReadArea = Area;
            CurrentReadAddress = StartAddress;
            CurrentReadLength = ReadLength;
            return true;
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
            EnsureDataGridViewRowCount(CurrentReadLength);
        }

        private void EnsureDataGridViewRowCount(int targetCount)
        {


            if (DGV == null)
                return;

            // 跨线程安全：在 UI 线程上执行实际更新
            if (DGV.InvokeRequired)
            {
                DGV.BeginInvoke((Action)(() => EnsureDataGridViewRowCount(targetCount)));
                return;
            }

            DGV.SuspendLayout();
            try
            {

                // 如果启用了 AllowUserToAddRows，最后一行为“新建行”，不计入实际数据行数
                bool hasNewRow = DGV.AllowUserToAddRows;
                int actualCount = DGV.Rows.Count - (hasNewRow ? 1 : 0);
                if (actualCount < 0) actualCount = 0;

                // 减少行数：从末尾安全删除（不删除“新建行”）
                while (actualCount > targetCount)
                {
                    int removeIndex = DGV.Rows.Count - (hasNewRow ? 2 : 1);
                    if (removeIndex < 0) break;
                    DGV.Rows.RemoveAt(removeIndex);
                    actualCount--;
                }

                // 增加行数：逐行添加空行（如需性能优化，可批量构建数组并一次 AddRange）
                while (actualCount < targetCount)
                {
                    DGV.Rows.Add();
                    actualCount++;
                }

                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    DGV.Rows[i].Cells[0].Value = CurrentReadArea.ToString() + (i + CurrentReadAddress).ToString();

                }

                if (melsecMcPLCSever.dRegisters.Count > 0)
                {
                    for (int i = 0; i < DGV.Rows.Count; i++)
                    {
                        short value = melsecMcPLCSever.dRegisters.ContainsKey(i) ? melsecMcPLCSever.dRegisters[i] : (short)0;
                        var bits = ConverterTool.ShortToBoolArray(value);
                        if (bits == null || bits.Length < 16) continue;

                        for (int j = 0; j < 16; j++)
                        {
                            int bitIndex = 15 - j; // map bits[15]..bits[0] to columns 1..16
                            DGV.Rows[i].Cells[j + 1].Value = bits[bitIndex] ? 1 : 0;
                            if (bits[bitIndex])
                            {
                                DGV.Rows[i].Cells[j + 1].Style.BackColor = Color.LightBlue;
                            }
                            else
                            {
                                DGV.Rows[i].Cells[j + 1].Style.BackColor = Color.Empty;
                            }
                        }

                        DGV.Rows[i].Cells[17].Value = ConverterTool.GetHighByte(value);
                        DGV.Rows[i].Cells[18].Value = ConverterTool.GetLowByte(value);
                        DGV.Rows[i].Cells[19].Value = value;
                        DGV.Rows[i].Cells[20].Value = ConverterTool.ShortToAscii(value);
                    }
                }
                else
                {
                    DGV.Rows.Clear();
                }
            }
            finally
            {
                DGV.ResumeLayout();
            }
        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //这个地方操作比较特殊，双击的时候，获取当前的行和列，然后重新换算数据到PLC里面去，写入数据
                int rowindex = DGV.CurrentCell.RowIndex;
                int columnindex = DGV.CurrentCell.ColumnIndex;
                if (columnindex <= 0 || columnindex > 16)
                {
                    return;
                }
                if (rowindex < 0)
                {
                    return;
                }
                ////1.拿到当前选中单元格的值
                //if (!short.TryParse(melsecMcPLCSever.dRegisters[rowindex], out var CurrentValue))
                //{
                //    return;
                //}

                //2.根据当前的值和列数，计算出要写入PLC的值
                int bitPosition = 16 - columnindex; // 列1对应bit15，列16对应bit0  
                short writeValue = (short)ConverterTool.SetBitToOne(melsecMcPLCSever.dRegisters[rowindex], bitPosition);
                if (melsecMcPLCSever.dRegisters .ContainsKey(rowindex))
                {
                    melsecMcPLCSever.dRegisters[rowindex] = writeValue;
                }

            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    //this.BeginInvoke((Action)(() => MessageBox.Show(this, ex.Message)));
                }
            }
        }

        private void btn_ClearLength_Click(object sender, EventArgs e)
        {
            if (!int.TryParse (tb_ClearLength.Text ,out var length))
            {
                return;
            }
            if (length> melsecMcPLCSever.dRegisters.Count)
            {
                return;
            }
            for (int i = 0; i < length; i++)
            {
                melsecMcPLCSever.dRegisters[i] = 0;
            }
        }

        private void btn_ClearAll_Click(object sender, EventArgs e)
        {
            foreach (var key in melsecMcPLCSever.dRegisters.Keys)
            {
                melsecMcPLCSever.dRegisters[key] = 0;
            }
        }

        private void btn_EndRead_Click(object sender, EventArgs e)
        {
            melsecMcPLCSever.dRegisters.Clear ();
        }
    }
}
