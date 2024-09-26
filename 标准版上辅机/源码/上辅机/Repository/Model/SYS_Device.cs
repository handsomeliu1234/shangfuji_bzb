using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_Device")]
    public class SYS_Device
    {
        public string DeviceID { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string DeviceJaneSpell { get; set; }
        public string DeviceTypeID { get; set; }
        public string DeviceDesc { get; set; }
        public string WorkShopID { get; set; }
        public int Enabled { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public string DeviceTypeName { get; set; }
        public string ShopName { get; set; }
    }
}