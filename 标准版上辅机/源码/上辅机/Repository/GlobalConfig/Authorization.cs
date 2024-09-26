using NewuCommon;
using System;

using System.Xml;

namespace Repository.GlobalConfig
{
    public static class Authorization
    {
        /// <summary>
        /// 判断是否在授权日期内
        /// </summary>
        /// <returns></returns>
        public static bool JudgeAuthorization()
        {
            try
            {
                double nowTime = DateTime.Now.ToOADate();
                if (nowTime < double.Parse(NewuGlobal.SoftConfig.SystemPara))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 增加授权时间天数
        /// </summary>
        /// <param name="day"></param>
        /// 更新增加的天数
        /// <returns></returns>
        public static bool UpDataParam(int day)
        {
            try
            {
                double nowTime = DateTime.Now.ToOADate();
                string str1 = DateTime.Now.ToOADate().ToString();
                double wings = double.Parse(str1);
                string str = ((int)wings + day).ToString();
                NewuGlobal.SoftConfig.SetSystemPara(str);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}