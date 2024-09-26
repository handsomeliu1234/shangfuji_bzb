namespace NewuCommon
{
    partial class ScaleDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleDetail));
            this.btnScaleName = new System.Windows.Forms.Button();
            this.labUnit = new System.Windows.Forms.Label();
            this.txtScaleValue = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picAuto = new System.Windows.Forms.PictureBox();
            this.picAlarm = new System.Windows.Forms.PictureBox();
            this.picManual = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labDisplay = new System.Windows.Forms.Label();
            this.picRun = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAuto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRun)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScaleName
            // 
            this.btnScaleName.Enabled = false;
            this.btnScaleName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScaleName.Location = new System.Drawing.Point(3, -1);
            this.btnScaleName.Name = "btnScaleName";
            this.btnScaleName.Size = new System.Drawing.Size(155, 20);
            this.btnScaleName.TabIndex = 0;
            this.btnScaleName.Text = "磅秤名称";
            this.btnScaleName.UseVisualStyleBackColor = true;
            // 
            // labUnit
            // 
            this.labUnit.AutoSize = true;
            this.labUnit.ForeColor = System.Drawing.Color.White;
            this.labUnit.Location = new System.Drawing.Point(144, 24);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(17, 12);
            this.labUnit.TabIndex = 2;
            this.labUnit.Text = "kg";
            // 
            // txtScaleValue
            // 
            this.txtScaleValue.BackColor = System.Drawing.Color.Black;
            this.txtScaleValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScaleValue.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.txtScaleValue.ForeColor = System.Drawing.Color.Red;
            this.txtScaleValue.Location = new System.Drawing.Point(0, 21);
            this.txtScaleValue.Name = "txtScaleValue";
            this.txtScaleValue.ReadOnly = true;
            this.txtScaleValue.Size = new System.Drawing.Size(161, 22);
            this.txtScaleValue.TabIndex = 4;
            this.txtScaleValue.Text = "100.23";
            this.txtScaleValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GrayText;
            this.panel1.Controls.Add(this.picRun);
            this.panel1.Controls.Add(this.picAuto);
            this.panel1.Controls.Add(this.picAlarm);
            this.panel1.Controls.Add(this.picManual);
            this.panel1.Location = new System.Drawing.Point(3, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 25);
            this.panel1.TabIndex = 5;
            // 
            // picAuto
            // 
            this.picAuto.Image = ((System.Drawing.Image)(resources.GetObject("picAuto.Image")));
            this.picAuto.Location = new System.Drawing.Point(130, 0);
            this.picAuto.Name = "picAuto";
            this.picAuto.Size = new System.Drawing.Size(25, 25);
            this.picAuto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAuto.TabIndex = 6;
            this.picAuto.TabStop = false;
            // 
            // picAlarm
            // 
            this.picAlarm.Image = ((System.Drawing.Image)(resources.GetObject("picAlarm.Image")));
            this.picAlarm.Location = new System.Drawing.Point(101, 0);
            this.picAlarm.Name = "picAlarm";
            this.picAlarm.Size = new System.Drawing.Size(25, 25);
            this.picAlarm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAlarm.TabIndex = 5;
            this.picAlarm.TabStop = false;
            this.picAlarm.Visible = false;
            // 
            // picManual
            // 
            this.picManual.Image = ((System.Drawing.Image)(resources.GetObject("picManual.Image")));
            this.picManual.Location = new System.Drawing.Point(130, 0);
            this.picManual.Name = "picManual";
            this.picManual.Size = new System.Drawing.Size(25, 25);
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
            // labDisplay
            // 
            this.labDisplay.AutoSize = true;
            this.labDisplay.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labDisplay.ForeColor = System.Drawing.SystemColors.Control;
            this.labDisplay.Location = new System.Drawing.Point(6, 47);
            this.labDisplay.Name = "labDisplay";
            this.labDisplay.Size = new System.Drawing.Size(145, 60);
            this.labDisplay.TabIndex = 6;
            this.labDisplay.Text = "物料：6607MB34\r\n标准：100.90\r\n实际：99.89\r\n设定：100 实际：45";
            // 
            // picRun
            // 
            this.picRun.Image = ((System.Drawing.Image)(resources.GetObject("picRun.Image")));
            this.picRun.InitialImage = ((System.Drawing.Image)(resources.GetObject("picRun.InitialImage")));
            this.picRun.Location = new System.Drawing.Point(76, 0);
            this.picRun.Name = "picRun";
            this.picRun.Size = new System.Drawing.Size(25, 25);
            this.picRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRun.TabIndex = 9;
            this.picRun.TabStop = false;
            this.picRun.Visible = false;
            // 
            // ScaleDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.labDisplay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labUnit);
            this.Controls.Add(this.txtScaleValue);
            this.Controls.Add(this.btnScaleName);
            this.Name = "ScaleDetail";
            this.Size = new System.Drawing.Size(161, 138);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAuto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRun)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScaleName;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.TextBox txtScaleValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picManual;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picAlarm;
        private System.Windows.Forms.PictureBox picAuto;
        private System.Windows.Forms.Label labDisplay;
        private System.Windows.Forms.PictureBox picRun;


    }
}
