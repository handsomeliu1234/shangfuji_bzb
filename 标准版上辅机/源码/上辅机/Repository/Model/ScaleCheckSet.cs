using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("ScaleCheckSet")]
    public class ScaleCheckSet
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string ID { get; set; }

        public string DeviceCode { get; set; }
        public int CheckScaleNo { get; set; }
        public string ScaleName { get; set; }
        public string ScaleWeight { get; set; }
        public string SetError { get; set; }
        public string DevicePartCode { get; set; }
        public string DevicePartName { get; set; }

        public string SaveUser { get; set; }
        public DateTime SaveTime { get; set; }
    }
}