using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newu;
using NewuCommon;
using NewuBLL;
using NewuBLL.MixBll;
using System.Diagnostics;
using System.Windows.Forms;
using System.Resources;
using Repository.Model;
using Repository.Repository;

namespace NewuBLL
{
    /**
     * 1. 报表模板路径
     * 2. 软件对应的数据库信息配置
     * 3. 当前设备运行生产信息 类   持有其引用
     * 4. 小药代码 （暂时保留）
     * 5. 系统用户登录信息
     * 6. 共享内存
     * 7. 类型编码list  （在其他地方，更方便的使用ENUM 得到编码 ,来获取其特定的类型编码ID) 下同
     * 8. 设备部件List
     * 9. 系统设备信息 list
     * 10. 设备类型 list
     * 11. 获取设备部件编码
     * 12. 是否在调试模式下   Author:mr_step
     */

    public static class NewuGlobal
    {
        /**
         * 新配方发送成功后，通知主监控界面 和 胶料磅秤界面 进行界面更新操作
         */
        private static IRefresh rubyRefresh = null;

        public static IRefresh RubyDataChange
        {
            get { return rubyRefresh; }
            set { rubyRefresh = value; }
        }

        private static ScanRefresh rubyScan = null;

        public static ScanRefresh RubyScan
        {
            get { return rubyScan; }
            set { rubyScan = value; }
        }

        private static IRefresh mixRefrsh = null;

        public static IRefresh MixDataChange
        {
            get { return mixRefrsh; }
            set { mixRefrsh = value; }
        }

        private static IRefresh mixGridRefrsh = null;

        public static IRefresh MixGridDataChange
        {
            get { return mixGridRefrsh; }
            set { mixGridRefrsh = value; }
        }

        /// <summary>
        /// 1 报表模版所在路径
        /// </summary>
        public static string ReportPath
        {
            get { return FunClass.CurrentPath + "\\Template"; }
        }

        #region SoftConfig软件配制

        private static LoadConfigBLL _softConfig;

        /// <summary>
        /// 2 读取SoftConfig文件
        /// </summary>
        public static LoadConfigBLL SoftConfig
        {
            get
            {
                if (_softConfig == null) _softConfig = new LoadConfigBLL();
                return _softConfig;
            } //如果没有set则在其他调用中不能赋值
            set
            {
                _softConfig = value;
            }
        }

        #endregion SoftConfig软件配制

        #region 3 当前生产信息类

        private static CurrentRunInfoBLL _runInfo;

        /// <summary>
        /// 当前生产信息
        /// </summary>
        public static CurrentRunInfoBLL RunInfo
        {
            get
            {
                if (_runInfo == null) _runInfo = new CurrentRunInfoBLL();
                return _runInfo;
            }
        }

        private static MixBll.MixStateBLL _MixState;

        /// <summary>
        /// 4.. 密炼配方信息
        /// </summary>
        public static MixStateBLL GetMixState
        {
            get
            {
                if (_MixState == null) _MixState = new MixStateBLL();
                return _MixState;
            }
        }

        #endregion 3 当前生产信息类

        #region 5 系统登录用户信息

        private static NewuModel.TB_UserInfoMDL _TB_UserInfo = new NewuModel.TB_UserInfoMDL();

        public static NewuModel.TB_UserInfoMDL SysUser
        {
            get
            {
                return _TB_UserInfo;
            }
            set { _TB_UserInfo = value; }
        }

        public static DateTime LoginTime
        {
            get;
            set;
        }

        #endregion 5 系统登录用户信息

        #region 6 内存共享

        public static CSharedString MemDB
        {
            get { return MemMgr.ReadMem; }
        }

        public static CSharedString MemW
        {
            get { return MemMgr.WriteMem; }
        }

        public static CSharedString MemF
        {
            get { return MemMgr.FlagMem; }
        }

        private static MemAddrMananger _MemMgr = new MemAddrMananger();

        public static MemAddrMananger MemMgr
        {
            get { return _MemMgr; }
            set { _MemMgr = value; }
        }

        #endregion 6 内存共享

        /// <summary>
        ///
        /// 直接从数据库获取TypeCode
        /// </summary>

        #region 7 类型编码

        //private static List<NewuModel.SYS_TypeCodeMDL> _TypeCodeList;
        //public static List<NewuModel.SYS_TypeCodeMDL> TypeCodeList
        //{
        //get
        //{
        //    if (_TypeCodeList == null)
        //    {
        //        SYS_TypeCodeBLL typeCodeBll = new SYS_TypeCodeBLL();
        //        _TypeCodeList = typeCodeBll.GetModelList("");
        //    }
        //    return _TypeCodeList;
        //}
        //}

        private static List<SYS_TypeCode> typeCodeList;

