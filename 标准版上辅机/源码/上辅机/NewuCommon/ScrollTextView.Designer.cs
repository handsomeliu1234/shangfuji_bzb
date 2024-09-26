namespace NewuCommon
{
    partial class ScrollTextView
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
            this.textView = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // textView
            // 
            this.textView.AutoSize = true;
            this.textView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textView.Font = new System.Drawing.Font("黑体", 26.25F, System.Drawing.FontStyle.Bold);
            this.textView.Location = new System.Drawing.Point(-6, 0);
            this.textView.Name = "textView";
            this.textView.Size = new System.Drawing.Size(1051, 35);
            this.textView.TabIndex = 0;
            this.textView.Text = "　　　　我们不一样，每个人都有不同的鲸鱼！　　　　　　　";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ScrollTextView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textView);
            this.Name = "ScrollTextView";
            this.Size = new System.Drawing.Size(1055, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textView;
        private System.Windows.Forms.Timer timer;
    }
}
