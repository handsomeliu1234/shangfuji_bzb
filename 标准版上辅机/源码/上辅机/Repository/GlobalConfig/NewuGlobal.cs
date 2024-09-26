using Newu;
using NewuCommon;
using Repository.Model;
using Repository.Repository;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Forms;
using WindowsCaptureVideo;

namespace Repository.GlobalConfig
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
        private static TB_RoleRepository roleRepository = new TB_RoleRepository();

        /**
         * 新配方发送成功后，通知主监控界面 和 胶料磅秤界面 进行界面更新操作
         */
        private static IRefresh rubyRefresh = null;

        public static IRefresh RubyDataChange
        {
            get
            {
                return rubyRefresh;
            }
            set
            {
                rubyRefresh = value;
            }
        }

        private static ScanRefresh rubyScan = null;

        public static ScanRefresh RubyScan
        {
            get
            {
                return rubyScan;
            }
            set
            {
                rubyScan = value;
            }
        }

        private static IRefresh mixRefrsh = null;

        public static IRefresh MixDataChange
        {
            get
            {
                return mixRefrsh;
            }
            set
            {
                mixRefrsh = value;
            }
        }

        public static IRefresh MixGridDataChange

        {
            get; set;
        }

        /// <summary>
        /// 1 报表模版所在路径
        /// </summary>
        public static string ReportPath
        {
            get
            {
                return FunClass.CurrentPath + "\\Template";
            }
        }

        #region SoftConfig软件配制

        private static LoadConfig _softConfig;

        /// <summary>
        /// 2 读取SoftConfig文件
        /// </summary>
        public static LoadConfig SoftConfig
        {
            get
            {
                if (_softConfig == null)
                    _softConfig = new LoadConfig();
                return _softConfig;
            } //如果没有set则在其他调用中不能赋值
            set
            {
                _softConfig = value;
            }
        }

        #endregion SoftConfig软件配制

        #region 3 当前生产信息类

        private static CurrentRunInfoUtil _runInfo;

        /// <summary>
        /// 当前生产信息
        /// </summary>
        public static CurrentRunInfoUtil RunInfo
        {
            get
            {
                if (_runInfo == null)
                    _runInfo = new CurrentRunInfoUtil();
                return _runInfo;
            }
        }

        private static MixerState mixerState;

        /// <summary>
        /// 4.. 密炼配方信息
        /// </summary>
        public static MixerState GetMixState
        {
            get
            {
                if (mixerState == null)
                    mixerState = new MixerState();
                return mixerState;
            }
        }

        #endregion 3 当前生产信息类

        #region 5 系统登录用户信息 替换为TB_UserInfo 20230110 李辉

        private static TB_UserInfo tB_UserInfo = new TB_UserInfo();

        public static TB_UserInfo TB_UserInfo
        {
            get
            {
                return tB_UserInfo;
            }
            set
            {
                tB_UserInfo = value;
            }
        }

        #endregion 5 系统登录用户信息 替换为TB_UserInfo 20230110 李辉

        #region 6 内存共享

        public static CSharedString MemDB
        {
            get
            {
                return MemMgr.ReadMem;
            }
        }

        public static CSharedString MemW
        {
            get
            {
                return MemMgr.WriteMem;
            }
        }

        public static CSharedString MemF
        {
            get
            {
                return MemMgr.FlagMem;
            }
        }

        private static MemAddrMananger _MemMgr = new MemAddrMananger();

        public static MemAddrMananger MemMgr
        {
            get
            {
                return _MemMgr;
            }
            set
            {
                _MemMgr = value;
            }
        }

        #endregion 6 内存共享

        #region 7 类型编码

        /// <summary>
        /// 直接从数据库获取TypeCode
        /// </summary>
        private static List<SYS_TypeCode> _typeCodeList;

        public static List<SYS_TypeCode> TypeCodeList
        {
            get
            {
                if (_typeCodeList == null)
                {
                    SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
                    _typeCodeList = typeCodeRepository.GetList("");
                }
                return _typeCodeList;
            }
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

        private static List<SYS_DevicePart> _devicePartList;

        public static List<SYS_DevicePart> DevicePartList
        {
            get
            {
                if (_devicePartList == null)
                {
                    SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
                    _devicePartList = devicePartRepository.GetList("");
                }
                return _devicePartList;
            }
        }

        //根据设备部件的PartNum查找设备部件编码
        public static string CarbonScales = DevicePartList.Find(d => d.PartNum == 1).DevicePartCode;

        public static string DrugScales = DevicePartList.Find(d => d.PartNum == 2).DevicePartCode;
        public static string PlaScales = DevicePartList.Find(d => d.PartNum == 3).DevicePartCode;
        public static string SiScales = DevicePartList.Find(d => d.PartNum == 4).DevicePartCode;
        public static string OilScales = DevicePartList.Find(d => d.PartNum == 5).DevicePartCode;
        public static string RubberScales = DevicePartList.Find(d => d.PartNum == 6).DevicePartCode;
        public static string WCarbonScales = DevicePartList.Find(d => d.PartNum == 7).DevicePartCode;
        public static string ZnoScales = DevicePartList.Find(d => d.PartNum == 8).DevicePartCode;
        public static string MixerParts = DevicePartList.Find(d => d.PartNum == 9).DevicePartCode;
        public static string DownMixers = DevicePartList.Find(d => d.PartNum == 10).DevicePartCode;
        public static string DrugScales2 = DevicePartList.Find(d => d.PartNum == 11).DevicePartCode;
        public static string OilScales2 = DevicePartList.Find(d => d.PartNum == 12).DevicePartCode;
        public static string ZnoScales2 = DevicePartList.Find(d => d.PartNum == 13).DevicePartCode;

        public const int CarbonPartNum = 1;
        public const int DrugPartNum = 2;
        public const int PlaPartNum = 3;
        public const int SiPartNum = 4;
        public const int OilPartNum = 5;
        public const int RubberPartNum = 6;
        public const int WCarbonPartNum = 7;
        public const int ZnoPartNum = 8;
        public const int MixerPartNum = 9;
        public const int DownMixerPartNum = 10;
        public const int DrugPartNum2 = 11;
        public const int OilPartNum2 = 12;
        public const int ZnoPartNum2 = 13;

        /// <summary>
        /// 根据枚举类型获取 部件ID
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string DevicePartCodeByID(DevicePartType type)
        {
            return GetDevicePartIDByPartCode(GetDevicePartCode(type));
        }

        /// <summary>
        /// 根据DevicePartID查找DevicePartCode
        /// </summary>
        public static string DevicePartCodeByID(string devicePartID)
        {
            string name = DevicePartList.Find(e => e.DevicePartID.Equals(devicePartID)).DevicePartCode;
            return name;
        }

        /// <summary>
        /// 根据DevicePartCode查找DevicePartID
        /// </summary>
        public static string GetDevicePartIDByPartCode(string _DevicePartCode)
        {
            string id = "";
            foreach (SYS_DevicePart item in DevicePartList)
            {
                if (item.DevicePartCode == _DevicePartCode)
                {
                    id = item.DevicePartID;
                    break;
                }
            }
            return id;
        }

        #endregion 8 设备部件

        #region 9 设备信息

        private static List<SYS_Device> _DeviceList;

        public static List<SYS_Device> DeviceList
        {
            get
            {
                if (_DeviceList == null)
                {
                    SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
                    _DeviceList = deviceRepository.GetList("");
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
            foreach (SYS_Device item in DeviceList)
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
            foreach (SYS_Device item in DeviceList)
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

        private static List<SYS_DeviceType> _DeviceTypeList;

        public static List<SYS_DeviceType> DeviceTypeList
        {
            get
            {
                if (_DeviceTypeList == null)
                {
                    SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();
                    _DeviceTypeList = deviceTypeRepository.GetList("");
                }
                return _DeviceTypeList;
            }
        }

        /// <summary>
        /// 根据DeviceTypeCode查找DeviceTypeID
        /// </summary>
        public static string DeviceTypeIDByCode(string _DeviceTypeCode)
        {
            string id = "";
            foreach (SYS_DeviceType item in DeviceTypeList)
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
                    _devicePartCode = CarbonScales;   //炭黑秤
                    break;

                case DevicePartType.WhiteCarbon:
                    _devicePartCode = WCarbonScales;   //白炭黑秤
                    break;

                case DevicePartType.Oil:
                    _devicePartCode = OilScales;   //油料秤
                    break;

                case DevicePartType.DrugMixer:
                    _devicePartCode = DrugScales;  //小药秤
                    break;

                case DevicePartType.DrugMixer2:
                    _devicePartCode = DrugScales2;  //小药秤2
                    break;

                case DevicePartType.MixUp:
                    _devicePartCode = MixerParts;   // 上密炼机
                    break;

                case DevicePartType.Rubber:
                    _devicePartCode = RubberScales;  // 胶料秤
                    break;

                case DevicePartType.Zno:   //  粉料秤
                    _devicePartCode = ZnoScales;
                    break;

                case DevicePartType.Plasticizer:   //  塑解剂秤
                    _devicePartCode = PlaScales;
                    break;

                case DevicePartType.Silane:   //  硅烷秤
                    _devicePartCode = SiScales;
                    break;

                case DevicePartType.MixDown:   //下密炼机
                    _devicePartCode = DownMixers;
                    break;

                case DevicePartType.Oil2:
                    _devicePartCode = OilScales2;
                    break;

                case DevicePartType.Zon2:
                    _devicePartCode = ZnoScales2;
                    break;
            }

            return _devicePartCode;
        }

        #endregion 11 获取设备部件编码

        #region 12 获取是否在调试模式状态 运行为true,调试为false,mr_step

        public static bool bDebugFlag;

        #endregion 12 获取是否在调试模式状态 运行为true,调试为false,mr_step

        #region 动作控制方式

        private static List<SYS_ActionControl> _actionControlList;

        public static List<SYS_ActionControl> ActionControlList
        {
            get
            {
                if (_actionControlList == null)
                {
                    SYS_ActionControlRepository actionControlRepository = new SYS_ActionControlRepository();
                    _actionControlList = actionControlRepository.GetList(" Enable=1 ");
                }
                return _actionControlList;
            }
        }

        #endregion 动作控制方式

        #region 密炼工艺步骤

        private static List<SYS_ActionStep> _actionStepList;

        public static List<SYS_ActionStep> ActionStepList
        {
            get
            {
                if (_actionStepList == null)
                {
                    SYS_ActionStepRepository actionStepRepository = new SYS_ActionStepRepository();
                    _actionStepList = actionStepRepository.GetList(" Enable=1 ");
                }
                return _actionStepList;
            }
        }

        #endregion 密炼工艺步骤

        #region 系统工艺参数

        private static List<SYS_TechParam> _techParamList;

        public static List<SYS_TechParam> TechParamList
        {
            get
            {
                if (_techParamList == null)
                {
                    SYS_TechParamFRepository techParamRepository = new SYS_TechParamFRepository();
                    _techParamList = techParamRepository.GetList(" Enable = 1 ");
                }
                return _techParamList;
            }
        }

        #endregion 系统工艺参数

        #region 用户信息

        private static List<TB_UserInfo> _userInfoList;

        public static List<TB_UserInfo> UserInfoList
        {
            get
            {
                if (_userInfoList == null)
                {
                    TB_UserInfoRepository userInfoRepository = new TB_UserInfoRepository();
                    _userInfoList = userInfoRepository.GetList("");
                }
                return _userInfoList;
            }
        }

        private static List<TB_Role> _tB_Roles;

        public static List<TB_Role> TB_Roles
        {
            get
            {
                if (_tB_Roles == null)
                {
                    _tB_Roles = roleRepository.GetList("");
                }
                return _tB_Roles;
            }
        }

        #endregion 用户信息

        private static string alarm_info = "";

        public static string AlarmInfo
        {
            get
            {
                return alarm_info;
            }
            set
            {
                alarm_info = value;
            }
        }

        private static string _now_mix_materialID;
        private static string _now_mix_orderID;
        private static string _now_mix_orderName;

        // 默认就是 密炼的
        public static string Now_MaterialID
        {
            get
            {
                return _now_mix_materialID;
            }
            set
            {
                _now_mix_materialID = value;
            }
        }

        public static string Now_OrderID
        {
            get
            {
                return _now_mix_orderID;
            }
            set
            {
                _now_mix_orderID = value;
            }
        }

        public static string Now_OrderName
        {
            get
            {
                return _now_mix_orderName;
            }
            set
            {
                _now_mix_orderName = value;
            }
        }

        private static string _now_weight_materialID;
        private static string _now_weight_orderID;
        private static string _now_weight_orderName;

        public static string Now_Weight_MaterialID
        {
            get
            {
                return _now_weight_materialID;
            }
            set
            {
                _now_weight_materialID = value;
            }
        }

        public static string Now_Weight_OrderID
        {
            get
            {
                return _now_weight_orderID;
            }
            set
            {
                _now_weight_orderID = value;
            }
        }

        public static string Now_Weight_OrderName
        {
            get
            {
                return _now_weight_orderName;
            }
            set
            {
                _now_weight_orderName = value;
            }
        }

        private static string _next_Mix_orderName = "No Recipe.";

        public static string Next_Mix_orderName
        {
            get
            {
                return _next_Mix_orderName;
            }
            set
            {
                _next_Mix_orderName = value;
            }
        }

        /// <summary>
        /// 下个配方名称
        /// </summary>
        public static string Next_OrderName
        {
            get; set;
        }

        /// <summary>
        /// 开卸料门
        /// </summary>
        public static int OpenDropDoorIndex
        {
            get; set;
        }

        /// <summary>
        /// 开卸料门(下)
        /// </summary>
        public static int OpenDropDoorIndexF
        {
            get; set;
        }

        public static Form FmMain
        {
            get; set;
        }

        public static bool MonitorShowed
        {
            get; set;
        }

        public static bool SendFlag
        {
            get; set;
        }

        public static Form FmMonitor
        {
            get; set;
        }

        public static CaptureVideoFFpeng CaptureVideo
        {
            get; set;
        }

        public static UserControl UserMonitor
        {
            get; set;
        }

        public static Form FmJL
        {
            get; set;
        }

        public static ResourceManager LanguagResourceManager
        {
            get; set;
        }

        public static System.Globalization.CultureInfo ZhCNCulture = new System.Globalization.CultureInfo("zh-CN", true);
        public static System.Globalization.CultureInfo EnUSCulture = new System.Globalization.CultureInfo("en-US", true);

        public static string SupportLanguage
        {
            get; set;
        }

        public static string GetRes(string str)
        {
            return LanguagResourceManager.GetString(str);
        }

        public static NLog.Logger LogCat(string fileName)
        {
            NLog.Logger logger = NLog.LogManager.GetLogger(fileName);
            return logger;
        }

        //1为单机版 2为网络版
        public static int VersionFlag = 1;
    }
}