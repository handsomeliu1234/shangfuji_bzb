using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_Curve")]
    public class RPT_Curve
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string CurveID { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int UpdateTotal { get; set; }
        public string OrderID { get; set; }
        public string DeviceCode { get; set; }
        public string MaterialCode { get; set; }
        public string VersionNo { get; set; }
        public string Lot { get; set; }
        public int PlanQty { get; set; }
        public int FactOrder { get; set; }
        public string RealTime { get; set; }
        public string Temp { get; set; }
        public string Power { get; set; }
        public string Press { get; set; }
        public string Energy { get; set; }
        public string Speed { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public int VersionID { get; set; }
        public int Is_Read { get; set; }
        public DateTime ReadTime { get; set; }
    }
}