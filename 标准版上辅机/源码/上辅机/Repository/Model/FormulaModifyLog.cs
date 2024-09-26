using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    /// <summary>
    /// 配方修改记录
    /// </summary>
    public class FormulaModifyLog
    {
        public string FormulaModifyLogID { get; set; }
        public string DeviceID { get; set; }
        public string DeviceCode { get; set; }
        public string FormulaID { get; set; }
        public string FormulaCode { get; set; }
        public string WeightInfoBefore { get; set; }
        public string TechInfoBefore { get; set; }
        public string WeightInfoAfter { get; set; }
        public string TechInfoAfter { get; set; }
        public DateTime SaveTime { get; set; }
        public string SaveUser { get; set; }
    }
}