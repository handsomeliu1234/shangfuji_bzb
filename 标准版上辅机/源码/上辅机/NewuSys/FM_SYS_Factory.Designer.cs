namespace NewuSys
{
    partial class FM_SYS_Factory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_SYS_Factory));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_FactoryAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_FactoryEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_FactoryPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_FactorySite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_FactoryJaneSpell = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_FactoryCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_FactoryName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(854, 415);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(118, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
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
            this.btnSave.Location = new System.Drawing.Point(23, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnSave.Size = new System.Drawing.Size(76, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_FactoryAddress);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_FactoryEmail);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_FactoryPhone);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_FactorySite);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_FactoryJaneSpell);
            this.groupBox2.Location = new System.Drawing.Point(14, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(826, 213);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详细信息";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(27, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 14);
            this.label7.TabIndex = 40;
            this.label7.Text = "工厂邮箱：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactoryAddress
            // 
            this.txt_FactoryAddress.Location = new System.Drawing.Point(226, 167);
            this.txt_FactoryAddress.Name = "txt_FactoryAddress";
            this.txt_FactoryAddress.Size = new System.Drawing.Size(443, 23);
            this.txt_FactoryAddress.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(27, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 14);
            this.label6.TabIndex = 38;
            this.label6.Text = "工厂地址：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactoryEmail
            // 
            this.txt_FactoryEmail.Location = new System.Drawing.Point(226, 135);
            this.txt_FactoryEmail.Name = "txt_FactoryEmail";
            this.txt_FactoryEmail.Size = new System.Drawing.Size(443, 23);
            this.txt_FactoryEmail.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(27, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 14);
            this.label5.TabIndex = 36;
            this.label5.Text = "工厂电话：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactoryPhone
            // 
            this.txt_FactoryPhone.AcceptsReturn = true;
            this.txt_FactoryPhone.Location = new System.Drawing.Point(226, 104);
            this.txt_FactoryPhone.Name = "txt_FactoryPhone";
            this.txt_FactoryPhone.Size = new System.Drawing.Size(443, 23);
            this.txt_FactoryPhone.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 14);
            this.label4.TabIndex = 34;
            this.label4.Text = "工厂网址：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactorySite
            // 
            this.txt_FactorySite.AcceptsReturn = true;
            this.txt_FactorySite.Location = new System.Drawing.Point(226, 72);
            this.txt_FactorySite.Name = "txt_FactorySite";
            this.txt_FactorySite.Size = new System.Drawing.Size(443, 23);
            this.txt_FactorySite.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(27, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 14);
            this.label3.TabIndex = 32;
            this.label3.Text = "工厂简拼：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactoryJaneSpell
            // 
            this.txt_FactoryJaneSpell.Location = new System.Drawing.Point(226, 41);
            this.txt_FactoryJaneSpell.Name = "txt_FactoryJaneSpell";
            this.txt_FactoryJaneSpell.Size = new System.Drawing.Size(443, 23);
            this.txt_FactoryJaneSpell.TabIndex = 31;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_FactoryCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_FactoryName);
            this.groupBox1.Location = new System.Drawing.Point(14, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(836, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主要信息";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(677, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 32;
            this.label9.Text = "*不能为空";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(677, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 31;
            this.label8.Text = "*不能为空";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(83, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "工厂编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactoryCode
            // 
            this.txt_FactoryCode.Location = new System.Drawing.Point(226, 80);
            this.txt_FactoryCode.Name = "txt_FactoryCode";
            this.txt_FactoryCode.Size = new System.Drawing.Size(443, 23);
            this.txt_FactoryCode.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(83, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "工厂名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_FactoryName
            // 
            this.txt_FactoryName.AcceptsTab = true;
            this.txt_FactoryName.Location = new System.Drawing.Point(226, 34);
            this.txt_FactoryName.Name = "txt_FactoryName";
            this.txt_FactoryName.Size = new System.Drawing.Size(443, 23);
            this.txt_FactoryName.TabIndex = 17;
            // 
            // FM_SYS_Factory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 415);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FM_SYS_Factory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工厂信息编辑";
            this.Load += new System.EventHandler(this.FM_SYS_Factory_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_FactoryCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_FactoryName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_FactoryAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_FactoryEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_FactoryPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_FactorySite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_FactoryJaneSpell;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}