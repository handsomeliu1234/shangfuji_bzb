using System;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Repository.DAL;
using Repository.Helper;
using Repository.Model;

namespace Repository.Repository
{
    public class RPT_BarCodePrintLogRepository : BaseDAL<RPT_BarCodePrintLog>, SqlMapperExtensions.ITableNameMapper
    {
        /// <summary>
        /// 表名前缀年份
        /// </summary>
        public int TableYear { get; set; }

        public RPT_BarCodePrintLogRepository()
        {
        }

        /// <summary>
        /// 没有[]包围的
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTableName(Type type)
        {
            if (TableYear != 0)
            {
                return TableYear.ToString() + "_" + EntityHelper.GetTableName(type);
            }
            else
            {
                return EntityHelper.GetTableName(type);
            }
        }

        /// <summary>
        /// 有[]包围的
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTableNameNew(Type type)
        {
            if (TableYear != 0)
            {
                return "[" + TableYear.ToString() + "_" + EntityHelper.GetTableName(type) + "]";
            }
            else
            {
                return EntityHelper.GetTableName(type);
            }
        }

        /// <summary>
        /// 根据订单id和实际车数
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="realbatch"></param>
        /// <returns></returns>
        public RPT_BarCodePrintLog GetByIndx(string orderid, int realbatch)
        {
            using (IDbConnection dbConnection = ConnectionXFData)
            {
                string query = string.Format(@"Select * from [{0}] where OrderID = @OrderID and RealBatch = @RealBtach", GetTableName(typeof(RPT_BarCodePrintLog)));
                var list = dbConnection.Query<RPT_BarCodePrintLog>(query, new { OrderID = orderid, RealBtach = realbatch }).ToList();
                if (list.Count() == 1)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public new bool Insert(RPT_BarCodePrintLog info)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXFData)
            {
                SqlMapperExtensions.TableNameMapper = GetTableNameNew;
                if (GetByIndx(info.OrderID, info.RealBatch) == null)
                {
                    dbConnection.Insert(info);
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        public bool Create(int year)
        {
            TableYear = year;
            using (IDbConnection dbConnection = ConnectionXFData)
            {
                string tablename = GetTableName(typeof(RPT_BarCodePrintLog));
                string str = string.Format(@"SELECT COUNT(name) from sys.tables where name = @Name");
                int result = dbConnection.ExecuteScalar<int>(str, new { Name = tablename });
                if (result == 0)
                {
                    string query = string.Format(@"CREATE TABLE [{0}](
                                                    [PrintID] [nvarchar](50) NOT NULL PRIMARY KEY,
                                                    [OrderID] [nvarchar](50) NULL,
	                                                [DeviceCode] [nvarchar](50) NULL,
	                                                [FormulaCode] [nvarchar](50) NULL,
	                                                [VersionNo] [nvarchar](50) NULL,
	                                                [RealBatch] [int] NULL,
	                                                [PrintBarCodeContent] [nvarchar](50) NULL,
	                                                [SaveTime] [nvarchar](50) NULL,
	                                                [SaveUser] [nvarchar](50) NULL
                                                    ) ON [PRIMARY]", tablename);
                    dbConnection.Execute(query);
                }
                return true;
            }
        }
    }
}