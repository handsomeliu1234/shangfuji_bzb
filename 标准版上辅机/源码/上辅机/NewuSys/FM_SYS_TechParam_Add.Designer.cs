namespace NewuSys
{
    partial class FM_SYS_TechParam_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_SYS_TechParam_Add));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Unit = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_DecDigit = new System.Windows.Forms.TextBox();
            this.txt_TechParamOrder = new System.Windows.Forms.TextBox();
            this.cmb_DevicePartID = new System.Windows.Forms.ComboBox();
            this.cmb_DeviceID = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_Enabled = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_TechParamNameEN = new System.Windows.Forms.TextBox();
            this.txt_TechParamNameCN = new System.Windows.Forms.TextBox();
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
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(442, 341);
            this.splitContainer1.SplitterDistance = 57;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(101, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(65, 30);
            this.btnClose.TabIndex = 9;
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
            this.btnSave.Location = new System.Drawing.Point(20, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(65, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Unit);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_DecDigit);
            this.groupBox1.Controls.Add(this.txt_TechParamOrder);
            this.groupBox1.Controls.Add(this.cmb_DevicePartID);
            this.groupBox1.Controls.Add(this.cmb_DeviceID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmb_Enabled);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_TechParamNameEN);
            this.groupBox1.Controls.Add(this.txt_TechParamNameCN);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 280);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统详细参数信息";
            // 
            // txt_Unit
            // 
            this.txt_Unit.AcceptsReturn = true;
            this.txt_Unit.Location = new System.Drawing.Point(124, 182);
            this.txt_Unit.Name = "txt_Unit";
            this.txt_Unit.Size = new System.Drawing.Size(214, 21);
            this.txt_Unit.TabIndex = 102;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(3, 185);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 12);
            this.label15.TabIndex = 101;
            this.label15.Text = "单位：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(342, 215);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 100;
            this.label13.Text = "*不能为空";
            // 
            // txt_DecDigit
            // 
            this.txt_DecDigit.Location = new System.Drawing.Point(124, 212);
            this.txt_DecDigit.Name = "txt_DecDigit";
            this.txt_DecDigit.Size = new System.Drawing.Size(214, 21);
            this.txt_DecDigit.TabIndex = 96;
            // 
            // txt_TechParamOrder
            // 
            this.txt_TechParamOrder.AcceptsReturn = true;
            this.txt_TechParamOrder.Location = new System.Drawing.Point(124, 152);
            this.txt_TechParamOrder.Name = "txt_TechParamOrder";
            this.txt_TechParamOrder.Size = new System.Drawing.Size(214, 21);
            this.txt_TechParamOrder.TabIndex = 95;
            // 
            // cmb_DevicePartID
            // 
            this.cmb_DevicePartID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DevicePartID.FormattingEnabled = true;
            this.cmb_DevicePartID.Location = new System.Drawing.Point(125, 62);
            this.cmb_DevicePartID.Name = "cmb_DevicePartID";
            this.cmb_DevicePartID.Size = new System.Drawing.Size(213, 20);
            this.cmb_DevicePartID.TabIndex = 94;
            // 
            // cmb_DeviceID
            // 
            this.cmb_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DeviceID.FormattingEnabled = true;
            this.cmb_DeviceID.Location = new System.Drawing.Point(124, 32);
            this.cmb_DeviceID.Name = "cmb_DeviceID";
            this.cmb_DeviceID.Size = new System.Drawing.Size(214, 20);
            this.cmb_DeviceID.TabIndex = 92;
            this.cmb_DeviceID.SelectedIndexChanged += new System.EventHandler(this.Cmb_DeviceID_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(342, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 88;
            this.label5.Text = "*不能为空";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 215);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 12);
            this.label11.TabIndex = 85;
            this.label11.Text = "小数位：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 155);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 12);
            this.label12.TabIndex = 84;
            this.label12.Text = "工艺参数顺序：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(187, 247);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 59;
            this.label10.Text = "*不能为空";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(342, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 58;
            this.label7.Text = "*不能为空";
            // 
            // cmb_Enabled
            // 
            this.cmb_Enabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Enabled.FormattingEnabled = true;
            this.cmb_Enabled.Location = new System.Drawing.Point(125, 243);
            this.cmb_Enabled.Name = "cmb_Enabled";
            this.cmb_Enabled.Size = new System.Drawing.Size(57, 20);
            this.cmb_Enabled.TabIndex = 57;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 247);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 12);
            this.label9.TabIndex = 56;
            this.label9.Text = "是否可用：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_TechParamNameEN
            // 
            this.txt_TechParamNameEN.AcceptsReturn = true;
            this.txt_TechParamNameEN.Location = new System.Drawing.Point(125, 122);
            this.txt_TechParamNameEN.Name = "txt_TechParamNameEN";
            this.txt_TechParamNameEN.Size = new System.Drawing.Size(213, 21);
            this.txt_TechParamNameEN.TabIndex = 50;
            // 
            // txt_TechParamNameCN
            // 
            this.txt_TechParamNameCN.AcceptsReturn = true;
            this.txt_TechParamNameCN.Location = new System.Drawing.Point(124, 92);
            this.txt_TechParamNameCN.Name = "txt_TechParamNameCN";
            this.txt_TechParamNameCN.Size = new System.Drawing.Size(214, 21);
            this.txt_TechParamNameCN.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "工艺参数名称英文：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "工艺参数名称中文：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "设备部件：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 12);
            this.label1.TabIndex = 45;
            this.label1.Text = "设备名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FM_SYS_TechParam_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 341);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FM_SYS_TechParam_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统工艺参数编辑";
            this.Load += new System.EventHandler(this.FM_SYS_TechParam_Add_Load);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_TechParamNameEN;
        private System.Windows.Forms.TextBox txt_TechParamNameCN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_DecDigit;
        private System.Windows.Forms.TextBox txt_TechParamOrder;
        private System.Windows.Forms.ComboBox cmb_DevicePartID;
        private System.Windows.Forms.ComboBox cmb_DeviceID;
        private System.Windows.Forms.ComboBox cmb_Enabled;
        private System.Windows.Forms.TextBox txt_Unit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}