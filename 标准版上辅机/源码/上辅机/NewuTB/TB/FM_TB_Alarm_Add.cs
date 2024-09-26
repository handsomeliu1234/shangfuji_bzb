using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_Alarm_Add : Form
    {
        private string alarmID = "";
        private TB_Alarm alarmModel = new TB_Alarm();

        private readonly TB_AlarmRepository alarmRepository = new TB_AlarmRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();

        private bool isAdd
        {
            get; set;
        }

        public FM_TB_Alarm_Add()
        {
            InitializeComponent();
            isAdd = true;
        }

        public FM_TB_Alarm_Add(string _AlarmID)
        {
            InitializeComponent();
            alarmID = _AlarmID;
            if (string.IsNullOrEmpty(_AlarmID))
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }
        }

        private void FM_TB_Alarm_Add_Load(object sender, EventArgs e)
        {
            SetLanguage();
            List<SYS_Device> Devicelist = deviceRepository.GetList("");
            cmb_DeviceID.DataSource = Devicelist;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            cmb_IsDisplay.DisplayMember = "names";
            cmb_IsDisplay.ValueMember = "values";
            cmb_IsDisplay.DataSource = EnableList.GetList();
            if (alarmID != "")
            {
                alarmModel = alarmRepository.GetModel(alarmID);
                cmb_DeviceID.SelectedValue = alarmModel.DeviceID;
                cmb_DevicePartID.SelectedValue = alarmModel.DevicePartID;
                txt_AlarmInfo.Text = alarmModel.AlarmInfo;
                txt_MemoryAddr.Text = alarmModel.MemoryAddr.ToString();
                txt_x.Text = alarmModel.TagAddress;
                cmb_IsDisplay.SelectedValue = alarmModel.IsDisplay;
            }
        }

        private void SetLanguage()
        {
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            label1.Text = NewuGlobal.GetRes("000182") + ":";
            label2.Text = NewuGlobal.GetRes("000130") + ":";
            label3.Text = NewuGlobal.GetRes("000365") + ":";
            label4.Text = NewuGlobal.GetRes("000747") + ":";
            label5.Text = NewuGlobal.GetRes("000748") + ":";
            label6.Text = NewuGlobal.GetRes("000749") + ":";
            groupBox1.Text = NewuGlobal.GetRes("000750") + ":";
            label7.Text = label8.Text = label9.Text = label10.Text = NewuGlobal.GetRes("000176");
            if (!NewuGlobal.SupportLanguage.Equals("1"))
                btnClose.Padding = new Padding(0, 0, 2, 0);
        }

        private void ClearControl()
        {
            cmb_DeviceID.SelectedValue = -1;
            cmb_DevicePartID.SelectedValue = -1;
            txt_AlarmInfo.Text = "";
            txt_MemoryAddr.Text = "";
            txt_x.Text = "";
            cmb_IsDisplay.SelectedValue = -1;
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_TB_Alarm fm = obj as FM_TB_Alarm;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            if (alarmModel.DeviceID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000182") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (alarmModel.DevicePartID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000130") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (alarmModel.AlarmInfo == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000049") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (alarmModel.MemoryAddr.ToString() == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000747") + NewuGlobal.GetRes("000162"));
                return false;
            }

            if (alarmModel.MemoryAddr <= 38000 || alarmModel.MemoryAddr >= 50240)
            {
                MessageBox.Show(NewuGlobal.GetRes("000747") + NewuGlobal.GetRes("000804"));
                return false;
            }

            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txt_AlarmInfo.Text == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000049") + NewuGlobal.GetRes("000162"));
                return;
            }
            if (txt_MemoryAddr.Text == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000747") + NewuGlobal.GetRes("000162"));
                return;
            }
            if (alarmModel == null)
            {
                alarmModel = new TB_Alarm();
            }
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                alarmModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            }
            else
            {
                alarmModel.DeviceID = "";
            }
            if (cmb_DevicePartID.SelectedIndex >= 0)
            {
                alarmModel.DevicePartID = cmb_DevicePartID.SelectedValue.ToString();
            }
            else
            {
                alarmModel.DevicePartID = "";
            }
            alarmModel.AlarmInfo = txt_AlarmInfo.Text.Trim();
            alarmModel.MemoryAddr = Convert.ToInt32(txt_MemoryAddr.Text);
            alarmModel.TagAddress = txt_x.Text.Trim();
            if (cmb_IsDisplay.SelectedIndex >= 0)
            {
                alarmModel.IsDisplay = Convert.ToInt32(cmb_IsDisplay.SelectedValue.ToString());
            }
            alarmModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
            {
                return;
            }

            bool isAccess;
            if (isAdd)
            {
                isAccess = alarmRepository.Add(alarmModel);
            }
            else
            {
                isAccess = alarmRepository.Updata(alarmModel);
            }

            if (isAccess)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(alarmModel.AlarmID))
                {
                    ClearControl();
                }
                RefreshGrid();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CmbGetdate()
        {
            string partId = "";
            cmb_DevicePartID.ValueMember = "DevicePartID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
                cmb_DevicePartID.DisplayMember = "Reserve1";
            else
                cmb_DevicePartID.DisplayMember = "DevicePartName";

            if (cmb_DeviceID.SelectedIndex >= 0)
                partId = cmb_DeviceID.SelectedValue.ToString();

            List<SYS_DevicePart> sYS_DeviceParts = devicePartRepository.GetDevicePartListByDeviceID(partId);
            cmb_DevicePartID.DataSource = sYS_DeviceParts;
        }

        private void Cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbGetdate();
        }

        private void Txt_x_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.Utils.TxtPreSetGcsU(e, true);
        }
    }
}