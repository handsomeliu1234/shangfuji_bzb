using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.Repository
{
    public class SYS_DeviceTypeRepository : BaseDAL<SYS_DeviceType>
    {
        public bool Add(SYS_DeviceType model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_DeviceType(
                                                      DeviceTypeID,
                                                      DeviceTypeCode,
                                                      DeviceTypeName,
                                                      DeviceTypeJaneSpell,
                                                      SaveTime,
                                                      Reserve1,
                                                      Reserve2,
                                                      Reserve3,
                                                      Reserve4,
                                                      Reserve5)
                                                 Values(
                                                      NEWID(),
                                                      @DeviceTypeCode,
                                                      @DeviceTypeName,
                                                      @DeviceTypeJaneSpell,
                                                      @SaveTime,
                                                      @Reserve1,
                                                      @Reserve2,
                                                      @Reserve3,
                                                      @Reserve4,
                                                      @Reserve5)", model);
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceTypeRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(SYS_DeviceType model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_DeviceType set
                                                      DeviceTypeID = @DeviceTypeID,
                                                      DeviceTypeCode = @DeviceTypeCode,
                                                      DeviceTypeName = @DeviceTypeName,
                                                      DeviceTypeJaneSpell = @DeviceTypeJaneSpell,
                                                      SaveTime = @SaveTime,
                                                      Reserve1 = @Reserve1,
                                                      Reserve2 = @Reserve2,
                                                      Reserve3 = @Reserve3,
                                                      Reserve4 = @Reserve4,
                                                      Reserve5 = @Reserve5 where DeviceTypeID=@DeviceTypeID", model);
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceTypeRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string deviceTypeID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from SYS_DeviceType where DeviceTypeID=@DeviceTypeID");
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        DeviceTypeID = deviceTypeID
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceTypeRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_DeviceType GetModel(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select top 1 DeviceTypeID,DeviceTypeCode,DeviceTypeName,DeviceTypeJaneSpell,SaveTime, Reserve1, Reserve2,Reserve3,Reserve4,Reserve5");
                    strSql.Append(" FROM SYS_DeviceType ");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    var list = dbConnection.QueryFirstOrDefault<SYS_DeviceType>(strSql.ToString());
                    return list;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceTypeRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_DeviceType> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select DeviceTypeID, DeviceTypeCode, DeviceTypeName, DeviceTypeJaneSpell, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_DeviceType ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr = "where" + sqlStr;
                    }
                    List<SYS_DeviceType> sYSDeviceType = connection.Query<SYS_DeviceType>(sqlStr).AsList();
                    return sYSDeviceType;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceTypeRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_DeviceType> GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    if (Top > 0)
                    {
                        strSql.Append(" top " + Top.ToString());
                    }
                    strSql.Append(" DeviceTypeID, DeviceTypeCode, DeviceTypeName, DeviceTypeJaneSpell, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_DeviceType ");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    strSql.Append(" order by " + filedOrder);
                    List<SYS_DeviceType> sYSDeviceTypes = connection.Query<SYS_DeviceType>(strSql.ToString()).AsList();
                    return sYSDeviceTypes;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_DeviceTypeRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}