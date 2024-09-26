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
    public class ViewFormulaWeighFRepository : BaseDAL<View_FormulaWeighF>
    {
        public List<View_FormulaWeighF> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * From View_FormulaWeigh where " + strWhere);
                    List<View_FormulaWeighF> view_FormulaWeigh = connection.Query<View_FormulaWeighF>(sqlStr).AsList();
                    return view_FormulaWeigh;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaWeighFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<View_FormulaWeighF> GetModelList(int top, string strWhere, string filedOrder)
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

                    sqlStr += " * FROM View_FormulaWeighF";
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += (" where " + strWhere);
                    }
                    sqlStr += (" order by " + filedOrder);
                    List<View_FormulaWeighF> view_FormulaWeigh = connection.Query<View_FormulaWeighF>(sqlStr).AsList();
                    return view_FormulaWeigh;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaWeighFRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}