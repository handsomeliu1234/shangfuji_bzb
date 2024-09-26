namespace NewuTB.Formula
{
    partial class FM_FormulaMix_AddStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_FormulaMix_AddStep));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkListMixUp = new System.Windows.Forms.CheckedListBox();
            this.checkListDrop = new System.Windows.Forms.CheckedListBox();
            this.radioButtonMixUp = new System.Windows.Forms.RadioButton();
            this.radioButtonDrop = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_StepSpeed = new System.Windows.Forms.ComboBox();
            this.cmb_StepPress = new System.Windows.Forms.ComboBox();
            this.cmb_ActionControlCode = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_KeepTime = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_StepEnergy = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_StepPower = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_StepTemp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_StepTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_StepOrder = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(681, 488);
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 17;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(110, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(15, 14);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnOk.Size = new System.Drawing.Size(76, 30);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "确定";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkListMixUp);
            this.groupBox1.Controls.Add(this.checkListDrop);
            this.groupBox1.Controls.Add(this.radioButtonMixUp);
            this.groupBox1.Controls.Add(this.radioButtonDrop);
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 216);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工艺步骤选择";
            // 
            // checkListMixUp
            // 
            this.checkListMixUp.FormattingEnabled = true;
            this.checkListMixUp.Location = new System.Drawing.Point(345, 49);
            this.checkListMixUp.MultiColumn = true;
            this.checkListMixUp.Name = "checkListMixUp";
            this.checkListMixUp.Size = new System.Drawing.Size(292, 148);
            this.checkListMixUp.TabIndex = 26;
            this.checkListMixUp.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckListMixUp_ItemCheck);
            // 
            // checkListDrop
            // 
            this.checkListDrop.FormattingEnabled = true;
            this.checkListDrop.Location = new System.Drawing.Point(33, 49);
            this.checkListDrop.Name = "checkListDrop";
            this.checkListDrop.Size = new System.Drawing.Size(270, 148);
            this.checkListDrop.TabIndex = 25;
            // 
            // radioButtonMixUp
            // 
            this.radioButtonMixUp.AutoSize = true;
            this.radioButtonMixUp.Location = new System.Drawing.Point(345, 27);
            this.radioButtonMixUp.Name = "radioButtonMixUp";
            this.radioButtonMixUp.Size = new System.Drawing.Size(67, 18);
            this.radioButtonMixUp.TabIndex = 22;
            this.radioButtonMixUp.Text = "密炼机";
            this.radioButtonMixUp.UseVisualStyleBackColor = true;
            // 
            // radioButtonDrop
            // 
            this.radioButtonDrop.AutoSize = true;
            this.radioButtonDrop.Checked = true;
            this.radioButtonDrop.Location = new System.Drawing.Point(35, 27);
            this.radioButtonDrop.Name = "radioButtonDrop";
            this.radioButtonDrop.Size = new System.Drawing.Size(81, 18);
            this.radioButtonDrop.TabIndex = 23;
            this.radioButtonDrop.TabStop = true;
            this.radioButtonDrop.Text = "投料步骤";
            this.radioButtonDrop.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_StepSpeed);
            this.groupBox2.Controls.Add(this.cmb_StepPress);
            this.groupBox2.Controls.Add(this.cmb_ActionControlCode);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txt_KeepTime);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txt_StepEnergy);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txt_StepPower);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txt_StepTemp);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txt_StepTime);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmb_StepOrder);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(14, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(659, 192);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "密炼详细数据设定";
            // 
            // cmb_StepSpeed
            // 
            this.cmb_StepSpeed.FormattingEnabled = true;
            this.cmb_StepSpeed.Location = new System.Drawing.Point(455, 124);
            this.cmb_StepSpeed.Name = "cmb_StepSpeed";
            this.cmb_StepSpeed.Size = new System.Drawing.Size(152, 22);
            this.cmb_StepSpeed.TabIndex = 48;
            // 
            // cmb_StepPress
            // 
            this.cmb_StepPress.FormattingEnabled = true;
            this.cmb_StepPress.Location = new System.Drawing.Point(118, 124);
            this.cmb_StepPress.Name = "cmb_StepPress";
            this.cmb_StepPress.Size = new System.Drawing.Size(154, 22);
            this.cmb_StepPress.TabIndex = 47;
            // 
            // cmb_ActionControlCode
            // 
            this.cmb_ActionControlCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ActionControlCode.FormattingEnabled = true;
            this.cmb_ActionControlCode.Location = new System.Drawing.Point(455, 30);
            this.cmb_ActionControlCode.Name = "cmb_ActionControlCode";
            this.cmb_ActionControlCode.Size = new System.Drawing.Size(152, 22);
            this.cmb_ActionControlCode.TabIndex = 46;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Blue;
            this.label21.Location = new System.Drawing.Point(281, 156);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(21, 14);
            this.label21.TabIndex = 44;
            this.label21.Text = "*s";
            // 
            // txt_KeepTime
            // 
            this.txt_KeepTime.AcceptsTab = true;
            this.txt_KeepTime.Location = new System.Drawing.Point(118, 153);
            this.txt_KeepTime.Name = "txt_KeepTime";
            this.txt_KeepTime.Size = new System.Drawing.Size(154, 23);
            this.txt_KeepTime.TabIndex = 43;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 156);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 14);
            this.label22.TabIndex = 42;
            this.label22.Text = "保持时间：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(612, 127);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(21, 14);
            this.label19.TabIndex = 41;
            this.label19.Text = "*r";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(337, 127);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 14);
            this.label20.TabIndex = 39;
            this.label20.Text = "转速：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(278, 127);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 14);
            this.label17.TabIndex = 38;
            this.label17.Text = "*kPa";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(20, 126);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 14);
            this.label18.TabIndex = 36;
            this.label18.Text = "压力：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(612, 96);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 14);
            this.label15.TabIndex = 35;
            this.label15.Text = "*kW.h";
            // 
            // txt_StepEnergy
            // 
            this.txt_StepEnergy.AcceptsReturn = true;
            this.txt_StepEnergy.Location = new System.Drawing.Point(455, 92);
            this.txt_StepEnergy.Name = "txt_StepEnergy";
            this.txt_StepEnergy.Size = new System.Drawing.Size(152, 23);
            this.txt_StepEnergy.TabIndex = 34;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(337, 96);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 14);
            this.label16.TabIndex = 33;
            this.label16.Text = "能量：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(276, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 14);
            this.label13.TabIndex = 32;
            this.label13.Text = "*kW";
            // 
            // txt_StepPower
            // 
            this.txt_StepPower.Location = new System.Drawing.Point(118, 92);
            this.txt_StepPower.Name = "txt_StepPower";
            this.txt_StepPower.Size = new System.Drawing.Size(154, 23);
            this.txt_StepPower.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 14);
            this.label14.TabIndex = 30;
            this.label14.Text = "功率：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(612, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 14);
            this.label11.TabIndex = 29;
            this.label11.Text = "*℃";
            // 
            // txt_StepTemp
            // 
            this.txt_StepTemp.Location = new System.Drawing.Point(455, 61);
            this.txt_StepTemp.Name = "txt_StepTemp";
            this.txt_StepTemp.Size = new System.Drawing.Size(152, 23);
            this.txt_StepTemp.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 27;
            this.label12.Text = "温度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(275, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 14);
            this.label10.TabIndex = 25;
            this.label10.Text = "*s";
            // 
            // txt_StepTime
            // 
            this.txt_StepTime.Location = new System.Drawing.Point(118, 61);
            this.txt_StepTime.Name = "txt_StepTime";
            this.txt_StepTime.Size = new System.Drawing.Size(154, 23);
            this.txt_StepTime.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 23;
            this.label9.Text = "时间：";
            // 
            // cmb_StepOrder
            // 
            this.cmb_StepOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_StepOrder.FormattingEnabled = true;
            this.cmb_StepOrder.Location = new System.Drawing.Point(118, 30);
            this.cmb_StepOrder.Name = "cmb_StepOrder";
            this.cmb_StepOrder.Size = new System.Drawing.Size(154, 22);
            this.cmb_StepOrder.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 20;
            this.label7.Text = "步骤顺序：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(337, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 14);
            this.label6.TabIndex = 18;
            this.label6.Text = "动作控制方式：";
            // 
            // FM_FormulaMix_AddStep2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 488);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FM_FormulaMix_AddStep2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配方工艺数据";
            this.Load += new System.EventHandler(this.FM_FormulaMix_AddStep_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_StepOrder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_StepTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_StepTemp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_StepPower;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_StepEnergy;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_KeepTime;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmb_ActionControlCode;
        private System.Windows.Forms.ComboBox cmb_StepSpeed;
        private System.Windows.Forms.ComboBox cmb_StepPress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkListMixUp;
        private System.Windows.Forms.CheckedListBox checkListDrop;
        private System.Windows.Forms.RadioButton radioButtonMixUp;
        private System.Windows.Forms.RadioButton radioButtonDrop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
    }
}