namespace NewuTB.Formula
{
    partial class FM_FormulaMix_AddRaw
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_WeighMaterial = new System.Windows.Forms.ComboBox();
            this.cmb_DropOrder = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_AllowError = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_WeighSetVal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_MaterialDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_Device = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_DevicePart = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_MaterialName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(588, 261);
            this.splitContainer1.TabIndex = 16;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(93, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 35);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(76, 35);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_WeighMaterial);
            this.groupBox2.Controls.Add(this.cmb_DropOrder);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_AllowError);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_WeighSetVal);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(11, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 95);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设定信息";
            // 
            // cmb_WeighMaterial
            // 
            this.cmb_WeighMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_WeighMaterial.FormattingEnabled = true;
            this.cmb_WeighMaterial.Location = new System.Drawing.Point(100, 29);
            this.cmb_WeighMaterial.Name = "cmb_WeighMaterial";
            this.cmb_WeighMaterial.Size = new System.Drawing.Size(159, 20);
            this.cmb_WeighMaterial.TabIndex = 22;
            // 
            // cmb_DropOrder
            // 
            this.cmb_DropOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DropOrder.FormattingEnabled = true;
            this.cmb_DropOrder.Location = new System.Drawing.Point(389, 29);
            this.cmb_DropOrder.Name = "cmb_DropOrder";
            this.cmb_DropOrder.Size = new System.Drawing.Size(159, 20);
            this.cmb_DropOrder.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "投料次序：";
            // 
            // txt_AllowError
            // 
            this.txt_AllowError.Location = new System.Drawing.Point(388, 55);
            this.txt_AllowError.Name = "txt_AllowError";
            this.txt_AllowError.Size = new System.Drawing.Size(160, 21);
            this.txt_AllowError.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "允许公差：";
            // 
            // txt_WeighSetVal
            // 
            this.txt_WeighSetVal.Location = new System.Drawing.Point(100, 55);
            this.txt_WeighSetVal.Name = "txt_WeighSetVal";
            this.txt_WeighSetVal.Size = new System.Drawing.Size(160, 21);
            this.txt_WeighSetVal.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "设定重量：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "称量材料：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_MaterialDesc);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmb_Device);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmb_DevicePart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_MaterialName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 96);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "默认信息";
            // 
            // txt_MaterialDesc
            // 
            this.txt_MaterialDesc.AcceptsReturn = true;
            this.txt_MaterialDesc.Location = new System.Drawing.Point(388, 33);
            this.txt_MaterialDesc.Name = "txt_MaterialDesc";
            this.txt_MaterialDesc.ReadOnly = true;
            this.txt_MaterialDesc.Size = new System.Drawing.Size(160, 21);
            this.txt_MaterialDesc.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(317, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "配方描述：";
            // 
            // cmb_Device
            // 
            this.cmb_Device.Enabled = false;
            this.cmb_Device.FormattingEnabled = true;
            this.cmb_Device.Location = new System.Drawing.Point(99, 60);
            this.cmb_Device.Name = "cmb_Device";
            this.cmb_Device.Size = new System.Drawing.Size(159, 20);
            this.cmb_Device.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "所属设备：";
            // 
            // cmb_DevicePart
            // 
            this.cmb_DevicePart.FormattingEnabled = true;
            this.cmb_DevicePart.Location = new System.Drawing.Point(388, 58);
            this.cmb_DevicePart.Name = "cmb_DevicePart";
            this.cmb_DevicePart.Size = new System.Drawing.Size(159, 20);
            this.cmb_DevicePart.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "称量部件：";
            // 
            // txt_MaterialName
            // 
            this.txt_MaterialName.AcceptsReturn = true;
            this.txt_MaterialName.Location = new System.Drawing.Point(99, 33);
            this.txt_MaterialName.Name = "txt_MaterialName";
            this.txt_MaterialName.ReadOnly = true;
            this.txt_MaterialName.Size = new System.Drawing.Size(160, 21);
            this.txt_MaterialName.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "配方编号：";
            // 
            // FM_FormulaMix_AddRaw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 261);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "FM_FormulaMix_AddRaw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配方称量数据";
            this.Load += new System.EventHandler(this.FM_FormulaMix_Add_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_DropOrder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_AllowError;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_WeighSetVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_MaterialName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmb_WeighMaterial;
        private System.Windows.Forms.ComboBox cmb_DevicePart;
        private System.Windows.Forms.ComboBox cmb_Device;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_MaterialDesc;
        private System.Windows.Forms.Label label8;


    }
}