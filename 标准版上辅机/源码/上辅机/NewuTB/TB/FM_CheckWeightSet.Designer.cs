
namespace NewuTB.TB
{
    partial class FM_CheckWeightSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_CheckWeightSet));
            this.lb_WeightSet = new System.Windows.Forms.Label();
            this.lb_AllowError = new System.Windows.Forms.Label();
            this.tb_WeightSet = new System.Windows.Forms.TextBox();
            this.tb_AllowError = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_WeightSet
            // 
            this.lb_WeightSet.Location = new System.Drawing.Point(84, 57);
            this.lb_WeightSet.Name = "lb_WeightSet";
            this.lb_WeightSet.Size = new System.Drawing.Size(108, 14);
            this.lb_WeightSet.TabIndex = 0;
            this.lb_WeightSet.Text = "设定重量：";
            this.lb_WeightSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_AllowError
            // 
            this.lb_AllowError.Location = new System.Drawing.Point(86, 106);
            this.lb_AllowError.Name = "lb_AllowError";
            this.lb_AllowError.Size = new System.Drawing.Size(106, 14);
            this.lb_AllowError.TabIndex = 1;
            this.lb_AllowError.Text = "设定误差：";
            this.lb_AllowError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_WeightSet
            // 
            this.tb_WeightSet.Location = new System.Drawing.Point(199, 54);
            this.tb_WeightSet.Name = "tb_WeightSet";
            this.tb_WeightSet.Size = new System.Drawing.Size(276, 23);
            this.tb_WeightSet.TabIndex = 3;
            // 
            // tb_AllowError
            // 
            this.tb_AllowError.Location = new System.Drawing.Point(199, 103);
            this.tb_AllowError.Name = "tb_AllowError";
            this.tb_AllowError.Size = new System.Drawing.Size(276, 23);
            this.tb_AllowError.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(338, 175);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 6;
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
            this.btnSave.Location = new System.Drawing.Point(199, 175);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnSave.Size = new System.Drawing.Size(76, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(89, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "说明：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(199, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "参数以“/”进行拼接";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FM_CheckWeightSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 250);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tb_AllowError);
            this.Controls.Add(this.tb_WeightSet);
            this.Controls.Add(this.lb_AllowError);
            this.Controls.Add(this.lb_WeightSet);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_CheckWeightSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动校秤设定";
            this.Load += new System.EventHandler(this.FM_CheckWeightSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_WeightSet;
        private System.Windows.Forms.Label lb_AllowError;
        private System.Windows.Forms.TextBox tb_WeightSet;
        private System.Windows.Forms.TextBox tb_AllowError;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}