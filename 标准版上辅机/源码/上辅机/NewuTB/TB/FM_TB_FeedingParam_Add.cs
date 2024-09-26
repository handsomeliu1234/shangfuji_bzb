using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewuCommon;
using NewuBLL;
using Repository.GlobalConfig;

namespace NewuTB.TB
{
    public partial class FM_TB_FeedingParam_Add : Form
    {
        private string feedingId;
        private int binNo;
        private string deviceId;
        private string typeCodeId;

        private NewuBLL.SYS_DeviceBLL deviceBll = new NewuBLL.SYS_DeviceBLL();
        private NewuBLL.SYS_TypeCodeBLL typeCodeBll = new NewuBLL.SYS_TypeCodeBLL();
        private NewuBLL.TB_FeedingParamBLL feedParamBll = new NewuBLL.TB_FeedingParamBLL();
        private bool IsAdd { get; set; }

        public FM_TB_FeedingParam_Add()
        {
            InitializeComponent();
            IsAdd = true;
        }

        public FM_TB_FeedingParam_Add(string feedingId, string binNo, string deviceId, string typeCodeId)
        {
            InitializeComponent();
            this.feedingId = feedingId;
            this.binNo = int.Parse(binNo);
            this.deviceId = deviceId;
            this.typeCodeId = typeCodeId;
            IsAdd = false;
        }

        private void FM_TB_FeedingParam_Load(object sender, EventArgs e)
        {
            DataSet ds = deviceBll.GetList(0, "", "DeviceName");
            cmb_DeviceID.DataSource = ds.Tables[0];
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.SelectedIndex = -1;

            //cmb_TypeCodeID.DataSource = typeCodeBll.GetList("TypeCodeName in('Carbon','WhiteCarbon','Oil','Drug')").Tables[0];
            cmb_TypeCodeID.DataSource = typeCodeBll.GetList("TypeCodeName in('Carbon','Zon')").Tables[0];

            cmb_TypeCodeID.DisplayMember = "TypeCodeDesc";
            cmb_TypeCodeID.ValueMember = "TypeCodeID";
            cmb_TypeCodeID.SelectedIndex = -1;

            InitView();
        }

        private void InitView()
        {
            if (!IsAdd)
            {
                NewuModel.TB_FeedingParam feedingParamModel = feedParamBll.GetModel(feedingId);
                if (feedingParamModel != null)
                {
                    tbBinNo.Text = feedingParamModel.BinNo.ToString();
                    tbBinNo.Enabled = false;
                    cmb_DeviceID.SelectedValue = feedingParamModel.DeviceID;
                    cmb_TypeCodeID.SelectedValue = feedingParamModel.TypeCodeID;

                    BigFreqKuai.Text = feedingParamModel.Big_FreqKuai.ToString();
                    BigFreqZhong.Text = feedingParamModel.Big_FreqZhong.ToString();
                    BigFreqMan.Text = feedingParamModel.Big_FreqMan.ToString();
                    BigFeedKuai.Text = feedingParamModel.Big_FeedKuai.ToString();
                    BigFeedMan.Text = feedingParamModel.Big_FeedMan.ToString();

                    SmallFreqKuai.Text = feedingParamModel.Small_FreqKuai.ToString();
                    SmallFreqZhong.Text = feedingParamModel.Small_FreqZhong.ToString();
                    SmallFreqMan.Text = feedingParamModel.Small_FreqMan.ToString();
                    SmallFeedKuai.Text = feedingParamModel.Small_FeedKuai.ToString();
                    SmallFeedMan.Text = feedingParamModel.Small_FeedMan.ToString();

                    PreFeedKuaiTi.Text = feedingParamModel.Sys_FeedKuaiTi.ToString();
                    PreFeedZhongTi.Text = feedingParamModel.Sys_FeedZhongTi.ToString();
                    PreFeedManTi.Text = feedingParamModel.Sys_FeedManTi.ToString();
                }
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!DataVerification())
            {
                return;
            }

            NewuModel.TB_FeedingParam feedingParamModel = new NewuModel.TB_FeedingParam(); ;

            if (IsAdd)
            {
                feedingParamModel.BinNo = int.Parse(tbBinNo.Text);
            }
            else
            {
                feedingParamModel = feedParamBll.GetModel(feedingId);
            }

            feedingParamModel.DeviceID = cmb_DeviceID.SelectedValue.ToString();
            feedingParamModel.TypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();

            feedingParamModel.Big_FreqKuai = decimal.Parse(BigFreqKuai.Text);
            feedingParamModel.Big_FreqZhong = decimal.Parse(BigFreqZhong.Text);
            feedingParamModel.Big_FreqMan = decimal.Parse(BigFreqMan.Text);
            feedingParamModel.Big_FeedKuai = decimal.Parse(BigFeedKuai.Text);
            feedingParamModel.Big_FeedMan = decimal.Parse(BigFeedMan.Text);

            feedingParamModel.Small_FreqKuai = decimal.Parse(SmallFreqKuai.Text);
            feedingParamModel.Small_FreqZhong = decimal.Parse(SmallFreqZhong.Text);
            feedingParamModel.Small_FreqMan = decimal.Parse(SmallFreqMan.Text);
            feedingParamModel.Small_FeedKuai = decimal.Parse(SmallFeedKuai.Text);
            feedingParamModel.Small_FeedMan = decimal.Parse(SmallFeedMan.Text);

            feedingParamModel.Sys_FeedKuaiTi = decimal.Parse(PreFeedKuaiTi.Text);
            feedingParamModel.Sys_FeedZhongTi = decimal.Parse(PreFeedZhongTi.Text);
            feedingParamModel.Sys_FeedManTi = decimal.Parse(PreFeedManTi.Text);
            feedingParamModel.SaveTime = DateTime.Now;
            feedingParamModel.SaveUserID = NewuGlobal.TB_UserInfo.UserID;

            if (IsAdd)
            {
                if (feedParamBll.Exists(deviceId, typeCodeId, binNo))
                {
                    if (MessageBox.Show("该斗号参数已经设定,是否更新?", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        feedParamBll.Update(feedingParamModel);
                        RefreshGrid();
                    }
                }
                else
                {
                    if (feedParamBll.Add(feedingParamModel))
                    {
                        MessageBox.Show("设定成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshGrid();
                    }
                }
            }
            else
            {
                if (feedParamBll.Update(feedingParamModel))
                {
                    MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

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
                MessageBox.Show("储斗编号不能为空且为正整数!");
                return flag;
            }

            if (BigFreqKuai.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFreqKuai.Text) == 0)
                {
                    MessageBox.Show("大螺旋变频器快速频率必须是数值!");
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
                    MessageBox.Show("大螺旋变频器中速频率必须是数值!");
                    return flag;
                }
            }
            else
            {
                BigFreqZhong.Text = "0";
            }

            if (BigFreqMan.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFreqMan.Text) == 0)
                {
                    MessageBox.Show("大螺旋变频器慢速频率必须是数值!");
                    return flag;
                }
            }
            else
            {
                BigFreqMan.Text = "0";
            }

