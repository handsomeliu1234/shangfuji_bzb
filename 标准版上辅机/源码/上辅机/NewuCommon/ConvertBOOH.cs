using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewuCommon
{

    public enum BOOH
    {
        _2,
        _10,
        _16

    }
    public static class ConvertBOOH
    {




        /// <summary>
        /// 参数 X表示十六进制字符串，数字len表示显示位数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Convert10To16(int data, int len)
        {

            String strA = data.ToString("x" + len);
            return strA;
        }



        public static string Convert16To10(string data, int len)
        {
            if (validData(data) == false) return "";

            int b = 0;
            try
            {
               b = int.Parse(data, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
            string r = formatStr(b.ToString(), len);

            return r;

        }



        public static string Convert10To2(int data, int len)
        {
            string r = Convert.ToString(data, 2);
            r = formatStr(r, len);
            return r;
        }


        public static string Convert2To10(string data, int len)
        {

            if (validData(data) == false) return "";

            int r = Convert.ToInt32(data, 2);

            string result = formatStr(r.ToString(), len);

            return result;

        }


        public static string formatStr(string r, int len)
        {

            if (r.Length >= len && len > 0)
            {
                r = r.Substring(0, len);
            }
            else
            {
                r = r.PadLeft(len, '0');
            }

            return r;
        }


        private static bool validData(string data)
        {
            if (data.IndexOf('\0') >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
