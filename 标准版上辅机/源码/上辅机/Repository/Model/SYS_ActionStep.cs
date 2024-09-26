using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_ActionStep")]
    public class SYS_ActionStep
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string StepCode { get; set; }

        public int StepValue { get; set; }
        public string StepNameCN { get; set; }
        public string StepNameEN { get; set; }
        public string DeviceID { get; set; }
        public string DevicePartID { get; set; }
        public DateTime SaveTime { get; set; }
        public int Enable { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }

        public int StepBit { get; set; }
    }
}