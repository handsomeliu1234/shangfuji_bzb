using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_Alarm")]
    public class TB_Alarm
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string AlarmID { get; set; }

        public string DeviceID { get; set; }
        public string DevicePartID { get; set; }
        public string AlarmInfo { get; set; }
        public int MemoryAddr { get; set; }
        public string TagAddress { get; set; }
        public int IsDisplay { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}