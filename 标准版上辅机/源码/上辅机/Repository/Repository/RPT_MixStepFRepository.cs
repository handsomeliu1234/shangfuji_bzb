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
    public class RPT_MixStepFRepository : BaseDAL<RPT_MixStepF>
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

        public List<RPT_MixStepF> GetList(string where, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = orderTran.Savetime.Year;
                    string sqlStr = string.Format(@"select MixStepID, DeviceCode, DevicePartCode, OrderID, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, StepOrder, StepName, ActionControlName, SetTime, ActTime, SetTemp, ActTemp, SetPower, ActPower, SetEnergy, ActEnergy, SetPress, ActPress, SetSpeed, ActSpeed, KeepTime, WorkGroup, WorkOrder, WorkerUserCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime from [{0}] ", GetTableName(typeof(RPT_MixStepF)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += (" where " + where);
                    List<RPT_MixStepF> rPT_MixStep = connection.Query<RPT_MixStepF>(sqlStr).AsList();
                    return rPT_MixStep;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepFRepository").Error(ex.ToString());
                return null;
            }
        }

        public void DeleteMixStep(string date, string orderID, int plcBatch)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = int.Parse(date);
                    string sqlStr = string.Format("Delete from [dbo].[{0}] where OrderID = @OrderID and FactOrder = @FactOrder and StepOrder = @StepOrder", GetTableName(typeof(RPT_MixStepF)));
                    int effect = dbConnection.Execute(sqlStr, new
                    {
                        OrderID = orderID,
                        FactOrder = plcBatch
                    });
                    if (effect > 0)
                        NewuGlobal.LogCat("RPT_MixStepFRepository").Info("删除成功");
                    else
                        NewuGlobal.LogCat("RPT_MixStepFRepository").Info("删除失败");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepFRepository").Error(ex.ToString());
            }
        }

        public void InsertMixStep(string date, List<RPT_MixStepF> model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXFData)
                {
                    TableYear = int.Parse(date);
                    string sqlStr = string.Format(@"insert into [dbo].[{0}] (MixStepID, DeviceCode, DevicePartCode, OrderID, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, StepOrder, StepName, ActionControlName, SetTime, ActTime, SetTemp, ActTemp, SetPower, ActPower, SetEnergy, ActEnergy, SetPress, ActPress, SetSpeed, ActSpeed, KeepTime, WorkGroup, WorkOrder, WorkerUserCode, SaveTime,VersionID,Is_Read,ReadTime)
                    values
                      (NewID(), @DeviceCode, @DevicePartCode, @OrderID, @MaterialCode, @VersionNo, @Lot, @PlanQty, @FactOrder, @StepOrder, @StepName, @ActionControlName, @SetTime, @ActTime, @SetTemp, @ActTemp, @SetPower, @ActPower, @SetEnergy, @ActEnergy, @SetPress, @ActPress, @SetSpeed, @ActSpeed, @KeepTime, @WorkGroup, @WorkOrder, @WorkerUserCode, @SaveTime,@VersionID,@Is_Read,@ReadTime)", GetTableName(typeof(RPT_MixStepF)));

                    int effect = dbConnection.Execute(sqlStr, model);
                    if (effect > 0)
                        NewuGlobal.LogCat("RPT_MixStepFRepository").Info("插入数据成功");
                    else
                        NewuGlobal.LogCat("RPT_MixStepFRepository").Info("插入数据失败");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_MixStepFRepository").Error(ex.ToString());
            }
        }
    }
}