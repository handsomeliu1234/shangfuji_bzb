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
    public class SYS_TypeCodeRepository : BaseDAL<SYS_TypeCode>
    {
        public bool Add(SYS_TypeCode model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strSql = string.Format(@"insert into SYS_TypeCode(
                                                        TypeCodeID,
                                                        TypeCodeName,
                                                        TypeCodeDesc,
                                                        TypeCodeSpell,
                                                        ParentTypeCodeID,
                                                        ParentTypeCodeDataSet,
                                                        SaveTime,
                                                        Enable,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                    values (
                                                        NEWID(),
                                                        @TypeCodeName,
                                                        @TypeCodeDesc,
                                                        @TypeCodeSpell,
                                                        @ParentTypeCodeID,
                                                        @ParentTypeCodeDataSet,
                                                        @SaveTime,
                                                        @Enable,
                                                        @Reserve1,
                                                        @Reserve2,
                                                        @Reserve3,
                                                        @Reserve4,
                                                        @Reserve5)");
                    int rows = connection.Execute(strSql.ToString(), model);
                    if (rows > 0)
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
                NewuGlobal.LogCat("SYS_TypeCodeRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool DeleteList(string typeCodeID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from SYS_TypeCode where TypeCodeID = @TypeCodeID ");
                    int rows = connection.Execute(strSql.ToString(), new
                    {
                        TypeCodeID = typeCodeID
                    });
                    if (rows > 0)
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
                NewuGlobal.LogCat("SYS_TypeCodeRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<SYS_TypeCode> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select TypeCodeID, TypeCodeName, TypeCodeDesc, TypeCodeSpell, ParentTypeCodeID, ParentTypeCodeDataSet, SaveTime, Enable,Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_TypeCode ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    List<SYS_TypeCode> sYS_TypeCodes = connection.Query<SYS_TypeCode>(strSql.ToString()).AsList();
                    return sYS_TypeCodes;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TypeCodeRepository").Error(ex.ToString());
                return null;
            }
        }

        public SYS_TypeCode GetModel(string typeCodeID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 TypeCodeID, TypeCodeName, TypeCodeDesc, TypeCodeSpell, ParentTypeCodeID, ParentTypeCodeDataSet, SaveTime, Enable,Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_TypeCode where TypeCodeID = @TypeCodeID ");
                    SYS_TypeCode sYS_TypeCode = connection.QueryFirstOrDefault<SYS_TypeCode>(sqlStr, new
                    {
                        TypeCodeID = typeCodeID
                    });
                    return sYS_TypeCode;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TypeCodeRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 先调用GetList一次性获取所有typecode集合
        /// </summary>
        /// <param name="_typeCodeEnum"></param>
        /// <returns></returns>
        public string GetTypeCodeNameByEnum(TypeCodeEnum _typeCodeEnum)
        {
            try
            {
                string codeStr = "";
                switch (_typeCodeEnum)
                {
                    case TypeCodeEnum.T炭黑:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "炭黑").TypeCodeName;
                        break;

                    case TypeCodeEnum.T粉料:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "粉料").TypeCodeName;
                        break;

                    case TypeCodeEnum.T白炭黑:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "白炭黑").TypeCodeName;
                        break;

                    case TypeCodeEnum.T胶料:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "胶料").TypeCodeName;
                        break;

                    case TypeCodeEnum.T塑解剂:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "塑解剂").TypeCodeName;
                        break;

                    case TypeCodeEnum.T油料:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "油料").TypeCodeName;
                        break;

                    case TypeCodeEnum.T药品:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "药品").TypeCodeName;
                        break;

                    case TypeCodeEnum.T硅烷:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "硅烷").TypeCodeName;
                        break;

                    case TypeCodeEnum.T母炼配方:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "上辅机母炼").TypeCodeName;
                        break;

                    case TypeCodeEnum.T终炼配方:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "上辅机终炼").TypeCodeName;
                        break;

                    case TypeCodeEnum.T小药配方:
                        codeStr = NewuGlobal.TypeCodeList.Find(e => e.TypeCodeDesc == "小药").TypeCodeName;
                        break;

                    case TypeCodeEnum.T原材料类型:
                        codeStr = "RawType";
                        break;

                    case TypeCodeEnum.T配方类型:
                        codeStr = "FormulaType";
                        break;
                }

                return codeStr;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_TypeCodeRepository").Error(ex.ToString());
                return "";
            }
        }
    }
}