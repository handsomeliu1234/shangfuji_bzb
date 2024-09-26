namespace Newu.Control
{
    partial class PagerControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.linkFirst = new System.Windows.Forms.Button();
            this.linkPrevious = new System.Windows.Forms.Button();
            this.linkNext = new System.Windows.Forms.Button();
            this.linkLast = new System.Windows.Forms.Button();
            this.txtPageNum = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(277, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPageSize.Location = new System.Drawing.Point(382, 9);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(75, 30);
            this.txtPageSize.TabIndex = 1;
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPageSize.TextChanged += new System.EventHandler(this.txtPageSize_TextChanged);
            this.txtPageSize.Leave += new System.EventHandler(this.txtPageSize_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(464, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "条";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(520, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "当前页:";
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPage.ForeColor = System.Drawing.Color.Red;
            this.lblCurrentPage.Location = new System.Drawing.Point(625, 14);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(21, 21);
            this.lblCurrentPage.TabIndex = 4;
            this.lblCurrentPage.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(667, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "/";
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPageCount.ForeColor = System.Drawing.Color.Red;
            this.lblPageCount.Location = new System.Drawing.Point(709, 14);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(21, 21);
            this.lblPageCount.TabIndex = 6;
            this.lblPageCount.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(754, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 21);
            this.label7.TabIndex = 7;
            this.label7.Text = "共";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalCount.Location = new System.Drawing.Point(805, 14);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(43, 21);
            this.lblTotalCount.TabIndex = 8;
            this.lblTotalCount.Text = "100";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(873, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 21);
            this.label9.TabIndex = 9;
            this.label9.Text = "条记录";
            // 
            // linkFirst
            // 
            this.linkFirst.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkFirst.ForeColor = System.Drawing.Color.Blue;
            this.linkFirst.Location = new System.Drawing.Point(993, 7);
            this.linkFirst.Name = "linkFirst";
            this.linkFirst.Size = new System.Drawing.Size(76, 35);
            this.linkFirst.TabIndex = 10;
            this.linkFirst.Text = "|<<";
            this.linkFirst.UseVisualStyleBackColor = true;
            this.linkFirst.Click += new System.EventHandler(this.linkFirst_Click);
            // 
            // linkPrevious
            // 
            this.linkPrevious.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkPrevious.ForeColor = System.Drawing.Color.Blue;
            this.linkPrevious.Location = new System.Drawing.Point(1079, 7);
            this.linkPrevious.Name = "linkPrevious";
            this.linkPrevious.Size = new System.Drawing.Size(76, 35);
            this.linkPrevious.TabIndex = 11;
            this.linkPrevious.Text = "<";
            this.linkPrevious.UseVisualStyleBackColor = true;
            this.linkPrevious.Click += new System.EventHandler(this.linkPrevious_Click);
            // 
            // linkNext
            // 
            this.linkNext.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkNext.ForeColor = System.Drawing.Color.Blue;
            this.linkNext.Location = new System.Drawing.Point(1165, 7);
            this.linkNext.Name = "linkNext";
            this.linkNext.Size = new System.Drawing.Size(76, 35);
            this.linkNext.TabIndex = 12;
            this.linkNext.Text = ">";
            this.linkNext.UseVisualStyleBackColor = true;
            this.linkNext.Click += new System.EventHandler(this.linkNext_Click);
            // 
            // linkLast
            // 
            this.linkLast.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLast.ForeColor = System.Drawing.Color.Blue;
            this.linkLast.Location = new System.Drawing.Point(1252, 7);
            this.linkLast.Name = "linkLast";
            this.linkLast.Size = new System.Drawing.Size(76, 35);
            this.linkLast.TabIndex = 13;
            this.linkLast.Text = ">>|";
            this.linkLast.UseVisualStyleBackColor = true;
            this.linkLast.Click += new System.EventHandler(this.linkLast_Click);
            // 
            // txtPageNum
            // 
            this.txtPageNum.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPageNum.Location = new System.Drawing.Point(1338, 9);
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(75, 31);
            this.txtPageNum.TabIndex = 14;
            this.txtPageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPageNum.TextChanged += new System.EventHandler(this.txtPageNum_TextChanged);
            this.txtPageNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageNum_KeyPress);
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGo.ForeColor = System.Drawing.Color.Black;
            this.btnGo.Location = new System.Drawing.Point(1424, 7);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(99, 35);
            this.btnGo.TabIndex = 15;
            this.btnGo.Text = "跳转";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // PagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtPageNum);
            this.Controls.Add(this.linkLast);
            this.Controls.Add(this.linkNext);
            this.Controls.Add(this.linkPrevious);
            this.Controls.Add(this.linkFirst);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblPageCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCurrentPage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPageSize);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PagerControl";
            this.Size = new System.Drawing.Size(2240, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPageSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button linkFirst;
        private System.Windows.Forms.Button linkPrevious;
        private System.Windows.Forms.Button linkNext;
        private System.Windows.Forms.Button linkLast;
        private System.Windows.Forms.TextBox txtPageNum;
        private System.Windows.Forms.Button btnGo;
    }
}
