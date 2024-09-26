namespace NewuView.Mix
{
    partial class FMTeechart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMTeechart));
            this.axTChart1 = new AxTeeChart.AxTChart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axTChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // axTChart1
            // 
            this.axTChart1.Enabled = true;
            this.axTChart1.Location = new System.Drawing.Point(31, 12);
            this.axTChart1.Name = "axTChart1";
            this.axTChart1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChart1.OcxState")));
            this.axTChart1.Size = new System.Drawing.Size(652, 403);
            this.axTChart1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FMTeechart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 424);
            this.Controls.Add(this.axTChart1);
            this.Name = "FMTeechart";
            this.Text = "实时曲线";
            ((System.ComponentModel.ISupportInitialize)(this.axTChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxTeeChart.AxTChart axTChart1;
        private System.Windows.Forms.Timer timer1;
    }
}