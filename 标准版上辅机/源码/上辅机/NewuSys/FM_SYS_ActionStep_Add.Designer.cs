namespace NewuSys
{
    partial class FM_SYS_ActionStep_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_SYS_ActionStep_Add));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_StepValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_DeviceID = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_DevicePartID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_StepNameEN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_StepNameCN = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_Enabled = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(424, 258);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(102, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(65, 30);
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
            this.btnSave.Location = new System.Drawing.Point(20, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(65, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmb_Enabled);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_StepValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmb_DeviceID);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmb_DevicePartID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_StepNameEN);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_StepNameCN);
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "动作步骤信息";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(272, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 47;
            this.label6.Text = "*不能为空";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(272, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "*不能为空";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "动作步骤值：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_StepValue
            // 
            this.txt_StepValue.AcceptsReturn = true;
            this.txt_StepValue.Location = new System.Drawing.Point(147, 87);
            this.txt_StepValue.Name = "txt_StepValue";
            this.txt_StepValue.Size = new System.Drawing.Size(114, 21);
            this.txt_StepValue.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(272, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 43;
            this.label4.Text = "*不能为空";
            // 
            // cmb_DeviceID
            // 
            this.cmb_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DeviceID.FormattingEnabled = true;
            this.cmb_DeviceID.Location = new System.Drawing.Point(147, 114);
            this.cmb_DeviceID.Name = "cmb_DeviceID";
            this.cmb_DeviceID.Size = new System.Drawing.Size(114, 20);
            this.cmb_DeviceID.TabIndex = 42;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(272, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 40;
            this.label11.Text = "*不能为空";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(272, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 39;
            this.label10.Text = "*不能为空";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "所属部件：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 12);
            this.label7.TabIndex = 36;
            this.label7.Text = "所属设备：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_DevicePartID
            // 
            this.cmb_DevicePartID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DevicePartID.FormattingEnabled = true;
            this.cmb_DevicePartID.Location = new System.Drawing.Point(147, 140);
            this.cmb_DevicePartID.Name = "cmb_DevicePartID";
            this.cmb_DevicePartID.Size = new System.Drawing.Size(114, 20);
            this.cmb_DevicePartID.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "步骤英文：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_StepNameEN
            // 
            this.txt_StepNameEN.AcceptsReturn = true;
            this.txt_StepNameEN.Location = new System.Drawing.Point(147, 60);
            this.txt_StepNameEN.Name = "txt_StepNameEN";
            this.txt_StepNameEN.Size = new System.Drawing.Size(114, 21);
            this.txt_StepNameEN.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "步骤名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_StepNameCN
            // 
            this.txt_StepNameCN.Location = new System.Drawing.Point(147, 33);
            this.txt_StepNameCN.Name = "txt_StepNameCN";
            this.txt_StepNameCN.Size = new System.Drawing.Size(114, 21);
            this.txt_StepNameCN.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(272, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 68;
            this.label8.Text = "*不能为空";
            // 
            // cmb_Enabled
            // 
            this.cmb_Enabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Enabled.FormattingEnabled = true;
            this.cmb_Enabled.Location = new System.Drawing.Point(147, 168);
            this.cmb_Enabled.Name = "cmb_Enabled";
            this.cmb_Enabled.Size = new System.Drawing.Size(67, 20);
            this.cmb_Enabled.TabIndex = 67;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(76, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 66;
            this.label12.Text = "是否可用：";
            // 
            // FM_SYS_ActionStep_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 258);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FM_SYS_ActionStep_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "动作步骤编辑";
            this.Load += new System.EventHandler(this.FM_SYS_ActionStep_Add_Load);
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
        private System.Windows.Forms.ComboBox cmb_DevicePartID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_StepNameEN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_StepNameCN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_DeviceID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_StepValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_Enabled;
        private System.Windows.Forms.Label label12;
    }
}