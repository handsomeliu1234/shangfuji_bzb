using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Repository.Model;
using Dapper;
using Newu;
using Repository.GlobalConfig;

namespace Repository.Repository
{
    public class SYS_ActionControlRepository : BaseDAL<SYS_ActionControl>
    {
        public bool Add(SYS_ActionControl model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_ActionControl(
                                                        ActionControlCode,
                                                        ActionControlValue,
                                                        ActionControlNameCN,
                                                        ActionControlNameEN,
                                                        DeviceID,
                                                        SaveUserID,
                                                        SaveTime,
                                                        Enable,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                   Values(
                                                        NEWID(),
                                                        @ActionControlValue,
                                                        @ActionControlNameCN,
                                                        @ActionControlNameEN,
                                                        @DeviceID,
                                                        @SaveUserID,
                                                        @SaveTime,
                                                        @Enable,
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
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(SYS_ActionControl model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_ActionControl set
                                                       ActionControlValue = @ActionControlValue,
                                                       ActionControlNameCN = @ActionControlNameCN,
                                                       ActionControlNameEN = @ActionControlNameEN,
                                                       DeviceID = @DeviceID,
                                                       SaveUserID = @SaveUserID,
                                                       SaveTime = @SaveTime,
                                                       Enable=@Enable,
                                                       Reserve1 = @Reserve1,
                                                       Reserve2 = @Reserve2,
                                                       Reserve3 = @Reserve3,
                                                       Reserve4 = @Reserve4,
                                                       Reserve5 = @Reserve5
                                                   where ActionControlCode = @ActionControlCode");

                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string actionControlCode)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from SYS_ActionControl where ActionControlCode = @ActionControlCode");
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        ActionControlCode = actionControlCode
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return false;
            }
        }

        public int GetMaxActionControlValue()
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select max(ActionControlValue) from SYS_ActionControl");
                    int getValue = dbConnection.Query<int>(sqlStr).FirstOrDefault();
                    return getValue;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return 0;
            }
        }

        public SYS_ActionControl GetModel(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 ActionControlCode, ActionControlValue, ActionControlNameCN, ActionControlNameEN, DeviceID, SaveUserID, SaveTime,Enable, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_ActionControl");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    var list = dbConnection.QueryFirstOrDefault<SYS_ActionControl>(strSql.ToString());
                    return list;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_ActionControl> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select ActionControlCode, ActionControlValue, ActionControlNameCN, ActionControlNameEN, DeviceID, SaveUserID, SaveTime,Enable, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_ActionControl");
                    if (!string.IsNullOrEmpty(strWhere))
                        sqlStr += " where " + strWhere;
                    List<SYS_ActionControl> sYSActionControl = connection.Query<SYS_ActionControl>(sqlStr).AsList();
                    return sYSActionControl;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_ActionControl> GetListJoin(string deviceID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strWhere = "";
                    if (deviceID != "")
                    {
                        strWhere = "a.DeviceID='" + deviceID + "' order by ActionControlValue";
                    }

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select a.*,");
                    strSql.Append("b.DeviceName  ");
                    strSql.Append("FROM SYS_ActionControl a left join SYS_Device b  ");
                    strSql.Append("on a.DeviceID=b.DeviceID ");

                    if (strWhere.Trim() != "")
                    {
                        strSql.Append("and " + strWhere);
                    }
                    return connection.Query<SYS_ActionControl>(strSql.ToString()).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionControlRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}