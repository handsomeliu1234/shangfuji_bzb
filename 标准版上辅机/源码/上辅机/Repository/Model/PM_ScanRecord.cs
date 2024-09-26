using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("PM_ScanRecord")]
    public class PM_ScanRecord
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string ScanRecordID { get; set; }

        public string DeviceID { get; set; }
        public string DeviceCode { get; set; }
        public string OrderID { get; set; }
        public string MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string TypeCodeName { get; set; }
        public string PortBarcode { get; set; }
        public string MatBarcode { get; set; }
        public int BatchOrder { get; set; }
        public string WorkGroup { get; set; }
        public string WorkOrder { get; set; }
        public string SaveUserID { get; set; }
        public string SaveUserCode { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }

        public string DeviceName { get; set; }
        public int VersionID { get; set; }
        public int Is_Read { get; set; }
        public DateTime ReadTime { get; set; }
    }

    public class WorkType
    {
        public int BinNo;
        public string BinID;
        public string TypeCodeID;
        public string MaterialID;
        public string MaterialCode;
        public string DeviceID;
        public string DeviceCode;
        public string PortBarcode;
        public int che;
        public DateTime STime;
    }
}