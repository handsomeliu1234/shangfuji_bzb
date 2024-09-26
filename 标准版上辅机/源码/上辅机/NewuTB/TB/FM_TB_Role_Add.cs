using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Windows.Forms;

namespace NewuTB.TB
{
    /// <summary>
    /// 权限分配窗体需要使用到
    /// </summary>
    public partial class FM_TB_Role_Add : Form
    {
        public delegate void DelegateOwnerGetData();

        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();
        private TB_Role roleModel;
        private string RoleID = "";

        private DelegateOwnerGetData OwnerGetDataMethod;

        public FM_TB_Role_Add()
        {
            InitializeComponent();
        }

        public FM_TB_Role_Add(DelegateOwnerGetData getdata)
        {
            InitializeComponent();
            OwnerGetDataMethod = new DelegateOwnerGetData(getdata);
        }

        public FM_TB_Role_Add(string _RoleID)
        {
            InitializeComponent();
            RoleID = _RoleID;
        }

        public FM_TB_Role_Add(string _RoleID, DelegateOwnerGetData getdata)
        {
            InitializeComponent();
            RoleID = _RoleID;

            OwnerGetDataMethod = new DelegateOwnerGetData(getdata);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (roleModel == null)
            {
                roleModel = new TB_Role();
            }
            roleModel.RoleName = txt_RoleName.Text.Trim();
            roleModel.CreateTime = dtp_CreateTime.Text.Trim();
            roleModel.RoleRemark = txt_RoleRemark.Text.Trim();
            roleModel.SaveUserID = txt_SaveUserID.Text.Trim();
            roleModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
            {
                return;
            }
            bool isAccess;
            if (string.IsNullOrEmpty(roleModel.RoleID))
            {
                isAccess = roleRepository.Add(roleModel);
            }
            else
            {
                isAccess = roleRepository.Update(roleModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(roleModel.RoleID))
                {
                    ClearControl();
                }
                RefreshGrid();

                OwnerGetDataMethod?.Invoke();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearControl()
        {
            txt_RoleName.Text = "";
            dtp_CreateTime.Text = "";
            txt_RoleRemark.Text = "";
            txt_SaveUserID.Text = "";
            txt_SaveUserID.Text = "";
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_TB_Role fm = obj as FM_TB_Role;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            if (roleModel.RoleName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000513") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (roleModel.CreateTime == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000522") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (roleModel.SaveUserID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000524") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }

        private void FM_TB_Role_Add_Load(object sender, EventArgs e)
        {
            SetLanguage();
            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;
            if (RoleID != "")
            {
                roleModel = roleRepository.GetModel(RoleID);

                txt_RoleName.Text = roleModel.RoleName;
                dtp_CreateTime.CustomFormat = roleModel.CreateTime;
                txt_RoleRemark.Text = roleModel.RoleRemark;
                txt_SaveUserID.Text = roleModel.SaveUserID;
            }
        }

        private void SetLanguage()
        {
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            label1.Text = NewuGlobal.GetRes("000521") + ":";
            label2.Text = NewuGlobal.GetRes("000522") + ":";
            label3.Text = NewuGlobal.GetRes("000523") + ":";
            label4.Text = NewuGlobal.GetRes("000524") + ":";
            label5.Text = NewuGlobal.GetRes("000162");
            label6.Text = NewuGlobal.GetRes("000162");
            label7.Text = NewuGlobal.GetRes("000162");
            groupBox1.Text = NewuGlobal.GetRes("000520");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }
    }
}