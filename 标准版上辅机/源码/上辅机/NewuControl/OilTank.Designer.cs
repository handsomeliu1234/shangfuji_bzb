namespace NewuCommon
{
    partial class OilTank
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OilTank));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHigh = new NewuCommon.NewuPicAngle();
            this.lblLow = new NewuCommon.NewuPicAngle();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 81);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "test";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHigh
            // 
            this.lblHigh.BackColor = System.Drawing.Color.DimGray;
            this.lblHigh.ForeColor = System.Drawing.Color.Black;
            this.lblHigh.Location = new System.Drawing.Point(57, 29);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.NewuAngle = 0;
            this.lblHigh.NewuImgBg = null;
            this.lblHigh.NewuImgFore = null;
            this.lblHigh.NewuPicTypeStyle = NewuCommon.NewuPicType.Background;
            this.lblHigh.Size = new System.Drawing.Size(14, 52);
            this.lblHigh.TabIndex = 1028;
            // 
            // lblLow
            // 
            this.lblLow.BackColor = System.Drawing.Color.DimGray;
            this.lblLow.ForeColor = System.Drawing.Color.Black;
            this.lblLow.Location = new System.Drawing.Point(58, 51);
            this.lblLow.Name = "lblLow";
            this.lblLow.NewuAngle = 0;
            this.lblLow.NewuImgBg = null;
            this.lblLow.NewuImgFore = null;
            this.lblLow.NewuPicTypeStyle = NewuCommon.NewuPicType.Background;
            this.lblLow.Size = new System.Drawing.Size(13, 30);
            this.lblLow.TabIndex = 1029;
            // 
            // OilTank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLow);
            this.Controls.Add(this.lblHigh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "OilTank";
            this.Size = new System.Drawing.Size(71, 81);
            this.Load += new System.EventHandler(this.OilTank_Load);
            this.SizeChanged += new System.EventHandler(this.OilTank_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private NewuPicAngle lblHigh;
        private NewuPicAngle lblLow;
    }
}
