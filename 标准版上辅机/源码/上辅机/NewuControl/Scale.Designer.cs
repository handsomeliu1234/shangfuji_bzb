namespace NewuCommon
{
    partial class Scale
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scale));
            this.labUnit = new System.Windows.Forms.Label();
            this.txtScaleValue = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picRun = new System.Windows.Forms.PictureBox();
            this.labScaleBatch = new System.Windows.Forms.Label();
            this.picAuto = new System.Windows.Forms.PictureBox();
            this.picAlarm = new System.Windows.Forms.PictureBox();
            this.picManual = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnScaleName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAuto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picManual)).BeginInit();
            this.SuspendLayout();
            // 
            // labUnit
            // 
            this.labUnit.AutoSize = true;
            this.labUnit.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUnit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labUnit.Location = new System.Drawing.Point(91, 23);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(17, 12);
            this.labUnit.TabIndex = 2;
            this.labUnit.Text = "kg";
            // 
            // txtScaleValue
            // 
            this.txtScaleValue.BackColor = System.Drawing.Color.Black;
            this.txtScaleValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScaleValue.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScaleValue.ForeColor = System.Drawing.Color.White;
            this.txtScaleValue.Location = new System.Drawing.Point(1, 20);
            this.txtScaleValue.Name = "txtScaleValue";
            this.txtScaleValue.ReadOnly = true;
            this.txtScaleValue.Size = new System.Drawing.Size(106, 19);
            this.txtScaleValue.TabIndex = 4;
            this.txtScaleValue.Text = "100.23";
            this.txtScaleValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.picRun);
            this.panel1.Controls.Add(this.labScaleBatch);
            this.panel1.Controls.Add(this.picAuto);
            this.panel1.Controls.Add(this.picAlarm);
            this.panel1.Controls.Add(this.picManual);
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 23);
            this.panel1.TabIndex = 5;
            // 
            // picRun
            // 
            this.picRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.picRun.Image = ((System.Drawing.Image)(resources.GetObject("picRun.Image")));
            this.picRun.InitialImage = ((System.Drawing.Image)(resources.GetObject("picRun.InitialImage")));
            this.picRun.Location = new System.Drawing.Point(43, 1);
            this.picRun.Name = "picRun";
            this.picRun.Size = new System.Drawing.Size(20, 20);
            this.picRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRun.TabIndex = 8;
            this.picRun.TabStop = false;
            this.picRun.Visible = false;
            // 
            // labScaleBatch
            // 
            this.labScaleBatch.AutoSize = true;
            this.labScaleBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.labScaleBatch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labScaleBatch.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labScaleBatch.ForeColor = System.Drawing.Color.White;
            this.labScaleBatch.Location = new System.Drawing.Point(0, 3);
            this.labScaleBatch.Name = "labScaleBatch";
            this.labScaleBatch.Size = new System.Drawing.Size(44, 16);
            this.labScaleBatch.TabIndex = 7;
            this.labScaleBatch.Text = "9999";
            this.labScaleBatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picAuto
            // 
            this.picAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.picAuto.Image = ((System.Drawing.Image)(resources.GetObject("picAuto.Image")));
            this.picAuto.Location = new System.Drawing.Point(85, 1);
            this.picAuto.Name = "picAuto";
            this.picAuto.Size = new System.Drawing.Size(20, 20);
            this.picAuto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAuto.TabIndex = 6;
            this.picAuto.TabStop = false;
            // 
            // picAlarm
            // 
            this.picAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.picAlarm.Image = ((System.Drawing.Image)(resources.GetObject("picAlarm.Image")));
            this.picAlarm.Location = new System.Drawing.Point(64, 1);
            this.picAlarm.Name = "picAlarm";
            this.picAlarm.Size = new System.Drawing.Size(20, 20);
            this.picAlarm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAlarm.TabIndex = 5;
            this.picAlarm.TabStop = false;
            this.picAlarm.Visible = false;
            // 
            // picManual
            // 
            this.picManual.Image = ((System.Drawing.Image)(resources.GetObject("picManual.Image")));
            this.picManual.Location = new System.Drawing.Point(85, 1);
            this.picManual.Name = "picManual";
            this.picManual.Size = new System.Drawing.Size(22, 22);
            this.picManual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picManual.TabIndex = 4;
            this.picManual.TabStop = false;
            this.picManual.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // btnScaleName
            // 
            this.btnScaleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScaleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btnScaleName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.btnScaleName.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScaleName.ForeColor = System.Drawing.Color.White;
            this.btnScaleName.Location = new System.Drawing.Point(0, 0);
            this.btnScaleName.Name = "btnScaleName";
            this.btnScaleName.Size = new System.Drawing.Size(106, 16);
            this.btnScaleName.TabIndex = 6;
            this.btnScaleName.Text = "磅秤名称";
            this.btnScaleName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Scale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.btnScaleName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labUnit);
            this.Controls.Add(this.txtScaleValue);
            this.Name = "Scale";
            this.Size = new System.Drawing.Size(106, 60);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAuto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picManual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.TextBox txtScaleValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picManual;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picAlarm;
        private System.Windows.Forms.PictureBox picAuto;
        private System.Windows.Forms.Label labScaleBatch;
        private System.Windows.Forms.PictureBox picRun;
        private System.Windows.Forms.TextBox btnScaleName;
    }
}
