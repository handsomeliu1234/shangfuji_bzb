using Repository.GlobalConfig;
using System;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace MultiLanguage
{
    /// <summary>
    /// 语言切换窗体
    /// </summary>
    public partial class FM_LanguageSwitch : Form, ILanguageChanged
    {
        public FM_LanguageSwitch()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            string lang = cmbLang.SelectedItem as string;
            SupportLanguageType langEnum = (SupportLanguageType)Enum.Parse(typeof(SupportLanguageType), lang);
            switch (langEnum)
            {
                case SupportLanguageType.Chinese:
                    if (!NewuGlobal.SoftConfig.Language.Equals(SupportLanguageType.Chinese.ToString()))
                    {
                        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("zh-CN", true);
                        culture.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
                        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                        System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                        NewuGlobal.SupportLanguage = "1";
                    }
                    break;

                case SupportLanguageType.English:
                    if (!NewuGlobal.SoftConfig.Language.Equals(SupportLanguageType.English.ToString()) || NewuGlobal.SoftConfig.Language.Equals(SupportLanguageType.English.ToString()))
                    {
                        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US", true);
                        culture.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
                        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                        System.Globalization.CultureInfo culture1 = new System.Globalization.CultureInfo("zh-CN", true);
                        System.Threading.Thread.CurrentThread.CurrentCulture = culture1;
                        NewuGlobal.SupportLanguage = "2";
                    }
                    break;

                case SupportLanguageType.Vietnamese:
                    if (!NewuGlobal.SoftConfig.Language.Equals(SupportLanguageType.Vietnamese.ToString()))
                    {
                        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN", true);
                        culture.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
                        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                        System.Globalization.CultureInfo culture1 = new System.Globalization.CultureInfo("zh-CN", true);
                        System.Threading.Thread.CurrentThread.CurrentCulture = culture1;
                        NewuGlobal.SupportLanguage = "3";
                    }
                    break;

                case SupportLanguageType.Thai:
                    if (!NewuGlobal.SoftConfig.Language.Equals(SupportLanguageType.Thai.ToString()))
                    {
                        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("th-TH", true);
                        culture.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
                        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                        System.Globalization.CultureInfo culture1 = new System.Globalization.CultureInfo("zh-CN", true);
                        System.Threading.Thread.CurrentThread.CurrentCulture = culture1;

                        NewuGlobal.SupportLanguage = "4";
                    }
                    break;

                default:
                    break;
            }

            LanguageChanged(langEnum);
            ILanguageChanged f = NewuGlobal.FmMain as ILanguageChanged;
            ILanguageChanged fm_JL = NewuGlobal.FmJL as ILanguageChanged;

            ILanguageChanged fmMonitor = NewuGlobal.FmMonitor as ILanguageChanged;
            ILanguageChanged userMonitor = NewuGlobal.UserMonitor as ILanguageChanged;
            if (f != null)
            {
                if (!NewuGlobal.SoftConfig.Language.Equals(langEnum.ToString()))
                {
                    userMonitor.LanguageChanged(langEnum);
                    f.LanguageChanged(langEnum);
                    fmMonitor.LanguageChanged(langEnum);
                }
            }
            if (Screen.AllScreens.Length > 1)
            {
                if (fm_JL != null)
                {
                    if (!NewuGlobal.SoftConfig.Language.Equals(langEnum.ToString()))
                    {
                        fm_JL.LanguageChanged(langEnum);
                    }
                }
            }
            NewuGlobal.SoftConfig.SetLanguage(lang);
           
            // 调用报警的那个
            SaveAlarmUtil.GetInstance().PrepareData(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_LanguageSwitch_Load(object sender, EventArgs e)
        {
            cmbLang.DataSource = Enum.GetNames(typeof(SupportLanguageType)).ToList();
            cmbLang.SelectedItem = NewuGlobal.SoftConfig.Language;
            SupportLanguageType langEnum = (SupportLanguageType)Enum.Parse(typeof(SupportLanguageType), NewuGlobal.SoftConfig.Language);
            LanguageChanged(langEnum);
        }

        /// <summary>
        /// 语言切换
        /// </summary>
        /// <param name="language"></param>
        public void LanguageChanged(SupportLanguageType language)
        {
            this.Text = NewuGlobal.LanguagResourceManager.GetString("000120") + NewuGlobal.LanguagResourceManager.GetString("000121");
            this.label1.Text = NewuGlobal.LanguagResourceManager.GetString("000120") + ":";
            this.btOk.Text = NewuGlobal.LanguagResourceManager.GetString("000121");
            this.btCancel.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btOk.Padding = new Padding(0, 0, 7, 0);
                btCancel.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btOk.Padding = new Padding(0, 0, 0, 0);
                btCancel.Padding = new Padding(0, 0, 0, 0);
            }
        }
    }
}