            if (BigFeedKuai.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFeedKuai.Text) == 0)
                {
                    MessageBox.Show("大螺旋快速加料量必须是数值!");
                    return flag;
                }
            }
            else
            {
                BigFeedKuai.Text = "0";
            }

            if (BigFeedMan.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFeedMan.Text) == 0)
                {
                    MessageBox.Show("大螺旋慢速加料量必须是数值!");
                    return flag;
                }
            }
            else
            {
                BigFeedMan.Text = "0";
            }

            if (SmallFreqKuai.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(SmallFreqKuai.Text) == 0)
                {
                    MessageBox.Show("小螺旋变频器快速频率必须是数值!");
                    return flag;
                }
            }
            else
            {
                SmallFreqKuai.Text = "0";
            }

            if (SmallFreqZhong.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(SmallFreqZhong.Text) == 0)
                {
                    MessageBox.Show("小螺旋变频器中速频率必须是数值!");
                    return flag;
                }
            }
            else
            {
                SmallFreqZhong.Text = "0";
            }

            if (SmallFreqMan.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(SmallFreqMan.Text) == 0)
                {
                    MessageBox.Show("小螺旋变频器慢速频率必须是数值!");
                    return flag;
                }
            }
            else
            {
                SmallFreqMan.Text = "0";
            }

            if (SmallFreqMan.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(SmallFreqMan.Text) == 0)
                {
                    MessageBox.Show("小螺旋变频器慢速频率必须是数值!");
                    return flag;
                }
            }
            else
            {
                SmallFreqMan.Text = "0";
            }

            if (SmallFeedKuai.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(SmallFeedKuai.Text) == 0)
                {
                    MessageBox.Show("大螺旋快速加料量必须是数值!");
                    return flag;
                }
            }
            else
            {
                SmallFeedKuai.Text = "0";
            }

            if (SmallFeedMan.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(SmallFeedMan.Text) == 0)
                {
                    MessageBox.Show("大螺旋慢速加料量必须是数值!");
                    return flag;
                }
            }
            else
            {
                SmallFeedMan.Text = "0";
            }

            if (PreFeedKuaiTi.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(PreFeedKuaiTi.Text) == 0)
                {
                    MessageBox.Show("系统预设快速称量提前时间必须是数值!");
                    return flag;
                }
            }
            else
            {
                PreFeedKuaiTi.Text = "0";
            }
            if (PreFeedZhongTi.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(PreFeedZhongTi.Text) == 0)
                {
                    MessageBox.Show("系统预设中速称量提前时间必须是数值!");
                    return flag;
                }
            }
            else
            {
                PreFeedZhongTi.Text = "0";
            }
            if (PreFeedManTi.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(PreFeedManTi.Text) == 0)
                {
                    MessageBox.Show("系统预设中速称量提前量必须是数值!");
                    return flag;
                }
            }
            else
            {
                PreFeedManTi.Text = "0";
            }

            return !flag;
        }
    }
}