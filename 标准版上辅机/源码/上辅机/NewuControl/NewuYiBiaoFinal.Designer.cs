﻿namespace NewuCommon
{
    partial class NewuYiBiaoFinal
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
            this.components = new System.ComponentModel.Container();
            this.txtScaleValue = new System.Windows.Forms.TextBox();
            this.labUnit = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnScaleName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtScaleValue
            // 
            this.txtScaleValue.BackColor = System.Drawing.Color.Black;
            this.txtScaleValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScaleValue.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScaleValue.ForeColor = System.Drawing.Color.White;
            this.txtScaleValue.Location = new System.Drawing.Point(3, 46);
            this.txtScaleValue.Name = "txtScaleValue";
            this.txtScaleValue.ReadOnly = true;
            this.txtScaleValue.Size = new System.Drawing.Size(176, 43);
            this.txtScaleValue.TabIndex = 8;
            this.txtScaleValue.Text = "45.9";
            this.txtScaleValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labUnit
            // 
            this.labUnit.AutoSize = true;
            this.labUnit.BackColor = System.Drawing.Color.Black;
            this.labUnit.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUnit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labUnit.Location = new System.Drawing.Point(185, 68);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(22, 21);
            this.labUnit.TabIndex = 7;
            this.labUnit.Text = "s";
            // 
            // btnScaleName
            // 
            this.btnScaleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btnScaleName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.btnScaleName.Font = new System.Drawing.Font("黑体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScaleName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnScaleName.Location = new System.Drawing.Point(0, 3);
            this.btnScaleName.Name = "btnScaleName";
            this.btnScaleName.Size = new System.Drawing.Size(212, 40);
            this.btnScaleName.TabIndex = 9;
            this.btnScaleName.Text = "配方时间";
            this.btnScaleName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NewuYiBiaoFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.btnScaleName);
            this.Controls.Add(this.labUnit);
            this.Controls.Add(this.txtScaleValue);
            this.Name = "NewuYiBiaoFinal";
            this.Size = new System.Drawing.Size(212, 93);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtScaleValue;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox btnScaleName;
    }
}
