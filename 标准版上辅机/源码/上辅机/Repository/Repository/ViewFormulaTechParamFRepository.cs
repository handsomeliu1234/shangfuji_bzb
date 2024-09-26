using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repository
{
    public class ViewFormulaTechParamFRepository : BaseDAL<View_FormulaTechParamF>
    {
        public List<View_FormulaTechParamF> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * From View_FormulaTechParamF where " + strWhere);
                    List<View_FormulaTechParamF> view_FormulatechParam = connection.Query<View_FormulaTechParamF>(sqlStr).AsList();
                    return view_FormulatechParam;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaTechParamFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<View_FormulaTechParamF> GetModelList(int top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select ");
                    if (top > 0)
                    {
                        sqlStr += "top " + top.ToString();
                    }

                    sqlStr += " * FROM View_FormulaTechParamF";
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += (" where " + strWhere);
                    }
                    sqlStr += (" order by " + filedOrder);
                    List<View_FormulaTechParamF> view_FormulatechParam = connection.Query<View_FormulaTechParamF>(sqlStr).AsList();
                    return view_FormulatechParam;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaTechParamFRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}