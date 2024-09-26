using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NewuCommon
{
    public static class FunClass
    {
        public static string CurrentPath
        {
            get
            {
                return Application.StartupPath.ToString();
            }
        }

        public static double GetMemHexDec(string r, int decimalInt)
        {
            if (string.IsNullOrEmpty(r))
            {
                return 0;
            }
            if (r.IndexOf('\0') >= 0)
            {
                return 0;
            }

            int d = Convert.ToInt32(r, 16);
            if (decimalInt < 0)
                return 0;

            return (double)d / (double)Math.Pow(10, decimalInt);
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int VCint(double d)
        {
            try
            {
                return (int)(d + 0.5);
            }
            catch
            {
                return 0;
            }
        }

        public static double VDbl(object obj)
        {
            try
            {
                if (obj != null)
                {
                    return Convert.ToDouble(obj.ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static decimal VDecimal(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 判断输入的是否为小数或整数，返回0则为空或字符，返回2则为整数
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int IsEmptyOrNumber(string txt)
        {
            int veri = 0;
            Regex regDig = new Regex(@"^\d+\.\d+$");
            Regex regNum = new Regex(@"^\d+$");
            if (txt.Trim() == "" || txt.Length < 0)
            {
                veri = 0;
            }
            else if (regDig.IsMatch(txt))
            {
                // 是小数
                veri = 1;
            }
            else if (regNum.IsMatch(txt))
            {
                // 是整数
                veri = 2;
            }
            return veri;
        }

        /// <summary>
        /// 判断输入的是否为数字，不是则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int VVal(object obj)
        {
            if (obj == null)
                return 0;
            string str = obj.ToString();

            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

            if (str.Contains("."))
            {
                foreach (char a in str)
                {
                    if (a == '.')
                        continue;
                    if (a < '0' || a > '9')
                        return 0;
                }
                double d = Convert.ToDouble(obj.ToString());
                return VCint(d);
            }

            foreach (char a in str)
            {
                if (a < '0' || a > '9')
                {
                    return 0;
                }
            }
            return Convert.ToInt32(obj.ToString());
        }

        /// <summary>
        /// 判断输入的是否为数字，不是则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool VValDouble(object obj)
        {
            string str = obj.ToString();
            return Regex.IsMatch(str, "([1-9]+[0-9]*|0)(\\.[\\d]+)?");
        }

        public static string VStr(object obj)
        {
            string r = "";
            if (obj != null)
            {
                r = obj.ToString();
            }
            return r;
        }

        public static bool IsIPAddress(string ipAddress)
        {
            Regex validipregex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            return (ipAddress != "" && validipregex.IsMatch(ipAddress.Trim()));
        }
    }
}