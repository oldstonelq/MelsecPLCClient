using PLCTest.Interface;
using PLCTest.PLCSever;
using PLCTest.Tool;
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
using static PLCTest.Models.Enums;

namespace PLCTest.View
{
    public partial class PLCSeverForm : Form
    {
        IPLCServer melsecMcPLCSever = null;
        /// <summary>
        /// 
        /// </summary>
        MemoryArea CurrentReadArea = MemoryArea.D;
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
        public PLCSeverForm(IPLCServer melsecMcPLCSever)
        {
            InitializeComponent();
            this.melsecMcPLCSever = melsecMcPLCSever;
        }

        private void btn_StartRead_Click(object sender, EventArgs e)
        {
            if (CheckCanStartRead())
            {
                melsecMcPLCSever.ClearRegisters(CurrentReadArea);
                for (int i = CurrentReadAddress; i < CurrentReadLength; i++)
                {
                    melsecMcPLCSever.TryAddRegister(CurrentReadArea, i);
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
            cmb_Area.Items.AddRange(Enum.GetNames(typeof(MemoryArea)));

            cmb_WriteBitArea.Items.Clear();
            cmb_WriteBitArea.Items.AddRange(Enum.GetNames(typeof(MemoryArea)));

            cmb_WriteWordArea.Items.Clear();
            cmb_WriteWordArea.Items.AddRange(Enum.GetNames(typeof(MemoryArea)));
        }

        private bool CheckCanStartRead()
        {
            if (string.IsNullOrEmpty(cmb_Area.Text))
            {
                return false;
            }
            if (!Enum.TryParse(cmb_Area.Text, out MemoryArea Area))
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
            if (melsecMcPLCSever != null && melsecMcPLCSever.SeverIsOpen)
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

                if (CurrentReadArea ==  MemoryArea.D)
                {
                    if (melsecMcPLCSever.GetRegisterCount(CurrentReadArea) > 0)
                    {
                        for (int i = 0; i < DGV.Rows.Count; i++)
                        {
                            var wordResult = melsecMcPLCSever.ReadWord(CurrentReadArea, i);
                            short value = wordResult.IsSuccess ? wordResult.Data : (short)0;
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

                            DGV.Rows[i].Cells[17].Value = ConverterTool.GetHighByteOfShort(value);
                            DGV.Rows[i].Cells[18].Value = ConverterTool.GetLowByteOfShort(value);
                            DGV.Rows[i].Cells[19].Value = value;
                            DGV.Rows[i].Cells[20].Value = ConverterTool.ShortToAscii(value);
                        }
                    }
                    else
                    {
                        DGV.Rows.Clear();
                    }
                }
                else
                {
                    if (melsecMcPLCSever.GetRegisterCount(CurrentReadArea) > 0)
                    {

                        for (int i = 0; i < DGV.Rows.Count; i++)
                        {
                            var bitResult = melsecMcPLCSever.ReadBit(CurrentReadArea, i);
                            bool bitValue = bitResult.IsSuccess && bitResult.Data;
                            DGV.Rows[i].Cells[16].Value = bitValue ? 1 : 0;
                            if (bitValue)
                            {
                                DGV.Rows[i].Cells[16].Style.BackColor = Color.LightBlue;
                            }
                            else
                            {
                                DGV.Rows[i].Cells[16].Style.BackColor = Color.Empty;
                            }
                        }
                    }
                    else
                    {
                        DGV.Rows.Clear();
                    }
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
                if (CurrentReadArea == MemoryArea .D)
                {
                    var wordResult = melsecMcPLCSever.ReadWord(CurrentReadArea, rowindex);
                    short currentValue = wordResult.IsSuccess ? wordResult.Data : (short)0;
                    //2.根据当前的值和列数，计算出要写入PLC的值
                    int bitPosition = 16 - columnindex; // 列1对应bit15，列16对应bit0  
                    short writeValue = (short)ConverterTool.SetBitToOne(currentValue, bitPosition);
                    melsecMcPLCSever.WriteWord(CurrentReadArea, rowindex, writeValue);
                }
                else
                {
                    var bitResult = melsecMcPLCSever.ReadBit(CurrentReadArea, rowindex);
                    bool currentBit = bitResult.IsSuccess && bitResult.Data;
                    melsecMcPLCSever.WriteBit(CurrentReadArea, rowindex, !currentBit);
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
                MessageBox.Show("Please Check ClearLength ");
                return;
            }
            if (length > melsecMcPLCSever.GetRegisterCount(CurrentReadArea))
            {
                return;
            }
            melsecMcPLCSever.ResetRegisterValues(CurrentReadArea, 0, length);
        }

        private void btn_ClearAll_Click(object sender, EventArgs e)
        {
            melsecMcPLCSever.ResetAllRegisterValues(CurrentReadArea);
        }

        private void btn_EndRead_Click(object sender, EventArgs e)
        {
            melsecMcPLCSever.ClearRegisters(CurrentReadArea);
        }

        private void btn_WriteBitData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_WriteBitArea.Text))
            {
                return;
            }
            if (!Enum.TryParse(cmb_WriteBitArea.Text, out MemoryArea WriteArea))
            {
                return;
            }
            if (!int.TryParse(tb_WriteBitAddress.Text, out var WriteAddress))
            {
                return;
            }
            if (!short.TryParse(tb_WriteBitValue.Text, out var WriteValue))
            {
                return;
            }
            if (WriteArea == MemoryArea.D) return;

            melsecMcPLCSever.WriteBit(WriteArea, WriteAddress, WriteValue == 1);
        }

        private void btn_WriteWordData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_WriteWordArea.Text))
            {
                return;
            }
            if (!Enum.TryParse(cmb_WriteWordArea.Text, out MemoryArea WriteArea))
            {
                return;
            }
            if (!int.TryParse(tb_WriteWordAddress.Text, out var WriteAddress))
            {
                return;
            }
            if (!short.TryParse(tb_WriteWordValue.Text, out var WriteValue))
            {
                return;
            }
            if (WriteArea != MemoryArea.D) return;
            melsecMcPLCSever.WriteWord(WriteArea, WriteAddress, WriteValue);
        }
    }
}
