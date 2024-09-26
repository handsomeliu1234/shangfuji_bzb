using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_DevicePart")]
    public class SYS_DevicePart
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string DevicePartID
        {
            get; set;
        }

        public string DeviceTypeID
        {
            get; set;
        }
        public string DevicePartCode
        {
            get; set;
        }
        public string DevicePartName
        {
            get; set;
        }
        public string DevicePartJaneSpell
        {
            get; set;
        }
        public DateTime SaveTime
        {
            get; set;
        }
        public int DevicePartNumber
        {
            get; set;
        }
        public int Enable
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

        public string OrderID
        {
            get; set;
        }
        public string MaterialCode
        {
            get; set;
        }
        public string MaterialID
        {
            get; set;
        }
        public string VersionNo
        {
            get; set;
        }
        public string Lot
        {
            get; set;
        }
        public string SetBatch
        {
            get; set;
        }

        public string DeviceCode
        {
            get; set;
        }
        public string DeviceID
        {
            get; set;
        }
        public int PartNum
        {
            get; set;
        }
    }
}