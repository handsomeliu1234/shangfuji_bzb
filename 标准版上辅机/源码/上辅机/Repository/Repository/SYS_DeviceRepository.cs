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
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class SYS_DeviceRepository : BaseDAL<SYS_Device>
    {
        public bool Add(SYS_Device model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_Device(
                                                        DeviceID,
                                                        DeviceCode,
                                                        DeviceName,
                                                        DeviceJaneSpell,
                                                        DeviceTypeID,
                                                        DeviceDesc,
                                                        WorkShopID,
                                                        Enabled,
                                                        SaveTime,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                 values(
                                                        NEWID(),
                                                        @DeviceCode,
                                                        @DeviceName,
                                                        @DeviceJaneSpell,
                                                        @DeviceTypeID,
                                                        @DeviceDesc,
                                                        @WorkShopID,
                                                        @Enabled,
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
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(SYS_Device model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_Device set
                                                        DeviceID = @DeviceID,
                                                        DeviceCode = @DeviceCode,
                                                        DeviceName = @DeviceName,
                                                        DeviceJaneSpell = @DeviceJaneSpell,
                                                        DeviceTypeID = @DeviceTypeID,
                                                        DeviceDesc = @DeviceDesc,
                                                        WorkShopID = @WorkShopID,
                                                        Enabled = @Enabled,
                                                        SaveTime = @SaveTime,
                                                        Reserve1 = @Reserve1,
                                                        Reserve2 = @Reserve2,
                                                        Reserve3 = @Reserve3,
                                                        Reserve4 = @Reserve4,
                                                        Reserve5 = @Reserve5
                                                        where DeviceID = @DeviceID", model);
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string DeviceID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from SYS_Device where DeviceID = @DeviceID");
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        DeviceID = DeviceID
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_Device GetModel(string deviceID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 DeviceID, DeviceCode, DeviceName, DeviceJaneSpell, DeviceTypeID, DeviceDesc, WorkShopID, Enabled, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_Device");
                    strSql.Append(" where DeviceID = @DeviceID ");
                    var list = dbConnection.Query<SYS_Device>(strSql.ToString(), new
                    {
                        DeviceID = deviceID
                    }).ToList();
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_Device> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select DeviceID, DeviceCode, DeviceName, DeviceJaneSpell, DeviceTypeID, DeviceDesc, WorkShopID, Enabled, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_Device ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr = "where" + strWhere;
                    }
                    List<SYS_Device> sYSDevice = connection.Query<SYS_Device>(sqlStr).AsList();
                    return sYSDevice;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_Device> GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");
                    if (Top > 0)
                    {
                        sqlStr.Append(" top " + Top.ToString());
                    }
                    sqlStr.Append(" DeviceID, DeviceCode, DeviceName, DeviceJaneSpell, DeviceTypeID, DeviceDesc, WorkShopID, Enabled, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_Device ");

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }

                    sqlStr.Append(" order by " + filedOrder);

                    List<SYS_Device> sYSDevice = connection.Query<SYS_Device>(sqlStr.ToString()).AsList();
                    return sYSDevice;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_Device> GetListByDeviceType(DeviceType deviceType)
        {
            try
            {
                string strWhere = "";
                if (deviceType == DeviceType.T上辅机)
                {
                    strWhere = "DeviceTypeID in('" + NewuGlobal.DeviceTypeIDByCode("MasterBatch") + "','" + NewuGlobal.DeviceTypeIDByCode("FinalBatch") + "')";
                }

                if (deviceType == DeviceType.T小药)
                {
                    strWhere = "DeviceTypeID ='" + NewuGlobal.DeviceTypeIDByCode("AutomaticXF") + "'";
                }
                List<SYS_Device> sYS_Devices = GetList(0, strWhere, "DeviceCode");
                return sYS_Devices;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_Device> GetModelListAddNullRows(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    List<SYS_Device> sYS_Devices = GetList(strWhere);
                    return sYS_Devices;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_Device> GetListJoin(string shopNameID, string deviceTypeID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strWhere = "";
                    string typeID = "";
                    string shopID = "";
                    if (deviceTypeID != "")
                    {
                        typeID = "a.DeviceTypeID='" + deviceTypeID + "'";
                    }

                    if (shopNameID != "")
                    {
                        shopID = "a.WorkShopID='" + shopNameID + "'";
                    }

                    if (typeID != "" && shopID != "")
                    {
                        strWhere = typeID + " and " + shopID;
                    }

                    if (typeID != "" && shopID == "")
                    {
                        strWhere = typeID;
                    }

                    if (typeID == "" && shopID != "")
                    {
                        strWhere = shopID;
                    }

                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("select DeviceID,DeviceCode,DeviceName,DeviceJaneSpell,a.DeviceTypeID,DeviceDesc,a.WorkShopID,Enabled,a.SaveTime,a.Reserve1,a.Reserve2,a.Reserve3,a.Reserve4,a.Reserve5,");
                    strSql.Append("b.DeviceTypeName,b.DeviceTypeCode,c.ShopName  ");
                    strSql.Append("FROM SYS_Device a,SYS_DeviceType b,SYS_WorkShop c ");
                    strSql.Append("where a.DeviceTypeID=b.DeviceTypeID and a.WorkShopID=c.WorkshopID ");

                    if (strWhere.Trim() != "")
                    {
                        strSql.Append("and " + strWhere);
                    }

                    strSql.Append(" order by c.ShopName,DeviceCode asc");
                    return connection.Query<SYS_Device>(strSql.ToString()).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}