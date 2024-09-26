using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_TechParam_Add : Form
    {
        private SYS_TechParam techParamModel = new SYS_TechParam();
        private string techParamID = "";
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly SYS_TechParamRepository techParamRepository = new SYS_TechParamRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();

        public FM_SYS_TechParam_Add()
        {
            InitializeComponent();
        }

        public FM_SYS_TechParam_Add(string id)
        {
            InitializeComponent();
            techParamID = id;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (techParamModel == null)
                techParamModel = new SYS_TechParam();
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                techParamModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            }
            else
            {
                techParamModel.DeviceID = "";
            }
            if (cmb_DevicePartID.SelectedIndex >= 0)
            {
                techParamModel.DevicePartID = cmb_DevicePartID.SelectedValue.ToString();
            }
            else
            {
                techParamModel.DevicePartID = "";
            }
            techParamModel.TechParamNameCN = txt_TechParamNameCN.Text.Trim();
            techParamModel.TechParamNameEN = txt_TechParamNameEN.Text.Trim();
            if (txt_TechParamOrder.Text == "")
            {
                techParamModel.TechParamOrder = 0;
            }
            else
            {
                techParamModel.TechParamOrder = Convert.ToInt32(txt_TechParamOrder.Text);
            }
            techParamModel.Unit = txt_Unit.Text;
            if (txt_DecDigit.Text == "")
            {
                techParamModel.DecDigit = 0;
            }
            else
            {
                techParamModel.DecDigit = Convert.ToInt32(txt_DecDigit.Text);
            }

            techParamModel.SaveUserID = NewuGlobal.TB_UserInfo.UserID;

            if (cmb_Enabled.SelectedIndex >= 0)
            {
                techParamModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue);
            }
            else
            {
                techParamModel.Enable = -1;
            }

            techParamModel.SaveTime = DateTime.Now;
            if (DataVerification() == false)
                return;

            bool isAccess;
            if (string.IsNullOrEmpty(techParamModel.TechParamID))
            {
                isAccess = techParamRepository.Add(techParamModel);
            }
            else
            {
                isAccess = techParamRepository.UpdateModel(techParamModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(techParamModel.TechParamID))
                    ClearControl();
                RefreshGrid();
                this.Close();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_TechParam fm = obj as FM_SYS_TechParam;
                fm.GetData();
            }
        }

        private void ClearControl()
        {
            txt_TechParamNameCN.Text = "";
            txt_TechParamNameEN.Text = "";
            cmb_DeviceID.SelectedIndex = -1;
            cmb_DevicePartID.SelectedIndex = -1;
            txt_TechParamOrder.Text = "";
            txt_Unit.Text = "";
            txt_DecDigit.Text = "";
            cmb_Enabled.SelectedIndex = -1;
        }

        private void FM_SYS_TechParam_Add_Load(object sender, EventArgs e)
        {
            try
            {
                cmb_DeviceID.DisplayMember = "DeviceName";
                cmb_DeviceID.ValueMember = "DeviceID";
                cmb_DeviceID.DataSource = deviceRepository.GetList("");
                cmb_DeviceID.DropDownStyle = ComboBoxStyle.DropDownList;

                cmb_Enabled.DisplayMember = "names";
                cmb_Enabled.ValueMember = "values";
                cmb_Enabled.DataSource = Newu.EnableList.GetList();
                cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;

                if (techParamID != "")
                {
                    techParamModel = techParamRepository.GetModel(techParamID);

                    txt_TechParamNameCN.Text = techParamModel.TechParamNameCN;
                    txt_TechParamNameEN.Text = techParamModel.TechParamNameEN;
                    txt_TechParamOrder.Text = techParamModel.TechParamOrder.ToString();
                    txt_Unit.Text = techParamModel.Unit;
                    txt_DecDigit.Text = techParamModel.DecDigit.ToString();
                    cmb_DeviceID.SelectedValue = techParamModel.DeviceID;
                    cmb_DevicePartID.SelectedValue = techParamModel.DevicePartID;

                    cmb_Enabled.SelectedValue = techParamModel.Enable;
                    cmb_Enabled.SelectedValue = techParamModel.Enable;
                }
                SetLanguage();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_TechParam_Add").Error(ex.ToString());
            }
        }

        private void SetLanguage()
        {
            label1.Text = NewuGlobal.GetRes("000451") + "：";
            label2.Text = NewuGlobal.GetRes("000028") + "：";
            label3.Text = NewuGlobal.GetRes("000760") + "CN：";
            label4.Text = NewuGlobal.GetRes("000760") + "EN：";
            label12.Text = NewuGlobal.GetRes("000761") + "：";
            label15.Text = NewuGlobal.GetRes("000763") + "：";
            label11.Text = NewuGlobal.GetRes("000762") + "：";
            label9.Text = NewuGlobal.GetRes("000188") + "：";
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            label5.Text = label7.Text = label10.Text = label13.Text = NewuGlobal.GetRes("000162");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool DataVerification()
        {
            if (techParamModel.TechParamNameCN == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000760") + "CN" + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (techParamModel.TechParamOrder == 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000761") + "CN" + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (techParamModel.DecDigit < 0)
            {
                MessageBox.Show("小数位不能为负数！");
                return false;
            }
            if (techParamModel.SaveUserID == "")
            {
                MessageBox.Show("保存用户不能为空！");
                return false;
            }
            if (techParamModel.Enable == -1)
            {
                MessageBox.Show(NewuGlobal.GetRes("000188") + "CN" + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }

        private void Cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deviceId = cmb_DeviceID.SelectedText;
            cmb_DevicePartID.ValueMember = "DevicePartID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                cmb_DevicePartID.DisplayMember = "Reserve1";
            }
            else
            {
                cmb_DevicePartID.DisplayMember = "DevicePartName";
            }
            if (cmb_DeviceID.SelectedIndex >= 0)
                deviceId = cmb_DeviceID.SelectedValue.ToString();

            List<SYS_DevicePart> sYS_DeviceParts = devicePartRepository.GetDevicePartListByDeviceID(deviceId);
            cmb_DevicePartID.DataSource = sYS_DeviceParts;
        }
    }
}