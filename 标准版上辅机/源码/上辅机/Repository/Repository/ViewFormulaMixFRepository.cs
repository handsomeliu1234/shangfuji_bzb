using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repository
{
    public class ViewFormulaMixFRepository : BaseDAL<View_FormulaMixF>
    {
        public List<View_FormulaMixF> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * FROM View_FormulaMixF");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += " where " + strWhere;
                    }
                    List<View_FormulaMixF> view_FormulaMixes = connection.Query<View_FormulaMixF>(sqlStr).AsList();
                    return view_FormulaMixes;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaMixFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<View_FormulaMixF> GetList(int top, string strWhere, string filedOrder)
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

                    sqlStr += " * FROM View_FormulaMixF";

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += (" where " + strWhere);
                    }

                    sqlStr += (" order by " + filedOrder);
                    List<View_FormulaMixF> view_FormulaMixes = connection.Query<View_FormulaMixF>(sqlStr).AsList();
                    return view_FormulaMixes;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaMixFRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}