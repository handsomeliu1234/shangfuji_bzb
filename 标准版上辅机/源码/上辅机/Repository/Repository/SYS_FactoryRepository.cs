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
    public class SYS_FactoryRepository : BaseDAL<SYS_Factory>
    {
        public bool Add(SYS_Factory model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_Factory (
                                                      FactoryID,
                                                      FactoryName,
                                                      FactoryCode,
                                                      FactoryJaneSpell,
                                                      FactorySite,
                                                      FactoryPhone,
                                                      FactoryEmail,
                                                      FactoryAddress,
                                                      SaveTime,
                                                      Reserve1,
                                                      Reserve2,
                                                      Reserve3,
                                                      Reserve4,
                                                      Reserve5
                                                 values(
                                                      NEWID(),
                                                      @FactoryName,
                                                      @FactoryCode,
                                                      @FactoryJaneSpell,
                                                      @FactorySite,
                                                      @FactoryPhone,
                                                      @FactoryEmail,
                                                      @FactoryAddress,
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
                NewuGlobal.LogCat("SYS_FactoryRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(SYS_Factory model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_Factory set
                                                       FactoryID = @FactoryID,
                                                       FactoryName = @FactoryName,
                                                       FactoryCode = @FactoryCode,
                                                       FactoryJaneSpell = @FactoryJaneSpell,
                                                       FactorySite = @FactorySite,
                                                       FactoryPhone = @FactoryPhone,
                                                       FactoryEmail = @FactoryEmail,
                                                       FactoryAddress = @FactoryAddress,
                                                       SaveTime = @SaveTime,
                                                       Reserve1 = @Reserve1,
                                                       Reserve2 = @Reserve2,
                                                       Reserve3 = @Reserve3,
                                                       Reserve4 = @Reserve4,
                                                       Reserve5 = @Reserve5", model);
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_FactoryRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<SYS_Factory> GetModel(string strWhere)
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 FactoryID, FactoryName, FactoryCode, FactoryJaneSpell, FactorySite, FactoryPhone, FactoryEmail, FactoryAddress, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_Factory");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                var list = dbConnection.Query<SYS_Factory>(strSql.ToString()).ToList();
                return list;
            }
        }

        public List<SYS_Factory> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select FactoryID, FactoryName, FactoryCode, FactoryJaneSpell, FactorySite, FactoryPhone, FactoryEmail, FactoryAddress, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_Factory ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr = "where " + strWhere;
                    }
                    List<SYS_Factory> sYSFactory = connection.Query<SYS_Factory>(sqlStr).AsList();
                    return sYSFactory;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_FactoryRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}