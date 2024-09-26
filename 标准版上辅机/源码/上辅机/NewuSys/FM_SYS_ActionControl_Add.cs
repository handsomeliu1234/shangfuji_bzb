using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_ActionControl_Add : Form
    {
        private SYS_ActionControl actionControlModel = null;
        private string actionControlCode = "";
        private readonly SYS_ActionControlRepository actionControlRepository = new SYS_ActionControlRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();

        /// <summary>
        /// 判断是新增操作还是编辑操作
        /// </summary>
        private bool IsAdd
        {
            get; set;
        }

        public FM_SYS_ActionControl_Add()
        {
            InitializeComponent();
            IsAdd = true;
        }

        public FM_SYS_ActionControl_Add(string _ActionControlCode)
        {
            InitializeComponent();
            actionControlCode = _ActionControlCode;
            if (string.IsNullOrEmpty(_ActionControlCode))
            {
                IsAdd = true;
            }
            else
            {
                IsAdd = false;
            }
        }

        private void FM_SYS_ActionControl_Add_Load(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Panel1.BackColor = NewuColor.PanelBg;
                cmb_DeviceID.DataSource = deviceRepository.GetModelListAddNullRows("");
                cmb_DeviceID.ValueMember = "DeviceID";
                cmb_DeviceID.DisplayMember = "DeviceName";
                cmb_DeviceID.SelectedIndex = 0;
                txt_UserID.Text = NewuGlobal.TB_UserInfo.UserCode;

                cmb_Enabled.DisplayMember = "names";
                cmb_Enabled.ValueMember = "values";
                cmb_Enabled.DataSource = Newu.EnableList.GetList();
                cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb_Enabled.SelectedIndex = 0;
                if (actionControlCode != "")
                {
                    actionControlModel = actionControlRepository.GetModel("ActionControlCode='" + actionControlCode + "'");

                    txt_ControlNameCN.Text = actionControlModel.ActionControlNameCN;
                    txt_ControlNameEN.Text = actionControlModel.ActionControlNameEN;
                    cmb_DeviceID.SelectedValue = actionControlModel.DeviceID;
                    txt_UserID.Text = actionControlModel.SaveUserID;
                    txt_ControlValue.Text = actionControlModel.ActionControlValue.ToString();
                    cmb_Enabled.SelectedValue = actionControlModel.Enable;
                }
                SetControlLanguageText();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_ActionControl_Add").Error(ex.ToString());
            }
        }

        private void SetControlLanguageText()
        {
            this.Text = NewuGlobal.LanguagResourceManager.GetString("000285") + NewuGlobal.LanguagResourceManager.GetString("000101");
            this.btnSave.Text = NewuGlobal.LanguagResourceManager.GetString("000108");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            groupBox1.Text = NewuGlobal.GetRes("000285") + NewuGlobal.GetRes("000578");  // 动作控制方式信息
            label2.Text = NewuGlobal.GetRes("000684") + "："; //控制方式
            label3.Text = NewuGlobal.GetRes("000684") + "EN："; //控制方式英文
            label1.Text = NewuGlobal.GetRes("000684") + "Value："; //控制方式值
            label4.Text = NewuGlobal.GetRes("000342") + "："; //所属设备
            label5.Text = NewuGlobal.GetRes("000393") + "："; //保存用户
            label11.Text = NewuGlobal.GetRes("000188") + "："; //是否启用

            label6.Text = NewuGlobal.GetRes("000162"); //*不能为空
            label7.Text = NewuGlobal.GetRes("000162");
            label8.Text = NewuGlobal.GetRes("000162");
            label9.Text = NewuGlobal.GetRes("000162");
            label10.Text = NewuGlobal.GetRes("000162");
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
            if (actionControlModel == null)
            {
                actionControlModel = new SYS_ActionControl();
            }
            int getVuale = actionControlRepository.GetMaxActionControlValue();
            if (IsAdd && getVuale == FunClass.VVal(txt_ControlValue.Text.Trim()))
            {
                MessageBox.Show("Value" + NewuGlobal.GetRes("000322"));
                return;
            }
            actionControlModel.ActionControlValue = FunClass.VVal(txt_ControlValue.Text.Trim());
            actionControlModel.ActionControlNameCN = txt_ControlNameCN.Text.Trim();
            actionControlModel.ActionControlNameEN = txt_ControlNameEN.Text.Trim();
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                actionControlModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            }
            else
            {
                actionControlModel.DeviceID = "-1";
            }
            actionControlModel.SaveUserID = txt_UserID.Text.Trim();
            actionControlModel.SaveTime = DateTime.Now;
            if (cmb_Enabled.SelectedIndex >= 0)
            {
                actionControlModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
            }
            else
            {
                actionControlModel.Enable = -1;
            }

            if (DataVerification() == false)
                return;

            bool isAccess;
            if (IsAdd)
            {
                isAccess = actionControlRepository.Add(actionControlModel);
            }
            else
            {
                isAccess = actionControlRepository.Update(actionControlModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(actionControlModel.ActionControlCode))
                {
                    ClearControl();
                }
                RefreshGrid();
                this.Close();
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
            txt_ControlValue.Text = "";
            txt_ControlNameCN.Text = "";
            txt_ControlNameEN.Text = "";
            cmb_DeviceID.SelectedIndex = -1;
            txt_UserID.Text = "";
            cmb_Enabled.SelectedIndex = -1;
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_ActionControl fm = obj as FM_SYS_ActionControl;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            //"控制方式中文不能为空！"
            if (actionControlModel.ActionControlNameCN == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000684") + " " + NewuGlobal.GetRes("000162"));
                return false;
            }
            //"控制方式英文不能为空！"
            if (actionControlModel.ActionControlNameEN == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000684") + "EN " + NewuGlobal.GetRes("000162"));
                return false;
            }
            //"控制方式值不能为空！"
            if (actionControlModel.ActionControlValue <= 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000167"));
                return false;
            }
            //"所属设备不能为空！"
            if (actionControlModel.DeviceID == "-1")
            {
                MessageBox.Show(NewuGlobal.GetRes("000342") + " " + NewuGlobal.GetRes("000162"));
                return false;
            }
            //"保存用户不能为空！"
            if (actionControlModel.SaveUserID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000393") + " " + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }
    }
}