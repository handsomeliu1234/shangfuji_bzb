using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_Device_Add : Form
    {
        private SYS_Device deviceModel;
        private string deviceID = "";
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_WorkShopRepository workShopRepository = new SYS_WorkShopRepository();
        private readonly SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public FM_SYS_Device_Add()
        {
            InitializeComponent();
        }

        public FM_SYS_Device_Add(string id)
        {
            InitializeComponent();
            deviceID = id;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (deviceModel == null)
            { deviceModel = new SYS_Device(); }

            deviceModel.DeviceName = txt_DeviceName.Text.Trim();
            deviceModel.DeviceCode = txt_DeviceCode.Text.Trim();
            if (cmb_DeviceTypeID.SelectedIndex >= 0)
            {
                deviceModel.DeviceTypeID = cmb_DeviceTypeID.SelectedValue.ToString();
            }
            else
            {
                deviceModel.DeviceTypeID = "";
            }
            if (cmb_WorkShopID.SelectedIndex >= 0)
            {
                deviceModel.WorkShopID = cmb_WorkShopID.SelectedValue.ToString();
            }
            else
            {
                deviceModel.WorkShopID = "";
            }
            if (cmb_Enabled.SelectedIndex >= 0)
            {
                deviceModel.Enabled = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
            }
            else
            {
                deviceModel.Enabled = -1;
            }
            deviceModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
            {
                return;
            }

            bool isAccess;
            if (string.IsNullOrEmpty(deviceModel.DeviceID))
            {
                isAccess = deviceRepository.Add(deviceModel);
            }
            else
            {
                isAccess = deviceRepository.Update(deviceModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show("设备信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(deviceModel.DeviceID))
                    ClearControl();
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("工厂信息保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_Device fm = obj as FM_SYS_Device;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            if (deviceModel.DeviceName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000442") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (deviceModel.DeviceCode == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000443") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (deviceModel.DeviceTypeID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000444") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (deviceModel.WorkShopID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000445") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (deviceModel.Enabled == -1)
            {
                MessageBox.Show(NewuGlobal.GetRes("000447") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }

        private void ClearControl()
        {
            txt_DeviceName.Text = "";
            txt_DeviceCode.Text = "";
            cmb_DeviceTypeID.SelectedIndex = -1;
            cmb_WorkShopID.SelectedIndex = -1;
            cmb_Enabled.SelectedIndex = -1;
        }

        private void FM_SYS_Device_Add_Load(object sender, EventArgs e)
        {
            try
            {
                cmb_DeviceTypeID.DisplayMember = "DeviceTypeName";
                cmb_DeviceTypeID.ValueMember = "DeviceTypeID";
                cmb_DeviceTypeID.DataSource = deviceTypeRepository.GetList("");
                cmb_DeviceTypeID.DropDownStyle = ComboBoxStyle.DropDownList;

                cmb_WorkShopID.DisplayMember = "ShopName";
                cmb_WorkShopID.ValueMember = "WorkshopID";
                cmb_WorkShopID.DataSource = workShopRepository.GetList("");
                cmb_WorkShopID.DropDownStyle = ComboBoxStyle.DropDownList;

                cmb_Enabled.DisplayMember = "names";
                cmb_Enabled.ValueMember = "values";
                cmb_Enabled.DataSource = Newu.EnableList.GetList();
                cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;

                if (deviceID != "")
                {
                    deviceModel = deviceRepository.GetModel(deviceID);

                    txt_DeviceName.Text = deviceModel.DeviceName;
                    txt_DeviceCode.Text = deviceModel.DeviceCode;
                    cmb_DeviceTypeID.SelectedValue = deviceModel.DeviceTypeID;
                    cmb_WorkShopID.SelectedValue = deviceModel.WorkShopID;
                    try
                    {
                        cmb_Enabled.SelectedValue = deviceModel.Enabled;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                SetLanguage();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        private void SetLanguage()
        {
            Text = NewuGlobal.LanguagResourceManager.GetString("000449");
            this.btnSave.Text = NewuGlobal.LanguagResourceManager.GetString("000108");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            label1.Text = NewuGlobal.GetRes("000442") + "：";
            label2.Text = NewuGlobal.GetRes("000443") + "：";
            label3.Text = NewuGlobal.GetRes("000444") + "：";
            label4.Text = NewuGlobal.GetRes("000445") + "：";
            label9.Text = NewuGlobal.GetRes("000447") + "：";

            label10.Text = label8.Text = label7.Text = label6.Text = label5.Text = NewuGlobal.GetRes("000162");

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