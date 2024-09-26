using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class PM_DevicePartOrderTranRepository : BaseDAL<PM_DevicePartOrderTran>
    {
        public List<PM_DevicePartOrderTran> GetModelList(string strWhere)
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select DevicePartOrderTranID, OrderID, DeviceCode, DeviceID, DevicePartCode, DevicePartID, MaterialCode, MaterialID, VersionNo, Lot, SetBatch, Savetime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5  FROM PM_DevicePartOrderTran ");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    var list = dbConnection.Query<PM_DevicePartOrderTran>(strSql.ToString()).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    NewuGlobal.LogCat("PM_DevicePartOrderTranRepository").Error(ex.ToString());
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据部件编码，从PM_DevicePartOrderTran中获取部件正在生产的订单
        /// </summary>
        /// <param name="devicePartCode"></param>
        /// <returns></returns>
        public PM_DevicePartOrderTran GetDevicePartOrder(string devicePartCode)
        {
            try
            {
                string strWhere = "DevicePartCode='" + devicePartCode + "' and DeviceCode='" + NewuGlobal.SoftConfig.DeviceCode + "'";

                List<PM_DevicePartOrderTran> tranList = GetModelList(strWhere);

                if (tranList.Count != 1)
                {
                    NewuGlobal.LogCat("PM_DevicePartOrderTranRepository").Info(devicePartCode + "进胶状态判定出现异常；表newu_order_tran中数据为：" + tranList.Count + "笔；");
                    return null;
                }

                return tranList[0];
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PM_DevicePartOrderTranRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}