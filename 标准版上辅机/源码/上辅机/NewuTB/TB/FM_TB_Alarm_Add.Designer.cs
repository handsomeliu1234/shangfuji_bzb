namespace NewuTB.TB
{
    partial class FM_TB_Alarm_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_TB_Alarm_Add));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_IsDisplay = new System.Windows.Forms.ComboBox();
            this.txt_x = new System.Windows.Forms.TextBox();
            this.txt_MemoryAddr = new System.Windows.Forms.TextBox();
            this.txt_AlarmInfo = new System.Windows.Forms.TextBox();
            this.cmb_DevicePartID = new System.Windows.Forms.ComboBox();
            this.cmb_DeviceID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(677, 343);
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(119, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(24, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnSave.Size = new System.Drawing.Size(76, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmb_IsDisplay);
            this.groupBox1.Controls.Add(this.txt_x);
            this.groupBox1.Controls.Add(this.txt_MemoryAddr);
            this.groupBox1.Controls.Add(this.txt_AlarmInfo);
            this.groupBox1.Controls.Add(this.cmb_DevicePartID);
            this.groupBox1.Controls.Add(this.cmb_DeviceID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(677, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报警详细信息";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(569, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 47;
            this.label10.Text = "*不能为空";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(202, 245);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 46;
            this.label9.Text = "*不能为空";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(342, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 45;
            this.label7.Text = "*不能为空";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(342, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 44;
            this.label8.Text = "*不能为空";
            // 
            // cmb_IsDisplay
            // 
            this.cmb_IsDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_IsDisplay.FormattingEnabled = true;
            this.cmb_IsDisplay.Location = new System.Drawing.Point(126, 240);
            this.cmb_IsDisplay.Name = "cmb_IsDisplay";
            this.cmb_IsDisplay.Size = new System.Drawing.Size(72, 22);
            this.cmb_IsDisplay.TabIndex = 11;
            // 
            // txt_x
            // 
            this.txt_x.Location = new System.Drawing.Point(126, 175);
            this.txt_x.Name = "txt_x";
            this.txt_x.Size = new System.Drawing.Size(91, 23);
            this.txt_x.TabIndex = 10;
            // 
            // txt_MemoryAddr
            // 
            this.txt_MemoryAddr.Location = new System.Drawing.Point(126, 206);
            this.txt_MemoryAddr.Name = "txt_MemoryAddr";
            this.txt_MemoryAddr.Size = new System.Drawing.Size(159, 23);
            this.txt_MemoryAddr.TabIndex = 9;
            // 
            // txt_AlarmInfo
            // 
            this.txt_AlarmInfo.Location = new System.Drawing.Point(126, 92);
            this.txt_AlarmInfo.Multiline = true;
            this.txt_AlarmInfo.Name = "txt_AlarmInfo";
            this.txt_AlarmInfo.Size = new System.Drawing.Size(436, 77);
            this.txt_AlarmInfo.TabIndex = 8;
            // 
            // cmb_DevicePartID
            // 
            this.cmb_DevicePartID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DevicePartID.FormattingEnabled = true;
            this.cmb_DevicePartID.Location = new System.Drawing.Point(126, 62);
            this.cmb_DevicePartID.Name = "cmb_DevicePartID";
            this.cmb_DevicePartID.Size = new System.Drawing.Size(212, 22);
            this.cmb_DevicePartID.TabIndex = 7;
            // 
            // cmb_DeviceID
            // 
            this.cmb_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DeviceID.FormattingEnabled = true;
            this.cmb_DeviceID.Location = new System.Drawing.Point(126, 31);
            this.cmb_DeviceID.Name = "cmb_DeviceID";
            this.cmb_DeviceID.Size = new System.Drawing.Size(212, 22);
            this.cmb_DeviceID.TabIndex = 6;
            this.cmb_DeviceID.SelectedIndexChanged += new System.EventHandler(this.Cmb_DeviceID_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(21, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "是否显示：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "标签地址：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "内存地址：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "报警信息：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "设备部件：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属设备：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FM_TB_Alarm_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 343);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FM_TB_Alarm_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报警信息编辑";
            this.Load += new System.EventHandler(this.FM_TB_Alarm_Add_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_DeviceID;
        private System.Windows.Forms.TextBox txt_x;
        private System.Windows.Forms.TextBox txt_MemoryAddr;
        private System.Windows.Forms.TextBox txt_AlarmInfo;
        private System.Windows.Forms.ComboBox cmb_DevicePartID;
        private System.Windows.Forms.ComboBox cmb_IsDisplay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}