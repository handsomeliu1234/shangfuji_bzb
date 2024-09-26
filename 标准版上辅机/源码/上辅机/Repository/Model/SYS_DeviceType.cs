using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_DeviceType")]
    public class SYS_DeviceType
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string DeviceTypeID { get; set; }

        public string DeviceTypeCode { get; set; }
        public string DeviceTypeName { get; set; }
        public string DeviceTypeJaneSpell { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}