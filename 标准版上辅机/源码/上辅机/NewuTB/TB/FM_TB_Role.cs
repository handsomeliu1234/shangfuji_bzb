using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_Role : Form, ILanguageChanged
    {
        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();
        private readonly TB_PrivilegeRepository privilegeRepository = new TB_PrivilegeRepository();

        public FM_TB_Role()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            List<TB_Role> tB_Roles = roleRepository.GetList("");
            if (!NewuGlobal.TB_UserInfo.RealName.Equals("admin"))
            {
                TB_Role tB_Role = tB_Roles.Find(r => r.RoleName.Equals("超级管理员"));
                tB_Roles.Remove(tB_Role);
            }
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("RoleID","角色ID", ColumnType.txt,false),
                new ColStruct("RoleName","角色名称"),
                new ColStruct("CreateTime","角色创建时间"),
                new ColStruct("RoleRemark","角色信息备注"),
                new ColStruct("SaveUserID","操作用户"),
                new ColStruct("SaveTime","最后保存时间")
            };
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;

            dgv.AddCols(cols);
            dgv.DataSource = tB_Roles;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetLanguage();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            TB_Role tB_Role = new TB_Role();
            ExcuteData(tB_Role, true);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, dgv.CurrentCell.RowIndex].Value.ToString();
                TB_Role tB_Role = roleRepository.GetModel(id);
                ExcuteData(tB_Role, false);
            }
        }

        private void ExcuteData(TB_Role roleModel, bool flag)
        {
            try
            {
                roleModel.RoleName = txt_RoleName.Text.Trim();
                roleModel.RoleRemark = txt_RoleRemark.Text.Trim();
                roleModel.SaveUserID = txt_SaveUserID.Text.Trim();
                roleModel.SaveTime = DateTime.Now;

                if (!DataVerification(roleModel))
                    return;

                bool result;
                if (flag)
                {
                    roleModel.CreateTime = DateTime.Now.ToString("G");
                    result = roleRepository.Add(roleModel);
                }
                else
                    result = roleRepository.Update(roleModel);

                if (result)
                {
                    hint.Text = NewuGlobal.GetRes("000171");
                    GetData();
                }
                else
                {
                    hint.Text = NewuGlobal.GetRes("000172");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_Role").Error(ex.ToString());
            }
        }

        private bool DataVerification(TB_Role roleModel)
        {
            if (roleModel.RoleName == "")
            {
                hint.Text = NewuGlobal.GetRes("000513") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (roleModel.CreateTime == "")
            {
                hint.Text = NewuGlobal.GetRes("000522") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (roleModel.SaveUserID == "")
            {
                hint.Text = NewuGlobal.GetRes("000524") + NewuGlobal.GetRes("000162");
                return false;
            }
            return true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FM_TB_Role_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            DelRole(this.dgv, 1, 0);
        }

        public bool DelRole(DataGridView grid, int roleNameColIndex, int roleIdColIndex)
        {
            bool isAccess = false;
            if (grid.Rows.Count == 0)
            {
                return false;
            }
            int rowIndex = grid.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = grid[roleIdColIndex, rowIndex].Value.ToString();
                bool IsCheckes = privilegeRepository.GetData("RoleID ='" + id + "'");

                DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    isAccess = roleRepository.Delete(id, IsCheckes);

                    if (isAccess)
                    {
                        hint.Text = NewuGlobal.GetRes("000173");
                        GetData();
                    }
                    else
                    {
                        hint.Text = NewuGlobal.GetRes("000174");
                    }
                }
            }
            return isAccess;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            label1.Text = NewuGlobal.GetRes("000521") + ":";
            label3.Text = NewuGlobal.GetRes("000523") + ":";
            label4.Text = NewuGlobal.GetRes("000524") + ":";
            btnAdd.Text = NewuGlobal.GetRes("000100");
            btnEdit.Text = NewuGlobal.GetRes("000101");
            btnDel.Text = NewuGlobal.GetRes("000102");
            btnClose.Text = NewuGlobal.GetRes("000103");
            hint.Text = NewuGlobal.GetRes("000170");
            groupBox1.Text = NewuGlobal.GetRes("000511");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
            }
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].HeaderText = NewuGlobal.GetRes("000" + (512 + i));
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");
            txt_RoleName.Text = dgv.CurrentRow.Cells["RoleName"].Value.ToString();
            txt_RoleRemark.Text = dgv.CurrentRow.Cells["RoleRemark"].Value.ToString();
            txt_SaveUserID.Text = dgv.CurrentRow.Cells["SaveUserID"].Value.ToString();
        }
    }
}