namespace NewuSys
{
    partial class FM_SYS_Menu_Add
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_SYS_Menu_Add));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbAutoRunShow = new System.Windows.Forms.ComboBox();
            this.cmb_ParentMenuID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_ControlType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Caption = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_Role = new System.Windows.Forms.ComboBox();
            this.cmb_IsShow = new System.Windows.Forms.ComboBox();
            this.lb_Role = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbResourceId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDelMenuLogo = new System.Windows.Forms.Button();
            this.picMenuLogo = new System.Windows.Forms.PictureBox();
            this.btnMenuLogo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_MenuOrder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_ShowDialog = new System.Windows.Forms.ComboBox();
            this.txt_ToolTip = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_ControlText = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_ControlName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_ContainerForm = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_NameSpaceAndClass = new System.Windows.Forms.TextBox();
            this.txt_ASSEMBLY = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenuLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(14, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnSave.Size = new System.Drawing.Size(76, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(108, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(820, 547);
            this.splitContainer1.SplitterDistance = 41;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbAutoRunShow);
            this.groupBox3.Controls.Add(this.cmb_ParentMenuID);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmb_ControlType);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txt_Caption);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(16, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(791, 112);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "菜单主要数据";
            // 
            // cbAutoRunShow
            // 
            this.cbAutoRunShow.FormattingEnabled = true;
            this.cbAutoRunShow.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cbAutoRunShow.Location = new System.Drawing.Point(623, 74);
            this.cbAutoRunShow.Name = "cbAutoRunShow";
            this.cbAutoRunShow.Size = new System.Drawing.Size(129, 22);
            this.cbAutoRunShow.TabIndex = 89;
            // 
            // cmb_ParentMenuID
            // 
            this.cmb_ParentMenuID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ParentMenuID.Enabled = false;
            this.cmb_ParentMenuID.FormattingEnabled = true;
            this.cmb_ParentMenuID.Location = new System.Drawing.Point(146, 38);
            this.cmb_ParentMenuID.Name = "cmb_ParentMenuID";
            this.cmb_ParentMenuID.Size = new System.Drawing.Size(174, 22);
            this.cmb_ParentMenuID.TabIndex = 84;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 14);
            this.label3.TabIndex = 83;
            this.label3.Text = "父菜单名称:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(473, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 14);
            this.label7.TabIndex = 88;
            this.label7.Text = "自动运行:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_ControlType
            // 
            this.cmb_ControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ControlType.FormattingEnabled = true;
            this.cmb_ControlType.Location = new System.Drawing.Point(623, 39);
            this.cmb_ControlType.Name = "cmb_ControlType";
            this.cmb_ControlType.Size = new System.Drawing.Size(129, 22);
            this.cmb_ControlType.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(329, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 18);
            this.label5.TabIndex = 70;
            this.label5.Text = "*不能为空";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_Caption
            // 
            this.txt_Caption.Location = new System.Drawing.Point(146, 73);
            this.txt_Caption.Name = "txt_Caption";
            this.txt_Caption.Size = new System.Drawing.Size(174, 23);
            this.txt_Caption.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(470, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 14);
            this.label2.TabIndex = 68;
            this.label2.Text = "控件类型:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 14);
            this.label1.TabIndex = 67;
            this.label1.Text = "标题名称:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_Role);
            this.groupBox2.Controls.Add(this.cmb_IsShow);
            this.groupBox2.Controls.Add(this.lb_Role);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbResourceId);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnDelMenuLogo);
            this.groupBox2.Controls.Add(this.picMenuLogo);
            this.groupBox2.Controls.Add(this.btnMenuLogo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_MenuOrder);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmb_ShowDialog);
            this.groupBox2.Controls.Add(this.txt_ToolTip);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txt_ControlText);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txt_ControlName);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txt_ContainerForm);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txt_NameSpaceAndClass);
            this.groupBox2.Controls.Add(this.txt_ASSEMBLY);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(16, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(791, 351);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单详细数据";
            // 
            // cb_Role
            // 
            this.cb_Role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Role.FormattingEnabled = true;
            this.cb_Role.Location = new System.Drawing.Point(146, 323);
            this.cb_Role.Name = "cb_Role";
            this.cb_Role.Size = new System.Drawing.Size(97, 22);
            this.cb_Role.TabIndex = 92;
            // 
            // cmb_IsShow
            // 
            this.cmb_IsShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_IsShow.FormattingEnabled = true;
            this.cmb_IsShow.Location = new System.Drawing.Point(547, 264);
            this.cmb_IsShow.Name = "cmb_IsShow";
            this.cmb_IsShow.Size = new System.Drawing.Size(61, 22);
            this.cmb_IsShow.TabIndex = 90;
            // 
            // lb_Role
            // 
            this.lb_Role.Location = new System.Drawing.Point(24, 326);
            this.lb_Role.Name = "lb_Role";
            this.lb_Role.Size = new System.Drawing.Size(116, 14);
            this.lb_Role.TabIndex = 93;
            this.lb_Role.Text = "角色:";
            this.lb_Role.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(357, 267);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(186, 14);
            this.label9.TabIndex = 91;
            this.label9.Text = "是否显示:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbResourceId
            // 
            this.tbResourceId.Location = new System.Drawing.Point(547, 227);
            this.tbResourceId.Name = "tbResourceId";
            this.tbResourceId.Size = new System.Drawing.Size(205, 23);
            this.tbResourceId.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(404, 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 14);
            this.label8.TabIndex = 89;
            this.label8.Text = "资源ID:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDelMenuLogo
            // 
            this.btnDelMenuLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelMenuLogo.Location = new System.Drawing.Point(146, 278);
            this.btnDelMenuLogo.Name = "btnDelMenuLogo";
            this.btnDelMenuLogo.Size = new System.Drawing.Size(97, 27);
            this.btnDelMenuLogo.TabIndex = 87;
            this.btnDelMenuLogo.Text = "删除图片";
            this.btnDelMenuLogo.UseVisualStyleBackColor = true;
            this.btnDelMenuLogo.Click += new System.EventHandler(this.BtnDelMenuLogo_Click);
            // 
            // picMenuLogo
            // 
            this.picMenuLogo.Location = new System.Drawing.Point(259, 230);
            this.picMenuLogo.Name = "picMenuLogo";
            this.picMenuLogo.Size = new System.Drawing.Size(92, 75);
            this.picMenuLogo.TabIndex = 86;
            this.picMenuLogo.TabStop = false;
            // 
            // btnMenuLogo
            // 
            this.btnMenuLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuLogo.Location = new System.Drawing.Point(146, 230);
            this.btnMenuLogo.Name = "btnMenuLogo";
            this.btnMenuLogo.Size = new System.Drawing.Size(97, 27);
            this.btnMenuLogo.TabIndex = 85;
            this.btnMenuLogo.Text = "选择图片";
            this.btnMenuLogo.UseVisualStyleBackColor = true;
            this.btnMenuLogo.Click += new System.EventHandler(this.BtnMenuLogo_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(21, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 14);
            this.label6.TabIndex = 84;
            this.label6.Text = "菜单Logo:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_MenuOrder
            // 
            this.txt_MenuOrder.Location = new System.Drawing.Point(547, 62);
            this.txt_MenuOrder.Name = "txt_MenuOrder";
            this.txt_MenuOrder.Size = new System.Drawing.Size(205, 23);
            this.txt_MenuOrder.TabIndex = 83;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(395, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 14);
            this.label4.TabIndex = 82;
            this.label4.Text = "显示排序:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_ShowDialog
            // 
            this.cmb_ShowDialog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ShowDialog.FormattingEnabled = true;
            this.cmb_ShowDialog.Location = new System.Drawing.Point(547, 28);
            this.cmb_ShowDialog.Name = "cmb_ShowDialog";
            this.cmb_ShowDialog.Size = new System.Drawing.Size(205, 22);
            this.cmb_ShowDialog.TabIndex = 81;
            // 
            // txt_ToolTip
            // 
            this.txt_ToolTip.Location = new System.Drawing.Point(547, 191);
            this.txt_ToolTip.Name = "txt_ToolTip";
            this.txt_ToolTip.Size = new System.Drawing.Size(205, 23);
            this.txt_ToolTip.TabIndex = 80;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(401, 194);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(139, 14);
            this.label21.TabIndex = 79;
            this.label21.Text = "工具提示:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_ControlText
            // 
            this.txt_ControlText.Location = new System.Drawing.Point(146, 133);
            this.txt_ControlText.Multiline = true;
            this.txt_ControlText.Name = "txt_ControlText";
            this.txt_ControlText.Size = new System.Drawing.Size(606, 45);
            this.txt_ControlText.TabIndex = 78;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(15, 136);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 14);
            this.label19.TabIndex = 77;
            this.label19.Text = "传递参数:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_ControlName
            // 
            this.txt_ControlName.Location = new System.Drawing.Point(146, 28);
            this.txt_ControlName.Name = "txt_ControlName";
            this.txt_ControlName.Size = new System.Drawing.Size(205, 23);
            this.txt_ControlName.TabIndex = 76;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(134, 14);
            this.label17.TabIndex = 75;
            this.label17.Text = "控件名称:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_ContainerForm
            // 
            this.txt_ContainerForm.Location = new System.Drawing.Point(146, 191);
            this.txt_ContainerForm.Name = "txt_ContainerForm";
            this.txt_ContainerForm.Size = new System.Drawing.Size(205, 23);
            this.txt_ContainerForm.TabIndex = 74;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(18, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(122, 14);
            this.label14.TabIndex = 72;
            this.label14.Text = "容器窗体:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(398, 31);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(142, 14);
            this.label15.TabIndex = 71;
            this.label15.Text = "显示模式:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NameSpaceAndClass
            // 
            this.txt_NameSpaceAndClass.Location = new System.Drawing.Point(146, 98);
            this.txt_NameSpaceAndClass.Name = "txt_NameSpaceAndClass";
            this.txt_NameSpaceAndClass.Size = new System.Drawing.Size(606, 23);
            this.txt_NameSpaceAndClass.TabIndex = 70;
            // 
            // txt_ASSEMBLY
            // 
            this.txt_ASSEMBLY.Location = new System.Drawing.Point(146, 62);
            this.txt_ASSEMBLY.Name = "txt_ASSEMBLY";
            this.txt_ASSEMBLY.Size = new System.Drawing.Size(205, 23);
            this.txt_ASSEMBLY.TabIndex = 69;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 14);
            this.label10.TabIndex = 68;
            this.label10.Text = "命名空间:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(9, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 14);
            this.label11.TabIndex = 67;
            this.label11.Text = "  程序集:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            // 
            // FM_SYS_Menu_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 547);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FM_SYS_Menu_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "菜单信息录入";
            this.Load += new System.EventHandler(this.FM_SYS_Menu_Add_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenuLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_ControlType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Caption;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_ToolTip;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_ControlText;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_ControlName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_ContainerForm;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_NameSpaceAndClass;
        private System.Windows.Forms.TextBox txt_ASSEMBLY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_ShowDialog;
        private System.Windows.Forms.ComboBox cmb_ParentMenuID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_MenuOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMenuLogo;
        private System.Windows.Forms.PictureBox picMenuLogo;
        private System.Windows.Forms.Button btnDelMenuLogo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbAutoRunShow;
        private System.Windows.Forms.TextBox tbResourceId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_IsShow;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_Role;
        private System.Windows.Forms.Label lb_Role;
    }
}