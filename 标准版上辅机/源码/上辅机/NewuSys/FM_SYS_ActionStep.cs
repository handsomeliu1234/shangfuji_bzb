using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_ActionStep : Form, ILanguageChanged
    {
        private readonly SYS_ActionStepRepository actionStepRepository = new SYS_ActionStepRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private List<SYS_ActionStep> sYS_ActionSteps;

        public FM_SYS_ActionStep()
        {
            InitializeComponent();
        }

        private void FM_SYS_ActionStep_Load(object sender, EventArgs e)
        {
            List<SYS_Device> list = deviceRepository.GetList("");
            cmbDeviceID.DataSource = list;
            cmbDeviceID.ValueMember = "DeviceID";
            cmbDeviceID.DisplayMember = "DeviceName";
            cmbDeviceID.SelectedIndex = 0;

            List<SYS_Device> devices = new List<SYS_Device>();
            devices.AddRange(list);
            cmb_DeviceID.DataSource = devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DataSource = EnableList.GetList();
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Enabled.SelectedIndex = 0;

            List<SYS_DevicePart> listN = devicePartRepository.GetList("");
            cmb_DevicePartID.DataSource = listN;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("StepCode","步骤编码", ColumnType.txt,false),
                new ColStruct("StepNameCN","步骤名称"),
                new ColStruct("StepNameEN","步骤名称英文"),
                new ColStruct("StepValue","步骤值"),
                new ColStruct("StepBit","二进制对应位数为true"),
                new ColStruct("DeviceID","设备ID", ColumnType.cmb,true),
                new ColStruct("DevicePartID","设备部件ID", ColumnType.cmb,true),
                new ColStruct("SaveTime","保存时间"),
                new ColStruct("Enable","是否启用", ColumnType.chk,true)
            };
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AddCols(cols);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.GetComboBoxColumn("DeviceID").DataSource = deviceRepository.GetList("");
            dgv.GetComboBoxColumn("DeviceID").ValueMember = "DeviceID";
            dgv.GetComboBoxColumn("DeviceID").DisplayMember = "DeviceName";

            dgv.GetComboBoxColumn("DevicePartID").DataSource = devicePartRepository.GetList("");
            dgv.GetComboBoxColumn("DevicePartID").ValueMember = "DevicePartID";

            GetData();
            SetControlLanguageText();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SYS_ActionStep actionStepModel = new SYS_ActionStep();
                ExcuteData(actionStepModel, true);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_ActionStep").Error(ex.ToString());
            }
        }

        /// <summary>
        /// actionStepModel.StepCode 新增时为null满足判断条件避免重复步骤插入
        /// 编辑时actionStepModel.StepCode有相同值不满足 !a.StepCode.Equals(actionStepModel.StepCode) 条件返回空对象可进行编辑
        /// </summary>
        /// <param name="actionStepModel"></param>
        /// <returns></returns>
        private bool Verification(SYS_ActionStep actionStepModel)
        {
            string msg = "";
            SYS_ActionStep tempModel = sYS_ActionSteps.Find(a => (a.StepNameCN.Equals(actionStepModel.StepNameCN) || a.StepNameEN.Equals(actionStepModel.StepNameEN) || a.StepValue == actionStepModel.StepValue) && !a.StepCode.Equals(actionStepModel.StepCode) && actionStepModel.DeviceID.Equals(actionStepModel.DeviceID) && a.DevicePartID.Equals(actionStepModel.DevicePartID));

            if (string.IsNullOrEmpty(actionStepModel.StepNameCN))
            {
                //"步骤名称不能为空！"
                msg = NewuGlobal.LanguagResourceManager.GetString("000720") + " " + NewuGlobal.LanguagResourceManager.GetString("000162");
            }
            else if (string.IsNullOrEmpty(actionStepModel.StepNameEN))
            {
                //"步骤名称不能为空！"
                msg = NewuGlobal.LanguagResourceManager.GetString("000720") + "EN  " + NewuGlobal.LanguagResourceManager.GetString("000162");
            }
            else if (actionStepModel.StepValue < 0 || actionStepModel.StepValue > 20 || actionStepModel.StepValue < 0)
            {
                //"动作步骤值不能为空且为数字！"
                msg = NewuGlobal.LanguagResourceManager.GetString("000244") + " " + NewuGlobal.LanguagResourceManager.GetString("000191");
            }
            else if (tempModel != null)
            {
                msg = NewuGlobal.GetRes("000030") + NewuGlobal.GetRes("000322");
            }

            if (!string.IsNullOrEmpty(msg))
            {
                hint.Text = msg;
                return false;
            }
            else
                return true;
        }

        private void SetControlLanguageText()
        {
            this.Text = NewuGlobal.LanguagResourceManager.GetString("000633");

            label2.Text = NewuGlobal.GetRes("000720") + "CN:"; //步骤名称
            label3.Text = NewuGlobal.GetRes("000720") + "EN:"; //步骤英文
            label5.Text = NewuGlobal.GetRes("000244") + ":"; //动作步骤值
            label7.Text = NewuGlobal.GetRes("000182") + ":"; //所属设备
            label9.Text = NewuGlobal.GetRes("000362") + ":"; //所属部件
            label12.Text = NewuGlobal.GetRes("000188") + ": ";  //是否可用
            hint.Text = NewuGlobal.GetRes("000170");

            this.btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            this.btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            this.btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");

            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");

            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            groupBox1.Text = NewuGlobal.GetRes("000381");
            dgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000720") + "CN";
            dgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000720") + "EN";
            dgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000244");
            dgv.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000721");
            dgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000026");
            dgv.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000028");
            dgv.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000187");
            dgv.Columns[8].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000188");

            cmb_DevicePartID.ValueMember = "DevicePartID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                cmb_DevicePartID.DisplayMember = "Reserve1";
                dgv.GetComboBoxColumn("DevicePartID").DisplayMember = "Reserve1";
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                cmb_DevicePartID.DisplayMember = "DevicePartName";
                dgv.GetComboBoxColumn("DevicePartID").DisplayMember = "DevicePartName";
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string stepCode = dgv.CurrentRow.Cells["StepCode"].Value.ToString();
                SYS_ActionStep sYS_ActionStep = actionStepRepository.GetModel(stepCode);
                ExcuteData(sYS_ActionStep, false);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_ActionStep").Error(ex.ToString());
            }
        }

        public void ExcuteData(SYS_ActionStep sYS_ActionStep, bool flag)
        {
            sYS_ActionStep.StepNameCN = txt_StepNameCN.Text.Trim();
            sYS_ActionStep.StepNameEN = txt_StepNameEN.Text.Trim();
            sYS_ActionStep.StepValue = FunClass.VVal(txt_StepValue.Text.Trim());
            sYS_ActionStep.SaveTime = DateTime.Now;
            if (cmb_DeviceID.SelectedIndex >= 0)
                sYS_ActionStep.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            else
                sYS_ActionStep.DeviceID = "";

            if (cmb_DevicePartID.SelectedIndex >= 0)
                sYS_ActionStep.DevicePartID = cmb_DevicePartID.SelectedValue.ToString();
            else
                sYS_ActionStep.DevicePartID = "";

            if (cmb_Enabled.SelectedIndex >= 0)
                sYS_ActionStep.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
            else
                sYS_ActionStep.Enable = -1;

            if (!Verification(sYS_ActionStep))
                return;

            bool result;

            if (flag)
                result = actionStepRepository.Add(sYS_ActionStep);
            else
                result = actionStepRepository.Update(sYS_ActionStep);

            if (result)
            {
                hint.Text = NewuGlobal.GetRes("000171");
                GetData();
            }
            else
                hint.Text = NewuGlobal.GetRes("000172");
        }

        public void GetData()
        {
            string deviceID = "";
            if (cmbDeviceID.SelectedIndex >= 0)
            {
                deviceID = cmbDeviceID.SelectedValue.ToString();
                sYS_ActionSteps = actionStepRepository.GetListAddBitColumn("DeviceID='" + deviceID + "'");
            }
            else
                sYS_ActionSteps = actionStepRepository.GetListAddBitColumn("");

            dgv.DataSource = sYS_ActionSteps;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, rowIndex].Value.ToString();
                string ActionStepName = dgv[2, rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show(NewuGlobal.LanguagResourceManager.GetString("000175") + " [ " + ActionStepName + " ] ?", NewuGlobal.LanguagResourceManager.GetString("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = actionStepRepository.Delete(id);
                    if (isAccess)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000173"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000174"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmbDeviceID.SelectedIndex = -1;
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");

            txt_StepNameCN.Text = dgv.CurrentRow.Cells["StepNameCN"].Value.ToString();
            txt_StepNameEN.Text = dgv.CurrentRow.Cells["StepNameEN"].Value.ToString();
            txt_StepValue.Text = dgv.CurrentRow.Cells["StepValue"].Value.ToString();
            cmb_DeviceID.SelectedValue = dgv.CurrentRow.Cells["DeviceID"].Value.ToString();
            cmb_DevicePartID.SelectedValue = dgv.CurrentRow.Cells["DevicePartID"].Value.ToString();
            cmb_Enabled.SelectedValue = dgv.CurrentRow.Cells["Enable"].Value.ToString();
        }
    }
}