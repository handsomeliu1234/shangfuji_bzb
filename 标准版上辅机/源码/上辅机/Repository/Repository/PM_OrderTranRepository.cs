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
    public class PM_OrderTranRepository : BaseDAL<PM_OrderTran>
    {
        public bool Add(PM_OrderTran model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into PM_OrderTran(
                                                    OrderID,
                                                    DeviceID,
                                                    DeviceName,
                                                    MaterialID,
                                                    MaterialCode,
                                                    VersionNo,
                                                    FormulaHostoryID,
                                                    OrderFrom,
                                                    SerialNumber,
                                                    Lot,
                                                    SetBatch,
                                                    IsStart,
                                                    IsDelete,
                                                    StartUserID,
                                                    StartUserCode,
                                                    WorkGroup,
                                                    WorkOrder,
                                                    Savetime,
                                                    StartTime,
                                                    EndTime,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                                values(
                                                    NEWID(),
                                                    @DeviceID,
                                                    @DeviceName,
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @VersionNo,
                                                    @FormulaHostoryID,
                                                    @OrderFrom,
                                                    @SerialNumber,
                                                    @Lot,
                                                    @SetBatch,
                                                    @IsStart,
                                                    @IsDelete,
                                                    @StartUserID,
                                                    @StartUserCode,
                                                    @WorkGroup,
                                                    @WorkOrder,
                                                    @Savetime,
                                                    @StartTime,
                                                    @EndTime,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5)");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(PM_OrderTran model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update [dbo].[PM_OrderTran] set
                                                    OrderID = @OrderID,
                                                    DeviceID = @DeviceID,
                                                    DeviceName = @DeviceName,
                                                    MaterialID = @MaterialID,
                                                    MaterialCode = @MaterialCode,
                                                    VersionNo = @VersionNo,
                                                    FormulaHostoryID = @FormulaHostoryID,
                                                    OrderFrom = @OrderFrom,
                                                    SerialNumber = @SerialNumber,
                                                    Lot = @Lot,
                                                    SetBatch = @SetBatch,
                                                    IsStart = @IsStart,
                                                    IsDelete = @IsDelete,
                                                    StartUserID = @StartUserID,
                                                    StartUserCode = @StartUserCode,
                                                    WorkGroup = @WorkGroup,
                                                    WorkOrder = @WorkOrder,
                                                    Savetime = @Savetime,
                                                    StartTime = @StartTime,
                                                    EndTime = @EndTime,
                                                    Reserve1 = @Reserve1,
                                                    Reserve2 = @Reserve2,
                                                    Reserve3 = @Reserve3,
                                                    Reserve4 = @Reserve4,
                                                    Reserve5 = @Reserve5 where OrderID = @OrderID");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return false;
            }
        }

        public PM_OrderTran GetModel(int top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("select ");
                    if (top > 0)
                    {
                        stringBuilder.Append(" top " + top.ToString());
                    }
                    stringBuilder.Append(" OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom, SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5  FROM PM_OrderTran ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        stringBuilder.Append(" where " + strWhere);
                    }
                    stringBuilder.Append(" order by " + filedOrder);
                    PM_OrderTran pM_OrderTran = connection.QueryFirstOrDefault<PM_OrderTran>(stringBuilder.ToString());
                    return pM_OrderTran;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public PM_OrderTran GetModel(string orderID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom,SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from PM_OrderTran where OrderID = @OrderID ");
                    PM_OrderTran pMOrderTran = connection.QueryFirstOrDefault<PM_OrderTran>(sqlStr, new
                    {
                        OrderID = orderID
                    });
                    return pMOrderTran;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_OrderTran> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom, SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 From PM_OrderTran");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<PM_OrderTran> pM_OrderTrans = connection.Query<PM_OrderTran>(sqlStr.ToString()).AsList();
                    return pM_OrderTrans;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_OrderTran> GetList(DateTime st, DateTime et)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom, SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 From PM_OrderTran where StartTime>=@StartTime and StartTime<=@EndTime");

                    List<PM_OrderTran> pM_OrderTrans = connection.Query<PM_OrderTran>(sqlStr.ToString(), new
                    {
                        StartTime = st,
                        EndTime = et
                    }).AsList();
                    return pM_OrderTrans;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_OrderTran> GetList(string deviceID, DateTime st, DateTime et)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    if (deviceID != "" && deviceID != null)
                    {
                        string query = string.Format("Select * from [dbo].[PM_OrderTran] where DeviceID = @DeviceID and IsStart>0 and [SaveTime] >= @St and [SaveTime] <= @Et  order by SaveTime");
                        return dbConnection.Query<PM_OrderTran>(query, new
                        {
                            DeviceID = deviceID,
                            St = st,
                            Et = et
                        }).ToList();
                    }
                    else
                    {
                        string query = string.Format("Select * from [dbo].[PM_OrderTran] where IsStart>0 and [SaveTime] >= @St and [SaveTime] <= @Et  order by SaveTime");
                        return dbConnection.Query<PM_OrderTran>(query, new
                        {
                            St = st,
                            Et = et
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public DataTable GetListTable(string OrderId)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    DataTable dt = new DataTable();
                    string strSql = string.Format(@"select OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom, SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 From PM_OrderTran  where OrderID='" + OrderId + "'");
                    IDataReader dataReader = dbConnection.ExecuteReader(strSql);
                    dt.Load(dataReader);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_OrderTran> GetList(int top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");
                    if (top > 0)
                    {
                        sqlStr.Append(" top " + top.ToString());
                    }
                    sqlStr.Append("OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom, SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from PM_OrderTran");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    sqlStr.Append(" order by " + filedOrder);
                    List<PM_OrderTran> pM_OrderTrans = connection.Query<PM_OrderTran>(sqlStr.ToString()).AsList();
                    return pM_OrderTrans;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<PM_OrderTran> GetModelJoinDevicePartOrderTran(string deviceId, string devicePartid)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select a.* from PM_OrderTran a,PM_DevicePartOrderTran b where a.OrderID=b.OrderID and b.DeviceID = @DeviceID and b.DevicePartID = @DevicePartID");
                    List<PM_OrderTran> pmOrders = connection.Query<PM_OrderTran>(sqlStr, new
                    {
                        DeviceID = deviceId,
                        DevicePartID = devicePartid
                    }).AsList();
                    return pmOrders;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return null;
            }
        }

        public void UpdateAllOrderAfterDelete(int serial)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    //所以用下面right函数实现
                    string sqlStr = "Update [dbo].[PM_OrderTran] set [SerialNumber] = [SerialNumber] - 1,Lot = LEFT(Lot, 4) + RIGHT('00'+CONVERT(VARCHAR(50),[SerialNumber] - 1),2) where IsDelete = 0 And IsStart = 0 and SerialNumber > @SerialNumber";
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        SerialNumber = serial
                    });
                    if (effectRow > 0)
                        NewuGlobal.LogCat("PM_OrderTranRepository").Info("更新成功");
                    else
                        NewuGlobal.LogCat("PM_OrderTranRepository").Info("更新失败");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
            }
        }

        public bool GetPlanFormularList(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select OrderID, DeviceID, DeviceName, MaterialID, MaterialCode, VersionNo, FormulaHostoryID, OrderFrom, SerialNumber, Lot, SetBatch, IsStart, IsDelete, StartUserID, StartUserCode, WorkGroup, WorkOrder, Savetime, StartTime, EndTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5  FROM PM_OrderTran where IsStart = 0 and IsDelete = 0");
                    List<PM_OrderTran> pM_OrderTrans = connection.Query<PM_OrderTran>(sqlStr).AsList();
                    foreach (var item in pM_OrderTrans)
                    {
                        if (item.MaterialID.Equals(materialID))
                            return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取订单启动状态
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrderStartState()
        {
            CreateTable ct = new CreateTable();
            string[] cmbCol = new string[] { "names", "values" };
            Type[] cmbType = new Type[] { typeof(string), typeof(int) };
            object[,] cmbVal = new object[,] { { NewuGlobal.GetRes("000632"), 0 }, { NewuGlobal.GetRes("000631"), 1 }, { NewuGlobal.GetRes("000103"), 2 } };
            DataTable isStartDt = ct.GetTable(cmbCol, cmbType, cmbVal);

            return isStartDt;
        }

        /// <summary>
        /// 获取最大生产序号,根据厂家要求修改
        /// </summary>
        /// <returns></returns>
        public int GetMaxPlanSerialNumber(DateTime dt)
        {
            try
            {
                string sql = "";
                int serialNumber;
                using (IDbConnection connection = ConnectionXF)
                {
                    //每天8点序号重排
                    if (dt.Hour >= 0 && dt.Hour < 8)
                    {
                        sql = "SELECT isnull (MAX(SerialNumber),0 ) FROM [dbo].[PM_OrderTran] WHERE IsDelete = 0 and ";
                        sql += "[Savetime] >= '" + dt.AddDays(-1).ToString("yyyy-MM-dd 00:00:00") + "' and [Savetime] < '" + dt.ToString("yyyy-MM-dd 08:00:00") + "'";
                    }
                    else
                    {
                        sql = "SELECT isnull (MAX(SerialNumber),0 ) FROM [dbo].[PM_OrderTran] WHERE IsDelete = 0 and ";
                        sql += "[Savetime] >= '" + dt.ToString("yyyy-MM-dd 08:00:00") + "' and [Savetime] < '" + dt.AddDays(1).ToString("yyyy-MM-dd 08:00:00") + "'";
                    }

                    serialNumber = connection.Query<int>(sql).FirstOrDefault();

                    return serialNumber;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_OrderTranRepository").Error(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="serial"></param>
        public void UpdateAllOrderSerial(int serial)
        {
            using (IDbConnection connection = ConnectionXF)
            {
                //所以用下面right函数实现
                string sql = "Update [dbo].[PM_OrderTran] set [SerialNumber] = [SerialNumber] + 1,Lot = LEFT(Lot, 4) + RIGHT('00'+CONVERT(VARCHAR(50),[SerialNumber] + 1),2) " +
              " where IsDelete = 0 And IsStart = 0 and SerialNumber >= " + serial;

                int number = connection.Execute(sql);
                return;
            }
        }
    }
}