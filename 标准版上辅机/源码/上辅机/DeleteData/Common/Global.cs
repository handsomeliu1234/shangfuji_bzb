using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DeleteData
{

    public static class Global
    {
       

       
        #region SoftConfig软件配制

        private static ConfigManager _softConfig;

        /// <summary>
        /// 读取SoftConfig文件
        /// </summary>
        public static ConfigManager SoftConfig
        {
            get
            {
                if (_softConfig == null)
                {
                    _softConfig = new ConfigManager();
                }
                return _softConfig;
            }
            set
            {
                _softConfig = value;
            }
        }
        #endregion

    }

}
