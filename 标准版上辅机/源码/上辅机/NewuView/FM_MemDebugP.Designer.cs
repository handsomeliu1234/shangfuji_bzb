namespace NewuView
{
    partial class FM_MemDebugP
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_MemDebugP));
            this.btnOpen = new System.Windows.Forms.Button();
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWriteAddr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWriteValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(442, 15);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(84, 30);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "初始化";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tmrTime
            // 
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Location = new System.Drawing.Point(313, 106);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(54, 18);
            this.chkAuto.TabIndex = 2;
            this.chkAuto.Text = "Auto";
            this.chkAuto.UseVisualStyleBackColor = true;
            this.chkAuto.Click += new System.EventHandler(this.chkAuto_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(96, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 23);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(96, 58);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(210, 23);
            this.textBox2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "长度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "开始地址：";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(96, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(210, 23);
            this.textBox3.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "值：";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(313, 58);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(76, 30);
            this.btnRead.TabIndex = 11;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 30);
            this.button1.TabIndex = 12;
            this.button1.Text = "区域最大范围";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(313, 227);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(76, 30);
            this.btnWrite.TabIndex = 20;
            this.btnWrite.Text = "写地址";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 19;
            this.label4.Text = "值：";
            // 
            // txtWriteAddr
            // 
            this.txtWriteAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWriteAddr.Location = new System.Drawing.Point(96, 189);
            this.txtWriteAddr.Name = "txtWriteAddr";
            this.txtWriteAddr.Size = new System.Drawing.Size(210, 23);
            this.txtWriteAddr.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 17;
            this.label5.Text = "地址：";
            // 
            // txtWriteValue
            // 
            this.txtWriteValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWriteValue.Location = new System.Drawing.Point(96, 231);
            this.txtWriteValue.Name = "txtWriteValue";
            this.txtWriteValue.Size = new System.Drawing.Size(210, 23);
            this.txtWriteValue.TabIndex = 14;
            // 
            // FM_MemDebugP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 273);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWriteAddr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWriteValue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.btnOpen);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_MemDebugP";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FM_Test_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWriteAddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWriteValue;
    }
}

