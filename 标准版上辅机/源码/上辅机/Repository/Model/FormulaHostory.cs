using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("FormulaHostory")]
    public class FormulaHostory
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string FormulaHostoryID { get; set; }

        public string MaterialID { get; set; }
        public int IsActive { get; set; }
        public string SaveRealName { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}