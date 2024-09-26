using System;

namespace Repository.Model
{
    /// <summary>
    /// 订单统计值,用于生产效率统计
    /// </summary>
    public class OrderStatistics
    {
        /// <summary>
        /// 设备
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 配方名称
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 配方ID
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string Lot { get; set; }
        /// <summary>
        /// 设定车数
        /// </summary>
        public int SetBatch { get; set; }
        /// <summary>
        /// 实际车数
        /// </summary>
        public int RealBatch { get; set; }
        /// <summary>
        /// 平均炼胶时间
        /// </summary>
        public int AvgLianJiaoTime { get; set; }
        /// <summary>
        /// 平均配方时间
        /// </summary>
        public int AvgFormulaTime { get; set; }
        /// <summary>
        /// 平均间隔时间
        /// </summary>
        public int AvgJianGeTime { get; set; }
        /// <summary>
        /// 设定间隔时间
        /// </summary>
        public int SetJianGeTime { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public int RealTime { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? SaveTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public int ProduceTime { get; set; }
        /// <summary>
        /// 生产效率
        /// </summary>
        public string ProductionEfficeTive { get; set; }
        /// <summary>
        /// 平均能量
        /// </summary>
        public decimal AvgEnergy { get; set; }
        /// <summary>
        /// 设定重量
        /// </summary>
        public decimal SetWeight { get; set; }
        /// <summary>
        /// 配方间隔时间
        /// </summary>
        public double MaterialUseTime { get; set; }
    }

    /// <summary>
    /// 平均生产效率
    /// </summary>
    public class AvgOrderStatistics
    {
        public double AvgRealTime { get; set; }  //平均有效时间
        public double AvgProduceTime { get; set; } //平均生产时间
        public string AvgProductionEfficeTive { get; set; }//平均有效时间
    }
}
