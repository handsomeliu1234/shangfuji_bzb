using Dapper;
using Newu;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repository
{
    public class RPT_AppEventRepository : BaseDAL<RPT_AppEvent>
    {
        public bool Add(RPT_AppEvent model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into RPT_AppEvent(
                                                        AppEventID,
                                                        AppEventType,
                                                        OrderID,
                                                        DeviceCode,
                                                        MaterialCode,
                                                        VersionNo,
                                                        Lot,
                                                        PlanQty,
                                                        FactOrder,
                                                        SaveTime,
                                                        UserID,
                                                        SaveRealName,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                    values (
                                                        NEWID(),
                                                        @AppEventType,
                                                        @OrderID,
                                                        @DeviceCode,
                                                        @MaterialCode,
                                                        @VersionNo,
                                                        @Lot,
                                                        @PlanQty,
                                                        @FactOrder,
                                                        @SaveTime,
                                                        @UserID,
                                                        @SaveRealName,
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
                NewuGlobal.LogCat("RPT_AppEventRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Add(AppEventType AppEventType)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
                    PM_OrderTran orderTran = orderTranRepository.GetModel(1, " IsStart=1 ", " Savetime desc ");
                    RPT_AppEvent rPT_AppEvent = new RPT_AppEvent
                    {
                        SaveTime = DateTime.Now,
                        DeviceCode = NewuGlobal.SoftConfig.DeviceCode
                    };
                    switch (AppEventType)
                    {
                        case AppEventType.UserLogin:
                            rPT_AppEvent.AppEventType = "UserLogin";
                            rPT_AppEvent.SaveRealName = NewuGlobal.TB_UserInfo.RealName;
                            rPT_AppEvent.UserID = NewuGlobal.TB_UserInfo.UserID;
                            break;

                        case AppEventType.SystemLogOut:
                            rPT_AppEvent.AppEventType = "SystemLogOut";
                            rPT_AppEvent.SaveRealName = NewuGlobal.TB_UserInfo.RealName;
                            rPT_AppEvent.UserID = NewuGlobal.TB_UserInfo.UserID;
                            break;

                        case AppEventType.AppRun:
                            rPT_AppEvent.AppEventType = "AppRun";
                            break;

                        case AppEventType.AppStop:
                            rPT_AppEvent.AppEventType = "AppStop";
                            rPT_AppEvent.SaveRealName = NewuGlobal.TB_UserInfo.RealName;
                            rPT_AppEvent.UserID = NewuGlobal.TB_UserInfo.UserID;
                            break;

                        case AppEventType.WorkShiftChange:
                            rPT_AppEvent.AppEventType = "WorkShiftChange";
                            rPT_AppEvent.SaveRealName = NewuGlobal.TB_UserInfo.RealName;
                            rPT_AppEvent.UserID = NewuGlobal.TB_UserInfo.UserID;
                            break;
                    }
                    if (orderTran == null)
                        return Add(rPT_AppEvent);

                    rPT_AppEvent.OrderID = orderTran.OrderID;
                    rPT_AppEvent.MaterialCode = orderTran.MaterialCode;
                    rPT_AppEvent.VersionNo = orderTran.VersionNo;
                    rPT_AppEvent.Lot = orderTran.Lot;
                    rPT_AppEvent.PlanQty = orderTran.SetBatch;
                    rPT_AppEvent.FactOrder = orderTran.SerialNumber;
                    return Add(rPT_AppEvent);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AppEventRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<RPT_AppEvent> GetList(string where)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select AppEventID, AppEventType, OrderID,DeviceCode, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, SaveTime, UserID, SaveRealName, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM [dbo].[{0}] Where " + where, typeof(RPT_AppEvent));
                    List<RPT_AppEvent> rPT_AppEvent = connection.Query<RPT_AppEvent>(sqlStr).AsList();
                    return rPT_AppEvent;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_AppEventRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}