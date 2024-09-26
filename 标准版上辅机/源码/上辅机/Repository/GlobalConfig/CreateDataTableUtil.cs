using Dapper;
using Repository.DAL;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.GlobalConfig
{
    public class CreateDataTableUtil : BaseDAL<EmptyModel>
    {
        private DbHelperSQL _dbData = new DbHelperSQL(ConnType.NewuSoftData);

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public bool IsExist(string tabName)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    if (tabName.Contains("["))
                    {
                        tabName = tabName.Substring(1, tabName.Length - 2);
                    }

                    string sqlStr = string.Format(@"select Name from sysobjects where name = @name");
                    List<string> list = connection.Query<string>(sqlStr, new
                    {
                        name = tabName
                    }).AsList();
                    if (list.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("CreateDataTableUtil").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 拷贝表结构到新表
        /// </summary>
        /// <param name="newTabName"></param>
        /// <param name="oldTabName"></param>
        public bool CreateTable(string newTabName, string oldTabName, string dbMain, string dbData)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    if (newTabName.Contains("["))
                    {
                        newTabName = newTabName.Substring(1, newTabName.Length - 2);
                    }

                    if (oldTabName.Contains("["))
                    {
                        oldTabName = oldTabName.Substring(1, oldTabName.Length - 2);
                    }

                    string sqlStr = string.Format(@"select * into [{0}].[dbo].[{1}] from [{2}].[dbo].[{3}] where 1=2; ", dbData, newTabName, dbMain, oldTabName);
                    sqlStr += $"  CREATE nonclustered INDEX IX_index on [{newTabName}]([OrderID],[FactOrder])";
                    connection.Query<int>(sqlStr).AsList();
                    //再次查询表是否存在
                    if (IsExist(newTabName))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("CreateDataTableUtil").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 数据库是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExistDB(string dbName)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    string sqlStr = "select Name from sys.sysdatabases where name = @name";
                    List<int> list = connection.Query<int>(sqlStr, new
                    {
                        name = dbName
                    }).AsList();
                    if (list.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("CreateDataTableUtil").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbName"></param>
        public void CreateDB(string path, string dbName)
        {
            try
            {
                if (IsExistDB(dbName))
                {
                    return;
                }
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("CREATE DATABASE " + dbName + " ON  PRIMARY ");
                sql.AppendLine("( NAME = N'" + dbName + "', FILENAME = N'" + path + "\\" + dbName + ".mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )");
                sql.AppendLine(" LOG ON ");
                sql.AppendLine("( NAME = N'" + dbName + "_log', FILENAME = N'" + path + "\\" + dbName + "_log.ldf' , SIZE = 5120KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)");
                int es = _dbData.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("CreateDataTableUtil").Error(ex.ToString());
            }
        }
    }
}