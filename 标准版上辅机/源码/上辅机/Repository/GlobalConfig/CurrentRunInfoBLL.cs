using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Newu;
using Repository.Model;
using Repository.Repository;

namespace Repository.GlobalConfig
{
    public class CurrentRunInfoBLL
    {
        public struct ScaleBatch
        {
            /// <summary>
            /// 设定车数
            /// </summary>
            public int SetBatch { get; set; }

            /// <summary>
            /// 实际车数
            /// </summary>
            public int RealBatch { get; set; }

            /// <summary>
            /// 投料次数和称量序号
            /// </summary>
            public int WeightNo { get; set; }
        }

        public delegate void ExeVoidMethod(FormulaPartEnum part, PM_OrderTran model);

        public delegate void ScaleRawChange(DevicePartType part, ScaleBatch scaleBatch);

        /// <summary>
        /// 称量部分配方变更
        /// </summary>
        public event ExeVoidMethod FormulaChangeEvent;

        /// <summary>
        /// 更换磅秤称量的物料
        /// </summary>
        public event ScaleRawChange ScaleRawChangeEvent;

        private PM_OrderTranRepository orderRepository = new PM_OrderTranRepository();

        private PM_OrderTran _rawOrderModel = new PM_OrderTran();
        private PM_OrderTran _mixOrderModel = new PM_OrderTran();

        public CurrentRunInfoBLL()
        {
            IsHistoryMode = false;
        }

        public CurrentRunInfoBLL(bool _isHistoryMode)
        {
            IsHistoryMode = true;
        }

        /// <summary>
        /// 称量部分当前生产订单
        /// </summary>
        public PM_OrderTran OrderRawModel
        {
            get { return _rawOrderModel; }
            set
            {
                if (value != null)
                {
                    _rawOrderModel = value;

                    if (FormulaChangeEvent != null)
                    {
                        FormulaChangeEvent(FormulaPartEnum.Raw, _rawOrderModel);
                    }
                }
            }
        }

        /// <summary>
        /// 密炼部分当前生产订单
        /// </summary>
        public PM_OrderTran OrderMixModel
        {
            get { return _mixOrderModel; }
            set
            {
                if (value != null)
                {
                    _mixOrderModel = value;
                    if (FormulaChangeEvent != null)
                    {
                        FormulaChangeEvent(FormulaPartEnum.Mix, _mixOrderModel);
                    }
                }
            }
        }

        /// <summary>
        /// 是否历史回放模式
        /// </summary>
        public bool IsHistoryMode { get; set; }

        /// <summary>
        /// 初始化生产数据
        /// </summary>
        /// <returns></returns>
        public bool InitData()
        {
            string rawDevicePartId = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.Rubber));
            string mixDevicePartId = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));

            List<PM_OrderTran> rawOrderLists = orderRepository.GetModelJoinDevicePartOrderTran(NewuGlobal.SoftConfig.DeviceID, rawDevicePartId);

            List<PM_OrderTran> mixOrderLists = orderRepository.GetModelJoinDevicePartOrderTran(NewuGlobal.SoftConfig.DeviceID, mixDevicePartId);

            if (mixOrderLists.Count == 1)
            {
                OrderMixModel = mixOrderLists[0];
                NewuGlobal.Now_MaterialID = OrderMixModel.MaterialID;
                NewuGlobal.Now_OrderID = OrderMixModel.OrderID;
                NewuGlobal.Now_OrderName = OrderMixModel.MaterialCode;
                if (rawOrderLists.Count == 1)
                {
                    OrderRawModel = rawOrderLists[0];
                    NewuGlobal.Now_Weight_MaterialID = OrderRawModel.MaterialID;
                    NewuGlobal.Now_Weight_OrderID = OrderRawModel.OrderID;
                    NewuGlobal.Now_Weight_OrderName = OrderRawModel.MaterialCode;
                }
            }
            else
            {
                return false;
            }

            if (mixOrderLists.Count == 1)
            {
                OrderMixModel = mixOrderLists[0];
            }
            else
            {
                return false;
            }

            return true;
        }

        private Dictionary<string, ScaleBatch> ScaleRunDic = new Dictionary<string, ScaleBatch>();

        /// <summary>
        /// 将 磅秤设定车数、实际车数、称量序号 纳入到监控
        /// </summary>
        /// <param name="part"></param>
        /// <param name="setBatch"></param>
        /// <param name="realBatch"></param>
        /// <param name="weightNo"></param>
        public void ScaleRealTimeRun(DevicePartType part, int setBatch, int realBatch, int weightNo)
        {
            if (ScaleRunDic.Count == 0)
            {
                foreach (string item in Enum.GetNames(typeof(DevicePartType)))
                {
                    ScaleRunDic.Add(item, new ScaleBatch());
                }
            }

            string key = part.ToString();
            ScaleBatch temp = ScaleRunDic[key];
            if (temp.SetBatch != setBatch || temp.RealBatch != realBatch || temp.WeightNo != weightNo)
            {
                temp.SetBatch = setBatch;
                temp.RealBatch = realBatch;
                temp.WeightNo = weightNo;

                if (ScaleRawChangeEvent != null) ScaleRawChangeEvent(part, temp);
            }
        }
    }
}