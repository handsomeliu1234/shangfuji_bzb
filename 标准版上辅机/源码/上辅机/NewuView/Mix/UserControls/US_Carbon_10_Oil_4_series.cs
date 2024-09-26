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
    public partial class US_Carbon_10_Oil_4_serise : UserControl, ILanguageChanged
    {
        private readonly PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly TB_BinSettingRepository binSettingRepository = new TB_BinSettingRepository();
        private List<TB_BinSeting> carbonBinSetings;
        private List<TB_BinSeting> oilBinSetings;

        public US_Carbon_10_Oil_4_serise()
        {
            InitializeComponent();
        }

        private void US_MonitorBase_Load(object sender, EventArgs e)
        {
            SetControlLanguageText();
            //加载储罐的名字
            LoadOilName();
            LoadBinName();

            //20230809 增加用于修改要称量物料的名字颜色 李辉
            Utils.DisplayCarbonControlStyle(carbonBinSetings, this);
            Utils.DisplayOilControlStyle(oilBinSetings, this);

            //异步监控
            NewRefresh_UI();
            NewuGlobal.UserMonitor = this;
        }

        #region 异步轮训，更新UI界面

        private async void NewRefresh_UI()
        {
            await Task.Run(() => NewMonitorRefresh());
        }

        private async void NewMonitorRefresh()
        {
            while (true)
            {
                MonitorRefresh();
                await Task.Delay(300);
            }
        }

        private void MonitorRefresh()
        {
            if (this.InvokeRequired)
            {
                Action ac = () => MonitorRefresh();
                this.Invoke(ac);
                return;
            }

            if (plc_state.ConnectTionState)
            {
                DisPlayYiBiao();

                DisPlayCarbonBin();

                DisPlayCarbonScale();

                DisPlayOilScale();

                // 胶料 画面动画
                DisPlayRubberScale();

                //动作密炼机
                ViewDisplay.MixPart(MixPart1);
                ViewDisplay.MixPartF(downMixPart1);

                if (NewuGlobal.SendFlag)
                {
                    Utils.DisplayCarbonControlStyle(carbonBinSetings, this);
                    Utils.DisplayOilControlStyle(oilBinSetings, this);
                }
            }
        }

        #endregion 异步轮训，更新UI界面

        #region 加载罐名

        // 加载油料罐的名称 暂时 不填入devicrID
        public void LoadOilName()
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
                NewuGlobal.LogCat("US_MonitorBase_10").Error(ex.ToString());
            }
        }

        // 加载炭黑储罐的名称
        public void LoadBinName()
        {
            try
            {
                string typeCodeNameC = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T炭黑);
                string typeCodeIdC = NewuGlobal.GetTypeCodeIDByCodeName(typeCodeNameC);

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
                NewuGlobal.LogCat("US_MonitorBase_10").Error(ex.ToString());
            }
        }

        #endregion 加载罐名

        /// <summary>
        /// 炭黑储罐以上监控 对应点核对核对正确 西门子PLC300
        /// </summary>
        private void DisPlayCarbonBin()
        {
            try
            {
                // 炭黑 1 - 10 斗
                int startIndex = (int)MixerTransDisplay.Carbon;
                const int MAX_Posion = 7;
                for (int cnt = 1; cnt <= 10; cnt++)
                {
                    CarbonBin cb = this.Controls["carbonBin0" + cnt] as CarbonBin;
                    cb.NewuSet料位(NewuGlobal.MemDB.GetInt(startIndex + (cnt - 1) * MAX_Posion, 4));
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_10").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 炭黑 下料界面显示
        /// </summary>
        private void DisPlayCarbonScale()
        {
            try
            {
                // 加1 - 10 号炭黑 下炭管的颜色控制
                int startIndex = (int)MixerDigitalMiningCarbon.Add1;
                for (int i = 1; i <= 10; i++)
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

                for (int i = 1; i <= 10; i++)
                {
                    LuoXuan lx = this.Controls["luoXuan" + i] as LuoXuan;

                    if (lx == null)
                        break;

                    if (i <= 5)
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
                        Utils.SetPipeStyle(uc_m3, true, Color.Black);
                    }
                    else if (To && Ro)
                    {
                        Utils.SetPipeStyle(uc_m2, true, Color.Black);
                        Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                    }
                    else
                    {
                        Utils.SetPipeStyle(uc_m2, false, Color.Silver);
                        Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                    }
                }
                else
                {
                    Utils.SetPipeStyle(uc_m1, false, Color.Silver);
                    Utils.SetPipeStyle(uc_m2, false, Color.Silver);
                    Utils.SetPipeStyle(uc_m3, false, Color.Silver);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_MonitorBase_10").Error(ex.ToString());
            }
        }

        /// OJBK 所有仪表类型监控
        private void DisPlayYiBiao()
        {
            //----------------------------------------------炭黑磅秤值
            ScaleDisPlay.Scale_C(YiBiaoCarbon);
            //----------------------------------------------油料1磅秤值
            ScaleDisPlay.Scale_O(YiBiaoOil1);
            //----------------------------------------------胶料磅秤值
            ScaleDisPlay.Scale_R(YiBiaoRubber);
            //----------------------------------------------密炼机相关仪表
            ScaleDisPlay.Scale_M(YiBiaoTimeTime);

            ScaleDisPlay.Scale_M_D(YiBiaoMixFTime);
        }

        /// <summary>
        /// 油料磅秤计量称量
        /// </summary>
        private void DisPlayOilScale()
        {
            // 加1 - 4 号油料 下油管 和 泄油阀 的颜色控制
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

            //1号油秤向密炼机注油 （阀门  两个管 电机）
            bool tempBool = NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningOil.Feeding);
            MotorOil01.setImageTag(tempBool);
            faOilScaleMid.setImageTag(tempBool);
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

        /// <summary>
        /// 供胶机 胶料秤 传送带 光电开关 界面显示
        /// </summary>
        private void DisPlayRubberScale()
        {
            Send_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltPE));
            Scale_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScaleMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScalePE));

            RubberGoJiao1.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeederMotor1));
            RubberGoJiao2.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeederMotor2));
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        protected void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            YiBiaoOil1.SetTitle(NewuGlobal.GetRes("000153"));
            YiBiaoCarbon.SetTitle(NewuGlobal.GetRes("000154"));
            YiBiaoRubber.SetTitle(NewuGlobal.GetRes("000156"));
            YiBiaoTimeTime.SetTitle(NewuGlobal.GetRes("000158"));
        }
    }
}