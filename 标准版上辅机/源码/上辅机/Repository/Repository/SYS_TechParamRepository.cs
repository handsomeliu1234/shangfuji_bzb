using Dapper;
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
    public class SYS_TechParamFRepository : BaseDAL<SYS_TechParam>
    {
        public bool Add(SYS_TechParam model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strSql = string.Format(@"insert into SYS_TechParam(
                                                        TechParamID,
                                                        DeviceID,
                                                        DevicePartID,
                                                        TechParamNameCN,
                                                        TechParamNameEN,
                                                        TechParamOrder,
                                                        DecDigit,
                                                        Unit,
                                                        Enable,
                                                        SaveUserID,
                                                        SaveTime,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                    values (
                                                        NEWID(),
                                                        @DeviceID,
                                                        @DevicePartID,
                                                        @TechParamNameCN,
                                                        @TechParamNameEN,
                                                        @TechParamOrder,
                                                        @DecDigit,
                                                        @Unit,
                                                        @Enable,
                                                        @SaveUserID,
                                                        @SaveTime,
                                                        @Reserve1,
                                                        @Reserve2,
                                                        @Reserve3,
                                                        @Reserve4,
                                                        @Reserve5)");

                    int effectRow = connection.Execute(strSql, model);
                    if (effectRow > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool UpdateModel(SYS_TechParam model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_TechParam set
                                                        TechParamID = @TechParamID,
                                                        DeviceID = @DeviceID,
                                                        DevicePartID = @DevicePartID,
                                                        TechParamNameCN = @TechParamNameCN,
                                                        TechParamNameEN = @TechParamNameEN,
                                                        TechParamOrder = @TechParamOrder,
                                                        DecDigit = @DecDigit,
                                                        Unit = @Unit,
                                                        Enable = @Enable,
                                                        SaveUserID = @SaveUserID,
                                                        SaveTime = @SaveTime,
                                                        Reserve1 = @Reserve1,
                                                        Reserve2 = @Reserve2,
                                                        Reserve3 = @Reserve3,
                                                        Reserve4 = @Reserve4,
                                                        Reserve5 = @Reserve5 where TechParamID = @TechParamID");
                    int effectRow = dbConnection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_TechParam GetModel(string techParamID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1  TechParamID, DeviceID, DevicePartID, TechParamNameCN, TechParamNameEN, TechParamOrder,  DecDigit, Unit, Enable, SaveUserID, SaveTime, Reserve1, Reserve2,  Reserve3, Reserve4, Reserve5 from SYS_TechParam where TechParamID = @TechParamID");
                    SYS_TechParam sYS_TechParam = dbConnection.QueryFirstOrDefault<SYS_TechParam>(sqlStr, new
                    {
                        TechParamID = techParamID
                    });
                    return sYS_TechParam;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TechParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_TechParam> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select TechParamID, DeviceID, DevicePartID, TechParamNameCN, TechParamNameEN, TechParamOrder, DecDigit, Unit, Enable, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_TechParam ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += "where " + strWhere;
                    }
                    List<SYS_TechParam> sYS_TechParams = connection.Query<SYS_TechParam>(sqlStr).AsList();
                    return sYS_TechParams;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TechParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_TechParam> GetList(int top, string whereStr, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");

                    if (top > 0)
                        sqlStr.Append("top" + top.ToString());

                    sqlStr.Append("TechParamID, DeviceID, DevicePartID, TechParamNameCN, TechParamNameEN, TechParamOrder, DecDigit, Unit, Enable, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_TechParam ");

                    if (!string.IsNullOrEmpty(whereStr))
                        sqlStr.Append("where " + whereStr);

                    sqlStr.Append(" order by " + filedOrder);

                    List<SYS_TechParam> sYS_TechParams = connection.Query<SYS_TechParam>(sqlStr.ToString()).AsList();
                    return sYS_TechParams;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TechParamRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}