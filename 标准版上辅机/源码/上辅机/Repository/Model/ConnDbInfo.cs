namespace Repository.Model
{
    public enum ConnType
    {
        NewuAutomation,
        NewuSoftData
    }

    public class ConnDbInfo
    {
        /// <summary>
        /// 数据库IP
        /// </summary>
        public string DB_IP { get; set; }

        /// <summary>
        /// 数据名称
        /// </summary>
        public string DB_NAME { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public string DB_USER { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string DB_PASS { get; set; }
    }
}