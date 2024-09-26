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
    public class FormulaMixFRepository : BaseDAL<FormulaMixF>
    {
        public int AddList(List<FormulaMixF> list)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaMixF(
                                                    FormulaMixID,
                                                    MaterialID,
                                                    MaterialCode,
                                                    DeviceID,
                                                    DeviceCode,
                                                    DevicePartID,
                                                    DevicePartCode,
                                                    StepOrder,
                                                    StepCode,
                                                    StepDesc,
                                                    ActionControlCode,
                                                    StepTime,
                                                    StepTemp,
                                                    StepPower,
                                                    StepEnergy,
                                                    StepPress,
                                                    StepSpeed,
                                                    KeepTime,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                            values (
                                                    NEWID(),
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @DevicePartID,
                                                    @DevicePartCode,
                                                    @StepOrder,
                                                    @StepCode,
                                                    @StepDesc,
                                                    @ActionControlCode,
                                                    @StepTime,
                                                    @StepTemp,
                                                    @StepPower,
                                                    @StepEnergy,
                                                    @StepPress,
                                                    @StepSpeed,
                                                    @KeepTime,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5)");
                    int effectRow = connection.Execute(sqlStr, list);

                    return effectRow;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMixFRepository").Error(ex.ToString());
                return 0;
            }
        }

        public bool DeleteAll(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from FormulaMixF where MaterialID = @MaterialID");
                    //配方中没有称量信息时导致返回0,if判断加入=0条件
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        MaterialID = materialID
                    });
                    if (effectRow >= 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMixFRepository").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 先删除再增加
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public void DeleteAndAddList(string materialID, List<FormulaMixF> list)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string deleteSql = string.Format(@"delete from FormulaMixF where MaterialID = @MaterialID");
                    string insertSql = string.Format(@"insert into FormulaMixF(
                                                    FormulaMixID,
                                                    MaterialID,
                                                    MaterialCode,
                                                    DeviceID,
                                                    DeviceCode,
                                                    DevicePartID,
                                                    DevicePartCode,
                                                    StepOrder,
                                                    StepCode,
                                                    StepDesc,
                                                    ActionControlCode,
                                                    StepTime,
                                                    StepTemp,
                                                    StepPower,
                                                    StepEnergy,
                                                    StepPress,
                                                    StepSpeed,
                                                    KeepTime,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                            values (
                                                    NEWID(),
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @DevicePartID,
                                                    @DevicePartCode,
                                                    @StepOrder,
                                                    @StepCode,
                                                    @StepDesc,
                                                    @ActionControlCode,
                                                    @StepTime,
                                                    @StepTemp,
                                                    @StepPower,
                                                    @StepEnergy,
                                                    @StepPress,
                                                    @StepSpeed,
                                                    @KeepTime,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5)");
                    IDbTransaction transaction = connection.BeginTransaction();
                    int effectRow = connection.Execute(deleteSql, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    int effectRows = connection.Execute(insertSql, list, transaction);
                    transaction.Commit();
                    if (effectRow > 0)
                        NewuGlobal.LogCat("FormulaMixFRepository").Info("FormulaMixF执行删除成功" + effectRow);
                    else
                    {
                        NewuGlobal.LogCat("FormulaMixFRepository").Info("FormulaMixF执行删除失败" + effectRow);
                    }

                    if (effectRows > 0)
                    {
                        NewuGlobal.LogCat("FormulaMixFRepository").Info("FormulaMixF执行添加成功" + effectRows);
                    }
                    else
                    {
                        NewuGlobal.LogCat("FormulaMixFRepository").Info("FormulaMixF执行添加失败" + effectRows);
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMixFRepository").Error(ex.ToString());
            }
        }

        public List<FormulaMixF> GetMixDetail(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select a.*,b.ActionControlNameCN,b.ActionControlNameEN from FormulaMixF a left join SYS_ActionControl b on a.ActionControlCode = b.ActionControlCode where a.MaterialID = @MaterialID order by a.StepOrder ASC");
                    List<FormulaMixF> formulaMixes = connection.Query<FormulaMixF>(sqlStr, new
                    {
                        MaterialID = materialID
                    }).AsList();
                    return formulaMixes;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMixFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<FormulaMixF> GetList(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select * FROM View_FormulaMixF where [MaterialID] = @MaterialID order by StepOrder");
                    List<FormulaMixF> view_FormulaMixes = connection.Query<FormulaMixF>(sqlStr, new
                    {
                        MaterialID = materialID
                    }).AsList();
                    return view_FormulaMixes;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaMixFRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}