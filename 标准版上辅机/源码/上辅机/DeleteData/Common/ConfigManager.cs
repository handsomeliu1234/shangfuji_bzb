using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DeleteData
{
    public class ConfigManager
    {
        public readonly string SoftConfigPath = FunClass.CurrentPath + "\\SoftConfig.xml";
       // public readonly string SoftConfigPath = "\\SoftConfig.xml";
        XmlHelper xmlHelp = new XmlHelper();
        public ConfigManager()
        {
            
        }

        private string _reportdbname;
        public string ReportDbName
        {
            get
            {
                if (string.IsNullOrEmpty(_reportdbname))
                {
                    XmlHelper help = new XmlHelper();
                    System.Xml.XmlNode node = ReadXml("Config//NewuSoftData");

                    _reportdbname = node.Attributes["DB"].Value.ToString();
                }

                return _reportdbname;
            }
            set { _reportdbname = value; }
        }

        private string _maindbname;
        public string MainDbName
        {
            get
            {
                if (string.IsNullOrEmpty(_maindbname))
                {
                    XmlHelper help = new XmlHelper();
                    System.Xml.XmlNode node = ReadXml("Config//NewuAutomation");

                    _maindbname = node.Attributes["DB"].Value.ToString();
                }

                return _maindbname;
            }
            set { _maindbname = value; }
        }



        public XmlNode GetAttributeValue(string path)
        {
            System.Xml.XmlNode node = ReadXml(path);
            return node;
        }       
        public void SetAttributeValue(string path, string Attribute,string value)
        {
            XmlDocument docXml = new XmlDocument();
            docXml.Load(SoftConfigPath);
            XmlNode xn = docXml.SelectSingleNode(path);
            xn.Attributes[Attribute].Value = value;
            docXml.Save(SoftConfigPath);    
        }
        private System.Xml.XmlNode ReadXml(string path)
        {
            System.Xml.XmlNode node = xmlHelp.ReadXmlNode(SoftConfigPath, path);
            return node;
        }

        private void WriteXml(string path, string value)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(SoftConfigPath);
            objXmlDoc.SelectSingleNode(path).FirstChild.Value = value;
            objXmlDoc.Save(SoftConfigPath);
        }
        /// <summary>
        /// 重载 WriteXml方法
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="ConfigPath"></param>
        private void WriteXml(string path, string value, string ConfigPath)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(ConfigPath);
            objXmlDoc.SelectSingleNode(path).FirstChild.Value = value;
            objXmlDoc.Save(ConfigPath);
        }
    }
}
