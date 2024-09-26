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
    public partial class FM_SYS_DevicePart : Form, ILanguageChanged
    {
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private readonly SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();
        private FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private FormulaWeighRepository formulaWeighRepository = new FormulaWeighRepository();
        private DataGridViewComboBoxColumn dgvComboBoxColumn;

        public FM_SYS_DevicePart()
        {
            InitializeComponent();
        }

        private void FM_SYS_DevicePart_Load(object sender, EventArgs e)
        {
            List<SYS_DeviceType> sYS_DeviceTypes = deviceTypeRepository.GetList(0, "", "DeviceTypeID,DeviceTypeCode");
            cmbDeviceTypeID.DataSource = sYS_DeviceTypes;
            cmbDeviceTypeID.ValueMember = "DeviceTypeID";
            cmbDeviceTypeID.DisplayMember = "DeviceTypeName";
            cmbDeviceTypeID.SelectedIndex = -1;

            cmbEnabled.DisplayMember = "names";
            cmbEnabled.ValueMember = "values";
            cmbEnabled.DataSource = EnableList.GetList();
            cmbEnabled.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEnabled.SelectedIndex = 0;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("DevicePartID","设备部件ID", ColumnType.txt,false),
                new ColStruct("Reserve1","设备部件名称CN"),
                new ColStruct("DevicePartName","设备部件名称EN"),
                new ColStruct("DevicePartCode","设备部件编码"),
                new ColStruct("DevicePartJaneSpell","设备部件简拼"),
                new ColStruct("DevicePartNumber","最大个数"),
                new ColStruct("DeviceTypeID","设备类型",ColumnType.cmb,false),
                new ColStruct("Enable","是否启用", ColumnType.chk,true)
            };
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AddCols(cols);

            List<SYS_DeviceType> devices = new List<SYS_DeviceType>();
            devices.AddRange(sYS_DeviceTypes);
            dgvComboBoxColumn = dgv.Columns["DeviceTypeID"] as DataGridViewComboBoxColumn;
            dgvComboBoxColumn.DataSource = devices;
            dgvComboBoxColumn.ValueMember = "DeviceTypeID";

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            List<SYS_Device> sYS_Devices = deviceRepository.GetList(0, "", "DeviceID,DeviceCode");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            SetLanguage();
            GetData();
        }

        public void GetData()
        {
            try
            {
                string deviceID = "";
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    deviceID = cmb_DeviceID.SelectedValue.ToString();
                }

                List<SYS_DevicePart> sYS_DeviceParts = devicePartRepository.GetDevicePartListByDeviceID(deviceID);
                dgv.DataSource = sYS_DeviceParts;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_DevicePart").Error(ex.ToString());
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SYS_DevicePart sYS_DevicePart = new SYS_DevicePart();
            ExcuteData(sYS_DevicePart, true);
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
                string id = dgv[0, rowIndex].Value.ToString();
                SYS_DevicePart sYS_DevicePart = devicePartRepository.GetModel(id);
                ExcuteData(sYS_DevicePart, false);
            }
        }

        private void ExcuteData(SYS_DevicePart devicePartModel, bool flag)
        {
            try
            {
                List<SYS_DevicePart> sYS_DeviceParts = dgv.DataSource as List<SYS_DevicePart>;
                SYS_DevicePart sYS_DevicePart = sYS_DeviceParts.Find(s => s.DevicePartCode.Contains(txt_PartCode.Text.Trim()));
                //部件已存在且新增
                if (sYS_DevicePart != null && flag)
                {
                    hint.Text = NewuGlobal.GetRes("000322");
                    return;
                }

                //编辑 MixDrugScales_002
                if (!flag)
                {
                    //设备部件启用改为不启用删除配方
                    if (Convert.ToInt32(cmbEnabled.SelectedValue) == 0)
                    {
                        List<FormulaWeigh> formulaWeighs = formulaWeighRepository.GetListByDevicepartID(dgv.CurrentRow.Cells["DevicePartID"].Value.ToString());
                        formulaMaterialRepository.Delete(formulaWeighs);
                    }

                    if (!dgv.CurrentRow.Cells["DevicePartCode"].Value.Equals(txt_PartCode.Text.Trim()) && sYS_DevicePart != null)
                    {
                        hint.Text = NewuGlobal.GetRes("000322");
                        return;
                    }
                }

                devicePartModel.Reserve1 = txt_PartNameC.Text.Trim();
                devicePartModel.DevicePartName = txt_PartName.Text.Trim();

                string devicePartCode = txt_PartCode.Text.Trim();
                devicePartModel.DevicePartCode = devicePartCode;
                devicePartModel.DevicePartJaneSpell = txt_PartJaneSpell.Text.Trim();

                if (cmbDeviceTypeID.SelectedIndex >= 0)
                    devicePartModel.DeviceTypeID = cmbDeviceTypeID.SelectedValue.ToString();
                else
                    devicePartModel.DeviceTypeID = "";

                if (cmbEnabled.SelectedIndex >= 0)
                {
                    int value = Convert.ToInt32(cmbEnabled.SelectedValue);
                    devicePartModel.Enable = value;
                    string attribute = "";
                    bool state;
                    //启用
                    if (value == 1)
                        state = true;
                    else
                        state = false;

                    if (devicePartCode.Equals(NewuGlobal.CarbonScales))
                    {
                        NewuGlobal.SoftConfig.Carbon = state;
                        attribute = "Carbon";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.DrugScales))
                    {
                        NewuGlobal.SoftConfig.Drug = state;
                        attribute = "Drug";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.OilScales))
                    {
                        NewuGlobal.SoftConfig.Oil = state;
                        attribute = "Oil";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.PlaScales))
                    {
                        NewuGlobal.SoftConfig.Pla = state;
                        attribute = "Pla";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.ZnoScales))
                    {
                        NewuGlobal.SoftConfig.Zno = state;
                        attribute = "Zno";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.RubberScales))
                    {
                        NewuGlobal.SoftConfig.Rubber = state;
                        attribute = "Rubber";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.SiScales))
                    {
                        NewuGlobal.SoftConfig.Silane = state;
                        attribute = "Silane";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.DownMixers))
                    {
                        NewuGlobal.SoftConfig.DownMixer = state;
                        attribute = "DownMixer";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.OilScales2))
                    {
                        NewuGlobal.SoftConfig.Oil2 = state;
                        attribute = "Oil2";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.DrugScales2))
                    {
                        NewuGlobal.SoftConfig.Drug2 = state;
                        attribute = "Drug2";
                    }
                    else if (devicePartCode.Equals(NewuGlobal.ZnoScales2))
                    {
                        NewuGlobal.SoftConfig.Zno2 = state;
                        attribute = "Zno2";
                    }

                    NewuGlobal.SoftConfig.SetDevicePartState(state, attribute);
                }
                else
                {
                    devicePartModel.Enable = -1;
                }

                devicePartModel.DevicePartNumber = FunClass.VVal(txt_DevicePartNumber.Text);
                devicePartModel.SaveTime = DateTime.Now;
                if (!DataVerification(devicePartModel))
                    return;

                bool result;
                if (flag)
                {
                    int num = devicePartRepository.GetNum();
                    devicePartModel.PartNum = num + 1;
                    result = devicePartRepository.Add(devicePartModel);
                }
                else
                    result = devicePartRepository.Update(devicePartModel);

                if (result)
                {
                    hint.Text = NewuGlobal.GetRes("000171");
                    GetData();
                }
                else
                    hint.Text = NewuGlobal.GetRes("000172");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_DevicePart").Error(ex.ToString());
            }
        }

        private bool DataVerification(SYS_DevicePart devicePartModel)
        {
            if (devicePartModel.Reserve1 == "")
            {
                hint.Text = NewuGlobal.GetRes("000028") + "CN" + NewuGlobal.GetRes("000162");
                return false;
            }
            if (devicePartModel.DevicePartName == "")
            {
                hint.Text = NewuGlobal.GetRes("000028") + "EN" + NewuGlobal.GetRes("000162");
                return false;
            }
            if (devicePartModel.DevicePartCode == "")
            {
                hint.Text = NewuGlobal.GetRes("000752") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (devicePartModel.DeviceTypeID == "")
            {
                hint.Text = NewuGlobal.GetRes("000026") + NewuGlobal.GetRes("000162");
                return false;
            }

            return true;
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
                DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    bool isAccess = devicePartRepository.Delete(id);
                    if (isAccess)
                    {
                        hint.Text = NewuGlobal.GetRes("000173");
                        GetData();
                    }
                    else
                        hint.Text = NewuGlobal.GetRes("000174");
                }
                catch (Exception ex)
                {
                    NewuGlobal.LogCat("FM_SYS_DevicePart").Error(ex.ToString());
                }
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceID.SelectedIndex = 0;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            this.btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            this.btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            this.btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            btnQuery.Text = NewuGlobal.LanguagResourceManager.GetString("000104");
            btnReset.Text = NewuGlobal.LanguagResourceManager.GetString("000105");
            label1.Text = NewuGlobal.LanguagResourceManager.GetString("000182") + ":";

            label11.Text = NewuGlobal.GetRes("000751") + "CN:";
            label2.Text = NewuGlobal.GetRes("000751") + "EN:";
            label4.Text = NewuGlobal.GetRes("000752") + ":";
            label3.Text = NewuGlobal.GetRes("000753") + ":";
            label12.Text = NewuGlobal.GetRes("000026") + ":";
            label7.Text = NewuGlobal.GetRes("000754") + ":";
            label14.Text = NewuGlobal.GetRes("000188") + ":";
            hint.Text = NewuGlobal.GetRes("000170") + ":" + NewuGlobal.GetRes("000827");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                dgvComboBoxColumn.DisplayMember = "DeviceTypeName";
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                dgvComboBoxColumn.DisplayMember = "DeviceTypeCode";
            }
            groupBox2.Text = NewuGlobal.LanguagResourceManager.GetString("000361");
            groupBox1.Text = NewuGlobal.LanguagResourceManager.GetString("000438");

            dgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000751") + "CN";
            dgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000751") + "EN";
            dgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000752");
            dgv.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000753");
            dgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000754");
            dgv.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000026");
            dgv.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000188");
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170") + ":" + NewuGlobal.GetRes("000827");
            txt_PartNameC.Text = dgv.CurrentRow.Cells["Reserve1"].Value.ToString();
            txt_PartName.Text = dgv.CurrentRow.Cells["DevicePartName"].Value.ToString();
            txt_PartCode.Text = dgv.CurrentRow.Cells["DevicePartCode"].Value.ToString();
            txt_PartJaneSpell.Text = dgv.CurrentRow.Cells["DevicePartJaneSpell"].Value.ToString();
            cmbDeviceTypeID.SelectedValue = dgv.CurrentRow.Cells["DeviceTypeID"].Value.ToString();
            txt_DevicePartNumber.Text = dgv.CurrentRow.Cells["DevicePartNumber"].Value.ToString();
            cmbEnabled.SelectedValue = dgv.CurrentRow.Cells["Enable"].Value.ToString();
        }

        private void Txt_DevicePartNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            NewuTB.Utils.Utils.TxtPreSetGcsU(e, false);
        }
    }
}