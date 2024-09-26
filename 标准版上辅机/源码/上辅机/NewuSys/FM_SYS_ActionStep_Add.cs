using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_ActionStep_Add : Form
    {
        private SYS_ActionStep actionStepModel;
        private string actionStepID = "";

        private readonly SYS_ActionStepRepository actionStepRepository = new SYS_ActionStepRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();

        /// <summary>
        /// 判断是新增操作还是编辑操作
        /// </summary>
        private bool IsAdd
        {
            get; set;
        }

        public FM_SYS_ActionStep_Add()
        {
            InitializeComponent();

            IsAdd = true;
        }

        public FM_SYS_ActionStep_Add(string id)
        {
            InitializeComponent();
            actionStepID = id;
            if (string.IsNullOrEmpty(id))
            {
                IsAdd = true;
            }
            else
            {
                IsAdd = false;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool Verification()
        {
            string msg = "";

            if (actionStepModel.StepNameCN == "")
            {       //"步骤名称不能为空！"
                msg = NewuGlobal.LanguagResourceManager.GetString("000720") + " " +
                NewuGlobal.LanguagResourceManager.GetString("000162");
            }
            else if (actionStepModel.StepNameEN == "")
            {
                msg = NewuGlobal.LanguagResourceManager.GetString("000720") + "EN  " + NewuGlobal.LanguagResourceManager.GetString("000162");
            }//"步骤名称不能为空！"
            else if (actionStepModel.StepValue < 0 || actionStepModel.StepValue > 20 || actionStepModel.StepValue == 0)
            {
                msg = NewuGlobal.LanguagResourceManager.GetString("000244") + " " + NewuGlobal.LanguagResourceManager.GetString("000191");
            }//"动作步骤值不能为空且为数字！"
            if (msg != "")
            {
                MessageBox.Show(msg, NewuGlobal.LanguagResourceManager.GetString("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (actionStepModel == null)
            {
                actionStepModel = new SYS_ActionStep();
            };

            actionStepModel.StepValue = FunClass.VVal(txt_StepValue.Text.Trim());
            actionStepModel.StepNameCN = txt_StepNameCN.Text.Trim();
            actionStepModel.StepNameEN = txt_StepNameEN.Text.Trim();
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                actionStepModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            }
            else
            {
                actionStepModel.DeviceID = "";
            }
            if (cmb_DevicePartID.SelectedIndex >= 0)
            {
                actionStepModel.DevicePartID = cmb_DevicePartID.SelectedValue.ToString();
            }
            else
            {
                actionStepModel.DevicePartID = "";
            }
            if (cmb_Enabled.SelectedIndex >= 0)
            {
                actionStepModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
            }
            else
            {
                actionStepModel.Enable = -1;
            }
            actionStepModel.SaveTime = DateTime.Now;

            if (Verification() == false)
            {
                return;
            }

            bool isAccess;
            if (IsAdd == false)
            {
                isAccess = actionStepRepository.Update(actionStepModel);
            }
            else
            {
                isAccess = actionStepRepository.Add(actionStepModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"步骤信息保存成功！", "信息"
                if (string.IsNullOrEmpty(actionStepModel.StepCode))
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

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_ActionStep fm = obj as FM_SYS_ActionStep;
                fm.GetData();
            }
        }

        private void FM_SYS_ActionStep_Add_Load(object sender, EventArgs e)
        {
            List<SYS_Device> list = deviceRepository.GetList("");
            cmb_DeviceID.DataSource = list;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            List<SYS_DevicePart> listN = devicePartRepository.GetList("");
            cmb_DevicePartID.DataSource = listN;
            cmb_DeviceID.SelectedIndex = 0;

            cmb_DevicePartID.ValueMember = "DevicePartID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                cmb_DevicePartID.DisplayMember = "Reserve1";
            }
            else
            {
                cmb_DevicePartID.DisplayMember = "DevicePartName";
            }
            cmb_DevicePartID.SelectedIndex = 0;

            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DataSource = Newu.EnableList.GetList();
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Enabled.SelectedIndex = 0;
            if (IsAdd == false && string.IsNullOrEmpty(actionStepID) == false)
            {
                actionStepModel = actionStepRepository.GetModel(actionStepID);
                txt_StepNameCN.Text = actionStepModel.StepNameCN;
                txt_StepNameEN.Text = actionStepModel.StepNameEN;
                txt_StepValue.Text = actionStepModel.StepValue.ToString();
                cmb_DeviceID.SelectedValue = actionStepModel.DeviceID;
                cmb_DevicePartID.SelectedValue = actionStepModel.DevicePartID;
                cmb_Enabled.SelectedValue = actionStepModel.Enable;
            }

            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            this.Text = NewuGlobal.LanguagResourceManager.GetString("000633");
            this.btnSave.Text = NewuGlobal.LanguagResourceManager.GetString("000108");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            groupBox1.Text = NewuGlobal.GetRes("000722");  // 动作步骤信息
            label2.Text = NewuGlobal.GetRes("000720") + "："; //步骤名称
            label3.Text = NewuGlobal.GetRes("000720") + "EN："; //步骤英文
            label5.Text = NewuGlobal.GetRes("000244") + "："; //动作步骤值
            label7.Text = NewuGlobal.GetRes("000182") + "："; //所属设备
            label9.Text = NewuGlobal.GetRes("000362") + "："; //所属部件
            label12.Text = NewuGlobal.GetRes("000188") + ": ";  //是否可用

            label4.Text = NewuGlobal.GetRes("000162"); //*不能为空
            label6.Text = NewuGlobal.GetRes("000162");
            label1.Text = NewuGlobal.GetRes("000162");
            label11.Text = NewuGlobal.GetRes("000162");
            label10.Text = NewuGlobal.GetRes("000162");
            label8.Text = NewuGlobal.GetRes("000162");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void ClearControl()
        {
            txt_StepValue.Text = "";
            txt_StepNameCN.Text = "";
            txt_StepNameEN.Text = "";
            cmb_DeviceID.SelectedIndex = -1;
            cmb_DevicePartID.SelectedIndex = -1;
            cmb_Enabled.SelectedIndex = -1;
        }
    }
}