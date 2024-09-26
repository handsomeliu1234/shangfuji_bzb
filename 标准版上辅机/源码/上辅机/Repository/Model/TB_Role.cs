using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_Role")]
    public class TB_Role
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string RoleID { get; set; }

        public string RoleName { get; set; }
        public string CreateTime { get; set; }
        public string RoleRemark { get; set; }
        public string SaveUserID { get; set; }
        public DateTime SaveTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}