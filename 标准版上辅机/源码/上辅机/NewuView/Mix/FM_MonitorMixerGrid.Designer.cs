namespace NewuView.Mix
{
    partial class FM_MonitorMixerGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvWeight = new NewuCommon.DataGridViewEx();
            this.panel2 = new System.Windows.Forms.Panel();
            this.YiBiaoSil = new NewuControl.ScaleDetail();
            this.YiBiaoRubber = new NewuControl.ScaleDetail();
            this.YiBiaoPla = new NewuControl.ScaleDetail();
            this.YiBiaoOil = new NewuControl.ScaleDetail();
            this.YiBiaoDrug = new NewuControl.ScaleDetail();
            this.YiBiaoCarbon = new NewuControl.ScaleDetail();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvTech = new NewuCommon.DataGridViewEx();
            this.mixPart1 = new NewuControl.MixPart();
            this.panel7 = new System.Windows.Forms.Panel();
            this.YiBiaoPressSpeed = new NewuControl.NewuYiBiao2();
            this.YiBiaoPowerEnergy = new NewuControl.NewuYiBiao2();
            this.YiBiaoTimeTime = new NewuControl.NewuYiBiao();
            this.YiBiaoMixTemp = new NewuControl.Scale();
            this.YiBiaoZon = new NewuControl.ScaleDetail();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeight)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTech)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvWeight);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1430, 254);
            this.panel1.TabIndex = 2;
            // 
            // dgvWeight
            // 
            this.dgvWeight.AllowUserToAddRows = false;
            this.dgvWeight.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvWeight.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWeight.BackgroundColor = System.Drawing.Color.White;
            this.dgvWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvWeight.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvWeight.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWeight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWeight.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvWeight.EnableHeadersVisualStyles = false;
            this.dgvWeight.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvWeight.Location = new System.Drawing.Point(0, 0);
            this.dgvWeight.Name = "dgvWeight";
            this.dgvWeight.ReadOnly = true;
            this.dgvWeight.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvWeight.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvWeight.RowTemplate.Height = 23;
            this.dgvWeight.RowTemplate.ReadOnly = true;
            this.dgvWeight.Size = new System.Drawing.Size(1430, 254);
            this.dgvWeight.TabIndex = 1;
            this.dgvWeight.VisibleOrderNumber = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.YiBiaoZon);
            this.panel2.Controls.Add(this.YiBiaoSil);
            this.panel2.Controls.Add(this.YiBiaoRubber);
            this.panel2.Controls.Add(this.YiBiaoPla);
            this.panel2.Controls.Add(this.YiBiaoOil);
            this.panel2.Controls.Add(this.YiBiaoDrug);
            this.panel2.Controls.Add(this.YiBiaoCarbon);
            this.panel2.Location = new System.Drawing.Point(2, 261);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1430, 132);
            this.panel2.TabIndex = 3;
            // 
            // YiBiaoSil
            // 
            this.YiBiaoSil.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoSil.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoSil.Location = new System.Drawing.Point(887, 5);
            this.YiBiaoSil.Name = "YiBiaoSil";
            this.YiBiaoSil.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoSil.NewuScaleAuto = false;
            this.YiBiaoSil.NewuScaleNameTxt = "硅烷秤";
            this.YiBiaoSil.NewuScaleOverAlarm = false;
            this.YiBiaoSil.NewuScaleStartRun = false;
            this.YiBiaoSil.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoSil.NewuScaleValue = "100.23";
            this.YiBiaoSil.NewuYiBiaoUnit = "kg";
            this.YiBiaoSil.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoSil.TabIndex = 19;
            // 
            // YiBiaoRubber
            // 
            this.YiBiaoRubber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoRubber.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoRubber.Location = new System.Drawing.Point(22, 3);
            this.YiBiaoRubber.Name = "YiBiaoRubber";
            this.YiBiaoRubber.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoRubber.NewuScaleAuto = false;
            this.YiBiaoRubber.NewuScaleNameTxt = "胶料";
            this.YiBiaoRubber.NewuScaleOverAlarm = false;
            this.YiBiaoRubber.NewuScaleStartRun = false;
            this.YiBiaoRubber.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoRubber.NewuScaleValue = "100.23";
            this.YiBiaoRubber.NewuYiBiaoUnit = "kg";
            this.YiBiaoRubber.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoRubber.TabIndex = 16;
            // 
            // YiBiaoPla
            // 
            this.YiBiaoPla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoPla.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoPla.Location = new System.Drawing.Point(720, 5);
            this.YiBiaoPla.Name = "YiBiaoPla";
            this.YiBiaoPla.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoPla.NewuScaleAuto = false;
            this.YiBiaoPla.NewuScaleNameTxt = "塑解剂";
            this.YiBiaoPla.NewuScaleOverAlarm = false;
            this.YiBiaoPla.NewuScaleStartRun = false;
            this.YiBiaoPla.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoPla.NewuScaleValue = "100.23";
            this.YiBiaoPla.NewuYiBiaoUnit = "kg";
            this.YiBiaoPla.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoPla.TabIndex = 16;
            // 
            // YiBiaoOil
            // 
            this.YiBiaoOil.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoOil.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoOil.Location = new System.Drawing.Point(200, 5);
            this.YiBiaoOil.Name = "YiBiaoOil";
            this.YiBiaoOil.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoOil.NewuScaleAuto = false;
            this.YiBiaoOil.NewuScaleNameTxt = "油料";
            this.YiBiaoOil.NewuScaleOverAlarm = false;
            this.YiBiaoOil.NewuScaleStartRun = false;
            this.YiBiaoOil.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoOil.NewuScaleValue = "100.23";
            this.YiBiaoOil.NewuYiBiaoUnit = "kg";
            this.YiBiaoOil.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoOil.TabIndex = 15;
            // 
            // YiBiaoDrug
            // 
            this.YiBiaoDrug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoDrug.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoDrug.Location = new System.Drawing.Point(553, 5);
            this.YiBiaoDrug.Name = "YiBiaoDrug";
            this.YiBiaoDrug.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoDrug.NewuScaleAuto = false;
            this.YiBiaoDrug.NewuScaleNameTxt = "小药";
            this.YiBiaoDrug.NewuScaleOverAlarm = false;
            this.YiBiaoDrug.NewuScaleStartRun = false;
            this.YiBiaoDrug.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoDrug.NewuScaleValue = "100.23";
            this.YiBiaoDrug.NewuYiBiaoUnit = "kg";
            this.YiBiaoDrug.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoDrug.TabIndex = 18;
            // 
            // YiBiaoCarbon
            // 
            this.YiBiaoCarbon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoCarbon.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoCarbon.Location = new System.Drawing.Point(386, 5);
            this.YiBiaoCarbon.Name = "YiBiaoCarbon";
            this.YiBiaoCarbon.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoCarbon.NewuScaleAuto = false;
            this.YiBiaoCarbon.NewuScaleNameTxt = "炭黑";
            this.YiBiaoCarbon.NewuScaleOverAlarm = false;
            this.YiBiaoCarbon.NewuScaleStartRun = false;
            this.YiBiaoCarbon.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoCarbon.NewuScaleValue = "100.23";
            this.YiBiaoCarbon.NewuYiBiaoUnit = "kg";
            this.YiBiaoCarbon.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoCarbon.TabIndex = 17;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.dgvTech);
            this.panel5.Location = new System.Drawing.Point(11, 398);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1339, 451);
            this.panel5.TabIndex = 6;
            // 
            // dgvTech
            // 
            this.dgvTech.AllowUserToAddRows = false;
            this.dgvTech.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvTech.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTech.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTech.BackgroundColor = System.Drawing.Color.White;
            this.dgvTech.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTech.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvTech.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTech.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTech.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTech.EnableHeadersVisualStyles = false;
            this.dgvTech.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvTech.Location = new System.Drawing.Point(0, 0);
            this.dgvTech.Name = "dgvTech";
            this.dgvTech.ReadOnly = true;
            this.dgvTech.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvTech.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTech.RowTemplate.Height = 23;
            this.dgvTech.RowTemplate.ReadOnly = true;
            this.dgvTech.Size = new System.Drawing.Size(1334, 319);
            this.dgvTech.TabIndex = 1;
            this.dgvTech.VisibleOrderNumber = false;
            // 
            // mixPart1
            // 
            this.mixPart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mixPart1.Location = new System.Drawing.Point(103, 417);
            this.mixPart1.Name = "mixPart1";
            this.mixPart1.NewuInDoorState = false;
            this.mixPart1.NewuMixRun = NewuControl.MixPart.NewuPicStyle.Stop;
            this.mixPart1.NewuOutDoorClose = true;
            this.mixPart1.NewuShuanLocation = NewuControl.MixPart.ShuanLocation.low;
            this.mixPart1.Size = new System.Drawing.Size(121, 251);
            this.mixPart1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.mixPart1);
            this.panel7.Controls.Add(this.YiBiaoPressSpeed);
            this.panel7.Controls.Add(this.YiBiaoPowerEnergy);
            this.panel7.Controls.Add(this.YiBiaoTimeTime);
            this.panel7.Controls.Add(this.YiBiaoMixTemp);
            this.panel7.Location = new System.Drawing.Point(1353, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(227, 848);
            this.panel7.TabIndex = 8;
            // 
            // YiBiaoPressSpeed
            // 
            this.YiBiaoPressSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoPressSpeed.BackColor = System.Drawing.Color.Black;
            this.YiBiaoPressSpeed.Location = new System.Drawing.Point(123, 674);
            this.YiBiaoPressSpeed.Name = "YiBiaoPressSpeed";
            this.YiBiaoPressSpeed.NewuYiBiaoNameTxt = "压力/转速";
            this.YiBiaoPressSpeed.NewuYiBiaoUnit = "MPa";
            this.YiBiaoPressSpeed.NewuYiBiaoUnit2 = "r/min";
            this.YiBiaoPressSpeed.NewuYiBiaoValue = "110";
            this.YiBiaoPressSpeed.NewuYiBiaoValue2 = "45.9";
            this.YiBiaoPressSpeed.Size = new System.Drawing.Size(104, 63);
            this.YiBiaoPressSpeed.TabIndex = 7;
            // 
            // YiBiaoPowerEnergy
            // 
            this.YiBiaoPowerEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoPowerEnergy.BackColor = System.Drawing.Color.Black;
            this.YiBiaoPowerEnergy.Location = new System.Drawing.Point(123, 744);
            this.YiBiaoPowerEnergy.Name = "YiBiaoPowerEnergy";
            this.YiBiaoPowerEnergy.NewuYiBiaoNameTxt = "功率/能量";
            this.YiBiaoPowerEnergy.NewuYiBiaoUnit = "kw";
            this.YiBiaoPowerEnergy.NewuYiBiaoUnit2 = "kw.h";
            this.YiBiaoPowerEnergy.NewuYiBiaoValue = "110";
            this.YiBiaoPowerEnergy.NewuYiBiaoValue2 = "45.9";
            this.YiBiaoPowerEnergy.Size = new System.Drawing.Size(104, 63);
            this.YiBiaoPowerEnergy.TabIndex = 6;
            // 
            // YiBiaoTimeTime
            // 
            this.YiBiaoTimeTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoTimeTime.BackColor = System.Drawing.Color.Black;
            this.YiBiaoTimeTime.Location = new System.Drawing.Point(7, 762);
            this.YiBiaoTimeTime.Name = "YiBiaoTimeTime";
            this.YiBiaoTimeTime.NewuScaleToolTip = "";
            this.YiBiaoTimeTime.NewuYiBiaoNameTxt = "炼胶时间";
            this.YiBiaoTimeTime.NewuYiBiaoUnit = "s";
            this.YiBiaoTimeTime.NewuYiBiaoValue = "45.9";
            this.YiBiaoTimeTime.Size = new System.Drawing.Size(110, 44);
            this.YiBiaoTimeTime.TabIndex = 5;
            // 
            // YiBiaoMixTemp
            // 
            this.YiBiaoMixTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoMixTemp.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoMixTemp.Location = new System.Drawing.Point(7, 674);
            this.YiBiaoMixTemp.Name = "YiBiaoMixTemp";
            this.YiBiaoMixTemp.NewuScaleAuto = true;
            this.YiBiaoMixTemp.NewuScaleBatch = "9999";
            this.YiBiaoMixTemp.NewuScaleNameTxt = "密炼机";
            this.YiBiaoMixTemp.NewuScaleOverAlarm = false;
            this.YiBiaoMixTemp.NewuScaleStartRun = false;
            this.YiBiaoMixTemp.NewuScaleToolTip = "";
            this.YiBiaoMixTemp.NewuScaleValue = "100.23";
            this.YiBiaoMixTemp.NewuYiBiaoUnit = "℃";
            this.YiBiaoMixTemp.Size = new System.Drawing.Size(110, 72);
            this.YiBiaoMixTemp.TabIndex = 4;
            // 
            // YiBiaoZon
            // 
            this.YiBiaoZon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YiBiaoZon.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YiBiaoZon.Location = new System.Drawing.Point(1054, 5);
            this.YiBiaoZon.Name = "YiBiaoZon";
            this.YiBiaoZon.NewuDisplayMsg = "物料：6607MB\r\n标准：100.01kg\r\n设定：100.00";
            this.YiBiaoZon.NewuScaleAuto = false;
            this.YiBiaoZon.NewuScaleNameTxt = "粉料秤";
            this.YiBiaoZon.NewuScaleOverAlarm = false;
            this.YiBiaoZon.NewuScaleStartRun = false;
            this.YiBiaoZon.NewuScaleToolTip = "磅秤净值";
            this.YiBiaoZon.NewuScaleValue = "100.23";
            this.YiBiaoZon.NewuYiBiaoUnit = "kg";
            this.YiBiaoZon.Size = new System.Drawing.Size(161, 121);
            this.YiBiaoZon.TabIndex = 20;
            // 
            // FM_MonitorMixerGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel7);
            this.Name = "FM_MonitorMixerGrid";
            this.Text = "母炼秤值详情";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FM_MonitorMixerGrid_FormClosing);
            this.Load += new System.EventHandler(this.FM_MonitorMixerGrid_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeight)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTech)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private NewuCommon.DataGridViewEx dgvWeight;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private NewuCommon.DataGridViewEx dgvTech;
        private NewuControl.MixPart mixPart1;
        private System.Windows.Forms.Panel panel7;
        private NewuControl.NewuYiBiao2 YiBiaoPressSpeed;
        private NewuControl.NewuYiBiao2 YiBiaoPowerEnergy;
        private NewuControl.NewuYiBiao YiBiaoTimeTime;
        private NewuControl.Scale YiBiaoMixTemp;
        private NewuControl.ScaleDetail YiBiaoOil;
        private NewuControl.ScaleDetail YiBiaoPla;
        private NewuControl.ScaleDetail YiBiaoCarbon;
        private NewuControl.ScaleDetail YiBiaoDrug;
        private NewuControl.ScaleDetail YiBiaoRubber;
        private NewuControl.ScaleDetail YiBiaoSil;
        private NewuControl.ScaleDetail YiBiaoZon;


    }
}