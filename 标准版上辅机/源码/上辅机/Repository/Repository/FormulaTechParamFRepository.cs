using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.Repository
{
    public class FormulaTechParamFRepository : BaseDAL<FormulaTechParamF>
    {
        private string techParamFDevice = "";
        private string devicePartFId = "";
        private List<SYS_TechParam> sysTechParamFs;
        private SYS_TechParamFRepository sysTechParamFRepository = new SYS_TechParamFRepository();

        private string TechParamFDevice
        {
            get
            {
                return techParamFDevice;
            }
            set
            {
                if (value != techParamFDevice || sysTechParamFs == null)
                {
                    techParamFDevice = value;
                    string whereStr = "DeviceID='" + techParamFDevice + "' and DevicePartID='" + devicePartFId + "'";
                    sysTechParamFs = sysTechParamFRepository.GetList(0, whereStr, "TechParamOrder");
                }
            }
        }

        public bool Add(FormulaTechParamF model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaTechParamF(
                                                    FormulaTechParamID,
                                                    MaterialID,
                                                    TechParamID,
                                                    TechParamVal,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                            values (
                                                    NEWID(),
                                                    @MaterialID,
                                                    @TechParamID,
                                                    @TechParamVal,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5)");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool AddList(List<FormulaTechParamF> list)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaTechParamF(
                                                    FormulaTechParamID,
                                                    MaterialID,
                                                    TechParamID,
                                                    TechParamVal,
                                                    Reserve1,
                                                    Reserve2,
                                                    Reserve3,
                                                    Reserve4,
                                                    Reserve5)
                                            values (
                                                    NEWID(),
                                                    @MaterialID,
                                                    @TechParamID,
                                                    @TechParamVal,
                                                    @Reserve1,
                                                    @Reserve2,
                                                    @Reserve3,
                                                    @Reserve4,
                                                    @Reserve5)");
                    int effectRow = connection.Execute(sqlStr, list);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool DeleteAll(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from FormulaTechParamF where MaterialID = @MaterialID");
                    //配方中没有称量信息时导致返回0,if判断加入=0条件
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        MaterialID = materialID
                    });
                    if (effectRow >= 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Exist(string formulaTechParamID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select count(1) from FormulaTechParamF  where FormulaTechParamID = @FormulaTechParamID");
                    List<FormulaTechParamF> formulaTechParams = connection.Query<FormulaTechParamF>(sqlStr, new
                    {
                        FormulaTechParamID = formulaTechParamID
                    }).AsList();
                    if (formulaTechParams.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(FormulaTechParamF model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update FormulaTechParamF set
                                                    MaterialID=@MaterialID,
                                                    TechParamID=@TechParamID,
                                                    TechParamVal=@TechParamVal,
                                                    Reserve1=@Reserve1,
                                                    Reserve2=@Reserve2,
                                                    Reserve3=@Reserve3,
                                                    Reserve4=@Reserve4,
                                                    Reserve5=@Reserve5
                                               where
                                                    FormulaTechParamID = @FormulaTechParamID");
                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool GetTechParamCount(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select a.* FROM FormulaTechParamF a right join SYS_TechParam b on a.TechParamID = b.TechParamID where a.MaterialID =  @MaterialID");
                    List<FormulaTechParamF> formulaTechParams = connection.Query<FormulaTechParamF>(sqlStr, new
                    {
                        MaterialID = materialID
                    }).AsList();
                    if (formulaTechParams.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<SYS_TechParam> GetListJoinSysTechF(string materialID, string deviceID, string devicePartId)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    this.devicePartFId = devicePartId;
                    TechParamFDevice = deviceID;
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    strSql.Append(" a.* ");
                    strSql.Append(" FROM FormulaTechParamF a right join SYS_TechParam b on a.TechParamID = b.TechParamID and b.Enable=1");
                    strSql.Append(" where a.MaterialID = @MaterialID and b.DevicePartID = @DevicePartID");

                    List<FormulaTechParam> formulaTechParamsF = connection.Query<FormulaTechParam>(strSql.ToString(), new
                    {
                        MaterialID = materialID,
                        DevicePartID = devicePartId
                    }).AsList();

                    if (formulaTechParamsF.Count != 0)
                    {
                        foreach (var item in sysTechParamFs)
                        {
                            item.FormulaTechParamID = "";
                            item.TechParamVal = 0;
                            foreach (var item1 in formulaTechParamsF)
                            {
                                if (item.TechParamID.Equals(item1.TechParamID))
                                {
                                    item.TechParamVal = item1.TechParamVal;
                                    item.FormulaTechParamID = item1.FormulaTechParamID;
                                    break;
                                }
                            }
                        }
                    }
                    return sysTechParamFs;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_TechParam> GetListJoinSysTech1(string materialID, string deviceID, string devicePartId)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    this.devicePartFId = devicePartId;
                    TechParamFDevice = deviceID;
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    strSql.Append(" a.* ");
                    strSql.Append(" FROM FormulaTechParamF a right join SYS_TechParam b on a.TechParamID = b.TechParamID");
                    strSql.Append(" where a.MaterialID = @MaterialID");
                    List<FormulaTechParamF> formulaTechParams = connection.Query<FormulaTechParamF>(strSql.ToString(), new
                    {
                        MaterialID = materialID
                    }).AsList();
                    if (sysTechParamFs.Count == 1 || formulaTechParams.Count != 0)
                    {
                        foreach (var item in sysTechParamFs)
                        {
                            item.FormulaTechParamID = "";
                            item.TechParamVal = 0;
                            foreach (var item1 in formulaTechParams)
                            {
                                if (item.TechParamID.Equals(item1.TechParamID))
                                {
                                    item.TechParamVal = 0;
                                    item.FormulaTechParamID = item1.FormulaTechParamID;
                                    break;
                                }
                            }
                        }
                    }
                    return sysTechParamFs;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TechParamDetailF> GetTechParamDetail(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder buid = new StringBuilder();
                    buid.Append(" SELECT b.DeviceID,b.DevicePartID,d.DeviceCode,c.DevicePartCode,b.TechParamNameCN,b.TechParamNameEN,");
                    buid.Append(" a.TechParamVal,b.TechParamOrder ");
                    buid.Append(" FROM FormulaTechParamF a ");
                    buid.Append(" left join SYS_TechParam b on a.TechParamID=b.TechParamID ");
                    buid.Append(" left join SYS_DevicePart c on b.DevicePartID=c.DevicePartID ");
                    buid.Append(" left join SYS_Device d on c.DeviceID=d.DeviceID ");
                    buid.Append(" where a.MaterialID='" + materialID + "' ");
                    buid.Append(" order by b.TechParamOrder ");

                    return connection.Query<TechParamDetailF>(buid.ToString()).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamFRepository").Error(ex.ToString());
                return null;
            }
        }
    }

    public class TechParamDetailF
    {
        public string DeviceID
        {
            get; set;
        }

        public string DevicePartID
        {
            get; set;
        }

        public string DeviceCode
        {
            get; set;
        }

        public string DevicePartCode
        {
            get; set;
        }

        public string TechParamNameCN
        {
            get; set;
        }

        public string TechParamNameEN
        {
            get; set;
        }

        public string TechParamVal
        {
            get; set;
        }

        public string TechParamOrder
        {
            get; set;
        }
    }
}