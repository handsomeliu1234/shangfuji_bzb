using System;

namespace Repository.GlobalConfig
{
    public class CreateTableRPT
    {
        private CreateDataTableUtil createUtil = new CreateDataTableUtil();

        public bool CreateTableRPT_All(DateTime orderTime)
        {
            if (CreateTableRPT_Weight(orderTime) && CreateTableRPT_MixStep(orderTime) && CreateTableRPT_DeviceEvent(orderTime) && CreateTableRPT_Curve(orderTime) && CreateTableRPT_WeightF(orderTime) && CreateTableRPT_MixStepF(orderTime) && CreateTableRPT_DeviceEventF(orderTime) && CreateTableRPT_CurveF(orderTime))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 创建称量报表存储表
        /// </summary>
        /// <param name="orderTime"></param>
        /// <returns></returns>
        private bool CreateTableRPT_Weight(DateTime orderTime)
        {
            string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_Weight]";

            if (!createUtil.IsExist(TableName))
            {
                if (!createUtil.CreateTable(TableName, "RPT_Weight", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        private bool CreateTableRPT_WeightF(DateTime orderTime)
        {
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_WeightF]";

                if (!createUtil.IsExist(TableName))
                {
                    if (!createUtil.CreateTable(TableName, "RPT_Weight", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
            return true;
        }

        /// <summary>
        /// 创建工艺报表存储表
        /// </summary>
        /// <param name="orderTime"></param>
        /// <returns></returns>
        private bool CreateTableRPT_MixStep(DateTime orderTime)
        {
            string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_MixStep]";

            if (!createUtil.IsExist(TableName))
            {
                if (!createUtil.CreateTable(TableName, "RPT_MixStep", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        private bool CreateTableRPT_MixStepF(DateTime orderTime)
        {
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_MixStepF]";

                if (!createUtil.IsExist(TableName))
                {
                    if (createUtil.CreateTable(TableName, "RPT_MixStep", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
            return true;
        }

        private bool CreateTableRPT_DeviceEvent(DateTime orderTime)
        {
            string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_DeviceEvent]";

            if (!createUtil.IsExist(TableName))
            {
                if (!createUtil.CreateTable(TableName, "RPT_DeviceEvent", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        private bool CreateTableRPT_DeviceEventF(DateTime orderTime)
        {
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_DeviceEventF]";

                if (!createUtil.IsExist(TableName))
                {
                    if (!createUtil.CreateTable(TableName, "RPT_DeviceEvent", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
            return true;
        }

        /// <summary>
        /// 创建曲线报表存储表
        /// </summary>
        /// <param name="orderTime"></param>
        /// <returns></returns>
        private bool CreateTableRPT_Curve(DateTime orderTime)
        {
            string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_Curve]";

            if (!createUtil.IsExist(TableName))
            {
                if (!createUtil.CreateTable(TableName, "RPT_Curve", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        private bool CreateTableRPT_CurveF(DateTime orderTime)
        {
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                string TableName = "[" + orderTime.ToString("yyyy") + "_RPT_CurveF]";

                if (!createUtil.IsExist(TableName))
                {
                    if (!createUtil.CreateTable(TableName, "RPT_Curve", NewuGlobal.SoftConfig.DBMain, NewuGlobal.SoftConfig.DBData))
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
            return true;
        }
    }
}