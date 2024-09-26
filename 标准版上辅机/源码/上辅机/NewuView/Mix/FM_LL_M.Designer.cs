namespace NewuView.Mix
{
    partial class FM_LL_M
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
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Weight_OrderName = new System.Windows.Forms.TextBox();
            this.tb_PLCState = new System.Windows.Forms.TextBox();
            this.tb_nowCar = new System.Windows.Forms.TextBox();
            this.tb_setCar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scrollDisplayBar1 = new NewuControl.ScrollDisplayBar();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Mix_OrderName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(80, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 740;
            this.label3.Text = "称量配方：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(82, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 20);
            this.label6.TabIndex = 739;
            this.label6.Text = "当前车数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(80, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 738;
            this.label5.Text = "设定车数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(51, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 737;
            this.label2.Text = "PLC通讯状态：";
            // 
            // tb_Weight_OrderName
            // 
            this.tb_Weight_OrderName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Weight_OrderName.Location = new System.Drawing.Point(209, 50);
            this.tb_Weight_OrderName.Name = "tb_Weight_OrderName";
            this.tb_Weight_OrderName.Size = new System.Drawing.Size(113, 30);
            this.tb_Weight_OrderName.TabIndex = 736;
            this.tb_Weight_OrderName.Text = "null";
            this.tb_Weight_OrderName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_PLCState
            // 
            this.tb_PLCState.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_PLCState.Location = new System.Drawing.Point(209, 12);
            this.tb_PLCState.Name = "tb_PLCState";
            this.tb_PLCState.Size = new System.Drawing.Size(113, 30);
            this.tb_PLCState.TabIndex = 735;
            this.tb_PLCState.Text = "连接中...";
            this.tb_PLCState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_nowCar
            // 
            this.tb_nowCar.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_nowCar.Location = new System.Drawing.Point(209, 161);
            this.tb_nowCar.Name = "tb_nowCar";
            this.tb_nowCar.Size = new System.Drawing.Size(113, 30);
            this.tb_nowCar.TabIndex = 734;
            this.tb_nowCar.Text = "35";
            this.tb_nowCar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_setCar
            // 
            this.tb_setCar.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_setCar.Location = new System.Drawing.Point(209, 120);
            this.tb_setCar.Name = "tb_setCar";
            this.tb_setCar.Size = new System.Drawing.Size(113, 30);
            this.tb_setCar.TabIndex = 733;
            this.tb_setCar.Text = "60";
            this.tb_setCar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 732;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.scrollDisplayBar1);
            this.panel2.Location = new System.Drawing.Point(3, 658);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1583, 83);
            this.panel2.TabIndex = 741;
            // 
            // scrollDisplayBar1
            // 
            this.scrollDisplayBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scrollDisplayBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollDisplayBar1.Location = new System.Drawing.Point(0, 0);
            this.scrollDisplayBar1.Name = "scrollDisplayBar1";
            this.scrollDisplayBar1.Size = new System.Drawing.Size(1583, 83);
            this.scrollDisplayBar1.TabIndex = 617;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(80, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 742;
            this.label4.Text = "炼胶配方：";
            // 
            // tb_Mix_OrderName
            // 
            this.tb_Mix_OrderName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Mix_OrderName.Location = new System.Drawing.Point(209, 85);
            this.tb_Mix_OrderName.Name = "tb_Mix_OrderName";
            this.tb_Mix_OrderName.Size = new System.Drawing.Size(113, 30);
            this.tb_Mix_OrderName.TabIndex = 743;
            this.tb_Mix_OrderName.Text = "null";
            this.tb_Mix_OrderName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FM_LL_M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 749);
            this.Controls.Add(this.tb_Mix_OrderName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Weight_OrderName);
            this.Controls.Add(this.tb_PLCState);
            this.Controls.Add(this.tb_nowCar);
            this.Controls.Add(this.tb_setCar);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Name = "FM_LL_M";
            this.Text = "母练监控";
            this.Load += new System.EventHandler(this.FM_LL_M_Load);
            this.Controls.SetChildIndex(this.carbonBin03, 0);
            this.Controls.SetChildIndex(this.RubberGoJiao2, 0);
            this.Controls.SetChildIndex(this.PipeCarbon15, 0);
            this.Controls.SetChildIndex(this.PipeCarbon25, 0);
            this.Controls.SetChildIndex(this.PipeCarbon83, 0);
            this.Controls.SetChildIndex(this.PipeCarbon73, 0);
            this.Controls.SetChildIndex(this.PipeCarbon65, 0);
            this.Controls.SetChildIndex(this.PipeCarbon61, 0);
            this.Controls.SetChildIndex(this.PipeCarbonScaleMid02, 0);
            this.Controls.SetChildIndex(this.MixPart1, 0);
            this.Controls.SetChildIndex(this.luoXuan3, 0);
            this.Controls.SetChildIndex(this.luoXuan4, 0);
            this.Controls.SetChildIndex(this.luoXuan6, 0);
            this.Controls.SetChildIndex(this.luoXuan8, 0);
            this.Controls.SetChildIndex(this.carbonBin04, 0);
            this.Controls.SetChildIndex(this.carbonBin05, 0);
            this.Controls.SetChildIndex(this.carbonBin06, 0);
            this.Controls.SetChildIndex(this.carbonBin07, 0);
            this.Controls.SetChildIndex(this.carbonBin08, 0);
            this.Controls.SetChildIndex(this.luoXuan5, 0);
            this.Controls.SetChildIndex(this.RubberGoJiao1, 0);
            this.Controls.SetChildIndex(this.MotorOil01, 0);
            this.Controls.SetChildIndex(this.YiBiaoCarbon, 0);
            this.Controls.SetChildIndex(this.YiBiaoOil1, 0);
            this.Controls.SetChildIndex(this.YiBiaoRubber, 0);
            this.Controls.SetChildIndex(this.YiBiaoPowerEnergy, 0);
            this.Controls.SetChildIndex(this.YiBiaoPressSpeed, 0);
            this.Controls.SetChildIndex(this.YiBiaoCarbonMid, 0);
            this.Controls.SetChildIndex(this.YiBiaoMixTemp, 0);
            this.Controls.SetChildIndex(this.YiBiaoTimeTime, 0);
            this.Controls.SetChildIndex(this.PipeCarbon31, 0);
            this.Controls.SetChildIndex(this.PipeCarbon32, 0);
            this.Controls.SetChildIndex(this.PipeCarbon33, 0);
            this.Controls.SetChildIndex(this.PipeCarbon34, 0);
            this.Controls.SetChildIndex(this.PipeCarbon35, 0);
            this.Controls.SetChildIndex(this.PipeCarbon41, 0);
            this.Controls.SetChildIndex(this.PipeCarbon51, 0);
            this.Controls.SetChildIndex(this.PipeCarbon75, 0);
            this.Controls.SetChildIndex(this.PipeCarbon74, 0);
            this.Controls.SetChildIndex(this.PipeCarbon72, 0);
            this.Controls.SetChildIndex(this.PipeCarbon71, 0);
            this.Controls.SetChildIndex(this.PipeCarbon81, 0);
            this.Controls.SetChildIndex(this.PipeCarbon82, 0);
            this.Controls.SetChildIndex(this.PipeCarbon84, 0);
            this.Controls.SetChildIndex(this.PipeCarbon85, 0);
            this.Controls.SetChildIndex(this.PipeCarbon11, 0);
            this.Controls.SetChildIndex(this.PipeCarbon21, 0);
            this.Controls.SetChildIndex(this.PipeCarbonScale, 0);
            this.Controls.SetChildIndex(this.PipeCarbonScaleMid03, 0);
            this.Controls.SetChildIndex(this.PipeCarbonScaleMid04, 0);
            this.Controls.SetChildIndex(this.PipeCarbonScaleMid05, 0);
            this.Controls.SetChildIndex(this.CarbonScaleMidBin, 0);
            this.Controls.SetChildIndex(this.PipeOilScaleMid12, 0);
            this.Controls.SetChildIndex(this.PipeOil35, 0);
            this.Controls.SetChildIndex(this.PipeOil34, 0);
            this.Controls.SetChildIndex(this.PipeOil33, 0);
            this.Controls.SetChildIndex(this.PipeOil32, 0);
            this.Controls.SetChildIndex(this.PipeOil31, 0);
            this.Controls.SetChildIndex(this.FaOil03, 0);
            this.Controls.SetChildIndex(this.PipeOil11, 0);
            this.Controls.SetChildIndex(this.FaOil01, 0);
            this.Controls.SetChildIndex(this.PipeOil12, 0);
            this.Controls.SetChildIndex(this.oilTank1, 0);
            this.Controls.SetChildIndex(this.PipeOil13, 0);
            this.Controls.SetChildIndex(this.PipeOil14, 0);
            this.Controls.SetChildIndex(this.PipeOil21, 0);
            this.Controls.SetChildIndex(this.FaOil02, 0);
            this.Controls.SetChildIndex(this.oilTank2, 0);
            this.Controls.SetChildIndex(this.PipeOil15, 0);
            this.Controls.SetChildIndex(this.OilScaleBin01, 0);
            this.Controls.SetChildIndex(this.luoXuan1, 0);
            this.Controls.SetChildIndex(this.luoXuan2, 0);
            this.Controls.SetChildIndex(this.luoXuan7, 0);
            this.Controls.SetChildIndex(this.PipeOilScale01, 0);
            this.Controls.SetChildIndex(this.OilScaleMidBin01, 0);
            this.Controls.SetChildIndex(this.PipeOilScaleMid11, 0);
            this.Controls.SetChildIndex(this.PipeCarbonScaleMid01, 0);
            this.Controls.SetChildIndex(this.FaCarbonScaleMid, 0);
            this.Controls.SetChildIndex(this.faOilScale, 0);
            this.Controls.SetChildIndex(this.faOilScaleMid, 0);
            this.Controls.SetChildIndex(this.lblCarbonTroubleshooting, 0);
            this.Controls.SetChildIndex(this.lblCarbonHost, 0);
            this.Controls.SetChildIndex(this.PipeCarbon63, 0);
            this.Controls.SetChildIndex(this.PipeCarbon64, 0);
            this.Controls.SetChildIndex(this.PipeCarbon62, 0);
            this.Controls.SetChildIndex(this.YiBiaoDrug, 0);
            this.Controls.SetChildIndex(this.PipeCarbon22, 0);
            this.Controls.SetChildIndex(this.PipeCarbon23, 0);
            this.Controls.SetChildIndex(this.PipeCarbon24, 0);
            this.Controls.SetChildIndex(this.PipeCarbon13, 0);
            this.Controls.SetChildIndex(this.PipeCarbon12, 0);
            this.Controls.SetChildIndex(this.PipeCarbon14, 0);
            this.Controls.SetChildIndex(this.chart1, 0);
            this.Controls.SetChildIndex(this.SilTank, 0);
            this.Controls.SetChildIndex(this.YiBiaoSil1, 0);
            this.Controls.SetChildIndex(this.PipeSil11, 0);
            this.Controls.SetChildIndex(this.faSil01, 0);
            this.Controls.SetChildIndex(this.SilScaleBin01, 0);
            this.Controls.SetChildIndex(this.PipeSilScale, 0);
            this.Controls.SetChildIndex(this.faSilScale, 0);
            this.Controls.SetChildIndex(this.PipeSilScaleMid11, 0);
            this.Controls.SetChildIndex(this.SilScaleMidBin01, 0);
            this.Controls.SetChildIndex(this.faSilScaleMid, 0);
            this.Controls.SetChildIndex(this.PipeSilScaleMid12, 0);
            this.Controls.SetChildIndex(this.ReCycBin1, 0);
            this.Controls.SetChildIndex(this.ReCycPipe11, 0);
            this.Controls.SetChildIndex(this.ReCycluoXuan1, 0);
            this.Controls.SetChildIndex(this.ReCycPipe13, 0);
            this.Controls.SetChildIndex(this.ReCycPipe12, 0);
            this.Controls.SetChildIndex(this.CarbonScaleBin, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.FaCarbonScale, 0);
            this.Controls.SetChildIndex(this.MotorSil, 0);
            this.Controls.SetChildIndex(this.carbonBin02, 0);
            this.Controls.SetChildIndex(this.PlasticizerYiBiao, 0);
            this.Controls.SetChildIndex(this.Scale_Rubber, 0);
            this.Controls.SetChildIndex(this.Scale_Drug, 0);
            this.Controls.SetChildIndex(this.Send_Rubber, 0);
            this.Controls.SetChildIndex(this.Scale_Pla, 0);
            this.Controls.SetChildIndex(this.oilTank3, 0);
            this.Controls.SetChildIndex(this.carbonBin01, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tb_setCar, 0);
            this.Controls.SetChildIndex(this.tb_nowCar, 0);
            this.Controls.SetChildIndex(this.tb_PLCState, 0);
            this.Controls.SetChildIndex(this.tb_Weight_OrderName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tb_Mix_OrderName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tb_Weight_OrderName;
        public System.Windows.Forms.TextBox tb_PLCState;
        public System.Windows.Forms.TextBox tb_nowCar;
        public System.Windows.Forms.TextBox tb_setCar;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel2;
        public NewuControl.ScrollDisplayBar scrollDisplayBar1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tb_Mix_OrderName;
    }
}