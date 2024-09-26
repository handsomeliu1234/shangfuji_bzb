namespace NewuTB.TB
{
    partial class FM_TB_UserInfo_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_TB_UserInfo_Add));
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_RealName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_RoleID = new System.Windows.Forms.ComboBox();
            this.cmb_DepartmentID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Jobs = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Phone = new System.Windows.Forms.TextBox();
            this.txt_UserPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_UserCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(334, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "*不能为空";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(334, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "*不能为空";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "真实姓名：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_RealName
            // 
            this.txt_RealName.AcceptsReturn = true;
            this.txt_RealName.Location = new System.Drawing.Point(106, 186);
            this.txt_RealName.Name = "txt_RealName";
            this.txt_RealName.Size = new System.Drawing.Size(213, 21);
            this.txt_RealName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "用户密码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(446, 372);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(91, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(65, 30);
            this.btnClose.TabIndex = 2;
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
            this.btnSave.Location = new System.Drawing.Point(14, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(65, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_RoleID);
            this.groupBox1.Controls.Add(this.cmb_DepartmentID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_Jobs);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_Phone);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_RealName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_UserPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_UserCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 321);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "车间详细信息";
            // 
            // cmb_RoleID
            // 
            this.cmb_RoleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_RoleID.FormattingEnabled = true;
            this.cmb_RoleID.Location = new System.Drawing.Point(106, 69);
            this.cmb_RoleID.Name = "cmb_RoleID";
            this.cmb_RoleID.Size = new System.Drawing.Size(213, 20);
            this.cmb_RoleID.TabIndex = 49;
            // 
            // cmb_DepartmentID
            // 
            this.cmb_DepartmentID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DepartmentID.FormattingEnabled = true;
            this.cmb_DepartmentID.Location = new System.Drawing.Point(106, 30);
            this.cmb_DepartmentID.Name = "cmb_DepartmentID";
            this.cmb_DepartmentID.Size = new System.Drawing.Size(213, 20);
            this.cmb_DepartmentID.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(334, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 47;
            this.label4.Text = "*不能为空";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(334, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "*不能为空";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "用户角色：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(14, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 12);
            this.label10.TabIndex = 43;
            this.label10.Text = "用户部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(334, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 41;
            this.label9.Text = "*不能为空";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(14, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "用户岗位：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Jobs
            // 
            this.txt_Jobs.Location = new System.Drawing.Point(106, 264);
            this.txt_Jobs.Name = "txt_Jobs";
            this.txt_Jobs.Size = new System.Drawing.Size(213, 21);
            this.txt_Jobs.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(14, 228);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 12);
            this.label12.TabIndex = 36;
            this.label12.Text = "用户电话：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Phone
            // 
            this.txt_Phone.Location = new System.Drawing.Point(106, 225);
            this.txt_Phone.Name = "txt_Phone";
            this.txt_Phone.Size = new System.Drawing.Size(213, 21);
            this.txt_Phone.TabIndex = 35;
            // 
            // txt_UserPassword
            // 
            this.txt_UserPassword.Location = new System.Drawing.Point(106, 147);
            this.txt_UserPassword.Name = "txt_UserPassword";
            this.txt_UserPassword.Size = new System.Drawing.Size(213, 21);
            this.txt_UserPassword.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "用户编码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_UserCode
            // 
            this.txt_UserCode.Location = new System.Drawing.Point(106, 108);
            this.txt_UserCode.Name = "txt_UserCode";
            this.txt_UserCode.Size = new System.Drawing.Size(213, 21);
            this.txt_UserCode.TabIndex = 10;
            // 
            // FM_TB_UserInfo_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 372);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FM_TB_UserInfo_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息编辑";
            this.Load += new System.EventHandler(this.FM_TB_UserInfo_Add_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_RealName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_UserPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_UserCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Jobs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Phone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_RoleID;
        private System.Windows.Forms.ComboBox cmb_DepartmentID;
    }
}