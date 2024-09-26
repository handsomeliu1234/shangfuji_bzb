using Repository.DAL;
using Repository.Model;
using System;
using Repository.Helper;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using Repository.GlobalConfig;

namespace Repository.Repository
{
    public class RPT_DeviceEventFRepository : BaseDAL<RPT_DeviceEventF>
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
        public RPT_DeviceEventFRepository()
        {
        }

        public void Add(int year, RPT_DeviceEventF model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = year;
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
                                                        @IntervalTime)", GetTableName(typeof(RPT_DeviceEventF)));
                    int effectRow = connection.Execute(sqlStr, model);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventFRepository").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 获取订单作业数据
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<RPT_DeviceEventF> GetOrderRunData(string orderid)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    string query = string.Format(@"Select * from [{0}] where OrderID = @OrderID and EventType = '作业' order by FactOrder", GetTableName(typeof(RPT_DeviceEventF)));
                    return dbConnection.Query<RPT_DeviceEventF>(query, new
                    {
                        OrderID = orderid
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventFRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 订单的平均数据
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<OrderAvgDataF> GetAvgOrderData(string orderid)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    string query = string.Format(@"Select AVG(UseTime) as AvgUseTime,AVG(CAST(Reserve5 as int)) as AvgLianJiaoTime,AVG(CAST(WorkGroup as int)) as AvgJianGeTime,COUNT(FactOrder) as FactNum from [{0}] where OrderID = @OrderID and EventType = '作业'", GetTableName(typeof(RPT_DeviceEventF)));
                    return dbConnection.Query<OrderAvgDataF>(query, new
                    {
                        OrderID = orderid
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_DeviceEventF> GetList(string strWhere, DateTime dt)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = dt.Year;
                    string strSql = string.Format(@"select DeviceEventID, DeviceCode, EventType, MaterialCode, VersionNo, OrderID, Lot, PlanQty, FactOrder, UseTime, InitTime, StartTime, EndTime, WorkGroup, WorkOrder, WorkerUserCode, Temp, Power, Energy, Speed, Press, PmMode, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime,IntervalTime FROM [{0}]", GetTableName(typeof(RPT_DeviceEventF)));
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    return dbConnection.Query<RPT_DeviceEventF>(strSql).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_DeviceEventF> GetList(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string strSql = string.Format(@"select DeviceEventID, DeviceCode, EventType, MaterialCode, VersionNo, OrderID, Lot, PlanQty, FactOrder, UseTime, InitTime, StartTime, EndTime, WorkGroup, WorkOrder, WorkerUserCode, Temp, Power, Energy, Speed, Press, PmMode, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime,IntervalTime FROM [{0}]", GetTableName(typeof(RPT_DeviceEventF)));
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    return dbConnection.Query<RPT_DeviceEventF>(strSql).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_DeviceEventFRepository").Error(ex.ToString());
                return null;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class OrderAvgDataF
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