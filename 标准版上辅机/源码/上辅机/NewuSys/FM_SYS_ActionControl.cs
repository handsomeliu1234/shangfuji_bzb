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
    public partial class FM_SYS_ActionControl : Form, ILanguageChanged
    {
        private readonly SYS_ActionControlRepository actionControlRepository = new SYS_ActionControlRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private List<SYS_ActionControl> sYS_ActionControls;

        public FM_SYS_ActionControl()
        {
            InitializeComponent();
        }

        private void FM_SYS_ActionControl_Load(object sender, EventArgs e)
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

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("ActionControlCode","控制方式编码", ColumnType.txt,false),
                new ColStruct("ActionControlNameCN","控制方式中文"),
                new ColStruct("ActionControlNameEN","控制方式英文"),
                new ColStruct("ActionControlValue","控制方式值"),
                new ColStruct("DeviceID","所属设备",ColumnType.cmb,true),
                new ColStruct("SaveUserID","保存用户"),
                new ColStruct("Enable","是否启用", ColumnType.chk,true)
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewComboBoxColumn dgvColDepartment = dgv.Columns["DeviceID"] as DataGridViewComboBoxColumn;
            dgvColDepartment.DisplayMember = "DeviceName";
            dgvColDepartment.ValueMember = "DeviceID";
            dgvColDepartment.DataSource = devices;

            GetData();
            SetControlLanguageText();
        }

        public void GetData()
        {
            string deviceID = "";
            if (cmbDeviceID.SelectedIndex >= 0)
            {
                deviceID = cmbDeviceID.SelectedValue.ToString();
            }

            sYS_ActionControls = actionControlRepository.GetListJoin(deviceID);
            dgv.DataSource = sYS_ActionControls;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SYS_ActionControl actionControl = new SYS_ActionControl();
                ExcuteData(actionControl, true);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_ActionControl").Error(ex.ToString());
            }
        }

        private bool DataVerification(SYS_ActionControl actionControlModel)
        {
            //"控制方式中文不能为空！"
            if (string.IsNullOrEmpty(actionControlModel.ActionControlNameCN))
            {
                hint.Text = NewuGlobal.GetRes("000684") + " " + NewuGlobal.GetRes("000162");
                return false;
            }
            //"控制方式英文不能为空！"
            if (string.IsNullOrEmpty(actionControlModel.ActionControlNameEN))
            {
                hint.Text = NewuGlobal.GetRes("000684") + "EN " + NewuGlobal.GetRes("000162");
                return false;
            }
            //"控制方式值不能为空！"
            if (actionControlModel.ActionControlValue <= 0)
            {
                hint.Text = NewuGlobal.GetRes("000167");
                return false;
            }
            //"所属设备不能为空！"
            if (string.IsNullOrEmpty(actionControlModel.DeviceID))
            {
                hint.Text = NewuGlobal.GetRes("000342") + " " + NewuGlobal.GetRes("000162");
                return false;
            }
            //"保存用户不能为空！"
            if (string.IsNullOrEmpty(actionControlModel.SaveUserID))
            {
                hint.Text = NewuGlobal.GetRes("000393") + " " + NewuGlobal.GetRes("000162");
                return false;
            }
            SYS_ActionControl tempModel = sYS_ActionControls.Find(s => (s.ActionControlNameCN.Equals(actionControlModel.ActionControlNameCN) || s.ActionControlNameEN.Equals(actionControlModel.ActionControlNameEN) || s.ActionControlValue == actionControlModel.ActionControlValue) && s.ActionControlCode != actionControlModel.ActionControlCode && s.DeviceID.Equals(actionControlModel.DeviceID));
            if (tempModel != null)
            {
                hint.Text = NewuGlobal.GetRes("000031") + NewuGlobal.GetRes("000322");
                return false;
            }

            return true;
        }

        private void ExcuteData(SYS_ActionControl actionControl, bool flag)
        {
            actionControl.ActionControlNameCN = txt_ControlNameCN.Text.Trim();
            actionControl.ActionControlNameEN = txt_ControlNameEN.Text.Trim();
            actionControl.ActionControlValue = FunClass.VVal(txt_ControlValue.Text.Trim());
            actionControl.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            actionControl.SaveUserID = txt_UserID.Text;
            actionControl.SaveTime = DateTime.Now;

            if (cmb_DeviceID.SelectedIndex >= 0)
                actionControl.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            else
                actionControl.DeviceID = "";

            if (cmb_Enabled.SelectedIndex >= 0)
                actionControl.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
            else
                actionControl.Enable = -1;

            if (!DataVerification(actionControl))
                return;

            bool result;
            if (flag)
                result = actionControlRepository.Add(actionControl);
            else
                result = actionControlRepository.Update(actionControl);

            if (result)
            {
                hint.Text = NewuGlobal.GetRes("000171");
                GetData();
            }
            else
                hint.Text = NewuGlobal.GetRes("000172");
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            string actionControlCode = dgv.CurrentRow.Cells["ActionControlCode"].Value.ToString();
            SYS_ActionControl sYS_ActionControl = actionControlRepository.GetModel("ActionControlCode = '" + actionControlCode + "'");
            ExcuteData(sYS_ActionControl, false);
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {   //确定要删除控制方式
                string id = dgv[0, rowIndex].Value.ToString();
                string ControlName = dgv[1, rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show(NewuGlobal.LanguagResourceManager.GetString("000175") + " [ " + ControlName + " ] ?", NewuGlobal.LanguagResourceManager.GetString("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = actionControlRepository.Delete(id);
                    if (isAccess)
                    {
                        hint.Text=NewuGlobal.LanguagResourceManager.GetString("000173");
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

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmbDeviceID.SelectedIndex = -1;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            this.Text = NewuGlobal.LanguagResourceManager.GetString("000285");
            this.btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            this.btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            this.btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            this.btnQuery.Text = NewuGlobal.LanguagResourceManager.GetString("000104");
            this.btnReset.Text = NewuGlobal.LanguagResourceManager.GetString("000105");
            label1.Text = NewuGlobal.LanguagResourceManager.GetString("000270")+":";

            label2.Text = NewuGlobal.GetRes("000684") + "CN:"; //控制方式
            label3.Text = NewuGlobal.GetRes("000684") + "EN:"; //控制方式英文
            label4.Text = NewuGlobal.GetRes("000684") + "Value:"; //控制方式值
            label5.Text = NewuGlobal.GetRes("000342") + ":"; //所属设备
            label6.Text = NewuGlobal.GetRes("000393") + ":"; //保存用户
            label11.Text = NewuGlobal.GetRes("000188") + ":"; //是否启用
            hint.Text = NewuGlobal.GetRes("000170");

            groupBox1.Text = NewuGlobal.GetRes("000298");  //  查询条件
            groupBox2.Text = NewuGlobal.GetRes("000031") + NewuGlobal.GetRes("000381");  // 动作控制方式信息列表

            dgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000634") + " CN";
            dgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000634") + " EN";
            dgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000634") + " Value";
            dgv.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000270");
            dgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000393");
            dgv.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000188");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnDel.Padding = btnClose.Padding = btnQuery.Padding = btnReset.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnDel.Padding = btnClose.Padding = btnQuery.Padding = btnReset.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");
            if (e.RowIndex < 0)
                return;

            txt_ControlNameCN.Text = dgv.CurrentRow.Cells["ActionControlNameCN"].Value.ToString();
            txt_ControlNameEN.Text = dgv.CurrentRow.Cells["ActionControlNameEN"].Value.ToString();
            txt_ControlValue.Text = dgv.CurrentRow.Cells["ActionControlValue"].Value.ToString();
            cmb_DeviceID.SelectedValue = dgv.CurrentRow.Cells["DeviceID"].Value.ToString();
            txt_UserID.Text = dgv.CurrentRow.Cells["SaveUserID"].Value.ToString();
            cmb_Enabled.SelectedValue = dgv.CurrentRow.Cells["Enable"].Value.ToString();
        }
    }
}