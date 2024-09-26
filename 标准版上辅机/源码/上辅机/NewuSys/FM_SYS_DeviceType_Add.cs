using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;

namespace NewuSys
{
    public partial class FM_SYS_DeviceType_Add : Form
    {
        private SYS_DeviceType deviceTypeModel;
        private string deviceTypeID;

        private readonly SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();

        public FM_SYS_DeviceType_Add()
        {
            InitializeComponent();
        }

        public FM_SYS_DeviceType_Add(string _DeviceTypeID)
        {
            InitializeComponent();
            deviceTypeID = _DeviceTypeID;
        }

        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (deviceTypeModel == null)
                deviceTypeModel = new SYS_DeviceType();

            deviceTypeModel.DeviceTypeName = txt_DeviceTypeName.Text.Trim();
            deviceTypeModel.DeviceTypeCode = txt_DeviceTypeCode.Text.Trim();
            deviceTypeModel.DeviceTypeJaneSpell = txt_DeviceTypeJaneSpell.Text.Trim();
            deviceTypeModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
                return;

            bool isAccess;
            if (string.IsNullOrEmpty(deviceTypeModel.DeviceTypeID))
            {
                isAccess = deviceTypeRepository.Add(deviceTypeModel);
            }
            else
            {
                isAccess = deviceTypeRepository.Update(deviceTypeModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show("工厂信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(deviceTypeModel.DeviceTypeID))
                { ClearControl(); }
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
                FM_SYS_DeviceType fm = obj as FM_SYS_DeviceType;
                fm.GetData();
            }
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FM_SYS_DeviceType_Add_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;

            if (!string.IsNullOrEmpty(deviceTypeID))
            {
                deviceTypeModel = deviceTypeRepository.GetModel(" DeviceTypeID = '" + deviceTypeID + "'");

                txt_DeviceTypeName.Text = deviceTypeModel.DeviceTypeName;
                txt_DeviceTypeCode.Text = deviceTypeModel.DeviceTypeCode;
                txt_DeviceTypeJaneSpell.Text = deviceTypeModel.DeviceTypeJaneSpell;
            }
            SetLanguage();
        }

        private void SetLanguage()
        {
            Text = NewuGlobal.GetRes("000759");
            label1.Text = NewuGlobal.GetRes("000756") + "：";
            label2.Text = NewuGlobal.GetRes("000757") + "：";
            label3.Text = NewuGlobal.GetRes("000758") + "：";
            label4.Text = label5.Text = NewuGlobal.GetRes("000162");
            groupBox1.Text = NewuGlobal.GetRes("000450");
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
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
            txt_DeviceTypeName.Text = "";
            txt_DeviceTypeCode.Text = "";
            txt_DeviceTypeJaneSpell.Text = "";
        }

        private bool DataVerification()
        {
            if (deviceTypeModel.DeviceTypeName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000756")+ NewuGlobal.GetRes("000162"));
                return false;
            }
            if (deviceTypeModel.DeviceTypeCode == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000757") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }
    }
}