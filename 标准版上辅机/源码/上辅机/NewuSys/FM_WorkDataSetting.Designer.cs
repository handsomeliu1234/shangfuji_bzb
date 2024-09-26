namespace NewuSys
{
    partial class FM_WorkDataSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_WorkDataSetting));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tb_OrderSet = new System.Windows.Forms.TextBox();
            this.tb_GroupSet = new System.Windows.Forms.TextBox();
            this.lb_OrderSet = new System.Windows.Forms.Label();
            this.lb_GroupSet = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(172, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "参数以“/”进行拼接";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(44, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "说明：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(289, 172);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 14;
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
            this.btnSave.Location = new System.Drawing.Point(172, 172);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this.btnSave.Size = new System.Drawing.Size(76, 30);
            this.btnSave.TabIndex = 13;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tb_OrderSet
            // 
            this.tb_OrderSet.Location = new System.Drawing.Point(175, 89);
            this.tb_OrderSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_OrderSet.Name = "tb_OrderSet";
            this.tb_OrderSet.Size = new System.Drawing.Size(321, 23);
            this.tb_OrderSet.TabIndex = 12;
            // 
            // tb_GroupSet
            // 
            this.tb_GroupSet.Location = new System.Drawing.Point(175, 37);
            this.tb_GroupSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_GroupSet.Name = "tb_GroupSet";
            this.tb_GroupSet.Size = new System.Drawing.Size(321, 23);
            this.tb_GroupSet.TabIndex = 11;
            // 
            // lb_OrderSet
            // 
            this.lb_OrderSet.Location = new System.Drawing.Point(34, 92);
            this.lb_OrderSet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_OrderSet.Name = "lb_OrderSet";
            this.lb_OrderSet.Size = new System.Drawing.Size(133, 16);
            this.lb_OrderSet.TabIndex = 10;
            this.lb_OrderSet.Text = "班次：";
            this.lb_OrderSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_GroupSet
            // 
            this.lb_GroupSet.Location = new System.Drawing.Point(31, 41);
            this.lb_GroupSet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_GroupSet.Name = "lb_GroupSet";
            this.lb_GroupSet.Size = new System.Drawing.Size(136, 16);
            this.lb_GroupSet.TabIndex = 9;
            this.lb_GroupSet.Text = "班组：";
            this.lb_GroupSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FM_WorkDataSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 249);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tb_OrderSet);
            this.Controls.Add(this.tb_GroupSet);
            this.Controls.Add(this.lb_OrderSet);
            this.Controls.Add(this.lb_GroupSet);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FM_WorkDataSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FM_WorkDataSetting";
            this.Load += new System.EventHandler(this.FM_WorkDataSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tb_OrderSet;
        private System.Windows.Forms.TextBox tb_GroupSet;
        private System.Windows.Forms.Label lb_OrderSet;
        private System.Windows.Forms.Label lb_GroupSet;
    }
}