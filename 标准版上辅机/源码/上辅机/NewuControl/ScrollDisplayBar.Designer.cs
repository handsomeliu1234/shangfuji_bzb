namespace NewuCommon
{
    partial class ScrollDisplayBar
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
            this.alarm_content = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // alarm_content
            // 
            this.alarm_content.AutoSize = true;
            this.alarm_content.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.alarm_content.Font = new System.Drawing.Font("楷体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.alarm_content.ForeColor = System.Drawing.Color.Red;
            this.alarm_content.Location = new System.Drawing.Point(3, 16);
            this.alarm_content.Name = "alarm_content";
            this.alarm_content.Size = new System.Drawing.Size(163, 35);
            this.alarm_content.TabIndex = 0;
            this.alarm_content.Text = "设备正常";
            // 
            // ScrollDisplayBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.alarm_content);
            this.Name = "ScrollDisplayBar";
            this.Size = new System.Drawing.Size(1226, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label alarm_content;
    }
}
