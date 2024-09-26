using Dapper;
using Newu;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class TB_OperateLogRepository : BaseDAL<TB_OperateLog>
    {
        public TB_OperateLogRepository()
        {
        }

        public bool Add(TB_OperateLog model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into TB_OperateLog(
                                                            OperateLogID,
                                                            DeviceID,
                                                            LogInfo,
                                                            LogType,
                                                            UserID,
                                                            SaveTime,
                                                            Reserve1,
                                                            Reserve2,
                                                            Reserve3,
                                                            Reserve4,
                                                            Reserve5
                                                            )
                                                      values (
                                                            NEWID(),
                                                            @DeviceID,
                                                            @LogInfo,
                                                            @LogType,
                                                            @UserID,
                                                            @SaveTime,
                                                            @Reserve1,
                                                            @Reserve2,
                                                            @Reserve3,
                                                            @Reserve4,
                                                            @Reserve5)", model);
                    if (effectRow > 0)
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
                NewuGlobal.LogCat("TB_OperateLogRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string operateLogID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"delete from TB_OperateLog where OperateLogID = @OperateLogID ",
                        new
                        {
                            OperateLogID = operateLogID
                        });
                    if (effectRow > 0)
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
                NewuGlobal.LogCat("TB_OperateLogRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<TB_OperateLog> QueryData(string where, DateTime start, DateTime end)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("SELECT * FROM TB_OperateLog where 1=1 ");
                    if (!string.IsNullOrEmpty(where.Trim()))
                    {
                        sqlStr.Append("and LogInfo like N'%" + where + "%' ");
                    }
                    sqlStr.Append("and SaveTime >= @StartTime and SaveTime <= @EndTime");
                    sqlStr.Append(" order by SaveTime ");
                    List<TB_OperateLog> lsQueryData = dbConnection.Query<TB_OperateLog>(sqlStr.ToString(), new
                    {
                        StartTime = start,
                        EndTime = end
                    }).ToList();
                    if (lsQueryData.Count > 0)
                    {
                        return lsQueryData;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_OperateLogRepository").Error(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 查询备份和还原数据库的记录slj
        /// </summary>
        /// <param name="where"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<TB_OperateLog> QueryBackupOrRestoreData()
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("SELECT * FROM TB_OperateLog where 1=1 ");
                   
                    sqlStr.Append("and LogType = 'BackUp' or LogType = 'Restore'");
                    sqlStr.Append(" order by SaveTime desc ");
                    List<TB_OperateLog> lsQueryData = dbConnection.Query<TB_OperateLog>(sqlStr.ToString()).ToList();
                    if (lsQueryData.Count > 0)
                    {
                        return lsQueryData;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_OperateLogRepository").Error(ex.ToString());
                return null;
            }
        }
        public void SaveAppLog(string logInfo, AppLogType type)
        {
            try
            {
                TB_OperateLog tB_OperateLog = new TB_OperateLog
                {
                    UserID = NewuGlobal.TB_UserInfo.UserCode,
                    SaveTime = DateTime.Now,
                    DeviceID = NewuGlobal.SoftConfig.DeviceID,
                    LogInfo = logInfo,
                    LogType = type.ToString()
                };
                Add(tB_OperateLog);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_OperateLogRepository").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 分页代码
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="pk">主键</param>
        /// <param name="queryCol">查询列</param>
        /// <param name="strWhere">条件</param>
        /// <param name="pageindex">起始页</param>
        /// <param name="pagesize">页码大小</param>
        /// <param name="sortfield">分类</param>
        /// <returns></returns>
        public List<TB_OperateLog> Paging(string tableName, string pk, string queryCol, string strWhere, string pageindex, string pagesize, string sortfield, out int pagecount)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("exec proc_DataPageRowNumber  ");
                    strSql.Append("'" + tableName + "',");
                    strSql.Append("'" + pk + "',");
                    strSql.Append("'" + queryCol + "',");
                    strSql.Append("N'" + strWhere + "',");
                    strSql.Append("'" + pageindex + "',");
                    strSql.Append("'" + pagesize + "',");
                    strSql.Append("'" + sortfield + "' ");
                    var reader = connection.QueryMultiple(strSql.ToString());
                    var dsList = reader.Read<TB_OperateLog>().ToList();
                    pagecount = reader.Read<int>().Single();
                    return dsList;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_OperateLogRepository").Error(ex.ToString());
                pagecount = 0;
                return null;
            }
        }
    }
}