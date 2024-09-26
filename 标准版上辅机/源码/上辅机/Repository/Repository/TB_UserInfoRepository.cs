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
    public class TB_UserInfoRepository : BaseDAL<TB_UserInfo>
    {
        public TB_UserInfoRepository()
        {
        }

        public bool Add(TB_UserInfo model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into TB_UserInfo(
                                                            UserID,
                                                            DepartmentID,
                                                            RoleID,
                                                            UserCode,
                                                            UserPassword,
                                                            RealName,
                                                            Phone,
                                                            Jobs,
                                                            SaveTime,
                                                            SaveUserID,
                                                            DeleteMark,
                                                            Reserve1,
                                                            Reserve2,
                                                            Reserve3,
                                                            Reserve4,
                                                            Reserve5)
                                                        values (
                                                            newid(),
                                                            @DepartmentID,
                                                            @RoleID,
                                                            @UserCode,
                                                            @UserPassword,
                                                            @RealName,
                                                            @Phone,
                                                            @Jobs,
                                                            @SaveTime,
                                                            @SaveUserID,
                                                            @DeleteMark,
                                                            @Reserve1,
                                                            @Reserve2,
                                                            @Reserve3,
                                                            @Reserve4,
                                                            @Reserve5)", model);
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
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(TB_UserInfo model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"update TB_UserInfo set
                                                              DepartmentID = @DepartmentID,
                                                              RoleID = @RoleID,
                                                              UserCode = @UserCode,
                                                              UserPassword = @UserPassword,
                                                              RealName = @RealName,
                                                              Phone = @Phone,
                                                              Jobs = @Jobs,
                                                              SaveTime = @SaveTime,
                                                              SaveUserID = @SaveUserID,
                                                              DeleteMark = @DeleteMark,
                                                              Reserve1 = @Reserve1,
                                                              Reserve2 = @Reserve2,
                                                              Reserve3 = @Reserve3,
                                                              Reserve4 = @Reserve4,
                                                              Reserve5 = @Reserve5
                                                            where UserID = @UserID", model);
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
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string UserID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute("delete from TB_UserInfo where UserID = @UserID ", new
                    {
                        UserID = UserID
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
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return false;
            }
        }

        public TB_UserInfo GetModel(string UserID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string strSql = string.Format(@"select top 1 UserID, DepartmentID, RoleID, UserCode, UserPassword, RealName, Phone, Jobs, SaveTime, SaveUserID, DeleteMark, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_UserInfo where UserID = @UserID");
                    TB_UserInfo user = dbConnection.QueryFirstOrDefault<TB_UserInfo>(strSql, new
                    {
                        UserID = UserID
                    });
                    return user;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return null;
            }
        }

        public TB_UserInfo GetModelByUserCode(string userCode)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string strSql = string.Format(@"select top 1 UserID, DepartmentID, RoleID, UserCode, UserPassword, RealName, Phone, Jobs, SaveTime, SaveUserID, DeleteMark, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_UserInfo where UserCode = @UserCode");
                    List<TB_UserInfo> lsGetModel = dbConnection.Query<TB_UserInfo>(strSql, new
                    {
                        UserCode = userCode
                    }).AsList();
                    return lsGetModel.Count > 0 ? lsGetModel[0] : null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_UserInfo> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select UserID, DepartmentID, RoleID, UserCode, UserPassword, RealName, Phone, Jobs, SaveTime, SaveUserID, DeleteMark, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_UserInfo");

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_UserInfo> lsGetList = dbConnection.Query<TB_UserInfo>(sqlStr.ToString()).ToList();
                    return lsGetList.Count > 0 ? lsGetList : null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_UserInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    if (Top > 0)
                    {
                        strSql.Append(" top " + Top.ToString());
                    }
                    strSql.Append(@" UserID, DepartmentID, RoleID, UserCode, UserPassword, RealName, Phone, Jobs, SaveTime, SaveUserID, DeleteMark, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_UserInfo");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    strSql.Append(" order by " + filedOrder);
                    List<TB_UserInfo> lsGetList = dbConnection.Query<TB_UserInfo>(strSql.ToString()).ToList();
                    if (lsGetList.Count > 0)
                    {
                        return lsGetList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                return null;
            }
        }

        public bool Query(string loginName, string pwd, out string msg, string workGroup, string workOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    bool flag = false;
                    TB_UserInfo tB_UserInfo = GetModelByUserCode(loginName);
                    if (tB_UserInfo != null)
                    {
                        if (pwd.Equals(tB_UserInfo.UserPassword))
                        {
                            flag = true;
                            msg = NewuGlobal.GetRes("000071");
                            tB_UserInfo.RoleName = new TB_RoleRepository().GetModel(tB_UserInfo.RoleID).RoleName;
                            tB_UserInfo.WorkGroup = workGroup;
                            tB_UserInfo.WorkOrder = workOrder;
                            NewuGlobal.TB_UserInfo = tB_UserInfo;
                            NewuGlobal.SoftConfig.SetWorker(workGroup, workOrder);
                        }
                        else
                        {
                            msg = NewuGlobal.GetRes("000072");
                        }
                    }
                    else
                    {
                        msg = NewuGlobal.GetRes("000073");
                    }
                    return flag;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_UserInfoRepository").Error(ex.ToString());
                msg = "";
                return false;
            }
        }
    }
}