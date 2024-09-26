using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_EveryBinNoFinishtime")]
    public class RPT_EveryBinNoFinishtime
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string TimeId { get; set; }

        public string OrderId { get; set; }
        public string FormulaName { get; set; }
        public string WeightMaterialName { get; set; }
        public int BinNo { get; set; }
        public int FactKuangOrder { get; set; }
        public int FinishTime { get; set; }
    }
}