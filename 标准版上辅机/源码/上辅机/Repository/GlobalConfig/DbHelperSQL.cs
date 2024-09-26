using Repository.Model;
using System.Data;
using System.Data.SqlClient;

namespace Repository.GlobalConfig
{
    /// <summary>
    /// 数据访问基础类 Copyright (C) Newu
    /// 发送配方的时候调用创建新表
    /// </summary>
    public class DbHelperSQL
    {
        public ConnDbInfo DB_INFO = new ConnDbInfo();

        public string ConnectionString
        {
            get
            {
                //数据库连接字符串(web.config来配置)，多数据库可使用DbHelperSQLP来实现.
                return string.Format(@"Data Source={0}; Initial Catalog = {1}; User Id = {2}; Password = {3};", DB_INFO.DB_IP, DB_INFO.DB_NAME, DB_INFO.DB_USER, DB_INFO.DB_PASS);
            }
        }

        public DbHelperSQL()
        {
            ReadConnfig(ConnType.NewuAutomation);
        }

        public DbHelperSQL(ConnType dbConn)
        {
            ReadConnfig(dbConn);
        }

        private void ReadConnfig(ConnType dbConn)
        {
            NewuCommon.XmlHelper help = new NewuCommon.XmlHelper();

            string connNode = "";

            switch (dbConn)
            {
                case ConnType.NewuAutomation:
                    connNode = "NewuAutomation";
                    break;

                case ConnType.NewuSoftData:
                    connNode = "NewuSoftData";
                    break;

                default:
                    break;
            }

            System.Xml.XmlNode node = help.ReadXmlNode(NewuCommon.FunClass.CurrentPath + "\\SoftConfig.xml", "Config//" + connNode);
            DB_INFO.DB_IP = node.Attributes["IP"].Value.ToString();
            DB_INFO.DB_NAME = node.Attributes["DB"].Value.ToString();
            DB_INFO.DB_USER = node.Attributes["USER"].Value.ToString();
            DB_INFO.DB_PASS = node.Attributes["PASS"].Value.ToString();
        }

        #region 执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        NewuGlobal.LogCat("DbHelperSQL").Error(e.ToString());
                        return 0;
                    }
                }
            }
        }

        public DataSet Query(string SQLString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        connection.Open();
                        SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                        command.Fill(ds, "ds");
                    }
                    catch (SqlException ex)
                    {
                        NewuGlobal.LogCat("DbHelperSQL").Error(ex.ToString());
                    }
                    return ds;
                }
            }
            catch (System.Exception ex)
            {

                NewuGlobal.LogCat("DbHelperSQL").Error(ex.ToString());
                return null;
            }
           
        }

        #endregion 执行简单SQL语句
    }
}