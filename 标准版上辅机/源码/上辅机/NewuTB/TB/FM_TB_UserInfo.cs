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
    public partial class FM_TB_UserInfo : Form, ILanguageChanged
    {
        private readonly TB_UserInfoRepository userInfoRepository = new TB_UserInfoRepository();
        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();
        private readonly TB_DepartmentRepository departmentRepository = new TB_DepartmentRepository();
        private List<TB_UserInfo> tB_UserInfos;

        public FM_TB_UserInfo()
        {
            InitializeComponent();
        }

        private void FM_TB_UserInfo_Load(object sender, EventArgs e)
        {
            List<TB_Department> list = departmentRepository.GetList("");
            cmb_DepartmentID.DataSource = list;
            cmb_DepartmentID.ValueMember = "DepartmentID";
            cmb_DepartmentID.DisplayMember = "DepartmentName";
            cmb_DepartmentID.SelectedIndex = -1;

            List<TB_Department> departments = new List<TB_Department>();
            departments.AddRange(list);
            cmbDepartmentID.DataSource = departments;
            cmbDepartmentID.ValueMember = "DepartmentID";
            cmbDepartmentID.DisplayMember = "DepartmentName";
            cmbDepartmentID.SelectedIndex = -1;

            List<TB_Role> listN = roleRepository.GetList("");
            cmbRoleID.DataSource = listN;
            cmbRoleID.ValueMember = "RoleID";
            cmbRoleID.DisplayMember = "RoleName";
            cmbRoleID.SelectedIndex = -1;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("UserID","用户", ColumnType.txt,false),
                new ColStruct("DepartmentID","部门", ColumnType.cmb,true),
                new ColStruct("RoleID","角色",ColumnType.cmb,true),
                new ColStruct("UserCode","用户编码"),
                new ColStruct("RealName","真实姓名"),
                new ColStruct("Jobs","岗位"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewComboBoxColumn dgvColDepartment = dgv.Columns["DepartmentID"] as DataGridViewComboBoxColumn;
            dgvColDepartment.DisplayMember = "DepartmentName";
            dgvColDepartment.ValueMember = "DepartmentID";
            dgvColDepartment.DataSource = departmentRepository.GetList("");

            DataGridViewComboBoxColumn dgvColRole = (DataGridViewComboBoxColumn)dgv.Columns["RoleID"];
            dgvColRole.DataSource = roleRepository.GetList("");
            dgvColRole.ValueMember = "RoleID";
            dgvColRole.DisplayMember = "RoleName";

            GetData();
            SetLanguage();
        }

        public void GetData()
        {
            try
            {
                string where = "";
                if (cmb_DepartmentID.SelectedIndex >= 0)
                {
                    string cmb_Depar = cmb_DepartmentID.SelectedValue.ToString();
                    where = " DepartmentID = '" + cmb_Depar + "'";
                }
                tB_UserInfos = userInfoRepository.GetList(0, where, "DepartmentID");
                if (!NewuGlobal.TB_UserInfo.RealName.Equals("newu"))
                {
                    TB_UserInfo tB_UserInfo = tB_UserInfos.Find(u => u.UserCode.Equals("admin"));
                    tB_UserInfos.Remove(tB_UserInfo);
                }
                dgv.DataSource = tB_UserInfos;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_UserInfo").Error(ex.ToString());
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            TB_UserInfo tB_UserInfo = new TB_UserInfo();
            ExcuteData(tB_UserInfo, true);
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
                TB_UserInfo tB_UserInfo = userInfoRepository.GetModel(id);
                ExcuteData(tB_UserInfo, false);
            }
        }

        private void ExcuteData(TB_UserInfo userInfoModel, bool flag)
        {
            try
            {
                userInfoModel.UserCode = txt_UserCode.Text.Trim();
                userInfoModel.UserPassword = txt_UserPassword.Text.Trim();
                userInfoModel.RealName = txt_RealName.Text.Trim();
                userInfoModel.Phone = txt_Phone.Text.Trim();
                userInfoModel.Jobs = txt_Jobs.Text.Trim();
                if (cmbDepartmentID.SelectedIndex >= 0)
                    userInfoModel.DepartmentID = cmbDepartmentID.SelectedValue.ToString();
                else
                    userInfoModel.DepartmentID = "";

                if (cmbRoleID.SelectedIndex >= 0)
                    userInfoModel.RoleID = cmbRoleID.SelectedValue.ToString();
                else
                    userInfoModel.RoleID = "";

                userInfoModel.SaveUserID = "";
                userInfoModel.SaveTime = DateTime.Now;
                userInfoModel.DeleteMark = "";

                if (!DataVerification(userInfoModel))
                    return;

                bool result;
                if (flag)
                {
                    result = userInfoRepository.Add(userInfoModel);
                }
                else
                {
                    result = userInfoRepository.Update(userInfoModel);
                }

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
                NewuGlobal.LogCat("").Error(ex.ToString());
            }
        }

        private bool DataVerification(TB_UserInfo userInfoModel)
        {
            if (userInfoModel.DepartmentID == "")
            {
                hint.Text = NewuGlobal.GetRes("000083") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (userInfoModel.RoleID == "")
            {
                hint.Text = NewuGlobal.GetRes("000084") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (userInfoModel.UserCode == "")
            {
                hint.Text = NewuGlobal.GetRes("000078") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (userInfoModel.UserPassword == "")
            {
                hint.Text = NewuGlobal.GetRes("000086") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (userInfoModel.RealName == "")
            {
                hint.Text = NewuGlobal.GetRes("000079") + NewuGlobal.GetRes("000162");
                return false;
            }
            return true;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                {
                    return;
                }
                int rowIndex = dgv.CurrentCell.RowIndex;
                if (rowIndex >= 0)
                {
                    string id = dgv[0, rowIndex].Value.ToString();
                    string ControlName = dgv[4, rowIndex].Value.ToString();
                    DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170") + "    " + ControlName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (isDel == DialogResult.Yes)
                    {
                        bool result = userInfoRepository.Delete(id);
                        if (result)
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
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_UserInfo").Error(ex.ToString());
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DepartmentID.SelectedIndex = -1;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            txtDepartment.Text = label10.Text = NewuGlobal.GetRes("000083") + ":";
            label7.Text = NewuGlobal.GetRes("000084") + ":";
            label1.Text = NewuGlobal.GetRes("000078") + ":";
            label2.Text = NewuGlobal.GetRes("000085") + ":";
            label3.Text = NewuGlobal.GetRes("000079") + ":";
            label11.Text = NewuGlobal.GetRes("000086") + ":";
            label12.Text = NewuGlobal.GetRes("000087") + ":";

            hint.Text = NewuGlobal.GetRes("000170");
            btnAdd.Text = NewuGlobal.GetRes("000100"); //新增
            btnEdit.Text = NewuGlobal.GetRes("000101"); //编辑
            btnDel.Text = NewuGlobal.GetRes("000102"); //删除
            btnClose.Text = NewuGlobal.GetRes("000103"); //关闭

            btnReset.Text = NewuGlobal.GetRes("000105");//重置
            btnQuery.Text = NewuGlobal.GetRes("000104");//查询
            txtDepartment.Text = NewuGlobal.GetRes("000037");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnReset.Padding = btnQuery.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnReset.Padding = btnQuery.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
            }
            if (dgv != null && dgv.Columns != null)
                for (int i = 1; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.GetRes((76 + i - 1).ToString("000000"));
                }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");
            cmbDepartmentID.SelectedValue = dgv.CurrentRow.Cells["DepartmentID"].Value.ToString();
            cmbRoleID.SelectedValue = dgv.CurrentRow.Cells["RoleID"].Value.ToString();
            txt_UserCode.Text = dgv.CurrentRow.Cells["UserCode"].Value.ToString();
            string userID = dgv.CurrentRow.Cells["UserID"].Value.ToString();
            TB_UserInfo tB_UserInfo = tB_UserInfos.Find(u => u.UserID.Equals(userID));
            txt_UserPassword.Text = tB_UserInfo.UserPassword;
            txt_RealName.Text = dgv.CurrentRow.Cells["RealName"].Value.ToString();
            txt_Phone.Text = tB_UserInfo.Phone;
            txt_Jobs.Text = tB_UserInfo.Jobs;
        }
    }
}