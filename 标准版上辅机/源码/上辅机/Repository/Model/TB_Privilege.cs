using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_Privilege")]
    public class TB_Privilege
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string PrivilegeID
        {
            get; set;
        }

        public string MenuID
        {
            get; set;
        }

        public string RoleID
        {
            get; set;
        }

        public int Enable
        {
            get; set;
        }

        public DateTime SaveTime
        {
            get; set;
        }

        public string Reserve1
        {
            get; set;
        }

        public string Reserve2
        {
            get; set;
        }

        public string Reserve3
        {
            get; set;
        }

        public string Reserve4
        {
            get; set;
        }

        public string Reserve5
        {
            get; set;
        }
    }
}