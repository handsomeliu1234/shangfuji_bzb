using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TB_SecurityRepository : BaseDAL<TB_Security>
    {
        public TB_SecurityRepository()
        {
        }

        public bool Add(TB_Security model)
        {
            try
            {
                using (IDbConnection db = ConnectionXF)
                {
                    int effectRow = db.Execute(@"Insert into [dbo].[TB_Security](
                                                        [ID],
                                                        [DeviceID],
                                                        [DeviceCode],
                                                        [Tag],
                                                        [Addr],
                                                        [IsUsed],
                                                        [SecurityDesc],
                                                        [SaveUser],
                                                        [SaveTime])
                                                    Values(
                                                        NEWID(),
                                                        @DeviceID,
                                                        @DeviceCode,
                                                        @Tag,
                                                        @Addr,
                                                        @IsUsed,
                                                        @SecurityDesc,
                                                        @SaveUser,
                                                        @SaveTime)", model);
                    if (effectRow > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_SecurityRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string condition, object param)
        {
            try
            {
                using (IDbConnection db = ConnectionXF)
                {
                    int effectRow = db.Execute(@"delete from [dbo].[TB_Security] WHERE " + condition, param);
                    if (effectRow > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_SecurityRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<TB_Security> GetList(string deviceID)
        {
            try
            {
                using (IDbConnection db = ConnectionXF)
                {
                    if (deviceID != null && deviceID != "")
                    {
                        return db.Query<TB_Security>(@"SELECT [ID], [DeviceID], [DeviceCode], [Tag], [Addr], [IsUsed], [SecurityDesc], [SaveUser], [SaveTime] FROM [dbo].[TB_Security] where [DeviceID] = @DeviceID order by [Addr]", new
                        {
                            DeviceID = deviceID
                        }).ToList();
                    }
                    else
                    {
                        return db.Query<TB_Security>(@"SELECT [ID], [DeviceID], [DeviceCode], [Tag], [Addr], [IsUsed], [SecurityDesc], [SaveUser], [SaveTime]
                    FROM [dbo].[TB_Security] order by [Addr] ").ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_SecurityRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_Security> GetUsedList(string deviceID)
        {
            using (IDbConnection db = ConnectionXF)
            {
                if (deviceID != null && deviceID != "")
                {
                    return db.Query<TB_Security>(@"SELECT [ID], [DeviceID], [DeviceCode], [Tag], [Addr], [IsUsed], [SecurityDesc], [SaveUser], [SaveTime] FROM [dbo].[TB_Security] where [DeviceID] = @DeviceID and [IsUsed] = 1 order by [Addr]", new
                    {
                        DeviceID = deviceID
                    }).ToList();
                }
                else
                {
                    return db.Query<TB_Security>(@"SELECT [ID],[DeviceID], [DeviceCode], [Tag], [Addr], [IsUsed], [SecurityDesc], [SaveUser], [SaveTime] FROM [dbo].[TB_Security] where [IsUsed] = 1 order by [Addr] ").ToList();
                }
            }
        }

        public new bool Update(TB_Security model)
        {
            try
            {
                using (IDbConnection db = ConnectionXF)
                {
                    int effectRow = db.Execute(@"UPDATE [dbo].[TB_Security] set
                                                    [DeviceID] = @DeviceID,
                                                    [DeviceCode] = @DeviceCode,
                                                    [Tag] = @Tag,
                                                    [Addr] = @Addr,
                                                    [IsUsed] = @IsUsed,
                                                    [SecurityDesc] = @SecurityDesc,
                                                    [SaveUser] = @SaveUser,
                                                    [SaveTime] = @SaveTime,
                                                WHERE ID = @ID", model);
                    if (effectRow > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_SecurityRepository").Error(ex.ToString());
                return false;
            }
        }
    }
}