using Newtonsoft.Json;
using Newu;
using NewuCommon;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GlobalConfig
{
    /// <summary>
    /// 保存称量和密炼报表
    /// </summary>
    public class SaveRawMix
    {
        private CSharedString ssMem = NewuGlobal.MemDB;

        private readonly PM_OrderTranRepository pM_OrderTranRepository = new PM_OrderTranRepository();

        private readonly RPT_WeightRepository weightRepository = new RPT_WeightRepository();
        private readonly PM_DevicePartOrderTranRepository devicePartOrderTranRepository = new PM_DevicePartOrderTranRepository();
        private readonly ViewFormulaWeighRepository formulaWeighRepository = new ViewFormulaWeighRepository();
        private readonly ViewFormulaMixRepository formulaMixRepository = new ViewFormulaMixRepository();
        private readonly RPT_BarcodeRecordRepository barcodeRecordRepository = new RPT_BarcodeRecordRepository();
        private readonly RPT_MixStepRepository mixStepRepository = new RPT_MixStepRepository();
        private readonly RPT_MixStepFRepository mixStepFRepository = new RPT_MixStepFRepository();
        private readonly ViewFormulaMixFRepository formulaMixFRepository = new ViewFormulaMixFRepository();

        public static List<MemoryNotify> weightOkSinal = new List<MemoryNotify>();

        public SaveRawMix()
        {
            NewuGlobal.LogCat("SaveRawMix").Info("记录报表程序启动");
            //获取每个设备部件的编码
            string CarbonCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Carbon);
            string PlasticizerCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Plasticizer);
            string OilCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Oil);
            string RubberCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Rubber);
            string DrugMixerCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.DrugMixer);
            string MixUpCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.MixUp);
            string SilCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Silane);
            string ZnoCode = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Zno);

            string CarbonCode2 = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Carbon2);
            string OilCode2 = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Oil2);
            string DrugMixerCode2 = NewuGlobal.GetDevicePartCode(Newu.DevicePartType.DrugMixer2);
            string mixCodeDown = NewuGlobal.GetDevicePartCode(DevicePartType.MixDown);

            //炭黑
            if (NewuGlobal.SoftConfig.Carbon)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Carbon, DevicePartCode = CarbonCode, BatchAddress = (int)MixerAnalogMiningActBatch.Carbon, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.CarbonDigit), DataAddress = (int)MixerReportWeightandMixer.Carbon });
            }

            //塑解剂
            if (NewuGlobal.SoftConfig.Pla)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Plasticizer, DevicePartCode = PlasticizerCode, BatchAddress = (int)MixerAnalogMiningActBatch.Plasticizer, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.PlaDigit), DataAddress = (int)MixerReportWeightandMixer.Plasticizer });
            }

            //油料1
            if (NewuGlobal.SoftConfig.Oil)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Oil, DevicePartCode = OilCode, BatchAddress = (int)MixerAnalogMiningActBatch.Oil, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.OilDigit), DataAddress = (int)MixerReportWeightandMixer.Oil });
            }

            //胶料1
            if (NewuGlobal.SoftConfig.Rubber)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Rubber, DevicePartCode = RubberCode, BatchAddress = (int)MixerAnalogMiningActBatch.Rubber, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.RubberDigit), DataAddress = (int)MixerReportWeightandMixer.Rubber });
            }

            //小药1
            if (NewuGlobal.SoftConfig.Drug)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.DrugMixer, DevicePartCode = DrugMixerCode, BatchAddress = (int)MixerAnalogMiningActBatch.DrugMixer, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.DrugDigit), DataAddress = (int)MixerReportWeightandMixer.DrugMixer });
            }

            //硅烷
            if (NewuGlobal.SoftConfig.Silane)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Zno, DevicePartCode = SilCode, BatchAddress = (int)MixerAnalogMiningActBatch.Silane, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.SilaneDigit), DataAddress = (int)MixerReportWeightandMixer.Silane });
            }

            //粉料
            if (NewuGlobal.SoftConfig.Zno)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Zno, DevicePartCode = ZnoCode, BatchAddress = (int)MixerAnalogMiningActBatch.Zno, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.ZnoDigit), DataAddress = (int)MixerReportWeightandMixer.Zno });
            }

            //炭黑2
            if (NewuGlobal.SoftConfig.Carbon2)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Carbon2, DevicePartCode = CarbonCode2, BatchAddress = (int)MixerAnalogMiningActBatch.Carbon2, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.CarbonDigit), DataAddress = (int)MixerReportWeightandMixer.Carbon2 });
            }

            //油料2
            if (NewuGlobal.SoftConfig.Oil2)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Oil2, DevicePartCode = OilCode2, BatchAddress = (int)MixerAnalogMiningActBatch.Oil2, DanWei = Math.Pow(10, NewuGlobal.SoftConfig.OilDigit), DataAddress = (int)MixerReportWeightandMixer.Oil2 });
            }

            //小药2
            if (NewuGlobal.SoftConfig.Drug2)
            {
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.DrugMixer2, DevicePartCode = DrugMixerCode2 });
            }

            weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.Mixer, DevicePartCode = MixUpCode, BatchAddress = (int)MixerAnalogMiningActBatch.Mixer });

            //根据曲线开始信号做报表检查工能
            weightOkSinal.Add(new MemoryNotify { Address = (int)MixerDigitalMiningMixer.CurveStart, DevicePartCode = MixUpCode });

            if (NewuGlobal.SoftConfig.DownMixer)
                weightOkSinal.Add(new MemoryNotify { Address = (int)MixerWeighOKSignal.MixerDown, DevicePartCode = mixCodeDown, BatchAddress = (int)MixerAnalogMiningActBatch.DownMixer });

            Task.Run(() => StartWeightOkSinal());

            foreach (var item in weightOkSinal)
            {
                item.PropertyChanged += WeightOkSinal_PropertyChanged;
            }
        }

        public bool WeightOkSinal = false;

        public async Task StartWeightOkSinal()
        {
            if (WeightOkSinal)
                return;

            WeightOkSinal = true;

            while (WeightOkSinal)
            {
                foreach (var item in weightOkSinal)
                {
                    if (WeightOkSinal)
                        item.Value = NewuGlobal.MemMgr.GetSharedMemIntValue(item.Address, 1);
                    else
                        return;
                }

                await Task.Delay(500);
            }
        }

        private void WeightOkSinal_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                MemoryNotify memoryNotify = sender as MemoryNotify;
                if (memoryNotify.Value == 1)
                {
                    if (memoryNotify.DevicePartCode == NewuGlobal.GetDevicePartCode(Newu.DevicePartType.MixUp))
                    {
                        if (memoryNotify.Address == (int)MixerWeighOKSignal.Mixer)
                        {
                            Task.Run(() =>
                            {
                                int plcBatch = ssMem.GetInt(memoryNotify.BatchAddress, 4);
                                if (plcBatch <= 0)
                                {
                                    return;
                                }
                                SaveMix(memoryNotify, plcBatch);
                            });
                        }
                        else if (memoryNotify.Address == (int)MixerWeighOKSignal.MixerDown)
                        {
                            Task.Run(() =>
                            {
                                int plcBatch = ssMem.GetInt(memoryNotify.BatchAddress, 4);
                                if (plcBatch <= 0)
                                {
                                    return;
                                }
                                SaveMixF(memoryNotify, plcBatch);
                            });
                        }
                        else
                        {
                            Task.Run(() =>
                            {
                                PM_DevicePartOrderTran pM_DevicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(memoryNotify.DevicePartCode);
                                int mixBatch = Convert.ToInt32(ssMem.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4)) + 1;//进料时密炼实际车数
                                RepairReport(pM_DevicePartOrderTran.OrderID, mixBatch);
                            });
                        }
                    }
                    else
                    {
                        Task.Run(() =>
                        {
                            int plcBatch = ssMem.GetInt(memoryNotify.BatchAddress, 4);
                            if (plcBatch <= 0)
                            {
                                return;
                            }
                            SaveWeight(memoryNotify, plcBatch);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveRawMix").Error(ex.ToString());
            }
        }

        private void SaveWeight(MemoryNotify memoryNotify, int plcBatch)
        {
            try
            {
                int j = 0;
                PM_DevicePartOrderTran pM_DevicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(memoryNotify.DevicePartCode);

                //根据订单信息->获取配方信息
                string strWhere = " MaterialID='" + pM_DevicePartOrderTran.MaterialID + "' and DevicePartCode='" + memoryNotify.DevicePartCode + "'";

                List<View_FormulaWeigh> view_FormulaWeighs = formulaWeighRepository.GetModelList(0, strWhere, "DropOrder,WeighOrder");

                if (view_FormulaWeighs.Count == 0)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("{0}保存报表异常；表FormulaWeigh中数据为：{1}笔", memoryNotify.DevicePartCode, view_FormulaWeighs.Count);
                    NewuGlobal.LogCat("SaveRawMix").Error(builder.ToString());
                    return;
                }

                //插入称量数据
                foreach (var item in view_FormulaWeighs)
                {
                    string Reserve1 = "";
                    string strWhere1 = "";

                    if (NewuGlobal.SoftConfig.RubScanner && item.DevicePartCode == NewuGlobal.RubberScales)
                    {
                        strWhere1 = "TypeCodeName='" + NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Rubber) + "' and OrderID ='" + NewuGlobal.Now_OrderID + "' and MaterialCode ='" + item.WeighMaterialCode.ToString() + "'";
                    }
                    else if (NewuGlobal.SoftConfig.DrugScanner && item.DevicePartCode == NewuGlobal.DrugScales)
                    {
                        strWhere1 = "TypeCodeName='" + NewuGlobal.GetDevicePartCode(Newu.DevicePartType.DrugMixer) + "' and OrderID ='" + NewuGlobal.Now_OrderID + "' and MaterialCode ='" + item.WeighMaterialCode.ToString() + "'";
                    }
                    else if (NewuGlobal.SoftConfig.OilScanner && item.DevicePartCode.ToString() == NewuGlobal.OilScales)
                    {
                        strWhere1 = "TypeCodeName='" + NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Oil) + "' and MaterialCode ='" + item.WeighMaterialCode + "' order by SaveTime desc";
                    }
                    else if (NewuGlobal.SoftConfig.CarBonScanner && item.DevicePartCode.ToString() == NewuGlobal.CarbonScales)
                    {
                        strWhere1 = "TypeCodeName='" + NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Carbon) + "' and MaterialCode ='" + item.WeighMaterialCode + "' order by SaveTime desc";
                    }

                    if (NewuGlobal.SoftConfig.OilScanner || NewuGlobal.SoftConfig.DrugScanner || NewuGlobal.SoftConfig.RubScanner || NewuGlobal.SoftConfig.CarBonScanner)
                    {
                        barcodeRecordRepository.TableYear = NewuGlobal.RunInfo.OrderRawModel.Savetime.Year;
                        List<RPT_BarcodeRecord> rPT_BarcodeRecords = barcodeRecordRepository.GetList(strWhere1);
                        if (rPT_BarcodeRecords != null)
                        {
                            if (rPT_BarcodeRecords.Count > 0)
                            {
                                Reserve1 = rPT_BarcodeRecords[0].Reserve1;
                            }
                            else
                            {
                                Reserve1 = "";
                            }
                        }
                    }
                    //Task.Delay(1000);

                    j++;
                    int PlcBin = ssMem.GetHex(memoryNotify.DataAddress + ((j - 1) * 8), 4);
                    double PlcReal = ssMem.GetHex(memoryNotify.DataAddress + ((j - 1) * 8) + 4, 4) / memoryNotify.DanWei;
                    double errorReal = PlcReal - FunClass.VDbl(item.WeighSetVal);

                    weightRepository.DeleteData(pM_DevicePartOrderTran.Savetime.ToString("yyyy"), pM_DevicePartOrderTran.OrderID, item.WeighMaterialCode, plcBatch, PlcBin % 10, PlcBin / 10);

                    RPT_Weight model = new RPT_Weight
                    {
                        DeviceCode = item.DeviceCode,
                        DevicePartCode = item.DevicePartCode,
                        OrderID = pM_DevicePartOrderTran.OrderID,
                        MaterialCode = pM_DevicePartOrderTran.MaterialCode,
                        TypeCodeName = item.TypeCodeDesc,
                        VersionNo = pM_DevicePartOrderTran.VersionNo,
                        Lot = pM_DevicePartOrderTran.Lot,
                        PlanQty = pM_DevicePartOrderTran.SetBatch,
                        FactOrder = plcBatch,
                        SetMaterialCode = item.WeighMaterialCode
                    };

                    if (item.BinNo == null)
                    {
                        model.SetBinNo = 0;
                    }
                    else
                    {
                        model.SetBinNo = int.Parse(item.BinNo);
                    }

                    model.SetWeight = item.WeighSetVal;
                    model.AllowError = item.AllowError;
                    model.ActWeight = decimal.Parse(PlcReal.ToString());
                    model.ActError = decimal.Parse(errorReal.ToString());
                    model.WeightOrder = PlcBin % 10;
                    model.DropOrder = PlcBin / 10;
                    model.WorkGroup = NewuGlobal.SoftConfig.WorkGroup;//李辉 20231106 修改为配置文件获取
                    model.WorkOrder = NewuGlobal.SoftConfig.WorkOrder;//李辉 20231106
                    model.WorkerUserCode = NewuGlobal.TB_UserInfo.UserCode;
                    model.SaveTime = DateTime.Now;
                    model.Reserve1 = Reserve1;
                    model.ReadTime = DateTime.Now;

                    if (NewuGlobal.SoftConfig.VersionID == "1")
                    {
                        model.VersionID = 1;
                        model.Is_Read = 0;
                    }
                    weightRepository.Add(pM_DevicePartOrderTran.Savetime.ToString("yyyy"), model);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveRawMix").Error(ex.ToString());
            }
        }

        private void SaveMix(MemoryNotify memoryNotify, int plcBatch)
        {
            try
            {
                int j = 0;
                //获取订单数据
                PM_DevicePartOrderTran pM_DevicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(memoryNotify.DevicePartCode);

                //根据订单数据->获取配方数据
                string strWhere = "MaterialID='" + pM_DevicePartOrderTran.MaterialID + "' and DevicePartCode='" + memoryNotify.DevicePartCode + "'";
                List<View_FormulaMix> view_FormulaMixes = formulaMixRepository.GetList(0, strWhere, "stepOrder");

                //插入密炼数据
                List<RPT_MixStep> list = new List<RPT_MixStep>();

                foreach (var item in view_FormulaMixes)
                {
                    j++;
                    int timeReal = ssMem.GetInt((int)MixerReportWeightandMixer.MixerTime + ((j - 1) * 28), 4);
                    double TempReal = ssMem.GetInt((int)MixerReportWeightandMixer.MixerTemp + ((j - 1) * 28), 4) / (1.0 * ScaleAccuracy.digitTemp);
                    double EngReal = 1.0 * ssMem.GetInt((int)MixerReportWeightandMixer.MixerEnergy + ((j - 1) * 28), 4) / (1.0 * ScaleAccuracy.digitEnergy);
                    double PressReal = 1.0 * ssMem.GetInt((int)MixerReportWeightandMixer.MixerPress + ((j - 1) * 28), 4) / (1.0 * ScaleAccuracy.digitPress);
                    double SpeedReal = 1.0 * ssMem.GetInt((int)MixerReportWeightandMixer.MixerSpeed + ((j - 1) * 28), 4) / (1.0 * ScaleAccuracy.digitSpeed);
                    int PowerReal = ssMem.GetInt((int)MixerReportWeightandMixer.MixerPower + ((j - 1) * 28), 4);
                    mixStepRepository.DeleteMixStep(pM_DevicePartOrderTran.Savetime.ToString("yyyy"), pM_DevicePartOrderTran.OrderID, plcBatch, item.StepOrder);

                    RPT_MixStep model = new RPT_MixStep
                    {
                        DeviceCode = pM_DevicePartOrderTran.DeviceCode,
                        DevicePartCode = pM_DevicePartOrderTran.DevicePartCode,
                        OrderID = pM_DevicePartOrderTran.OrderID,
                        MaterialCode = pM_DevicePartOrderTran.MaterialCode,
                        VersionNo = pM_DevicePartOrderTran.VersionNo,
                        Lot = pM_DevicePartOrderTran.Lot,
                        PlanQty = pM_DevicePartOrderTran.SetBatch,
                        FactOrder = plcBatch,
                        StepOrder = item.StepOrder,
                        StepName = item.StepDesc,
                        ActionControlName = item.ActionControlNameCN,
                        SetTime = item.StepTime,
                        ActTime = timeReal,
                        SetTemp = item.StepTemp,
                        ActTemp = decimal.Parse(TempReal.ToString()),
                        SetPower = item.StepPower,
                        ActPower = PowerReal,
                        SetEnergy = item.StepEnergy,
                        ActEnergy = decimal.Parse(EngReal.ToString()),
                        SetPress = item.StepPress,
                        ActPress = decimal.Parse(PressReal.ToString()),
                        SetSpeed = item.StepSpeed,
                        ActSpeed = decimal.Parse(SpeedReal.ToString()),
                        KeepTime = 0,
                        WorkGroup = NewuGlobal.TB_UserInfo.WorkGroup,
                        WorkOrder = NewuGlobal.TB_UserInfo.WorkOrder,
                        WorkerUserCode = NewuGlobal.TB_UserInfo.UserCode,
                        SaveTime = DateTime.Now,
                        ReadTime = DateTime.Now
                    };

                    if (NewuGlobal.SoftConfig.VersionID == "1")
                    {
                        model.VersionID = 1;
                        model.Is_Read = 0;
                    }
                    list.Add(model);
                }
                mixStepRepository.InsertMixStep(pM_DevicePartOrderTran.Savetime.ToString("yyyy"), list);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveRawMix").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 下密炼密炼数据
        /// </summary>
        /// <param name="memoryNotify"></param>
        /// <param name="plcBatch"></param>
        private void SaveMixF(MemoryNotify memoryNotify, int plcBatch)
        {
            try
            {
                int j = 0;
                //获取订单数据
                PM_DevicePartOrderTran pM_DevicePartOrderTran = devicePartOrderTranRepository.GetDevicePartOrder(memoryNotify.DevicePartCode);

                //根据订单数据->获取配方数据
                string strWhere = $" MaterialID = '{pM_DevicePartOrderTran.MaterialID}' and DevicePartCode = '{memoryNotify.DevicePartCode}'";
                List<View_FormulaMixF> view_FormulaMixeFs = formulaMixFRepository.GetList(0, strWhere, "stepOrder");

                //插入密炼数据
                List<RPT_MixStepF> list = new List<RPT_MixStepF>();

                for (int i = 0; i < view_FormulaMixeFs.Count; i++)
                {
                    int timeReal = ssMem.GetInt((int)MixerReportWeightandMixer.MixerTime + (j * 28), 4);
                    double TempReal = ssMem.GetInt((int)MixerReportWeightandMixer.MixerTemp + (j * 28), 4) / (1.0 * ScaleAccuracy.digitTemp);
                    double EngReal = 1.0 * ssMem.GetInt((int)MixerReportWeightandMixer.MixerEnergy + (j * 28), 4) / (1.0 * ScaleAccuracy.digitEnergy);
                    double PressReal = 1.0 * ssMem.GetInt((int)MixerReportWeightandMixer.MixerPress + (j * 28), 4) / (1.0 * ScaleAccuracy.digitPress);
                    double SpeedReal = 1.0 * ssMem.GetInt((int)MixerReportWeightandMixer.MixerSpeed + (j * 28), 4) / (1.0 * ScaleAccuracy.digitSpeed);
                    int PowerReal = ssMem.GetInt((int)MixerReportWeightandMixer.MixerPower + (j * 28), 4);
                    mixStepRepository.DeleteMixStep(pM_DevicePartOrderTran.Savetime.ToString("yyyy"), pM_DevicePartOrderTran.OrderID, plcBatch, view_FormulaMixeFs[i].StepOrder);

                    RPT_MixStepF model = new RPT_MixStepF
                    {
                        DeviceCode = pM_DevicePartOrderTran.DeviceCode,
                        DevicePartCode = pM_DevicePartOrderTran.DevicePartCode,
                        OrderID = pM_DevicePartOrderTran.OrderID,
                        MaterialCode = pM_DevicePartOrderTran.MaterialCode,
                        VersionNo = pM_DevicePartOrderTran.VersionNo,
                        Lot = pM_DevicePartOrderTran.Lot,
                        PlanQty = pM_DevicePartOrderTran.SetBatch,
                        FactOrder = plcBatch,
                        StepOrder = view_FormulaMixeFs[i].StepOrder,
                        StepName = view_FormulaMixeFs[i].StepDesc,
                        ActionControlName = view_FormulaMixeFs[i].ActionControlNameCN,
                        SetTime = view_FormulaMixeFs[i].StepTime,
                        ActTime = timeReal,
                        SetTemp = view_FormulaMixeFs[i].StepTemp,
                        ActTemp = decimal.Parse(TempReal.ToString()),
                        SetPower = view_FormulaMixeFs[i].StepPower,
                        ActPower = PowerReal,
                        SetEnergy = view_FormulaMixeFs[i].StepEnergy,
                        ActEnergy = decimal.Parse(EngReal.ToString()),
                        SetPress = view_FormulaMixeFs[i].StepPress,
                        ActPress = decimal.Parse(PressReal.ToString()),
                        SetSpeed = view_FormulaMixeFs[i].StepSpeed,
                        ActSpeed = decimal.Parse(SpeedReal.ToString()),
                        KeepTime = 0,
                        WorkGroup = NewuGlobal.TB_UserInfo.WorkGroup,
                        WorkOrder = NewuGlobal.TB_UserInfo.WorkOrder,
                        WorkerUserCode = NewuGlobal.TB_UserInfo.UserCode,
                        SaveTime = DateTime.Now,
                        ReadTime = DateTime.Now
                    };

                    if (NewuGlobal.SoftConfig.VersionID == "1")
                    {
                        model.VersionID = 1;
                        model.Is_Read = 0;
                    }
                    list.Add(model);
                }
                mixStepFRepository.InsertMixStep(pM_DevicePartOrderTran.Savetime.ToString("yyyy"), list);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveRawMix").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 自动修复称量报表（进胶前调用）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="factOrder"></param>
        private void RepairReport(string orderId, int factOrder)
        {
            try
            {
                //查询到标准配方
                PM_OrderTran orderTran = pM_OrderTranRepository.GetModel(orderId);
                if (orderTran == null || orderTran.SetBatch < factOrder)
                    return;
                Formula BaseFormula = JsonConvert.DeserializeObject<Formula>(orderTran.Reserve5);
                List<View_FormulaWeigh> BaseformulaWeighs = BaseFormula.FormulaWeights;

                //查询当前车次的称量配方信息
                List<RPT_Weight> rPT_Weights = weightRepository.GetList(orderId, factOrder, orderTran);

                //循环所有车次解决缺失数据
                foreach (View_FormulaWeigh item in BaseformulaWeighs)
                {
                    List<RPT_Weight> list = rPT_Weights.FindAll(t => t.SetMaterialCode.Equals(item.WeighMaterialCode) && t.TypeCodeName.Equals(item.TypeCodeDesc) && t.DropOrder.Equals(item.DropOrder) && t.WeightOrder.Equals(item.WeighOrder));
                    if (list.Count == 0)
                    {
                        RPT_Weight model = new RPT_Weight();
                        model.DeviceCode = item.DeviceCode;
                        model.DevicePartCode = item.DevicePartCode;
                        model.OrderID = orderTran.OrderID;
                        model.MaterialCode = orderTran.MaterialCode;
                        model.TypeCodeName = item.TypeCodeDesc;
                        model.VersionNo = orderTran.VersionNo;
                        model.Lot = orderTran.Lot;
                        model.PlanQty = orderTran.SetBatch;
                        model.FactOrder = factOrder;
                        model.SetMaterialCode = item.WeighMaterialCode;
                        if (item.BinNo == null)
                        {
                            model.SetBinNo = 0;
                        }
                        else
                        {
                            model.SetBinNo = int.Parse(item.BinNo);
                        }
                        model.SetWeight = item.WeighSetVal;
                        model.AllowError = item.AllowError;
                        model.ActWeight = item.WeighSetVal;
                        model.ActError = item.WeighSetVal - model.ActWeight;
                        model.WeightOrder = item.WeighOrder;
                        model.DropOrder = item.DropOrder;
                        model.WorkGroup = NewuGlobal.SoftConfig.WorkGroup;
                        model.WorkOrder = NewuGlobal.SoftConfig.WorkOrder;
                        model.WorkerUserCode = NewuGlobal.TB_UserInfo.UserCode;
                        model.SaveTime = DateTime.Now;
                        model.ReadTime = DateTime.Now;
                        if (NewuGlobal.SoftConfig.VersionID == "1")
                        {
                            model.VersionID = 1;
                            model.Is_Read = 0;
                        }
                        weightRepository.Add(orderTran.Savetime.ToString("yyyy"), model);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SaveRawMix").Error(ex.ToString());
            }
        }
    }
}