using Newu;
using NewuCommon;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.GlobalConfig
{
    public class TransFromulaUtil
    {
        private DbHelperSQL SqlHelp = new DbHelperSQL();

        /// <summary>
        /// 向PLC传送 炭黑
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <returns></returns>
        public bool PlcCarbonData(string _MaterialId, out string errStr)
        {
            try
            {
                string strSql = "select a.*,b.* from FormulaWeigh a,TB_BinSeting b  where  a.WeighMaterialID = b.MaterialID and a.MaterialID='" + _MaterialId + "' and a.DevicePartCode='" + NewuGlobal.GetDevicePartCode(DevicePartType.Carbon) + "' order by DropOrder ASC,WeighOrder ASC ";

                DataSet ds = SqlHelp.Query(strSql);

                int digit = NewuGlobal.SoftConfig.CarbonDigit;
                string memStr = ReadyPlcRawData(ds, 10, out errStr, false, digit, false);
                if (errStr != "")
                {
                    return false;
                }

                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Carbon, memStr);

                if (flag)
                {
                    return true;
                }
                else
                {
                    errStr = "向PLC发送 炭黑数据 超时";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 向PLC传送 硅烷
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <returns></returns>
        public bool PlcSilData(string _MaterialId, out string errStr)
        {
            string strSql = "select * from View_FormulaWeigh where MaterialID='" + _MaterialId + "' and DevicePartCode='" + NewuGlobal.GetDevicePartCode(DevicePartType.Silane) + "' order by DropOrder ASC,WeighOrder ASC ";

            DataSet ds = SqlHelp.Query(strSql);

            int digit = NewuGlobal.SoftConfig.SilaneDigit;
            string memStr = ReadyPlcRawData(ds, 10, out errStr, false, digit, false);
            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Silane, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送 硅烷数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 向PLC传送 小药
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        /// 小药 罐号默认为1 不与 TB_BinSeting 做匹配查询 todo: 2018.4.16
        public bool PlcDrugData(string _MaterialId, out string errStr)
        {
            string strSql = "select * from FormulaWeigh  where MaterialID='"
                + _MaterialId + "' and DevicePartCode='"
                + NewuGlobal.GetDevicePartCode(DevicePartType.DrugMixer)
                + "' order by DropOrder ASC,WeighOrder ASC ";

            DataSet ds = SqlHelp.Query(strSql);

            int digit = NewuGlobal.SoftConfig.DrugDigit;
            string memStr = ReadyPlcRawData(ds, 10, out errStr, true, digit, false);
            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.DrugMixer, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送 小药数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 向PLC传送 油料
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcOilDataData1(string _MaterialId, out string errStr)
        {
            string strSql = "select a.*,b.* from FormulaWeigh a,TB_BinSeting b  where  a.WeighMaterialID=b.MaterialID and a.MaterialID='"
                + _MaterialId + "' and a.DevicePartCode='"
                + NewuGlobal.GetDevicePartCode(DevicePartType.Oil)
                + "' order by DropOrder ASC,WeighOrder ASC ";

            DataSet ds = SqlHelp.Query(strSql);
            int digit = NewuGlobal.SoftConfig.OilDigit;
            string memStr = ReadyPlcRawData(ds, 10, out errStr, false, digit, false);
            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Oil, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送 油料数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 向PLC传送 胶料
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcRubberData(string _MaterialId, out string errStr)
        {
            string strSql = "select * from View_FormulaWeigh where MaterialID='"
                + _MaterialId + "' and DevicePartCode='"
                + NewuGlobal.GetDevicePartCode(DevicePartType.Rubber)
                + "' order by DropOrder ASC,WeighOrder ASC ";

            DataSet ds = SqlHelp.Query(strSql);

            int digit = NewuGlobal.SoftConfig.RubberDigit;
            string memStr = ReadyPlcRawData(ds, 10, out errStr, true, digit, false);
            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Rubber, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送 胶料数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 向PLC传送 粉料
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcZnoData(string _MaterialId, out string errStr)
        {
            string strSql = "select a.*,b.* from FormulaWeigh a,TB_BinSeting b  where  a.WeighMaterialID=b.MaterialID and a.MaterialID='"
                + _MaterialId + "' and a.DevicePartCode='"
                + NewuGlobal.GetDevicePartCode(DevicePartType.Zno)
                + "' order by DropOrder ASC,WeighOrder ASC ";

            DataSet ds = SqlHelp.Query(strSql);

            int digit = NewuGlobal.SoftConfig.ZnoDigit;
            string memStr = ReadyPlcRawData(ds, 10, out errStr, false, digit, false);
            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Zno, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送 粉料数据 超时";
                return false;
            }
        }

        public bool PlcPlaData(string _MaterialId, out string errStr)
        {
            string strSql = "select * from View_FormulaWeigh  where MaterialID='"
                            + _MaterialId + "' and DevicePartCode='"
                            + NewuGlobal.GetDevicePartCode(DevicePartType.Plasticizer)
                            + "' order by DropOrder ASC,WeighOrder ASC ";
            DataSet ds = SqlHelp.Query(strSql);

            int digit = NewuGlobal.SoftConfig.PlaDigit;
            string memStr = ReadyPlcRawData(ds, 10, out errStr, false, digit, false);
            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Plasticizer, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送塑解剂数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 准备PLC 称量数据
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="PlcDataRowCount">PLC数据行数</param>
        /// <param name="errMsg">错误返回</param>
        /// <param name="digit">称量精确位数</param>
        /// &gt;
        /// <returns>准备好的PLC数据</returns>
        public string ReadyPlcRawData(DataSet ds, int PlcDataRowCount, out string errMsg, bool is_Drug_Ruby, int digit, bool UPERROR)
        {
            errMsg = "";

            string memStr = "";
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int DropNo = FunClass.VVal(row["DropOrder"]);
                    int WeightNo = FunClass.VVal(row["WeighOrder"]);
                    decimal SetWeight = FunClass.VDecimal(row["WeighSetVal"]);

                    decimal BinKuaiVal = FunClass.VDecimal(row["Reserve2"]) * (int)Math.Pow(10, digit); //快称值

                    decimal BinTiVal = (SetWeight - FunClass.VDecimal(row["Reserve1"])) * (int)Math.Pow(10, digit);  //预定重-提前量

                    decimal BinErrUp = (SetWeight + FunClass.VDecimal(row["AllowError"])) * (int)Math.Pow(10, digit);  //预定重 + 误差

                    decimal BinErrDown = (SetWeight - FunClass.VDecimal(row["AllowError"])) * (int)Math.Pow(10, digit);  //预定重 - 误差：

                    if (UPERROR == true)
                        BinErrDown += 20;

                    SetWeight = FunClass.VDecimal(row["WeighSetVal"]) * (int)Math.Pow(10, digit); //预定重量

                    int BinNo;
                    if (is_Drug_Ruby)
                    {
                        /*是否使用供胶机 由Reserve5 --> Rubber
                         * 1：下层供胶机
                         * 2：上层供胶机
                         * 3：双层供胶机
                         * 0：不适用供胶机
                         * 李辉 20230705 修改
                         */
                        BinNo = FunClass.VVal(row["Rubber"]);
                    }
                    else
                    {
                        BinNo = FunClass.VVal(row["BinNo"]); //罐号
                    }

                    if (BinKuaiVal > 65535 || BinErrUp > 65535)
                        throw (new ArgumentException("称量物料超重！"));

                    //称量顺序	预定重量	  快称值	 预定重-提前量	预定重+误差	预定重-误差	斗号
                    //‘称量顺序’由两位数字构成，十位数字是称量次数【1-9】，个位是当前次数里的称量顺序【1-9】
                    memStr += (DropNo * 10 + WeightNo).ToString("X4");   //称量顺序

                    memStr += ((int)SetWeight).ToString("X4");  //预定重量

                    memStr += ((int)BinKuaiVal).ToString("X4");   // 快称值

                    memStr += ((int)BinTiVal).ToString("X4");   //预定重-提前量

                    memStr += ((int)BinErrUp).ToString("X4");   //预定重 + 误差

                    memStr += ((int)BinErrDown).ToString("X4");   //预定重 - 误差

                    memStr += ((int)BinNo).ToString("X4");   //罐号

                    if (SetWeight <= 0 || BinTiVal < 0 || BinKuaiVal < 0 || BinErrUp < 0 || BinErrDown < 0 || DropNo < 0 || WeightNo < 0 || BinNo < 0)
                    {
                        errMsg = "称量数据中存在数据非法，配方参数值不可小于等于0！";
                        break;
                    }
                }
                int charLen = PlcDataRowCount * 28;
                memStr = memStr.PadRight(charLen, '0');
            }
            catch (Exception ex)
            {
                errMsg = ex.ToString();
            }

            return memStr;
        }

        /// <summary>
        /// 向PLC传送 密炼
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcMixData(string _MaterialId, out string errStr)
        {
            string strSql = "select a.*,b.ActionControlValue from FormulaMix a,SYS_ActionControl b " +
                            "where a.MaterialID='" + _MaterialId + "' and a.ActionControlCode=b.ActionControlCode " +
                            "order by StepOrder ";

            DataSet ds = SqlHelp.Query(strSql);
            int DataCount = ds.Tables[0].Rows.Count;
            string memStr = ReadyPlcMixData(ds, 30, out errStr);
            if (errStr != "")
            {
                return false;
            }

            string mems1 = memStr.Substring(0, 400);
            string mems2 = memStr.Substring(400, 400);
            string mems3 = memStr.Substring(800, 400);

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Mixer, mems1);
            bool flag1 = NewuGlobal.MemMgr.Sync((int)MixerWeight.Mixer + 400, mems2);
            bool flag2 = NewuGlobal.MemMgr.Sync((int)MixerWeight.Mixer + 800, mems3);

            if (!flag || !flag1 || !flag2)
            {
                errStr = "向PLC发送 密炼工艺数据 超时";
                return false;
            }

            if (DataCount > 0)
            {
                int DropDoorTime = FunClass.VVal(ds.Tables[0].Rows[DataCount - 1]["StepTime"]);
                if (DropDoorTime < 0)
                {
                    errStr = "密炼排胶时间为0秒，向PLC发送失败";
                    return false;
                }

                bool flag3 = NewuGlobal.MemMgr.Sync(AddressConst.DischargeRubberTime, DropDoorTime.ToString("X4"));

                if (!flag3)
                {
                    errStr = "向PLC发送 卸料时间 超时";
                    return false;
                }
            }
            else
            {
                errStr = "没有密炼工艺数据，向PLC发送 卸料时间 失败";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 向PLC传送 下密炼
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcMixFData(string _MaterialId, out string errStr)
        {
            string strSql = "select a.*,b.ActionControlValue from FormulaMixF a,SYS_ActionControl b " +
                            "where a.MaterialID='" + _MaterialId + "' and a.ActionControlCode=b.ActionControlCode " +
                            "order by StepOrder ";

            DataSet ds = SqlHelp.Query(strSql);
            int DataCount = ds.Tables[0].Rows.Count;
            string memStr = ReadyPlcMixData(ds, 30, out errStr);
            if (errStr != "")
            {
                return false;
            }

            string mems1 = memStr.Substring(0, 400);

            bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.MixerDown, mems1);

            if (!flag)
            {
                errStr = "向PLC发送 下密炼工艺数据 超时";
                return false;
            }

            if (DataCount > 0)
            {
                int DropDoorTime = FunClass.VVal(ds.Tables[0].Rows[DataCount - 1]["StepTime"]);
                if (DropDoorTime < 0)
                {
                    errStr = "下密炼排胶时间为0秒，向PLC发送失败";
                    return false;
                }

                bool flag1 = NewuGlobal.MemMgr.Sync(AddressConst.DischargeRubberDownTime, DropDoorTime.ToString("X4"));

                if (!flag1)
                {
                    errStr = "向PLC发送 卸料时间 超时";
                    return false;
                }
            }
            else
            {
                errStr = "没有下密炼工艺数据，向PLC发送 卸料时间 失败";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 准备PLC 密炼数据
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="PlcDataRowCount">PLC数据行数</param>
        /// <param name="errMsg">错误返回</param>
        /// <returns>准备好的PLC数据</returns>

        public string ReadyPlcMixData(DataSet ds, int PlcDataRowCount, out string errMsg)
        {
            string memStr = "";
            errMsg = "";
            List<string> list = new List<string>
            {
                "炭黑",
                "胶料",
                "油",
                "粉料",
                "小药",
                "硅烷",
                "Carbon",
                "Rubber",
                "Oil",
                "Zno",
                "Drug",
                "Si"
            };
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // 判定该工艺步骤为投料步骤 还是密炼步骤
                    bool isTou = false;
                    foreach (var a in list)
                    {
                        if (row["StepDesc"].ToString().Contains(a))
                        {
                            isTou = true;
                            break;
                        }
                    }

                    int StepCode1 = FunClass.VVal(row["StepCode"]);      //称量步骤编码  二进制其表示
                    int StepCode2 = FunClass.VVal(row["StepCode"]);      //投料步骤编码  二进制其表示
                    int StepTime = FunClass.VVal(row["StepTime"]);      //步骤时间

                    int StepTemp = FunClass.VVal((FunClass.VDbl(row["StepTemp"]) * ScaleAccuracy.digitTemp)); //步骤温度
                    double ccc = (FunClass.VDbl(row["StepEnergy"]) * ScaleAccuracy.digitEnergy);  //能量乘以10

                    int StepEnergy = FunClass.VVal(ccc.ToString());
                    int ActionControlValue = FunClass.VVal(row["ActionControlValue"]);      //动作控制方式值编码
                    int StepSpeed = FunClass.VVal((FunClass.VDbl(row["StepSpeed"]) * ScaleAccuracy.digitSpeed)); //zjq 转速小数位变成1位
                    int StepPower = FunClass.VVal(row["StepPower"]);        //步骤功率
                    int KeepTime = FunClass.VVal(row["KeepTime"]);      //步骤保持时间

                    double StepPress = FunClass.VDbl(row["StepPress"]);     //步骤压力
                    int stepPress = int.Parse((StepPress * ScaleAccuracy.digitPress).ToString("0000"));

                    if (isTou)  //如果为投料步骤
                    {
                        StepCode2 = 0;
                    }
                    else
                    {
                        StepCode1 = 0;
                    }
                    /// 工艺号 时间 温度 能量 控制方式 压力 转速 功率 反应量 空转时间
                    memStr += StepCode1.ToString("X4");   //称量动作编码
                    memStr += StepTime.ToString("X4");
                    memStr += (StepTemp).ToString("X4");  //2018.5.22  温度不乘10   // 2018.12.21 温度再次不乘10
                    memStr += StepEnergy.ToString("X4");   // 能量在上一步 已乘小数位数
                    memStr += ActionControlValue.ToString("X4");
                    memStr += stepPress.ToString("X4");
                    memStr += StepSpeed.ToString("X4");
                    memStr += StepPower.ToString("X4");
                    memStr += StepCode2.ToString("X4"); // 密炼动作编码
                    memStr += KeepTime.ToString("X4");

                    bool isDataErr = ActionControlValue < 0 || StepSpeed < 0 || StepPress < 0;
                    isDataErr = isDataErr || (StepTime < 0 && StepTemp < 0);
                    if (isDataErr)
                    {
                        errMsg = "密炼工艺数据中存在数据非法";
                        break;
                    }
                }

                int charLen = PlcDataRowCount * 40;
                memStr = memStr.PadRight(charLen, '0');
            }
            catch (Exception ex)
            {
                errMsg = ex.ToString();
            }

            return memStr;
        }

        /// <summary>
        /// 向PLC传送 系统参数
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcSysData(string _MaterialId, out string errStr)
        {
            errStr = "";
            string memStr = "";
            string devicePartID = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
            //techDigit 小数位
            string strSql = $"SELECT b.TechParamOrder,b.TechParamNameCN,b.DecDigit,a.TechParamVal,b.Enable FROM FormulaTechParam a,SYS_TechParam b where a.MaterialID='{_MaterialId}' and a.TechParamID=b.TechParamID and b.DevicePartID='{devicePartID}'  and b.Enable=1 order by b.TechParamOrder ";

            DataSet ds = SqlHelp.Query(strSql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                double techVal = FunClass.VDbl(row["TechParamVal"]);
                int techDigit = FunClass.VVal(row["DecDigit"]);
                memStr += ((int)(techVal * Math.Pow(10, techDigit))).ToString("X4");

                if (techVal < 0)
                {
                    errStr = "配方系统参数中存在非法数据";
                    break;
                }
            }

            memStr = memStr.PadRight(10 * 8, '0');

            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync(AddressConst.MixerSystemParams, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC发送 配方系统数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 向PLC传送 下密炼系统参数
        /// </summary>
        /// <param name="_MaterialId"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcSysDataF(string _MaterialId, out string errStr)
        {
            errStr = "";
            string memStr = "";
            string devicePartID = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixDown));
            //techDigit 小数位
            string strSql = "SELECT b.TechParamOrder,b.TechParamNameCN,b.DecDigit,a.TechParamVal " +
                            "FROM FormulaTechParamF a,SYS_TechParam b " +
                            "where a.MaterialID='" + _MaterialId + "' " +
                            "and a.TechParamID=b.TechParamID and b.[Enable]=1 " +
                            "and b.DevicePartID='" + devicePartID + "' " +
                            "order by b.TechParamOrder ";

            DataSet ds = SqlHelp.Query(strSql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                double techVal = FunClass.VDbl(row["TechParamVal"]);
                int techDigit = FunClass.VVal(row["DecDigit"]);
                memStr += ((int)(techVal * Math.Pow(10, techDigit))).ToString("X4");

                if (techVal < 0)
                {
                    errStr = "配方系统参数中存在非法数据";
                    break;
                }
            }

            memStr = memStr.PadRight(10 * 8, '0');

            if (errStr != "")
            {
                return false;
            }

            bool flag = NewuGlobal.MemMgr.Sync(AddressConst.MixerDownSystemParams, memStr);

            if (flag)
            {
                return true;
            }
            else
            {
                errStr = "向PLC下密炼发送 配方系统数据 超时";
                return false;
            }
        }

        /// <summary>
        /// 清空称量、密炼数据
        /// </summary>
        /// <param name="partType">设备类型</param>
        /// <param name="errStr">返回输出信息</param>
        /// <returns></returns>
        public bool TranWeightData(DevicePartType partType, out string errStr)
        {
            errStr = "";
            if (partType == DevicePartType.Carbon)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Carbon, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "炭黑数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.Oil)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Oil, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "油料设数据区域清空超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.Rubber)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Rubber, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "胶料数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.DrugMixer)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.DrugMixer, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "小药数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.Zno)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Zno, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "粉料数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.Plasticizer)
            {
                int plaDataPos = (int)MixerWeight.Plasticizer;

                bool flag = NewuGlobal.MemMgr.Sync(plaDataPos, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "塑解剂称量数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.Silane)
            {
                int siDataPos = (int)MixerWeight.Silane;
                bool flag = NewuGlobal.MemMgr.Sync(siDataPos, "".PadRight(70 * 4, '0'));

                if (!flag)
                {
                    errStr = "硅烷称量数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.MixUp)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.Mixer, "".PadRight(30 * 10 * 4, '0'));

                if (!flag)
                {
                    errStr = "密炼工艺数据区清空发送超时";
                    return false;
                }
            }
            else if (partType == DevicePartType.MixDown)
            {
                bool flag = NewuGlobal.MemMgr.Sync((int)MixerWeight.MixerDown, "".PadRight(30 * 10 * 4, '0'));

                if (!flag)
                {
                    errStr = "密炼工艺数据区清空发送超时";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 清空 称量部件、密炼 设定车数、实际车数
        /// </summary>
        /// <param name="c"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool TranWeightCarNum(char c, out string errStr)
        {
            int Addr = (int)MixerAnalogMiningSetBatch.Carbon;
            errStr = "";
            // 不能清除设定密炼车数 和 当前密炼车数 ！
            StringBuilder memSttr = new StringBuilder();
            for (int i = 1; i <= 10; i++)
            {
                memSttr.Append("0000");
            }

            bool flag = NewuGlobal.MemMgr.Sync(Addr, memSttr.ToString());

            //清空小药秤2设定车数
            flag = flag && NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningSetBatch.DrugMixer2, "0000");

            if (!flag)
            {
                errStr = "清空车数发送 超时";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送 称量部件、密炼 设定车数
        /// </summary>
        /// <param name="setNum"></param>
        /// <param name="materialid"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>

        public bool TranSetWeightCarNum(int setNum, string materialid, out string errStr)
        {
            int Addr = (int)MixerAnalogMiningSetBatch.Carbon;
            errStr = "";
            StringBuilder memSttr = new StringBuilder();
            string temp = setNum.ToString("D4");

            if (!NewuGlobal.SoftConfig.Carbon)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            if (!NewuGlobal.SoftConfig.Carbon2)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            if (!NewuGlobal.SoftConfig.Oil)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            if (!NewuGlobal.SoftConfig.Oil2)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            if (!NewuGlobal.SoftConfig.Zno)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            if (!NewuGlobal.SoftConfig.Zno2)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            memSttr.Append(temp);//胶料

            if (!NewuGlobal.SoftConfig.Drug)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            if (!NewuGlobal.SoftConfig.Silane)
                memSttr.Append("0000");
            else
                memSttr.Append(temp);

            memSttr.Append(temp);//密炼设定车数

            bool flag = NewuGlobal.MemMgr.Sync(Addr, memSttr.ToString());

            if (!NewuGlobal.SoftConfig.Drug2)
                flag = flag && NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningSetBatch.DrugMixer2, "0000");
            else
                flag = flag && NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningSetBatch.DrugMixer2, temp);

            if (!flag)
            {
                errStr = "清空车数发送 超时";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送Weight标志位
        /// </summary>
        /// <param name="c"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool TranStartWeightFlag(char c, out string errStr)
        {
            int Addr = (int)MixerTransFlag.Carbon;
            errStr = "";

            bool flag = NewuGlobal.MemMgr.Sync(Addr, "".PadRight(4 * 9, c));

            if (!flag)
            {
                errStr = "称量标志位发送 超时";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送密炼标志位
        /// </summary>
        /// <param name="c"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool TranStartMixFlag(char c, out string errStr)
        {
            int Addr = (int)MixerTransFlag.Mixer;
            errStr = "";

            bool flag = NewuGlobal.MemMgr.Sync(Addr, "".PadRight(4, c));

            if (!flag)
            {
                errStr = "密炼标志位发送 超时";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送恒温炼胶数据
        /// </summary>
        /// <param name="MaterialID"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        public bool PlcHwljData(string _MaterialId, out string errStr)
        {
            errStr = "";
            FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();

            var material = formulaMaterialRepository.GetModel(_MaterialId);
            if ((material.Reserve1 != "" && material.Reserve1 != "0") && (material.Reserve2 != "" && material.Reserve2 != "0"))
            {
                string memStr = GetAsciiCode(material.Reserve2);
                int c = FunClass.VVal(material.Reserve1);
                string mTmep = (c * 10).ToString("X4");   //*10 发送给PLC  恒温炼胶的温度
                if (errStr != "")
                {
                    return false;
                }

                bool flag = NewuGlobal.MemMgr.Sync(AddressConst.ConstantTempChar, memStr + mTmep);

                if (flag)
                {
                    return true;
                }
                else
                {
                    errStr = "向PLC发送 恒温炼胶数据 超时";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 若字符串中存在字母则转为ASCII码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetAsciiCode(string str)
        {
            string result = "";
            foreach (char c in str.ToCharArray())
            {
                result += Asc(c.ToString()).ToString("X1");
            }
            return result;
        }

        /// <summary>
        /// 转ASCII码
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public int Asc(string character)
        {
            if (character.Length == 1)
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                int intAsciiCode = ascii.GetBytes(character)[0];
                return intAsciiCode;
            }
            else
            {
                throw new Exception("Character is not valid");
            }
        }
    }
}