using Dapper;
using Dapper.Contrib.Extensions;
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
    public class RPT_WeightRepository : BaseDAL<RPT_Weight>, SqlMapperExtensions.ITableNameMapper
    {
        public RPT_WeightRepository()
        {
        }

        /// <summary>
        /// 表名前缀年份
        /// </summary>
        public int TableYear
        {
            get; set;
        }

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
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public void Add(string date, RPT_Weight model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = int.Parse(date);
                    string sqlStr = string.Format(@"insert into [dbo].[{0}] (WeightID, DeviceCode, DevicePartCode, OrderID, MaterialCode, TypeCodeName, VersionNo, Lot, PlanQty, FactOrder, SetMaterialCode, SetBinNo, SetWeight, AllowError, ActWeight, ActError, WeightOrder, DropOrder, WorkGroup, WorkOrder, WorkerUserCode, SaveTime,  Reserve1 ,Reserve2,  Reserve3,  Reserve4,  Reserve5,VersionID,Is_Read,ReadTime)
                    values(
                      NewID(), @DeviceCode, @DevicePartCode, @OrderID, @MaterialCode, @TypeCodeName, @VersionNo, @Lot, @PlanQty, @FactOrder, @SetMaterialCode, @SetBinNo, @SetWeight, @AllowError, @ActWeight, @ActError, @WeightOrder, @DropOrder, @WorkGroup, @WorkOrder, @WorkerUserCode, @SaveTime, @Reserve1,@Reserve2,  @Reserve3,  @Reserve4,  @Reserve5,@VersionID,@Is_Read,@ReadTime)", GetTableName(typeof(RPT_Weight)));

                    int effect = dbConnection.Execute(sqlStr, model);
                    if (effect > 0)
                        NewuGlobal.LogCat("称量报表").Info("数据插入成功！" + "配方名称：【" + model.MaterialCode + "】" + "物料名称：【" + model.SetMaterialCode + "】" + "斗号：【" + model.SetBinNo + "】" + "设定重量：【" + model.SetWeight + "】" + "实际重量：【" + model.ActWeight + "】" + "称量顺序：【" + model.WeightOrder + "】" + "投料顺序：【" + model.DropOrder + "】" + "条码内容：【" + model.Reserve1 + "】");
                    else
                        NewuGlobal.LogCat("称量报表").Info("数据插入失败");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
            }
        }

        public void DeleteData(string date, string orderID, string setMaterialCode, int factOrder, int weightOrder, int dropOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = int.Parse(date);
                    string sqlStr = string.Format("Delete from [dbo].[{0}] where OrderID = @OrderID and SetMaterialCode = @SetMaterialCode and FactOrder = @FactOrder and WeightOrder = @WeightOrder and DropOrder = @DropOrder", GetTableName(typeof(RPT_Weight)));
                    int effect = dbConnection.Execute(sqlStr, new
                    {
                        OrderID = orderID,
                        SetMaterialCode = setMaterialCode,
                        FactOrder = factOrder,
                        WeightOrder = weightOrder,
                        DropOrder = dropOrder
                    });
                    if (effect > 0)
                        NewuGlobal.LogCat("称量报表").Info("删除数据成功");
                    else
                        NewuGlobal.LogCat("称量报表").Info("无数据删除");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
            }
        }

        public List<RPT_Weight> GetList(string orderID, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string sqlStr = string.Format(@"Select OrderID, MaterialCode, FactOrder from [{0}] where OrderID = @OrderID group by orderId, factOrder, MaterialCode", GetTableName(typeof(RPT_Weight)));
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(sqlStr, new
                    {
                        OrderID = orderID
                    }).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetList(DateTime st, DateTime et)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = st.Year;
                    string sqlStr = string.Format(@"select DeviceCode,OrderID,MaterialCode,SetMaterialCode,lot,PlanQty,count(FactOrder) as FactOrder from [dbo].[{0}] where SaveTime>= @st and SaveTime<=@et group by MaterialCode,lot,PlanQty,SetMaterialCode,DeviceCode,OrderID", GetTableName(typeof(RPT_Weight)));
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(sqlStr, new
                    {
                        st = st,
                        et = et
                    }).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetYieldList(DateTime st, DateTime et)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = st.Year;
                    string sqlStr = string.Format(@"select DeviceCode,OrderID,materialcode,lot,SetMaterialCode, count(MaterialCode) as Reserve1 from [dbo].[{0}] where SaveTime>= @st and SaveTime<=@et and abs(ActError)<=AllowError group by MaterialCode,lot,SetMaterialCode,DeviceCode,OrderID", GetTableName(typeof(RPT_Weight)));
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(sqlStr, new
                    {
                        st = st,
                        et = et
                    }).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetListByTypeCodeName(DateTime st, DateTime et)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = st.Year;
                    string sqlStr = string.Format(@"select typecodename, SetMaterialCode,count(SetMaterialCode) as PlanQty from [dbo].[{0}]  where SaveTime>= @st and SaveTime<=@et group by typecodename, SetMaterialCode order by typecodename", GetTableName(typeof(RPT_Weight)));
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(sqlStr, new
                    {
                        st = st,
                        et = et
                    }).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetYieldListByTypeCodeName(DateTime st, DateTime et)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = st.Year;
                    string sqlStr = string.Format(@"select typecodename, SetMaterialCode,count(SetMaterialCode) as FactOrder from [dbo].[{0}]  where SaveTime>= @st and SaveTime<=@et  and abs(ActError)<=AllowError  group by typecodename ,SetMaterialCode order by typecodename", GetTableName(typeof(RPT_Weight)));
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(sqlStr, new
                    {
                        st = st,
                        et = et
                    }).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetListWhere(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string strSql = string.Format(@"select a.MaterialDesc as Reserve1, b.WeightID,b.DeviceCode,b.DevicePartCode,b.OrderID,b.MaterialCode,b.TypeCodeName,b.VersionNo,b.Lot,b.PlanQty,b.FactOrder,b.SetMaterialCode,b.SetBinNo,CAST(b.SetWeight AS decimal(10, 2)) AS SetWeight,CAST(b.AllowError AS decimal(10, 2)) AS AllowError,CAST(b.ActWeight AS decimal(10, 2)) AS ActWeight,CAST(b.ActError AS decimal(10, 2)) AS ActError,b.WeightOrder,b.DropOrder,b.WorkGroup,b.WorkOrder,b.WorkerUserCode,b.SaveTime,b.Reserve2,b.Reserve3,b.Reserve4,b.Reserve5 from [StandardMixer].[dbo].[FormulaMaterial] a left join  [{0}] b on a.MaterialCode=b.SetMaterialCode", GetTableName(typeof(RPT_Weight)));

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(strSql).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetList(string orderId, int factOrder, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string sqlStr = string.Format(@"select WeightID, DeviceCode, DevicePartCode, OrderID, MaterialCode, TypeCodeName, VersionNo, Lot, PlanQty, FactOrder, SetMaterialCode, SetBinNo, SetWeight, AllowError, ActWeight, ActError, WeightOrder,DropOrder, WorkGroup, WorkOrder, WorkerUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime from [{0}] where OrderID = @OrderID and FactOrder=@FactOrder Order by TypeCodeName, DropOrder, WeightOrder", GetTableName(typeof(RPT_Weight)));
                    List<RPT_Weight> rPT_Weights = dbConnection.Query<RPT_Weight>(sqlStr, new
                    {
                        OrderID = orderId,
                        FactOrder = factOrder
                    }).AsList();
                    return rPT_Weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public DataTable GetWeightTable(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    DataTable dt = new DataTable();
                    string strSql = string.Format(@" select WeightID, DeviceCode, DevicePartCode, OrderID, MaterialCode, TypeCodeName, VersionNo, Lot, PlanQty, FactOrder, SetMaterialCode, SetBinNo, SetWeight, AllowError, ActWeight, ActError, WeightOrder, DropOrder, WorkGroup, WorkOrder, WorkerUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}] ", GetTableName(typeof(RPT_Weight)));

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    var reader = dbConnection.ExecuteReader(strSql);
                    dt.Load(reader);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight> GetList(int top, string strWhere, string filedOrder, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string strSql = string.Format(@"select ");
                    if (top > 0)
                    {
                        strSql += (" top " + top.ToString());
                    }
                    strSql += string.Format(@" WeightID, DeviceCode, DevicePartCode, OrderID, MaterialCode, TypeCodeName, VersionNo, Lot, PlanQty, FactOrder, SetMaterialCode, SetBinNo, SetWeight, AllowError, ActWeight, ActError, WeightOrder, DropOrder, WorkGroup, WorkOrder, WorkerUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}] ", GetTableName(typeof(RPT_Weight)));

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql += (" where " + strWhere);
                    }
                    strSql += (" order by " + filedOrder);
                    return dbConnection.Query<RPT_Weight>(strSql).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Weight_GetBatchReport> GetBatchReportList(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string strSql = string.Format(@"exec getBatchReport '" + strWhere + "','" + TableYear + "'");
                    List<RPT_Weight_GetBatchReport> rPT_Weight_GetBatchReports = dbConnection.Query<RPT_Weight_GetBatchReport>(strSql).AsList();
                    return rPT_Weight_GetBatchReports;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public DataTable GetBatchReportTable(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    DataTable dt = new DataTable();
                    string strSql = string.Format(@"exec  getBatchReport '" + strWhere + "','" + TableYear + "'");
                    IDataReader dataReader = dbConnection.ExecuteReader(strSql);
                    dt.Load(dataReader);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 物料统计
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<RPT_Weight> GetMaterialStatistics(DateTime dt, string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = dt.Year;
                    string sqlStr = string.Format(@"SELECT TypeCodeName, SetMaterialCode, SUM(SetWeight) as 'SetWeight', sum(ActWeight) as 'ActWeight'  FROM [dbo].[{0}] where " + strWhere + " group by TypeCodeName,SetMaterialCode order by TypeCodeName ", GetTableName(typeof(RPT_Weight)));

                    List<RPT_Weight> weights = connection.Query<RPT_Weight>(sqlStr).AsList();
                    return weights;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<WeightFactOrder> GetPageList(string orderID, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string strSql = string.Format(@"select  (case when c.aFact>c.bFact then c.aFact else c.bFact end) FactOrder  from (select MAX(a.FactOrder) aFact, MAX(b.FactOrder) bFact from [{0}] a, [{1}] b where a.OrderID = b.OrderID and a.OrderID = @OrderID) c", GetTableName(typeof(RPT_MixStep)), GetTableName(typeof(RPT_Weight)));

                    return connection.Query<WeightFactOrder>(strSql, new
                    {
                        OrderID = orderID
                    }).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
                return null;
            }
        }

        public void DeleteDataTable(int year)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    //判断数据库里面有几年的数据
                    int tableCounts = SelectYearsTable("_RPT_Weight");
                    int needDeleteYears = tableCounts - year;
                    if (needDeleteYears > 0)
                    {
                        for (int i = 0; i < needDeleteYears; i++)
                        {
                            TableYear = DateTime.Now.Year - year - i;
                            bool result = ExistTable(TableYear.ToString() + "_RPT_Weight");
                            if (result)
                            {
                                string sqlStr = string.Format($" Drop table [{TableYear}_RPT_Weight];" +
                               $" Drop table [{TableYear}_RPT_MixStep];" +
                               $" Drop table [{TableYear}_RPT_DeviceEvent]; " +
                               $" Drop table [{TableYear}_RPT_Curve];" +
                               $" Drop table [{TableYear}_RPT_CurveF];" +
                               $" Drop table [{TableYear}_RPT_MixStepF];" +
                               $" Drop table [{TableYear}_RPT_WeightF];" +
                               $" Drop table [{TableYear}_RPT_DeviceEventF];");
                                int effect = dbConnection.Execute(sqlStr);
                                if (effect > 0)
                                {
                                    NewuGlobal.LogCat("数据报表").Info("删除数据成功");
                                }
                                else
                                {
                                    NewuGlobal.LogCat("数据报表").Info("无数据删除");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_WeightRepository").Error(ex.ToString());
            }
        }

        public bool ExistTable(string tableName)
        {
            using (IDbConnection dbConnection = ConnectionXFData)
            {
                string sql = $"select count(*) from sys.tables where name='{tableName}'";
                int cmdresult;
                object obj = dbConnection.ExecuteScalar(sql);
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public int SelectYearsTable(string tableName)
        {
            using (IDbConnection dbConnection = ConnectionXFData)
            {
                string sql = $"select count(*) from sys.tables where name like'%{tableName}'";
                DataTable dt = new DataTable();
                var dataReader = dbConnection.ExecuteReader(sql);
                dt.Load(dataReader);
                if (dt.Rows.Count > 0)
                {
                    return int.Parse(dt.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class WeightFactOrder
    {
        public int FactOrder;
    }
}