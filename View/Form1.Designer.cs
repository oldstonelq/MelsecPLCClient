namespace PLCClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gb_info = new System.Windows.Forms.GroupBox();
            this.btn_StartRead = new System.Windows.Forms.Button();
            this.tb_ReadLength = new System.Windows.Forms.TextBox();
            this.tb_StartAddress = new System.Windows.Forms.TextBox();
            this.cmb_Area = new System.Windows.Forms.ComboBox();
            this.lab_ReadLength = new System.Windows.Forms.Label();
            this.lab_StartAddress = new System.Windows.Forms.Label();
            this.lab_Area = new System.Windows.Forms.Label();
            this.gb_writedata = new System.Windows.Forms.GroupBox();
            this.btn_WriteData = new System.Windows.Forms.Button();
            this.lab_Data = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gb_cleardata = new System.Windows.Forms.GroupBox();
            this.btn_ClearAll = new System.Windows.Forms.Button();
            this.btn_ClearLength = new System.Windows.Forms.Button();
            this.tb_ClearLength = new System.Windows.Forms.TextBox();
            this.lab_ClearLength = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_ReadLengDecrease1 = new System.Windows.Forms.Button();
            this.btn_ReadLengAdd1 = new System.Windows.Forms.Button();
            this.btn_ReadLengDecrease5 = new System.Windows.Forms.Button();
            this.btn_ReadLengAdd5 = new System.Windows.Forms.Button();
            this.btn_ReadLengDecrease20 = new System.Windows.Forms.Button();
            this.btn_ReadLengAdd20 = new System.Windows.Forms.Button();
            this.btn_ReadLengDecrease50 = new System.Windows.Forms.Button();
            this.btn_ReadLengAdd50 = new System.Windows.Forms.Button();
            this.Column_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_bit1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Hight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_All = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_ascII = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.gb_info.SuspendLayout();
            this.gb_writedata.SuspendLayout();
            this.gb_cleardata.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.70325F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.29675F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel1.Controls.Add(this.gb_info, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gb_writedata, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gb_cleardata, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.88889F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gb_info
            // 
            this.gb_info.Controls.Add(this.btn_StartRead);
            this.gb_info.Controls.Add(this.tb_ReadLength);
            this.gb_info.Controls.Add(this.tb_StartAddress);
            this.gb_info.Controls.Add(this.cmb_Area);
            this.gb_info.Controls.Add(this.lab_ReadLength);
            this.gb_info.Controls.Add(this.lab_StartAddress);
            this.gb_info.Controls.Add(this.lab_Area);
            this.gb_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_info.Location = new System.Drawing.Point(3, 3);
            this.gb_info.Name = "gb_info";
            this.gb_info.Size = new System.Drawing.Size(231, 117);
            this.gb_info.TabIndex = 0;
            this.gb_info.TabStop = false;
            this.gb_info.Text = "Address Information\t";
            // 
            // btn_StartRead
            // 
            this.btn_StartRead.Location = new System.Drawing.Point(11, 60);
            this.btn_StartRead.Name = "btn_StartRead";
            this.btn_StartRead.Size = new System.Drawing.Size(211, 50);
            this.btn_StartRead.TabIndex = 6;
            this.btn_StartRead.Tag = "button1";
            this.btn_StartRead.Text = "StartRead";
            this.btn_StartRead.UseVisualStyleBackColor = true;
            this.btn_StartRead.Click += new System.EventHandler(this.btn_StartRead_Click);
            // 
            // tb_ReadLength
            // 
            this.tb_ReadLength.Location = new System.Drawing.Point(159, 33);
            this.tb_ReadLength.Name = "tb_ReadLength";
            this.tb_ReadLength.Size = new System.Drawing.Size(63, 21);
            this.tb_ReadLength.TabIndex = 5;
            this.tb_ReadLength.Text = "50";
            // 
            // tb_StartAddress
            // 
            this.tb_StartAddress.Location = new System.Drawing.Point(64, 33);
            this.tb_StartAddress.Name = "tb_StartAddress";
            this.tb_StartAddress.Size = new System.Drawing.Size(75, 21);
            this.tb_StartAddress.TabIndex = 4;
            this.tb_StartAddress.Text = "0";
            // 
            // cmb_Area
            // 
            this.cmb_Area.AutoCompleteCustomSource.AddRange(new string[] {
            "A",
            "B",
            "C",
            "D"});
            this.cmb_Area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Area.FormattingEnabled = true;
            this.cmb_Area.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D"});
            this.cmb_Area.Location = new System.Drawing.Point(10, 33);
            this.cmb_Area.Name = "cmb_Area";
            this.cmb_Area.Size = new System.Drawing.Size(40, 20);
            this.cmb_Area.TabIndex = 3;
            // 
            // lab_ReadLength
            // 
            this.lab_ReadLength.AutoSize = true;
            this.lab_ReadLength.Location = new System.Drawing.Point(156, 17);
            this.lab_ReadLength.Name = "lab_ReadLength";
            this.lab_ReadLength.Size = new System.Drawing.Size(65, 12);
            this.lab_ReadLength.TabIndex = 2;
            this.lab_ReadLength.Text = "ReadLength";
            // 
            // lab_StartAddress
            // 
            this.lab_StartAddress.AutoSize = true;
            this.lab_StartAddress.Location = new System.Drawing.Point(61, 17);
            this.lab_StartAddress.Name = "lab_StartAddress";
            this.lab_StartAddress.Size = new System.Drawing.Size(77, 12);
            this.lab_StartAddress.TabIndex = 1;
            this.lab_StartAddress.Text = "StartAddress";
            // 
            // lab_Area
            // 
            this.lab_Area.AutoSize = true;
            this.lab_Area.Location = new System.Drawing.Point(8, 17);
            this.lab_Area.Name = "lab_Area";
            this.lab_Area.Size = new System.Drawing.Size(29, 12);
            this.lab_Area.TabIndex = 0;
            this.lab_Area.Text = "Area";
            // 
            // gb_writedata
            // 
            this.gb_writedata.Controls.Add(this.btn_ReadLengAdd50);
            this.gb_writedata.Controls.Add(this.btn_ReadLengDecrease50);
            this.gb_writedata.Controls.Add(this.btn_ReadLengAdd20);
            this.gb_writedata.Controls.Add(this.btn_ReadLengDecrease20);
            this.gb_writedata.Controls.Add(this.btn_ReadLengAdd5);
            this.gb_writedata.Controls.Add(this.btn_ReadLengDecrease5);
            this.gb_writedata.Controls.Add(this.btn_ReadLengAdd1);
            this.gb_writedata.Controls.Add(this.btn_ReadLengDecrease1);
            this.gb_writedata.Controls.Add(this.btn_WriteData);
            this.gb_writedata.Controls.Add(this.lab_Data);
            this.gb_writedata.Controls.Add(this.textBox1);
            this.gb_writedata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_writedata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_writedata.Location = new System.Drawing.Point(240, 3);
            this.gb_writedata.Name = "gb_writedata";
            this.gb_writedata.Size = new System.Drawing.Size(422, 117);
            this.gb_writedata.TabIndex = 1;
            this.gb_writedata.TabStop = false;
            this.gb_writedata.Text = "Write Data";
            // 
            // btn_WriteData
            // 
            this.btn_WriteData.Location = new System.Drawing.Point(324, 31);
            this.btn_WriteData.Name = "btn_WriteData";
            this.btn_WriteData.Size = new System.Drawing.Size(75, 23);
            this.btn_WriteData.TabIndex = 2;
            this.btn_WriteData.Text = "WriteData";
            this.btn_WriteData.UseVisualStyleBackColor = true;
            // 
            // lab_Data
            // 
            this.lab_Data.AutoSize = true;
            this.lab_Data.Location = new System.Drawing.Point(7, 17);
            this.lab_Data.Name = "lab_Data";
            this.lab_Data.Size = new System.Drawing.Size(29, 12);
            this.lab_Data.TabIndex = 1;
            this.lab_Data.Text = "Data";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 21);
            this.textBox1.TabIndex = 0;
            // 
            // gb_cleardata
            // 
            this.gb_cleardata.Controls.Add(this.btn_ClearAll);
            this.gb_cleardata.Controls.Add(this.btn_ClearLength);
            this.gb_cleardata.Controls.Add(this.tb_ClearLength);
            this.gb_cleardata.Controls.Add(this.lab_ClearLength);
            this.gb_cleardata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_cleardata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_cleardata.Location = new System.Drawing.Point(668, 3);
            this.gb_cleardata.Name = "gb_cleardata";
            this.gb_cleardata.Size = new System.Drawing.Size(129, 117);
            this.gb_cleardata.TabIndex = 2;
            this.gb_cleardata.TabStop = false;
            this.gb_cleardata.Text = "Clear Data";
            // 
            // btn_ClearAll
            // 
            this.btn_ClearAll.Location = new System.Drawing.Point(8, 60);
            this.btn_ClearAll.Name = "btn_ClearAll";
            this.btn_ClearAll.Size = new System.Drawing.Size(112, 50);
            this.btn_ClearAll.TabIndex = 9;
            this.btn_ClearAll.Text = "ClearAll";
            this.btn_ClearAll.UseVisualStyleBackColor = true;
            this.btn_ClearAll.Click += new System.EventHandler(this.btn_ClearAll_Click);
            // 
            // btn_ClearLength
            // 
            this.btn_ClearLength.Location = new System.Drawing.Point(75, 32);
            this.btn_ClearLength.Name = "btn_ClearLength";
            this.btn_ClearLength.Size = new System.Drawing.Size(53, 23);
            this.btn_ClearLength.TabIndex = 8;
            this.btn_ClearLength.Text = "Clear";
            this.btn_ClearLength.UseVisualStyleBackColor = true;
            this.btn_ClearLength.Click += new System.EventHandler(this.btn_ClearLength_Click);
            // 
            // tb_ClearLength
            // 
            this.tb_ClearLength.Location = new System.Drawing.Point(6, 32);
            this.tb_ClearLength.Name = "tb_ClearLength";
            this.tb_ClearLength.Size = new System.Drawing.Size(63, 21);
            this.tb_ClearLength.TabIndex = 7;
            // 
            // lab_ClearLength
            // 
            this.lab_ClearLength.AutoSize = true;
            this.lab_ClearLength.Location = new System.Drawing.Point(6, 17);
            this.lab_ClearLength.Name = "lab_ClearLength";
            this.lab_ClearLength.Size = new System.Drawing.Size(71, 12);
            this.lab_ClearLength.TabIndex = 6;
            this.lab_ClearLength.Text = "ClearLength";
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 328);
            this.panel1.TabIndex = 3;
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeColumns = false;
            this.DGV.AllowUserToResizeRows = false;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Address,
            this.Column_bit15,
            this.Column_bit14,
            this.Column_bit13,
            this.Column_bit12,
            this.Column_bit11,
            this.Column_bit10,
            this.Column_9,
            this.Column_bit8,
            this.Column_bit7,
            this.Column_bit6,
            this.Column_bit5,
            this.Column_bit4,
            this.Column_bit3,
            this.Column_bit2,
            this.Column_bit1,
            this.Column_0,
            this.Column_Hight,
            this.Column_Low,
            this.Column_All,
            this.Column_ascII});
            this.DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV.Location = new System.Drawing.Point(0, 0);
            this.DGV.MultiSelect = false;
            this.DGV.Name = "DGV";
            this.DGV.RowTemplate.Height = 23;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV.Size = new System.Drawing.Size(794, 328);
            this.DGV.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_ReadLengDecrease1
            // 
            this.btn_ReadLengDecrease1.Location = new System.Drawing.Point(6, 78);
            this.btn_ReadLengDecrease1.Name = "btn_ReadLengDecrease1";
            this.btn_ReadLengDecrease1.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengDecrease1.TabIndex = 3;
            this.btn_ReadLengDecrease1.Tag = "-1";
            this.btn_ReadLengDecrease1.Text = "-1";
            this.btn_ReadLengDecrease1.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengAdd1
            // 
            this.btn_ReadLengAdd1.Location = new System.Drawing.Point(59, 78);
            this.btn_ReadLengAdd1.Name = "btn_ReadLengAdd1";
            this.btn_ReadLengAdd1.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengAdd1.TabIndex = 4;
            this.btn_ReadLengAdd1.Tag = "1";
            this.btn_ReadLengAdd1.Text = "+1";
            this.btn_ReadLengAdd1.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengDecrease5
            // 
            this.btn_ReadLengDecrease5.Location = new System.Drawing.Point(112, 78);
            this.btn_ReadLengDecrease5.Name = "btn_ReadLengDecrease5";
            this.btn_ReadLengDecrease5.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengDecrease5.TabIndex = 5;
            this.btn_ReadLengDecrease5.Tag = "-5";
            this.btn_ReadLengDecrease5.Text = "-5";
            this.btn_ReadLengDecrease5.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengAdd5
            // 
            this.btn_ReadLengAdd5.Location = new System.Drawing.Point(165, 78);
            this.btn_ReadLengAdd5.Name = "btn_ReadLengAdd5";
            this.btn_ReadLengAdd5.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengAdd5.TabIndex = 6;
            this.btn_ReadLengAdd5.Tag = "5";
            this.btn_ReadLengAdd5.Text = "+5";
            this.btn_ReadLengAdd5.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengDecrease20
            // 
            this.btn_ReadLengDecrease20.Location = new System.Drawing.Point(218, 78);
            this.btn_ReadLengDecrease20.Name = "btn_ReadLengDecrease20";
            this.btn_ReadLengDecrease20.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengDecrease20.TabIndex = 7;
            this.btn_ReadLengDecrease20.Tag = "-20";
            this.btn_ReadLengDecrease20.Text = "-20";
            this.btn_ReadLengDecrease20.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengAdd20
            // 
            this.btn_ReadLengAdd20.Location = new System.Drawing.Point(271, 78);
            this.btn_ReadLengAdd20.Name = "btn_ReadLengAdd20";
            this.btn_ReadLengAdd20.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengAdd20.TabIndex = 8;
            this.btn_ReadLengAdd20.Tag = "20";
            this.btn_ReadLengAdd20.Text = "+20";
            this.btn_ReadLengAdd20.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengDecrease50
            // 
            this.btn_ReadLengDecrease50.Location = new System.Drawing.Point(324, 78);
            this.btn_ReadLengDecrease50.Name = "btn_ReadLengDecrease50";
            this.btn_ReadLengDecrease50.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengDecrease50.TabIndex = 9;
            this.btn_ReadLengDecrease50.Tag = "-50";
            this.btn_ReadLengDecrease50.Text = "-50";
            this.btn_ReadLengDecrease50.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLengAdd50
            // 
            this.btn_ReadLengAdd50.Location = new System.Drawing.Point(377, 78);
            this.btn_ReadLengAdd50.Name = "btn_ReadLengAdd50";
            this.btn_ReadLengAdd50.Size = new System.Drawing.Size(40, 20);
            this.btn_ReadLengAdd50.TabIndex = 10;
            this.btn_ReadLengAdd50.Tag = "50";
            this.btn_ReadLengAdd50.Text = "+50";
            this.btn_ReadLengAdd50.UseVisualStyleBackColor = true;
            // 
            // Column_Address
            // 
            this.Column_Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_Address.HeaderText = "Address";
            this.Column_Address.Name = "Column_Address";
            this.Column_Address.ReadOnly = true;
            this.Column_Address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit15
            // 
            this.Column_bit15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit15.HeaderText = "15";
            this.Column_bit15.Name = "Column_bit15";
            this.Column_bit15.ReadOnly = true;
            this.Column_bit15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit14
            // 
            this.Column_bit14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit14.HeaderText = "14";
            this.Column_bit14.Name = "Column_bit14";
            this.Column_bit14.ReadOnly = true;
            this.Column_bit14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit13
            // 
            this.Column_bit13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit13.HeaderText = "13";
            this.Column_bit13.Name = "Column_bit13";
            this.Column_bit13.ReadOnly = true;
            this.Column_bit13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit12
            // 
            this.Column_bit12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit12.HeaderText = "12";
            this.Column_bit12.Name = "Column_bit12";
            this.Column_bit12.ReadOnly = true;
            this.Column_bit12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit11
            // 
            this.Column_bit11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit11.HeaderText = "11";
            this.Column_bit11.Name = "Column_bit11";
            this.Column_bit11.ReadOnly = true;
            this.Column_bit11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit10
            // 
            this.Column_bit10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit10.HeaderText = "10";
            this.Column_bit10.Name = "Column_bit10";
            this.Column_bit10.ReadOnly = true;
            this.Column_bit10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_9
            // 
            this.Column_9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_9.HeaderText = "9";
            this.Column_9.Name = "Column_9";
            this.Column_9.ReadOnly = true;
            this.Column_9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit8
            // 
            this.Column_bit8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit8.HeaderText = "8";
            this.Column_bit8.Name = "Column_bit8";
            this.Column_bit8.ReadOnly = true;
            this.Column_bit8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit7
            // 
            this.Column_bit7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit7.HeaderText = "7";
            this.Column_bit7.Name = "Column_bit7";
            this.Column_bit7.ReadOnly = true;
            this.Column_bit7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit6
            // 
            this.Column_bit6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit6.HeaderText = "6";
            this.Column_bit6.Name = "Column_bit6";
            this.Column_bit6.ReadOnly = true;
            this.Column_bit6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit5
            // 
            this.Column_bit5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit5.HeaderText = "5";
            this.Column_bit5.Name = "Column_bit5";
            this.Column_bit5.ReadOnly = true;
            this.Column_bit5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit4
            // 
            this.Column_bit4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit4.HeaderText = "4";
            this.Column_bit4.Name = "Column_bit4";
            this.Column_bit4.ReadOnly = true;
            this.Column_bit4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit3
            // 
            this.Column_bit3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit3.HeaderText = "3";
            this.Column_bit3.Name = "Column_bit3";
            this.Column_bit3.ReadOnly = true;
            this.Column_bit3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit2
            // 
            this.Column_bit2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit2.HeaderText = "2";
            this.Column_bit2.Name = "Column_bit2";
            this.Column_bit2.ReadOnly = true;
            this.Column_bit2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_bit1
            // 
            this.Column_bit1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_bit1.HeaderText = "1";
            this.Column_bit1.Name = "Column_bit1";
            this.Column_bit1.ReadOnly = true;
            this.Column_bit1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_0
            // 
            this.Column_0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_0.HeaderText = "0";
            this.Column_0.Name = "Column_0";
            this.Column_0.ReadOnly = true;
            this.Column_0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Hight
            // 
            this.Column_Hight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_Hight.HeaderText = "Hight";
            this.Column_Hight.Name = "Column_Hight";
            this.Column_Hight.ReadOnly = true;
            this.Column_Hight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Low
            // 
            this.Column_Low.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_Low.HeaderText = "Low";
            this.Column_Low.Name = "Column_Low";
            this.Column_Low.ReadOnly = true;
            this.Column_Low.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_All
            // 
            this.Column_All.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_All.HeaderText = "All";
            this.Column_All.Name = "Column_All";
            this.Column_All.ReadOnly = true;
            this.Column_All.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_ascII
            // 
            this.Column_ascII.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_ascII.HeaderText = "ASCII";
            this.Column_ascII.Name = "Column_ascII";
            this.Column_ascII.ReadOnly = true;
            this.Column_ascII.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 457);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gb_info.ResumeLayout(false);
            this.gb_info.PerformLayout();
            this.gb_writedata.ResumeLayout(false);
            this.gb_writedata.PerformLayout();
            this.gb_cleardata.ResumeLayout(false);
            this.gb_cleardata.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gb_info;
        private System.Windows.Forms.GroupBox gb_writedata;
        private System.Windows.Forms.GroupBox gb_cleardata;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.TextBox tb_ReadLength;
        private System.Windows.Forms.TextBox tb_StartAddress;
        private System.Windows.Forms.ComboBox cmb_Area;
        private System.Windows.Forms.Label lab_ReadLength;
        private System.Windows.Forms.Label lab_StartAddress;
        private System.Windows.Forms.Label lab_Area;
        private System.Windows.Forms.Button btn_StartRead;
        private System.Windows.Forms.Button btn_ClearAll;
        private System.Windows.Forms.Button btn_ClearLength;
        private System.Windows.Forms.TextBox tb_ClearLength;
        private System.Windows.Forms.Label lab_ClearLength;
        private System.Windows.Forms.Button btn_WriteData;
        private System.Windows.Forms.Label lab_Data;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_ReadLengDecrease50;
        private System.Windows.Forms.Button btn_ReadLengAdd20;
        private System.Windows.Forms.Button btn_ReadLengDecrease20;
        private System.Windows.Forms.Button btn_ReadLengAdd5;
        private System.Windows.Forms.Button btn_ReadLengDecrease5;
        private System.Windows.Forms.Button btn_ReadLengAdd1;
        private System.Windows.Forms.Button btn_ReadLengDecrease1;
        private System.Windows.Forms.Button btn_ReadLengAdd50;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_bit1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Hight;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Low;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_All;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ascII;
    }
}

