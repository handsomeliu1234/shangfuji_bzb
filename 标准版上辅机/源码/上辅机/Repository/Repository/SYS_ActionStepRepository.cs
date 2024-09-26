using Dapper;
using Newu;
using NewuCommon;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repository.Repository
{
    public class SYS_ActionStepRepository : BaseDAL<SYS_ActionStep>
    {
        public bool Add(SYS_ActionStep model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_ActionStep(
                                                        StepCode,
                                                        StepValue,
                                                        StepNameCN,
                                                        StepNameEN,
                                                        DeviceID,
                                                        DevicePartID,
                                                        SaveTime,
                                                        Enable,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                   Values(
                                                        NEWID(),
                                                        @StepValue,
                                                        @StepNameCN,
                                                        @StepNameEN,
                                                        @DeviceID,
                                                        @DevicePartID,
                                                        @SaveTime,
                                                        @Enable,
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
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return false;
            }
        }

        public new bool Update(SYS_ActionStep model)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"update SYS_ActionStep set
                                                       StepValue = @StepValue,
                                                       StepNameCN = @StepNameCN,
                                                       StepNameEN = @StepNameEN,
                                                       DeviceID = @DeviceID,
                                                       DevicePartID = @DevicePartID,
                                                       SaveTime = @SaveTime,
                                                       Enable = @Enable,
                                                       Reserve1 = @Reserve1,
                                                       Reserve2 = @Reserve2,
                                                       Reserve3 = @Reserve3,
                                                       Reserve4 = @Reserve4,
                                                       Reserve5 = @Reserve5
                                                   where StepCode = @StepCode");

                    int effectRow = connection.Execute(sqlStr, model);
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool Delete(string stepCode)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"delete from SYS_ActionStep where StepCode = @StepCode");
                    int effectRow = connection.Execute(sqlStr, new
                    {
                        StepCode = stepCode
                    });
                    if (effectRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_ActionStep GetModel(string stepCode)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 StepCode, StepValue, StepNameCN, StepNameEN, DeviceID, DevicePartID, SaveTime, Enable,Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_ActionStep where StepCode = @StepCode");
                    SYS_ActionStep sYS_ActionStep = connection.QueryFirstOrDefault<SYS_ActionStep>(sqlStr, new
                    {
                        StepCode = stepCode
                    });
                    return sYS_ActionStep;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_ActionStep> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select StepCode, StepValue, StepNameCN, StepNameEN, DeviceID, DevicePartID, SaveTime, Enable,Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_ActionStep ");
                    if (!string.IsNullOrEmpty(strWhere))
                        sqlStr += " where " + strWhere;
                    List<SYS_ActionStep> sYSActionStep = connection.Query<SYS_ActionStep>(sqlStr).AsList();
                    return sYSActionStep;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 工艺步骤顺序
        /// </summary>
        /// <returns></returns>
        public DataTable GetStepOrderTable()
        {
            try
            {
                CreateTable cTable = new CreateTable();
                string[] fields = new string[] { "name", "value" };
                Type[] types = new Type[] { typeof(string), typeof(int) };

                object[,] values = new object[,]
                {
                    {"No.1",1},
                    {"No.2",2},
                    {"No.3",3},
                    {"No.4",4},
                    {"No.5",5},
                    {"No.6",6},
                    {"No.7",7},
                    {"No.8",8},
                    {"No.9",9},
                    {"No.10",10},
                    {"No.11",11},
                    {"No.12",12},
                    {"No.13",13},
                    {"No.14",14},
                    {"No.15",15},
                    {"No.16",16},
                    {"No.17",17},
                    {"No.18",18},
                    {"No.19",19},
                    {"No.20",20},
                    {"No.21",21},
                    {"No.22",22},
                    {"No.23",23},
                    {"No.24",24},
                    {"No.25",25},
                    {"No.26",26},
                    {"No.27",27},
                    {"No.28",28},
                    {"No.29",29},
                    {"No.30",30},
                    {"",0}
                };
                DataTable dt = cTable.GetTable(fields, types, values);
                return dt;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据设备ID和部件ID，获得数据列表
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<SYS_ActionStep> GetModelListByDevice(string deviceID, string[] deviceParts)
        {
            try
            {
                string strWhere = "1=1 ";
                string parts = "";
                if (!string.IsNullOrEmpty(deviceID))
                {
                    strWhere += "and (DeviceID='" + deviceID + "' or DeviceID='') ";
                }

                for (int i = 0; i < deviceParts.Length; i++)
                {
                    if (parts == "")
                    {
                        parts = "'" + deviceParts[i] + "',";
                    }
                    else
                    {
                        parts += "'" + deviceParts[i] + "',";
                    }
                }

                if (parts != "")
                {
                    parts = parts.Substring(0, parts.Length - 1);
                    strWhere += "and DevicePartID in(" + parts + ")";
                }
                List<SYS_ActionStep> sYS_ActionSteps = GetList(0, strWhere, "StepValue ASC");
                return sYS_ActionSteps;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return null;
            }
        }

        public List<SYS_ActionStep> GetList(int top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");
                    if (top > 0)
                    {
                        sqlStr.Append(" top " + top.ToString());
                    }
                    sqlStr.Append("StepCode, StepValue, StepNameCN, StepNameEN, DeviceID, DevicePartID, SaveTime,Enable,Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_ActionStep ");

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    sqlStr.Append(" order by " + filedOrder);
                    List<SYS_ActionStep> sYSActionStep = connection.Query<SYS_ActionStep>(sqlStr.ToString()).AsList();
                    return sYSActionStep;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据密炼工艺步骤中的StepCode(即十进制之和)，找到CheckListItem中的匹配项目
        /// </summary>
        public void FindCheckListItemByMixStepCode(int stepCode, CheckedListBox[] listBoxs, string StepDesc)
        {
            try
            {
                // 莫名其妙的问题 卧槽 19.3.25
                if (stepCode < 0)
                    return;
                if (stepCode == 0)
                {
                    listBoxs[1].SetItemChecked(0, true);
                    return;
                }

                string bitStr = Convert.ToString(stepCode, 2);

                bitStr = new string(bitStr.ToCharArray().Reverse().ToArray());
                int i = 1;

                if (StepDesc.Contains("胶料") || StepDesc.Contains("药") || StepDesc.Contains("炭黑") || StepDesc.Contains("塑") || StepDesc.Contains("油") || StepDesc.Contains("硅") || StepDesc.Contains("粉"))
                {
                    i = 0;
                }
                if (StepDesc.Contains("Rubber") || StepDesc.Contains("Drug") || StepDesc.Contains("Carbon") || StepDesc.Contains("Plast") || StepDesc.Contains("Oil") || StepDesc.Contains("Sil") || StepDesc.Contains("Zno"))
                {
                    i = 0;
                }
                for (int j = 0; j < listBoxs[i].Items.Count; j++)
                {
                    Item<int, string> item = (Item<int, string>)listBoxs[i].Items[j];

                    for (int k = 0; k < bitStr.Length; k++)
                    {
                        if (bitStr.Length >= item.Value && item.Value >= 1)
                        {
                            if (bitStr.Substring(item.Value - 1, 1) == "1")
                            {
                                listBoxs[i].SetItemChecked(j, true);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 根据密炼工艺步骤中的StepCode(即十进制之和)，找到CheckListItem中的匹配项目
        /// </summary>
        public void FindCheckListItemByMixStepCode(int stepCode, CheckedListBox[] listBoxs)
        {
            try
            {
                // 莫名其妙的问题 卧槽 19.3.25
                if (stepCode <= 0)
                    return;
                string bitStr = Convert.ToString(stepCode, 2);
                bitStr = new string(bitStr.ToCharArray().Reverse().ToArray());

                for (int i = 0; i < listBoxs.Length; i++)
                {
                    for (int j = 0; j < listBoxs[i].Items.Count; j++)
                    {
                        Item<int, string> item = (Item<int, string>)listBoxs[i].Items[j];

                        for (int k = 0; k < bitStr.Length; k++)
                        {
                            if (bitStr.Length >= item.Value && item.Value >= 1)
                            {
                                if (bitStr.Substring(item.Value - 1, 1) == "1")
                                {
                                    listBoxs[i].SetItemChecked(j, true);
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 根据设备编号和设备部件编号，获取工艺步骤信息并填充到CheckList中
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="deviceParts"></param>
        /// <param name="checkList"></param>
        public void FunStepToCheckList(string deviceID, string stepType, CheckedListBox checkList)
        {
            List<SYS_ActionStep> dropList = new List<SYS_ActionStep>();
            if (stepType == "MixStep")
            {
                dropList = NewuGlobal.ActionStepList.Where(t => t.DevicePartID == NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp)) && t.DeviceID == deviceID).ToList();
            }
            else if (stepType == "DropStep")
            {
                dropList = NewuGlobal.ActionStepList.Where(t => t.DevicePartID != NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp)) && t.DeviceID == deviceID).ToList();
            }
            //List<SYS_ActionStep> Droplist = this.GetModelListByDevice(deviceID, deviceParts);
            foreach (SYS_ActionStep item in dropList)
            {
                if (NewuGlobal.SupportLanguage != "1" || NewuGlobal.SupportLanguage == null)
                {
                    Item<int, string> checkItem = new Item<int, string>(item.StepValue, item.StepNameEN);
                    checkList.Items.Add(checkItem);
                }
                else
                {
                    Item<int, string> checkItem = new Item<int, string>(item.StepValue, item.StepNameCN);
                    checkList.Items.Add(checkItem);
                }
            }
        }

        public List<SYS_ActionStep> GetListAddBitColumn(string strWhere)
        {
            try
            {
                List<SYS_ActionStep> sYS_ActionSteps = GetList(0, strWhere, "DevicePartId,StepValue,DeviceId");
                foreach (var item in sYS_ActionSteps)
                {
                    int cifang = FunClass.VVal(item.StepValue) - 1;
                    item.StepBit = (int)Math.Pow(2, cifang);
                }
                return sYS_ActionSteps;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_ActionStepRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}