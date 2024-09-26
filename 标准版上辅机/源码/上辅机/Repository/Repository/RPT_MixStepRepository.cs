using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repository
{
    public class RPT_MixStepRepository : BaseDAL<RPT_MixStep>
    {
        /// <summary>
        /// 表名前缀年份
        /// </summary>
        public int TableYear
        {
            get; set;
        }

        /// <summary>
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

        public List<RPT_MixStep> GetList(string where, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string sqlStr = string.Format(@"select MixStepID, DeviceCode, DevicePartCode, OrderID, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, StepOrder, StepName, ActionControlName, SetTime, ActTime, SetTemp, ActTemp, SetPower, ActPower, SetEnergy, ActEnergy, SetPress, ActPress, SetSpeed, ActSpeed, KeepTime, WorkGroup, WorkOrder, WorkerUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime from [{0}] ", GetTableName(typeof(RPT_MixStep)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += (" where " + where);
                    List<RPT_MixStep> rPT_MixStep = connection.Query<RPT_MixStep>(sqlStr).AsList();
                    return rPT_MixStep;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepRepository").Error(ex.ToString());
                return null;
            }
        }

        public DataTable GetMixStepTable(string strWhere, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    DataTable dt = new DataTable();
                    string sqlStr = string.Format(@"select MixStepID, DeviceCode, DevicePartCode, OrderID, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, StepOrder, StepName, ActionControlName, SetTime, ActTime, SetTemp, ActTemp, SetPower, ActPower, SetEnergy, ActEnergy, SetPress, ActPress, SetSpeed, ActSpeed, KeepTime, WorkGroup, WorkOrder, WorkerUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime from [{0}] ", GetTableName(typeof(RPT_MixStep)));
                    if (!string.IsNullOrEmpty(strWhere))
                        sqlStr += (" where " + strWhere);
                    sqlStr += ("order by StepOrder ");
                    IDataReader dataReader = connection.ExecuteReader(sqlStr);
                    dt.Load(dataReader);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepRepository").Error(ex.ToString());
                return null;
            }
        }

        public void DeleteMixStep(string date, string orderID, int plcBatch, int stepOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = int.Parse(date);
                    string sqlStr = string.Format("Delete from [dbo].[{0}] where OrderID = @OrderID and FactOrder = @FactOrder and StepOrder = @StepOrder", GetTableName(typeof(RPT_MixStep)));
                    int effect = dbConnection.Execute(sqlStr, new
                    {
                        OrderID = orderID,
                        FactOrder = plcBatch,
                        StepOrder = stepOrder
                    });
                    if (effect > 0)
                        NewuGlobal.LogCat("密炼报表").Info("删除数据成功");
                    else
                        NewuGlobal.LogCat("密炼报表").Info("无数据删除");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepRepository").Error(ex.ToString());
            }
        }

        public void InsertMixStep(string date, List<RPT_MixStep> model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = int.Parse(date);
                    string sqlStr = string.Format(@"insert into [dbo].[{0}] (MixStepID, DeviceCode, DevicePartCode, OrderID, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, StepOrder, StepName, ActionControlName, SetTime, ActTime, SetTemp, ActTemp, SetPower, ActPower, SetEnergy, ActEnergy, SetPress, ActPress, SetSpeed, ActSpeed, KeepTime, WorkGroup, WorkOrder, WorkerUserCode, SaveTime,VersionID,Is_Read,ReadTime)
                    values
                      (NewID(), @DeviceCode, @DevicePartCode, @OrderID, @MaterialCode, @VersionNo, @Lot, @PlanQty, @FactOrder, @StepOrder, @StepName, @ActionControlName, @SetTime, @ActTime, @SetTemp, @ActTemp, @SetPower, @ActPower, @SetEnergy, @ActEnergy, @SetPress, @ActPress, @SetSpeed, @ActSpeed, @KeepTime, @WorkGroup, @WorkOrder, @WorkerUserCode, @SaveTime,@VersionID,@Is_Read,@ReadTime)", GetTableName(typeof(RPT_MixStep)));
                    int effect = dbConnection.Execute(sqlStr, model);

                    if (effect > 0)
                    {
                        foreach (var item in model)
                        {
                            NewuGlobal.LogCat("密炼报表").Info("数据插入成功！" + "配方名称：【" + item.MaterialCode + "】" + "称量批号：【" + item.Lot + "】" + "实际车数：【" + item.FactOrder + "】" + "序号：【" + item.StepOrder + "】" + "工艺步骤：【" + item.StepName + "】" + "时间：【" + item.ActTime + "】" + "温度：【" + item.ActTemp + "】" + "能量：【" + item.ActEnergy + "】" + "压力：【" + item.ActPress + "】" + "转速：【" + item.ActSpeed + "】");
                        }
                    }
                    else
                        NewuGlobal.LogCat("密炼报表").Info("插入数据失败");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepRepository").Error(ex.ToString());
            }
        }
    }
}