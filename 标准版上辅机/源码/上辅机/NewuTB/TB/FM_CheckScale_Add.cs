using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// </summary>
namespace NewuTB.TB
{
    public partial class FM_CheckScale_Add : Form
    {
        private ScaleCheckSet scaleCheckSet;
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();

        public FM_CheckScale_Add()
        {
            InitializeComponent();
        }

        public FM_CheckScale_Add(ScaleCheckSet m)
        {
            InitializeComponent();
            scaleCheckSet = m;
        }

        private void FM_CheckScale_Add_Load(object sender, EventArgs e)
        {
            SetLanguage();
            List<SYS_Device> sYS_Devices = deviceRepository.GetList(0, "", "DeviceName");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            List<SYS_DevicePart> sYS_DeviceParts = devicePartRepository.GetDevicePartList();
            cb_ScaleType.DataSource = sYS_DeviceParts;
            cb_ScaleType.ValueMember = "DevicePartCode";
            if (NewuGlobal.SoftConfig.Language.Equals("Chinese"))
                cb_ScaleType.DisplayMember = "Reserve1";
            else
                cb_ScaleType.DisplayMember = "DevicePartName";

            string weightSet = NewuGlobal.SoftConfig.WeightSet;
            string[] weightList = weightSet.Split('/');
            decimal[] dWeightList = weightList.Select(Convert.ToDecimal).ToArray();

            string allowError = NewuGlobal.SoftConfig.AllowError;
            string[] allowErrorList = allowError.Split('/');
            decimal[] dAllowErrorList = allowErrorList.Select(Convert.ToDecimal).ToArray();

            Array.Sort(dWeightList);
            Array.Sort(dAllowErrorList);

            cb_StandradWeight.DataSource = dWeightList;
            cb_AllowErr.DataSource = dAllowErrorList;

            if (scaleCheckSet != null)
            {
                cmb_DeviceID.SelectedValue = NewuGlobal.DeviceIDByCode(scaleCheckSet.DeviceCode);

                cb_ScaleType.SelectedValue = scaleCheckSet.DevicePartCode;

                cb_StandradWeight.SelectedItem = scaleCheckSet.ScaleWeight;

                cb_AllowErr.SelectedItem = scaleCheckSet.SetError;

                tbCheckScaleNo.Text = scaleCheckSet.CheckScaleNo.ToString();
                tbScaleName.Text = scaleCheckSet.ScaleName;
            }
        }

        private void SetLanguage()
        {
            label1.Text = NewuGlobal.GetRes("000182") + ":";
            label2.Text = NewuGlobal.GetRes("000115") + ":";
            label3.Text = NewuGlobal.GetRes("000114") + ":";
            label4.Text = NewuGlobal.GetRes("000116") + ":";
            label5.Text = NewuGlobal.GetRes("000117") + ":";
            label6.Text = NewuGlobal.GetRes("000118") + ":";
            label7.Text = NewuGlobal.GetRes("000162");
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnSave.Text = NewuGlobal.GetRes("000108");
            if (NewuGlobal.SoftConfig.Language.Equals("Chinese"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// 保存校秤数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            ScaleCheckSetRepository repo = new ScaleCheckSetRepository();
            bool isAdd = false;
            if (scaleCheckSet == null) //新增
            {
                isAdd = true;
                scaleCheckSet = new ScaleCheckSet
                {
                    ID = Guid.NewGuid().ToString()
                };
            }
            if (cmb_DeviceID.SelectedValue != null)
            {
                scaleCheckSet.DeviceCode = NewuGlobal.DeviceCodeByID(cmb_DeviceID.SelectedValue as string);
            }

            if (cb_ScaleType.SelectedValue != null)
            {
                scaleCheckSet.DevicePartCode = cb_ScaleType.SelectedValue as string;
                scaleCheckSet.DevicePartName = cb_ScaleType.Text;
            }

            if (cb_StandradWeight.SelectedValue != null)
            {
                scaleCheckSet.ScaleWeight = cb_StandradWeight.SelectedValue.ToString();
            }

            if (cb_AllowErr.SelectedValue != null)
            {
                scaleCheckSet.SetError = cb_AllowErr.SelectedValue.ToString();
            }
            if (tbCheckScaleNo.Text != null)
            {
                scaleCheckSet.CheckScaleNo = NewuCommon.FunClass.VVal(tbCheckScaleNo.Text);
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000114") + NewuGlobal.GetRes("000162"));
                return;
            }

            if (tbScaleName.Text != null)
            {
                scaleCheckSet.ScaleName = tbScaleName.Text;
            }

            scaleCheckSet.SaveTime = DateTime.Now;
            scaleCheckSet.SaveUser = NewuGlobal.TB_UserInfo.UserCode;
            bool result;
            if (isAdd) //新增
            {
                result = repo.Insert(scaleCheckSet);
            }
            else
            {
                result = repo.Update(scaleCheckSet);
            }

            if (result)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"));
            }
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}