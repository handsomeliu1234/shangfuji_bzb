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
    public class RPT_CurveFRepository : BaseDAL<RPT_CurveF>
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

        public bool Add(RPT_CurveF model)
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
                                                      @Lot,PlanQty,
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
                                                      @Is_Read)", GetTableName(typeof(RPT_CurveF)));
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveFRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(RPT_CurveF model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    string sqlStr = string.Format(@"update [{0}] set
                                                       CurveID = @CurveID,
                                                       LastUpdateTime = @LastUpdateTime,
                                                       UpdateTotal = @UpdateTotal,
                                                       RealTime = @RealTime,
                                                       Temp = @Temp,
                                                       Power = @Power,
                                                       Press= @Press,
                                                       Energy = @Energy,
                                                       Speed = @Speed,
                                                       Reserve1 = @Reserve1,
                                                       Reserve2 = @Reserve2,
                                                       Reserve3 = @Reserve3,
                                                       Reserve4 = @Reserve4,
                                                       Reserve5 = @Reserve5,
                                                       VersionID = @VersionID,
                                                       Is_Read = @Is_Read,
                                                       ReadTime = @ReadTime
                                                       where CurveID = @CurveID", GetTableName(typeof(RPT_CurveF)));
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveFRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<RPT_CurveF> GetList(string where, DateTime dt)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = dt.Year;
                    string sqlStr = string.Format(@"select CurveID, CreateTime, LastUpdateTime, UpdateTotal, OrderID, DeviceCode, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, RealTime, Temp, Power, Press, Energy, Speed, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}]", GetTableName(typeof(RPT_CurveF)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += " where " + where;

                    List<RPT_CurveF> rPT_Curve = connection.Query<RPT_CurveF>(sqlStr).AsList();
                    return rPT_Curve;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<RPT_CurveF> GetList(string where, PM_OrderTran ordertran)
        {
            try
            {
                using (IDbConnection connection = ConnectionXFData)
                {
                    TableYear = ordertran.Savetime.Year;
                    string sqlStr = string.Format(@"select CurveID, CreateTime, LastUpdateTime, UpdateTotal, OrderID, DeviceCode, MaterialCode, VersionNo, Lot, PlanQty, FactOrder, RealTime, Temp, Power, Press, Energy, Speed, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5,VersionID,Is_Read,ReadTime  FROM [{0}] ", GetTableName(typeof(RPT_CurveF)));
                    if (!string.IsNullOrEmpty(where))
                        sqlStr += " where " + where;

                    List<RPT_CurveF> rPT_Curve = connection.Query<RPT_CurveF>(sqlStr).AsList();
                    return rPT_Curve;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_CurveFRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}