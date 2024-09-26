using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RPT_CurveRepository : BaseDAL<RPT_Curve>
    {
        /// <summary>
        /// 表名前缀年份
        /// </summary>
        public int TableYear
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTableName(Type type)
        {
            if (TableYear != 0)
            {
                return TableYear.ToString() + "_" + EntityHelper.GetTableName(type);
            }
            else
            {
                return EntityHelper.GetTableName(type);
            }
        }

        public bool Add(RPT_Curve model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    string sqlStr = string.Format(@"insert into [{0}](
                                                      CurveID,
                                                      CreateTime,
                                                      LastUpdateTime,
                                                      UpdateTotal,
                                                      OrderID,
                                                      DeviceCode,
                                                      MaterialCode,
                                                      VersionNo,
                                                      Lot,
                                                      PlanQty,
                                                      FactOrder,
                                                      RealTime,
                                                      Temp,
                                                      Power,
                                                      Press,
                                                      Energy,
                                                      Speed,
                                                      Reserve1,
                                                      Reserve2,
                                                      Reserve3,
                                                      Reserve4,
                                                      Reserve5,
                                                      VersionID,
                                                      Is_Read)
                                                  values(
                                                      @CurveID,
                                                      @CreateTime,
                                                      @LastUpdateTime,
                                                      @UpdateTotal,
                                                      @OrderID,
                                                      @DeviceCode,
                                                      @MaterialCode,
                                                      @VersionNo,
                                                      @Lot,
                                                      @PlanQty,
                                                      @FactOrder,
                                                      @RealTime,
                                                      @Temp,
                                                      @Power,
                                                      @Press,
                                                      @Energy,
                                                      @Speed,
                                                      @Reserve1,
                                                      @Reserve2,
                                                      @Reserve3,
                                                      @Reserve4,
                                                      @Reserve5,
                                                      @VersionID,
                                                      @Is_Read )", GetTableName(typeof(RPT_Curve)));
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(RPT_Curve model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    string sqlStr = string.Format(@"update [{0}] set
                                                       CurveID = @CurveID,
                                                       LastUpdateTime = @LastUpdateTime,
                                                       UpdateTotal = UpdateTotal+ @UpdateTotal,
                                                       RealTime = RealTime + @RealTime,
                                                       Temp = Temp + @Temp,
                                                       Power = Power + @Power,
                                                       Press= Press + @Press,
                                                       Energy = Energy + @Energy,
                                                       Speed = Speed + @Speed,
                                                       Reserve1 = Reserve1 + @Reserve1,
                                                       Reserve2 = Reserve2 + @Reserve2,
                                                       Reserve3 = Reserve3 + @Reserve3,
                                                       Reserve4 = Reserve4 + @Reserve4,
                                                       Reserve5 = Reserve5 + @Reserve5,
                                                       VersionID = @VersionID,
                                                       Is_Read = @Is_Read
                                                       where CurveID = @CurveID", GetTableName(typeof(RPT_Curve)));
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveRepository").Error(ex.ToString());
                return false;
            }
        }

        public RPT_Curve GetModel(string where, PM_OrderTran orderTran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = orderTran.StartTime.Value.Year;
                    string sqlStr = string.Format(@"select top (1) CurveID, CreateTime, LastUpdateTime, UpdateTotal, OrderID, DeviceCode, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, RealTime, Temp, Power, Press, Energy, Speed, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}]", GetTableName(typeof(RPT_Curve)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += " where " + where;

                    RPT_Curve rPT_Curve = connection.QueryFirstOrDefault<RPT_Curve>(sqlStr);
                    return rPT_Curve;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Curve> GetList(string where, DateTime dt)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = dt.Year;
                    string sqlStr = string.Format(@"select CurveID, CreateTime, LastUpdateTime, UpdateTotal, OrderID, DeviceCode, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, RealTime, Temp, Power, Press, Energy, Speed, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}]", GetTableName(typeof(RPT_Curve)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += " where " + where;

                    List<RPT_Curve> rPT_Curve = connection.Query<RPT_Curve>(sqlStr).AsList();
                    return rPT_Curve;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_Curve> GetList(string where, PM_OrderTran ordertran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = ordertran.StartTime.Value.Year;
                    string sqlStr = string.Format(@"select CurveID, CreateTime, LastUpdateTime, UpdateTotal, OrderID, DeviceCode, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, RealTime, Temp, Power, Press, Energy, Speed, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}] ", GetTableName(typeof(RPT_Curve)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += " where " + where;

                    List<RPT_Curve> rPT_Curve = connection.Query<RPT_Curve>(sqlStr).AsList();
                    return rPT_Curve;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}