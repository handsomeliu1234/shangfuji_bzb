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
    public partial class US_Carbon_8_Oil_4_Zno_2 : UserControl, ILanguageChanged
    {
        private readonly PLC_Connection_State plcConnectionState = PLC_Connection_State.GetInstance();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly TB_BinSettingRepository binSettingRepository = new TB_BinSettingRepository();
        private List<TB_BinSeting> carbonBinSetings;
        private List<TB_BinSeting> oilBinSetings;
        private List<TB_BinSeting> siBinSetings;
        private List<TB_BinSeting> znoBinSetings;

        public US_Carbon_8_Oil_4_Zno_2()
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
            LoadSilName();
            LoadZnOName();
            //20230809 增加用于修改要称量物料的名字颜色 李辉
            Utils.DisplayCarbonControlStyle(carbonBinSetings, this);
            Utils.DisplayOilControlStyle(oilBinSetings, this);
            Utils.DisplayZnoControlStyle(znoBinSetings, this);
            Utils.DisplaySiControlStyle(siBinSetings, this);

            RefreshUI();
            NewuGlobal.UserMonitor = this;
        }

        private void LoadCarbonBinName()
        {
            try
            {
                string typeCodeName = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T炭黑);
                string typeCodeIdC = NewuGlobal.GetTypeCodeIDByCodeName(typeCodeName);

                string typeCodeNameWC = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T白炭黑);
                string typeCodeIdWC = NewuGlobal.GetTypeCodeIDByCodeName(typeCodeNameWC);

                carbonBinSetings = binSettingRepository.GetListJoinMaterialCodeIn("", typeCodeIdC, typeCodeIdWC);

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
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void LoadSilName()
        {
            try
            {
                string typeCodeName = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T硅烷);
                siBinSetings = Utils.GetTB_BinSetings(typeCodeName);
                int cnt = 1;
                foreach (var binsetting in siBinSetings)
                {
                    OilTank cb = Controls["SilTank0" + cnt] as OilTank;
                    if (cb == null)
                        continue;
                    cb.NewuLabText = " " + cnt + "#" + "\n" + binsetting.MaterialCode;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void LoadZnOName()
        {
            try
            {
                string typeCodeName = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T粉料);
                znoBinSetings = Utils.GetTB_BinSetings(typeCodeName);
                int cnt = 1;
                foreach (var binSetting in znoBinSetings)
                {
                    CarbonBin cb = Controls["znoBin0" + cnt] as CarbonBin;
                    if (cb == null)
                        continue;

                    cb.NewuLabText = " " + cnt + "#" + "\n" + binSetting.MaterialCode;
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private async void NewMonitorReresh()
        {
            try
            {
                while (true)
                {
                    MonitorRefresh();
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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

                    DisPlaySilScale();

                    DisPlayZnoScale();

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
                        Utils.DisplayZnoControlStyle(znoBinSetings, this);
                        Utils.DisplaySiControlStyle(siBinSetings, this);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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

                //硅烷磅秤值
                ScaleDisPlay.Scale_S(YiBiaoSil);

                //小药秤值
                ScaleDisPlay.Scale_D(YiBiaoDrug);

                //粉料磅秤值
                ScaleDisPlay.Scale_Z(YiBiaoZno);
                //粉料中间斗磅秤值
                ScaleDisPlay.Scale_Z_MID(YiBiaoZnoMid);

                //胶料磅秤值
                ScaleDisPlay.Scale_R(YiBiaoRubber);

                //塑解剂秤值
                ScaleDisPlay.DeatailScale_P(YiBiaoPlasticizer);

                //密炼机相关仪表
                ScaleDisPlay.Scale_M(YiBiaoTimeTime);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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
                CarbonScaleMidBin.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.DischargeMisalignment));

                //卸炭黑
                if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.Drop))
                {
                    FaCarbonScale.setImageTag(true);
                    Utils.SetPipeStyle(PipeCarbonScale, true, Color.Black);
                }
                else
                {
                    FaCarbonScale.setImageTag(false);
                    Utils.SetPipeStyle(PipeCarbonScale, false, Color.Black);
                }

                //投炭黑
                bool To = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.Feeding); //投炭黑
                bool Ro = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningCarbon.DischargeMisalignment);  //炭黑排错位
                FaZnoScaleMid.setImageTag(To);
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

                if (To)
                {
                    Utils.SetPipeStyle(uc_m1, true, Color.Black);

                    if (To && !Ro)
                    {
                        Utils.SetPipeStyle(uc_m2, true, Color.Black);
                        Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m4, false, Color.Silver);
                    }
                    else if (To && Ro)
                    {
                        Utils.SetPipeStyle(uc_m2, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m3, true, Color.Black);
                        Utils.SetPipeStyle(uc_m4, true, Color.Black);
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
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void DisPlayOilScale()
        {
            try
            {
                // 加1 - 2 号油料 下油管 和 泄油阀 的颜色控制
                int startIndex = (int)MixerDigitalMiningOil.Add1;
                for (int i = 1; i <= 4; i++)
                {
                    NewuPicAngle fa = this.Controls["FaOil0" + i] as NewuPicAngle;
                    if (fa == null)
                        continue;

                    fa.setImageTag(NewuGlobal.MemDB.Getbool(startIndex + i - 1));

                    for (int j = 1; j <= 3; j++)
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
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void DisPlaySilScale()
        {
            try
            {
                bool siAdd = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningSilane.Add);
                faSil01.setImageTag(siAdd);
                if (siAdd)
                    Utils.SetPipeStyle(PipeSil11, true, Color.White);
                else
                    Utils.SetPipeStyle(PipeSil11, false, Color.Silver);

                bool siInMix = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningSilane.Feeding);
                MotorSil.setImageTag(siInMix);
                if (siInMix)
                {
                    Utils.SetPipeStyle(PipeSilScaleMid11, true, Color.White);
                    Utils.SetPipeStyle(PipeSilScaleMid12, true, Color.White);
                }
                else
                {
                    Utils.SetPipeStyle(PipeSilScaleMid11, false, Color.Silver);
                    Utils.SetPipeStyle(PipeSilScaleMid12, false, Color.Silver);
                }

                //硅烷称称好
                SilScaleBin01.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningSilane.WeightingOK));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void DisPlayZnoScale()
        {
            try
            {
                int startIndex = (int)MixerTransDisplay.Zno;
                int maxPosition = 5;
                for (int i = 1; i <= 2; i++)
                {
                    CarbonBin cb = Controls["znoBin0" + i] as CarbonBin;
                    cb.NewuSet料位(NewuGlobal.MemDB.GetInt(startIndex + (i - 1) * maxPosition, 4));
                }

                int startPos = (int)MixerDigitalMiningZno.Add1;

                for (int i = 1; i <= 2; i++)
                {
                    for (int j = 1; j <= 1; j++)
                    {
                        UCConduit pc = this.Controls["PipeZno" + i + j] as UCConduit;
                        if (pc == null)
                            break;
                        if (NewuGlobal.MemDB.Getbool(startPos + i - 1))
                            Utils.SetPipeStyle(pc, true, Color.Chocolate);
                        else
                            Utils.SetPipeStyle(pc, true, Color.Silver);
                    }
                }

                for (int i = 9; i <= 10; i++)
                {
                    LuoXuan lx = this.Controls["luoXuan" + i] as LuoXuan;

                    if (lx == null)
                        break;

                    if (i <= 9)
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

                //粉料称好
                ZnoScaleBin.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningZno.WeightingOK));
                //中间斗
                ZnoScaleMidBin.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningZno.MidHaveMat));

                bool xieZno = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningZno.Drop);//卸粉料
                FaZnoScale.setImageTag(xieZno);
                if (xieZno)
                    Utils.SetPipeStyle(PipeZnoScale, true, Color.Chocolate);
                else
                    Utils.SetPipeStyle(PipeZnoScale, true, Color.Silver);

                bool To = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningZno.Feeding); //投粉料
                bool Ro = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningZno.DischargeMisalignment);  //粉料排错位
                FaZnoScaleMid.setImageTag(To);

                if (Ro)
                {
                    lblZnoTroubleshooting.BackColor = Color.Lime; //粉料排错位光电
                    lblZnoHost.BackColor = SystemColors.ControlLightLight;
                }
                else
                {
                    lblZnoTroubleshooting.BackColor = SystemColors.ControlLightLight; //粉料排错位光电
                    lblZnoHost.BackColor = Color.Lime; //主机位
                }

                if (To)
                {
                    Utils.SetPipeStyle(PipeZnoScaleMid01, true, Color.Chocolate);
                    if (!Ro)
                    {
                        Utils.SetPipeStyle(PipeZnoScaleMid02, true, Color.Chocolate);
                        Utils.SetPipeStyle(PipeZnoScaleMid03, false, Color.Silver);
                        Utils.SetPipeStyle(PipeZnoScaleMid04, false, Color.Silver);
                    }
                    else
                    {
                        Utils.SetPipeStyle(PipeZnoScaleMid03, true, Color.Chocolate);
                        Utils.SetPipeStyle(PipeZnoScaleMid04, true, Color.Chocolate);
                        Utils.SetPipeStyle(PipeZnoScaleMid02, false, Color.Silver);
                    }
                }
                else
                {
                    Utils.SetPipeStyle(PipeZnoScaleMid01, false, Color.Silver);
                    Utils.SetPipeStyle(PipeZnoScaleMid02, false, Color.Silver);
                    Utils.SetPipeStyle(PipeZnoScaleMid03, false, Color.Silver);
                    Utils.SetPipeStyle(PipeZnoScaleMid04, false, Color.Silver);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
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
                    Utils.SetPipeStyle(ReCycPipe11, true, Color.Silver);
                    Utils.SetPipeStyle(ReCycPipe12, true, Color.Silver);
                    ReCycluoXuan1.NewuRunState = 0;
                }

                // 回收斗
                ReCycBin1.NewuSet料位(NewuGlobal.MemDB.GetInt((int)MixerTransDisplay.CarbonRecovery, 4));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void DisPlayRubberScale()
        {
            try
            {
                Send_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltPE));
                Scale_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScaleMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScalePE));

                Scale_Drug.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningDrug.Motor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningDrug.Photoelectricity));
                Scale_Pla.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningPlasticizer.Motor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningPlasticizer.Photoelectricity));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_8").Error(ex.ToString());
            }
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            YiBiaoOil.SetTitle(NewuGlobal.GetRes("000153"));
            YiBiaoSil.SetTitle(NewuGlobal.GetRes("000725"));
            YiBiaoCarbon.SetTitle(NewuGlobal.GetRes("000154"));
            YiBiaoCarbonMid.SetTitle(NewuGlobal.GetRes("000155"));
            YiBiaoDrug.SetTitle(NewuGlobal.GetRes("000724"));
            YiBiaoZno.SetTitle(NewuGlobal.GetRes("000726"));
            YiBiaoZnoMid.SetTitle(NewuGlobal.GetRes("000730"));
            YiBiaoRubber.SetTitle(NewuGlobal.GetRes("000156"));
            YiBiaoTimeTime.SetTitle(NewuGlobal.GetRes("000158"));
            YiBiaoPlasticizer.SetTitle(NewuGlobal.GetRes("000729"));
        }
    }
}