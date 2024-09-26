using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    //报表保存信号相关
    public class RptFlagMDL
    {
        public int FlagAddr { get; set; }
        public int BatchAddr { get; set; }

        public int DataAddr { get; set; }

        public double DanWei { get; set; }

        public string Desc { get; set; }
        public bool IsSave { get; set; }
    }
}