        public static List<SYS_TypeCode> TypeCodeList
        {
            get
            {
                if (typeCodeList == null)
                {
                    SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
                    typeCodeList = typeCodeRepository.GetList("");
                }
                return typeCodeList;
            }
        }

        /// <summary>
        /// 根据TypeCodeID查找TypeCodeName
        /// </summary>
        public static string GetTypeNameByTypeCodeID(string _TypeCodeID)
        {
            string name = "";
            foreach (SYS_TypeCode item in TypeCodeList)
            {
                if (item.TypeCodeID == _TypeCodeID)
                {
                    name = item.TypeCodeName;
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// 根据TypeCodeName查找TypeCodeID
        /// </summary>
        public static string GetTypeCodeIDByCodeName(string _TypeCodeName)
        {
            string id = "";
            foreach (SYS_TypeCode item in TypeCodeList)
            {
                if (item.TypeCodeName == _TypeCodeName)
                {
                    id = item.TypeCodeID;
                    break;
                }
            }
            return id;
        }

        #endregion 7 类型编码

        #region 8 设备部件

        private static List<NewuModel.SYS_DevicePartMDL> _DevicePartList;

        public static List<NewuModel.SYS_DevicePartMDL> DevicePartList
        {
            get
            {
                if (_DevicePartList == null)
                {
                    NewuBLL.SYS_DevicePartBLL devicePartBll = new NewuBLL.SYS_DevicePartBLL();
                    _DevicePartList = devicePartBll.GetModelList("");
                }
                return _DevicePartList;
            }
        }

        /// <summary>
        /// 根据枚举类型获取 部件ID
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string DevicePartCodeByID(Newu.DevicePartType type)
        {
            return GetDevicePartIDByPartCode(GetDevicePartCode(type));
        }

        /// <summary>
        /// 根据DevicePartID查找DevicePartCode
        /// </summary>
        public static string DevicePartCodeByID(string _DevicePartID)
        {
            string name = "";
            foreach (NewuModel.SYS_DevicePartMDL item in DevicePartList)
            {
                if (item.DevicePartID == _DevicePartID)
                {
                    name = item.DevicePartCode;
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// 根据DevicePartCode查找DevicePartID
        /// </summary>
        public static string GetDevicePartIDByPartCode(string _DevicePartCode)
        {
            string id = "";
            foreach (NewuModel.SYS_DevicePartMDL item in DevicePartList)
            {
                if (item.DevicePartCode == _DevicePartCode)
                {
                    id = item.DevicePartID;
                    break;
                }
            }
            return id;
        }

        public static string GetDevicePartCodeByPartName(string _DevicePartName)
        {
            string code = "";
            foreach (NewuModel.SYS_DevicePartMDL item in DevicePartList)
            {
                if (item.DevicePartName == _DevicePartName)
                {
                    code = item.DevicePartCode;
                    break;
                }
            }
            return code;
        }

        #endregion 8 设备部件

        #region 9 设备信息

        private static List<NewuModel.SYS_DeviceMDL> _DeviceList;

        public static List<NewuModel.SYS_DeviceMDL> DeviceList
        {
            get
            {
                if (_DeviceList == null)
                {
                    NewuBLL.SYS_DeviceBLL deviceBll = new NewuBLL.SYS_DeviceBLL();
                    _DeviceList = deviceBll.GetModelList("");
                }
                return _DeviceList;
            }
        }

        /// <summary>
        /// 根据DeviceID查找DeviceCode
        /// </summary>
        public static string DeviceCodeByID(string _DeviceID)
        {
            string name = "";
            foreach (NewuModel.SYS_DeviceMDL item in DeviceList)
            {
                if (item.DeviceID == _DeviceID)
                {
                    name = item.DeviceCode;
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// 根据DeviceCode查找DeviceID
        /// </summary>
        public static string DeviceIDByCode(string _DeviceCode)
        {
            string id = "";
            foreach (NewuModel.SYS_DeviceMDL item in DeviceList)
            {
                if (item.DeviceCode == _DeviceCode)
                {
                    id = item.DeviceID;
                    break;
                }
            }
            return id;
        }

        #endregion 9 设备信息

        #region 10 设备类型

        private static List<NewuModel.SYS_DeviceTypeMDL> _DeviceTypeList;

        public static List<NewuModel.SYS_DeviceTypeMDL> DeviceTypeList
        {
            get
            {
                if (_DeviceTypeList == null)
                {
                    NewuBLL.SYS_DeviceTypeBLL deviceTypeBll = new NewuBLL.SYS_DeviceTypeBLL();
                    _DeviceTypeList = deviceTypeBll.GetModelList("");
                }
                return _DeviceTypeList;
            }
        }

        /// <summary>
        /// 根据DeviceTypeID查找DeviceTypeCode
        /// </summary>
        public static string DeviceTypeCodeByID(string _DeviceTypeID)
        {
            string name = "";
            foreach (NewuModel.SYS_DeviceTypeMDL item in DeviceTypeList)
            {
                if (item.DeviceTypeID == _DeviceTypeID)
                {
                    name = item.DeviceTypeCode;
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// 根据DeviceTypeCode查找DeviceTypeID
        /// </summary>
        public static string DeviceTypeIDByCode(string _DeviceTypeCode)
        {
            string id = "";
            foreach (NewuModel.SYS_DeviceTypeMDL item in DeviceTypeList)
            {
                if (item.DeviceTypeCode == _DeviceTypeCode)
                {
                    id = item.DeviceTypeID;
                    break;
                }
            }
            return id;
        }

        #endregion 10 设备类型

        #region 11 获取设备部件编码

        /// <summary>
        /// 获取设备部件编码
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDevicePartCode(DevicePartType type)
        {
            string _devicePartCode = "";
            switch (type)
            {
                case DevicePartType.Carbon:
                    _devicePartCode = "CarbonScales_001";   //炭黑秤
                    break;

                case DevicePartType.Oil:
                    _devicePartCode = "OilScales_001";   //油料秤
                    break;

                case DevicePartType.DrugMixer:
                    _devicePartCode = "MixDrugScales_001";  //小药秤
                    break;

                case DevicePartType.MixUp:
                    _devicePartCode = "UpMix_001";   // 上密炼机
                    break;

                case DevicePartType.Rubber:
                    _devicePartCode = "MixRubberScales_001";  // 胶料秤
                    break;

                case DevicePartType.Zon:   //  粉料秤
                    _devicePartCode = "ZonScales_001";
                    break;

                case DevicePartType.Plasticizer:   //  塑解剂秤
                    _devicePartCode = "PlasticScales_001";
                    break;

                case DevicePartType.Silane:   //  硅烷秤
                    _devicePartCode = "SilaneScales_001";
                    break;

                case DevicePartType.MixDown:   //下密炼机
                    _devicePartCode = "DownMix_001";
                    break;
            }

            return _devicePartCode;
        }

        #endregion 11 获取设备部件编码

        #region 12 获取是否在调试模式状态 运行为true,调试为false,mr_step

        public static bool bDebugFlag;

        #endregion 12 获取是否在调试模式状态 运行为true,调试为false,mr_step

        private static string alarm_info = "";

        public static string AlarmInfo
        {
            get { return alarm_info; }
            set { alarm_info = value; }
        }

        private static string _now_mix_materialID;
        private static string _now_mix_orderID;
        private static string _now_mix_orderName;

        // 默认就是  密炼的
        public static string Now_MaterialID
        {
            get { return _now_mix_materialID; }
            set { _now_mix_materialID = value; }
        }

        public static string Now_OrderID
        {
            get { return _now_mix_orderID; }
            set { _now_mix_orderID = value; }
        }

        public static string Now_OrderName
        {
            get { return _now_mix_orderName; }
            set { _now_mix_orderName = value; }
        }

        private static string _now_weight_materialID;
        private static string _now_weight_orderID;
        private static string _now_weight_orderName;

        public static string Now_Weight_MaterialID
        {
            get { return _now_weight_materialID; }
            set { _now_weight_materialID = value; }
        }

        public static string Now_Weight_OrderID
        {
            get { return _now_weight_orderID; }
            set { _now_weight_orderID = value; }
        }

        public static string Now_Weight_OrderName
        {
            get { return _now_weight_orderName; }
            set { _now_weight_orderName = value; }
        }

        private static string _next_Mix_orderName = "No Recipe.";

        public static string Next_Mix_orderName
        {
            get { return _next_Mix_orderName; }
            set { _next_Mix_orderName = value; }
        }

        /// <summary>
        /// 下个配方名称
        /// </summary>
        public static string Next_OrderName { get; set; }

        /// <summary>
        /// 开卸料门
        /// </summary>
        public static int OpenDropDoorIndex { get; set; }

        public static Form FmMain { get; set; }

        public static bool MonitorShowed { get; set; }
        public static Form FmMonitor { get; set; }

        /// <summary>
        /// 子窗体
        /// </summary>
        public static UserControl FmOwned { get; set; }

        public static UserControl UserMonitor { get; set; }
        public static Form FmJL { get; set; }
        public static ResourceManager LanguagResourceManager { get; set; }

        public static System.Globalization.CultureInfo ZhCNCulture = new System.Globalization.CultureInfo("zh-CN", true);
        public static System.Globalization.CultureInfo EnGBCulture = new System.Globalization.CultureInfo("en-GB", true);
        public static string SupportLanguage { get; set; }

        public static string GetRes(string str)
        {
            return LanguagResourceManager.GetString(str);
        }
    }
}