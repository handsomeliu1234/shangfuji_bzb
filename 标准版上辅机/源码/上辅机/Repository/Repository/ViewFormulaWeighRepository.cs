using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repository
{
    public class ViewFormulaWeighRepository : BaseDAL<View_FormulaWeigh>
    {
        public List<View_FormulaWeigh> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * From View_FormulaWeigh where " + strWhere);
                    List<View_FormulaWeigh> view_FormulaWeigh = connection.Query<View_FormulaWeigh>(sqlStr).AsList();
                    return view_FormulaWeigh;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaWeighRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<View_FormulaWeigh> GetModelList(int top, string strWhere, string filedOrder)
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

                    sqlStr += " * FROM View_FormulaWeigh";
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += (" where " + strWhere);
                    }
                    sqlStr += (" order by " + filedOrder);
                    List<View_FormulaWeigh> view_FormulaWeigh = connection.Query<View_FormulaWeigh>(sqlStr).AsList();
                    return view_FormulaWeigh;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaWeighRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}