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
    public class TB_AlarmRepository : BaseDAL<TB_Alarm>
    {
        public bool Add(TB_Alarm model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"Insert into [dbo].[TB_Alarm](
                                                            [AlarmID],
                                                            [DeviceID],
                                                            [DevicePartID],
                                                            [AlarmInfo],
                                                            [MemoryAddr],
                                                            [TagAddress],
                                                            [IsDisplay],
                                                            [SaveTime] )
                                                        Values(
                                                            NEWID(),
                                                            @DeviceID,
                                                            @DevicePartID,
                                                            @AlarmInfo,
                                                            @MemoryAddr,
                                                            @TagAddress,
                                                            @IsDisplay,
                                                            @SaveTime )", model);
                    if (effectRow != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_AlarmRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public bool Updata(TB_Alarm model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"Update [dbo].[TB_Alarm] set
                                                            [DeviceID] = @DeviceID,
                                                            [DevicePartID] = @DevicePartID,
                                                            [AlarmInfo] = @AlarmInfo,
                                                            [MemoryAddr] = @MemoryAddr,
                                                            [TagAddress] = @TagAddress,
                                                            [IsDisplay] = @IsDisplay,
                                                            [SaveTime] = @SaveTime,
                                                            [Reserve1] = @Reserve1,
                                                            [Reserve2] = @Reserve2,
                                                            [Reserve3] = @Reserve3,
                                                            [Reserve4] = @Reserve4,
                                                            [Reserve5] = @Reserve5 Where AlarmID = @AlarmID", model);
                    if (effectRow != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_AlarmRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string AlarmID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"Delete from [dbo].[TB_Alarm] Where AlarmID=@AlarmID", new
                    {
                        AlarmID = AlarmID
                    });

                    if (effectRow != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_AlarmRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool DeleteByDeviceID(string deviceid)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"Delete from [dbo].[TB_Alarm] Where DeviceID=@DeviceID", new
                    {
                        DeviceID = deviceid
                    });

                    if (effectRow != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_AlarmRepository").Error(ex.ToString());
                return false;
            }
        }

        public TB_Alarm GetModel(string AlarmID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select top 1 AlarmID, DeviceID, DevicePartID, AlarmInfo, MemoryAddr, TagAddress, IsDisplay, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from [dbo].[TB_Alarm] where AlarmID = @AlarmID");
                    List<TB_Alarm> tBAlarm = dbConnection.Query<TB_Alarm>(sqlStr, new
                    {
                        AlarmID = AlarmID
                    }).AsList();

                    if (tBAlarm.Count > 0)
                        return tBAlarm[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_AlarmRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_Alarm> GetModelList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select AlarmID, DeviceID, DevicePartID, AlarmInfo, MemoryAddr, TagAddress, IsDisplay, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM [dbo].[TB_Alarm]");
                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_Alarm> lsGetModelList = dbConnection.Query<TB_Alarm>(sqlStr.ToString()).AsList();
                    return lsGetModelList;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_AlarmRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}