using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_AlarmLog")]
    public class RPT_AlarmLog
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string AlarmLogID { get; set; }

        public string OrderID { get; set; }
        public string MaterialCode { get; set; }
        public string Lot { get; set; }
        public int PlanQty { get; set; }
        public int FactOrder { get; set; }
        public string DeviceName { get; set; }
        public string DevicePartName { get; set; }
        public string AlarmInfo { get; set; }
        public string WorkGroup { get; set; }
        public string WorkOrder { get; set; }
        public string MemoryAddr { get; set; }
        public string AlarmState { get; set; }
        public string SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}