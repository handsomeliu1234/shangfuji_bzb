using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;

namespace NewuSoft
{
    public partial class FmLogin : Form
    {
        private readonly TB_UserInfoRepository userInfoRepository = new TB_UserInfoRepository();
        private readonly RPT_AppEventRepository appEventRepository = new RPT_AppEventRepository();
        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();

        public FmLogin()
        {
            InitializeComponent();
            SetLanguage();
            cb_workGroup.DataSource = NewuGlobal.SoftConfig.WorkGroupSet.Split('/');
            cb_workOrder.DataSource = NewuGlobal.SoftConfig.WorkOrderSet.Split('/');
            cb_workGroup.SelectedItem = NewuGlobal.SoftConfig.WorkGroup;
            cb_workOrder.SelectedItem = NewuGlobal.SoftConfig.WorkOrder;
        }

        private void SetLanguage()
        {
            try
            {
                label1.Text = NewuGlobal.GetRes("000089") + ":";
                label2.Text = NewuGlobal.GetRes("000090") + ":";
                label3.Text = NewuGlobal.GetRes("000317") + ":";
                label4.Text = NewuGlobal.GetRes("000318") + ":";
                btn_Login.Text = NewuGlobal.GetRes("000010");
                btnExit.Text = NewuGlobal.GetRes("000012");
                lblMsg.Text = "*" + NewuGlobal.GetRes("000170") + "*";
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FmLogin").Error(ex.ToString());
            }
        }

        private bool CheckText()
        {
            if (string.IsNullOrEmpty(cbName.Text.ToString()))//帐号是否为空
            {
                lblMsg.Text = "*" + NewuGlobal.GetRes("000089") + NewuGlobal.GetRes("000162") + "*";
                return false;
            }
            if (string.IsNullOrEmpty(txtPwd.Text))//密码是否为空
            {
                lblMsg.Text = "*" + NewuGlobal.GetRes("000090") + NewuGlobal.GetRes("000162") + "*";
                return false;
            }

            return true;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (CheckText())
            {
                string workGroup = cb_workGroup.SelectedItem.ToString();
                string workOrder = cb_workOrder.SelectedItem.ToString();
                //判断用户登录是否成功
                bool isSuccess = userInfoRepository.Query(cbName.Text, txtPwd.Text, out string msg, workGroup, workOrder);

                //使用logo字母作为超级管理员密码
                if (cbName.Text.Equals("admin") && txtPwd.Text.Equals("newuninewuni"))
                {
                    isSuccess = true;
                    TB_UserInfo tB_UserInfo = userInfoRepository.GetModelByUserCode(cbName.Text);
                    tB_UserInfo.RoleName = new TB_RoleRepository().GetModel(tB_UserInfo.RoleID).RoleName;
                    tB_UserInfo.WorkGroup = workGroup;
                    tB_UserInfo.WorkOrder = workOrder;
                    NewuGlobal.TB_UserInfo = tB_UserInfo;
                    NewuGlobal.SoftConfig.SetWorker(workGroup, workOrder);
                    msg = NewuGlobal.GetRes("000071");
                }

                if (isSuccess)
                {
                    lblMsg.Text = "*" + msg + "*";
                    appEventRepository.Add(AppEventType.UserLogin);//记录登录日志
                    NewuGlobal.TB_UserInfo.RealName = cbName.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    this.Dispose();
                }
                else
                {
                    lblMsg.Text = "*" + msg + "*";
                    this.DialogResult = DialogResult.None;
                }
            }
            else
                this.DialogResult = DialogResult.None;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                if (!NewuGlobal.FmMain.Visible)
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FmLogin").Error(ex.ToString());
            }
        }

        private void FmLogin_Load(object sender, EventArgs e)
        {
            cbName.KeyPress += new KeyPressEventHandler(TxtName_KeyPress);
            txtPwd.KeyPress += new KeyPressEventHandler(TxtName_KeyPress);
            cbName.Focus();
            InitView();
        }

        private void InitView()
        {
            List<TB_UserInfo> tB_UserInfos = userInfoRepository.GetList("");
            TB_UserInfo tB_UserInfo = tB_UserInfos.Find(t => t.UserCode.Equals("admin"));
            tB_UserInfos.Remove(tB_UserInfo);
            string aim = cbName.Text;
            int cnt = 0;
            int aimCnt = 1;

            foreach (var row in tB_UserInfos)
            {
                cbName.Items.Add(row.UserCode);
                if (aim == row.UserCode)
                {
                    aimCnt = cnt;
                }
                cnt++;
            }
            cbName.SelectedIndex = aimCnt;

            if (Debugger.IsAttached)
            {
                cbName.Text = "admin";
                txtPwd.Text = "newuninewuni";
            }
        }

        private void TxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = '\r';
            if (e.KeyChar == d)
            {
                BtnLogin_Click(null, null);
            }
        }
    }
}