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
using System.Windows.Forms;

namespace Repository.Repository
{
    public class SYS_DevicePartRepository : BaseDAL<SYS_DevicePart>
    {
        public bool Add(SYS_DevicePart model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_DevicePart(
                                                          DevicePartID,
                                                          DeviceTypeID,
                                                          DevicePartCode,
                                                          DevicePartName,
                                                          DevicePartJaneSpell,
                                                          SaveTime,
                                                          DevicePartNumber,
                                                          Enable,
                                                          Reserve1,
                                                          Reserve2,
                                                          Reserve3,
                                                          Reserve4,
                                                          Reserve5,
                                                          PartNum)
                                                      values(
                                                          NEWID(),
                                                          @DeviceTypeID,
                                                          @DevicePartCode,
                                                          @DevicePartName,
                                                          @DevicePartJaneSpell,
                                                          @SaveTime,
                                                          @DevicePartNumber,
                                                          @Enable,
                                                          @Reserve1,
                                                          @Reserve2,
                                                          @Reserve3,
                                                          @Reserve4,
														  @Reserve5,
                                                          @PartNum )");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(SYS_DevicePart model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_DevicePart set
                                                       DevicePartID = @DevicePartID,
                                                       DeviceTypeID = @DeviceTypeID,
                                                       DevicePartCode = @DevicePartCode,
                                                       DevicePartName = @DevicePartName,
                                                       DevicePartJaneSpell = @DevicePartJaneSpell,
                                                       SaveTime = @SaveTime,
                                                       DevicePartNumber = @DevicePartNumber,
                                                       Enable = @Enable,
                                                       Reserve1 = @Reserve1,
                                                       Reserve2 = @Reserve2,
                                                       Reserve3 = @Reserve3,
                                                       Reserve4 = @Reserve4,
                                                       Reserve5 = @Reserve5,
                                                       PartNum = @PartNum where DevicePartID = @DevicePartID ");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else

                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string devicePartID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from SYS_DevicePart where DevicePartID = @DevicePartID");
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        DevicePartID = devicePartID
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_DevicePart GetModel(string devicePartID)
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select top 1 DevicePartID, DeviceTypeID, DevicePartCode, DevicePartName, DevicePartJaneSpell, SaveTime, DevicePartNumber,Enable, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 ,PartNum from SYS_DevicePart where DevicePartID = @DevicePartID ");
                var list = dbConnection.Query<SYS_DevicePart>(strSql.ToString(), new
                {
                    DevicePartID = devicePartID
                }).ToList();
                return list[0];
            }
        }

