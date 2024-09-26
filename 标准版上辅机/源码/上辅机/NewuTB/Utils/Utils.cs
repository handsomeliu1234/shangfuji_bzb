using HZH_Controls.Controls;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NewuTB.Utils
{
    public static class Utils
    {
        public static void SetPipeStyle(UCConduit uCConduit, bool flag, Color color)
        {
            if (flag)
            {
                uCConduit.LiquidSpeed = 300;
                uCConduit.LiquidColor = color;
            }
            else
            {
                uCConduit.LiquidSpeed = 500000;
                uCConduit.LiquidColor = color;
            }
        }

        public static List<TB_BinSeting> GetTB_BinSetings(string typeCodeName)
        {
            try
            {
                string typeCodeId = NewuGlobal.GetTypeCodeIDByCodeName(typeCodeName);
                List<TB_BinSeting> tB_BinSetings = new TB_BinSettingRepository().GetListJoinMaterialCode("", typeCodeId);
                return tB_BinSetings;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Utils").Error(ex.ToString());
                return null;
            }
        }

        public static void DisplayCarbonControlStyle(List<TB_BinSeting> binSetings, UserControl control)
        {
            try
            {
                int cnt = 1;
                foreach (var binSetting in binSetings)
                {
                    CarbonBin cb = control.Controls["carbonBin0" + cnt] as CarbonBin;
                    if (cb == null)
                        continue;

                    cb.NewuLabForeColor = Color.RoyalBlue;
                    cnt++;
                }
                SYS_TypeCodeRepository sYS_TypeCodeRepository = new SYS_TypeCodeRepository();
                string typecodeNameC = sYS_TypeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T炭黑);
                string typecodeNameWC = sYS_TypeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T白炭黑);
                string where = "MaterialID='" + NewuGlobal.Now_Weight_MaterialID + "' and TypeCodeName in ('" + typecodeNameC + "' , '" + typecodeNameWC + "') ";
                List<View_FormulaWeigh> formulaWeighs = new ViewFormulaWeighRepository().GetModelList(0, where, "BinNo asc");
                if (formulaWeighs != null)
                {
                    if (formulaWeighs.Count > 0)
                    {
                        foreach (var item in formulaWeighs)
                        {
                            CarbonBin cb = control.Controls["carbonBin0" + item.BinNo] as CarbonBin;
                            if (cb == null)
                                continue;
                            cb.NewuLabForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Utils").Error(ex.ToString());
            }
        }

        public static void DisplayOilControlStyle(List<TB_BinSeting> binSetings, UserControl control)
        {
            try
            {
                int cnt = 1;
                foreach (var binSetting in binSetings)
                {
                    OilTank cb = control.Controls["oilTank0" + cnt] as OilTank;
                    if (cb == null)
                        continue;

                    cb.NewuLabForeColor = Color.White;
                    cnt++;
                }
                string typecodeName = new SYS_TypeCodeRepository().GetTypeCodeNameByEnum(TypeCodeEnum.T油料);
                string where = "MaterialID='" + NewuGlobal.Now_Weight_MaterialID + "' and TypeCodeName='" + typecodeName + "'";
                List<View_FormulaWeigh> formulaWeighs = new ViewFormulaWeighRepository().GetModelList(0, where, "BinNo asc");
                if (formulaWeighs != null)
                {
                    if (formulaWeighs.Count > 0)
                    {
                        foreach (var item in formulaWeighs)
                        {
                            OilTank cb = control.Controls["oilTank0" + item.BinNo] as OilTank;
                            if (cb == null)
                                continue;
                            cb.NewuLabForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Utils").Error(ex.ToString());
            }
        }

        public static void DisplayZnoControlStyle(List<TB_BinSeting> binSetings, UserControl control)
        {
            try
            {
                int cnt = 1;
                foreach (var binSetting in binSetings)
                {
                    OilTank cb = control.Controls["carbonBin0" + cnt] as OilTank;
                    if (cb == null)
                        continue;

                    cb.NewuLabForeColor = Color.Blue;
                    cnt++;
                }
                string typecodeName = new SYS_TypeCodeRepository().GetTypeCodeNameByEnum(TypeCodeEnum.T粉料);
                string where = "MaterialID='" + NewuGlobal.Now_Weight_MaterialID + "' and TypeCodeName='" + typecodeName + "'";
                List<View_FormulaWeigh> formulaWeighs = new ViewFormulaWeighRepository().GetModelList(0, where, "BinNo asc");
                if (formulaWeighs != null)
                {
                    if (formulaWeighs.Count > 0)
                    {
                        foreach (var item in formulaWeighs)
                        {
                            OilTank cb = control.Controls["carbonBin0" + item.BinNo] as OilTank;
                            if (cb == null)
                                continue;
                            cb.NewuLabForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Utils").Error(ex.ToString());
            }
        }

        public static void DisplaySiControlStyle(List<TB_BinSeting> binSetings, UserControl control)
        {
            try
            {
                int cnt = 1;
                foreach (var binSetting in binSetings)
                {
                    OilTank cb = control.Controls["SilTank0" + cnt] as OilTank;
                    if (cb == null)
                        continue;

                    cb.NewuLabForeColor = Color.White;
                    cnt++;
                }
                string typecodeName = new SYS_TypeCodeRepository().GetTypeCodeNameByEnum(TypeCodeEnum.T硅烷);
                string where = "MaterialID='" + NewuGlobal.Now_Weight_MaterialID + "' and TypeCodeName='" + typecodeName + "'";
                List<View_FormulaWeigh> formulaWeighs = new ViewFormulaWeighRepository().GetModelList(0, where, "BinNo asc");
                if (formulaWeighs != null)
                {
                    if (formulaWeighs.Count > 0)
                    {
                        foreach (var item in formulaWeighs)
                        {
                            OilTank cb = control.Controls["SilTank0" + item.BinNo] as OilTank;
                            if (cb == null)
                                continue;
                            cb.NewuLabForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("Utils").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 只允许输入数字
        /// </summary>
        /// <param name="e"></param>
        /// <param name="flag">允许输入小数点标识</param>
        public static void TxtPreSetGcsU(KeyPressEventArgs e, bool flag)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            if (!flag && e.KeyChar == 46)
                e.Handled = true;
        }


        public static DataTable FormatDeviceEventValue(DataTable dt)
        {
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    item["StartTime"] = Convert.ToDateTime(item["StartTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    item["EndTime"] = Convert.ToDateTime(item["EndTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    item["Temp"] = Convert.ToDecimal(item["Temp"]).ToString("f0");
                    item["Speed"] = Convert.ToDecimal(item["Speed"]).ToString("f1");
                    item["Energy"] = Convert.ToDecimal(item["Energy"]).ToString("f1");
                    item["Power"] = Convert.ToDecimal(item["Power"]).ToString("f0");
                    item["Press"] = Convert.ToDecimal(item["Press"]).ToString("f2");
                }
                return dt;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_BatchReportDetail").Error(ex.ToString());
                return dt;
            }
        }

        public static DataTable FormatWeightValue(DataTable dt)
        {
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["DevicePartCode"].ToString() == NewuGlobal.DrugScales || item["DevicePartCode"].ToString() == NewuGlobal.SiScales)
                    {
                        item["SetWeight"] = Convert.ToDecimal(item["SetWeight"]).ToString("0.000");
                        item["ActWeight"] = Convert.ToDecimal(item["ActWeight"]).ToString("0.000");
                        item["AllowError"] = Convert.ToDecimal(item["AllowError"]).ToString("0.000");
                        item["ActError"] = Convert.ToDecimal(item["ActError"]).ToString("0.000");
                    }
                    else
                    {
                        item["SetWeight"] = Convert.ToDecimal(item["SetWeight"]).ToString("0.00");
                        item["ActWeight"] = Convert.ToDecimal(item["ActWeight"]).ToString("0.00");
                        item["AllowError"] = Convert.ToDecimal(item["AllowError"]).ToString("0.00");
                        item["ActError"] = Convert.ToDecimal(item["ActError"]).ToString("0.00");
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_BatchReportDetail").Error(ex.ToString());
                return dt;
            }
        }

        public static DataTable FormatMixStepValue(DataTable dt)
        {
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    item["SetTime"] = Convert.ToDecimal(item["SetTime"]).ToString("f0");
                    item["ActTime"] = Convert.ToDecimal(item["ActTime"]).ToString("f0");
                    item["SetTemp"] = Convert.ToDecimal(item["SetTemp"]).ToString("f0");
                    item["ActTemp"] = Convert.ToDecimal(item["ActTemp"]).ToString("f0");
                    item["SetPower"] = Convert.ToDecimal(item["SetPower"]).ToString("f0");
                    item["ActPower"] = Convert.ToDecimal(item["ActPower"]).ToString("f0");

                    item["SetEnergy"] = Convert.ToDecimal(item["SetEnergy"]).ToString("f2");
                    item["ActEnergy"] = Convert.ToDecimal(item["ActEnergy"]).ToString("f2");
                    item["SetPress"] = Convert.ToDecimal(item["SetPress"]).ToString("f2");
                    item["ActPress"] = Convert.ToDecimal(item["ActPress"]).ToString("f2");

                    item["SetSpeed"] = Convert.ToDecimal(item["SetSpeed"]).ToString("f1");
                    item["ActSpeed"] = Convert.ToDecimal(item["ActSpeed"]).ToString("f1");
                }
                return dt;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_BatchReportDetail").Error(ex.ToString());
                return dt;
            }
        }

        /// <summary>
        /// 批报表专用方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable FormatExecValue(DataTable dt)
        {
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["DevicePartCode"].ToString() == NewuGlobal.DrugScales || item["DevicePartCode"].ToString() == NewuGlobal.SiScales)
                    {
                        item["SetWeight"] = Convert.ToDecimal(item["SetWeight"]).ToString("f3");
                        item["ActWeight"] = Convert.ToDecimal(item["ActWeight"]).ToString("f3");
                    }
                    else
                    {
                        item["SetWeight"] = Convert.ToDecimal(item["SetWeight"]).ToString("f2");
                        item["ActWeight"] = Convert.ToDecimal(item["ActWeight"]).ToString("f2");
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_BatchReportDetail").Error(ex.ToString());
                return dt;
            }
        }

    }
}