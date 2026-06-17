using PLCClient.Model;
using PLCClient.Tool;
using PLCClient.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        PLCControl pLCControl = null;
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
        /// <summary>
        /// 
        /// </summary>
        bool IsWorking = false;
        /// <summary>
        /// 
        /// </summary>
        byte[] CurrentReadbyte = new byte[0];
        /// <summary>
        /// 
        /// </summary>
        short[] CurrentReadshort = new short[0];
        /// <summary>
        /// 
        /// </summary>
        byte[] CurrentReadbyteHight = new byte[0];
        /// <summary>
        /// 
        /// </summary>
        byte[] CurrentReadbyteLow = new byte[0];
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #region Button
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartRead_Click(object sender, EventArgs e)
        {
            if (CheckCanStartRead())
            {
               // pLCControl = new PLCControl("192.168.123.202", "8194");
               pLCControl = new PLCControl("127.0.0.1", "8000");
                Task.Run(() => Thread_ReadData());
                IsWorking = true;
            }
            else
            {
                MessageBox.Show("Please Check Address Information ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearLength_Click(object sender, EventArgs e)
        {
            if (pLCControl != null && pLCControl.GetConnected())
            {
                if (!int.TryParse(tb_ClearLength.Text, out var ClearLength))
                {
                    MessageBox.Show("Please Check ClearLength ");
                }
                else
                {
                    short[] clearData = new short[ClearLength];
                    var res= pLCControl.WriteDevice(CurrentReadArea, CurrentReadAddress, clearData);
                }
            }
            else
            {
                MessageBox.Show("Please Check Connection Status ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearAll_Click(object sender, EventArgs e)
        {
            if (pLCControl != null && pLCControl.GetConnected())
            {
                short[] clearData = new short[CurrentReadLength];
                pLCControl.WriteDevice(CurrentReadArea, CurrentReadAddress, clearData);
            }
            else
            {
                MessageBox.Show("Please Check Connection Status ");
            }
        }
        private void ChangeCurrentReadLength()
        {



        }
        #endregion

        #region UIChangge
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshUI();
        }
        /// <summary>
        /// 
        /// </summary>
        private void RefreshUI()
        {
            RefreshBtn();
            RefreshDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void RefreshBtn()
        {
            if (IsWorking)
            {


            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void RefreshDgv()
        {
            EnsureDataGridViewRowCount(CurrentReadLength);
        }

        // 用这个方法替换原来的 for (int i = 0; i < CurrentReadLength; i++) 循环
        // 安全地根据 CurrentReadLength 调整 dataGridView1 的行数，处理跨线程调用并避免“新建行”被误删。
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
                    DGV.Rows[i].Cells[0].Value = CurrentReadArea.ToString() + i.ToString();

                }

                if (CurrentReadshort.Length > 0)
                {
                    for (int i = 0; i < DGV.Rows.Count; i++)
                    {
                        var bits = ConverterTool.ShortToBoolArray(CurrentReadshort[i]);
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

                        DGV.Rows[i].Cells[17].Value = CurrentReadbyteHight[i];
                        DGV.Rows[i].Cells[18].Value = CurrentReadbyteLow[i];
                        DGV.Rows[i].Cells[19].Value = CurrentReadshort[i];
                        DGV.Rows[i].Cells[20].Value = ConverterTool.ShortToAscii(CurrentReadshort[i]);
                    }
                }

            }
            finally
            {
                DGV.ResumeLayout();
            }
        }
        #endregion

        #region Work
        /// <summary>
        /// 
        /// </summary>
        private void Thread_ReadData()
        {
            while (true)
            {
                try
                {
                    if (pLCControl != null && pLCControl.GetConnected())
                    {
                        var readresult = pLCControl.ReadDevice(CurrentReadArea, CurrentReadAddress, CurrentReadLength * 2, out CurrentReadbyte);
                        if (readresult && CurrentReadbyte != null)
                        {
                            // 将 byte[] 两两组合成 short[]，根据设备字节序选择 expectedLittleEndian（true = 小端，false = 大端）
                            CurrentReadshort = ConverterTool.BytesToShorts(CurrentReadbyte, expectedLittleEndian: true);
                            ConverterTool.StoreReadBytesAsHighLow(CurrentReadbyte, ref CurrentReadbyteHight, ref CurrentReadbyteLow, highByteFirst: true);
                        }
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    if (this.IsHandleCreated)
                    {
                        //this.BeginInvoke((Action)(() => MessageBox.Show(this, ex.Message)));
                    }
                }
            }
        }
        #endregion

        #region CheckHelp
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        #endregion
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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

                var data = new int[1] { 1 };


                var res = pLCControl.WriteDevice(McRegisterType.X, rowindex, 1, new int[1] { 1 });
                if (!res)
                {
                    MessageBox.Show("Write Bit Error ");
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
     
        private void btn_WriteWordData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_WriteWordArea.Text))
            {
                return;
            }
            if (!Enum.TryParse(cmb_WriteWordArea.Text, out McRegisterType WriteArea))
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

            if (pLCControl != null && pLCControl.GetConnected())
            {
                pLCControl.WriteDevice(WriteArea, WriteAddress, WriteValue);
            }
            else
            {
                MessageBox.Show("Please Check Connection Status ");
            }
        }

        private void btn_WriteBitData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_WriteBitArea.Text))
            {
                return;
            }
            if (!Enum.TryParse(cmb_WriteBitArea.Text, out McRegisterType WriteArea))
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

            if (pLCControl != null && pLCControl.GetConnected())
            {
                pLCControl.WriteDevice(WriteArea, WriteAddress,1, new int[1] { WriteValue });
            }
            else
            {
                MessageBox.Show("Please Check Connection Status ");
            }
        }
    }
}




