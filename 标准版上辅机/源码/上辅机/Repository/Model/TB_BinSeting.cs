using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_BinSeting")]
    public class TB_BinSeting
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string BinID { get; set; }

        public int BinNo { get; set; }
        public string DeviceID { get; set; }
        public string MaterialID { get; set; }
        public string TypeCodeID { get; set; }
        public decimal PreSetKuai { get; set; }
        public decimal PreSetZhong { get; set; }
        public decimal PreSetTiQian { get; set; }
        public decimal PreSetWuUp { get; set; }
        public decimal PreSetWuDown { get; set; }
        public decimal FrequenceUp { get; set; }
        public decimal FrequenceMid { get; set; }
        public decimal FrequenceDown { get; set; }
        public string SaveUserID { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }

        public string MaterialCode { get; set; }
    }
}