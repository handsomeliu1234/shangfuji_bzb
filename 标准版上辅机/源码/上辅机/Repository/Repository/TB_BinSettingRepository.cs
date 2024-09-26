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
    public class TB_BinSettingRepository : BaseDAL<TB_BinSeting>
    {
        public TB_BinSettingRepository()
        {
        }

        public bool Add(TB_BinSeting model, out string message)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into TB_BinSeting(
                                                            BinID,
                                                            BinNo,
                                                            DeviceID,
                                                            MaterialID,
                                                            TypeCodeID,
                                                            PreSetKuai,
                                                            PreSetZhong,
                                                            PreSetTiQian,
                                                            PreSetWuUp,
                                                            PreSetWuDown,
                                                            FrequenceUp,
                                                            FrequenceMid,
                                                            FrequenceDown,
                                                            SaveUserID,
                                                            SaveTime,
                                                            Reserve1,
                                                            Reserve2,
                                                            Reserve3,
                                                            Reserve4,
                                                            Reserve5)
                                                        values (
                                                            NEWID(),
                                                            @BinNo,
                                                            @DeviceID,
                                                            @MaterialID,
                                                            @TypeCodeID,
                                                            @PreSetKuai,
                                                            @PreSetZhong,
                                                            @PreSetTiQian,
                                                            @PreSetWuUp,
                                                            @PreSetWuDown,
                                                            @FrequenceUp,
                                                            @FrequenceMid,
                                                            @FrequenceDown,
                                                            @SaveUserID,
                                                            @SaveTime,
                                                            @Reserve1,
                                                            @Reserve2,
                                                            @Reserve3,
                                                            @Reserve4,
                                                            @Reserve5)", model);
                    if (effectRow != 0)
                    {
                        message = NewuGlobal.GetRes("000171");
                        return true;
                    }
                    else
                    {
                        message = NewuGlobal.GetRes("000172");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                message = ex.Message;
                return false;
            }
        }

        public new bool Update(TB_BinSeting model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"update TB_BinSeting set
                                                            BinNo = @BinNo,
                                                            DeviceID = @DeviceID,
                                                            MaterialID = @MaterialID,
                                                            TypeCodeID = @TypeCodeID,
                                                            PreSetKuai = @PreSetKuai,
                                                            PreSetZhong = @PreSetZhong,
                                                            PreSetTiQian = @PreSetTiQian,
                                                            PreSetWuUp = @PreSetWuUp,
                                                            PreSetWuDown = @PreSetWuDown,
                                                            FrequenceUp = @FrequenceUp,
                                                            FrequenceMid = @FrequenceMid,
                                                            FrequenceDown = @FrequenceDown,
                                                            SaveUserID = @SaveUserID,
                                                            SaveTime = @SaveTime,
                                                            Reserve1 = @Reserve1,
                                                            Reserve2 = @Reserve2,
                                                            Reserve3 = @Reserve3,
                                                            Reserve4 = @Reserve4,
                                                            Reserve5 = @Reserve5
                                                       where BinID = @BinID", model);
                    if (effectRow != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string binID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute("delete from TB_BinSeting where BinID = @BinID", new
                    {
                        BinID = binID
                    });
                    if (effectRow != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return false;
            }
        }

        public TB_BinSeting GetModel(string BinID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 BinID, BinNo, DeviceID, MaterialID, TypeCodeID, PreSetKuai, PreSetZhong, PreSetTiQian, PreSetWuUp, PreSetWuDown, FrequenceUp, FrequenceMid, FrequenceDown, SaveUserID, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_BinSeting  where BinID = @BinID");
                    List<TB_BinSeting> tBBinSeting = dbConnection.Query<TB_BinSeting>(sqlStr, new
                    {
                        BinID = BinID
                    }).ToList();
                    if (tBBinSeting.Count > 0)
                        return tBBinSeting[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        public TB_BinSeting GetModel(int binID, string deviceID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 BinID, BinNo, DeviceID, MaterialID, TypeCodeID, PreSetKuai, PreSetZhong, PreSetTiQian, PreSetWuUp, PreSetWuDown, FrequenceUp, FrequenceMid, FrequenceDown, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_BinSeting where BinID = @BinID and DeviceID = @DeviceID");
                    List<TB_BinSeting> lsGetModel = dbConnection.Query<TB_BinSeting>(sqlStr, new
                    {
                        BinID = binID,
                        DeviceID = deviceID
                    }).ToList();
                    if (lsGetModel.Count > 0)
                        return lsGetModel[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        public TB_BinSeting GetModel(int binNo, string deviceID, string typeCodeID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 BinID, BinNo, DeviceID, MaterialID, TypeCodeID, PreSetKuai, PreSetZhong, PreSetTiQian, PreSetWuUp, PreSetWuDown, FrequenceUp, FrequenceMid, FrequenceDown, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_BinSeting where BinNo = @BinNo and DeviceID = @DeviceID and TypeCodeID = @TypeCodeID");
                    List<TB_BinSeting> lsGetModel = dbConnection.Query<TB_BinSeting>(sqlStr, new
                    {
                        BinNo = binNo,
                        DeviceID = deviceID,
                        TypeCodeID = typeCodeID
                    }).ToList();
                    if (lsGetModel.Count > 0)
                        return lsGetModel[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        public TB_BinSeting GetModel(int binNo, string materialID, string deviceID, string typeCodeID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 BinID, BinNo, DeviceID, MaterialID, TypeCodeID, PreSetKuai, PreSetZhong, PreSetTiQian, PreSetWuUp, PreSetWuDown, FrequenceUp, FrequenceMid, FrequenceDown, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_BinSeting where (BinNo = @BinNo or MaterialID = @MaterialID) and DeviceID = @DeviceID and TypeCodeID = @TypeCodeID");
                    List<TB_BinSeting> lsGetModel = dbConnection.Query<TB_BinSeting>(sqlStr, new
                    {
                        BinNo = binNo,
                        MaterialID = materialID,
                        DeviceID = deviceID,
                        TypeCodeID = typeCodeID
                    }).ToList();
                    if (lsGetModel.Count > 0)
                        return lsGetModel[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_BinSeting> GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");
                    if (Top > 0)
                    {
                        sqlStr.Append(" top " + Top.ToString());
                    }
                    sqlStr.Append(" BinID, BinNo, DeviceID , MaterialID, TypeCodeID, PreSetKuai, PreSetZhong, PreSetTiQian, PreSetWuUp, PreSetWuDown, FrequenceUp, FrequenceMid, FrequenceDown, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_BinSeting");

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    sqlStr.Append(" order by " + filedOrder);
                    List<TB_BinSeting> lsgetList = dbConnection.Query<TB_BinSeting>(sqlStr.ToString()).ToList();
                    if (lsgetList.Count > 0)
                        return lsgetList;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_BinSeting> GetModelList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select BinID, BinNo, DeviceID, MaterialID, TypeCodeID, PreSetKuai, PreSetZhong, PreSetTiQian, PreSetWuUp, PreSetWuDown, FrequenceUp, FrequenceMid, FrequenceDown, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_BinSeting");
                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_BinSeting> lsGetModelList = dbConnection.Query<TB_BinSeting>(sqlStr.ToString()).ToList();
                    if (lsGetModelList.Count > 0)
                        return lsGetModelList;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_BinSeting> GetListJoinMaterialCode(string deviceId, string typeCodeId)
        {
            try
            {
                string where = " 1=1 ";

                if (deviceId != "")
                {
                    where += "and a.DeviceID='" + deviceId + "' ";
                }

                if (typeCodeId != "")
                {
                    where += "and a.TypeCodeID='" + typeCodeId + "'";
                }
                where += " order by a.BinNo";

                return GetListJoinMaterial(where);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_BinSeting> GetListJoinMaterialCodeIn(string deviceId, string typeCodeId, string typeCodeId2)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string where = " 1=1 ";

                    if (!string.IsNullOrEmpty(deviceId))
                    {
                        where += "and a.DeviceID='" + deviceId + "' ";
                    }

                    if (!string.IsNullOrEmpty(typeCodeId))
                    {
                        where += "and a.TypeCodeID in ('" + typeCodeId + "','" + typeCodeId2 + "')";
                    }
                    where += " order by a.BinNo";

                    return GetListJoinMaterial(where);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }

        private List<TB_BinSeting> GetListJoinMaterial(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select a.BinNo, a.DeviceID, a.TypeCodeID, a.MaterialID, b.MaterialCode, a.PreSetKuai, a.PreSetTiQian, a.PreSetWuUp, a.PreSetWuDown, a.FrequenceUp, a.FrequenceMid, a.FrequenceDown, a.SaveUserID, a.SaveTime, a.Reserve1, a.Reserve2, a.Reserve3, a.Reserve4, a.Reserve5 FROM TB_BinSeting a left join FormulaMaterial b on a.MaterialID=b.MaterialID");
                    if (strWhere.Trim() != "")
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<TB_BinSeting> tB_BinSetings = connection.Query<TB_BinSeting>(sqlStr.ToString()).AsList();
                    return tB_BinSetings;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_BinSettingRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}