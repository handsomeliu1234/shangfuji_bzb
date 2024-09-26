using NewuCommon;
using System;
using System.Web.UI.WebControls;
using System.Xml;

namespace Repository.GlobalConfig
{
    public class LoadConfig
    {
        public readonly string SoftConfigPath = FunClass.CurrentPath + "\\SoftConfig.xml";

        public LoadConfig()
        {
            //设置设备
            XmlNode node = ReadXml("Config//SystemPara");
            SystemPara = node.FirstChild.Value;

            node = ReadXml("Config//UnUseTime");
            UnUseTime = decimal.Parse(node.FirstChild.Value);

            node = ReadXml("Config//PLC");
            PLC_IP = node.Attributes["IP"].Value.ToString();

            node = ReadXml("Config//LocalPC");
            PCIP = node.Attributes["IP"].Value.ToString();
            ListenerPort_R = node.Attributes["listenerPort_R"].Value.ToString();
            ListenerPort_D = node.Attributes["listenerPort_D"].Value.ToString();
            ListenerPort_O = node.Attributes["listenerPort_O"].Value.ToString();
            ListenerPort_ZnO = node.Attributes["listenerPort_ZnO"].Value.ToString();
            ListenerPort_C = node.Attributes["listenerPort_C"].Value.ToString();
            ListenerPort_C2 = node.Attributes["listenerPort_C2"].Value.ToString();
            ListenerPort_D2 = node.Attributes["listenerPort_D2"].Value.ToString();
            ListenerPort_O2 = node.Attributes["listenerPort_O2"].Value.ToString();

            node = ReadXml("Config//Worker");
            WorkGroup = node.Attributes["group"].Value.ToString();
            WorkOrder = node.Attributes["order"].Value.ToString();

            WorkGroupSet = node.Attributes["GroupSet"].Value.ToString();
            WorkOrderSet = node.Attributes["OrderSet"].Value.ToString();

            node = ReadXml("Config//AutoCheckWeightSet");
            WeightSet = node.Attributes["WeightSet"].Value.ToString();
            AllowError = node.Attributes["AllowError"].Value.ToString();

            node = ReadXml("Config//Monitor");
            MonitorView = node.FirstChild.Value;

            node = ReadXml("Config//Language");
            Language = node.FirstChild.Value;

            node = ReadXml("Config//CarbonDigit");
            CarbonDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//OilDigit");
            OilDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//RubberDigit");
            RubberDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//ZnoDigit");
            ZnoDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//SilaneDigit");
            SilaneDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//PlaDigit");
            PlaDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//DrugDigit");
            DrugDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//TempDigit");
            TempDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//EnergyDigit");
            EnergyDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//SpeedDigit");
            SpeedDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//TempValueScale");
            TempValueScale = int.Parse(node.FirstChild.Value);
            node = ReadXml("Config//SpeedValueScale");
            SpeedValueScale = int.Parse(node.FirstChild.Value);
            node = ReadXml("Config//RamValueScale");
            RamValueScale = int.Parse(node.FirstChild.Value);
            node = ReadXml("Config//PowerValueScale");
            PowerValueScale = int.Parse(node.FirstChild.Value);
            node = ReadXml("Config//EnergyValueScale");
            EnergyValueScale = int.Parse(node.FirstChild.Value);
            node = ReadXml("Config//PressValueScale");
            PressValueScale = int.Parse(node.FirstChild.Value);
            node = ReadXml("Config//VoltageValueScale");
            VoltageValueScale = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//UseLowerLimit");
            if (node.FirstChild.Value.Equals("true") || node.FirstChild.Value.Equals("True"))
            {
                UseLowerLimit = true;
            }
            else
            {
                UseLowerLimit = false;
            }

            node = ReadXml("Config//PressDigit");
            PressDigit = int.Parse(node.FirstChild.Value);

            node = ReadXml("Config//manualWeight");
            if (node.FirstChild.Value.Equals("true") || node.FirstChild.Value.Equals("True"))
            {
                ManualWeight = true;
            }
            else
            {
                ManualWeight = false;
            }

            node = ReadXml("Config//AutoScaleCheck");
            if (node.FirstChild.Value.Equals("true") || node.FirstChild.Value.Equals("True"))
            {
                AutoScaleCheck = true;
            }
            else
            {
                AutoScaleCheck = false;
            }

            node = ReadXml("Config//IsContinue");
            if (node.FirstChild.Value.Equals("true") || node.FirstChild.Value.Equals("True"))
            {
                IsContinue = true;
            }
            else
            {
                IsContinue = false;
            }

            node = ReadXml("Config//IsShowPartBatch");
            if (node.FirstChild.Value.Equals("true") || node.FirstChild.Value.Equals("True"))
            {
                IsShowPartBatch = true;
            }
            else
            {
                IsShowPartBatch = false;
            }

            node = ReadXml("Config//Scanner");
            if (node.Attributes["RubUSE"].Value == "True" || node.Attributes["RubUSE"].Value == "true")
            {
                RubScanner = true;
            }
            else
            {
                RubScanner = false;
            }
            if (node.Attributes["DrugUSE"].Value == "True" || node.Attributes["DrugUSE"].Value == "true")
            {
                DrugScanner = true;
            }
            else
            {
                DrugScanner = false;
            }
            if (node.Attributes["OilUSE"].Value == "True" || node.Attributes["OilUSE"].Value == "true")
            {
                OilScanner = true;
            }
            else
            {
                OilScanner = false;
            }
            if (node.Attributes["ZnoUSE"].Value == "True" || node.Attributes["ZnoUSE"].Value == "true")
            {
                ZnOScanner = true;
            }
            else
            {
                ZnOScanner = false;
            }
            if (node.Attributes["CarbonUSE"].Value == "True" || node.Attributes["CarbonUSE"].Value == "true")
            {
                CarBonScanner = true;
            }
            else
            {
                CarBonScanner = false;
            }

            if (node.Attributes["CarbonUSE2"].Value == "True" || node.Attributes["CarbonUSE2"].Value == "true")
            {
                CarBonScanner2 = true;
            }
            else
            {
                CarBonScanner2 = false;
            }

            if (node.Attributes["DrugUSE2"].Value == "True" || node.Attributes["DrugUSE2"].Value == "true")
            {
                DrugScanner2 = true;
            }
            else
            {
                DrugScanner2 = false;
            }
            if (node.Attributes["OilUSE2"].Value == "True" || node.Attributes["OilUSE2"].Value == "true")
            {
                OilScanner2 = true;
            }
            else
            {
                OilScanner2 = false;
            }

            if (FunClass.VVal(node.Attributes["ADDR"].Value) != 0)
            {
                ScannerAddr = FunClass.VVal(node.Attributes["ADDR"].Value);
            }

            node = ReadXml("Config//DevicePart");
            if (node.Attributes["Carbon"].Value == "True" || node.Attributes["Carbon"].Value == "true")
            {
                Carbon = true;
            }
            else
            {
                Carbon = false;
            }

            if (node.Attributes["Rubber"].Value == "True" || node.Attributes["Rubber"].Value == "true")
            {
                Rubber = true;
            }
            else
            {
                Rubber = false;
            }

            if (node.Attributes["Oil"].Value == "True" || node.Attributes["Oil"].Value == "true")
            {
                Oil = true;
            }
            else
            {
                Oil = false;
            }

            if (node.Attributes["Zno"].Value == "True" || node.Attributes["Zno"].Value == "true")
            {
                Zno = true;
            }
            else
            {
                Zno = false;
            }

            if (node.Attributes["Drug"].Value == "True" || node.Attributes["Drug"].Value == "true")
            {
                Drug = true;
            }
            else
            {
                Drug = false;
            }

            if (node.Attributes["Pla"].Value == "True" || node.Attributes["Pla"].Value == "true")
            {
                Pla = true;
            }
            else
            {
                Pla = false;
            }

            if (node.Attributes["Silane"].Value == "True" || node.Attributes["Silane"].Value == "true")
            {
                Silane = true;
            }
            else
            {
                Silane = false;
            }

            if (node.Attributes["DownMixer"].Value == "True" || node.Attributes["DownMixer"].Value == "true")
                DownMixer = true;
            else
                DownMixer = false;

            if (node.Attributes["Carbon2"].Value == "True" || node.Attributes["Carbon2"].Value == "true")
                Carbon2 = true;
            else
                Carbon2 = false;

            if (node.Attributes["Oil2"].Value == "True" || node.Attributes["Oil2"].Value == "true")
                Oil2 = true;
            else
                Oil2 = false;

            if (node.Attributes["Drug2"].Value == "True" || node.Attributes["Drug2"].Value == "true")
                Drug2 = true;
            else
                Drug2 = false;

            if (node.Attributes["Zno2"].Value == "True" || node.Attributes["Zno2"].Value == "true")
                Zno2 = true;
            else
                Zno2 = false;

            node = ReadXml("Config//NewuAutomation");
            DBMain = node.Attributes["DB"].Value.ToString();
            node = ReadXml("Config//NewuSoftData");
            DBData = node.Attributes["DB"].Value.ToString();
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceID
        {
            get
            {
                if (string.IsNullOrEmpty(DeviceCode) == false)
                {
                    return NewuGlobal.DeviceIDByCode(DeviceCode);
                }
                else
                {
                    return "";
                }
            }
        }

        private string _DeviceCode;

        /// <summary>
        /// 设备编码（其实就是名称如 1#上辅机母炼系统 ）
        /// </summary>
        public string DeviceCode
        {
            get
            {
                if (string.IsNullOrEmpty(_DeviceCode))
                {
                    XmlNode node = ReadXml("Config//DeviceCode");

                    _DeviceCode = node.Attributes["Code"].Value.ToString();
                }

                return _DeviceCode;
            }
        }

        private int _mixType = -1;

        public int MixType
        {
            get
            {
                if (_mixType == -1)
                {
                    XmlNode node = ReadXml("Config//MixType");
                    if (node.Attributes["Code"].Value.ToString() == "FinalMix")
                    {
                        _mixType = 1;
                    }
                    else
                    {
                        _mixType = 0;
                    }
                }
                return _mixType;
            }
        }

        /// <summary>
        /// 机台版本（单机版还是网络版）
        /// </summary>
        private string _versionID;

        public string VersionID
        {
            get
            {
                XmlNode xn = ReadXml("Config//Version");
                _versionID = xn.FirstChild.Value;
                return _versionID;
            }
            set
            {
                WriteXml("Config//Version", value);
                _versionID = value;
            }
        }

        /// <summary>
        /// 导出文件的盘符路径
        /// </summary>
        private string _exportpath;

        public string ExportPath
        {
            get
            {
                XmlNode xn = ReadXml("Config//ExportPath");
                _exportpath = xn.FirstChild.Value;
                return _exportpath;
            }
            set
            {
                WriteXml("Config//ExportPath", value);
                _exportpath = value;
            }
        }

        /// <summary>
        /// 软件版本
        /// </summary>
        private string _softwareversion;

        public string SoftwareVersion
        {
            get
            {
                XmlNode xn = ReadXml("Config//SoftWareVersion");
                _softwareversion = xn.FirstChild.Value;
                return _softwareversion;
            }
            set
            {
                WriteXml("Config//SoftWareVersion", value);
                _softwareversion = value;
            }
        }

        /// <summary>
        /// 数据库表是否清理
        /// </summary>
        private bool _dbcleanenable;

        public bool DBCleanEnable
        {
            get
            {
                XmlNode xn = ReadXml("Config//DBClean");
                if (xn.Attributes["Enable"].Value == "true" || xn.Attributes["Enable"].Value == "True")
                {
                    _dbcleanenable = true;
                }
                else
                {
                    _dbcleanenable = false;
                }
                return _dbcleanenable;
            }
            set
            {
                SetAttributeValue("Config//DBClean", "Enable", value.ToString());
                _dbcleanenable = value;
            }
        }

        //删除日志天数
        private int _days = 0;

        public int DeleteFileLogDays
        {
            get
            {
                System.Xml.XmlNode node = ReadXml("Config//DeleteFileLogDays");
                _days = FunClass.VVal(node.Attributes["Day"].Value);
                return _days;
            }
        }

        //删除日志时间
        private string _time = "";

        public string DeleteFileLogTime
        {
            get
            {
                System.Xml.XmlNode node = ReadXml("Config//DeleteFileLogDays");
                _time = FunClass.VStr(node.Attributes["Time"].Value);
                return _time;
            }
        }

        /// <summary>
        /// 数据库表删除年限
        /// </summary>
        private int _dbcleanyear;

        public int DBCleanYear
        {
            get
            {
                XmlNode xn = ReadXml("Config//DBClean");
                if (FunClass.VVal(xn.Attributes["Year"].Value) != 0)
                {
                    _dbcleanyear = FunClass.VVal(xn.Attributes["Year"].Value);
                }
                return _dbcleanyear;
            }
            set
            {
                SetAttributeValue("Config//DBClean", "Year", value.ToString());
                _dbcleanyear = value;
            }
        }

        /// <summary>
        /// 是终炼 返回true 否则返回false
        /// </summary>
        /// <returns></returns>
        public bool IsFinalMix()
        {
            return MixType != 0;
        }

        public XmlNode GetAttributeValue(string path)
        {
            XmlNode node = ReadXml(path);
            return node;
        }

        public void SetAttributeValue(string path, string Attribute, string value)
        {
            XmlDocument docXml = new XmlDocument();
            docXml.Load(SoftConfigPath);

            XmlNode xn = docXml.SelectSingleNode(path);
            xn.Attributes[Attribute].Value = value;
            docXml.Save(SoftConfigPath);
        }

        private XmlNode ReadXml(string path)
        {
            XmlHelper help = new XmlHelper();
            XmlNode node = help.ReadXmlNode(SoftConfigPath, path);
            return node;
        }

        #region 属性

        public string SystemPara
        {
            get; set;
        }

        public decimal UnUseTime
        {
            get; set;
        }

        /// <summary>
        /// plc通讯地址
        /// </summary>
        public string PLC_IP
        {
            get; set;
        }

        public string PCIP
        {
            get; set;
        }

        public bool RubScanner
        {
            get; set;
        }

        public bool DrugScanner
        {
            get; set;
        }

        public bool OilScanner
        {
            get; set;
        }

        public bool ZnOScanner
        {
            get; set;
        }

        public bool CarBonScanner
        {
            get; set;
        }

        public bool CarBonScanner2
        {
            get; set;
        }

        public bool DrugScanner2
        {
            get; set;
        }

        public bool OilScanner2
        {
            get; set;
        }

        public int ScannerAddr
        {
            get; set;
        }

        public string ListenerPort_R
        {
            get; set;
        }

        public string ListenerPort_D
        {
            get; set;
        }

        public string ListenerPort_D2
        {
            get; set;
        }

        public string ListenerPort_O
        {
            get; set;
        }

        public string ListenerPort_O2
        {
            get; set;
        }

        public string ListenerPort_ZnO
        {
            get; set;
        }

        public string ListenerPort_C
        {
            get; set;
        }

        public string ListenerPort_C2
        {
            get; set;
        }

        public string WorkGroup
        {
            get; set;
        }

        public string WorkOrder
        {
            get; set;
        }

        public string WorkGroupSet
        {
            get; set;
        }

        public string WorkOrderSet
        {
            get; set;
        }

        public bool AutoScaleCheck
        {
            get; set;
        }

        public bool IsContinue
        {
            get; set;
        }

        public bool IsShowPartBatch
        {
            get; set;
        }

        public bool ManualWeight
        {
            get; set;
        }

        public bool Carbon
        {
            get; set;
        }

        public bool Rubber
        {
            get; set;
        }

        public bool Oil
        {
            get; set;
        }

        public bool Zno
        {
            get; set;
        }

        public bool Drug
        {
            get; set;
        }

        public bool Pla
        {
            get; set;
        }

        public bool Silane
        {
            get; set;
        }

        public bool DownMixer
        {
            get; set;
        }

        public bool Carbon2
        {
            get; set;
        }

        public bool Oil2
        {
            get; set;
        }

        public bool Drug2
        {
            get; set;
        }

        public bool Zno2
        {
            get; set;
        }

        public string DBMain
        {
            get; set;
        }

        public string DBData
        {
            get; set;
        }

        public int CarbonDigit
        {
            get; set;
        }

        public int OilDigit
        {
            get; set;
        }

        public int RubberDigit
        {
            get; set;
        }

        public int ZnoDigit
        {
            get; set;
        }

        public int SilaneDigit
        {
            get; set;
        }

        public int PlaDigit
        {
            get; set;
        }

        public int DrugDigit
        {
            get; set;
        }

        public int TempDigit
        {
            get; set;
        }

        public int EnergyDigit
        {
            get; set;
        }

        public int SpeedDigit
        {
            get; set;
        }

        public int TempValueScale
        {
            get; set;
        }

        public int SpeedValueScale
        {
            get; set;
        }

        public int RamValueScale
        {
            get; set;
        }

        public int PowerValueScale
        {
            get; set;
        }

        public int EnergyValueScale
        {
            get; set;
        }

        public int PressValueScale
        {
            get; set;
        }

        public int VoltageValueScale
        {
            get; set;
        }

        public bool UseLowerLimit
        {
            get; set;
        }

        public int PressDigit
        {
            get; set;
        }

        public string WeightSet
        {
            get; set;
        }

        public string AllowError
        {
            get; set;
        }

        public string MonitorView
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        #endregion 属性

        public void SetPlcIP(string ip)
        {
            SetAttributeValue("Config//PLC", "IP", ip);
        }

        public void SetPCIP(string ip)
        {
            SetAttributeValue("Config//LocalPC", "IP", ip);
        }

        public void SetAutoScaleCheck(bool flag)
        {
            WriteXml("Config//AutoScaleCheck", flag.ToString());
            AutoScaleCheck = flag;
        }

        public void SetDBCleanParam(string enable, string year)
        {
            if (enable == "True" || enable == "true")
            {
                DBCleanEnable = true;
            }
            else
            {
                DBCleanEnable = false;
            }
            SetAttributeValue("Config//DBClean", "Enable", enable);
            DBCleanYear = FunClass.VVal(year);
            SetAttributeValue("Config//DBClean", "Year", year);
        }

        public void SetScannerState(bool state, string attribute)
        {
            switch (attribute)
            {
                case "RubUSE":
                    RubScanner = state;
                    break;

                case "DrugUSE":
                    DrugScanner = state;
                    break;

                case "OilUSE":
                    OilScanner = state;
                    break;

                case "ZnoUSE":
                    ZnOScanner = state;
                    break;

                case "CarbonUSE":
                    CarBonScanner = state;
                    break;

                case "CarbonUSE2":
                    CarBonScanner2 = state;
                    break;

                case "DrugUSE2":
                    DrugScanner2 = state;
                    break;

                case "OilUSE2":
                    OilScanner2 = state;
                    break;
            }
            SetAttributeValue("Config//Scanner", attribute, state.ToString());
        }

        public void SetDevicePartState(bool state, string attribute)
        {
            if (!string.IsNullOrEmpty(attribute))
                SetAttributeValue("Config//DevicePart", attribute, state.ToString());
        }

        public void SetAutoCheckParam(string weightSet, string allowError)
        {
            WeightSet = weightSet;
            AllowError = allowError;
            SetAttributeValue("Config//AutoCheckWeightSet", "WeightSet", weightSet.ToString());
            SetAttributeValue("Config//AutoCheckWeightSet", "AllowError", allowError.ToString());
        }

        public void SetWorker(string workGroup, string workOrder)
        {
            WorkGroup = workGroup;
            WorkOrder = workOrder;
            SetAttributeValue("Config//Worker", "group", workGroup);
            SetAttributeValue("Config//Worker", "order", workOrder);
        }

        public void SetWorkSetting(string workGroupSet, string workOrderSet)
        {
            WorkGroupSet = workGroupSet;
            WorkOrderSet = workOrderSet;
            SetAttributeValue("Config//Worker", "GroupSet", workGroupSet);
            SetAttributeValue("Config//Worker", "OrderSet", workOrderSet);
        }

        public void SetLanguage(string lang)
        {
            WriteXml("Config//Language", lang);
            Language = lang;
        }

        public void SetIsContinue(bool isContinue)
        {
            WriteXml("Config//IsContinue", isContinue.ToString());
            IsContinue = isContinue;
        }

        private void WriteXml(string path, string value)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(SoftConfigPath);
            objXmlDoc.SelectSingleNode(path).FirstChild.Value = value;
            objXmlDoc.Save(SoftConfigPath);
        }

        public void SetSystemPara(string systemPara)
        {
            WriteXml("Config//SystemPara", systemPara);
            SystemPara = systemPara;
        }
    }
}