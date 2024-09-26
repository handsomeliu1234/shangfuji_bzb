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
    public class TB_FeedingParamRepository : BaseDAL<TB_FeedingParam>
    {
        public TB_FeedingParamRepository()
        {
        }

        public bool Exists(string deviceId, string typeCodeId, int binNo)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    List<TB_FeedingParam> lsExists = dbConnection.Query<TB_FeedingParam>(@"select * from TB_FeedingParam where DeviceID = @DeviceID and TypeCodeID = @TypeCodeID and BinNo = @BinNo", new
                    {
                        DeviceID = deviceId,
                        TypeCodeID = typeCodeId,
                        BinNo = binNo
                    }).ToList();

                    if (lsExists.Count > 0)
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
                NewuGlobal.LogCat("").Error(ex.ToString());
                return false;
            }
        }

        public bool Add(TB_FeedingParam model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"insert into TB_FeedingParam(
                                                            FeedingID,
                                                            DeviceID,
                                                            TypeCodeID,
                                                            BinNo,
                                                            Big_FreqKuai,
                                                            Big_FreqZhong,
                                                            Big_FreqMan,
                                                            Small_FreqKuai,
                                                            Small_FreqZhong,
                                                            Small_FreqMan,
                                                            Big_FeedKuai,
                                                            Big_FeedMan,
                                                            Small_FeedKuai,
                                                            Small_FeedMan,
                                                            Sys_FeedKuaiTi,
                                                            Sys_FeedZhongTi,
                                                            Sys_FeedManTi,
                                                            SaveUserID,
                                                            SaveTime,
                                                            Reserve1,
                                                            Reserve2,
                                                            Reserve3,
                                                            Reserve4,
                                                            Reserve5)
                                                        values (
                                                            NEWID(),
                                                            @DeviceID,
                                                            @TypeCodeID,
                                                            @BinNo,
                                                            @Big_FreqKuai,
                                                            @Big_FreqZhong,
                                                            @Big_FreqMan,
                                                            @Small_FreqKuai,
                                                            @Small_FreqZhong,
                                                            @Small_FreqMan,
                                                            @Big_FeedKuai,
                                                            @Big_FeedMan,
                                                            @Small_FeedKuai,
                                                            @Small_FeedMan,
                                                            @Sys_FeedKuaiTi,
                                                            @Sys_FeedZhongTi,
                                                            @Sys_FeedManTi,
                                                            @SaveUserID,
                                                            @SaveTime,
                                                            @Reserve1,
                                                            @Reserve2,
                                                            @Reserve3,
                                                            @Reserve4,
                                                            @Reserve5)", model);
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
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(TB_FeedingParam model)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute(@"update TB_FeedingParam set
                                                            DeviceID = @DeviceID,
                                                            TypeCodeID = @TypeCodeID,
                                                            BinNo = @BinNo,
                                                            Big_FreqKuai = @Big_FreqKuai,
                                                            Big_FreqZhong = @Big_FreqZhong,
                                                            Big_FreqMan = @Big_FreqMan,
                                                            Small_FreqKuai = @Small_FreqKuai,
                                                            Small_FreqZhong = @Small_FreqZhong,
                                                            Small_FreqMan = @Small_FreqMan,
                                                            Big_FeedKuai = @Big_FeedKuai,
                                                            Big_FeedMan = @Big_FeedMan,
                                                            Small_FeedKuai = @Small_FeedKuai,
                                                            Small_FeedMan = @Small_FeedMan,
                                                            Sys_FeedKuaiTi = @Sys_FeedKuaiTi,
                                                            Sys_FeedZhongTi = @Sys_FeedZhongTi,
                                                            Sys_FeedManTi = @Sys_FeedManTi,
                                                            SaveUserID = @SaveUserID,
                                                            SaveTime = @SaveTime,
                                                            Reserve1 = @Reserve1,
                                                            Reserve2 = @Reserve2,
                                                            Reserve3 = @Reserve3,
                                                            Reserve4 = @Reserve4,
                                                            Reserve5 = @Reserve5
                                                        where FeedingID = @FeedingID", model);
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
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public bool Delete(string feedingID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    int effectRow = dbConnection.Execute("delete from TB_FeedingParam where FeedingID = @FeedingID", new
                    {
                        FeedingID = feedingID
                    });
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
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public TB_FeedingParam GetModel(string feedingID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 FeedingID, DeviceID, TypeCodeID, BinNo, Big_FreqKuai, Big_FreqZhong, Big_FreqMan, Small_FreqKuai, Small_FreqZhong, Small_FreqMan, Big_FeedKuai, Big_FeedMan, Small_FeedKuai, Small_FeedMan, Sys_FeedKuaiTi, Sys_FeedZhongTi, Sys_FeedManTi, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_FeedingParam where FeedingID = @FeedingID");
                    TB_FeedingParam lsGetModel = dbConnection.QueryFirstOrDefault<TB_FeedingParam>(sqlStr, new
                    {
                        FeedingID = feedingID
                    });
                    return lsGetModel;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public TB_FeedingParam GetModel(int binNo, string typeCodeID, string deviceID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 FeedingID, DeviceID, TypeCodeID, BinNo, Big_FreqKuai, Big_FreqZhong, Big_FreqMan, Small_FreqKuai, Small_FreqZhong, Small_FreqMan, Big_FeedKuai, Big_FeedMan, Small_FeedKuai, Small_FeedMan, Sys_FeedKuaiTi, Sys_FeedZhongTi, Sys_FeedManTi, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from TB_FeedingParam where BinNo = @BinNo  and TypeCodeID = @TypeCodeID and DeviceID = @DeviceID");
                    TB_FeedingParam lsGetModel = dbConnection.QueryFirstOrDefault<TB_FeedingParam>(sqlStr, new
                    {
                        BinNo = binNo,
                        TypeCodeID = typeCodeID,
                        DeviceID = deviceID
                    });
                    return lsGetModel;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TB_FeedingParam> GetList(int top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");
                    if (top > 0)
                    {
                        sqlStr.Append(" top " + top.ToString());
                    }
                    sqlStr.Append(" FeedingID, DeviceID, TypeCodeID, BinNo, Big_FreqKuai, Big_FreqZhong, Big_FreqMan, Small_FreqKuai, Small_FreqZhong, Small_FreqMan, Big_FeedKuai, Big_FeedMan, Small_FeedKuai, Small_FeedMan, Sys_FeedKuaiTi, Sys_FeedZhongTi, Sys_FeedManTi, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM TB_FeedingParam ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    sqlStr.Append(" order by " + filedOrder);
                    List<TB_FeedingParam> lsGetList = dbConnection.Query<TB_FeedingParam>(sqlStr.ToString()).AsList();
                    if (lsGetList.Count > 0)
                    {
                        return lsGetList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public List<TB_FeedingParam> GetModelList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"select FeedingID, DeviceID, TypeCodeID, BinNo, Big_FreqKuai, Big_FreqZhong, Big_FreqMan, Small_FreqKuai, Small_FreqZhong, Small_FreqMan, Big_FeedKuai, Big_FeedMan, Small_FeedKuai, Small_FeedMan, Sys_FeedKuaiTi, Sys_FeedZhongTi, Sys_FeedManTi, SaveUserID, SaveTime, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5  FROM TB_FeedingParam ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    List<TB_FeedingParam> lsGetModelList = dbConnection.Query<TB_FeedingParam>(strSql.ToString()).ToList();
                    if (lsGetModelList.Count > 0)
                    {
                        return lsGetModelList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("TB_FeedingParamRepository").Error(ex.ToString());
                throw ex;
            }
        }
    }
}