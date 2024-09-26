namespace NewuRPT
{
    partial class FM_MixCurve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_MixCurve));
            this.tChart1 = new Steema.TeeChart.TChart();
            this.area1 = new Steema.TeeChart.Styles.Area();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.annotation1 = new Steema.TeeChart.Tools.Annotation();
            this.cursorTool1 = new Steema.TeeChart.Tools.CursorTool();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.bt_print = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.chkCur = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tLabNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.marksTip1 = new Steema.TeeChart.Tools.MarksTip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tChart1
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Bottom.Labels.DateTimeFormat = "yyyy/M/d dddd";
            this.tChart1.Axes.Bottom.Labels.MultiLine = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Right.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            // 
            // 
            // 
            this.tChart1.Axes.Right.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            // 
            // 
            // 
            this.tChart1.Axes.Top.Visible = false;
            this.tChart1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChart1.Legend.CheckBoxes = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.tChart1.Legend.FontSeriesColor = true;
            this.tChart1.Legend.HorizMargin = -1;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Legend.Title.Pen.Visible = false;
            this.tChart1.Legend.TopLeftPos = 0;
            this.tChart1.Location = new System.Drawing.Point(88, 10);
            this.tChart1.Name = "tChart1";
            // 
            // 
            // 
            this.tChart1.Panel.MarginRight = 0D;
            this.tChart1.Series.Add(this.area1);
            this.tChart1.Series.Add(this.fastLine1);
            this.tChart1.Size = new System.Drawing.Size(863, 480);
            this.tChart1.TabIndex = 11;
            this.tChart1.Tools.Add(this.annotation1);
            this.tChart1.Tools.Add(this.cursorTool1);
            this.tChart1.Tools.Add(this.marksTip1);
            this.tChart1.BeforeDraw += new Steema.TeeChart.PaintChartEventHandler(this.tChart1_BeforeDraw);
            // 
            // area1
            // 
            // 
            // 
            // 
            this.area1.AreaBrush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            // 
            // 
            // 
            this.area1.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.area1.Gradient.Transparency = 70;
            // 
            // 
            // 
            this.area1.AreaLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(61)))), ((int)(((byte)(98)))));
            // 
            // 
            // 
            this.area1.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.area1.Brush.Visible = false;
            this.area1.ColorEach = false;
            // 
            // 
            // 
            this.area1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(61)))), ((int)(((byte)(98)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.area1.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.area1.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.area1.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.area1.Marks.Callout.Distance = 0;
            this.area1.Marks.Callout.Draw3D = false;
            this.area1.Marks.Callout.Length = 10;
            this.area1.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.area1.Pointer.Brush.Gradient.Transparency = 70;
            this.area1.Pointer.Dark3D = false;
            this.area1.Pointer.Draw3D = false;
            this.area1.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.area1.Stairs = true;
            this.area1.Title = "背景";
            this.area1.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right;
            // 
            // 
            // 
            this.area1.XValues.DataMember = "X";
            this.area1.XValues.DateTime = true;
            this.area1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.area1.YValues.DataMember = "Y";
            // 
            // fastLine1
            // 
            this.fastLine1.ColorEach = false;
            // 
            // 
            // 
            this.fastLine1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.fastLine1.LinePen.Width = 2;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine1.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None;
            this.fastLine1.Marks.Callout.ArrowHeadSize = 8;
            // 
            // 
            // 
            this.fastLine1.Marks.Callout.Brush.Color = System.Drawing.Color.Black;
            this.fastLine1.Marks.Callout.Distance = 0;
            this.fastLine1.Marks.Callout.Draw3D = false;
            this.fastLine1.Marks.Callout.Length = 10;
            this.fastLine1.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.fastLine1.Title = "fastLine1";
            this.fastLine1.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLine1.XValues.DataMember = "X";
            this.fastLine1.XValues.DateTime = true;
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            // 
            // annotation1
            // 
            this.annotation1.AutoSize = true;
            this.annotation1.Cursor = System.Windows.Forms.Cursors.Default;
            this.annotation1.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom;
            this.annotation1.PositionUnits = Steema.TeeChart.PositionUnits.Percent;
            // 
            // 
            // 
            // 
            // 
            // 
            this.annotation1.Shape.Shadow.Visible = true;
            // 
            // cursorTool1
            // 
            this.cursorTool1.FollowMouse = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel1.Controls.Add(this.bt_print);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnBack);
            this.splitContainer1.Panel1.Controls.Add(this.btnPre);
            this.splitContainer1.Panel1.Controls.Add(this.chkCur);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tChart1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1145, 573);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(241, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Y坐标回显";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // bt_print
            // 
            this.bt_print.Location = new System.Drawing.Point(321, 23);
            this.bt_print.Name = "bt_print";
            this.bt_print.Size = new System.Drawing.Size(72, 28);
            this.bt_print.TabIndex = 23;
            this.bt_print.Text = "打印";
            this.bt_print.UseVisualStyleBackColor = true;
            this.bt_print.Click += new System.EventHandler(this.bt_print_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1018, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 28);
            this.button1.TabIndex = 22;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(462, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 27);
            this.label1.TabIndex = 21;
            this.label1.Text = "配方第*车曲线";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(109, 25);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 28);
            this.btnBack.TabIndex = 20;
            this.btnBack.Text = ">> 下一车";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(31, 25);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(72, 28);
            this.btnPre.TabIndex = 19;
            this.btnPre.Text = "<<上一车";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // chkCur
            // 
            this.chkCur.AutoSize = true;
            this.chkCur.Location = new System.Drawing.Point(187, 30);
            this.chkCur.Name = "chkCur";
            this.chkCur.Size = new System.Drawing.Size(48, 16);
            this.chkCur.TabIndex = 16;
            this.chkCur.Text = "坐标";
            this.chkCur.UseVisualStyleBackColor = true;
            this.chkCur.CheckedChanged += new System.EventHandler(this.chkCur_CheckedChanged_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tLabNum});
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1145, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tLabNum
            // 
            this.tLabNum.Name = "tLabNum";
            this.tLabNum.Size = new System.Drawing.Size(56, 17);
            this.tLabNum.Text = "数据量：";
            // 
            // marksTip1
            // 
            this.marksTip1.HideDelay = 500;
            // 
            // FM_MixCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FM_MixCurve";
            this.Text = "FM_MixCurve";
            this.Load += new System.EventHandler(this.FM_MixCurve_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Steema.TeeChart.TChart tChart1;
        private Steema.TeeChart.Styles.Area area1;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private Steema.TeeChart.Tools.Annotation annotation1;
        private Steema.TeeChart.Tools.CursorTool cursorTool1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.CheckBox chkCur;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tLabNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bt_print;
        private System.Windows.Forms.CheckBox checkBox1;
        private Steema.TeeChart.Tools.MarksTip marksTip1;
    }
}