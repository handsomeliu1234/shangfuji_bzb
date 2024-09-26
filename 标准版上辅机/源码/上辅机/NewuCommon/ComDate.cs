using System;

namespace NewuCommon
{
    public static class ComDate
    {
        public static string YMD(DateTime D)
        {
            return string.Format("{0:yyyy\\-MM\\-dd}", D);
        }

        public static string SqlServerDate(DateTime t)
        {
            return t.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static DateTime MinDate(DateTime D)
        {
            string min = YMD(D) + " 00:00:00";

            return DateTime.Parse(min);
        }

        public static DateTime MaxDate(DateTime D)
        {
            string max = YMD(D) + " 23:59:59";
            return DateTime.Parse(max);
        }
    }
}