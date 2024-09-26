namespace NewuCommon
{
    partial class RubberScale2
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
            this.Sensor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Sensor
            // 
            this.Sensor.BackColor = System.Drawing.Color.Aqua;
            this.Sensor.Enabled = false;
            this.Sensor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Sensor.Location = new System.Drawing.Point(19, 0);
            this.Sensor.Name = "Sensor";
            this.Sensor.Size = new System.Drawing.Size(80, 28);
            this.Sensor.TabIndex = 611;
            this.Sensor.UseVisualStyleBackColor = false;
            this.Sensor.Visible = false;
            // 
            // RubberScale2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Sensor);
            this.Name = "RubberScale2";
            this.Resize += new System.EventHandler(this.RubberScale2_Resize);
            this.Controls.SetChildIndex(this.Sensor, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Sensor;
    }
}
