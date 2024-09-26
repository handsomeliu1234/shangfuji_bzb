using HZH_Controls.Controls;
using MultiLanguage;
using NewuCommon;
using NewuTB.Utils;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class US_Carbon_8_Oil_1 : UserControl, ILanguageChanged
    {
        private readonly PLC_Connection_State plcConnectionState = PLC_Connection_State.GetInstance();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private List<TB_BinSeting> carbonBinSetings;
        private List<TB_BinSeting> oilBinSetings;

        public US_Carbon_8_Oil_1()
        {
            InitializeComponent();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void US_MonitorBase_8_Load(object sender, EventArgs e)
        {
            SetControlLanguageText();
            //加载储罐的名字
            LoadCarbonBinName();
            LoadOilName();

            //20230809 增加用于修改要称量物料的名字颜色 李辉
            Utils.DisplayCarbonControlStyle(carbonBinSetings, this);
            Utils.DisplayOilControlStyle(oilBinSetings, this);

            RefreshUI();
            NewuGlobal.UserMonitor = this;
        }

        private void LoadCarbonBinName()
        {
            try
            {
                string typeCodeName = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T炭黑);
                carbonBinSetings = Utils.GetTB_BinSetings(typeCodeName);
                int cnt = 1;
                foreach (var binSetting in carbonBinSetings)
                {
                    CarbonBin cb = Controls["carbonBin0" + cnt] as CarbonBin;
                    if (cb == null)
                        continue;

                    cb.NewuLabText = " " + cnt + "#" + "\n" + binSetting.MaterialCode;
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void LoadOilName()
        {
            try
            {
                string typeCodeName = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T油料);
                oilBinSetings = Utils.GetTB_BinSetings(typeCodeName);
                int cnt = 1;
                foreach (var binsetting in oilBinSetings)
                {
                    OilTank cb = Controls["oilTank0" + cnt] as OilTank;
                    if (cb == null)
                        continue;
                    cb.NewuLabText = cnt + "#" + "\n" + binsetting.MaterialCode;
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private async void RefreshUI()
        {
            try
            {
                await Task.Run(() => NewMonitorReresh());
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private async void NewMonitorReresh()
        {
            try
            {
                while (true)
                {
                    MonitorRefresh();
                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void MonitorRefresh()
        {
            try
            {
                if (InvokeRequired)
                {
                    Action ac = () => MonitorRefresh();
                    Invoke(ac);
                    return;
                }
                if (plcConnectionState.ConnectTionState)
                {
                    DisPlayYiBiao();

                    DisPlayCarbonBin();

                    DisPlayCarbonScale();

                    DisPlayOilScale();

                    //回收罐
                    DisPlayReCycCarbonBin();

                    // 胶料 画面动画
                    DisPlayRubberScale();

                    //动作密炼机
                    ViewDisplay.MixPart(MixPart1);

                    if (NewuGlobal.SendFlag)
                    {
                        Utils.DisplayCarbonControlStyle(carbonBinSetings, this);
                        Utils.DisplayOilControlStyle(oilBinSetings, this);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void DisPlayYiBiao()
        {
            try
            {
                //炭黑磅秤值
                ScaleDisPlay.Scale_C(YiBiaoCarbon);
                //炭黑中间斗磅秤值
                ScaleDisPlay.Scale_C_MID(YiBiaoCarbonMid);

                //油料磅秤值
                ScaleDisPlay.Scale_O(YiBiaoOil);

                //胶料磅秤值
                ScaleDisPlay.Scale_R(YiBiaoRubber);

                //密炼机相关仪表
                ScaleDisPlay.Scale_M(YiBiaoTimeTime);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void DisPlayCarbonBin()
        {
            try
            {
                int startIndex = (int)MixerTransDisplay.Carbon;
                int maxPosition = 7;
                for (int i = 1; i <= 8; i++)
                {
                    CarbonBin cb = Controls["carbonBin0" + i] as CarbonBin;
                    cb.NewuSet料位(NewuGlobal.MemDB.GetInt(startIndex + (i - 1) * maxPosition, 4));
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void DisPlayCarbonScale()
        {
            try
            {
                int startIndex = (int)MixerDigitalMiningCarbon.Add1;
                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        UCConduit pc = this.Controls["PipeCarbon" + i + j] as UCConduit;
                        if (pc == null)
                            break;
                        if (NewuGlobal.MemDB.Getbool(startIndex + i - 1))
                            Utils.SetPipeStyle(pc, true, Color.Black);
                        else
                            Utils.SetPipeStyle(pc, false, Color.Silver);
                    }
                }

                for (int i = 1; i <= 8; i++)
                {
                    LuoXuan lx = this.Controls["luoXuan" + i] as LuoXuan;

                    if (lx == null)
                        break;

                    if (i <= 4)
                    {
                        if (NewuGlobal.MemDB.Getbool(startIndex + i - 1))
                        {
                            lx.NewuRunState = 1;
                        }
                        else
                        {
                            lx.NewuRunState = 0;
                        }
                    }
                    else
                    {
                        if (NewuGlobal.MemDB.Getbool(startIndex + i - 1))
                        {
                            lx.NewuRunState = 3;
                        }
                        else
                        {
                            lx.NewuRunState = 2;
                        }
                    }
                }

                //炭黑称好
                CarbonScaleBin.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.WeightingOK));

                //炭黑中间斗有料
                CarbonScaleMidBin.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.MidHaveMat));

                //卸炭黑
                if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.Drop))
                {
                    FaCarbonScale.setImageTag(true);
                    Utils.SetPipeStyle(PipeCarbonScale, true, Color.Black);
                }
                else
                {
                    FaCarbonScale.setImageTag(false);
                    Utils.SetPipeStyle(PipeCarbonScale, false, Color.Silver);
                }

                //投炭黑
                bool To = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.Feeding); //投炭黑
                bool Ro = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.DischargeMisalignment);  //炭黑排错位

                if (Ro)
                {
                    lblCarbonTroubleshooting.BackColor = Color.Lime; //炭黑排错位光电
                    lblCarbonHost.BackColor = SystemColors.ControlLightLight;
                }
                else
                {
                    lblCarbonTroubleshooting.BackColor = SystemColors.ControlLightLight; //炭黑排错位光电
                    lblCarbonHost.BackColor = Color.Lime; //主机位
                }

                FaCarbonScaleMid.setImageTag(To);

                if (To)
                {
                    Utils.SetPipeStyle(uc_m1, true, Color.Black);

                    if (To && !Ro)
                    {
                        Utils.SetPipeStyle(uc_m2, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m3, true, Color.Black);
                        Utils.SetPipeStyle(uc_m4, true, Color.Black);
                    }
                    else if (To && Ro)
                    {
                        Utils.SetPipeStyle(uc_m2, true, Color.Black);
                        Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m4, false, Color.Silver);
                    }
                    else
                    {
                        Utils.SetPipeStyle(uc_m2, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m4, false, Color.Silver);
                    }
                }
                else
                {
                    Utils.SetPipeStyle(uc_m1, false, Color.Silver);
                    Utils.SetPipeStyle(uc_m2, false, Color.Silver);
                    Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                    Utils.SetPipeStyle(uc_m4, false, Color.Silver);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void DisPlayOilScale()
        {
            try
            {
                // 加1 - 2 号油料 下油管 和 泄油阀 的颜色控制
                int startIndex = (int)MixerDigitalMiningOil.Add1;
                for (int i = 1; i <= 1; i++)
                {
                    NewuPicAngle fa = this.Controls["FaOil0" + i] as NewuPicAngle;
                    if (fa == null)
                        continue;

                    fa.setImageTag(NewuGlobal.MemDB.Getbool(startIndex + i - 1));

                    for (int j = 1; j <= 1; j++)
                    {
                        UCConduit pi = this.Controls["uc_O" + i + j] as UCConduit;
                        if (pi == null)
                            break;
                        if (NewuGlobal.MemDB.Getbool(startIndex + i - 1))
                            Utils.SetPipeStyle(pi, true, Color.Brown);
                        else
                            Utils.SetPipeStyle(pi, false, Color.Silver);
                    }
                }

                //1号油秤，称好
                OilScaleBin01.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningOil.WeightingOK));

                //1号油秤中间斗，油料到位
                OilScaleMidBin01.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningOil.MidHaveMat));

                //1号油秤向中间斗卸油 （阀门，管）
                bool xie = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningOil.Drop);
                faOilScale.setImageTag(xie);
                if (xie)
                    Utils.SetPipeStyle(uc_O1, true, Color.Brown);
                else
                    Utils.SetPipeStyle(uc_O1, false, Color.Silver);

                bool tempBool = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningOil.Feeding);
                faOilScaleMid.setImageTag(tempBool);
                MotorOil01.setImageTag(tempBool);
                if (tempBool)
                {
                    Utils.SetPipeStyle(uc_O2, true, Color.Brown);
                    Utils.SetPipeStyle(uc_O3, true, Color.Brown);
                }
                else
                {
                    Utils.SetPipeStyle(uc_O2, false, Color.Silver);
                    Utils.SetPipeStyle(uc_O3, false, Color.Silver);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void DisPlayReCycCarbonBin()
        {
            try
            {
                bool result = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.Recovery1);
                if (result)
                {
                    Utils.SetPipeStyle(ReCycPipe11, true, Color.Black);
                    Utils.SetPipeStyle(ReCycPipe12, true, Color.Black);
                    ReCycluoXuan1.NewuRunState = 1;
                }
                else
                {
                    Utils.SetPipeStyle(ReCycPipe11, false, Color.Silver);
                    Utils.SetPipeStyle(ReCycPipe12, false, Color.Silver);
                    ReCycluoXuan1.NewuRunState = 1;
                }

                // 回收斗
                ReCycBin1.NewuSet料位(NewuGlobal.MemDB.GetInt((int)MixerTransDisplay.CarbonRecovery, 4));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void DisPlayRubberScale()
        {
            try
            {
                Send_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltPE));
                Scale_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScaleMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScalePE));
                RubberGoJiao1.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeederMotor1));
                RubberGoJiao2.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeederMotor2));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Carbon_8_Oill_1").Error(ex.ToString());
            }
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            YiBiaoOil.SetTitle(NewuGlobal.GetRes("000153"));
            YiBiaoCarbon.SetTitle(NewuGlobal.GetRes("000154"));
            YiBiaoCarbonMid.SetTitle(NewuGlobal.GetRes("000155"));
            YiBiaoRubber.SetTitle(NewuGlobal.GetRes("000156"));
            YiBiaoTimeTime.SetTitle(NewuGlobal.GetRes("000158"));
        }
    }
}