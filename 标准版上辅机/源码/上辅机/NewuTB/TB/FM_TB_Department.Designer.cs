namespace NewuTB.TB
{
    partial class FM_TB_Department
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_TB_Department));
            CommonTools.TreeListColumn treeListColumn1 = new CommonTools.TreeListColumn("DepartmentName", "部门名称");
            CommonTools.TreeListColumn treeListColumn2 = new CommonTools.TreeListColumn("DepartmentCode", "部门编号");
            CommonTools.TreeListColumn treeListColumn3 = new CommonTools.TreeListColumn("DepartmentRemark", "备注");
            CommonTools.TreeListColumn treeListColumn4 = new CommonTools.TreeListColumn("DepartmentID", "部门ID");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.hint = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_DepartmentRemark = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_ParentDepartmentID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_DepartmentJaneSpell = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_DepartmentCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_DepartmentName = new System.Windows.Forms.TextBox();
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
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.hint);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txt_DepartmentRemark);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.cmb_ParentDepartmentID);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txt_DepartmentJaneSpell);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txt_DepartmentCode);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txt_DepartmentName);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddChild);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnDel);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.Panel1MinSize = 75;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1612, 708);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // hint
            // 
            this.hint.AutoSize = true;
            this.hint.Font = new System.Drawing.Font("黑体", 15F);
            this.hint.ForeColor = System.Drawing.Color.Red;
            this.hint.Location = new System.Drawing.Point(757, 46);
            this.hint.Name = "hint";
            this.hint.Size = new System.Drawing.Size(49, 20);
            this.hint.TabIndex = 53;
            this.hint.Text = "提示";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(684, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 14);
            this.label5.TabIndex = 45;
            this.label5.Text = "部门备注：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DepartmentRemark
            // 
            this.txt_DepartmentRemark.AcceptsReturn = true;
            this.txt_DepartmentRemark.Location = new System.Drawing.Point(839, 12);
            this.txt_DepartmentRemark.Multiline = true;
            this.txt_DepartmentRemark.Name = "txt_DepartmentRemark";
            this.txt_DepartmentRemark.Size = new System.Drawing.Size(159, 23);
            this.txt_DepartmentRemark.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(667, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 14);
            this.label8.TabIndex = 43;
            this.label8.Text = "*";
            // 
            // cmb_ParentDepartmentID
            // 
            this.cmb_ParentDepartmentID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ParentDepartmentID.FormattingEnabled = true;
            this.cmb_ParentDepartmentID.Location = new System.Drawing.Point(500, 46);
            this.cmb_ParentDepartmentID.Name = "cmb_ParentDepartmentID";
            this.cmb_ParentDepartmentID.Size = new System.Drawing.Size(159, 22);
            this.cmb_ParentDepartmentID.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(345, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "部门层级：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(345, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 14);
            this.label3.TabIndex = 40;
            this.label3.Text = "部门简拼：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DepartmentJaneSpell
            // 
            this.txt_DepartmentJaneSpell.AcceptsReturn = true;
            this.txt_DepartmentJaneSpell.Location = new System.Drawing.Point(500, 10);
            this.txt_DepartmentJaneSpell.Name = "txt_DepartmentJaneSpell";
            this.txt_DepartmentJaneSpell.Size = new System.Drawing.Size(159, 23);
            this.txt_DepartmentJaneSpell.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(325, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 38;
            this.label7.Text = "*";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 14);
            this.label2.TabIndex = 37;
            this.label2.Text = "部门编码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DepartmentCode
            // 
            this.txt_DepartmentCode.Location = new System.Drawing.Point(159, 45);
            this.txt_DepartmentCode.Name = "txt_DepartmentCode";
            this.txt_DepartmentCode.Size = new System.Drawing.Size(159, 23);
            this.txt_DepartmentCode.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(325, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 14);
            this.label6.TabIndex = 35;
            this.label6.Text = "*";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 14);
            this.label1.TabIndex = 34;
            this.label1.Text = "部门名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DepartmentName
            // 
            this.txt_DepartmentName.Location = new System.Drawing.Point(159, 10);
            this.txt_DepartmentName.Name = "txt_DepartmentName";
            this.txt_DepartmentName.Size = new System.Drawing.Size(159, 23);
            this.txt_DepartmentName.TabIndex = 33;
            // 
            // btnAddChild
            // 
            this.btnAddChild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddChild.Image = ((System.Drawing.Image)(resources.GetObject("btnAddChild.Image")));
            this.btnAddChild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddChild.Location = new System.Drawing.Point(1190, 27);
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(111, 30);
            this.btnAddChild.TabIndex = 4;
            this.btnAddChild.Text = "新增子节点";
            this.btnAddChild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddChild.UseVisualStyleBackColor = true;
            this.btnAddChild.Click += new System.EventHandler(this.BtnAddChild_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1508, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(1414, 27);
            this.btnDel.Name = "btnDel";
            this.btnDel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDel.Size = new System.Drawing.Size(76, 30);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "删除";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(1319, 27);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEdit.Size = new System.Drawing.Size(76, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(1060, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(111, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新增父节点";
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
            this.groupBox1.Size = new System.Drawing.Size(1612, 628);
            this.groupBox1.TabIndex = 2;
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
            treeListColumn4.AutoSizeMinSize = 0;
            treeListColumn4.AutoSizeRatio = 0F;
            treeListColumn4.Width = 0;
            this.treeListView1.Columns.AddRange(new CommonTools.TreeListColumn[] {
            treeListColumn1,
            treeListColumn2,
            treeListColumn3,
            treeListColumn4});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeListView1.Images = null;
            this.treeListView1.Location = new System.Drawing.Point(3, 19);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.RowOptions.ItemHeight = 23;
            this.treeListView1.Size = new System.Drawing.Size(1606, 606);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeListView1_MouseClick);
            // 
            // FM_TB_Department
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1612, 708);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_TB_Department";
            this.Text = "部门信息管理";
            this.Load += new System.EventHandler(this.FM_TB_Department_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddChild;
        private CommonTools.TreeListView treeListView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_DepartmentName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DepartmentCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_DepartmentJaneSpell;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_ParentDepartmentID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_DepartmentRemark;
        private System.Windows.Forms.Label hint;
    }
}