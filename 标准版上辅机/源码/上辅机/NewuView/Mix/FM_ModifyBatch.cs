using Newu;
using NewuCommon;
using NewuTB.Utils;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class FM_ModifyBatch : Form
    {
        private CSharedString ss = NewuGlobal.MemDB;
        private TB_OperateLogRepository operateLogRepository = new TB_OperateLogRepository();
        private PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();

        public FM_ModifyBatch()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FM_ModifyBatch_Load(object sender, EventArgs e)
        {
            RefreshUI();
            SetControlLanguageText();
        }

        /// <summary>
        /// 初始化时获取当前的 设定车数 和 实际车数
        /// </summary>
        private void RefreshUI()
        {
            if (NewuGlobal.SoftConfig.IsFinalMix())
            {
                label1.Visible = label4.Visible = label3.Visible = label5.Visible = input_C.Visible = input_P.Visible = input_F.Visible = input_O.Visible = input_S.Visible = set_C.Visible = set_P.Visible = set_F.Visible = set_O.Visible = set_S.Visible = false;
            }
            // 炭 油 胶 药 粉 密
            input_C.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Carbon, 4).ToString();
            input_F.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Zno, 4).ToString();
            input_O.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Oil, 4).ToString();
            input_R.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Rubber, 4).ToString();
            input_D.Text = ss.GetInt((int)MixerAnalogMiningActBatch.DrugMixer, 4).ToString();
            input_P.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Plasticizer, 4).ToString();
            input_M.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4).ToString();
            input_S.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Silane, 4).ToString();

            set_C.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Carbon, 4);
            set_F.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Zno, 4);
            set_O.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Oil, 4);
            set_R.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Rubber, 4);
            set_D.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.DrugMixer, 4);
            set_P.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Plasticizer, 4);
            set_M.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4);
            set_S.Text = NewuGlobal.GetRes("000481") + ": " + ss.GetInt((int)MixerAnalogMiningSetBatch.Silane, 4);

            set_Car.Text = ss.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4).ToString();
        }

        //修改设定车数
        private void Bt_change_set_Click(object sender, EventArgs e)
        {
            int Addr = (int)MixerAnalogMiningSetBatch.Carbon;
            int preCarNum = ss.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4);
            string changeCarNum = set_Car.Text.ToString().PadLeft(4, '0');
            StringBuilder memSttr = new StringBuilder();
            if (NewuGlobal.SoftConfig.Carbon)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            if (NewuGlobal.SoftConfig.Carbon2)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            if (NewuGlobal.SoftConfig.Oil)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            if (NewuGlobal.SoftConfig.Oil2)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            if (NewuGlobal.SoftConfig.Zno)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            if (NewuGlobal.SoftConfig.Zno2)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            memSttr.Append(changeCarNum);

            if (NewuGlobal.SoftConfig.Drug)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            if (NewuGlobal.SoftConfig.Silane)
                memSttr.Append(changeCarNum);
            else
                memSttr.Append("0000");

            memSttr.Append(changeCarNum);//密炼设定车数

            bool flag = NewuGlobal.MemMgr.Sync(Addr, memSttr.ToString());

            PM_OrderTran pM_OrderTran = orderTranRepository.GetModel(NewuGlobal.Now_OrderID);
            pM_OrderTran.SetBatch = int.Parse(changeCarNum);
            bool result = orderTranRepository.Update(pM_OrderTran);

            if (flag && result)
            {
                labMsg.Text = NewuGlobal.GetRes("000171");

                TB_OperateLog tB_OperateLog = new TB_OperateLog
                {
                    UserID = NewuGlobal.TB_UserInfo.UserCode,
                    SaveTime = DateTime.Now,
                    DeviceID = NewuGlobal.SoftConfig.DeviceID,
                    LogInfo = string.Format("{0}：{1}： {2} {3}：{4}", NewuGlobal.Now_OrderName, NewuGlobal.GetRes("000126"), preCarNum, NewuGlobal.GetRes("000576"), int.Parse(changeCarNum)),
                    LogType = AppLogType.Update.ToString()
                };
                operateLogRepository.Add(tB_OperateLog);
            }
            else
            {
                labMsg.Text = NewuGlobal.GetRes("000172");
            }
            Thread.Sleep(1000);
            RefreshUI();
        }

        private void Bt_change_realy_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                string name = bt_mix.Name;
                string devicepart = "";
                int actBatch = 0;
                string newActBatch = "";
                bool result = false;
                if (button.Name.Equals(bt_mix.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Mixer, input_M.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000696");
                    newActBatch = input_M.Text.ToString();
                }
                else if (button.Name.Equals(bt_rubber.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Rubber, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Rubber, input_R.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000693");
                    newActBatch = input_R.Text.ToString();
                }
                else if (button.Name.Equals(bt_drug.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.DrugMixer, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.DrugMixer, input_D.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000707");
                    newActBatch = input_D.Text.ToString();
                }
                else if (button.Name.Equals(bt_carbon.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Carbon, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Carbon, input_C.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000708");
                    newActBatch = input_C.Text.ToString();
                }
                else if (button.Name.Equals(bt_oil.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Oil, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Oil, input_O.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000710");
                    newActBatch = input_O.Text.ToString();
                }
                else if (button.Name.Equals(bt_zno.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Zno, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Zno, input_F.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000712");
                    newActBatch = input_F.Text.ToString();
                }
                else if (button.Name.Equals(bt_si.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Silane, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Silane, input_S.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000711");
                    newActBatch = input_S.Text.ToString();
                }
                else if (button.Name.Equals(bt_pla.Name))
                {
                    actBatch = ss.GetInt((int)MixerAnalogMiningActBatch.Plasticizer, 4);
                    result = NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningActBatch.Plasticizer, input_P.Text.PadLeft(4, '0'));
                    devicepart = NewuGlobal.GetRes("000709");
                    newActBatch = input_P.Text.ToString();
                }

                TB_OperateLog tB_OperateLog = new TB_OperateLog
                {
                    UserID = NewuGlobal.TB_UserInfo.UserCode,
                    SaveTime = DateTime.Now,
                    DeviceID = NewuGlobal.SoftConfig.DeviceID,
                    LogInfo = string.Format("{0}：{1} {2}：{3}  {4}：{5}", NewuGlobal.Now_OrderName, devicepart, NewuGlobal.GetRes("000370"), actBatch, NewuGlobal.GetRes("000576"), newActBatch),
                    LogType = AppLogType.Update.ToString()
                };
                operateLogRepository.Add(tB_OperateLog);

                if (result)
                    labMsg.Text = NewuGlobal.GetRes("000171");
                else
                    labMsg.Text = NewuGlobal.GetRes("000172");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_ModifyBatch").Error(ex.ToString());
            }
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.TxtPreSetGcsU(e, false);
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/

            this.Text = NewuGlobal.GetRes("000564"); // 生产车数修改

            groupBox2.Text = NewuGlobal.GetRes("000565");  //设定车次修改
            groupBox1.Text = NewuGlobal.GetRes("000566");  //实际车次修改

            label2.Text = NewuGlobal.GetRes("000567") + ":";//*计划生产设定车次
            label6.Text = NewuGlobal.GetRes("000574") + ":";// *胶料车次
            label8.Text = NewuGlobal.GetRes("000571") + ":";// *药品车次
            label1.Text = NewuGlobal.GetRes("000568") + ":";// *炭黑车次
            label4.Text = NewuGlobal.GetRes("000570") + ":";// *油料车次
            label3.Text = NewuGlobal.GetRes("000569") + ":";// *粉料车次
            label5.Text = NewuGlobal.GetRes("000573") + ":";// *硅烷车次
            label11.Text = NewuGlobal.GetRes("000575") + ":";// *密炼车次
            label10.Text = NewuGlobal.GetRes("000572") + ":";// *塑解剂车次

            labMsg.Text = NewuGlobal.GetRes("000170");
            bt_change_set.Text = NewuGlobal.GetRes("000576"); //确认修改

            bt_carbon.Text = bt_rubber.Text = bt_mix.Text = bt_drug.Text = bt_oil.Text = bt_pla.Text = bt_si.Text = bt_zno.Text = NewuGlobal.GetRes("000576"); //确认修改

            btnClose.Text = NewuGlobal.GetRes("000103"); //关闭
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                bt_carbon.Padding = bt_rubber.Padding = bt_mix.Padding = bt_drug.Padding = bt_oil.Padding = bt_pla.Padding = bt_si.Padding = bt_zno.Padding = new Padding(10, 0, 10, 0);

                bt_change_set.Padding = new Padding(10, 0, 10, 0);

                btnClose.Padding = new Padding(10, 0, 10, 0);
            }
            else
            {
                bt_carbon.Padding = bt_rubber.Padding = bt_mix.Padding = bt_drug.Padding = bt_oil.Padding = bt_pla.Padding = bt_si.Padding = bt_zno.Padding = new Padding(5, 0, 0, 0);
                bt_change_set.Padding = new Padding(5, 0, 0, 0);
                btnClose.Padding = new Padding(5, 0, 10, 0);
            }
        }
    }
}