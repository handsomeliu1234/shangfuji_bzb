namespace NewuView.Mix
{
    partial class Base_Monitor_Z
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Base_Monitor_Z));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Send_Rubber = new NewuControl.RubberScale2();
            this.RubberGoJiao2 = new NewuControl.NewuPicAngle();
            this.YiBiaoPowerEnergy = new NewuControl.NewuYiBiao2();
            this.YiBiaoTimeTime = new NewuControl.NewuYiBiao();
            this.YiBiaoMixTemp = new NewuControl.Scale();
            this.YiBiaoPressSpeed = new NewuControl.NewuYiBiao2();
            this.YB_Rubber = new NewuControl.Scale();
            this.RubberGoJiao1 = new NewuControl.NewuPicAngle();
            this.Part_Mix = new NewuControl.MixPart();
            this.Scale_Rubber = new NewuControl.RubberScale2();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(296, 71);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "温度";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "功率";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "压力";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "转速";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "能量";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "电压";
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.Name = "栓位";
            series8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Color = System.Drawing.Color.White;
            series8.Legend = "Legend1";
            series8.Name = "bg";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Series.Add(series8);
            this.chart1.Size = new System.Drawing.Size(394, 285);
            this.chart1.TabIndex = 239;
            this.chart1.Text = "chart1";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 100;
            // 
            // Send_Rubber
            // 
            this.Send_Rubber.Location = new System.Drawing.Point(368, 491);
            this.Send_Rubber.Name = "Send_Rubber";
            this.Send_Rubber.NewuPicDisplayStyle = NewuControl.RubberScale.NewuPicStyle.RubberConveyorStop;
            this.Send_Rubber.Size = new System.Drawing.Size(141, 47);
            this.Send_Rubber.TabIndex = 733;
            // 
            // RubberGoJiao2
            // 
            this.RubberGoJiao2.Location = new System.Drawing.Point(586, 443);
            this.RubberGoJiao2.Name = "RubberGoJiao2";
            this.RubberGoJiao2.NewuAngle = 0;
            this.RubberGoJiao2.NewuImgBg = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao2.NewuImgBg")));
            this.RubberGoJiao2.NewuImgFore = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao2.NewuImgFore")));
            this.RubberGoJiao2.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            this.RubberGoJiao2.Size = new System.Drawing.Size(121, 51);
            this.RubberGoJiao2.TabIndex = 231;
            // 
            // YiBiaoPowerEnergy
            // 
            this.YiBiaoPowerEnergy.BackColor = System.Drawing.Color.Black;
            this.YiBiaoPowerEnergy.Location = new System.Drawing.Point(95, 566);
            this.YiBiaoPowerEnergy.Name = "YiBiaoPowerEnergy";
            this.YiBiaoPowerEnergy.NewuYiBiaoNameTxt = "功率/能量";
            this.YiBiaoPowerEnergy.NewuYiBiaoUnit = "kW";
            this.YiBiaoPowerEnergy.NewuYiBiaoUnit2 = "kW.h";
            this.YiBiaoPowerEnergy.NewuYiBiaoValue = "110";
            this.YiBiaoPowerEnergy.NewuYiBiaoValue2 = "45.9";
            this.YiBiaoPowerEnergy.Size = new System.Drawing.Size(104, 63);
            this.YiBiaoPowerEnergy.TabIndex = 226;
            // 
            // YiBiaoTimeTime
            // 
            this.YiBiaoTimeTime.BackColor = System.Drawing.Color.Black;
            this.YiBiaoTimeTime.Location = new System.Drawing.Point(95, 333);
            this.YiBiaoTimeTime.Name = "YiBiaoTimeTime";
            this.YiBiaoTimeTime.NewuScaleToolTip = "";
            this.YiBiaoTimeTime.NewuYiBiaoNameTxt = "配方时间";
            this.YiBiaoTimeTime.NewuYiBiaoUnit = "s";
            this.YiBiaoTimeTime.NewuYiBiaoValue = "45.9";
            this.YiBiaoTimeTime.Size = new System.Drawing.Size(110, 44);
            this.YiBiaoTimeTime.TabIndex = 223;
            // 
            // YiBiaoMixTemp
            // 
            this.YiBiaoMixTemp.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoMixTemp.Location = new System.Drawing.Point(95, 482);
            this.YiBiaoMixTemp.Name = "YiBiaoMixTemp";
            this.YiBiaoMixTemp.NewuScaleAuto = false;
            this.YiBiaoMixTemp.NewuScaleBatch = "10";
            this.YiBiaoMixTemp.NewuScaleNameTxt = "密炼机";
            this.YiBiaoMixTemp.NewuScaleOverAlarm = false;
            this.YiBiaoMixTemp.NewuScaleStartRun = false;
            this.YiBiaoMixTemp.NewuScaleToolTip = "";
            this.YiBiaoMixTemp.NewuScaleValue = "154";
            this.YiBiaoMixTemp.NewuYiBiaoUnit = "℃";
            this.YiBiaoMixTemp.Size = new System.Drawing.Size(110, 72);
            this.YiBiaoMixTemp.TabIndex = 222;
            // 
            // YiBiaoPressSpeed
            // 
            this.YiBiaoPressSpeed.BackColor = System.Drawing.Color.Black;
            this.YiBiaoPressSpeed.Location = new System.Drawing.Point(101, 394);
            this.YiBiaoPressSpeed.Name = "YiBiaoPressSpeed";
            this.YiBiaoPressSpeed.NewuYiBiaoNameTxt = "压力/转速";
            this.YiBiaoPressSpeed.NewuYiBiaoUnit = "MPa";
            this.YiBiaoPressSpeed.NewuYiBiaoUnit2 = "r/min";
            this.YiBiaoPressSpeed.NewuYiBiaoValue = "45.9";
            this.YiBiaoPressSpeed.NewuYiBiaoValue2 = "35";
            this.YiBiaoPressSpeed.Size = new System.Drawing.Size(104, 63);
            this.YiBiaoPressSpeed.TabIndex = 221;
            // 
            // YB_Rubber
            // 
            this.YB_Rubber.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YB_Rubber.Location = new System.Drawing.Point(515, 551);
            this.YB_Rubber.Name = "YB_Rubber";
            this.YB_Rubber.NewuScaleAuto = false;
            this.YB_Rubber.NewuScaleBatch = "10";
            this.YB_Rubber.NewuScaleNameTxt = "胶料磅秤";
            this.YB_Rubber.NewuScaleOverAlarm = false;
            this.YB_Rubber.NewuScaleStartRun = false;
            this.YB_Rubber.NewuScaleToolTip = "";
            this.YB_Rubber.NewuScaleValue = "-10.23";
            this.YB_Rubber.NewuYiBiaoUnit = "kg";
            this.YB_Rubber.Size = new System.Drawing.Size(110, 72);
            this.YB_Rubber.TabIndex = 220;
            // 
            // RubberGoJiao1
            // 
            this.RubberGoJiao1.Location = new System.Drawing.Point(586, 386);
            this.RubberGoJiao1.Name = "RubberGoJiao1";
            this.RubberGoJiao1.NewuAngle = 0;
            this.RubberGoJiao1.NewuImgBg = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao1.NewuImgBg")));
            this.RubberGoJiao1.NewuImgFore = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao1.NewuImgFore")));
            this.RubberGoJiao1.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            this.RubberGoJiao1.Size = new System.Drawing.Size(121, 51);
            this.RubberGoJiao1.TabIndex = 219;
            // 
            // Part_Mix
            // 
            this.Part_Mix.Location = new System.Drawing.Point(262, 372);
            this.Part_Mix.Name = "Part_Mix";
            this.Part_Mix.NewuInDoorState = false;
            this.Part_Mix.NewuMixRun = NewuControl.MixPart.NewuPicStyle.Stop;
            this.Part_Mix.NewuOutDoorClose = true;
            this.Part_Mix.NewuShuanLocation = NewuControl.MixPart.ShuanLocation.low;
            this.Part_Mix.Size = new System.Drawing.Size(121, 251);
            this.Part_Mix.TabIndex = 217;
            // 
            // Scale_Rubber
            // 
            this.Scale_Rubber.Location = new System.Drawing.Point(503, 491);
            this.Scale_Rubber.Name = "Scale_Rubber";
            this.Scale_Rubber.NewuPicDisplayStyle = NewuControl.RubberScale.NewuPicStyle.RubberConveyorStop;
            this.Scale_Rubber.Size = new System.Drawing.Size(141, 47);
            this.Scale_Rubber.TabIndex = 734;
            // 
            // Base_Monitor_Z
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1350, 747);
            this.Controls.Add(this.Send_Rubber);
            this.Controls.Add(this.Scale_Rubber);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.RubberGoJiao2);
            this.Controls.Add(this.YiBiaoPowerEnergy);
            this.Controls.Add(this.YiBiaoTimeTime);
            this.Controls.Add(this.YiBiaoMixTemp);
            this.Controls.Add(this.YiBiaoPressSpeed);
            this.Controls.Add(this.YB_Rubber);
            this.Controls.Add(this.RubberGoJiao1);
            this.Controls.Add(this.Part_Mix);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Name = "Base_Monitor_Z";
            this.Text = "Base_Monitor_Z";
            this.Load += new System.EventHandler(this.Base_Monitor_Z_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private NewuControl.NewuPicAngle RubberGoJiao2;
        private NewuControl.NewuYiBiao2 YiBiaoPowerEnergy;
        private NewuControl.NewuYiBiao YiBiaoTimeTime;
        private NewuControl.Scale YiBiaoMixTemp;
        private NewuControl.NewuYiBiao2 YiBiaoPressSpeed;
        private NewuControl.Scale YB_Rubber;
        private NewuControl.NewuPicAngle RubberGoJiao1;
        private NewuControl.MixPart Part_Mix;
        private System.Windows.Forms.ToolTip toolTip1;
        private NewuControl.RubberScale2 Send_Rubber;
        private NewuControl.RubberScale2 Scale_Rubber;
    }
}