namespace NewuCommon
{
    partial class LuoXuan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LuoXuan));
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picRun = new System.Windows.Forms.PictureBox();
            this.picStopRight = new System.Windows.Forms.PictureBox();
            this.picRunRight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStopRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRunRight)).BeginInit();
            this.SuspendLayout();
            // 
            // picStop
            // 
            this.picStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picStop.Image = ((System.Drawing.Image)(resources.GetObject("picStop.Image")));
            this.picStop.Location = new System.Drawing.Point(0, 0);
            this.picStop.Name = "picStop";
            this.picStop.Size = new System.Drawing.Size(765, 162);
            this.picStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStop.TabIndex = 0;
            this.picStop.TabStop = false;
            // 
            // picRun
            // 
            this.picRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picRun.Image = ((System.Drawing.Image)(resources.GetObject("picRun.Image")));
            this.picRun.Location = new System.Drawing.Point(0, 0);
            this.picRun.Name = "picRun";
            this.picRun.Size = new System.Drawing.Size(765, 162);
            this.picRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRun.TabIndex = 1;
            this.picRun.TabStop = false;
            // 
            // picStopRight
            // 
            this.picStopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picStopRight.Image = ((System.Drawing.Image)(resources.GetObject("picStopRight.Image")));
            this.picStopRight.Location = new System.Drawing.Point(0, 0);
            this.picStopRight.Name = "picStopRight";
            this.picStopRight.Size = new System.Drawing.Size(765, 162);
            this.picStopRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStopRight.TabIndex = 2;
            this.picStopRight.TabStop = false;
            // 
            // picRunRight
            // 
            this.picRunRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picRunRight.Image = ((System.Drawing.Image)(resources.GetObject("picRunRight.Image")));
            this.picRunRight.Location = new System.Drawing.Point(0, 0);
            this.picRunRight.Name = "picRunRight";
            this.picRunRight.Size = new System.Drawing.Size(765, 162);
            this.picRunRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRunRight.TabIndex = 3;
            this.picRunRight.TabStop = false;
            // 
            // LuoXuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picRunRight);
            this.Controls.Add(this.picStopRight);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picRun);
            this.Name = "LuoXuan";
            this.Size = new System.Drawing.Size(765, 162);
            this.SizeChanged += new System.EventHandler(this.LuoXuan_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStopRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRunRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picStop;
        private System.Windows.Forms.PictureBox picRun;
        private System.Windows.Forms.PictureBox picStopRight;
        private System.Windows.Forms.PictureBox picRunRight;
    }
}
