using Dapper;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FormulaTechParamRepository : BaseDAL<FormulaTechParam>
    {
        private string techParamDevice = "";
        private string devicePartId = "";
        private List<SYS_TechParam> sysTechParams;
        private SYS_TechParamFRepository sysTechParamRepository = new SYS_TechParamFRepository();

        private string TechParamDevice
        {
            get
            {
                return techParamDevice;
            }
            set
            {
                if (value != techParamDevice || sysTechParams == null)
                {
                    techParamDevice = value;
                    string whereStr = "DeviceID='" + techParamDevice + "' and DevicePartID='" + devicePartId + "' and Enable=1 ";
                    sysTechParams = sysTechParamRepository.GetList(0, whereStr, "TechParamOrder");
                }
            }
        }

        public bool Add(FormulaTechParam model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaTechParam(
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
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool AddList(List<FormulaTechParam> list)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into FormulaTechParam(
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
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool DeleteAll(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from FormulaTechParam where MaterialID = @MaterialID");
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
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Exist(string formulaTechParamID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select count(1) from FormulaTechParam  where FormulaTechParamID = @FormulaTechParamID");
                    List<FormulaTechParam> formulaTechParams = connection.Query<FormulaTechParam>(sqlStr, new
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
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(FormulaTechParam model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update FormulaTechParam set
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
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool GetTechParamCount(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select a.* FROM FormulaTechParam a right join SYS_TechParam b on a.TechParamID = b.TechParamID and b.Enable=1 where a.MaterialID =  @MaterialID");
                    List<FormulaTechParam> formulaTechParams = connection.Query<FormulaTechParam>(sqlStr, new
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
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return false;
            }
        }

        public List<SYS_TechParam> GetListJoinSysTech(string materialID, string deviceID, string devicePartId)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    this.devicePartId = devicePartId;
                    TechParamDevice = deviceID;
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    strSql.Append(" a.* ");
                    strSql.Append(" FROM FormulaTechParam a right join SYS_TechParam b on a.TechParamID = b.TechParamID and b.Enable=1");
                    strSql.Append(" where a.MaterialID = @MaterialID order by b.TechParamOrder ");
                    List<FormulaTechParam> formulaTechParams = connection.Query<FormulaTechParam>(strSql.ToString(), new
                    {
                        MaterialID = materialID,
                    }).AsList();
                    if (sysTechParams.Count == 1 || formulaTechParams.Count != 0)
                    {
                        foreach (var item in sysTechParams)
                        {
                            item.FormulaTechParamID = "";
                            item.TechParamVal = 0;
                            foreach (var item1 in formulaTechParams)
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
                    return sysTechParams;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_TechParam> GetListJoinSysTech1(string materialID, string deviceID, string devicePartId)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    this.devicePartId = devicePartId;
                    TechParamDevice = deviceID;
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select ");
                    strSql.Append(" a.* ");
                    strSql.Append(" FROM FormulaTechParam a right join SYS_TechParam b on a.TechParamID = b.TechParamID and b.Enable=1");
                    strSql.Append(" where a.MaterialID = @MaterialID");
                    List<FormulaTechParam> formulaTechParams = connection.Query<FormulaTechParam>(strSql.ToString(), new
                    {
                        MaterialID = materialID
                    }).AsList();
                    if (sysTechParams.Count == 1 || formulaTechParams.Count != 0)
                    {
                        foreach (var item in sysTechParams)
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
                    return sysTechParams;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<TechParamDetail> GetTechParamDetail(string materialID)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder buid = new StringBuilder();
                    buid.Append(" SELECT b.DeviceID,b.DevicePartID,d.DeviceCode,c.DevicePartCode,b.TechParamNameCN,b.TechParamNameEN,");
                    buid.Append(" a.TechParamVal,b.TechParamOrder ");
                    buid.Append(" FROM FormulaTechParam a ");
                    buid.Append(" left join SYS_TechParam b on a.TechParamID=b.TechParamID and b.Enable=1");
                    buid.Append(" left join SYS_DevicePart c on b.DevicePartID=c.DevicePartID ");
                    buid.Append(" left join SYS_Device d on c.DeviceID=d.DeviceID ");
                    buid.Append(" where a.MaterialID='" + materialID + "' ");
                    buid.Append(" order by b.TechParamOrder ");

                    return connection.Query<TechParamDetail>(buid.ToString()).AsList();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FormulaTechParamRepository").Error(ex.ToString());
                return null;
            }
        }
    }

    public class TechParamDetail
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