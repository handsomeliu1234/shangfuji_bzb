using Dapper;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Repository.Model;

namespace Repository.Repository
{
    /// <summary>
    /// 自动校秤报表仓库
    /// </summary>
    public class RPT_AutoCheckScaleMixRepository : BaseDAL<RPT_AutoCheckScale>
    {
        public RPT_AutoCheckScaleMixRepository()
        {
        }

        public List<RPT_AutoCheckScale> GetList(string devicecode, DateTime st, DateTime et)
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                if (devicecode != "")
                {
                    return dbConnection.Query<RPT_AutoCheckScale>(@"select * from [dbo].[RPT_AutoCheckScale] where [DeviceCode] = @DeviceCode and [SaveTime] >= @St and [SaveTime] <= @Et order by [SaveTime],[CheckScaleNo]", new
                    {
                        DeviceCode = devicecode,
                        St = st,
                        Et = et
                    }).ToList();
                }

                {
                    return dbConnection.Query<RPT_AutoCheckScale>(@"select * from [dbo].[RPT_AutoCheckScale] where [SaveTime] >= @St and [SaveTime] <= @Et order by [SaveTime],[CheckScaleNo]", new
                    {
                        St = st,
                        Et = et
                    }).ToList();
                }
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public bool AddList(IList<RPT_AutoCheckScale> modelList)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"INSERT INTO [dbo].[RPT_AutoCheckScale] (
                                                            [ID],
                                                            [DeviceCode]
                                                            [CheckScaleNo]
                                                            [ScaleName]
                                                            [SetError]
                                                            [RealWeight]
                                                            [Result]
                                                            [SaveTime]
                                                            [SaveUser])
                                                        VALUES (
                                                            @ID,
                                                            @DeviceCode,
                                                            @CheckScaleNo,
                                                            @ScaleName,
                                                            @ScaleWeight,
                                                            @SetError,
                                                            @RealWeight,
                                                            @Result,
                                                            @SaveTime,
                                                            @SaveUser) ", modelList);
                    if (effectRow != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 是否校核过秤
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="dt"></param>
        /// <param name="scaleCount"></param>
        /// <returns></returns>
        public bool IsCheckScale(string deviceCode, DateTime dt, int scaleCount)
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                int count = dbConnection.ExecuteScalar<int>(@"select count(*) from (select distinct [CheckScaleNo] from [RPT_AutoCheckScale] where DeviceCode = @DeviceCode and [SaveTime] >= @SaveTime and [Result] = 1) as a", new
                {
                    DeviceCode = deviceCode,
                    SaveTime = dt
                });
                if (count == scaleCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}