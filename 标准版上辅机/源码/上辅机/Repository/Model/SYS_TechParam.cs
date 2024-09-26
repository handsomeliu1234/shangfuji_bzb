using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_TechParam")]
    public class SYS_TechParam
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string TechParamID { get; set; }

        public string DeviceID { get; set; }
        public string DevicePartID { get; set; }
        public string TechParamNameCN { get; set; }
        public string TechParamNameEN { get; set; }
        public int TechParamOrder { get; set; }
        public int DecDigit { get; set; }
        public string Unit { get; set; }
        public int Enable { get; set; }
        public string SaveUserID { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }

        public decimal TechParamVal { get; set; }
        public string FormulaTechParamID { get; set; }
    }
}