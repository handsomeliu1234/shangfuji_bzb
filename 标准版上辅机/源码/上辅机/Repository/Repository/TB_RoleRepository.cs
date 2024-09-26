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
    public class TB_RoleRepository : BaseDAL<TB_Role>
    {
        public TB_RoleRepository()
        {
        }

        public bool Add(TB_Role model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into TB_Role(
                                                            RoleID,
                                                            RoleName,
                                                            CreateTime,
                                                            RoleRemark,
                                                            SaveUserID,
                                                            SaveTime,
                                                            Reserve1,
                                                            Reserve2,
                                                            Reserve3,
                                                            Reserve4,
                                                            Reserve5)
                                                        values (
                                                            NEWID(),
                                                            @RoleName,
                                                            @CreateTime,
                                                            @RoleRemark,
                                                            @SaveUserID,
                                                            @SaveTime,
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
                NewuGlobal.LogCat("TB_RoleRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public new bool Update(TB_Role model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"update TB_Role set
                                                            RoleName = @RoleName,
                                                            CreateTime = @CreateTime,
                                                            RoleRemark = @RoleRemark,
                                                            SaveUserID = @SaveUserID,
                                                            SaveTime= @SaveTime,
                                                            Reserve1 = @Reserve1,
                                                            Reserve2 = @Reserve2,
                                                            Reserve3 = @Reserve3,
                                                            Reserve4 = @Reserve4,
                                                            Reserve5 = @Reserve5
                                                         where RoleID = @RoleID", model);
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
                NewuGlobal.LogCat("TB_RoleRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string RoleID, bool IsChecked)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    TB_PrivilegeRepository PrivilegeRepository = new TB_PrivilegeRepository();
                    if (IsChecked)
                    {
                        bool isAccessPrivilege = PrivilegeRepository.Delete(RoleID);
                        bool isAccessRole = Delete(RoleID);

                        return isAccessPrivilege && isAccessRole;
                    }
                    else
                    {
                        bool isAccessRole = this.Delete(RoleID);

                        return isAccessRole;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_RoleRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string roleID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"delete from TB_Role where RoleID = @RoleID", new
                    {
                        RoleID = roleID
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
                NewuGlobal.LogCat("TB_RoleRepository").Error(ex.ToString());
                return false;
            }
        }

        public TB_Role GetModel(string roleID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select top 1 RoleID, RoleName, CreateTime, RoleRemark, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_Role where RoleID = @RoleID");
                    List<TB_Role> lsGetModel = dbConnection.Query<TB_Role>(sqlStr, new
                    {
                        RoleID = roleID
                    }).ToList();
                    if (lsGetModel.Count != 0)
                    {
                        return lsGetModel[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_RoleRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_Role> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select RoleID, RoleName, CreateTime, RoleRemark, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_Role  ");
                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_Role> lsGetList = dbConnection.Query<TB_Role>(sqlStr.ToString()).ToList();
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
                NewuGlobal.LogCat("TB_RoleRepository").Error(ex.ToString());
                return null;
            }
        }

    }
}