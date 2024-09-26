using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_UserInfo_Add : Form
    {
        private readonly TB_UserInfoRepository userInfoRepository = new TB_UserInfoRepository();
        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();
        private readonly TB_DepartmentRepository departmentRepository = new TB_DepartmentRepository();

        private TB_UserInfo userInfoModel = new TB_UserInfo();
        private string UserID = "";

        /// <summary>
        /// 判断是新增操作还是编辑操作
        /// </summary>
        private bool isAdd
        {
            get; set;
        }

        public FM_TB_UserInfo_Add()
        {
            InitializeComponent();
            isAdd = true;
        }

        public FM_TB_UserInfo_Add(string _UserID)
        {
            InitializeComponent();
            UserID = _UserID;
            if (string.IsNullOrEmpty(_UserID))
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }
        }

        private void FM_TB_UserInfo_Add_Load(object sender, EventArgs e)
        {
            List<TB_Department> list = departmentRepository.GetList("");
            cmb_DepartmentID.DataSource = list;
            cmb_DepartmentID.ValueMember = "DepartmentID";
            cmb_DepartmentID.DisplayMember = "DepartmentName";
            cmb_DepartmentID.SelectedIndex = -1;

            List<TB_Role> listN = roleRepository.GetList("");
            cmb_RoleID.DataSource = listN;
            cmb_RoleID.ValueMember = "RoleID";
            cmb_RoleID.DisplayMember = "RoleName";
            cmb_RoleID.SelectedIndex = -1;

            if (UserID != "")
            {
                userInfoModel = userInfoRepository.GetModel(UserID);

                cmb_DepartmentID.SelectedValue = userInfoModel.DepartmentID;
                cmb_RoleID.SelectedValue = userInfoModel.RoleID;
                txt_UserCode.Text = userInfoModel.UserCode;
                txt_UserPassword.Text = userInfoModel.UserPassword;
                txt_RealName.Text = userInfoModel.RealName;
                txt_Phone.Text = userInfoModel.Phone;
                txt_Jobs.Text = userInfoModel.Jobs;
            }
            SetLanguage();
        }

        private void SetLanguage()
        {
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnSave.Text = NewuGlobal.GetRes("000108");

            label10.Text = NewuGlobal.GetRes("000083") + "：";
            label7.Text = NewuGlobal.GetRes("000084") + "：";
            label1.Text = NewuGlobal.GetRes("000078") + "：";
            label2.Text = NewuGlobal.GetRes("000085") + "：";
            label3.Text = NewuGlobal.GetRes("000079") + "：";
            label12.Text = NewuGlobal.GetRes("000086") + "：";
            label11.Text = NewuGlobal.GetRes("000087") + "：";

            label4.Text = NewuGlobal.GetRes("000162");
            label5.Text = NewuGlobal.GetRes("000162");
            label6.Text = NewuGlobal.GetRes("000162");
            label8.Text = NewuGlobal.GetRes("000162");
            label9.Text = NewuGlobal.GetRes("000162");
            Text = NewuGlobal.GetRes("000088");
            groupBox1.Text = NewuGlobal.GetRes("000082");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (userInfoModel == null)
            {
                userInfoModel = new TB_UserInfo();
            }
            userInfoModel.UserCode = txt_UserCode.Text.Trim();
            userInfoModel.UserPassword = txt_UserPassword.Text.Trim();
            userInfoModel.RealName = txt_RealName.Text.Trim();
            userInfoModel.Phone = txt_Phone.Text.Trim();
            userInfoModel.Jobs = txt_Jobs.Text.Trim();
            if (cmb_DepartmentID.SelectedIndex >= 0)
            {
                userInfoModel.DepartmentID = cmb_DepartmentID.SelectedValue.ToString();
            }
            else
            {
                userInfoModel.DepartmentID = "";
            }
            if (cmb_RoleID.SelectedIndex >= 0)
            {
                userInfoModel.RoleID = cmb_RoleID.SelectedValue.ToString();
            }
            else
            {
                userInfoModel.RoleID = "";
            }
            userInfoModel.SaveUserID = "";
            userInfoModel.SaveTime = DateTime.Now;
            userInfoModel.DeleteMark = "";

            if (DataVerification() == false)
            {
                return;
            }

            bool isAccess;
            if (isAdd)
            {
                isAccess = userInfoRepository.Add(userInfoModel);
            }
            else
            {
                isAccess = userInfoRepository.Update(userInfoModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(userInfoModel.UserID))
                {
                    ClearControl();
                }
                RefreshGrid();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearControl()
        {
            txt_UserCode.Text = "";
            txt_UserPassword.Text = "";
            txt_RealName.Text = "";
            txt_Phone.Text = "";
            txt_Jobs.Text = "";
            cmb_RoleID.SelectedIndex = -1;
            cmb_DepartmentID.SelectedIndex = -1;
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_TB_UserInfo fm = obj as FM_TB_UserInfo;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            if (userInfoModel.DepartmentID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000083") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (userInfoModel.RoleID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000084") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (userInfoModel.UserCode == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000078") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (userInfoModel.UserPassword == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000086") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (userInfoModel.RealName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000079") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }
    }
}