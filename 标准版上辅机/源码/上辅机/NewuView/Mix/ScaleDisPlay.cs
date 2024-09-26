using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public static class ScaleDisPlay
    {
        private static CSharedString SS = NewuGlobal.MemDB;
        private static List<FormulaWeigh> rawList;

        public static void Init(CSharedString ss)
        {
            SS = ss;
        }

        #region 炭 粉 油 药 胶 监控界面仪表数据

        /// <summary>
        ///炭黑秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_C(Scale sc)
        {
            //炭黑磅秤值
            double carbonVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Carbon, 4), NewuGlobal.SoftConfig.CarbonDigit);
            if (SS.Getbool((int)MixerDigitalMiningCarbon.Sign))
            {
                sc.NewuScaleValue = "-" + carbonVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = carbonVal.ToString();
            }
            if (SS.Getbool((int)MixerDigitalMiningCarbon.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }

            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Carbon, 4).ToString();  //设定车数
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningCarbon.Auto);  //是否自动
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningCarbon.OverTolerance);  //超差
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningCarbon.WeightingStart); //炭黑开始称量
        }

        /// <summary>
        ///炭黑中间 秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_C_MID(NewuYiBiao sc)
        {
            double carbonMidVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.CarbonMid, 4), NewuGlobal.SoftConfig.CarbonDigit);
            if (SS.Getbool((int)MixerDigitalMiningCarbon.MidSign))
            {
                sc.NewuYiBiaoValue = "-" + carbonMidVal.ToString();
            }
            else
            {
                sc.NewuYiBiaoValue = carbonMidVal.ToString();
            }
        }

        /// <summary>
        ///粉料 秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_Z(Scale sc)
        {
            double znoVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Zno, 4), NewuGlobal.SoftConfig.ZnoDigit);
            if (SS.Getbool((int)MixerDigitalMiningZno.Sign))
            {
                sc.NewuScaleValue = "-" + znoVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = znoVal.ToString();
            }
            if (SS.Getbool((int)MixerDigitalMiningZno.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Zno, 4).ToString();
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningZno.Auto);
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningZno.OverTolerance);
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningZno.WeightingStart);
        }

        /// <summary>
        ///粉料1中间 秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_Z_MID(NewuYiBiao sc)
        {
            double znoMidVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.ZnoMid, 4), NewuGlobal.SoftConfig.ZnoDigit);
            if (SS.Getbool((int)MixerDigitalMiningZno.MidSign))
            {
                sc.NewuYiBiaoValue = "-" + znoMidVal.ToString();
            }
            else
            {
                sc.NewuYiBiaoValue = znoMidVal.ToString();
            }
        }

        /// <summary>
        ///油料1秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_O(Scale sc)
        {
            double oilVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Oil, 4), NewuGlobal.SoftConfig.OilDigit);
            if (SS.Getbool((int)MixerDigitalMiningOil.Sign))
            {
                sc.NewuScaleValue = "-" + oilVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = oilVal.ToString();
            }

            if (SS.Getbool((int)MixerDigitalMiningOil.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Oil, 4).ToString();
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningOil.Auto);
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningOil.OverTolerance);
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningOil.WeightingStart);
        }

        /// <summary>
        ///油料1中间 秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_O_MID(NewuYiBiao sc)
        {
            double oilMidVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.OilMid, 4), NewuGlobal.SoftConfig.OilDigit);
            if (SS.Getbool((int)MixerDigitalMiningOil.Sign))
            {
                sc.NewuYiBiaoValue = "-" + oilMidVal.ToString();
            }
            else
            {
                sc.NewuYiBiaoValue = oilMidVal.ToString();
            }
        }

        /// <summary>
        ///硅烷 秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_S(Scale sc)
        {
            double silVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Silane, 4), NewuGlobal.SoftConfig.SilaneDigit);
            if (SS.Getbool((int)MixerDigitalMiningSilane.Sign))
            {
                sc.NewuScaleValue = "-" + silVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = silVal.ToString();
            }
            if (SS.Getbool((int)MixerDigitalMiningSilane.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Silane, 4).ToString();
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningSilane.Auto);
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningSilane.OverTolerance);
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningSilane.WeightingStart);
        }

        /// <summary>
        ///小药秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_D(Scale sc)
        {
            double carbonVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.DrugMixer, 4), NewuGlobal.SoftConfig.DrugDigit);
            if (SS.Getbool((int)MixerDigitalMiningDrug.Sign))
            {
                sc.NewuScaleValue = "-" + carbonVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = carbonVal.ToString();
            }
            if (SS.Getbool((int)MixerDigitalMiningDrug.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.DrugMixer, 4).ToString();  //设定车数
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningDrug.Auto);  //是否自动
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningDrug.OverTolerance);  //超差
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningDrug.WeightingStart); //开始称量
        }

        /// <summary>
        ///终炼小药秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_DFinal(ScaleFinal sc)
        {
            double carbonVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.DrugMixer, 4), NewuGlobal.SoftConfig.DrugDigit);
            if (SS.Getbool((int)MixerDigitalMiningDrug.Sign))
            {
                sc.NewuScaleValue = "-" + carbonVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = carbonVal.ToString();
            }
            if (SS.Getbool((int)MixerDigitalMiningDrug.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            //小药磅秤车数
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.DrugMixer, 4).ToString();  //设定车数
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningDrug.WeightingOK);  //是否自动
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningDrug.OverTolerance);  //超差
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningDrug.WeightingStart); //炭黑开始称量
        }

        /// <summary>
        ///胶料 秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_R(Scale sc)
        {
            double rubberVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Rubber, 4), NewuGlobal.SoftConfig.RubberDigit);
            if (SS.Getbool((int)MixerDigitalMiningRubber.Sign))
            {
                sc.NewuScaleValue = "-" + rubberVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = rubberVal.ToString();
            }

            if (SS.Getbool((int)MixerDigitalMiningRubber.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Rubber, 4).ToString();
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningRubber.Auto);
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningRubber.OverTolerance);
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningRubber.WeightingStart);
        }

        /// <summary>
        ///终炼胶料秤
        /// </summary>
        /// <param name="sc"></param>
        public static void Scale_RFinal(ScaleFinal sc)
        {
            double rubberVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Rubber, 4), NewuGlobal.SoftConfig.RubberDigit);
            if (SS.Getbool((int)MixerDigitalMiningRubber.Sign))
            {
                sc.NewuScaleValue = "-" + rubberVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = rubberVal.ToString();
            }

            if (SS.Getbool((int)MixerDigitalMiningRubber.WeightingOK))
            {
                sc.IsChengOK1 = true;
            }
            else
            {
                sc.IsChengOK1 = false;
            }
            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Rubber, 4).ToString();
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningRubber.Auto);
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningRubber.OverTolerance);
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningRubber.WeightingStart);
        }

        /// <summary>
        ///塑解剂秤
        /// </summary>
        /// <param name="sc"></param>
        public static void DeatailScale_P(Scale sc)
        {
            double palVal = FunClass.GetMemHexDec(SS.GetStr((int)MixerAnalogMiningWeight.Plasticizer, 4), NewuGlobal.SoftConfig.PlaDigit);
            if (SS.Getbool((int)MixerDigitalMiningZno2.Sign))
            {
                sc.NewuScaleValue = "-" + palVal.ToString();
            }
            else
            {
                sc.NewuScaleValue = palVal.ToString();
            }

            sc.NewuScaleBatch = SS.GetInt((int)MixerAnalogMiningActBatch.Plasticizer, 4).ToString();
            sc.NewuScaleAuto = SS.Getbool((int)MixerDigitalMiningZno2.Auto);
            sc.NewuScaleOverAlarm = SS.Getbool((int)MixerDigitalMiningZno2.OverTolerance);
            sc.NewuScaleStartRun = SS.Getbool((int)MixerDigitalMiningZno2.WeightingStart);
        }

        #endregion 炭 粉 油 药 胶 监控界面仪表数据

        #region 密炼机通用仪表

        /// <summary>
        /// 密炼机仪表
        /// </summary>
        /// <param name="YiBiaoPowerEnergy"></param>
        /// <param name="YiBiaoPressSpeed"></param>
        /// <param name="YiBiaoMixTemp"></param>
        /// <param name="YiBiaoTimeTime"></param>
        public static void Scale_M(NewuYiBiao YiBiaoTimeTime)
        {
            YiBiaoTimeTime.NewuYiBiaoValue = SS.GetInt(AddressConst.MixerTime, 4).ToString();  //炼胶时间
        }

        /// <summary>
        /// 下密炼炼胶时间
        /// </summary>
        /// <param name="YiBiaoTimeTime"></param>
        public static void Scale_M_D(NewuYiBiao yibiaoTimeDown)
        {
            yibiaoTimeDown.NewuYiBiaoValue = SS.GetInt(AddressConst.MixerFTime, 4).ToString();  //炼胶时间
        }

        public static void Scale_M(Label lb_press, Label lb_speed, Label lb_power, Label lb_energy, Label lb_temp, Label lb_factbatch, NewuPicAngle p_run, NewuPicAngle p_mixerStatus, NewuPicAngle p_alarm)
        {
            lb_factbatch.Text = SS.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4).ToString();//实际车数

            lb_temp.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Temp, 4) / ScaleAccuracy.digitTemp).ToString() + " ℃";//温度

            lb_power.Text = SS.GetInt((int)MixerAnalogMiningMixer.Power, 4).ToString() + " kw";//功率

            lb_press.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Press, 4) / ScaleAccuracy.digitPress).ToString() + " Mpa";//压力

            lb_speed.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Speed, 4) / ScaleAccuracy.digitSpeed).ToString() + " r/min";//转速

            lb_energy.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Energy, 4) / ScaleAccuracy.digitEnergy).ToString() + " kw/h";//能量

            p_mixerStatus.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixer.Auto) ? NewuPicType.Background : NewuPicType.Foreground;//密炼自动

            p_run.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixer.Running) ? NewuPicType.Background : NewuPicType.Foreground;//密炼运行

            p_alarm.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixer.MixerAlarm) ? NewuPicType.Background : NewuPicType.Foreground;//密炼机报警
        }

        public static void Scale_M_D(Label lb_pressD, Label lb_speedD, Label lb_powerD, Label lb_energyD, Label lb_tempD, Label lb_factbatchD, NewuPicAngle p_runD, NewuPicAngle p_alarmD, NewuPicAngle p_unLoadingD, NewuPicAngle p_mixerStatusD)
        {
            lb_factbatchD.Text = SS.GetInt((int)MixerAnalogMiningActBatch.DownMixer, 4).ToString();//实际车数

            lb_tempD.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixerDown.Temp, 4) / ScaleAccuracy.digitTemp).ToString() + " ℃";//温度

            lb_powerD.Text = SS.GetInt((int)MixerAnalogMiningMixerDown.Power, 4).ToString() + " kw";//功率

            lb_pressD.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixerDown.Press, 4) / ScaleAccuracy.digitPress).ToString() + " Mpa";//压力

            lb_speedD.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixerDown.Speed, 4) / ScaleAccuracy.digitSpeed).ToString() + " r/min";//转速

            lb_energyD.Text = (1.0 * SS.GetInt((int)MixerAnalogMiningMixerDown.Energy, 4) / ScaleAccuracy.digitEnergy).ToString() + " kw/h";//能量

            p_mixerStatusD.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixerDown.Auto) ? NewuPicType.Background : NewuPicType.Foreground;//密炼自动

            p_runD.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixerDown.Running) ? NewuPicType.Background : NewuPicType.Foreground;//密炼运行

            p_alarmD.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixerDown.MixerAlarm) ? NewuPicType.Background : NewuPicType.Foreground;//密炼机报警

            p_unLoadingD.NewuPicTypeStyle = SS.Getbool((int)MixerDigitalMiningMixerDown.BelowRamOpen) ? NewuPicType.Background : NewuPicType.Foreground;//密炼机报警
        }

        /// <summary>
        /// 密炼机终炼仪表
        /// </summary>
        /// <param name="YiBiaoPowerEnergy"></param>
        /// <param name="YiBiaoPressSpeed"></param>
        /// <param name="YiBiaoMixTemp"></param>
        /// <param name="YiBiaoTimeTime"></param>
        public static void Scale_MFinal(NewuYiBiaoFinal YiBiaoTimeTime)
        {
            YiBiaoTimeTime.NewuYiBiaoValue = SS.GetInt(AddressConst.MixerTime, 4).ToString();  //时时间，(分步)
        }

        /// <summary>
        /// 密炼机终炼仪表
        /// </summary>
        /// <param name="YiBiaoPowerEnergy"></param>
        /// <param name="YiBiaoPressSpeed"></param>
        /// <param name="YiBiaoMixTemp"></param>
        /// <param name="YiBiaoTimeTime"></param>
        public static void Scale_MFinal(NewuYiBiao YiBiaoTimeTime)
        {
            YiBiaoTimeTime.NewuYiBiaoValue = SS.GetInt(AddressConst.MixerTime, 4).ToString();  //时时间，(分步)
        }

        public static void MixerLocation(NewuPicAngle p_HighLocation, NewuPicAngle p_midLocation, NewuPicAngle p_lowLocation, NewuPicAngle p_addDoor, NewuPicAngle p_unLoadDoor, NewuPicAngle p_mixerStatus)
        {
            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixer.RamHigh))
                p_HighLocation.NewuPicTypeStyle = NewuPicType.Background;
            else
                p_HighLocation.NewuPicTypeStyle = NewuPicType.Foreground;

            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixer.RamMid))
                p_midLocation.NewuPicTypeStyle = NewuPicType.Background;
            else
                p_midLocation.NewuPicTypeStyle = NewuPicType.Foreground;

            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixer.RamLow))
                p_lowLocation.NewuPicTypeStyle = NewuPicType.Background;
            else
                p_lowLocation.NewuPicTypeStyle = NewuPicType.Foreground;

            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixer.FeedingDoorOpen))
                p_addDoor.NewuPicTypeStyle = NewuPicType.Background;
            else
                p_addDoor.NewuPicTypeStyle = NewuPicType.Foreground;

            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixer.BelowRamOpen))
                p_unLoadDoor.NewuPicTypeStyle = NewuPicType.Background;
            else
                p_unLoadDoor.NewuPicTypeStyle = NewuPicType.Foreground;

            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixer.Manual))
                p_mixerStatus.NewuPicTypeStyle = NewuPicType.Background;
            else
                p_mixerStatus.NewuPicTypeStyle = NewuPicType.Foreground;
        }

        #endregion 密炼机通用仪表

        /// <summary>
        /// 通过Moder对应数据类，返回仪表上所显示出来的数据
        /// </summary>
        /// <param name="findModel"></param>
        /// <returns></returns>
        private static string MackMessage(DevicePartType wtf, int valtemp)
        {
            FormulaWeigh findModel = null;
            if (rawList == null)
                rawList = new FormulaWeighRepository().GetModelListNew(0, "MaterialID = '" + NewuGlobal.RunInfo.OrderMixModel.MaterialID + "'", "DevicePartID,DropOrder,WeighOrder");
            string DevicePartCode = NewuGlobal.GetDevicePartCode(wtf);

            foreach (FormulaWeigh item in rawList)
            {
                if (item.DevicePartCode == DevicePartCode && item.DropOrder == valtemp / 10 && item.WeighOrder == valtemp % 10)
                {
                    findModel = item;
                    break;
                }
            }
            if (findModel == null)
                return "暂无称量数据";
            string disPlayMsg = "";
            disPlayMsg += "物料：" + findModel.WeighMaterialCode + "\r\n";
            disPlayMsg += "设定：" + findModel.WeighSetVal + "\r\n";
            disPlayMsg += "误差：" + findModel.AllowError;
            return disPlayMsg;
        }
    }
}