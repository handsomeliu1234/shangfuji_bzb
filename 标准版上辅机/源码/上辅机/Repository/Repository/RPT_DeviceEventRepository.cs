using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Repository
{
    public class RPT_DeviceEventRepository : BaseDAL<RPT_DeviceEvent>
    {
        /// <summary>
        /// 表名前缀年份
        /// </summary>
        public int TableYear
        {
            get; set;
        }

        /// <summary>
        ///
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
        ///
        /// </summary>
        public RPT_DeviceEventRepository()
        {
        }

        public void Add(string date, RPT_DeviceEvent model)
        {
            try
            {
                TableYear = int.Parse(date);
                using (IDbConnection connection = ConnectionXFData)
                {
                    string sqlStr = string.Format(@"insert into [{0}] (
                                                        DeviceEventID,
                                                        DeviceCode,
                                                        EventType,
                                                        MaterialCode,
                                                        VersionNo,
                                                        OrderID,
                                                        Lot,
                                                        PlanQty,
                                                        FactOrder,
                                                        UseTime,
                                                        InitTime,
                                                        StartTime,
                                                        EndTime,
                                                        WorkGroup,
                                                        WorkOrder,
                                                        WorkerUserCode,
                                                        Temp,
                                                        Power,
                                                        Energy,
                                                        Speed,
                                                        Press,
                                                        PmMode,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5,
                                                        VersionID,
                                                        Is_Read,
                                                        IntervalTime)
                                                    values (
                                                        NEWID(),
                                                        @DeviceCode,
                                                        @EventType,
                                                        @MaterialCode,
                                                        @VersionNo,
                                                        @OrderID,
                                                        @Lot,
                                                        @PlanQty,
                                                        @FactOrder,
                                                        @UseTime,
                                                        @InitTime,
                                                        @StartTime,
                                                        @EndTime,
                                                        @WorkGroup,
                                                        @WorkOrder,
                                                        @WorkerUserCode,
                                                        @Temp,
                                                        @Power,
                                                        @Energy,
                                                        @Speed,
                                                        @Press,
                                                        @PmMode,
                                                        @Reserve1,
                                                        @Reserve2,
                                                        @Reserve3,
                                                        @Reserve4,
                                                        @Reserve5,
                                                        @VersionID,
                                                        @Is_Read,
                                                        @IntervalTime)", GetTableName(typeof(RPT_DeviceEvent)));
                    int effectRow = connection.Execute(sqlStr, model);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventRepository").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 获取订单作业数据
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<RPT_DeviceEvent> GetOrderRunData(string orderid)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    string query = string.Format(@"Select * from [{0}] where OrderID = @OrderID and EventType = '作业' order by FactOrder", GetTableName(typeof(RPT_DeviceEvent)));
                    return dbConnection.Query<RPT_DeviceEvent>(query, new
                    {
                        OrderID = orderid
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 订单的平均数据
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<OrderAvgData> GetAvgOrderData(string orderid)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    string query = string.Format(@"Select AVG(UseTime) as AvgUseTime,AVG(CAST(Reserve5 as int)) as AvgLianJiaoTime,AVG(CAST(WorkGroup as int)) as AvgJianGeTime,COUNT(FactOrder) as FactNum from [{0}] where OrderID = @OrderID and EventType = '作业'", GetTableName(typeof(RPT_DeviceEvent)));
                    return dbConnection.Query<OrderAvgData>(query, new
                    {
                        OrderID = orderid
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_DeviceEvent> GetList(string strWhere, DateTime dt)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = dt.Year;
                    string strSql = string.Format(@"select DeviceEventID, DeviceCode, EventType, MaterialCode, VersionNo, OrderID, Lot, PlanQty, FactOrder, UseTime, InitTime, StartTime, EndTime, WorkGroup, WorkOrder, WorkerUserCode, Temp, Power, Energy, Speed, Press, PmMode, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime,IntervalTime FROM [{0}]", GetTableName(typeof(RPT_DeviceEvent)));
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    return dbConnection.Query<RPT_DeviceEvent>(strSql).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventRepository").Error(ex.ToString());
                return null;
            }
        }

        public DataTable GetDeviceEventTable(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    DataTable dt = new DataTable();
                    string strSql = string.Format(@"select DeviceEventID, DeviceCode, EventType, MaterialCode, VersionNo, OrderID, Lot, PlanQty, FactOrder, UseTime, InitTime, StartTime, EndTime, WorkGroup, WorkOrder, WorkerUserCode, Temp, Power, Energy, Speed, Press, PmMode, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime,IntervalTime FROM [{0}]", GetTableName(typeof(RPT_DeviceEvent)));
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    strSql += ("order by FactOrder");
                    var reader = dbConnection.ExecuteReader(strSql);
                    dt.Load(reader);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_DeviceEvent> GetList(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string strSql = string.Format(@"select DeviceEventID, DeviceCode, EventType, MaterialCode, VersionNo, OrderID, Lot, PlanQty, FactOrder, UseTime, InitTime, StartTime, EndTime, WorkGroup, WorkOrder, WorkerUserCode, Temp, Power, Energy, Speed, Press, PmMode, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,IntervalTime FROM [{0}]", GetTableName(typeof(RPT_DeviceEvent)));
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    return dbConnection.Query<RPT_DeviceEvent>(strSql).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventRepository").Error(ex.ToString());
                return null;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class OrderAvgData
    {
        public int AvgUseTime
        {
            get; set;
        }

        public int AvgLianJiaoTime
        {
            get; set;
        }

        public int AvgJianGeTime
        {
            get; set;
        }

        public int FactNum
        {
            get; set;
        }
    }
}