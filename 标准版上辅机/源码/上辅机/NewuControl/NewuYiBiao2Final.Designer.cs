namespace NewuCommon
{
    partial class NewuYiBiao2Final
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
            this.btnScaleName = new System.Windows.Forms.Button();
            this.txtScaleValue = new System.Windows.Forms.TextBox();
            this.labUnit = new System.Windows.Forms.Label();
            this.labUnit2 = new System.Windows.Forms.Label();
            this.txtScaleValue2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnScaleName
            // 
            this.btnScaleName.Enabled = false;
            this.btnScaleName.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnScaleName.Location = new System.Drawing.Point(0, 0);
            this.btnScaleName.Name = "btnScaleName";
            this.btnScaleName.Size = new System.Drawing.Size(251, 56);
            this.btnScaleName.TabIndex = 6;
            this.btnScaleName.Text = "磅秤名称";
            this.btnScaleName.UseVisualStyleBackColor = true;
            // 
            // txtScaleValue
            // 
            this.txtScaleValue.BackColor = System.Drawing.Color.Black;
            this.txtScaleValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScaleValue.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScaleValue.ForeColor = System.Drawing.Color.Red;
            this.txtScaleValue.Location = new System.Drawing.Point(0, 55);
            this.txtScaleValue.Name = "txtScaleValue";
            this.txtScaleValue.ReadOnly = true;
            this.txtScaleValue.Size = new System.Drawing.Size(213, 43);
            this.txtScaleValue.TabIndex = 8;
            this.txtScaleValue.Text = "110.7";
            this.txtScaleValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labUnit
            // 
            this.labUnit.AutoSize = true;
            this.labUnit.BackColor = System.Drawing.Color.Black;
            this.labUnit.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUnit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labUnit.Location = new System.Drawing.Point(217, 69);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(34, 21);
            this.labUnit.TabIndex = 7;
            this.labUnit.Text = "kg";
            // 
            // labUnit2
            // 
            this.labUnit2.AutoSize = true;
            this.labUnit2.BackColor = System.Drawing.Color.Black;
            this.labUnit2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUnit2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labUnit2.Location = new System.Drawing.Point(217, 117);
            this.labUnit2.Name = "labUnit2";
            this.labUnit2.Size = new System.Drawing.Size(34, 21);
            this.labUnit2.TabIndex = 9;
            this.labUnit2.Text = "kg";
            // 
            // txtScaleValue2
            // 
            this.txtScaleValue2.BackColor = System.Drawing.Color.Black;
            this.txtScaleValue2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScaleValue2.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScaleValue2.ForeColor = System.Drawing.Color.Red;
            this.txtScaleValue2.Location = new System.Drawing.Point(0, 103);
            this.txtScaleValue2.Name = "txtScaleValue2";
            this.txtScaleValue2.ReadOnly = true;
            this.txtScaleValue2.Size = new System.Drawing.Size(210, 43);
            this.txtScaleValue2.TabIndex = 10;
            this.txtScaleValue2.Text = "110.7";
            this.txtScaleValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NewuYiBiao2Final
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.labUnit2);
            this.Controls.Add(this.txtScaleValue2);
            this.Controls.Add(this.labUnit);
            this.Controls.Add(this.btnScaleName);
            this.Controls.Add(this.txtScaleValue);
            this.Name = "NewuYiBiao2Final";
            this.Size = new System.Drawing.Size(251, 141);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScaleName;
        private System.Windows.Forms.TextBox txtScaleValue;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.Label labUnit2;
        private System.Windows.Forms.TextBox txtScaleValue2;
    }
}
