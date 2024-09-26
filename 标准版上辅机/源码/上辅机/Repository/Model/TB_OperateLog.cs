using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_OperateLog")]
    public class TB_OperateLog
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string OperateLogID
        {
            get; set;
        }

        public string DeviceID
        {
            get; set;
        }

        public string LogInfo
        {
            get; set;
        }

        public string LogType
        {
            get; set;
        }

        public string UserID
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