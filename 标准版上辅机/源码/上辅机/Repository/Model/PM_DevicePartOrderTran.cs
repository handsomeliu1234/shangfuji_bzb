using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("PM_DevicePartOrderTran")]
    public class PM_DevicePartOrderTran
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string DevicePartOrderTranID { get; set; }

        public string OrderID { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceID { get; set; }
        public string DevicePartCode { get; set; }
        public string DevicePartID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialID { get; set; }
        public string VersionNo { get; set; }
        public string Lot { get; set; }
        public int SetBatch { get; set; }
        public DateTime Savetime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}