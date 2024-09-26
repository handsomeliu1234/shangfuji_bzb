using Newu;
using System;

namespace Repository.GlobalConfig
{
    //Im NewuAutomation 2018.8.14
    /*
                   _ooOoo_
                  o8888888o
                  88" . "88
                  (| -_- |)
                  O\  =  /O
               ____/`---'\____
             .'  \\|     |//  `.
            /  \\|||  :  |||//  \
           /  _||||| -:- |||||-  \
           |   | \\\  -  /// |   |
           | \_|  ''\---/''  |   |
           \  .-\__  `-`  ___/-. /
         ___`. .'  /--.--\  `. . __
      ."" '<  `.___\_<|>_/___.'  >'"".
     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
     \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======
                   `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
         佛祖保佑       永无BUG
*/

    /// <summary>
    /// 业务层类 在此处统一配置秤的 精度， 为简化以后的开发 配置对应与每个设备部件 -- 秤 默认配置： 碳 胶料 精度为 0.1 油 粉 精度为0.01 小药 精度为0.001
    ///
    /// 该精度包括 向PLC发送数据 以及从PLC 或 数据库读取数据并在界面上显示 全为该对应配置的小数位数
    ///
    ///
    /// dgvTech 不在此处配置 加了todo 手动该
    /// </summary>
    public static class ScaleAccuracy
    {
        public static int digitTemp = (int)Math.Pow(10, NewuGlobal.SoftConfig.TempDigit);   //10'
        public static int digitEnergy = (int)Math.Pow(10, NewuGlobal.SoftConfig.EnergyDigit);
        public static int digitSpeed = (int)Math.Pow(10, NewuGlobal.SoftConfig.SpeedDigit); //转速 1位小数
        public static int digitPress = (int)Math.Pow(10, NewuGlobal.SoftConfig.PressDigit);

        /// <summary>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetDigitByPartCode(string str)
        {
            int digit;

            if (str.Equals(NewuGlobal.CarbonScales))
            {
                digit = NewuGlobal.SoftConfig.CarbonDigit;   //炭黑秤
            }
            else if (str.Equals(NewuGlobal.RubberScales))
            {
                digit = NewuGlobal.SoftConfig.RubberDigit;  // 胶料秤
            }
            else if (str.Equals(NewuGlobal.OilScales))
            {
                digit = NewuGlobal.SoftConfig.OilDigit;  //油料秤
            }
            else if (str.Equals(NewuGlobal.ZnoScales))
            {
                digit = NewuGlobal.SoftConfig.ZnoDigit;
            }
            else if (str.Equals(NewuGlobal.PlaScales))
            {
                digit = NewuGlobal.SoftConfig.PlaDigit;
            }
            else if (str.Equals(NewuGlobal.SiScales))
            {
                digit = NewuGlobal.SoftConfig.SilaneDigit;
            }
            else if (str.Equals(NewuGlobal.DrugScales))
            {
                digit = NewuGlobal.SoftConfig.DrugDigit;  //小药秤
            }
            else
                digit = 2;

            return digit;
        }

        /******  以下  无需改动配置   ****/

        /// <summary>
        /// </summary>
        /// <param name="dvp"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal AgreeWeightShow(DevicePartType dvp, decimal data)
        {
            try
            {
                return Math.Round(data, GetDigit(dvp));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ScaleAccuracy").Error(ex.ToString());
                return data;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="dvp"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string AgreeWeightShow(DevicePartType dvp, int num)
        {
            int pos = GetDigit(dvp);
            if (pos == 1)
            {
                return ((1.0 * num) / (Math.Pow(10, pos))).ToString("f1");
            }
            else if (pos == 2)
            {
                return ((1.0 * num) / (Math.Pow(10, pos))).ToString("f2");
            }
            else
            {
                return ((1.0 * num) / (Math.Pow(10, pos))).ToString("f3");
            }
        }

        /// <summary>
        ///为方便修改  直接放入softconfig
        /// </summary>
        /// <param name="dvp"></param>
        /// <returns></returns>
        public static int GetDigit(DevicePartType dvp)
        {
            int digit;
            switch (dvp)
            {
                case DevicePartType.Carbon:
                    digit = NewuGlobal.SoftConfig.CarbonDigit;
                    break;

                case DevicePartType.Rubber:
                    digit = NewuGlobal.SoftConfig.RubberDigit;
                    break;

                case DevicePartType.Oil:
                    digit = NewuGlobal.SoftConfig.OilDigit;
                    break;

                case DevicePartType.Zno:
                    digit = NewuGlobal.SoftConfig.ZnoDigit;
                    break;

                case DevicePartType.Silane:
                    digit = NewuGlobal.SoftConfig.SilaneDigit;
                    break;

                case DevicePartType.Plasticizer:
                    digit = NewuGlobal.SoftConfig.PlaDigit;
                    break;

                case DevicePartType.DrugMixer:
                    digit = NewuGlobal.SoftConfig.DrugDigit;
                    break;

                default:
                    digit = 2;
                    break;
            }
            return digit;
        }
    }
}