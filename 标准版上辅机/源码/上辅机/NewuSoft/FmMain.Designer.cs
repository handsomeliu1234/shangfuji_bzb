namespace NewuSoft
{
    partial class FmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.IndexTabPage = new System.Windows.Forms.TabPage();
            this.toolStripStatusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuItemClosePage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newuToolBarMenu = new NewuCommon.NewuToolBar();
            this.menuStripSys = new System.Windows.Forms.MenuStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1904, 986);
            this.panel1.TabIndex = 36;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.IndexTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1904, 986);
            this.tabControl1.TabIndex = 31;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl1_DrawItem);
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TabControl1_MouseDoubleClick);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TabControl1_MouseDown);
            // 
            // IndexTabPage
            // 
            this.IndexTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.IndexTabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.IndexTabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IndexTabPage.Location = new System.Drawing.Point(4, 24);
            this.IndexTabPage.Name = "IndexTabPage";
            this.IndexTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IndexTabPage.Size = new System.Drawing.Size(1896, 958);
            this.IndexTabPage.TabIndex = 1;
            this.IndexTabPage.Text = "首页";
            // 
            // toolStripStatusTime
            // 
            this.toolStripStatusTime.Name = "toolStripStatusTime";
            this.toolStripStatusTime.Size = new System.Drawing.Size(120, 21);
            this.toolStripStatusTime.Text = "时间：12:01:01";
            // 
            // toolStripStatusUser
            // 
            this.toolStripStatusUser.Name = "toolStripStatusUser";
            this.toolStripStatusUser.Size = new System.Drawing.Size(102, 21);
            this.toolStripStatusUser.Text = "用户：wings";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusTime,
            this.toolStripStatusUser,
            this.toolStripStatusVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1035);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1904, 26);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusVersion
            // 
            this.toolStripStatusVersion.Name = "toolStripStatusVersion";
            this.toolStripStatusVersion.Size = new System.Drawing.Size(126, 21);
            this.toolStripStatusVersion.Text = "系统版本:单机版";
            // 
            // MenuItemClosePage
            // 
            this.MenuItemClosePage.Name = "MenuItemClosePage";
            this.MenuItemClosePage.Size = new System.Drawing.Size(112, 26);
            this.MenuItemClosePage.Text = "关闭";
            this.MenuItemClosePage.Click += new System.EventHandler(this.MenuItemClosePage_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemClosePage});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 30);
            // 
            // newuToolBarMenu
            // 
            this.newuToolBarMenu.AutoSize = false;
            this.newuToolBarMenu.BackColor = System.Drawing.SystemColors.Control;
            this.newuToolBarMenu.Location = new System.Drawing.Point(0, 24);
            this.newuToolBarMenu.Name = "newuToolBarMenu";
            this.newuToolBarMenu.Size = new System.Drawing.Size(1904, 25);
            this.newuToolBarMenu.TabIndex = 2;
            this.newuToolBarMenu.Text = "newuToolBar1";
            // 
            // menuStripSys
            // 
            this.menuStripSys.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripSys.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStripSys.Location = new System.Drawing.Point(0, 0);
            this.menuStripSys.Name = "menuStripSys";
            this.menuStripSys.Size = new System.Drawing.Size(1904, 24);
            this.menuStripSys.TabIndex = 1;
            this.menuStripSys.Text = "menuStripSys";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(1712, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 80);
            this.panel2.TabIndex = 0;
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1904, 1061);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.newuToolBarMenu);
            this.Controls.Add(this.menuStripSys);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripSys;
            this.Name = "FmMain";
            this.Text = "万向新元智能密炼上辅机系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FmMain_Activated);
            this.Deactivate += new System.EventHandler(this.FmMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmMain_FormClosing);
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FmMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NewuCommon.NewuToolBar newuToolBarMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage IndexTabPage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusUser;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClosePage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusVersion;
        private System.Windows.Forms.MenuStrip menuStripSys;
        private System.Windows.Forms.Panel panel2;
    }
}

