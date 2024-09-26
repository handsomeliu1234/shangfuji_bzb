using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteData
{
    //删除系统数据库表数据类
    public class fmNewuSoft
    {
        private DbHelperSQL _dbData = new DbHelperSQL(ConnType.NewuAutomation);

        //删除系统数据库中的数据
        public int DeleteSysData(string st, string ed, string tableName, string sumCount, string timesubsegment)
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
        /// 获取系统所选数据表中所有数据
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetSysDataSumCount(string tableName)
        {
            string count = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from  [" + tableName + "]  ");
            DataSet ds = _dbData.Query(strSql.ToString());
            count = ds.Tables[0].Rows[0][0].ToString();
            return count;
        }

        /// <summary>
        /// 获取所选区间内数据表中数据
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetSysDataIntervalCount(string st, string ed, string tableName, string timesubsegment)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from  [" + tableName + "]  ");
            strSql.Append($"  where {timesubsegment}>='{st}' and {timesubsegment}<='{ed}'  ");
            DataSet ds = _dbData.Query(strSql.ToString());
            string count = ds.Tables[0].Rows[0][0].ToString();
            return count;
        }
    }
}