namespace NewuView
{
    partial class FM_InternalSoftwarSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_InternalSoftwarSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSvnVer = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.txtSoftwareVer = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDataBaseConfig = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtYears = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtDisk = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnExportDisk = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "软件版本";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Svn版本";
            // 
            // txtSvnVer
            // 
            this.txtSvnVer.Location = new System.Drawing.Point(115, 62);
            this.txtSvnVer.Name = "txtSvnVer";
            this.txtSvnVer.Size = new System.Drawing.Size(61, 23);
            this.txtSvnVer.TabIndex = 3;
            this.txtSvnVer.Text = "804";
            this.txtSvnVer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSvnVer_KeyPress);
            // 
            // btSave
            // 
            this.btSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.Image = ((System.Drawing.Image)(resources.GetObject("btSave.Image")));
            this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSave.Location = new System.Drawing.Point(32, 174);
            this.btSave.Name = "btSave";
            this.btSave.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btSave.Size = new System.Drawing.Size(76, 30);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "保存";
            this.btSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btClose
            // 
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Image = ((System.Drawing.Image)(resources.GetObject("btClose.Image")));
            this.btClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btClose.Location = new System.Drawing.Point(136, 174);
            this.btClose.Name = "btClose";
            this.btClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btClose.Size = new System.Drawing.Size(76, 30);
            this.btClose.TabIndex = 7;
            this.btClose.Text = "关闭";
            this.btClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSoftwareVer
            // 
            this.txtSoftwareVer.Location = new System.Drawing.Point(115, 22);
            this.txtSoftwareVer.Name = "txtSoftwareVer";
            this.txtSoftwareVer.Size = new System.Drawing.Size(61, 23);
            this.txtSoftwareVer.TabIndex = 8;
            this.txtSoftwareVer.Text = "2.0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(254, 237);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtSoftwareVer);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btSave);
            this.tabPage1.Controls.Add(this.txtSvnVer);
            this.tabPage1.Controls.Add(this.btClose);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(246, 209);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "软件版本";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDataBaseConfig);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.txtYears);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.cbEnable);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(246, 209);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据库清理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDataBaseConfig
            // 
            this.btnDataBaseConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDataBaseConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataBaseConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnDataBaseConfig.Image")));
            this.btnDataBaseConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataBaseConfig.Location = new System.Drawing.Point(27, 172);
            this.btnDataBaseConfig.Name = "btnDataBaseConfig";
            this.btnDataBaseConfig.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDataBaseConfig.Size = new System.Drawing.Size(71, 30);
            this.btnDataBaseConfig.TabIndex = 12;
            this.btnDataBaseConfig.Text = "保存";
            this.btnDataBaseConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDataBaseConfig.UseVisualStyleBackColor = true;
            this.btnDataBaseConfig.Click += new System.EventHandler(this.btnDataBaseConfig_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(138, 173);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.button2.Size = new System.Drawing.Size(71, 30);
            this.button2.TabIndex = 13;
            this.button2.Text = "关闭";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtYears
            // 
            this.txtYears.Location = new System.Drawing.Point(120, 70);
            this.txtYears.Name = "txtYears";
            this.txtYears.Size = new System.Drawing.Size(61, 23);
            this.txtYears.TabIndex = 11;
            this.txtYears.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYears_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "保留年数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "是否启用";
            // 
            // cbEnable
            // 
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(120, 39);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(15, 14);
            this.cbEnable.TabIndex = 8;
            this.cbEnable.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtDisk);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.btnExportDisk);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(246, 209);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "导出文件盘符";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtDisk
            // 
            this.txtDisk.Location = new System.Drawing.Point(78, 71);
            this.txtDisk.Name = "txtDisk";
            this.txtDisk.Size = new System.Drawing.Size(87, 23);
            this.txtDisk.TabIndex = 16;
            this.txtDisk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDisk_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "盘符";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(124, 171);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.button3.Size = new System.Drawing.Size(71, 30);
            this.button3.TabIndex = 14;
            this.button3.Text = "关闭";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnExportDisk
            // 
            this.btnExportDisk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExportDisk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportDisk.Image = ((System.Drawing.Image)(resources.GetObject("btnExportDisk.Image")));
            this.btnExportDisk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportDisk.Location = new System.Drawing.Point(18, 173);
            this.btnExportDisk.Name = "btnExportDisk";
            this.btnExportDisk.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnExportDisk.Size = new System.Drawing.Size(71, 30);
            this.btnExportDisk.TabIndex = 13;
            this.btnExportDisk.Text = "保存";
            this.btnExportDisk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportDisk.UseVisualStyleBackColor = true;
            this.btnExportDisk.Click += new System.EventHandler(this.btnExportDisk_Click);
            // 
            // FM_InternalSoftwarSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 237);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_InternalSoftwarSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件内部设置";
            this.Load += new System.EventHandler(this.FM_InternalSoftwarSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSvnVer;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TextBox txtSoftwareVer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDataBaseConfig;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtYears;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbEnable;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtDisk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnExportDisk;
    }
}