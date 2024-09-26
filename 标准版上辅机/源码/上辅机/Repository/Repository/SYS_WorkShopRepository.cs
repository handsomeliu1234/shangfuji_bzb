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
    public class SYS_WorkShopRepository : BaseDAL<SYS_WorkShop>
    {
        public SYS_WorkShopRepository()
        {
        }

        public bool Add(SYS_WorkShop model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into SYS_WorkShop(
                                                        WorkshopID,
                                                        FactoryID,
                                                        ShopName,
                                                        WorkshopCode,
                                                        WorkshopJaneSpell,
                                                        SaveTime,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                    values (
                                                        NEWID(),
                                                        @FactoryID,
                                                        @ShopName,
                                                        @WorkshopCode,
                                                        @WorkshopJaneSpell,
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
                NewuGlobal.LogCat("SYS_WorkShopRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public new bool Update(SYS_WorkShop model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"update SYS_WorkShop set
                                                            FactoryID = @FactoryID,
                                                            ShopName = @ShopName,
                                                            WorkshopCode = @WorkshopCode,
                                                            WorkshopJaneSpell = @WorkshopJaneSpell,
                                                            SaveTime = @SaveTime,
                                                            Reserve1 = @Reserve1,
                                                            Reserve2 = @Reserve2,
                                                            Reserve3 = @Reserve3,
                                                            Reserve4 = @Reserve4,
                                                            Reserve5 = @Reserve5
                                                        where WorkshopID = @WorkshopID", model);
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
                NewuGlobal.LogCat("SYS_WorkShopRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_WorkShop GetModel(string workshopID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@" select top 1 WorkshopID, FactoryID, ShopName, WorkshopCode, WorkshopJaneSpell, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_WorkShop where WorkshopID = @WorkshopID");
                    List<SYS_WorkShop> lsGetModel = dbConnection.Query<SYS_WorkShop>(sqlStr, new
                    {
                        WorkshopID = workshopID
                    }).ToList();
                    if (lsGetModel.Count > 0)
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
                NewuGlobal.LogCat("SYS_WorkShopRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_WorkShop> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"select a.WorkshopID, a.FactoryID, b.FactoryName, a.ShopName, a.WorkshopCode, a.WorkshopJaneSpell, a.SaveTime, a.Reserve1, a.Reserve2, a.Reserve3, a.Reserve4, a.Reserve5 FROM SYS_WorkShop a  left join SYS_Factory b on a.FactoryID = b.FactoryID ");

                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    List<SYS_WorkShop> sYSWorkShop = dbConnection.Query<SYS_WorkShop>(strSql.ToString()).AsList();
                    return sYSWorkShop;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_WorkShopRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public List<SYS_WorkShop> GetModelList(string strWhere)
        {
            return GetList(strWhere);
        }

        public List<SYS_WorkShop> GetAllList()
        {
            return GetList("");
        }
    }
}