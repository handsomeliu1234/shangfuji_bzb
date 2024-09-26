using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_Department")]
    public class TB_Department
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string DepartmentID { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentRemark { get; set; }
        public string ParentDepartmentID { get; set; }
        public string DepartmentJaneSpell { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}