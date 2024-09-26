
namespace NewuView.Mix
{
    partial class US_Monitor_Final_series
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(US_Monitor_Final_series));
            this.Send_Rubber = new NewuCommon.RubberScale2();
            this.RubberGoJiao1 = new NewuCommon.NewuPicAngle();
            this.RubberGoJiao2 = new NewuCommon.NewuPicAngle();
            this.Scale_Rubber = new NewuCommon.RubberScale2();
            this.mixPart1 = new NewuCommon.MixPart();
            this.YiBiaoTimeTime = new NewuCommon.NewuYiBiao();
            this.YiBiaoRubber = new NewuCommon.Scale();
            this.YiBiaoMixFTime = new NewuCommon.NewuYiBiao();
            this.downMixPart1 = new NewuCommon.DownMixPart();
            this.SuspendLayout();
            // 
            // Send_Rubber
            // 
            this.Send_Rubber.Location = new System.Drawing.Point(508, 290);
            this.Send_Rubber.Name = "Send_Rubber";
            this.Send_Rubber.NewuPicDisplayStyle = NewuCommon.RubberScale.NewuPicStyle.RubberConveyorStop;
            this.Send_Rubber.Size = new System.Drawing.Size(210, 67);
            this.Send_Rubber.TabIndex = 1318;
            // 
            // RubberGoJiao1
            // 
            this.RubberGoJiao1.Location = new System.Drawing.Point(921, 172);
            this.RubberGoJiao1.Name = "RubberGoJiao1";
            this.RubberGoJiao1.NewuAngle = 0;
            this.RubberGoJiao1.NewuImgBg = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao1.NewuImgBg")));
            this.RubberGoJiao1.NewuImgFore = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao1.NewuImgFore")));
            this.RubberGoJiao1.NewuPicTypeStyle = NewuCommon.NewuPicType.Foreground;
            this.RubberGoJiao1.Size = new System.Drawing.Size(96, 44);
            this.RubberGoJiao1.TabIndex = 1323;
            // 
            // RubberGoJiao2
            // 
            this.RubberGoJiao2.Location = new System.Drawing.Point(921, 237);
            this.RubberGoJiao2.Name = "RubberGoJiao2";
            this.RubberGoJiao2.NewuAngle = 0;
            this.RubberGoJiao2.NewuImgBg = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao2.NewuImgBg")));
            this.RubberGoJiao2.NewuImgFore = ((System.Drawing.Image)(resources.GetObject("RubberGoJiao2.NewuImgFore")));
            this.RubberGoJiao2.NewuPicTypeStyle = NewuCommon.NewuPicType.Foreground;
            this.RubberGoJiao2.Size = new System.Drawing.Size(96, 46);
            this.RubberGoJiao2.TabIndex = 1324;
            // 
            // Scale_Rubber
            // 
            this.Scale_Rubber.Location = new System.Drawing.Point(725, 290);
            this.Scale_Rubber.Margin = new System.Windows.Forms.Padding(4);
            this.Scale_Rubber.Name = "Scale_Rubber";
            this.Scale_Rubber.NewuPicDisplayStyle = NewuCommon.RubberScale.NewuPicStyle.RubberConveyorStop;
            this.Scale_Rubber.Size = new System.Drawing.Size(210, 67);
            this.Scale_Rubber.TabIndex = 1317;
            // 
            // mixPart1
            // 
            this.mixPart1.BackColor = System.Drawing.Color.Transparent;
            this.mixPart1.Location = new System.Drawing.Point(387, 173);
            this.mixPart1.Name = "mixPart1";
            this.mixPart1.NewuInDoorState = false;
            this.mixPart1.NewuMixRun = NewuCommon.MixPart.NewuPicStyle.Stop;
            this.mixPart1.NewuOutDoorClose = true;
            this.mixPart1.NewuShuanLocation = NewuCommon.MixPart.ShuanLocation.low;
            this.mixPart1.Size = new System.Drawing.Size(121, 251);
            this.mixPart1.TabIndex = 1337;
            // 
            // YiBiaoTimeTime
            // 
            this.YiBiaoTimeTime.BackColor = System.Drawing.Color.Black;
            this.YiBiaoTimeTime.ForeColor = System.Drawing.Color.White;
            this.YiBiaoTimeTime.Location = new System.Drawing.Point(546, 364);
            this.YiBiaoTimeTime.Name = "YiBiaoTimeTime";
            this.YiBiaoTimeTime.NewuScaleToolTip = "";
            this.YiBiaoTimeTime.NewuYiBiaoNameTxt = "配方时间";
            this.YiBiaoTimeTime.NewuYiBiaoUnit = "s";
            this.YiBiaoTimeTime.NewuYiBiaoValue = "45.9";
            this.YiBiaoTimeTime.Size = new System.Drawing.Size(110, 44);
            this.YiBiaoTimeTime.TabIndex = 1338;
            // 
            // YiBiaoRubber
            // 
            this.YiBiaoRubber.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoRubber.IsChengOK1 = false;
            this.YiBiaoRubber.Location = new System.Drawing.Point(767, 364);
            this.YiBiaoRubber.Name = "YiBiaoRubber";
            this.YiBiaoRubber.NewuScaleAuto = true;
            this.YiBiaoRubber.NewuScaleBatch = "9999";
            this.YiBiaoRubber.NewuScaleNameTxt = "胶料秤";
            this.YiBiaoRubber.NewuScaleOverAlarm = false;
            this.YiBiaoRubber.NewuScaleStartRun = false;
            this.YiBiaoRubber.NewuScaleToolTip = "";
            this.YiBiaoRubber.NewuScaleValue = "100.23";
            this.YiBiaoRubber.NewuYiBiaoUnit = "kg";
            this.YiBiaoRubber.Size = new System.Drawing.Size(106, 60);
            this.YiBiaoRubber.TabIndex = 1339;
            // 
            // YiBiaoMixFTime
            // 
            this.YiBiaoMixFTime.BackColor = System.Drawing.Color.Black;
            this.YiBiaoMixFTime.ForeColor = System.Drawing.Color.White;
            this.YiBiaoMixFTime.Location = new System.Drawing.Point(546, 446);
            this.YiBiaoMixFTime.Margin = new System.Windows.Forms.Padding(4);
            this.YiBiaoMixFTime.Name = "YiBiaoMixFTime";
            this.YiBiaoMixFTime.NewuScaleToolTip = "";
            this.YiBiaoMixFTime.NewuYiBiaoNameTxt = "配方时间";
            this.YiBiaoMixFTime.NewuYiBiaoUnit = "s";
            this.YiBiaoMixFTime.NewuYiBiaoValue = "45.9";
            this.YiBiaoMixFTime.Size = new System.Drawing.Size(110, 44);
            this.YiBiaoMixFTime.TabIndex = 2187;
            // 
            // downMixPart1
            // 
            this.downMixPart1.Location = new System.Drawing.Point(387, 424);
            this.downMixPart1.Name = "downMixPart1";
            this.downMixPart1.NewuMixRun = NewuCommon.DownMixPart.NewuPicStyle.Stop;
            this.downMixPart1.NewuOutDoorClose = true;
            this.downMixPart1.Size = new System.Drawing.Size(121, 66);
            this.downMixPart1.TabIndex = 2186;
            // 
            // US_Monitor_Final_series
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.YiBiaoMixFTime);
            this.Controls.Add(this.downMixPart1);
            this.Controls.Add(this.YiBiaoRubber);
            this.Controls.Add(this.YiBiaoTimeTime);
            this.Controls.Add(this.mixPart1);
            this.Controls.Add(this.Send_Rubber);
            this.Controls.Add(this.RubberGoJiao1);
            this.Controls.Add(this.RubberGoJiao2);
            this.Controls.Add(this.Scale_Rubber);
            this.Name = "US_Monitor_Final_series";
            this.Size = new System.Drawing.Size(1100, 547);
            this.Load += new System.EventHandler(this.US_Monitor_Final_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public NewuCommon.RubberScale2 Send_Rubber;
        public NewuCommon.RubberScale2 Scale_Rubber;
        public NewuCommon.NewuPicAngle RubberGoJiao1;
        public NewuCommon.NewuPicAngle RubberGoJiao2;
        private NewuCommon.MixPart mixPart1;
        private NewuCommon.NewuYiBiao YiBiaoTimeTime;
        private NewuCommon.Scale YiBiaoRubber;
        public NewuCommon.NewuYiBiao YiBiaoMixFTime;
        private NewuCommon.DownMixPart downMixPart1;
    }
}
