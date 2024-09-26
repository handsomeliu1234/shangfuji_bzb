namespace NewuTB.TB
{
    partial class FM_TB_BinSeting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_TB_BinSeting));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmb_BarCode = new System.Windows.Forms.ComboBox();
            this.lb_Barcode = new System.Windows.Forms.Label();
            this.cmb_MaterialID = new System.Windows.Forms.ComboBox();
            this.lb_MaterialName = new System.Windows.Forms.Label();
            this.cmb_TypeCodeID = new System.Windows.Forms.ComboBox();
            this.lb_EquipmentType = new System.Windows.Forms.Label();
            this.lb_PreAllowDown = new System.Windows.Forms.Label();
            this.txtPreSetGcsD = new System.Windows.Forms.TextBox();
            this.cmbDeviceID = new System.Windows.Forms.ComboBox();
            this.lb_Device = new System.Windows.Forms.Label();
            this.lb_PreAllowUp = new System.Windows.Forms.Label();
            this.txtPreSetGcsU = new System.Windows.Forms.TextBox();
            this.lb_PreTiQian = new System.Windows.Forms.Label();
            this.txtPreSetTiqian = new System.Windows.Forms.TextBox();
            this.lb_PreQuick = new System.Windows.Forms.Label();
            this.txtPreSetKuai = new System.Windows.Forms.TextBox();
            this.lb_BinNo = new System.Windows.Forms.Label();
            this.txt_BinNo = new System.Windows.Forms.TextBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTypeCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lab_Equipment = new System.Windows.Forms.Label();
            this.cmb_DeviceID = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv = new NewuCommon.DataGridViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.splitContainer1.Panel1.Controls.Add(this.cmb_BarCode);
            this.splitContainer1.Panel1.Controls.Add(this.lb_Barcode);
            this.splitContainer1.Panel1.Controls.Add(this.cmb_MaterialID);
            this.splitContainer1.Panel1.Controls.Add(this.lb_MaterialName);
            this.splitContainer1.Panel1.Controls.Add(this.cmb_TypeCodeID);
            this.splitContainer1.Panel1.Controls.Add(this.lb_EquipmentType);
            this.splitContainer1.Panel1.Controls.Add(this.lb_PreAllowDown);
            this.splitContainer1.Panel1.Controls.Add(this.txtPreSetGcsD);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDeviceID);
            this.splitContainer1.Panel1.Controls.Add(this.lb_Device);
            this.splitContainer1.Panel1.Controls.Add(this.lb_PreAllowUp);
            this.splitContainer1.Panel1.Controls.Add(this.txtPreSetGcsU);
            this.splitContainer1.Panel1.Controls.Add(this.lb_PreTiQian);
            this.splitContainer1.Panel1.Controls.Add(this.txtPreSetTiqian);
            this.splitContainer1.Panel1.Controls.Add(this.lb_PreQuick);
            this.splitContainer1.Panel1.Controls.Add(this.txtPreSetKuai);
            this.splitContainer1.Panel1.Controls.Add(this.lb_BinNo);
            this.splitContainer1.Panel1.Controls.Add(this.txt_BinNo);
            this.splitContainer1.Panel1.Controls.Add(this.btnDel);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1924, 477);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // cmb_BarCode
            // 
            this.cmb_BarCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BarCode.FormattingEnabled = true;
            this.cmb_BarCode.Location = new System.Drawing.Point(1302, 14);
            this.cmb_BarCode.Name = "cmb_BarCode";
            this.cmb_BarCode.Size = new System.Drawing.Size(142, 22);
            this.cmb_BarCode.TabIndex = 120;
            // 
            // lb_Barcode
            // 
            this.lb_Barcode.Location = new System.Drawing.Point(1191, 17);
            this.lb_Barcode.Name = "lb_Barcode";
            this.lb_Barcode.Size = new System.Drawing.Size(105, 14);
            this.lb_Barcode.TabIndex = 119;
            this.lb_Barcode.Text = "Material Code:";
            this.lb_Barcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_MaterialID
            // 
            this.cmb_MaterialID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MaterialID.FormattingEnabled = true;
            this.cmb_MaterialID.Location = new System.Drawing.Point(1043, 14);
            this.cmb_MaterialID.Name = "cmb_MaterialID";
            this.cmb_MaterialID.Size = new System.Drawing.Size(142, 22);
            this.cmb_MaterialID.TabIndex = 115;
            this.cmb_MaterialID.SelectedIndexChanged += new System.EventHandler(this.Cmb_MaterialID_SelectedIndexChanged);
            // 
            // lb_MaterialName
            // 
            this.lb_MaterialName.Location = new System.Drawing.Point(876, 19);
            this.lb_MaterialName.Name = "lb_MaterialName";
            this.lb_MaterialName.Size = new System.Drawing.Size(161, 14);
            this.lb_MaterialName.TabIndex = 116;
            this.lb_MaterialName.Text = "Material Name:";
            this.lb_MaterialName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_TypeCodeID
            // 
            this.cmb_TypeCodeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_TypeCodeID.FormattingEnabled = true;
            this.cmb_TypeCodeID.Location = new System.Drawing.Point(729, 14);
            this.cmb_TypeCodeID.Name = "cmb_TypeCodeID";
            this.cmb_TypeCodeID.Size = new System.Drawing.Size(141, 22);
            this.cmb_TypeCodeID.TabIndex = 113;
            this.cmb_TypeCodeID.SelectedIndexChanged += new System.EventHandler(this.Cmb_TypeCodeID_SelectedIndexChanged);
            // 
            // lb_EquipmentType
            // 
            this.lb_EquipmentType.Location = new System.Drawing.Point(574, 19);
            this.lb_EquipmentType.Name = "lb_EquipmentType";
            this.lb_EquipmentType.Size = new System.Drawing.Size(149, 14);
            this.lb_EquipmentType.TabIndex = 114;
            this.lb_EquipmentType.Text = "Material Type:";
            this.lb_EquipmentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_PreAllowDown
            // 
            this.lb_PreAllowDown.Location = new System.Drawing.Point(877, 50);
            this.lb_PreAllowDown.Name = "lb_PreAllowDown";
            this.lb_PreAllowDown.Size = new System.Drawing.Size(161, 14);
            this.lb_PreAllowDown.TabIndex = 112;
            this.lb_PreAllowDown.Text = "Preset tolerance down:";
            this.lb_PreAllowDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPreSetGcsD
            // 
            this.txtPreSetGcsD.Location = new System.Drawing.Point(1044, 46);
            this.txtPreSetGcsD.Name = "txtPreSetGcsD";
            this.txtPreSetGcsD.Size = new System.Drawing.Size(142, 23);
            this.txtPreSetGcsD.TabIndex = 111;
            this.txtPreSetGcsD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPreSetGcsU_KeyPress);
            // 
            // cmbDeviceID
            // 
            this.cmbDeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceID.FormattingEnabled = true;
            this.cmbDeviceID.Location = new System.Drawing.Point(419, 14);
            this.cmbDeviceID.Name = "cmbDeviceID";
            this.cmbDeviceID.Size = new System.Drawing.Size(142, 22);
            this.cmbDeviceID.TabIndex = 109;
            this.cmbDeviceID.SelectedIndexChanged += new System.EventHandler(this.CmbDeviceID_SelectedIndexChanged);
            // 
            // lb_Device
            // 
            this.lb_Device.Location = new System.Drawing.Point(278, 18);
            this.lb_Device.Name = "lb_Device";
            this.lb_Device.Size = new System.Drawing.Size(135, 14);
            this.lb_Device.TabIndex = 110;
            this.lb_Device.Text = "Device:";
            this.lb_Device.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_PreAllowUp
            // 
            this.lb_PreAllowUp.Location = new System.Drawing.Point(567, 52);
            this.lb_PreAllowUp.Name = "lb_PreAllowUp";
            this.lb_PreAllowUp.Size = new System.Drawing.Size(156, 14);
            this.lb_PreAllowUp.TabIndex = 108;
            this.lb_PreAllowUp.Text = "Preset tolerance up:";
            this.lb_PreAllowUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPreSetGcsU
            // 
            this.txtPreSetGcsU.Location = new System.Drawing.Point(729, 46);
            this.txtPreSetGcsU.Name = "txtPreSetGcsU";
            this.txtPreSetGcsU.Size = new System.Drawing.Size(142, 23);
            this.txtPreSetGcsU.TabIndex = 107;
            this.txtPreSetGcsU.TextChanged += new System.EventHandler(this.TxtPreSetGcsU_TextChanged);
            this.txtPreSetGcsU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPreSetGcsU_KeyPress);
            // 
            // lb_PreTiQian
            // 
            this.lb_PreTiQian.Location = new System.Drawing.Point(275, 51);
            this.lb_PreTiQian.Name = "lb_PreTiQian";
            this.lb_PreTiQian.Size = new System.Drawing.Size(138, 14);
            this.lb_PreTiQian.TabIndex = 106;
            this.lb_PreTiQian.Text = "Preset in advance:";
            this.lb_PreTiQian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPreSetTiqian
            // 
            this.txtPreSetTiqian.Location = new System.Drawing.Point(419, 46);
            this.txtPreSetTiqian.Name = "txtPreSetTiqian";
            this.txtPreSetTiqian.Size = new System.Drawing.Size(142, 23);
            this.txtPreSetTiqian.TabIndex = 105;
            this.txtPreSetTiqian.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPreSetGcsU_KeyPress);
            // 
            // lb_PreQuick
            // 
            this.lb_PreQuick.Location = new System.Drawing.Point(19, 51);
            this.lb_PreQuick.Name = "lb_PreQuick";
            this.lb_PreQuick.Size = new System.Drawing.Size(105, 14);
            this.lb_PreQuick.TabIndex = 104;
            this.lb_PreQuick.Text = "Preset Fast:";
            this.lb_PreQuick.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPreSetKuai
            // 
            this.txtPreSetKuai.Location = new System.Drawing.Point(129, 46);
            this.txtPreSetKuai.Name = "txtPreSetKuai";
            this.txtPreSetKuai.Size = new System.Drawing.Size(142, 23);
            this.txtPreSetKuai.TabIndex = 103;
            this.txtPreSetKuai.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPreSetGcsU_KeyPress);
            // 
            // lb_BinNo
            // 
            this.lb_BinNo.Location = new System.Drawing.Point(19, 18);
            this.lb_BinNo.Name = "lb_BinNo";
            this.lb_BinNo.Size = new System.Drawing.Size(105, 14);
            this.lb_BinNo.TabIndex = 97;
            this.lb_BinNo.Text = "Bucket Number:";
            this.lb_BinNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_BinNo
            // 
            this.txt_BinNo.Location = new System.Drawing.Point(130, 13);
            this.txt_BinNo.Name = "txt_BinNo";
            this.txt_BinNo.Size = new System.Drawing.Size(142, 23);
            this.txt_BinNo.TabIndex = 96;
            // 
            // btnDel
            // 
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(1649, 19);
            this.btnDel.Name = "btnDel";
            this.btnDel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDel.Size = new System.Drawing.Size(76, 30);
            this.btnDel.TabIndex = 6;
            this.btnDel.Text = "删除";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(1554, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEdit.Size = new System.Drawing.Size(76, 30);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "编辑";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(1460, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnAdd.Size = new System.Drawing.Size(76, 30);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "新增";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1924, 397);
            this.splitContainer2.SplitterDistance = 60;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbTypeCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.lab_Equipment);
            this.groupBox1.Controls.Add(this.cmb_DeviceID);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1924, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(253, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 15;
            this.label7.Text = "物料类型";
            // 
            // cmbTypeCode
            // 
            this.cmbTypeCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeCode.FormattingEnabled = true;
            this.cmbTypeCode.Location = new System.Drawing.Point(332, 26);
            this.cmbTypeCode.Name = "cmbTypeCode";
            this.cmbTypeCode.Size = new System.Drawing.Size(125, 22);
            this.cmbTypeCode.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 15F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(758, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 118;
            this.label5.Text = "提示";
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.Location = new System.Drawing.Point(572, 21);
            this.btnReset.Name = "btnReset";
            this.btnReset.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnReset.Size = new System.Drawing.Size(76, 30);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "重置";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(477, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnQuery.Size = new System.Drawing.Size(76, 30);
            this.btnQuery.TabIndex = 13;
            this.btnQuery.Text = "查询";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // lab_Equipment
            // 
            this.lab_Equipment.Location = new System.Drawing.Point(23, 31);
            this.lab_Equipment.Name = "lab_Equipment";
            this.lab_Equipment.Size = new System.Drawing.Size(70, 14);
            this.lab_Equipment.TabIndex = 11;
            this.lab_Equipment.Text = "设备：";
            this.lab_Equipment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_DeviceID
            // 
            this.cmb_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DeviceID.FormattingEnabled = true;
            this.cmb_DeviceID.Location = new System.Drawing.Point(96, 28);
            this.cmb_DeviceID.Name = "cmb_DeviceID";
            this.cmb_DeviceID.Size = new System.Drawing.Size(147, 22);
            this.cmb_DeviceID.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(667, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1924, 332);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息列表";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.Location = new System.Drawing.Point(3, 19);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1918, 310);
            this.dgv.TabIndex = 5;
            this.dgv.VisibleOrderNumber = true;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CellClick);
            // 
            // FM_TB_BinSeting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 477);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_TB_BinSeting";
            this.Text = "储斗参数管理";
            this.Load += new System.EventHandler(this.FM_TB_BinSeting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lab_Equipment;
        private System.Windows.Forms.ComboBox cmb_DeviceID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_BinNo;
        private System.Windows.Forms.TextBox txt_BinNo;
        private System.Windows.Forms.Label lb_PreQuick;
        private System.Windows.Forms.TextBox txtPreSetKuai;
        private System.Windows.Forms.Label lb_PreTiQian;
        private System.Windows.Forms.TextBox txtPreSetTiqian;
        private System.Windows.Forms.Label lb_PreAllowUp;
        private System.Windows.Forms.TextBox txtPreSetGcsU;
        private System.Windows.Forms.ComboBox cmbDeviceID;
        private System.Windows.Forms.Label lb_Device;
        private System.Windows.Forms.Label lb_PreAllowDown;
        private System.Windows.Forms.TextBox txtPreSetGcsD;
        private System.Windows.Forms.ComboBox cmb_TypeCodeID;
        private System.Windows.Forms.Label lb_EquipmentType;
        private System.Windows.Forms.ComboBox cmb_MaterialID;
        private System.Windows.Forms.Label lb_MaterialName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_BarCode;
        private System.Windows.Forms.Label lb_Barcode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTypeCode;
        private NewuCommon.DataGridViewEx dgv;
    }
}