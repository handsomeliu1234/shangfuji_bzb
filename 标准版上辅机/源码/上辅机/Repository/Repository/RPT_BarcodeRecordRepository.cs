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
    public class RPT_BarcodeRecordRepository : BaseDAL<RPT_BarcodeRecord>
    {
        public int TableYear
        {
            get; set;
        }

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

        public bool Add(RPT_BarcodeRecord model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into [{0}](
                                                      BarcordID,
                                                      OrderID,
                                                      DeviceCode,
                                                      MaterialCode,
                                                      TypeCodeName,
                                                      WeighMaterialCode,
                                                      SaveTime,
                                                      Reserve1,
                                                      Reserve2,
                                                      Reserve3,
                                                      Reserve4,
                                                      Reserve5,
                                                      Reserve6,
                                                      Reserve7)
                                               values(
                                                      @BarcordID,
                                                      @OrderID,
                                                      @DeviceCode,
                                                      @MaterialCode,
                                                      @TypeCodeName,
                                                      @WeighMaterialCode,
                                                      @SaveTime,
                                                      @Reserve1,
                                                      @Reserve2,
                                                      @Reserve3,
                                                      @Reserve4,
                                                      @Reserve5,
                                                      @Reserve6,
                                                      @Reserve7)", GetTableName(typeof(RPT_BarcodeRecord)));
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)

                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_BarcodeRecordRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<RPT_BarcodeRecord> GetList(string where)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select BarcordID, OrderID, DeviceCode, MaterialCode, TypeCodeName, WeighMaterialCode, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5, Reserve6, Reserve7 FROM [dbo].[{0}] Where " + where, GetTableName(typeof(RPT_BarcodeRecord)));
                    List<RPT_BarcodeRecord> rPT_Barcode = connection.Query<RPT_BarcodeRecord>(sqlStr).AsList();
                    return rPT_Barcode;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_BarcodeRecordRepository").Error(ex.ToString());
                return null;
            }
        }

        public bool IsExists(string orderID, string typeCodeName, string materialCode)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select count(1) from [{0}] where OrderID = @OrderID and TypeCodeName =@TypeCodeName and MaterialCode = @MaterialCode", GetTableName(typeof(RPT_BarcodeRecord)));
                    List<RPT_BarcodeRecord> rPT_Barcode = connection.Query<RPT_BarcodeRecord>(sqlStr, new
                    {
                        OrderID = orderID,
                        TypeCodeName = typeCodeName,
                        MaterialCode = materialCode
                    }).AsList();
                    if (rPT_Barcode.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_BarcodeRecordRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool UpdateBarcode(string reserve1, DateTime saveTime, string orderID, string materialCode)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update [{0}] set
                                                       Reserve1 = @Reserve1
                                                       SaveTime = @SaveTime
                                                  where
                                                       OrderID = @OrderID and MaterialCode = @MaterialCode", GetTableName(typeof(RPT_BarcodeRecord)));
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        Reserve1 = reserve1,
                        SaveTime = saveTime,
                        OrderID = orderID,
                        MaterialCode = materialCode
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("RPT_BarcodeRecordRepository").Error(ex.ToString());
                return false;
            }
        }
    }
}