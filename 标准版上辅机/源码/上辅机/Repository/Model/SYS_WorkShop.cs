using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_WorkShop")]
    public class SYS_WorkShop
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string WorkshopID { get; set; }

        public string FactoryID { get; set; }
        public string ShopName { get; set; }
        public string WorkshopCode { get; set; }
        public string WorkshopJaneSpell { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }

        public string FactoryName { get; set; }
    }
}