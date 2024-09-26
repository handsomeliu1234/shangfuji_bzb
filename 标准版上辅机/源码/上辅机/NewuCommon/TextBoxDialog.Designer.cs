namespace NewuCommon
{
    partial class TextBoxDialog
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
            this.btnDalog = new System.Windows.Forms.Button();
            this.txtDalog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnDalog
            // 
            this.btnDalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDalog.Location = new System.Drawing.Point(132, -1);
            this.btnDalog.Name = "btnDalog";
            this.btnDalog.Size = new System.Drawing.Size(31, 22);
            this.btnDalog.TabIndex = 28;
            this.btnDalog.Text = "…";
            this.btnDalog.UseVisualStyleBackColor = true;
            this.btnDalog.Click += new System.EventHandler(this.btnDalog_Click);
            // 
            // txtDalog
            // 
            this.txtDalog.Location = new System.Drawing.Point(0, 0);
            this.txtDalog.Name = "txtDalog";
            this.txtDalog.ReadOnly = true;
            this.txtDalog.Size = new System.Drawing.Size(130, 21);
            this.txtDalog.TabIndex = 27;
            // 
            // TextBoxDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDalog);
            this.Controls.Add(this.txtDalog);
            this.Name = "TextBoxDialog";
            this.Size = new System.Drawing.Size(163, 109);
            this.Resize += new System.EventHandler(this.TextBoxDialog_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDalog;
        private System.Windows.Forms.TextBox txtDalog;


    }
}
