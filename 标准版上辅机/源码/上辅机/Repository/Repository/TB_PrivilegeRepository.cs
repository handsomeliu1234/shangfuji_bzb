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
    public class TB_PrivilegeRepository : BaseDAL<TB_Privilege>
    {
        public TB_PrivilegeRepository()
        {
        }

        public bool Delete(string RoleID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from TB_Privilege where RoleID=@RoleID");
                    int effectRow = dbConnection.Execute(sqlStr, new
                    {
                        RoleID = RoleID
                    });
                    if (effectRow != 0)
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
                NewuGlobal.LogCat("TB_PrivilegeRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool DeleteList(string MenuIDlist)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from TB_Privilege where MenuID in (" + MenuIDlist + ")");
                    int effectRow = dbConnection.Execute(sqlStr);
                    if (effectRow != 0)
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
                NewuGlobal.LogCat("TB_PrivilegeRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<TB_Privilege> GetModelList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select PrivilegeID, MenuID, RoleID, Enable, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_Privilege ");
                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_Privilege> lsGetModelList = dbConnection.Query<TB_Privilege>(sqlStr.ToString()).ToList();
                    if (lsGetModelList.Count > 0)
                    {
                        return lsGetModelList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_PrivilegeRepository").Error(ex.ToString());
                return null;
            }
        }

        public int SaveList(List<TB_Privilege> list)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into TB_Privilege(
                                                    PrivilegeID,
                                                    MenuID,
                                                    RoleID,
                                                    Enable,
                                                    SaveTime)
                                                values(
                                                    NEWID(),
                                                    @MenuID,
                                                    @RoleID,
                                                    @Enable,
                                                    @SaveTime)");
                    int effectRow = dbConnection.Execute(sqlStr, list);
                    return effectRow;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_PrivilegeRepository").Error(ex.ToString());
                return 0;
            }
        }

        public bool GetData(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select  count(1) FROM TB_Privilege ");

                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    int v = dbConnection.Query<int>(sqlStr.ToString()).FirstOrDefault();
                    if (v == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_PrivilegeRepository").Error(ex.ToString());
                return false;
            }
        }
    }
}