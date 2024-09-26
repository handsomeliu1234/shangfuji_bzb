using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class TB_Security
    {
        public string ID { get; set; }
        public string DeviceID { get; set; }
        public string DeviceCode { get; set; }
        public string Tag { get; set; }
        public string Addr { get; set; }
        public bool IsUsed { get; set; }
        public string SecurityDesc { get; set; }
        public string SaveUser { get; set; }
        public DateTime SaveTime { get; set; }
    }
}