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
    public partial class FM_SYS_Device : Form, ILanguageChanged
    {
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_WorkShopRepository workShopRepository = new SYS_WorkShopRepository();
        private readonly SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();
        private DataGridViewComboBoxColumn dgvComboBoxColumn;

        public FM_SYS_Device()
        {
            InitializeComponent();
        }

        private void FM_SYS_Device_Load(object sender, EventArgs e)
        {
            List<SYS_WorkShop> sYS_WorkShops = workShopRepository.GetAllList();
            cmb_WorkShop.DisplayMember = "ShopName";
            cmb_WorkShop.ValueMember = "WorkshopID";
            cmb_WorkShop.DataSource = sYS_WorkShops;
            cmb_WorkShop.SelectedIndex = -1;

            List<SYS_DeviceType> sYS_DeviceTypes = deviceTypeRepository.GetList("");
            
            cmb_DeviceType.ValueMember = "DeviceTypeID";
            cmb_DeviceType.DataSource = sYS_DeviceTypes;
            cmb_DeviceType.SelectedIndex = -1;

            List<SYS_DeviceType> deviceTypes = new List<SYS_DeviceType>();
            deviceTypes.AddRange(sYS_DeviceTypes);
            cmbDeviceTypeID.ValueMember = "DeviceTypeID";
            cmbDeviceTypeID.DataSource = deviceTypes;
            cmbDeviceTypeID.DropDownStyle = ComboBoxStyle.DropDownList;

            List<SYS_WorkShop> workShops = new List<SYS_WorkShop>();
            workShops.AddRange(sYS_WorkShops);
            cmbWorkShopID.DisplayMember = "ShopName";
            cmbWorkShopID.ValueMember = "WorkshopID";
            cmbWorkShopID.DataSource = workShops;
            cmbWorkShopID.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbEnabled.DisplayMember = "names";
            cmbEnabled.ValueMember = "values";
            cmbEnabled.DataSource = EnableList.GetList();
            cmbEnabled.DropDownStyle = ComboBoxStyle.DropDownList;

            ColStruct[] cols = new ColStruct[] {
                new ColStruct("DeviceID","ID", ColumnType.txt,false),
                new ColStruct("DeviceName","设备名称"),
                new ColStruct("DeviceCode","设备编码"),
                new ColStruct("DeviceTypeID","设备类型",ColumnType.cmb,true),
                new ColStruct("WorkshopID","所属车间",ColumnType.cmb,true),
                new ColStruct("Enabled","是否启用", ColumnType.chk,true)
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            List<SYS_DeviceType> devices = new List<SYS_DeviceType>();
            devices.AddRange(sYS_DeviceTypes);
            dgvComboBoxColumn = dgv.Columns["DeviceTypeID"] as DataGridViewComboBoxColumn;
            dgvComboBoxColumn.DataSource = devices;
            dgvComboBoxColumn.ValueMember = "DeviceTypeID";

            DataGridViewComboBoxColumn workShopComboBoxColumn = dgv.Columns["WorkshopID"] as DataGridViewComboBoxColumn;
            workShopComboBoxColumn.DataSource = workShops;
            workShopComboBoxColumn.DisplayMember = "ShopName";
            workShopComboBoxColumn.ValueMember = "WorkshopID";

            SetLanguage();
            GetData();
        }

        public void GetData()
        {
            try
            {
                string typeId = "";
                string shopId = "";
                if (cmb_DeviceType.SelectedIndex >= 0)
                    typeId = cmb_DeviceType.SelectedValue.ToString();

                if (cmb_WorkShop.SelectedIndex >= 0)
                    shopId = cmb_WorkShop.SelectedValue.ToString();

                List<SYS_Device> sYS_Devices = deviceRepository.GetListJoin(shopId, typeId);
                dgv.DataSource = sYS_Devices;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Device").Error(ex.ToString());
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_WorkShop.SelectedIndex = -1;
            cmb_DeviceType.SelectedIndex = -1;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SYS_Device sYS_Device = new SYS_Device();
            ExcuteData(sYS_Device, true);
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
                string id = dgv[0, dgv.CurrentCell.RowIndex].Value.ToString();
                SYS_Device sYS_Device = deviceRepository.GetModel(id);
                ExcuteData(sYS_Device, false);
            }
        }

        private void ExcuteData(SYS_Device deviceModel, bool flag)
        {
            try
            {
                deviceModel.DeviceName = txt_DeviceName.Text.Trim();
                deviceModel.DeviceCode = txt_DeviceCode.Text.Trim();
                if (cmbDeviceTypeID.SelectedIndex >= 0)
                    deviceModel.DeviceTypeID = cmbDeviceTypeID.SelectedValue.ToString();
                else
                    deviceModel.DeviceTypeID = "";

                if (cmbWorkShopID.SelectedIndex >= 0)
                    deviceModel.WorkShopID = cmbWorkShopID.SelectedValue.ToString();
                else
                    deviceModel.WorkShopID = "";

                if (cmbEnabled.SelectedIndex >= 0)
                    deviceModel.Enabled = Convert.ToInt32(cmbEnabled.SelectedValue.ToString());
                else
                    deviceModel.Enabled = -1;

                deviceModel.SaveTime = DateTime.Now;

                if (!DataVerification(deviceModel))
                    return;

                bool result;
                if (flag)
                    result = deviceRepository.Add(deviceModel);
                else
                    result = deviceRepository.Update(deviceModel);

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
                NewuGlobal.LogCat("FM_SYS_Device").Error(ex.ToString());
            }
        }

        private bool DataVerification(SYS_Device deviceModel)
        {
            if (deviceModel.DeviceName == "")
            {
                hint.Text = NewuGlobal.GetRes("000442") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (deviceModel.DeviceCode == "")
            {
                hint.Text = NewuGlobal.GetRes("000443") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (deviceModel.DeviceTypeID == "")
            {
                hint.Text = NewuGlobal.GetRes("000444") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (deviceModel.WorkShopID == "")
            {
                hint.Text = NewuGlobal.GetRes("000445") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (deviceModel.Enabled == -1)
            {
                hint.Text = NewuGlobal.GetRes("000447") + NewuGlobal.GetRes("000162");
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
                string deviceName = dgv[2, rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show("[ " + deviceName + " ] " + NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = deviceRepository.Delete(id);
                    if (isAccess)
                    {
                        hint.Text = NewuGlobal.GetRes("000173");

                        GetData();
                    }
                    else
                    {
                        hint.Text = NewuGlobal.GetRes("000174");
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
            label2.Text = NewuGlobal.LanguagResourceManager.GetString("000445") + ":";
            label1.Text = NewuGlobal.LanguagResourceManager.GetString("000444") + ":";

            label3.Text = NewuGlobal.GetRes("000442") + ":";
            label4.Text = NewuGlobal.GetRes("000443") + ":";
            label6.Text = NewuGlobal.GetRes("000444") + ":";
            label10.Text = NewuGlobal.GetRes("000445") + ":";
            label12.Text = NewuGlobal.GetRes("000447") + ":";
            hint.Text = NewuGlobal.GetRes("000170");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                dgvComboBoxColumn.DisplayMember = "DeviceTypeName";
                cmbDeviceTypeID.DisplayMember= cmb_DeviceType.DisplayMember = "DeviceTypeName";
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                dgvComboBoxColumn.DisplayMember = "DeviceTypeCode";
                cmbDeviceTypeID.DisplayMember = "DeviceTypeCode";
            }
            gropBox1.Text = NewuGlobal.LanguagResourceManager.GetString("000586");
            groupBox1.Text = NewuGlobal.LanguagResourceManager.GetString("000438");
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].HeaderText = NewuGlobal.GetRes("000" + (441 + i));
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");
            txt_DeviceName.Text = dgv.CurrentRow.Cells["DeviceName"].Value.ToString();
            txt_DeviceCode.Text = dgv.CurrentRow.Cells["DeviceCode"].Value.ToString();
            cmbDeviceTypeID.SelectedValue = dgv.CurrentRow.Cells["DeviceTypeID"].Value.ToString();
            cmb_WorkShop.SelectedValue = dgv.CurrentRow.Cells["WorkshopID"].Value.ToString();
            cmbEnabled.SelectedValue = dgv.CurrentRow.Cells["Enabled"].Value.ToString();
        }
    }
}