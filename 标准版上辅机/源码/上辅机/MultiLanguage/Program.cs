using Repository.GlobalConfig;
using System;
using System.Resources;
using System.Windows.Forms;

namespace MultiLanguage
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string lang = NewuGlobal.SoftConfig.Language;
            SupportLanguageType langEnum = (SupportLanguageType)Enum.Parse(typeof(SupportLanguageType), lang);
            switch (langEnum)
            {
                case SupportLanguageType.Chinese:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
                    break;

                case SupportLanguageType.English:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    break;

                case SupportLanguageType.Vietnamese:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
                    break;

                default:
                    break;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FM_LanguageSwitch());
        }
    }
}