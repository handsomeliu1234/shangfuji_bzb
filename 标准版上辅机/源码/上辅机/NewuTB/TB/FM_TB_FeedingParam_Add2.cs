using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_FeedingParam_Add2 : Form
    {
        private string feedingId;
        private int binNo;
        private string deviceId;
        private string typeCodeId;

        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly TB_FeedingParamRepository feedingParamRepository = new TB_FeedingParamRepository();
        private bool isAdd;

        public FM_TB_FeedingParam_Add2()
        {
            InitializeComponent();
            isAdd = true;
            SetControlLanguageText();
        }

        public FM_TB_FeedingParam_Add2(string feedingId, string binNo, string deviceId, string typeCodeId)
        {
            InitializeComponent();
            this.feedingId = feedingId;
            this.binNo = int.Parse(binNo);
            this.deviceId = deviceId;
            this.typeCodeId = typeCodeId;
            isAdd = false;
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            this.Text = NewuGlobal.GetRes("000457"); //加料参数设定

            label6.Text = NewuGlobal.GetRes("000464") + ":";// *储斗编号
            label2.Text = NewuGlobal.GetRes("000467") + ":";// *螺旋快速
            label1.Text = NewuGlobal.GetRes("000468") + ":";// *螺旋低速

            label10.Text = NewuGlobal.GetRes("000460") + ":";// *所属设备
            labEquipmentTyp.Text = NewuGlobal.GetRes("000466") + ":";// *类型编码
            /***********  常见按钮   ***********/

            btClose.Text = NewuGlobal.GetRes("000103"); //关闭
            btSave.Text = NewuGlobal.GetRes("000108"); //保存
            if (NewuGlobal.SupportLanguage.Equals("1"))
                btClose.Padding = new Padding(0, 0, 7, 0);
            else
                btClose.Padding = new Padding(0, 0, 0, 0);

            lab_BinNoVeri.Text = label4.Text = label3.Text = NewuGlobal.GetRes("000167");
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            if (!DataVerification())
            {
                return;
            }

            TB_FeedingParam feedingParamModel = new TB_FeedingParam();

            if (isAdd)
            {
                feedingParamModel.BinNo = int.Parse(tbBinNo.Text);
                binNo = int.Parse(tbBinNo.Text);
                deviceId = cmb_DeviceID.SelectedValue.ToString();
                typeCodeId = cmb_TypeCodeID.SelectedValue.ToString();
            }
            else
            {
                feedingParamModel = feedingParamRepository.GetModel(feedingId);
            }

            feedingParamModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            feedingParamModel.TypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();

            feedingParamModel.Big_FreqKuai = decimal.Parse(BigFreqKuai.Text);
            feedingParamModel.Big_FreqZhong = decimal.Parse(BigFreqZhong.Text);
            feedingParamModel.SaveTime = DateTime.Now;
            feedingParamModel.SaveUserID = NewuGlobal.TB_UserInfo.UserID;

            if (isAdd)
            {
                if (feedingParamRepository.Exists(deviceId, typeCodeId, binNo))
                {   //"该斗号参数已经设定,是否更新?", "警告"
                    if (MessageBox.Show(NewuGlobal.GetRes("000177"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        feedingParamRepository.Update(feedingParamModel);
                        RefreshGrid();
                    }
                }
                else
                {
                    if (feedingParamRepository.Add(feedingParamModel))
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"设定成功！"
                        RefreshGrid();
                    }
                }
            }
            else
            {
                if (feedingParamRepository.Update(feedingParamModel))
                {
                    MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"更新成功！"
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"更新失败！"
                }
            }
        }

        private void BtClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FM_TB_FeedingParam_Add2_Load(object sender, EventArgs e)
        {
            List<SYS_Device> sYS_Devices = deviceRepository.GetList(0, "", "DeviceName");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.SelectedIndex = 0;

            cmb_TypeCodeID.DataSource = typeCodeRepository.GetList("TypeCodeName in('Carbon','Zno')");

            if (NewuGlobal.SupportLanguage.Equals("1"))
                cmb_TypeCodeID.DisplayMember = "TypeCodeDesc";
            else
                cmb_TypeCodeID.DisplayMember = "TypeCodeName";

            cmb_TypeCodeID.ValueMember = "TypeCodeID";
            cmb_TypeCodeID.SelectedIndex = 0;

            InitView();
        }

        private void InitView()
        {
            if (!isAdd)
            {
                TB_FeedingParam feedingParamModel = feedingParamRepository.GetModel(feedingId);
                if (feedingParamModel != null)
                {
                    tbBinNo.Text = feedingParamModel.BinNo.ToString();
                    tbBinNo.Enabled = false;
                    cmb_DeviceID.SelectedValue = feedingParamModel.DeviceID;
                    cmb_TypeCodeID.SelectedValue = feedingParamModel.TypeCodeID;

                    BigFreqKuai.Text = feedingParamModel.Big_FreqKuai.ToString();
                    BigFreqZhong.Text = feedingParamModel.Big_FreqZhong.ToString();
                }
            }
        }

        private void RefreshGrid()
        {
            object obj = Owner;

            if (obj != null)
            {
                FM_TB_FeedingParam fm = obj as FM_TB_FeedingParam;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            bool flag = false;
            if (FunClass.IsEmptyOrNumber(tbBinNo.Text) == 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000404"));//"储斗编号不能为空且为正整数!"
                return flag;
            }

            if (BigFreqKuai.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFreqKuai.Text) == 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000404"));//"螺旋变频器快速必须是数值!"
                    return flag;
                }
            }
            else
            {
                BigFreqKuai.Text = "0";
            }

            if (BigFreqZhong.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFreqZhong.Text) == 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000404"));//"螺旋变频器快速必须是数值!"
                    return flag;
                }
            }
            else
            {
                BigFreqZhong.Text = "0";
            }

            return !flag;
        }
    }
}