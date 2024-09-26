using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace DeleteData
{
    //删除报表数据库表数据类
    public class fmNewuSoftData
    {
        DbHelperSQL _dbData = new DbHelperSQL(ConnType.NewuSoftData);
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public bool isExist(string tabName)
        {
            string sql = " select Name from sysobjects where name='" + tabName + "' ";
            DataSet ds = _dbData.Query(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 拷贝表结构到新表
        /// </summary>
        /// <param name="newTabName"></param>
        /// <param name="oldTabName"></param>
        public bool createTable(string newTabName, string oldTabName)
        {
            try
            {
                if (newTabName.Contains("[")) { newTabName = newTabName.Substring(1, newTabName.Length - 2); }
                if (oldTabName.Contains("[")) { oldTabName = oldTabName.Substring(1, oldTabName.Length - 2); }
                if (isExist(newTabName) == true) { return true; }
                string sql = " select * into [test_Data].[dbo].[" + newTabName + "] from [test_Data].[dbo].[" + oldTabName + "] where 1=2 ";
                int es = _dbData.ExecuteSql(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 数据库是否存在
        /// </summary>
        /// <returns></returns>
        public Boolean isExistDB(string dbName)
        {
            string sql = "select Name from sys.sysdatabases where name='" + dbName + "'";
            DataSet ds = _dbData.Query(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbName"></param>
        public void createDB(string path, string dbName)
        {
            if (isExistDB(dbName) == true) { return; }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE DATABASE " + dbName + " ON  PRIMARY ");
            sql.AppendLine("( NAME = N'" + dbName + "', FILENAME = N'" + path + "\\" + dbName + ".mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )");
            sql.AppendLine(" LOG ON ");
            sql.AppendLine("( NAME = N'" + dbName + "_log', FILENAME = N'" + path + "\\" + dbName + "_log.ldf' , SIZE = 5120KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)");
            int es = _dbData.ExecuteSql(sql.ToString());
        }

        //删除报表数据库中的数据
        public int DeleteRprData(string st, string ed, string tableName, string sumCount, string timesubsegment)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("set statistics time on;");
            strSql.Append("declare @Time datetime=getdate();");
            strSql.Append("set ROWCOUNT " + sumCount + " ;");
            strSql.Append("while 1=1");
            strSql.Append("begin ");
            strSql.Append("   begin tran;   ");
            strSql.Append("delete from  [" + tableName + "] ");
            strSql.Append($" where {timesubsegment}>='{st} ' and {timesubsegment}<='{ed} ' ");
            strSql.Append("   commit;");
            strSql.Append("IF @@ROWCOUNT = 0");
            strSql.Append("break;");
            strSql.Append("end ");
            strSql.Append("set ROWCOUNT  0;");
            strSql.Append("select DATEDIFF(MS,@Time,GETDATE())");
            strSql.Append("set statistics time off;");
            DataSet ds = _dbData.Query(strSql.ToString());
            int time;
            if (ds != null)
            {
                time = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                time = 0;
            }
            return time;
        }

        /// <summary>
        /// 获取报表所选数据表中所有数据
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetRptDataSumCount(string tableName)
        {
            string count = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from  [" + tableName + "]  ");
            DataSet ds = _dbData.Query(strSql.ToString());
            count = ds.Tables[0].Rows[0][0].ToString();
            return count;
        }

        /// <summary>
        /// 获取报表所选区间数据表中数据
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetRptDataIntervalCount(string st, string ed, string tableName, string timesubsegment)
        {
            string count = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from  [" + tableName + "]  ");
            strSql.Append($"  where {timesubsegment}>='{st}' and {timesubsegment}<='{ed}'  ");
            DataSet ds = _dbData.Query(strSql.ToString());
            count = ds.Tables[0].Rows[0][0].ToString();
            return count;
        }

        public bool ShrinkLog(string rptdbname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" USE [master] ");
            strSql.Append($" ALTER DATABASE {rptdbname} SET RECOVERY SIMPLE WITH NO_WAIT ");
            strSql.Append($" ALTER DATABASE {rptdbname} SET RECOVERY SIMPLE  ");
            strSql.Append($" USE {rptdbname} ");
            strSql.Append($" DBCC SHRINKFILE (N'{rptdbname}' , 1, TRUNCATEONLY) ");
            strSql.Append($" DBCC SHRINKFILE (N'{rptdbname}_Log' , 1, TRUNCATEONLY) ");
            strSql.Append(" USE [master] ");
            strSql.Append($" ALTER DATABASE {rptdbname} SET RECOVERY FULL WITH NO_WAIT ");
            strSql.Append($" ALTER DATABASE {rptdbname} SET RECOVERY FULL  ");
            int row = _dbData.ExecuteSql(strSql.ToString());
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
