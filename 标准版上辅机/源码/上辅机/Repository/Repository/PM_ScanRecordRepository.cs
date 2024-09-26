using Dapper;
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
    public class PM_ScanRecordRepository : BaseDAL<PM_ScanRecord>
    {
        public bool Add(PM_ScanRecord model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into PM_ScanRecord(
                                                    ScanRecordID,
                                                    DeviceID,
                                                    DeviceCode,
                                                    OrderID,
                                                    MaterialID,
                                                    MaterialCode,
                                                    TypeCodeName,
                                                    PortBarcode,
                                                    MatBarcode,
                                                    BatchOrder,
                                                    WorkGroup,
                                                    WorkOrder,
                                                    SaveUserID,
                                                    SaveUserCode,
                                                    SaveTime,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5,
                                                    VersionID,
                                                    Is_Read,
                                                    ReadTime
                                                values(
                                                    NEWID();
                                                    @ScanRecordID,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @OrderID,
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @TypeCodeName,
                                                    @PortBarcode,
                                                    @MatBarcode,
                                                    @BatchOrder,
                                                    @WorkGroup,
                                                    @WorkOrder,
                                                    @SaveUserID,
                                                    @SaveUserCode,
                                                    @SaveTime,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5,
                                                    @VersionID,
                                                    @Is_Read,
                                                    @ReadTime
                                                              )");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_ScanRecordRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<PM_ScanRecord> GetList(string where)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format("select ScanRecordID, DeviceID, DeviceCode, OrderID, MaterialID, MaterialCode, TypeCodeName, PortBarcode, MatBarcode, BatchOrder, WorkGroup, WorkOrder, SaveUserID, SaveUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime FROM [dbo].[PM_ScanRecord] Where " + where);
                    List<PM_ScanRecord> pM_ScanRecord = connection.Query<PM_ScanRecord>(sqlStr).AsList();
                    return pM_ScanRecord;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_ScanRecordRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_ScanRecord> QueryData(string deviceid, DateTime start, DateTime end)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select a.ScanRecordID,a.DeviceID,a.DeviceCode,a.OrderID,a.MaterialID,a.MaterialCode,a.TypeCodeName,a.PortBarcode,a.MatBarcode,a.BatchOrder,a.WorkGroup,a.WorkOrder,a.SaveUserID,a.SaveUserCode,a.SaveTime,a.Reserve1,a.Reserve2,a.Reserve3,a.Reserve4,a.Reserve5,a.VersionID,a.Is_Read,a.ReadTime,");
                    strSql.Append("b.DeviceName  ");
                    strSql.Append(" from PM_ScanRecord a,SYS_Device b ");
                    strSql.Append("where a.DeviceID=b.DeviceID ");

                    if (!string.IsNullOrEmpty(deviceid.Trim()))
                    {
                        strSql.Append(" and a.DeviceID = '" + deviceid + "' ");
                    }
                    strSql.Append("and a.SaveTime>='" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' and a.SaveTime<='" + end.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    strSql.Append(" order by a.SaveTime ");

                    List<PM_ScanRecord> pM_ScanRecords = connection.Query<PM_ScanRecord>(strSql.ToString()).AsList();
                    return pM_ScanRecords;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_ScanRecordRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_ScanRecord> QueryDetialData(string deviceid, DateTime start, DateTime end)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select a.DeviceCode,a.TypeCodeName,a.MaterialCode,a.MatBarcode,a.SaveUserCode,a.SaveTime,a.Reserve1,");
                    strSql.Append("b.DeviceName  ");
                    strSql.Append(" from PM_ScanRecord a,SYS_Device b ");
                    strSql.Append("where a.DeviceID=b.DeviceID ");

                    if (!string.IsNullOrEmpty(deviceid.Trim()))
                    {
                        strSql.Append(" and a.DeviceID = '" + deviceid + "' ");
                    }
                    strSql.Append("and a.SaveTime>='" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' and a.SaveTime<='" + end.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    strSql.Append(" order by a.SaveTime ");

                    List<PM_ScanRecord> pM_ScanRecords = connection.Query<PM_ScanRecord>(strSql.ToString()).AsList();
                    return pM_ScanRecords;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_ScanRecordRepository").Error(ex.ToString());
                return null;
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
        public List<PM_ScanRecord> Paging(string tableName, string pk, string queryCol, string strWhere, string pageindex, string pagesize, string sortfield, out int pagecount)
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
                    List<PM_ScanRecord> dsList = reader.Read<PM_ScanRecord>().ToList();
                    pagecount = reader.Read<int>().Single();
                    return dsList;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_ScanRecordRepository").Error(ex.ToString());
                pagecount = 0;
                return null;
            }
        }
    }
}