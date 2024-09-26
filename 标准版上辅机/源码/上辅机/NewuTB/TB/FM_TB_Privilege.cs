using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_Privilege : Form, ILanguageChanged
    {
        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();
        private readonly TB_PrivilegeRepository privilegeRepository = new TB_PrivilegeRepository();
        private readonly SYS_MenuRepository menuRepository = new SYS_MenuRepository();
        private string roleID;

        public FM_TB_Privilege()
        {
            InitializeComponent();
        }

        private void FM_TB_Privilege_Load(object sender, EventArgs e)
        {
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("RoleID","角色ID", ColumnType.txt,false),
                new ColStruct("RoleName","角色名称"),
                new ColStruct("RoleRemark","角色说明"),
                new ColStruct("CreateTime","创建时间"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            treeView1.CheckBoxes = true;
            GetData();
            SetLanguage();
        }

        public void GetData()
        {
            try
            {
                List<TB_Role> tB_Roles = roleRepository.GetList("");
                TB_Role tB_Role = tB_Roles.Find(r => r.RoleName.Equals("超级管理员"));
                roleID = tB_Role.RoleID;
                if (!NewuGlobal.TB_UserInfo.RealName.Equals("admin"))
                {
                    tB_Roles.Remove(tB_Role);
                }
                dgv.DataSource = tB_Roles;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_Privilege").Error(ex.ToString());
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FM_TB_Role_Add fm = new FM_TB_Role_Add(GetData);
            fm.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            string id = StaticCom.GetDataGridViewID(dgv, 0);
            if (id != "")
            {
                FM_TB_Role_Add fm = new FM_TB_Role_Add(id, GetData);
                fm.ShowDialog();
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            FM_TB_Role fm = new FM_TB_Role();
            bool isAccess = fm.DelRole(this.dgv, 1, 0);
            if (isAccess == true)
            {
                GetData();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSavePrivilege_Click(object sender, EventArgs e)
        {
            string _roleID = StaticCom.GetDataGridViewID(dgv, 0);
            if (_roleID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000208"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ArrayList nodeNames = treeView1.GetNodeNameArr();

            List<TB_Privilege> list = new List<TB_Privilege>();
            foreach (object item in nodeNames)
            {
                TreeViewEx.NodeCheck _nodeCheck = (TreeViewEx.NodeCheck)item;
                list.Add(new TB_Privilege()
                {
                    Enable = Convert.ToInt32(_nodeCheck.IsCheck),
                    RoleID = _roleID,
                    SaveTime = DateTime.Now,
                    MenuID = _nodeCheck.NodeName
                });
            }

            privilegeRepository.Delete(_roleID);
            int effectInt = privilegeRepository.SaveList(list);

            if (effectInt > 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            treeView1.Nodes.Clear();
            string roleid = dgv.CurrentRow.Cells["RoleID"].Value.ToString();
            List<SYS_Menu> sYS_Menus;
            if (roleID.Equals(roleid))
            {
                sYS_Menus = menuRepository.GetList(0, "", "MenuOrder");
            }
            else
                sYS_Menus = menuRepository.GetList(0, "Reserve3 !='" + roleID + "'", "MenuOrder");

            if (sYS_Menus != null)
            {
                menuRepository.NodeTree = treeView1;
                menuRepository.LoaderNodes(null, "", sYS_Menus);
            }
            treeView1.ExpandAll();

            //滚动条显示在最顶部
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.TopNode = treeView1.Nodes[0];
            }

            treeView1.ClearCheck();
            string _roleID = StaticCom.GetDataGridViewID(dgv, 0);
            if (_roleID != "")
            {
                List<TB_Privilege> list = privilegeRepository.GetModelList("RoleID='" + _roleID + "'");
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        TreeNode node = treeView1.FindNode(item.MenuID);
                        if (node != null)
                        {
                            node.Checked = Convert.ToBoolean(item.Enable);
                        }
                    }
                }
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            btnSavePrivilege.Text = NewuGlobal.GetRes("000041");
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnAdd.Text = NewuGlobal.GetRes("000091");
            btnEdit.Text = NewuGlobal.GetRes("000092");
            btnDel.Text = NewuGlobal.GetRes("000093");
            groupBox1.Text = NewuGlobal.GetRes("000099");
            groupBox2.Text = NewuGlobal.GetRes("000511");
            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000521");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000523");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000522");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000517");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnSavePrivilege.Size = new Size(91, 30);
                btnClose.Padding = new Padding(0, 0, 7, 0);
                btnClose.Location = new Point(129, 17);

                btnEdit.Size = new Size(91, 30);
                btnDel.Size = new Size(91, 30);
                btnDel.Location = new Point(285, 17);
            }
            else
            {
                btnSavePrivilege.Size = new Size(155, 30);
                btnClose.Padding = new Padding(0, 0, 0, 0);
                btnClose.Location = new Point(188, 17);

                btnEdit.Size = new Size(90, 30);
                btnDel.Size = new Size(100, 30);
                btnDel.Location = new Point(286, 17);
            }
            GetTree();
        }

        public void GetTree()
        {
            Dgv_CellClick(null, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;

                return cp;
            }
        }
    }
}