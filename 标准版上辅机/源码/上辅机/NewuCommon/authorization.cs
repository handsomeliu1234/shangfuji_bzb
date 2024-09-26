using System;

using System.Xml;

namespace NewuCommon
{
    public class Authorization
    {
        public readonly string SoftConfigPath = FunClass.CurrentPath + "\\SoftConfig.xml";
        private string _SystemPara;

        private XmlNode ReadXml(string path)
        {
            XmlHelper help = new XmlHelper();
            XmlNode node = help.ReadXmlNode(SoftConfigPath, path);
            return node;
        }

        public string SystemPara
        {
            get
            {
                if (string.IsNullOrEmpty(_SystemPara))
                {
                    XmlNode node = ReadXml("Config//SystemPara");

                    _SystemPara = node.InnerText;
                }

                return _SystemPara;
            }
        }

        public Authorization()
        {
        }

        /// <summary>
        /// 判断是否在授权日期内
        /// </summary>
        /// <returns></returns>
        public bool JudgeAuthorization()
        {
            try
            {
                double nowTime = DateTime.Now.ToOADate();
                if (nowTime < double.Parse(SystemPara))
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
        public bool UpDataParam(int day)
        {
            try
            {
                double nowTime = DateTime.Now.ToOADate();
                string str1 = DateTime.Now.ToOADate().ToString();
                double wings = double.Parse(str1);
                string str = ((int)wings + day).ToString();

                XmlDocument doc = new XmlDocument();
                doc.Load(SoftConfigPath);
                XmlNode xn = doc.SelectSingleNode("//SystemPara");
                string transDate = xn.InnerText;
                xn.InnerText = str;
                doc.Save(SoftConfigPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}