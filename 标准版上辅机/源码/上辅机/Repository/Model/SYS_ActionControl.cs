using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_ActionControl")]
    public class SYS_ActionControl
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string ActionControlCode { get; set; }

        public int ActionControlValue { get; set; }
        public string ActionControlNameCN { get; set; }
        public string ActionControlNameEN { get; set; }
        public string DeviceID { get; set; }
        public string SaveUserID { get; set; }
        public DateTime SaveTime { get; set; }
        public int Enable { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public string DeviceName { get; set; }
    }
}