namespace NewuTB.TB
{
    partial class FM_TB_FeedingParam_Add2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_TB_FeedingParam_Add2));
            this.lab_BinNoVeri = new System.Windows.Forms.Label();
            this.cmb_TypeCodeID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbBinNo = new System.Windows.Forms.TextBox();
            this.cmb_DeviceID = new System.Windows.Forms.ComboBox();
            this.labEquipmentTyp = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BigFreqZhong = new System.Windows.Forms.TextBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BigFreqKuai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_BinNoVeri
            // 
            this.lab_BinNoVeri.AutoSize = true;
            this.lab_BinNoVeri.ForeColor = System.Drawing.Color.Red;
            this.lab_BinNoVeri.Location = new System.Drawing.Point(387, 52);
            this.lab_BinNoVeri.Name = "lab_BinNoVeri";
            this.lab_BinNoVeri.Size = new System.Drawing.Size(140, 14);
            this.lab_BinNoVeri.TabIndex = 109;
            this.lab_BinNoVeri.Text = "*不能为空且为正整数";
            // 
            // cmb_TypeCodeID
            // 
            this.cmb_TypeCodeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_TypeCodeID.FormattingEnabled = true;
            this.cmb_TypeCodeID.Location = new System.Drawing.Point(184, 196);
            this.cmb_TypeCodeID.Name = "cmb_TypeCodeID";
            this.cmb_TypeCodeID.Size = new System.Drawing.Size(194, 22);
            this.cmb_TypeCodeID.TabIndex = 105;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(61, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 14);
            this.label6.TabIndex = 108;
            this.label6.Text = "储罐编号：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBinNo
            // 
            this.tbBinNo.Location = new System.Drawing.Point(184, 49);
            this.tbBinNo.Name = "tbBinNo";
            this.tbBinNo.Size = new System.Drawing.Size(194, 23);
            this.tbBinNo.TabIndex = 103;
            // 
            // cmb_DeviceID
            // 
            this.cmb_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DeviceID.FormattingEnabled = true;
            this.cmb_DeviceID.Location = new System.Drawing.Point(184, 166);
            this.cmb_DeviceID.Name = "cmb_DeviceID";
            this.cmb_DeviceID.Size = new System.Drawing.Size(194, 22);
            this.cmb_DeviceID.TabIndex = 104;
            // 
            // labEquipmentTyp
            // 
            this.labEquipmentTyp.Location = new System.Drawing.Point(61, 203);
            this.labEquipmentTyp.Name = "labEquipmentTyp";
            this.labEquipmentTyp.Size = new System.Drawing.Size(117, 14);
            this.labEquipmentTyp.TabIndex = 107;
            this.labEquipmentTyp.Text = "材料类型：";
            this.labEquipmentTyp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(61, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 14);
            this.label10.TabIndex = 106;
            this.label10.Text = "设备：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(61, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 14);
            this.label1.TabIndex = 111;
            this.label1.Text = "螺旋中速：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BigFreqZhong
            // 
            this.BigFreqZhong.Location = new System.Drawing.Point(184, 120);
            this.BigFreqZhong.Name = "BigFreqZhong";
            this.BigFreqZhong.Size = new System.Drawing.Size(194, 23);
            this.BigFreqZhong.TabIndex = 110;
            // 
            // btClose
            // 
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btClose.Image = ((System.Drawing.Image)(resources.GetObject("btClose.Image")));
            this.btClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btClose.Location = new System.Drawing.Point(325, 268);
            this.btClose.Name = "btClose";
            this.btClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btClose.Size = new System.Drawing.Size(76, 30);
            this.btClose.TabIndex = 115;
            this.btClose.Text = "关闭";
            this.btClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // btSave
            // 
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btSave.Image = ((System.Drawing.Image)(resources.GetObject("btSave.Image")));
            this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSave.Location = new System.Drawing.Point(182, 268);
            this.btSave.Name = "btSave";
            this.btSave.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btSave.Size = new System.Drawing.Size(76, 30);
            this.btSave.TabIndex = 114;
            this.btSave.TabStop = false;
            this.btSave.Text = "保存";
            this.btSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.BtSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(387, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 14);
            this.label3.TabIndex = 116;
            this.label3.Text = "*不能为空且为正整数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(387, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 14);
            this.label4.TabIndex = 117;
            this.label4.Text = "*不能为空且为正整数";
            // 
            // BigFreqKuai
            // 
            this.BigFreqKuai.Location = new System.Drawing.Point(184, 85);
            this.BigFreqKuai.Name = "BigFreqKuai";
            this.BigFreqKuai.Size = new System.Drawing.Size(194, 23);
            this.BigFreqKuai.TabIndex = 112;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(61, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 14);
            this.label2.TabIndex = 113;
            this.label2.Text = "螺旋快速：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FM_TB_FeedingParam_Add2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 376);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BigFreqZhong);
            this.Controls.Add(this.BigFreqKuai);
            this.Controls.Add(this.lab_BinNoVeri);
            this.Controls.Add(this.cmb_TypeCodeID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbBinNo);
            this.Controls.Add(this.cmb_DeviceID);
            this.Controls.Add(this.labEquipmentTyp);
            this.Controls.Add(this.label10);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_TB_FeedingParam_Add2";
            this.Text = "FM_TB_FeedingParam_Add2";
            this.Load += new System.EventHandler(this.FM_TB_FeedingParam_Add2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_BinNoVeri;
        private System.Windows.Forms.ComboBox cmb_TypeCodeID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbBinNo;
        private System.Windows.Forms.ComboBox cmb_DeviceID;
        private System.Windows.Forms.Label labEquipmentTyp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BigFreqZhong;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox BigFreqKuai;
        private System.Windows.Forms.Label label2;
    }
}