        public int GetNum()
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"select max(PartNum) as PartNum from SYS_DevicePart");
                    var num = dbConnection.QueryFirstOrDefault<int>(strSql.ToString());
                    return num;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return 0;
            }
        }

        public List<SYS_DevicePart> GetDevicePartList()
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string where = "";
                    if (!NewuGlobal.SoftConfig.IsFinalMix())
                    {
                        where = " DevicePartCode not in ('" + NewuGlobal.GetDevicePartCode(DevicePartType.MixUp) + "','" + NewuGlobal.GetDevicePartCode(DevicePartType.MixDown) + "')";
                    }
                    else
                    {
                        where = " DevicePartCode not in ('" + NewuGlobal.GetDevicePartCode(DevicePartType.MixUp) + "','" + NewuGlobal.GetDevicePartCode(DevicePartType.MixDown) + "','" + NewuGlobal.GetDevicePartCode(DevicePartType.Silane) + "','" + NewuGlobal.GetDevicePartCode(DevicePartType.Carbon) + "','" + NewuGlobal.GetDevicePartCode(DevicePartType.Oil) + "','" + NewuGlobal.GetDevicePartCode(DevicePartType.Zno) + "')";
                    }
                    List<SYS_DevicePart> sYS_DeviceParts = GetList(where);
                    return sYS_DeviceParts;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_DevicePart> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select DevicePartID, DeviceTypeID, DevicePartCode, DevicePartName, DevicePartJaneSpell, SaveTime, DevicePartNumber, Enable,Reserve1, Reserve2, Reserve3, Reserve4, Reserve5, PartNum from SYS_DevicePart");
                    if (!string.IsNullOrEmpty(strWhere))
                        sqlStr += " where " + strWhere;
                    List<SYS_DevicePart> sYSDevicepart = connection.Query<SYS_DevicePart>(sqlStr).AsList();
                    return sYSDevicepart;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return null;
            }
        }

        /// 根据设备部件简拼获取ID </summary> <param name="devicePartJaneSpell"></param> <returns></returns>
        public SYS_DevicePart GetDevicePartByCode()
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select DevicePartID, DeviceTypeID, DevicePartCode, DevicePartName, DevicePartJaneSpell, SaveTime, DevicePartNumber,Enable from SYS_DevicePart order by DevicePartCode");
                    List<SYS_DevicePart> sYS_DeviceParts = connection.Query<SYS_DevicePart>(sqlStr).AsList();
                    return sYS_DeviceParts[0];
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_DevicePart> GetDevicePartListByDeviceID(string deviceID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * from SYS_DevicePart c where DeviceTypeID in ( select b.DeviceTypeID from SYS_Device a left join SYS_DeviceType b on a.DeviceTypeID = b.DeviceTypeID where a.DeviceID = @DeviceID)  order by c.DevicePartCode");

                    List<SYS_DevicePart> sYS_DeviceParts = connection.Query<SYS_DevicePart>(sqlStr, new
                    {
                        DeviceID = deviceID
                    }).AsList();
                    return sYS_DeviceParts;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return null;
            }
        }

        public string GetPMOrderToDevicePartTranWeight(PM_OrderTran ordertran, string devicePartCode, string devicepartCodeF)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    if (ordertran.OrderID == "")
                        return "OrderID is not Empty";

                    string sqlStr = string.Format(@"SELECT a.OrderID, b.DeviceCode, b.DeviceID, c.DevicePartCode, c.DevicePartID, a.MaterialCode, a.MaterialID, a.VersionNo, a.Lot,a.SetBatch, a.WorkGroup as Reserve1, a.WorkOrder as Reserve2 FROM PM_OrderTran a, SYS_Device b, SYS_DevicePart c where a.DeviceID = b.DeviceID and b.DeviceTypeID = c.DeviceTypeID and a.OrderID = @OrderID ");
                    if (!string.IsNullOrEmpty(devicePartCode))
                        sqlStr += "and c.DevicePartCode not in(@DevicePartCode,@DevicepartCodeF)";

                    List<SYS_DevicePart> sYS_DeviceParts = connection.Query<SYS_DevicePart>(sqlStr, new
                    {
                        OrderID = ordertran.OrderID,
                        DevicePartCode = devicePartCode,
                        DevicepartCodeF = devicepartCodeF
                    }).AsList();
                    if (sYS_DeviceParts.Count == 0)
                        return "list count is 0";

                    string deleteSql = string.Format(@"delete from PM_DevicePartOrderTran where DeviceCode = @DeviceCode and DevicePartCode = @DevicePartCode");
                    string addSql = string.Format(@"insert into PM_DevicePartOrderTran (
                                                        DevicePartOrderTranID,
                                                        OrderID,
                                                        DeviceCode,
                                                        DeviceID,
                                                        DevicePartCode,
                                                        DevicePartID,
                                                        MaterialCode,
                                                        MaterialID,
                                                        VersionNo,
                                                        Lot,
                                                        SetBatch,
                                                        Savetime,
                                                        Reserve1,
                                                        Reserve2)
                                                    values(
                                                        NEWID(),
                                                        @OrderID,
                                                        @DeviceCode,
                                                        @DeviceID,
                                                        @DevicePartCode,
                                                        @DevicePartID,
                                                        @MaterialCode,
                                                        @MaterialID,
                                                        @VersionNo,
                                                        @Lot,
                                                        @SetBatch,
                                                        getdate(),
                                                        @Reserve1,
                                                        @Reserve2)");

                    IDbTransaction transaction = connection.BeginTransaction();
                    int deleteRow = connection.Execute(deleteSql, sYS_DeviceParts, transaction);
                    if (deleteRow <= 0)
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("删除失败" + deleteRow);
                    else
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("删除成功" + deleteRow);

                    int addRow = connection.Execute(addSql, sYS_DeviceParts, transaction);
                    transaction.Commit();

                    if (addRow <= 0)
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("添加失败" + deleteRow);
                    else
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("添加成功" + deleteRow);

                    return "";
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return "";
            }
        }

        public string GetPMOrderToDevicePartTranMix(PM_OrderTran orderTran, string devicePartCode, string devicepartCodeF)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    if (orderTran.OrderID == "")
                        return "OrderID is not Empty";

                    string sqlStr = string.Format(@"SELECT a.OrderID, b.DeviceCode, b.DeviceID, c.DevicePartCode, c.DevicePartID, a.MaterialCode, a.MaterialID, a.VersionNo, a.Lot,a.SetBatch, a.WorkGroup as Reserve1, a.WorkOrder as Reserve2 FROM PM_OrderTran a, SYS_Device b, SYS_DevicePart c where a.DeviceID = b.DeviceID and b.DeviceTypeID = c.DeviceTypeID and a.OrderID = @OrderID ");
                    if (!string.IsNullOrEmpty(devicePartCode))
                        sqlStr += "and c.DevicePartCode in(@DevicePartCode,@DevicepartCodeF)";

                    List<SYS_DevicePart> sYS_DeviceParts = connection.Query<SYS_DevicePart>(sqlStr, new
                    {
                        OrderID = orderTran.OrderID,
                        DevicePartCode = devicePartCode,
                        DevicepartCodeF = devicepartCodeF
                    }).AsList();
                    if (sYS_DeviceParts.Count == 0)
                        return "list count is 0";

                    string deleteSql = string.Format(@"delete from PM_DevicePartOrderTran where DeviceCode = @DeviceCode and DevicePartCode = @DevicePartCode");
                    string addSql = string.Format(@"insert into PM_DevicePartOrderTran (
                                                        DevicePartOrderTranID,
                                                        OrderID,
                                                        DeviceCode,
                                                        DeviceID,
                                                        DevicePartCode,
                                                        DevicePartID,
                                                        MaterialCode,
                                                        MaterialID,
                                                        VersionNo,
                                                        Lot,
                                                        SetBatch,
                                                        Savetime,
                                                        Reserve1,
                                                        Reserve2)
                                                    values(
                                                        NEWID(),
                                                        @OrderID,
                                                        @DeviceCode,
                                                        @DeviceID,
                                                        @DevicePartCode,
                                                        @DevicePartID,
                                                        @MaterialCode,
                                                        @MaterialID,
                                                        @VersionNo,
                                                        @Lot,
                                                        @SetBatch,
                                                        getdate(),
                                                        @Reserve1,
                                                        @Reserve2)");

                    IDbTransaction transaction = connection.BeginTransaction();
                    int deleteRow = connection.Execute(deleteSql, sYS_DeviceParts, transaction);

                    if (deleteRow <= 0)
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("GetPMOrderToDevicePartTranMix删除失败" + deleteRow);
                    else
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("GetPMOrderToDevicePartTranMix删除成功" + deleteRow);

                    int addRow = connection.Execute(addSql, sYS_DeviceParts, transaction);

                    transaction.Commit();

                    if (addRow <= 0)
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("GetPMOrderToDevicePartTranMix添加失败" + deleteRow);
                    else
                        NewuGlobal.LogCat("SYS_DevicePartRepository").Info("GetPMOrderToDevicePartTranMix添加成功" + deleteRow);

                    return "";
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DevicePartRepository").Error(ex.ToString());
                return "";
            }
        }
    }
}