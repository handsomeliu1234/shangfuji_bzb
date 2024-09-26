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
    public class ViewFormulaTechParamRepository : BaseDAL<View_FormulaTechParam>
    {
        public List<View_FormulaTechParam> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * From View_FormulaTechParam where " + strWhere);
                    List<View_FormulaTechParam> view_FormulatechParam = connection.Query<View_FormulaTechParam>(sqlStr).AsList();
                    return view_FormulatechParam;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaTechParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<View_FormulaTechParam> GetModelList(int top, string strWhere, string filedOrder)
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

                    sqlStr += " * FROM View_FormulaTechParam";
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr += (" where " + strWhere);
                    }
                    sqlStr += (" order by " + filedOrder);
                    List<View_FormulaTechParam> view_FormulatechParam = connection.Query<View_FormulaTechParam>(sqlStr).AsList();
                    return view_FormulatechParam;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewFormulaTechParamRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}