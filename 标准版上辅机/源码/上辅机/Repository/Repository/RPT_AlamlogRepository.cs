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
    public class RPT_AlarmlogRepository : BaseDAL<RPT_AlarmLog>
    {
        public bool Add(RPT_AlarmLog model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into RPT_AlarmLog(
                                                    AlarmLogID,
                                                    OrderID,
                                                    MaterialCode,
                                                    Lot,
                                                    PlanQty,
                                                    DeviceName,
                                                    DevicePartName,
                                                    AlarmInfo,
                                                    WorkGroup,
                                                    WorkOrder,
                                                    MemoryAddr,
                                                    AlarmState,
                                                    SaveTime,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                                values(
                                                    NEWID(),
                                                    @OrderID,
                                                    @MaterialCode,
                                                    @Lot,
                                                    @PlanQty,
                                                    @DeviceName,
                                                    @DevicePartName,
                                                    @AlarmInfo,
                                                    @WorkGroup,
                                                    @WorkOrder,
                                                    @MemoryAddr,
                                                    @AlarmState,
                                                    @SaveTime,
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
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<RPT_AlarmLog> GetList(string where)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select AlarmLogID, OrderID, MaterialCode, Lot, PlanQty, FactOrder, DeviceName, DevicePartName, AlarmInfo, WorkGroup, WorkOrder, MemoryAddr, AlarmState, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM [dbo].[RPT_AlarmLog] Where " + where);
                    List<RPT_AlarmLog> rPT_AlarmLog = connection.Query<RPT_AlarmLog>(sqlStr).AsList();
                    return rPT_AlarmLog;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
                return null;
            }
        }

        public void SaveAlarmLogMix(TB_Alarm model, bool alarm_type)
        {
            try
            {
                string deviceID = NewuGlobal.SoftConfig.DeviceID;
                PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
                PM_DevicePartOrderTranRepository devicePartOrderTranRepository = new PM_DevicePartOrderTranRepository();
                string StrWhere = "DeviceID = '" + deviceID + "'";
                List<PM_DevicePartOrderTran> pM_DevicePartOrderTrans = devicePartOrderTranRepository.GetModelList(StrWhere);
                if (pM_DevicePartOrderTrans != null && pM_DevicePartOrderTrans.Count > 0)
                {
                    PM_OrderTran pM_OrderTran = orderTranRepository.GetModel(pM_DevicePartOrderTrans[0].OrderID);
                    pM_OrderTran.WorkGroup = NewuGlobal.SoftConfig.WorkGroup;//李辉 20231106 TB_UserInfo 修改为配置文件获取
                    pM_OrderTran.WorkOrder = NewuGlobal.SoftConfig.WorkOrder;//李辉 20231106

                    if (alarm_type)
                        SaveAlarmLog(model, pM_OrderTran, AlarmType.Produce);
                    else
                        SaveAlarmLog(model, pM_OrderTran, AlarmType.Disappear);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
            }
        }

        private void SaveAlarmLog(TB_Alarm tbAlarmMode, PM_OrderTran orderModel, AlarmType alarmType)
        {
            try
            {
                RPT_AlarmLog alarmLogModel = new RPT_AlarmLog();
                if (orderModel != null)
                {
                    alarmLogModel.OrderID = orderModel.OrderID;
                    alarmLogModel.MaterialCode = orderModel.MaterialCode;
                    alarmLogModel.Lot = orderModel.Lot;
                    alarmLogModel.PlanQty = orderModel.SetBatch;
                    alarmLogModel.DeviceName = orderModel.DeviceName;
                    alarmLogModel.WorkGroup = orderModel.WorkGroup;
                    alarmLogModel.WorkOrder = orderModel.WorkOrder;
                    if (alarmType == AlarmType.Produce)
                        alarmLogModel.AlarmState = "on";
                    else
                        alarmLogModel.AlarmState = "off";
                }

                if (tbAlarmMode != null)
                {
                    alarmLogModel.DevicePartName = NewuGlobal.DevicePartCodeByID(tbAlarmMode.DevicePartID);
                    alarmLogModel.AlarmInfo = tbAlarmMode.AlarmInfo;
                    alarmLogModel.MemoryAddr = tbAlarmMode.MemoryAddr.ToString();
                }
                alarmLogModel.SaveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Add(alarmLogModel);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
            }
        }

        public List<RPT_AlarmLog> QueryData(string AlarmInfo, string DevicePartName, DateTime start, DateTime end)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT * FROM RPT_AlarmLog where 1=1 ");
                    if (!string.IsNullOrEmpty(AlarmInfo.Trim()))
                    {
                        strSql.Append("and AlarmInfo like '%" + AlarmInfo + "%' ");
                    }
                    if (!string.IsNullOrEmpty(DevicePartName.Trim()))
                    {
                        strSql.Append("and DevicePartName like '%" + DevicePartName + "%' ");
                    }
                    strSql.Append("and SaveTime>='" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' and SaveTime<='" + end.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    strSql.Append(" order by SaveTime desc");
                    return connection.Query<RPT_AlarmLog>(strSql.ToString()).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
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
        public List<RPT_AlarmLog> Paging(string tableName, string pk, string queryCol, string strWhere, string pageindex, string pagesize, string sortfield, out int pagecount)
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
                    var alarmList = reader.Read<RPT_AlarmLog>().ToList();
                    pagecount = reader.Read<int>().Single();
                    return alarmList;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
                pagecount = 0;
                return null;
            }
        }

        public List<AlarmStates> GetAlarmLogStat(string dtStart, string dtEnd)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strSql = "select AlarmInfo,Count(*)/2 as AlarmCount from RPT_AlarmLog where SaveTime >='" + dtStart + "' and SaveTime <='" + dtEnd + "' group by AlarmInfo order by AlarmCount desc";
                    return connection.Query<AlarmStates>(strSql).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AlarmlogRepository").Error(ex.ToString());
                return null;
            }
        }

        public enum AlarmType
        {
            /// <summary>
            /// 报警解除
            /// </summary>
            Disappear,

            /// <summary>
            /// 报警产生
            /// </summary>
            Produce
        }
    }

    public class AlarmStates
    {
        public string AlarmInfo
        {
            get; set;
        }

        public int AlarmCount
        {
            get; set;
        }
    }
}