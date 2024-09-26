using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    /// <summary>
    /// 设备停起记录
    /// </summary>
    [Dapper.Contrib.Extensions.Table("RPT_DeviceEvent")]
    public class RPT_DeviceEvent
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string DeviceEventID
        {
            get; set;
        }

        public string DeviceCode
        {
            get; set;
        }

        public string EventType
        {
            get; set;
        }

        public string MaterialCode
        {
            get; set;
        }

        public string VersionNo
        {
            get; set;
        }

        public string OrderID
        {
            get; set;
        }

        public string Lot
        {
            get; set;
        }

        public int? PlanQty
        {
            get; set;
        }

        public int? FactOrder
        {
            get; set;
        }

        /// <summary>
        /// 配方时间
        /// </summary>
        public int? UseTime
        {
            get; set;
        }

        public DateTime InitTime
        {
            get; set;
        }

        public DateTime StartTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public string WorkGroup
        {
            get; set;
        }

        public string WorkOrder
        {
            get; set;
        }

        public string WorkerUserCode
        {
            get; set;
        }

        public decimal? Temp
        {
            get; set;
        }

        public decimal? Power
        {
            get; set;
        }

        public decimal? Energy
        {
            get; set;
        }

        public decimal? Speed
        {
            get; set;
        }

        public decimal? Press
        {
            get; set;
        }

        public string PmMode
        {
            get; set;
        }

        public string Reserve1
        {
            get; set;
        }

        public string Reserve2
        {
            get; set;
        }

        public string Reserve3
        {
            get; set;
        }

        /// <summary>
        /// 投胶时间
        /// </summary>
        public string Reserve4
        {
            get; set;
        }

        /// <summary>
        /// 炼胶时间
        /// </summary>
        public string Reserve5
        {
            get; set;
        }

        public int VersionID
        {
            get; set;
        }

        public int Is_Read
        {
            get; set;
        }

        public DateTime ReadTime
        {
            get; set;
        }

        /// <summary>
        /// 间隔时间
        /// </summary>
        public string IntervalTime
        {
            get; set;
        }
    }
}