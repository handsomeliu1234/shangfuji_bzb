using Dapper;
using Newtonsoft.Json;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repository
{
    public class TB_FormulaOperateLogRepository : BaseDAL<TB_FormulaOperateLog>
    {
        /// <summary>
        /// 将修改配方前的称量数据、密炼工艺部、系统参数分别转换为字符串保存到对应字段中
        /// </summary>
        /// <param name="Weigh"></param>
        /// <param name="mix"></param>
        /// <param name="techParam"></param>
        /// <returns></returns>
        public bool Add(List<FormulaWeigh> weigh, List<FormulaMix> mix, List<FormulaTechParam> techParam, List<FormulaMixF> mixF, List<FormulaTechParamF> techParamF, string materialID, string materialCode)
        {
            try
            {
                string strWeigh = JsonConvert.SerializeObject(weigh);
                string strMix = JsonConvert.SerializeObject(mix);
                string strTechParam = JsonConvert.SerializeObject(techParam);
                string strMixF = JsonConvert.SerializeObject(mixF);
                string strTechParamF = JsonConvert.SerializeObject(techParamF);
                using (IDbConnection connection = ConnectionXF)
                {
                    string strSql = string.Format(@"insert into dbo.[{0}] (
                                                        [MaterialID],
                                                        [MaterialCode],
                                                        [FormulaWeight],
                                                        [FormulaMixStep],
                                                        [FormulaTechParam],
                                                        [FormulaMixStepF],
                                                        [FormulaTechParamF],
                                                        [SaveTime])
                                                    Values(
                                                        @MaterialID,
                                                        @MaterialCode,
                                                        @FormulaWeight,
                                                        @FormulaMixStep,
                                                        @FormulaTechParam,
                                                        @FormulaMixStepF,
                                                        @FormulaTechParamF,
                                                        @SaveTime)", EntityHelper.GetTableName(typeof(TB_FormulaOperateLog)));
                    int row = connection.Execute(strSql, new
                    {
                        FormulaWeight = strWeigh,
                        FormulaMixStep = strMix,
                        FormulaTechParam = strTechParam,
                        FormulaMixStepF = strMixF,
                        FormulaTechParamF = strTechParamF,
                        MaterialID = materialID,
                        MaterialCode = materialCode,
                        SaveTime = DateTime.Now
                    });
                    if (row > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FormulaOperateLogRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<TB_FormulaOperateLog> GetList(string materialID, DateTime startTime, DateTime endTime)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strSql = string.Format(@"select [id], [MaterialID], [MaterialCode], [FormulaWeight], [FormulaMixStep], [FormulaTechParam], [FormulaMixStepF], [FormulaTechParamF], [Version], [SaveTime] FROM [dbo].[{0}] where MaterialID = @MaterialID and SaveTime >= @startTime and SaveTime <= @endTime order by SaveTime desc", EntityHelper.GetTableName(typeof(TB_FormulaOperateLog)));
                    List<TB_FormulaOperateLog> tB_FormulaOperateLogs = connection.Query<TB_FormulaOperateLog>(strSql, new
                    {
                        MaterialID = materialID,
                        startTime = startTime,
                        endTime = endTime
                    }).AsList();
                    return tB_FormulaOperateLogs;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FormulaOperateLogRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}