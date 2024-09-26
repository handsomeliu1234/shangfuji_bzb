using NewuCommon;
using Repository.Model;
using Repository.Repository;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace Repository.GlobalConfig
{
    /// <summary>
    /// 记录报表-曲线类,设备停起记录
    /// </summary>
    public class SaveCurve
    {
        private MixState upMixState = new MixState();
        private MixState upMixStateF = new MixState();
        private RptFlagMDL mixFlag;
        private RptFlagMDL mixFFlag;

        private Timer timer = new Timer();
        private Timer timer2 = new Timer();

        private CSharedString ssMem = NewuGlobal.MemDB;
        private CStopWatch cTime = new CStopWatch();
        private CStopWatch cTimeF = new CStopWatch();

        private readonly PM_DevicePartOrderTranRepository devicePartOrderTranRepository = new PM_DevicePartOrderTranRepository();
        private readonly PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private readonly RPT_DeviceEvent deviceEvent = new RPT_DeviceEvent();
        private readonly RPT_DeviceEventF deviceEventF = new RPT_DeviceEventF();
        private RPT_DeviceEventFRepository deviceEventFRepository = new RPT_DeviceEventFRepository();
        private RPT_CurveFRepository curveFRepository = new RPT_CurveFRepository();

        private int timeNum = 0;
        private int timeNumF = 0;

        private StringBuilder timeSb = new StringBuilder();
        private StringBuilder tempSb = new StringBuilder();
        private StringBuilder powerSb = new StringBuilder();
        private StringBuilder pressSb = new StringBuilder();
        private StringBuilder speedSb = new StringBuilder();
        private StringBuilder engSb = new StringBuilder();
        private StringBuilder autoSb = new StringBuilder();
        private StringBuilder voltageSb = new StringBuilder();
        private StringBuilder autoRunSb = new StringBuilder();
        private StringBuilder ram_stateSb = new StringBuilder();

        private StringBuilder timeFSb = new StringBuilder();
        private StringBuilder tempFSb = new StringBuilder();
        private StringBuilder powerFSb = new StringBuilder();
        private StringBuilder pressFSb = new StringBuilder();
        private StringBuilder speedFSb = new StringBuilder();
        private StringBuilder engFSb = new StringBuilder();
        private StringBuilder autoFSb = new StringBuilder();
        private StringBuilder voltageFSb = new StringBuilder();
        private StringBuilder autoRunFSb = new StringBuilder();

        /// <summary>
        /// 炼胶时间计时
        /// </summary>
        private int cnt = 0;

        private int cntF = 0;

        private bool isauto = true;
        private bool isautoF = true;

        /// <summary>
        /// 投胶料时间
        /// </summary>
        private int cntDropInRubber = 0;

        //上密炼到下密炼时间
        private int cntDropInRubberF = 0;

        public SaveCurve()
        {
            StartDeviceEvent();
            upMixState.RealBatch = ssMem.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4);
            upMixState.MixST = DateTime.Now;   //防止程序重新启动报错

            string MixUpCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.MixUp);
            mixFlag = new RptFlagMDL() { FlagAddr = (int)MixerWeighOKSignal.Mixer, Desc = MixUpCode, BatchAddr = (int)MixerAnalogMiningActBatch.Mixer };

            //timer 是传给主界面炼胶时间
            timer.Elapsed += Timer_Tick;
            timer.Interval = 1000;
            timer.Enabled = true;

            if (NewuGlobal.SoftConfig.DownMixer)
            {
                string MixDownCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.MixDown);
                mixFFlag = new RptFlagMDL() { FlagAddr = (int)MixerWeighOKSignal.MixerDown, Desc = MixDownCode, BatchAddr = (int)MixerAnalogMiningActBatch.DownMixer };
                //timer2 是传给主界下密炼面炼胶时间
                timer2.Elapsed += Timer_Tick2;
                timer2.Interval = 1000;
                timer2.Enabled = true;
            }
        }

        private void StartDeviceEvent()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    CurveDataEvent();
                    if (NewuGlobal.SoftConfig.DownMixer)
                        CurveDataEventF();
                    await Task.Delay(1000);
                }
            });
        }

        private void CurveDataEvent()
        {
            try
            {
                //炼胶开始信号,曲线开始记录信号点,加完胶料,上顶栓下压时候给
                upMixState.IsInReady = ssMem.Getbool((int)MixerDigitalMiningMixer.CurveStart);

                if (upMixState.IsInReady && !upMixState.IsInMix)
                {
                    upMixState.RealBatch = Convert.ToInt32(ssMem.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4));//密炼实际车数
                    NewuGlobal.LogCat("曲线报表").Info("曲线开始记录点出现，混炼时间：" + "【" + cnt + "】" + "当前密炼车数：【" + upMixState.RealBatch + "】");
                    upMixState.IsInMix = true;
                    upMixState.MixED = DateTime.Now;
                    NewuGlobal.GetMixState.isRun = true;
                    upMixState.DropCycle = (int)(upMixState.MixED - upMixState.MixST).TotalSeconds;  //间隔时间
                    SaveDeviceEvent(upMixState.MixST, upMixState.MixED, "停机");
                }
                else if (ssMem.Getbool((int)MixerDigitalMiningMixer.CurveEnd))  //炼胶结束
                {
                    if (ssMem.Getbool((int)MixerDigitalMiningMixer.CurveStart) == false && upMixState.IsInMix == true)
                    {
                        NewuGlobal.LogCat("曲线报表").Info("曲线结束记录点出现，混炼时间：" + "【" + cnt + "】" + "当前密炼车数：【" + upMixState.RealBatch + "】");
                        upMixState.IsInMix = false;
                        upMixState.MixST = DateTime.Now;
                        SaveDeviceEvent(upMixState.MixED, upMixState.MixST, "作业");
                        SaveCurveData(true);
                        timeNum = 0;
                        isauto = true;
                        cnt = 0;
                        cntDropInRubber = 0;//把加胶时间清零
                        upMixState.DropCycle = 0;
                        upMixState.CurveID = "";
                        upMixState.IsInReady = false;
                        NewuGlobal.GetMixState.isRun = false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveCurve").Error(ex.ToString());
            }
        }

        private void CurveDataEventF()
        {
            try
            {
                //炼胶开始信号,曲线开始记录信号点,加完胶料,上顶栓下压时候给
                upMixStateF.IsInReady = ssMem.Getbool((int)MixerDigitalMiningMixerDown.CurveStart);

                if (upMixStateF.IsInReady && !upMixStateF.IsInMix)
                {
                    upMixStateF.RealBatch = Convert.ToInt32(ssMem.GetInt((int)MixerAnalogMiningActBatch.DownMixer, 4));//密炼实际车数
                    NewuGlobal.LogCat("下密炼曲线报表").Info("曲线开始记录点出现，混炼时间：" + "【" + cnt + "】" + "当前密炼车数：【" + upMixState.RealBatch + "】");
                    upMixStateF.IsInMix = true;
                    upMixStateF.MixED = DateTime.Now;
                    NewuGlobal.GetMixState.isRun = true;
                    upMixStateF.DropCycle = (int)(upMixStateF.MixED - upMixStateF.MixST).TotalSeconds;  //间隔时间
                    SaveDeviceEventF(upMixStateF.MixST, upMixStateF.MixED, "停机");
                }
                else if (ssMem.Getbool((int)MixerDigitalMiningMixer.CurveEnd))  //炼胶结束
                {
                    if (ssMem.Getbool((int)MixerDigitalMiningMixer.CurveStart) == false && upMixState.IsInMix == true)
                    {
                        NewuGlobal.LogCat("曲线报表").Info("曲线结束记录点出现，混炼时间：" + "【" + cnt + "】" + "当前密炼车数：【" + upMixState.RealBatch + "】");
                        upMixStateF.IsInMix = false;
                        upMixStateF.MixST = DateTime.Now;
                        SaveDeviceEventF(upMixState.MixED, upMixState.MixST, "作业");
                        SaveCurveDataF(true);
                        timeNumF = 0;
                        isautoF = true;
                        cntF = 0;
                        cntDropInRubberF = 0;//把加胶时间清零
                        upMixStateF.DropCycle = 0;
                        upMixStateF.CurveID = "";
                        upMixStateF.IsInReady = false;
                        NewuGlobal.GetMixState.isRun = false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveCurve").Error(ex.ToString());
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //675 加料门开信号 加料门开且物料没有进入密炼机
            if (ssMem.Getbool((int)MixerDigitalMiningMixer.FeedingDoorOpen) && !upMixState.IsInMix)
            {
                cntDropInRubber++;  //加胶时间
            }

            //胶料进入密炼机
            if (upMixState.IsInMix)
            {
                //曲线记录过程中 只要有一秒钟手动  也认为该车为手动炼胶
                if (ssMem.Getbool((int)MixerDigitalMiningMixer.Manual))
                {
                    isauto = false;
                }
                cnt++;//可以考虑和电气协商有plc累加，避免软件异常记录错误 20231208
                ssMem.SetStr(AddressConst.MixerTime, cnt.ToString("0000"));

                //曲线采集频率 1次/s
                if (cTime.Elapsed() >= 1000)
                {
                    SaveCurveData(false);
                    cTime.Reset();
                }
            }

            if (!ssMem.Getbool((int)MixerDigitalMiningMixer.CurveStart))
            {
                ssMem.SetStr(AddressConst.MixerTime, "0000");
            }
        }

        private void Timer_Tick2(object sender, ElapsedEventArgs e)
        {
            //上密炼卸料门开、下密炼自动、物料没有进入下密炼机
            if (ssMem.Getbool((int)MixerDigitalMiningMixerDown.Auto) && ssMem.Getbool((int)MixerDigitalMiningMixer.BelowRamOpen) && !upMixStateF.IsInMix)
            {
                cntDropInRubberF++;  //加胶时间
            }

            //胶料进入密炼机
            if (upMixStateF.IsInMix)
            {
                //曲线记录过程中 只要有一秒钟手动  也认为该车为手动炼胶
                if (ssMem.Getbool((int)MixerDigitalMiningMixerDown.Manual))
                {
                    isautoF = false;
                }
                cntF++;//可以考虑和电气协商有plc累加，避免软件异常记录错误 20231208
                ssMem.SetStr(AddressConst.MixerFTime, cntF.ToString("D4"));

                //曲线采集频率 1次/s
                if (cTimeF.Elapsed() >= 1000)
                {
                    SaveCurveDataF(false);
                    cTimeF.Reset();
                }
            }

            if (!ssMem.Getbool((int)MixerDigitalMiningMixerDown.CurveStart))
            {
                ssMem.SetStr(AddressConst.MixerFTime, "0000");
            }
        }

        /// <summary>
        /// 保存 停机/运行 记录 //停机和运行记录车数相差一车
        /// </summary>
        /// <param name="st">开始时间</param>
        /// <param name="ed">结束时间</param>
        /// <param name="unitType">停机/运行</param>
        private void SaveDeviceEvent(DateTime st, DateTime ed, string unitType)
        {
            try
            {
                PM_DevicePartOrderTran devicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(mixFlag.Desc);

                RPT_DeviceEventRepository deviceEventRepository = new RPT_DeviceEventRepository
                {
                    TableYear = NewuGlobal.RunInfo.OrderMixModel.StartTime.Value.Year
                };

                if (ssMem.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4) > upMixState.RealBatch && upMixState.LastOrderCode == devicePartOrderTran.OrderID)
                {
                    upMixState.RealBatch = ssMem.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4);
                }

                if (unitType == "作业")
                {
                    //读取密炼区域中开卸料门的时间
                    if (NewuGlobal.OpenDropDoorIndex != 0)
                    {
                        int tempdata = ssMem.GetInt((int)MixerReportWeightandMixer.MixerTime + NewuGlobal.OpenDropDoorIndex * 28, 4);
                        if (tempdata != 0)
                        {
                            deviceEvent.Reserve5 = tempdata.ToString();
                        }
                    }
                    //炼胶时间
                    PM_OrderTran order = orderTranRepository.GetModel(NewuGlobal.RunInfo.OrderMixModel.OrderID);
                    if (order != null)
                    {
                        order.Reserve1 = upMixState.RealBatch.ToString();
                        order.EndTime = ed;  //结束时间
                        orderTranRepository.Update(order);
                    }

                    deviceEvent.Temp = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.DischargeTemp, 4) / ScaleAccuracy.digitTemp).ToString()); //排胶温度
                    deviceEvent.Power = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.DischargePower, 4)).ToString());                           //排胶功率
                    deviceEvent.Press = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.DischargePress, 4) / ScaleAccuracy.digitPress).ToString());//排胶压力
                    deviceEvent.Speed = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.Speed, 4) / ScaleAccuracy.digitSpeed).ToString());//转速
                    deviceEvent.Energy = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.DischargeEnergy, 4) / ScaleAccuracy.digitEnergy).ToString());//排胶能量
                    deviceEvent.Reserve1 = ssMem.GetInt((int)MixerAnalogMiningWeightTime.Carbon1, 4) + "," + ssMem.GetInt((int)MixerAnalogMiningWeightTime.Carbon2, 4) + "," + ssMem.GetInt((int)MixerAnalogMiningWeightTime.Carbon3, 4); //一次炭黑，二次炭黑，一次粉料
                    deviceEvent.Reserve2 = ssMem.GetInt((int)MixerAnalogMiningWeightTime.Oil1, 4) + "," + ssMem.GetInt((int)MixerAnalogMiningWeightTime.Oil2, 4) + "," + ssMem.GetInt((int)MixerAnalogMiningWeightTime.Oil3, 4); //加油时间
                    deviceEvent.Reserve3 = ssMem.GetInt((int)MixerAnalogMiningWeightTime.Silane1, 4) + "," + ssMem.GetInt((int)MixerAnalogMiningWeightTime.Silane2, 4) + "," + ssMem.GetInt((int)MixerAnalogMiningWeightTime.Silane3, 4); //硅烷时间
                    deviceEvent.Reserve4 = cntDropInRubber.ToString(); //加胶时间
                    deviceEvent.Reserve5 = cnt.ToString(); //炼胶总时间
                    deviceEvent.IntervalTime = upMixState.DropCycle.ToString(); //间隔时间
                }

                string strAuto;
                if (isauto)
                    strAuto = "自动";
                else
                    strAuto = "手动";

                RPT_DeviceEvent mixBatch = new RPT_DeviceEvent();
                {
                    mixBatch.DeviceCode = devicePartOrderTran.DeviceCode;
                    mixBatch.DeviceEventID = Guid.NewGuid().ToString();
                    mixBatch.Lot = devicePartOrderTran.Lot;
                    mixBatch.EventType = unitType;
                    mixBatch.MaterialCode = devicePartOrderTran.MaterialCode;
                    mixBatch.VersionNo = devicePartOrderTran.VersionNo;
                    mixBatch.PlanQty = devicePartOrderTran.SetBatch;
                    mixBatch.FactOrder = upMixState.RealBatch;
                    mixBatch.OrderID = devicePartOrderTran.OrderID;
                    mixBatch.PmMode = strAuto;
                    mixBatch.WorkerUserCode = NewuGlobal.TB_UserInfo.UserCode;
                    mixBatch.WorkGroup = devicePartOrderTran.Reserve1;
                    mixBatch.WorkOrder = devicePartOrderTran.Reserve2;
                    mixBatch.Temp = deviceEvent.Temp;
                    mixBatch.Power = deviceEvent.Power;
                    mixBatch.Press = deviceEvent.Press;
                    mixBatch.Speed = deviceEvent.Speed;
                    mixBatch.Energy = deviceEvent.Energy;

                    // re 1-4 加炭黑时间 加油时间 加硅烷时间 缺加胶料时间和投粉料
                    mixBatch.Reserve1 = deviceEvent.Reserve1;    //加炭黑1，加炭黑2，加粉料
                    mixBatch.Reserve2 = deviceEvent.Reserve2;    //加油时间
                    mixBatch.Reserve3 = deviceEvent.Reserve3;    //硅烷时间
                    mixBatch.Reserve4 = deviceEvent.Reserve4;    //投胶时间
                    mixBatch.Reserve5 = deviceEvent.Reserve5;    //炼胶时间
                    mixBatch.InitTime = DateTime.Now;
                    mixBatch.EndTime = ed;
                    mixBatch.StartTime = st;
                    mixBatch.UseTime = (int)(ed - st).TotalSeconds; // 配方时间
                    mixBatch.IntervalTime = deviceEvent.IntervalTime;  //记录间隔时间
                };

                if (NewuGlobal.SoftConfig.VersionID == "1")
                {
                    mixBatch.VersionID = 1;
                    mixBatch.Is_Read = 0;
                }
                deviceEventRepository.Add(devicePartOrderTran.Savetime.ToString("yyyy"), mixBatch);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveCurve").Error(ex.ToString());
            }
        }

        private void SaveDeviceEventF(DateTime st, DateTime ed, string unitType)
        {
            try
            {
                PM_DevicePartOrderTran devicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(mixFFlag.Desc);

                if (ssMem.GetInt((int)MixerAnalogMiningActBatch.DownMixer, 4) > upMixStateF.RealBatch && upMixStateF.LastOrderCode == devicePartOrderTran.OrderID)
                {
                    upMixStateF.RealBatch = ssMem.GetInt((int)MixerAnalogMiningActBatch.DownMixer, 4);
                }

                if (unitType == "作业")
                {
                    //读取密炼区域中开卸料门的时间
                    if (NewuGlobal.OpenDropDoorIndexF != 0)
                    {
                        int tempdata = ssMem.GetInt((int)MixerReportWeightandMixer.MixerDownTime + NewuGlobal.OpenDropDoorIndexF * 28, 4);
                        if (tempdata != 0)
                        {
                            deviceEventF.Reserve5 = tempdata.ToString();
                        }
                    }
                    //炼胶时间
                    PM_OrderTran order = orderTranRepository.GetModel(NewuGlobal.RunInfo.OrderMixModel.OrderID);
                    if (order != null)
                    {
                        order.Reserve1 = upMixState.RealBatch.ToString();
                        order.EndTime = ed;  //结束时间
                        orderTranRepository.Update(order);
                    }

                    deviceEventF.Temp = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.DischargeTemp, 4) / ScaleAccuracy.digitTemp).ToString()); //排胶温度
                    deviceEventF.Power = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.DischargePower, 4)).ToString());                           //排胶功率
                    deviceEventF.Press = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.DischargePress, 4) / ScaleAccuracy.digitPress).ToString());//排胶压力
                    deviceEventF.Speed = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.Speed, 4) / ScaleAccuracy.digitSpeed).ToString());//转速
                    deviceEventF.Energy = decimal.Parse((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.DischargeEnergy, 4) / ScaleAccuracy.digitEnergy).ToString());//排胶能量
                    deviceEventF.Reserve4 = cntDropInRubber.ToString(); //加胶时间
                    deviceEventF.Reserve5 = cntF.ToString(); //炼胶总时间
                    deviceEventF.IntervalTime = upMixStateF.DropCycle.ToString(); //间隔时间
                }

                string strAuto;
                if (isautoF)
                    strAuto = "自动";
                else
                    strAuto = "手动";

                RPT_DeviceEventF mixBatch = new RPT_DeviceEventF();
                {
                    mixBatch.DeviceCode = devicePartOrderTran.DeviceCode;
                    mixBatch.DeviceEventID = Guid.NewGuid().ToString();
                    mixBatch.Lot = devicePartOrderTran.Lot;
                    mixBatch.EventType = unitType;
                    mixBatch.MaterialCode = devicePartOrderTran.MaterialCode;
                    mixBatch.VersionNo = devicePartOrderTran.VersionNo;
                    mixBatch.PlanQty = devicePartOrderTran.SetBatch;
                    mixBatch.FactOrder = upMixStateF.RealBatch;
                    mixBatch.OrderID = devicePartOrderTran.OrderID;
                    mixBatch.PmMode = strAuto;
                    mixBatch.WorkerUserCode = NewuGlobal.TB_UserInfo.UserCode;
                    mixBatch.WorkGroup = devicePartOrderTran.Reserve1;
                    mixBatch.WorkOrder = devicePartOrderTran.Reserve2;
                    mixBatch.Temp = deviceEventF.Temp;
                    mixBatch.Power = deviceEventF.Power;
                    mixBatch.Press = deviceEventF.Press;
                    mixBatch.Speed = deviceEventF.Speed;
                    mixBatch.Energy = deviceEventF.Energy;

                    // re 1-4 加炭黑时间 加油时间 加硅烷时间 缺加胶料时间和投粉料
                    mixBatch.Reserve1 = deviceEventF.Reserve1;    //加炭黑1，加炭黑2，加粉料
                    mixBatch.Reserve2 = deviceEventF.Reserve2;    //加油时间
                    mixBatch.Reserve3 = deviceEventF.Reserve3;    //硅烷时间
                    mixBatch.Reserve4 = deviceEventF.Reserve4;    //投胶时间
                    mixBatch.Reserve5 = deviceEventF.Reserve5;    //炼胶时间
                    mixBatch.InitTime = DateTime.Now;
                    mixBatch.EndTime = ed;
                    mixBatch.StartTime = st;
                    mixBatch.UseTime = (int)(ed - st).TotalSeconds; // 配方时间
                    mixBatch.IntervalTime = deviceEventF.IntervalTime;  //记录间隔时间
                };

                if (NewuGlobal.SoftConfig.VersionID == "1")
                {
                    mixBatch.VersionID = 1;
                    mixBatch.Is_Read = 0;
                }
                deviceEventFRepository.Add(NewuGlobal.RunInfo.OrderMixModel.StartTime.Value.Year,
               mixBatch);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveCurve").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 存储曲线
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="isSave">是否启用保存</param>
        private void SaveCurveData(bool isSave)
        {
            try
            {
                #region 数据拼接

                timeNum++;
                timeSb.Append("/" + timeNum);
                ram_stateSb.Append("/" + ssMem.GetHex((int)MixerAnalogMiningMixer.RealTimeRam, 4));
                tempSb.Append("/" + ((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.Temp, 4)) / ScaleAccuracy.digitTemp).ToString());
                powerSb.Append("/" + ssMem.GetInt((int)MixerAnalogMiningMixer.Power, 4));
                pressSb.Append("/" + ((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.Press, 4)) / ScaleAccuracy.digitPress).ToString());
                speedSb.Append("/" + ((ssMem.GetInt((int)MixerAnalogMiningMixer.Speed, 4)) / ScaleAccuracy.digitSpeed).ToString());
                engSb.Append("/" + ((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixer.Energy, 4)) / ScaleAccuracy.digitEnergy).ToString());
                voltageSb.Append("/" + ssMem.GetInt((int)MixerAnalogMiningMixer.Voltage, 4));
                autoRunSb.Append("/" + ssMem.GetInt((int)MixerDigitalMiningMixer.Manual, 1));

                #endregion 数据拼接

                #region 开始进行保存或更新数据

                if (isSave == true || Regex.Matches(timeSb.ToString(), @"/").Count >= 10)
                {
                    PM_DevicePartOrderTran devicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(mixFlag.Desc);

                    RPT_CurveRepository curveRepository = new RPT_CurveRepository
                    {
                        TableYear = NewuGlobal.RunInfo.OrderMixModel.Savetime.Year
                    };

                    RPT_Curve curveModel = new RPT_Curve
                    {
                        CreateTime = DateTime.Now,
                        CurveID = Guid.NewGuid().ToString(),
                        DeviceCode = devicePartOrderTran.DeviceCode,
                        MaterialCode = devicePartOrderTran.MaterialCode,
                        FactOrder = upMixState.RealBatch,
                        VersionNo = devicePartOrderTran.VersionNo,
                        Lot = devicePartOrderTran.Lot,
                        LastUpdateTime = DateTime.Now,
                        OrderID = devicePartOrderTran.OrderID,
                        PlanQty = devicePartOrderTran.SetBatch,
                        Energy = engSb.ToString(),
                        Power = powerSb.ToString(),
                        Press = pressSb.ToString(),
                        Speed = speedSb.ToString(),
                        Temp = tempSb.ToString(),
                        RealTime = timeSb.ToString(),
                        Reserve1 = ram_stateSb.ToString(),
                        Reserve2 = voltageSb.ToString(),
                        Reserve3 = autoRunSb.ToString()
                    };

                    if (NewuGlobal.SoftConfig.VersionID == "1")
                    {
                        curveModel.VersionID = 1;
                        curveModel.Is_Read = 0;
                    }
                    if (string.IsNullOrEmpty(upMixState.CurveID))
                    {
                        curveRepository.Add(curveModel);

                        //记录下当前该笔数据的CurveID
                        upMixState.CurveID = curveModel.CurveID;
                    }
                    else
                    {
                        curveModel.CurveID = upMixState.CurveID;
                        curveModel.UpdateTotal += 1;
                        curveRepository.Update(curveModel);
                    }

                    ClearSb(1);
                }

                #endregion 开始进行保存或更新数据
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveCurve").Error(ex.ToString());
            }
        }

        private void SaveCurveDataF(bool isSave)
        {
            try
            {
                #region 数据拼接

                timeNumF++;
                timeFSb.Append("/" + timeNumF);
                tempFSb.Append("/" + ((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.Temp, 4)) / ScaleAccuracy.digitTemp).ToString());
                powerFSb.Append("/" + ssMem.GetInt((int)MixerAnalogMiningMixerDown.Power, 4));
                pressFSb.Append("/" + ((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.Press, 4)) / ScaleAccuracy.digitPress).ToString());
                speedFSb.Append("/" + ((ssMem.GetInt((int)MixerAnalogMiningMixerDown.Speed, 4)) / ScaleAccuracy.digitSpeed).ToString());
                engFSb.Append("/" + ((1.0 * ssMem.GetInt((int)MixerAnalogMiningMixerDown.Energy, 4)) / ScaleAccuracy.digitEnergy).ToString());
                voltageFSb.Append("/" + ssMem.GetInt((int)MixerAnalogMiningMixerDown.Voltage, 4));
                autoRunFSb.Append("/" + ssMem.GetInt((int)MixerDigitalMiningMixerDown.Manual, 1));

                #endregion 数据拼接

                #region 开始进行保存或更新数据

                if (isSave == true || Regex.Matches(timeFSb.ToString(), @"/").Count >= 10)
                {
                    PM_DevicePartOrderTran devicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(mixFFlag.Desc);

                    curveFRepository.TableYear = NewuGlobal.RunInfo.OrderMixModel.Savetime.Year;

                    RPT_CurveF curveModelF = new RPT_CurveF
                    {
                        CreateTime = DateTime.Now,
                        CurveID = Guid.NewGuid().ToString(),
                        DeviceCode = devicePartOrderTran.DeviceCode,
                        MaterialCode = devicePartOrderTran.MaterialCode,
                        FactOrder = upMixStateF.RealBatch,
                        VersionNo = devicePartOrderTran.VersionNo,
                        Lot = devicePartOrderTran.Lot,
                        LastUpdateTime = DateTime.Now,
                        OrderID = devicePartOrderTran.OrderID,
                        PlanQty = devicePartOrderTran.SetBatch,
                        Energy = engFSb.ToString(),
                        Power = powerFSb.ToString(),
                        Press = pressFSb.ToString(),
                        Speed = speedFSb.ToString(),
                        Temp = tempFSb.ToString(),
                        RealTime = timeFSb.ToString(),
                        Reserve2 = voltageFSb.ToString(),
                        Reserve3 = autoRunFSb.ToString()
                    };

                    if (NewuGlobal.SoftConfig.VersionID == "1")
                    {
                        curveModelF.VersionID = 1;
                        curveModelF.Is_Read = 0;
                    }
                    if (string.IsNullOrEmpty(upMixStateF.CurveID))
                    {
                        curveFRepository.Add(curveModelF);

                        //记录下当前该笔数据的CurveID
                        upMixStateF.CurveID = curveModelF.CurveID;
                    }
                    else
                    {
                        curveModelF.CurveID = upMixStateF.CurveID;
                        curveModelF.UpdateTotal += 1;
                        curveFRepository.Update(curveModelF);
                    }

                    ClearSb(2);
                }

                #endregion 开始进行保存或更新数据
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveCurve").Error(ex.ToString());
            }
        }

        public void ClearSb(int flag)
        {
            if (flag == 1)
            {
                timeSb.Remove(0, timeSb.Length);
                tempSb.Remove(0, tempSb.Length);
                powerSb.Remove(0, powerSb.Length);
                pressSb.Remove(0, pressSb.Length);
                speedSb.Remove(0, speedSb.Length);
                engSb.Remove(0, engSb.Length);
                autoSb.Remove(0, autoSb.Length);
                voltageSb.Remove(0, voltageSb.Length);
                autoRunSb.Remove(0, autoRunSb.Length);
                ram_stateSb.Remove(0, ram_stateSb.Length);
            }
            else
            {
                timeFSb.Remove(0, timeFSb.Length);
                tempFSb.Remove(0, tempFSb.Length);
                powerFSb.Remove(0, powerFSb.Length);
                pressFSb.Remove(0, pressFSb.Length);
                speedFSb.Remove(0, speedFSb.Length);
                engFSb.Remove(0, engFSb.Length);
                autoFSb.Remove(0, autoFSb.Length);
                voltageFSb.Remove(0, voltageFSb.Length);
                autoRunFSb.Remove(0, autoRunFSb.Length);
            }
        }
    }
}