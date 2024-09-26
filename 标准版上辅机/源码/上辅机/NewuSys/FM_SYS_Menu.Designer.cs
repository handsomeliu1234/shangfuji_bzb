
namespace NewuSys
{
    partial class FM_SYS_Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_SYS_Menu));
            CommonTools.TreeListColumn treeListColumn1 = new CommonTools.TreeListColumn("Caption", "标题");
            CommonTools.TreeListColumn treeListColumn2 = new CommonTools.TreeListColumn("ControlType", "控件类型");
            CommonTools.TreeListColumn treeListColumn3 = new CommonTools.TreeListColumn("ShowDialog", "对话框形式");
            CommonTools.TreeListColumn treeListColumn4 = new CommonTools.TreeListColumn("ControlText", "功能描述");
            CommonTools.TreeListColumn treeListColumn5 = new CommonTools.TreeListColumn("MenuID", "ID");
            CommonTools.TreeListColumn treeListColumn6 = new CommonTools.TreeListColumn("AutoShow", "启动状态");
            CommonTools.TreeListColumn treeListColumn7 = new CommonTools.TreeListColumn("Show", "显示排序");
            CommonTools.TreeListColumn treeListColumn8 = new CommonTools.TreeListColumn("ResourceID", "资源ID");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAddChild = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeListView1 = new CommonTools.TreeListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnAddChild);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnDel);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(880, 461);
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnAddChild
            // 
            this.btnAddChild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddChild.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddChild.Image = ((System.Drawing.Image)(resources.GetObject("btnAddChild.Image")));
            this.btnAddChild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddChild.Location = new System.Drawing.Point(149, 8);
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(122, 30);
            this.btnAddChild.TabIndex = 9;
            this.btnAddChild.Text = "新增子节点";
            this.btnAddChild.UseVisualStyleBackColor = true;
            this.btnAddChild.Click += new System.EventHandler(this.BtnAddChild_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(464, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(373, 8);
            this.btnDel.Name = "btnDel";
            this.btnDel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDel.Size = new System.Drawing.Size(76, 30);
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "删除";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(281, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEdit.Size = new System.Drawing.Size(76, 30);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "编辑";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(14, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 30);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "新增顶级节点";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeListView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 406);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模块信息列表";
            // 
            // treeListView1
            // 
            treeListColumn1.AutoSize = true;
            treeListColumn1.AutoSizeMinSize = 0;
            treeListColumn1.Width = 50;
            treeListColumn2.AutoSize = true;
            treeListColumn2.AutoSizeMinSize = 0;
            treeListColumn2.Width = 50;
            treeListColumn3.AutoSize = true;
            treeListColumn3.AutoSizeMinSize = 0;
            treeListColumn3.Width = 50;
            treeListColumn4.AutoSize = true;
            treeListColumn4.AutoSizeMinSize = 0;
            treeListColumn4.Width = 50;
            treeListColumn5.AutoSizeMinSize = 0;
            treeListColumn5.Width = 0;
            treeListColumn6.AutoSize = true;
            treeListColumn6.AutoSizeMinSize = 0;
            treeListColumn6.Width = 50;
            treeListColumn7.AutoSize = true;
            treeListColumn7.AutoSizeMinSize = 0;
            treeListColumn7.Width = 50;
            treeListColumn8.AutoSizeMinSize = 0;
            treeListColumn8.Width = 50;
            this.treeListView1.Columns.AddRange(new CommonTools.TreeListColumn[] {
            treeListColumn1,
            treeListColumn2,
            treeListColumn3,
            treeListColumn4,
            treeListColumn5,
            treeListColumn6,
            treeListColumn7,
            treeListColumn8});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeListView1.Images = null;
            this.treeListView1.Location = new System.Drawing.Point(3, 19);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.RowOptions.ItemHeight = 23;
            this.treeListView1.Size = new System.Drawing.Size(874, 384);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.Text = "treeListView1";
            // 
            // FM_SYS_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 461);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FM_SYS_Menu";
            this.Text = "菜单信息管理";
            this.Load += new System.EventHandler(this.FM_SYS_Menu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddChild;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private CommonTools.TreeListView treeListView1;
    }
}