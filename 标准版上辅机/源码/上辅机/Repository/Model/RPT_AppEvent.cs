using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_AppEvent")]
    public class RPT_AppEvent
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string AppEventID { get; set; }

        public string AppEventType { get; set; }
        public string OrderID { get; set; }
        public string DeviceCode { get; set; }
        public string MaterialCode { get; set; }
        public string VersionNo { get; set; }
        public string Lot { get; set; }
        public int PlanQty { get; set; }
        public int FactOrder { get; set; }
        public DateTime SaveTime { get; set; }
        public string UserID { get; set; }
        public string SaveRealName { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}