using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_UserInfo")]
    public class TB_UserInfo
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string UserID
        {
            get; set;
        }

        public string DepartmentID
        {
            get; set;
        }

        public string RoleID
        {
            get; set;
        }

        public string UserCode
        {
            get; set;
        }

        public string UserPassword
        {
            get; set;
        }

        public string RealName
        {
            get; set;
        }

        public string Phone
        {
            get; set;
        }

        public string Jobs
        {
            get; set;
        }

        public DateTime SaveTime
        {
            get; set;
        }

        public string SaveUserID
        {
            get; set;
        }

        public string DeleteMark
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

        #region 扩展属性

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get; set;
        }

        //班组
        public string WorkGroup
        {
            get; set;
        }

        //班次
        public string WorkOrder
        {
            get; set;
        }

        #endregion 扩展属性
    }

    public class SaveUser
    {
        private string _saveUserID;
        private string _realName;

        public SaveUser(string saveUserID, string realName)
        {
            _saveUserID = saveUserID;
            _realName = realName;
        }

        public string SaveUserID
        {
            get
            {
                return _saveUserID;
            }
            set
            {
                _saveUserID = value;
            }
        }

        public string RealName
        {
            get
            {
                return _realName;
            }
            set
            {
                _realName = value;
            }
        }
    }
}