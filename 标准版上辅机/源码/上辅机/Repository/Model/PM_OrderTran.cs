using System;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("PM_OrderTran")]
    public class PM_OrderTran
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string OrderID
        {
            get; set;
        }

        public string DeviceID
        {
            get; set;
        }

        public string DeviceName
        {
            get; set;
        }

        public string MaterialID
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

        public string FormulaHostoryID
        {
            get; set;
        }

        public string OrderFrom
        {
            get; set;
        }

        public int SerialNumber
        {
            get; set;
        }

        public string Lot
        {
            get; set;
        }

        public int SetBatch
        {
            get; set;
        }

        public int IsStart
        {
            get; set;
        }

        public int IsDelete
        {
            get; set;
        }

        public string StartUserID
        {
            get; set;
        }

        public string StartUserCode
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

        public DateTime Savetime
        {
            get; set;
        }

        public DateTime? StartTime
        {
            get; set;
        }

        public DateTime? EndTime
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

        public string Reserve4
        {
            get; set;
        }

        public string Reserve5
        {
            get; set;
        }
    }
}