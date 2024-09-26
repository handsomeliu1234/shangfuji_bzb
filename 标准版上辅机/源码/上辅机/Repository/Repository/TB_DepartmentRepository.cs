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
    public class TB_DepartmentRepository : BaseDAL<TB_Department>
    {
        public TB_DepartmentRepository()
        {
        }

        public bool Add(TB_Department model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into TB_Department(
                                                            DepartmentID,
                                                            DepartmentCode,
                                                            DepartmentName,
                                                            DepartmentRemark,
                                                            ParentDepartmentID,
                                                            DepartmentJaneSpell,
                                                            SaveTime,
                                                            Reserve1,
                                                            Reserve2,
                                                            Reserve3,
                                                            Reserve4,
                                                            Reserve5)
                                                        values (
                                                            NEWID(),
                                                            @DepartmentCode,
                                                            @DepartmentName,
                                                            @DepartmentRemark,
                                                            @ParentDepartmentID,
                                                            @DepartmentJaneSpell,
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
                NewuGlobal.LogCat("TB_DepartmentRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public new bool Update(TB_Department model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"update TB_Department set
                                                            DepartmentCode = @DepartmentCode,
                                                            DepartmentName = @DepartmentName,
                                                            DepartmentRemark = @DepartmentRemark,
                                                            ParentDepartmentID = @ParentDepartmentID,
                                                            DepartmentJaneSpell = @DepartmentJaneSpell,
                                                            SaveTime = @SaveTime,
                                                            Reserve1 = @Reserve1,
                                                            Reserve2 = @Reserve2,
                                                            Reserve3 = @Reserve3,
                                                            Reserve4 = @Reserve4,
                                                            Reserve5 = @Reserve5
                                                         where DepartmentID = @DepartmentID", model);
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
                NewuGlobal.LogCat("TB_DepartmentRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public bool DeleteList(string DepartmentIDlist)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"delete from TB_Department where DepartmentID in (" + DepartmentIDlist + ")");
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
                NewuGlobal.LogCat("TB_DepartmentRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public TB_Department GetModel(string DepartmentID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select top 1 DepartmentID, DepartmentCode, DepartmentName, DepartmentRemark, ParentDepartmentID, DepartmentJaneSpell, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_Department where DepartmentID = @DepartmentID");
                    List<TB_Department> getModel = dbConnection.Query<TB_Department>(sqlStr, new
                    {
                        DepartmentID = DepartmentID
                    }).ToList();
                    if (getModel.Count > 0)
                    {
                        return getModel[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_DepartmentRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_Department> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select DepartmentID, DepartmentCode, DepartmentName, DepartmentRemark, ParentDepartmentID, DepartmentJaneSpell, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_Department ");
                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_Department> lsGetList = dbConnection.Query<TB_Department>(sqlStr.ToString()).ToList();
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
                NewuGlobal.LogCat("TB_DepartmentRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_Department> GetModelList(string strWhere)
        {
            return this.GetList(strWhere);
        }

        public List<TB_Department> GetAllList()
        {
            return this.GetList("");
        }
    }
}