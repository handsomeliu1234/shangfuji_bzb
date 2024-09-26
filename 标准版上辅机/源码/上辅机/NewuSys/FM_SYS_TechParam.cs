using MultiLanguage;
using Newu;
using NewuCommon;
using NewuTB.Utils;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_TechParam : Form, ILanguageChanged
    {
        private readonly SYS_TechParamFRepository techParamRepository = new SYS_TechParamFRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private DataGridViewComboBoxColumn cmb_DevicePartid;

        public FM_SYS_TechParam()
        {
            InitializeComponent();
        }

        private void FM_SYS_TechParam_Load(object sender, EventArgs e)
        {
            List<SYS_Device> sYS_Devices = deviceRepository.GetList("");
            cmbDeviceID.DataSource = sYS_Devices;
            cmbDeviceID.DisplayMember = "DeviceName";
            cmbDeviceID.ValueMember = "DeviceID";
            cmbDeviceID.SelectedIndex = -1;

            List<SYS_Device> list = new List<SYS_Device>();
            list.AddRange(sYS_Devices);
            cmb_DeviceID.DataSource = list;
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_DeviceID.SelectedIndex = -1;

            cmb_Enabled.DataSource = EnableList.GetList();
            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;

            ColStruct[] cols = new ColStruct[] {
                new ColStruct("TechParamID","工艺参数ID", ColumnType.txt,false),
                new ColStruct("DeviceID","所属设备", ColumnType.cmb,true),
                new ColStruct("DevicePartID","设备部件", ColumnType.cmb,true),
                new ColStruct("TechParamNameCN","工艺参数名称CN"),
                new ColStruct("TechParamNameEN","工艺参数名称EN"),
                new ColStruct("TechParamOrder","工艺参数顺序"),
                new ColStruct("DecDigit","小数位"),
                new ColStruct("Unit","单位"),
                new ColStruct("SaveTime","保存时间"),
                new ColStruct("Enable","是否启用", ColumnType.chk,true)
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewComboBoxColumn cmb_Deviceid = (DataGridViewComboBoxColumn)dgv.Columns["DeviceID"];
            cmb_Deviceid.DataSource = deviceRepository.GetList("");
            cmb_Deviceid.DisplayMember = "DeviceName";
            cmb_Deviceid.ValueMember = "DeviceID";

            cmb_DevicePartid = (DataGridViewComboBoxColumn)dgv.Columns["DevicePartID"];
            cmb_DevicePartid.DataSource = devicePartRepository.GetList("");
            cmb_DevicePartid.ValueMember = "DevicePartID";

            GetData();
            SetLanguage();
        }

        public void GetData()
        {
            List<SYS_TechParam> sYS_TechParams = techParamRepository.GetList(0, "", "TechParamOrder");
            dgv.DataSource = sYS_TechParams;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SYS_TechParam techParam = new SYS_TechParam();
                ExcuteData(techParam, true);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_TechParam").Error(ex.ToString());
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                    return;
                int rowIndex = dgv.CurrentCell.RowIndex;
                if (rowIndex >= 0)
                {
                    string id = dgv[0, rowIndex].Value.ToString();
                    SYS_TechParam sYS_TechParam = techParamRepository.GetModel(id);
                    ExcuteData(sYS_TechParam, false);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_TechParam").Error(ex.ToString());
            }

        }

        private void ExcuteData(SYS_TechParam techParamModel, bool flag)
        {
            if (cmb_DeviceID.SelectedIndex >= 0)
                techParamModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            else
                techParamModel.DeviceID = "";

            if (cmb_DevicePartID.SelectedIndex >= 0)
                techParamModel.DevicePartID = cmb_DevicePartID.SelectedValue.ToString();
            else
                techParamModel.DevicePartID = "";

            techParamModel.TechParamNameCN = txt_TechParamNameCN.Text.Trim();
            techParamModel.TechParamNameEN = txt_TechParamNameEN.Text.Trim();

            if (string.IsNullOrEmpty(txt_TechParamOrder.Text))
                techParamModel.TechParamOrder = 0;
            else
                techParamModel.TechParamOrder = Convert.ToInt32(txt_TechParamOrder.Text);

            techParamModel.Unit = txt_Unit.Text;

            if (string.IsNullOrEmpty(txt_DecDigit.Text))
                techParamModel.DecDigit = 0;
            else
                techParamModel.DecDigit = Convert.ToInt32(txt_DecDigit.Text);

            techParamModel.SaveUserID = NewuGlobal.TB_UserInfo.UserID;

            if (cmb_Enabled.SelectedIndex >= 0)
                techParamModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue);
            else
                techParamModel.Enable = -1;

            techParamModel.SaveTime = DateTime.Now;

            if (!DataVerification(techParamModel))
                return;

            bool result;
            if (flag)
                result = techParamRepository.Add(techParamModel);
            else
                result = techParamRepository.UpdateModel(techParamModel);

            if (result)
            {
                hint.Text = NewuGlobal.GetRes("000171");
                GetData();
            }
            else
                hint.Text = NewuGlobal.GetRes("000172");
        }

        private bool DataVerification(SYS_TechParam techParamModel)
        {
            if (techParamModel.TechParamNameCN == "")
            {
                hint.Text = NewuGlobal.GetRes("000760") + "CN" + NewuGlobal.GetRes("000162");
                return false;
            }
            if (techParamModel.TechParamOrder == 0)
            {
                hint.Text = NewuGlobal.GetRes("000761") + "CN" + NewuGlobal.GetRes("000162");
                return false;
            }
            if (techParamModel.DecDigit < 0)
            {
                hint.Text = NewuGlobal.GetRes("000167");
                return false;
            }
            if (techParamModel.SaveUserID == "")
            {
                hint.Text = NewuGlobal.GetRes("000393") + " " + NewuGlobal.GetRes("000162");
                return false;
            }
            if (techParamModel.Enable == -1)
            {
                hint.Text = NewuGlobal.GetRes("000188") + "CN" + NewuGlobal.GetRes("000162");
                return false;
            }
            return true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
                return;
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, rowIndex].Value.ToString();
                string deviceName;
                if (NewuGlobal.SupportLanguage.Equals("1"))
                    deviceName = dgv[3, rowIndex].Value.ToString();
                else
                    deviceName = dgv[4, rowIndex].Value.ToString();

                DialogResult isDel = MessageBox.Show(NewuGlobal.LanguagResourceManager.GetString("000175") + " [ " + deviceName + " ] ?", NewuGlobal.LanguagResourceManager.GetString("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = techParamRepository.Delete(id);
                    if (isAccess)
                    {
                        hint.Text = NewuGlobal.LanguagResourceManager.GetString("000173");
                        GetData();
                    }
                    else
                    {
                        hint.Text = NewuGlobal.LanguagResourceManager.GetString("000174");
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<SYS_TechParam> list;
            String value = Convert.ToString(cmbDeviceID.SelectedValue);
            if (value != "")
            {
                list = techParamRepository.GetList("DeviceID='" + value + "' order by TechParamOrder");
            }
            else
            {
                list = techParamRepository.GetList(0, "", "TechParamOrder");
            }
            dgv.DataSource = list;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cmbDeviceID.SelectedIndex = -1;
            dgv.DataSource = null;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            label2.Text = label1.Text = NewuGlobal.GetRes("000532") + ":";
            label3.Text = NewuGlobal.GetRes("000028") + ":";
            label4.Text = NewuGlobal.GetRes("000760") + "CN:";
            label5.Text = NewuGlobal.GetRes("000760") + "EN:";
            label12.Text = NewuGlobal.GetRes("000761") + ":";
            label15.Text = NewuGlobal.GetRes("000763") + ":";
            label11.Text = NewuGlobal.GetRes("000762") + ":";
            label9.Text = NewuGlobal.GetRes("000188") + ":";
            hint.Text = NewuGlobal.GetRes("000170");

            this.btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            this.btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            this.btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            btnQuery.Text = NewuGlobal.LanguagResourceManager.GetString("000104");
            btnReset.Text = NewuGlobal.LanguagResourceManager.GetString("000105");
            groupBox1.Text = NewuGlobal.GetRes("000553");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                cmb_DevicePartid.DisplayMember = "Reserve1";
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                cmb_DevicePartid.DisplayMember = "DevicePartName";
            }

            dgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000182");
            dgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000028");
            dgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000760") + "CN";
            dgv.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000760") + "EN";
            dgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000761");
            dgv.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000762");
            dgv.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000763");
            dgv.Columns[8].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000081");
            dgv.Columns[9].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000188");
        }

        private void Cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_DeviceID.SelectedValue != null)
            {
                string deviceId = cmb_DeviceID.SelectedValue.ToString();
                cmb_DevicePartID.ValueMember = "DevicePartID";
                if (NewuGlobal.SupportLanguage.Equals("1"))
                    cmb_DevicePartID.DisplayMember = "Reserve1";
                else
                    cmb_DevicePartID.DisplayMember = "DevicePartName";

                if (cmb_DeviceID.SelectedIndex >= 0)
                    deviceId = cmb_DeviceID.SelectedValue.ToString();

                List<SYS_DevicePart> sYS_DeviceParts = devicePartRepository.GetDevicePartListByDeviceID(deviceId);
                cmb_DevicePartID.DataSource = sYS_DeviceParts;
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");

            cmb_DeviceID.SelectedValue = dgv.CurrentRow.Cells["DeviceID"].Value.ToString();
            cmb_DevicePartID.SelectedValue = dgv.CurrentRow.Cells["DevicePartID"].Value.ToString();
            txt_TechParamNameCN.Text = dgv.CurrentRow.Cells["TechParamNameCN"].Value.ToString();
            txt_TechParamNameEN.Text = dgv.CurrentRow.Cells["TechParamNameEN"].Value.ToString();
            txt_TechParamOrder.Text = dgv.CurrentRow.Cells["TechParamOrder"].Value.ToString();
            txt_Unit.Text = dgv.CurrentRow.Cells["Unit"].Value.ToString();
            txt_DecDigit.Text = dgv.CurrentRow.Cells["DecDigit"].Value.ToString();
            cmb_Enabled.SelectedValue = dgv.CurrentRow.Cells["Enable"].Value.ToString();
        }

        private void Txt_TechParam_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.TxtPreSetGcsU(e, false);
        }
    }
}