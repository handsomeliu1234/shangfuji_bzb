using Newtonsoft.Json;
using Newu;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.GlobalConfig
{
    /// <summary>
    /// 连续配方发送类 考虑到连续配方情况:称量dgv和密炼dgv分开刷新
    /// </summary>
    public class ContinuedOrder
    {
        public event EventHandler<FormulaTranEventArgs> OnSentFormulaProgress;

        private bool isTrans = false;
        private static ContinuedOrder continuedOrder;
        private int progressValue = 0;

        public int ProgressValue
        {
            get
            {
                return progressValue;
            }
        }

        public bool IsSerise
        {
            get; set;
        }

        private static PM_OrderTran nowNoMixOrder;// 最好使用orderModle

        private static PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();

        private TransFromulaUtil transInstance = new TransFromulaUtil();

        private SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();

        private CreateTableRPT createTableRPT = new CreateTableRPT();

        private ViewFormulaWeighRepository viewFormulaWeighRepository = new ViewFormulaWeighRepository();
        private ViewFormulaMixRepository viewFormulaMixRepository = new ViewFormulaMixRepository();
        private ViewFormulaTechParamRepository ViewFormulaTechParamRepository = new ViewFormulaTechParamRepository();

        public static ContinuedOrder GetInstance()
        {
            if (continuedOrder == null)
            {
                continuedOrder = new ContinuedOrder();
            }
            return continuedOrder;
        }

        private ContinuedOrder()
        {
            StartContinueOrder();
        }

        private async void Timer1_Callback()
        {
            try
            {
                while (true)
                {
                    // 发送下一个配方称量和密炼数据
                    if (NewuGlobal.SoftConfig.IsContinue && !isTrans && NewuGlobal.MemDB.GetHex((int)MixerAnalogMiningMixer.MixerContinueRecipe, 4) == 1)
                    {
                        isTrans = true;
                        AddMsgToTextBox("", "isInTrans");
                        nowNoMixOrder = GetOrderModel();
                        if (nowNoMixOrder == null)
                        {
                            NewuGlobal.MemDB.SetStr((int)MixerAnalogMiningMixer.WeightContinueRecipe, "0000");
                            NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningMixer.WeightContinueRecipe, "0000");
                            NewuGlobal.MemDB.SetStr((int)MixerAnalogMiningMixer.MixerContinueRecipe, "0000");
                            NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningMixer.MixerContinueRecipe, "0000");
                            return;  // 最后一个配方。 让PLC歇着吧
                        }

                        //发送称量和密炼配方
                        if (TransferNewWeight(nowNoMixOrder) && TransferNewMix(nowNoMixOrder))
                        {
                            UpdateOrder(nowNoMixOrder);
                            if (NewuGlobal.RubyDataChange != null)
                                NewuGlobal.RubyDataChange.RefreshData(true);
                            if (NewuGlobal.MixDataChange != null)
                                NewuGlobal.MixDataChange.RefreshData(true);
                            if (NewuGlobal.MixGridDataChange != null)
                                NewuGlobal.MixGridDataChange.RefreshData(true); //UserControl

                            NewuGlobal.MemDB.SetStr((int)MixerAnalogMiningMixer.WeightContinueRecipe, "0000");
                            NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningMixer.WeightContinueRecipe, "0000");
                            NewuGlobal.MemDB.SetStr((int)MixerAnalogMiningMixer.MixerContinueRecipe, "0000");
                            NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningMixer.MixerContinueRecipe, "0000");
                        }
                        else
                        {
                            NewuGlobal.GetMixState.runOrderID = null;
                            NewuGlobal.GetMixState.isRun = false;
                        }

                        isTrans = false;
                    }

                    //获取下个配方
                    PM_OrderTran order = GetNextOrderModel();
                    if (order != null)
                    {
                        NewuGlobal.Next_OrderName = order.MaterialCode;
                    }
                    else
                    {
                        NewuGlobal.Next_OrderName = "";
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ContinuedOrder").Error(ex.ToString());
            }
        }

        // 检索当前未完成订单中优先级别高的订单MDL 并记录 发送
        public PM_OrderTran GetOrderModel()
        {
            string where = "IsStart = 0 and IsDelete = 0 order by StartTime ";

            List<PM_OrderTran> list = orderTranRepository.GetList(where);
            if (list.Count <= 0)
                return null;

            int minSerialNumber = list[0].SerialNumber;
            PM_OrderTran mdl = list[0];
            foreach (var a in list)
            {
                if (a.SerialNumber < minSerialNumber)
                {
                    minSerialNumber = a.SerialNumber;
                    mdl = a;
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取下个配方
        /// </summary>
        /// <returns></returns>
        public PM_OrderTran GetNextOrderModel()
        {
            string where = "IsStart = 0 and IsDelete = 0 order by SerialNumber ";
            List<PM_OrderTran> list = orderTranRepository.GetList(where);
            if (list == null || list.Count <= 0)
                return null;

            int minSerialNumber = list[0].SerialNumber;
            PM_OrderTran mdl = list[0];
            foreach (var a in list)
            {
                if (a.SerialNumber < minSerialNumber)
                {
                    minSerialNumber = a.SerialNumber;
                    mdl = a;
                }
            }
            return mdl;
        }

        /// <summary>
        /// 开始连续配方
        /// 1. 发送当前选中的配方 并开启timer轮训
        /// </summary>
        /// <returns></returns>
        public bool StartContinueOrder()
        {
            try
            {
                if (NewuGlobal.SoftConfig.IsContinue)
                {
                    IsSerise = true;
                }
                else
                {
                    IsSerise = false;
                }
                Task.Run(Timer1_Callback);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ContinuedOrder").Error(ex.ToString());
            }
            return true;
        }

        /// <summary>
        /// 停止连续配方 停止是否 判定当前数据是否未发送完毕
        /// </summary>
        /// <returns></returns>
        public bool StopContinueOrder()
        {
            IsSerise = false;
            return true;
        }

        /// <summary>
        /// 连续配方 配方发送 （称量部分）
        /// </summary>
        /// <param name="setBatch">设定车数</param>
        /// <returns></returns>
        public bool TransferNewWeight(PM_OrderTran mdl)
        {
            this.progressValue = 0;
            if (!createTableRPT.CreateTableRPT_All(Convert.ToDateTime(mdl.Savetime)))
            {
                AddMsgToTextBox("", "自动创建报表存储表失败！");
                return false;
            }

            string tb_show_msg = "";
            this.progressValue = 10;
            string msgText = "正在建立PLC数据通道......Connecting to PLC...";
            AddMsgToTextBox(tb_show_msg, msgText);

            // 发送称量开始标志位 （称量位置） 设为1
            bool ExecuteBool = transInstance.TranStartWeightFlag('1', out string errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "称量：发送标志位成功！Weighing: Send flag successfully!");

            // 清空称量设定车数 & 实际车数 设为零
            ExecuteBool = transInstance.TranWeightCarNum('0', out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "称量：车数清空成功！Weighing: Number of batch has been emptied successfully!");

            this.progressValue = 20;
            if (NewuGlobal.SoftConfig.Carbon)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.Carbon, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }
            if (NewuGlobal.SoftConfig.Drug)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.DrugMixer, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Oil)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.Oil, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Pla)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.Plasticizer, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Silane)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.Silane, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Zno)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.Zno, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Rubber)
            {
                ExecuteBool = transInstance.TranWeightData(DevicePartType.Rubber, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }
            AddMsgToTextBox(tb_show_msg, "称量：清空称量数据成功！Weighing: Cleared weighing data, successfully !");

            //发送恒温炼胶数据
            ExecuteBool = transInstance.PlcHwljData(mdl.MaterialID, out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;

            //发送称量数据
            if (NewuGlobal.SoftConfig.Rubber)
            {
                ExecuteBool = transInstance.PlcRubberData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }
            if (NewuGlobal.SoftConfig.Pla)
            {
                ExecuteBool = transInstance.PlcPlaData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Silane)
            {
                ExecuteBool = transInstance.PlcSilData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Carbon)
            {
                ExecuteBool = transInstance.PlcCarbonData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Oil)
            {
                ExecuteBool = transInstance.PlcOilDataData1(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Drug)
            {
                ExecuteBool = transInstance.PlcDrugData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            if (NewuGlobal.SoftConfig.Zno)
            {
                ExecuteBool = transInstance.PlcZnoData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
            }

            AddMsgToTextBox(tb_show_msg, "称量：发送称量数据成功！Weighing: Send weighing data successfully!");

            //发送 称量设定车数

            ExecuteBool = transInstance.TranSetWeightCarNum(mdl.SetBatch, mdl.MaterialID, out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "称量：发送设定车数数据成功！Weighing: Send the set number of cars data successfully!");

            this.progressValue = 30;

            //发送 称量结束标志位
            ExecuteBool = transInstance.TranStartWeightFlag('0', out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "称量：所有数据发送完毕！！！！！！Weighing: All data is sent! ! !");

            this.progressValue = 40;
            //拆分订单信息  将本次更新的配方  更新对应 devicePartTran

            devicePartRepository.GetPMOrderToDevicePartTranWeight(mdl, NewuGlobal.GetDevicePartCode(DevicePartType.MixUp), NewuGlobal.GetDevicePartCode(DevicePartType.MixDown));

            AddMsgToTextBox(tb_show_msg, "拆分订单信息完毕！Splitting the order information is complete!");

            ////发送完毕称量数据后  更新 for main dgv
            NewuGlobal.Now_Weight_MaterialID = mdl.MaterialID;
            NewuGlobal.Now_Weight_OrderID = mdl.OrderID;
            NewuGlobal.Now_Weight_OrderName = mdl.MaterialCode;

            this.progressValue = 50;
            return true;
ErrorFlag:
            AddMsgToTextBox(tb_show_msg, "称量：数据发送失败\r\n原因：" + errStr);
            return false;
        }

        /// <summary>
        /// 连续配方 工艺发送 （工艺部分）
        /// </summary>
        /// <param name="setBatch">设定车数</param>
        /// <returns></returns>
        public bool TransferNewMix(PM_OrderTran mdl)
        {
            NewuGlobal.SendFlag = true;
            string tb_show_msg = "";
            // 发送密炼开始标志位 设为1
            bool ExecuteBool = transInstance.TranStartMixFlag('1', out string errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "密炼：标志位成功！Mixing: The flag is successful!");
            this.progressValue = 60;

            /*****   清空密炼区域的数据  ***&****/
            ExecuteBool = transInstance.TranWeightData(DevicePartType.MixUp, out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "密炼：清空密炼数据成功！Mixing: Successfully cleared the Tantra data!");
            this.progressValue = 70;
            ExecuteBool = transInstance.PlcSysData(mdl.MaterialID, out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "密炼：发送系统参数成功！Mixing: Sending system parameters successfully!");

            ExecuteBool = transInstance.PlcMixData(mdl.MaterialID, out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "密炼：发送密炼数据成功！Mixing: Send the Banbury data successfully!");

            //下密炼
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                ExecuteBool = transInstance.PlcSysDataF(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
                AddMsgToTextBox(tb_show_msg, "下密炼：发送系统参数成功！Down Mixing: Sending system parameters successfully!");

                ExecuteBool = transInstance.PlcMixFData(mdl.MaterialID, out errStr);
                if (ExecuteBool == false)
                    goto ErrorFlag;
                AddMsgToTextBox(tb_show_msg, "下密炼：发送密炼数据成功！Down Mixing: Send the Banbury data successfully!");
            }

            //结束标志位
            ExecuteBool = transInstance.TranStartMixFlag('0', out errStr);
            if (ExecuteBool == false)
                goto ErrorFlag;
            AddMsgToTextBox(tb_show_msg, "密炼：发送结束表示位数据成功！Mixing: the end of sending indicates the success of the bit data!");
            this.progressValue = 80;

            //拆分订单信息  将本次更新的配方  更新对应 devicePartTran
            devicePartRepository.GetPMOrderToDevicePartTranMix(mdl, NewuGlobal.GetDevicePartCode(DevicePartType.MixUp), NewuGlobal.GetDevicePartCode(DevicePartType.MixDown));
            this.progressValue = 90;

            AddMsgToTextBox(tb_show_msg, "拆分订单信息完毕！Final Victory!");
            //记录密炼的
            NewuGlobal.Now_MaterialID = mdl.MaterialID;
            NewuGlobal.Now_OrderID = mdl.OrderID;
            NewuGlobal.Now_OrderName = mdl.MaterialCode;
            this.progressValue = 100;

            return true;

ErrorFlag:
            AddMsgToTextBox(tb_show_msg, "密炼：数据发送失败\r\n原因：" + errStr);
            return false;
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="pmOrderTran"></param>
        private void UpdateOrder(PM_OrderTran pmOrderTran)
        {
            NewuGlobal.GetMixState.isRun = true;

            NewuGlobal.GetMixState.runOrderID = pmOrderTran.OrderID;
            pmOrderTran.IsStart = 1;//传送配方后将是否启动改为1
            pmOrderTran.StartTime = DateTime.Now;

            FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
            FormulaMaterial formulaMaterial = formulaMaterialRepository.GetModel(pmOrderTran.MaterialID);
            string where = "MaterialID='" + pmOrderTran.MaterialID + "'";
            List<View_FormulaWeigh> formulaWeighs = viewFormulaWeighRepository.GetModelList(0, where, "DropOrder,WeighOrder asc");
            List<View_FormulaMix> formulaMix = viewFormulaMixRepository.GetList(0, where, "StepOrder asc");
            List<View_FormulaTechParam> formulatechparam = ViewFormulaTechParamRepository.GetModelList(0, where, "TechParamOrder asc");

            Formula formula = new Formula
            {
                FormulaWeights = formulaWeighs,
                FormulaMixs = formulaMix,
                FormulaTechParam = formulatechparam
            };

            string formuladata = JsonConvert.SerializeObject(formula);
            pmOrderTran.Reserve5 = formuladata;
            pmOrderTran.Reserve2 = formulaMaterial.Reserve3; //设定间隔时间

            bool flag = orderTranRepository.Update(pmOrderTran);//更新订单信息  变成已经启动状态！

            if (flag)
            {
                AddMsgToTextBox("", "订单信息已更新！密炼数据下发PLC成功！");
            }
            else
            {
                AddMsgToTextBox("", "订单信息更新失败！密炼数据下发PLC成功！");
            }
        }

        /// <summary>
        /// 发送配方主逻辑 zjq
        /// </summary>
        /// <param name="pmOrderTran"></param>
        /// <returns></returns>
        public bool SendOrder(PM_OrderTran pmOrderTran)  // 就发送该 mdl
        {
            if (TransferNewWeight(pmOrderTran) && TransferNewMix(pmOrderTran))
            {
                UpdateOrder(pmOrderTran);
                if (NewuGlobal.RubyDataChange != null)
                    NewuGlobal.RubyDataChange.RefreshData(true); //主屏

                if (NewuGlobal.MixDataChange != null)
                    NewuGlobal.MixDataChange.RefreshData(true); //副屏

                if (NewuGlobal.MixGridDataChange != null)
                    NewuGlobal.MixGridDataChange.RefreshData(true); //UserControl

                NewuGlobal.SendFlag = false;
                return true;
            }
            else
            {
                NewuGlobal.GetMixState.runOrderID = null;
                NewuGlobal.GetMixState.isRun = false;
                return false;
            }
        }

        private void AddMsgToTextBox(string tb_show_msg, string msg)
        {
            if (OnSentFormulaProgress != null)
            {
                OnSentFormulaProgress(this, new FormulaTranEventArgs
                {
                    Message = msg
                });
            }
        }
    }
}