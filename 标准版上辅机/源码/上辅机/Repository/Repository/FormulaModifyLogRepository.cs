using Dapper;
using Repository.DAL;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Repository
{
    /// <summary>
    /// 配方修改记录仓库
    /// </summary>
    public class FormulaModifyLogRepository : BaseDAL<FormulaModifyLog>
    {
        private IDbConnection db;

        public FormulaModifyLogRepository()
        {
        }

        public bool Add(FormulaModifyLog model)
        {
            using (db = ConnectionXF)
            {
                try
                {
                    int effectRow = db.Execute(@"INSERT INTO [dbo].[FormulaModifyLog] (
                                                        [FormulaModifyLogID],
                                                        [DeviceID],
                                                        [DeviceCode],
                                                        [FormulaID],
                                                        [FormulaCode],
                                                        [WeightInfoBefore],
                                                        [TechInfoBefore],
                                                        [WeightInfoAfter],
                                                        [TechInfoAfter],
                                                        [SaveTime],
                                                        [SaveUser])
                                                 VALUES (
                                                        NEWID(),
                                                        @DeviceID,
                                                        @DeviceCode,
                                                        @FormulaID,
                                                        @FormulaCode,
                                                        @WeightInfoBefore,
                                                        @TechInfoBefore,
                                                        @WeightInfoAfter,
                                                        @TechInfoAfter,
                                                        @SaveTime,
                                                        @SaveUser)", model);
                    if (effectRow != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<FormulaModifyLog> GetList(string deviceID, DateTime st, DateTime et)
        {
            using (db = ConnectionXF)
            {
                try
                {
                    if (deviceID != "")
                    {
                        return db.Query<FormulaModifyLog>(@"SELECT [FormulaModifyLogID], [DeviceID], [DeviceCode],[FormulaID], [FormulaCode], [WeightInfoBefore], [TechInfoBefore], [WeightInfoAfter], [TechInfoAfter], [SaveTime], [SaveUser] FROM [dbo].[FormulaModifyLog] where [DeviceID] = @DeviceID and [SaveTime] >= @St and [SaveTime] <= @Et order by [DeviceID],[SaveTime]", new { DeviceID = deviceID, St = st, Et = et }).ToList();
                    }
                    else
                    {
                        return db.Query<FormulaModifyLog>(@"SELECT [FormulaModifyLogID], [DeviceID], [DeviceCode], [FormulaID], [FormulaCode], [WeightInfoBefore], [TechInfoBefore], [WeightInfoAfter], [TechInfoAfter], [SaveTime], [SaveUser] FROM [dbo].[FormulaModifyLog] where [SaveTime] >= @St and [SaveTime] <= @Et order by [DeviceID],[SaveTime]", new { St = st, Et = et }).ToList();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}