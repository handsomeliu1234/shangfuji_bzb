using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_Factory")]
    public class SYS_Factory
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string FactoryID { get; set; }

        public string FactoryName { get; set; }
        public string FactoryCode { get; set; }
        public string FactoryJaneSpell { get; set; }
        public string FactorySite { get; set; }
        public string FactoryPhone { get; set; }
        public string FactoryEmail { get; set; }
        public string FactoryAddress { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}