using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_DevicePart_Add : Form
    {
        private string devicePartID = "";
        private readonly SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private SYS_DevicePart devicePartModel;

        public FM_SYS_DevicePart_Add()
        {
            InitializeComponent();
        }

        public FM_SYS_DevicePart_Add(string _DevicePartID)
        {
            InitializeComponent();
            devicePartID = _DevicePartID;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (devicePartModel == null)
            {
                devicePartModel = new SYS_DevicePart();
            }

            devicePartModel.Reserve1 = txt_PartNameC.Text.Trim();
            devicePartModel.DevicePartName = txt_PartName.Text.Trim();
            devicePartModel.DevicePartCode = txt_PartCode.Text.Trim();
            devicePartModel.DevicePartJaneSpell = txt_PartJaneSpell.Text.Trim();

            if (cmb_DeviceTypeID.SelectedIndex >= 0)
            {
                devicePartModel.DeviceTypeID = cmb_DeviceTypeID.SelectedValue.ToString();
            }
            else
            {
                devicePartModel.DeviceTypeID = "";
            }

            if (cmb_Enabled.SelectedIndex >= 0)
            {
                devicePartModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue);
            }
            else
            {
                devicePartModel.Enable = -1;
            }

            devicePartModel.DevicePartNumber = NewuCommon.FunClass.VVal(txt_DevicePartNumber.Text);
            devicePartModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
            {
                return;
            }

            bool isAccess;
            if (string.IsNullOrEmpty(devicePartModel.DevicePartID))
            {
                isAccess = devicePartRepository.Add(devicePartModel);
            }
            else
            {
                isAccess = devicePartRepository.Update(devicePartModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show("部件信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(devicePartModel.DevicePartID))
                {
                    ClearControl();
                }
                RefreshGrid();
                this.Close();
            }
            else
            {
                MessageBox.Show("部件信息保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearControl()
        {
            txt_PartName.Text = "";
            txt_PartCode.Text = "";
            cmb_DeviceTypeID.SelectedIndex = -1;
            cmb_Enabled.SelectedIndex = -1;
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_DevicePart fm = obj as FM_SYS_DevicePart;
                fm.GetData();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool DataVerification()
        {
            if (devicePartModel.Reserve1 == "")
            {
                MessageBox.Show("部件中文名称不能为空！");
                return false;
            }
            if (devicePartModel.DevicePartName == "")
            {
                MessageBox.Show("部件英文名称不能为空！");
                return false;
            }
            if (devicePartModel.DevicePartCode == "")
            {
                MessageBox.Show("部件编码不能为空！");
                return false;
            }
            if (devicePartModel.DeviceTypeID == "")
            {
                MessageBox.Show("设备类型不能为空！");
                return false;
            }

            return true;
        }

        private void FM_SYS_DevicePart_Add_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;
            List<SYS_DeviceType> sYS_DeviceTypes = deviceTypeRepository.GetList(0, "", "DeviceTypeID,DeviceTypeCode");
            cmb_DeviceTypeID.DataSource = sYS_DeviceTypes;
            cmb_DeviceTypeID.ValueMember = "DeviceTypeID";
            cmb_DeviceTypeID.DisplayMember = "DeviceTypeName";
            cmb_DeviceTypeID.SelectedIndex = -1;

            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DataSource = EnableList.GetList();
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Enabled.SelectedIndex = 0;

            if (devicePartID != "")
            {
                devicePartModel = devicePartRepository.GetModel(devicePartID);

                txt_PartName.Text = devicePartModel.DevicePartName;
                txt_PartNameC.Text = devicePartModel.Reserve1;
                txt_PartCode.Text = devicePartModel.DevicePartCode;
                txt_PartJaneSpell.Text = devicePartModel.DevicePartJaneSpell;
                cmb_DeviceTypeID.SelectedValue = devicePartModel.DeviceTypeID;
                txt_DevicePartNumber.Text = devicePartModel.DevicePartNumber.ToString();
                cmb_Enabled.SelectedValue = devicePartModel.Enable;
            }
            SetLanguage();
        }

        private void SetLanguage()
        {
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            label11.Text = NewuGlobal.GetRes("000751") + "CN：";
            label1.Text = NewuGlobal.GetRes("000751") + "EN：";
            label2.Text = NewuGlobal.GetRes("000752") + "：";
            label3.Text = NewuGlobal.GetRes("000753") + "：";
            label4.Text = NewuGlobal.GetRes("000026") + "：";
            label7.Text = NewuGlobal.GetRes("000754") + "：";
            label13.Text = NewuGlobal.GetRes("000188") + "：";
            label5.Text = label6.Text = label8.Text = label9.Text = label10.Text = label12.Text = NewuGlobal.GetRes("000162");
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