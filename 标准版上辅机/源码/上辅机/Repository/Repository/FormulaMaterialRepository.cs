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
    public class FormulaMaterialRepository : BaseDAL<FormulaMaterial>
    {
        public bool Add(FormulaMaterial model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into [dbo].[FormulaMaterial] (
                                                    MaterialID,
                                                    MaterialCode,
                                                    VersionNo,
                                                    DeviceID,
                                                    DeviceCode,
                                                    MaterialDesc,
                                                    TypeCodeID,
                                                    MaterialFrom,
                                                    BarCode,
                                                    UseToMaterialID,
                                                    Enable,
                                                    SaveUserID,
                                                    SaveRealName,
                                                    SaveTime,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                                Values(
                                                    NEWID(),
                                                    @MaterialCode,
                                                    @VersionNo,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @MaterialDesc,
                                                    @TypeCodeID,
                                                    @MaterialFrom,
                                                    @BarCode,
                                                    @UseToMaterialID,
                                                    @Enable,
                                                    @SaveUserID,
                                                    @SaveRealName,
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
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from FormulaMix where MaterialID = @MaterialID");
                    string sqlStr1 = string.Format(@"delete from FormulaWeigh where MaterialID = @MaterialID");
                    string sqlStr2 = string.Format(@"delete from FormulaTechParam where MaterialID in(select MaterialID from FormulaMaterial where MaterialID = @MaterialID)");
                    string sqlStr3 = string.Format(@"delete from PM_DevicePartOrderTran where MaterialID = @MaterialID");
                    string sqlStr4 = string.Format(@"delete from FormulaMaterial where MaterialID = @MaterialID");

                    IDbTransaction transaction = connection.BeginTransaction();
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    int effectRow1 = connection.Execute(sqlStr1, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    int effectRow2 = connection.Execute(sqlStr2, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    int effectRow3 = connection.Execute(sqlStr3, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    int effectRow4 = connection.Execute(sqlStr4, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    transaction.Commit();

                    bool flag = false;
                    flag = GetResult(effectRow, "FormulaMix");
                    flag = GetResult(effectRow1, "FormulaWeigh");
                    flag = GetResult(effectRow2, "FormulaTechParam");
                    flag = GetResult(effectRow3, "PM_DevicePartOrderTran");
                    flag = GetResult(effectRow4, "FormulaMaterial");
                    return flag;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(List<FormulaWeigh> formulaWeights)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from FormulaMix where MaterialID = @MaterialID");
                    string sqlStr1 = string.Format(@"delete from FormulaWeigh where MaterialID = @MaterialID");
                    string sqlStr2 = string.Format(@"delete from FormulaTechParam where MaterialID in(select MaterialID from FormulaMaterial where MaterialID = @MaterialID)");
                    string sqlStr3 = string.Format(@"delete from PM_DevicePartOrderTran where MaterialID = @MaterialID");
                    string sqlStr4 = string.Format(@"delete from FormulaMaterial where MaterialID = @MaterialID");

                    IDbTransaction transaction = connection.BeginTransaction();
                    int effectRow = connection.Execute(sqlStr, formulaWeights, transaction);
                    int effectRow1 = connection.Execute(sqlStr1, formulaWeights, transaction);
                    int effectRow2 = connection.Execute(sqlStr2, formulaWeights, transaction);
                    int effectRow3 = connection.Execute(sqlStr3, formulaWeights, transaction);
                    int effectRow4 = connection.Execute(sqlStr4, formulaWeights, transaction);
                    transaction.Commit();

                    bool flag = false;
                    flag = GetResult(effectRow, "FormulaMix");
                    flag = GetResult(effectRow1, "FormulaWeigh");
                    flag = GetResult(effectRow2, "FormulaTechParam");
                    flag = GetResult(effectRow3, "PM_DevicePartOrderTran");
                    flag = GetResult(effectRow4, "FormulaMaterial");
                    return flag;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return false;
            }
        }

        private bool GetResult(int effectRow, string tableName)
        {
            try
            {
                if (effectRow > 0)
                {
                    NewuGlobal.LogCat("FormulaMaterialRepository").Info("表" + tableName + "执行删除成功");
                    return true;
                }
                else
                {
                    NewuGlobal.LogCat("FormulaMaterialRepository").Info("表" + tableName + "执行删除失败");
                    return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(FormulaMaterial model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update [dbo].[FormulaMaterial] set
                                                    MaterialID = @MaterialID,
                                                    MaterialCode = @MaterialCode,
                                                    VersionNo = @VersionNo,
                                                    DeviceID = @DeviceID,
                                                    DeviceCode = @DeviceCode,
                                                    MaterialDesc = @MaterialDesc,
                                                    TypeCodeID = @TypeCodeID,
                                                    MaterialFrom = @MaterialFrom,
                                                    BarCode = @BarCode,
                                                    UseToMaterialID = @UseToMaterialID,
                                                    Enable = @Enable,
                                                    SaveUserID = @SaveUserID,
                                                    SaveRealName = @SaveRealName,
                                                    SaveTime = @SaveTime,
                                                    Reserve1 = @Reserve1,
                                                    Reserve2 = @Reserve2,
                                                    Reserve3 = @Reserve3,
                                                    Reserve4 = @Reserve4,
                                                    Reserve5 = @Reserve5  where MaterialID = @MaterialID");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool UpdateSig(string materialID, int a)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update [dbo].[FormulaMaterial] set
                                                    Enable = @Enable
                                                    where MaterialID = @MaterialID");
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        MaterialID = materialID,
                        Enable = a
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<FormulaMaterial> GetList(string where)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select MaterialID, MaterialCode, VersionNo, DeviceID, DeviceCode, MaterialDesc, TypeCodeID, MaterialFrom, BarCode, UseToMaterialID, Enable, SaveUserID, SaveRealName, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM [dbo].[FormulaMaterial]");
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += " where " + where;
                    List<FormulaMaterial> formulaMaterials = connection.Query<FormulaMaterial>(sqlStr).AsList();
                    return formulaMaterials;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<FormulaMaterial> GetList(int top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    if (top > 0)
                    {
                        strSql.Append(" top " + top.ToString());
                    }
                    strSql.Append(" MaterialID, MaterialCode, VersionNo, DeviceID, DeviceCode, MaterialDesc, TypeCodeID, MaterialFrom, BarCode, UseToMaterialID, Enable, SaveUserID, SaveRealName, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5");
                    strSql.Append(" FROM FormulaMaterial ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    strSql.Append(" order by " + filedOrder);
                    List<FormulaMaterial> formulaMaterials = connection.Query<FormulaMaterial>(strSql.ToString()).AsList();
                    return formulaMaterials;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<FormulaMaterial> GetListByDeviceIDandTypeCodeID(int top, string deviceID, string typeCodeID, string filedOrder)
        {
            try
            {
                string where = "1=1";
                if (!string.IsNullOrEmpty(deviceID))
                    where += " and (DeviceID = '" + deviceID + "' or DeviceID='')";

                if (!string.IsNullOrEmpty(typeCodeID))
                    where += " and TypeCodeID = '" + typeCodeID + "'";

                List<FormulaMaterial> formulaMaterials = GetList(top, where, filedOrder);
                return formulaMaterials;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<FormulaMaterial> GetMaterialListByDeviceAndTypeCode(string deviceID, TypeCodeEnum[] typeEnum)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
                    string[] typeCodes = new string[typeEnum.Length];
                    for (int i = 0; i < typeEnum.Length; i++)
                    {
                        string typeCode = typeCodeRepository.GetTypeCodeNameByEnum(typeEnum[i]);
                        typeCodes[i] = typeCode;
                    }

                    string sqlStr = "select * from FormulaMaterial a, SYS_TypeCode b where a.TypeCodeID=b.TypeCodeID and (a.DeviceID='" + deviceID + "' or a.DeviceID='')";

                    string types = "";

                    for (int i = 0; i < typeCodes.Length; i++)
                    {
                        if (i == 0)
                        {
                            types = " b.TypeCodeName='" + typeCodes[i] + "' ";
                        }
                        else
                        {
                            types += " or b.TypeCodeName='" + typeCodes[i] + "' ";
                        }
                    }

                    sqlStr += " and (" + types + ")";
                    List<FormulaMaterial> formulaMaterials = connection.Query<FormulaMaterial>(sqlStr).AsList();
                    return formulaMaterials;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<FormulaMaterial> GetMaterialListByDeviceAndTypeCode(string deviceID, string typeCodeID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();

                    string sqlStr = string.Format(@"select * from FormulaMaterial a, SYS_TypeCode b where a.TypeCodeID = b.TypeCodeID and (a.DeviceID = @DeviceID or a.DeviceID='')  and a.TypeCodeID = @TypeCodeID");

                    List<FormulaMaterial> formulaMaterials = connection.Query<FormulaMaterial>(sqlStr, new
                    {
                        DeviceID = deviceID,
                        TypeCodeID = typeCodeID
                    }).AsList();
                    return formulaMaterials;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return null;
            }
        }

        public FormulaMaterial GetModel(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 MaterialID, MaterialCode, VersionNo, DeviceID, DeviceCode, MaterialDesc, TypeCodeID, MaterialFrom, BarCode, UseToMaterialID, Enable, SaveUserID, SaveRealName, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from FormulaMaterial where MaterialID = @MaterialID ");
                    FormulaMaterial formulaMaterial = connection.QueryFirstOrDefault<FormulaMaterial>(sqlStr, new
                    {
                        MaterialID = materialID
                    });
                    return formulaMaterial;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMaterialRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}