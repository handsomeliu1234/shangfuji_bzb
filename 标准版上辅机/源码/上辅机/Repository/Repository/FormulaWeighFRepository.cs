using Dapper;
using Newu;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.Repository
{
    public class FormulaWeighFRepository : BaseDAL<FormulaWeighF>
    {
        public bool Add(FormulaWeighF model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaWeighF(
                                                    FormulaWeighID,
                                                    MaterialID,
                                                    MaterialCode,
                                                    DevicePartID,
                                                    DevicePartCode,
                                                    WeighMaterialID,
                                                    DeviceID,
                                                    DeviceCode,
                                                    WeighSetVal,
                                                    AllowError,
                                                    Weighorder,
                                                    DropOrder,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5,
                                                    Scanner,
                                                    Rubber)
                                             values(
                                                    NEWID();
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @DevicePartID,
                                                    @DevicePartCode,
                                                    @WeighMaterialID,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @WeighSetVal,
                                                    @AllowError,
                                                    @Weighorder,
                                                    @DropOrder,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5,
                                                    @Scanner,
                                                    @Rubber)");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return false;
            }
        }

        public int AddList(List<FormulaWeighF> list)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaWeighF(
                                                    FormulaWeighID,
                                                    MaterialID,
                                                    MaterialCode,
                                                    DevicePartID,
                                                    DevicePartCode,
                                                    WeighMaterialID,
                                                    DeviceID,
                                                    DeviceCode,
                                                    WeighSetVal,
                                                    AllowError,
                                                    Weighorder,
                                                    DropOrder,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5,
                                                    Scanner,
                                                    Rubber)
                                             values(
                                                    NEWID(),
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @DevicePartID,
                                                    @DevicePartCode,
                                                    @WeighMaterialID,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @WeighSetVal,
                                                    @AllowError,
                                                    @Weighorder,
                                                    @DropOrder,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5,
                                                    @Scanner,
                                                    @Rubber)");
                    int effectRow = connection.Execute(sqlStr, list);

                    return effectRow;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return 0;
            }
        }

        public bool DeleteAll(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from FormulaWeighF where MaterialID = @MaterialID");
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
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return false;
            }
        }

        public void DeleteAndInsert(string materialID, List<FormulaWeighF> list)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string deleteSql = string.Format(@"delete from FormulaWeighF where MaterialID = @MaterialID");
                    string addSql = string.Format(@"insert into FormulaWeighF(
                                                    FormulaWeighID,
                                                    MaterialID,
                                                    MaterialCode,
                                                    DevicePartID,
                                                    DevicePartCode,
                                                    WeighMaterialID,
                                                    WeighMaterialCode,
                                                    DeviceID,
                                                    DeviceCode,
                                                    WeighSetVal,
                                                    AllowError,
                                                    Weighorder,
                                                    DropOrder,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5,
                                                    Scanner,
                                                    Rubber)
                                             values(
                                                    NEWID(),
                                                    @MaterialID,
                                                    @MaterialCode,
                                                    @DevicePartID,
                                                    @DevicePartCode,
                                                    @WeighMaterialID,
                                                    @WeighMaterialCode,
                                                    @DeviceID,
                                                    @DeviceCode,
                                                    @WeighSetVal,
                                                    @AllowError,
                                                    @Weighorder,
                                                    @DropOrder,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5,
                                                    @Scanner,
                                                    @Rubber)");
                    IDbTransaction transaction = connection.BeginTransaction();
                    int deleteRow = connection.Execute(deleteSql, new
                    {
                        MaterialID = materialID
                    }, transaction);
                    int addRow = connection.Execute(addSql, list, transaction);
                    transaction.Commit();
                    if (deleteRow > 0)
                        NewuGlobal.LogCat("FormulaWeighFRepository").Info("FormulaWeighF执行删除成功" + deleteRow);
                    else
                    {
                        NewuGlobal.LogCat("FormulaWeighFRepository").Info("FormulaWeighF执行删除失败" + deleteRow);
                        transaction.Rollback();
                    }

                    if (addRow > 0)
                    {
                        NewuGlobal.LogCat("FormulaWeighFRepository").Info("FormulaWeighF执行添加成功" + addRow);
                    }
                    else
                    {
                        NewuGlobal.LogCat("FormulaWeighFRepository").Info("FormulaWeighF执行添加失败" + addRow);
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
            }
        }

        public List<FormulaWeighF> GetModelListNew(int top, string strWhere, string filedOrder)
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
                    strSql.Append(" * ");
                    strSql.Append(" FROM FormulaWeighF ");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    strSql.Append(" order by " + filedOrder);
                    List<FormulaWeighF> formulaWeighs = connection.Query<FormulaWeighF>(strSql.ToString()).AsList();
                    return formulaWeighs;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return null;
            }
        }

        public DataTable DropTable()
        {
            try
            {
                CreateTable cTable = new CreateTable();
                string[] fields = new string[] { "name", "value" };
                Type[] types = new Type[] { typeof(string), typeof(int) };
                //第几次投入密炼机
                object[,] values = new object[,] {
                    { "1", "1" },
                    { "2", "2" },
                    { "3", "3" }
                };

                DataTable dt = cTable.GetTable(fields, types, values);
                return dt;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<WeightDetailF> GetWeightDetail(string matID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder build = new StringBuilder();
                    build.Append("select e.MaterialID,e.MaterialCode,b.MaterialCode as WeightMaterialCode,");
                    build.Append("c.TypeCodeName,c.TypeCodeDesc,a.WeighSetVal,a.AllowError,a.DropOrder,a.WeighOrder,d.BinNo ");
                    build.Append("from FormulaWeighF a ");
                    build.Append("left join FormulaMaterial b on a.WeighMaterialID=b.MaterialID ");
                    build.Append("left join SYS_TypeCode c on b.TypeCodeID=c.TypeCodeID ");
                    build.Append("left join TB_BinSeting d on a.WeighMaterialID=d.MaterialID ");
                    build.Append("left join FormulaMaterial e on a.MaterialID=e.MaterialID ");
                    build.Append("where a.MaterialID='" + matID + "' ");
                    build.Append("order by a.DevicePartID,a.DropOrder,a.WeighOrder");

                    return connection.Query<WeightDetailF>(build.ToString()).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<FormulaWeighF> GetFormulaWeighFList(int top, string devicePartID, string materialID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select top {0} *  FROM FormulaWeigh where MaterialID = @MaterialID and DevicePartID = @DevicePartID order by DevicePartID,DropOrder,WeighOrder", "1");
                    return dbConnection.Query<FormulaWeighF>(sqlStr, new
                    {
                        MaterialID = materialID,
                        DevicePartID = devicePartID
                    }).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaWeighFRepository").Error(ex.ToString());
                return null;
            }
        }
    }

    public class WeightDetailF
    {
        private string MaterialID
        {
            get; set;
        }

        private string MaterialCode
        {
            get; set;
        }

        private string WeightMaterialCode
        {
            get; set;
        }

        private string TypeCodeName
        {
            get; set;
        }

        private string TypeCodeDesc
        {
            get; set;
        }

        private decimal WeighSetVal
        {
            get; set;
        }

        private decimal AllowError
        {
            get; set;
        }

        private int DropOrder
        {
            get; set;
        }

        private int WeighOrder
        {
            get; set;
        }

        private int BinNo
        {
            get; set;
        }
    }
}