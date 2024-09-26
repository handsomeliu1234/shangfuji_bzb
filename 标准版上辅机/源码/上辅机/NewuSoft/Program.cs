using System;
using System.Windows.Forms;
using System.Threading;
using System.Resources;
using MultiLanguage;
using Repository.GlobalConfig;
using System.Globalization;
using System.Data;

namespace NewuSoft
{
    /// <summary>
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            #region
            try
            {
                // createdNew: 在此方法返回时，如果创建了局部互斥体（即，如果 name 为 null 或空字符串）或指定的命名系统互斥体，则包含布尔值 true； 如果指定的命名系统互斥体已存在，则为false
                using (Mutex mutex = new Mutex(true, Application.ProductName, out bool createNew))
                {
                    if (createNew)
                    {
                        using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(new DbHelperSQL().ConnectionString))
                        {
                            try
                            {
                                connection.Open();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("数据库连接失败");
                                return;
                            }
                        }

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        string lang = NewuGlobal.SoftConfig.Language;
                        SupportLanguageType langEnum = (SupportLanguageType)Enum.Parse(typeof(SupportLanguageType), lang);
                        switch (langEnum)
                        {
                            case SupportLanguageType.Chinese:
                                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", true);
                                break;

                            case SupportLanguageType.English:
                                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", true);
                                break;

                            case SupportLanguageType.Vietnamese:
                                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", true);
                                break;

                            default:
                                break;
                        }
                        //加载该语言下的资源文件
                        ResourceManager rm = new ResourceManager("MultiLanguage.Lang", typeof(FM_LanguageSwitch).Assembly);
                        NewuGlobal.LanguagResourceManager = rm;

                        //处理未捕获异常
                        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                        //处理UI线程异常
                        Application.ThreadException += Application_ThreadException;
                        //处理非UI线程异常
                        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                        Application.Run(new FmMain());
                    }
                    else
                    {
                        MessageBox.Show("上辅机程序已经在运行中...");
                        Thread.Sleep(1000);
                        // 终止此进程并为基础操作系统提供指定的退出代码。
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("启动软件").Error(ex.ToString());
            }
            #endregion
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            NewuGlobal.LogCat("Program").Error(e.ExceptionObject.ToString());
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            NewuGlobal.LogCat("Program").Error(e.Exception.ToString());
            Environment.Exit(1);
        }
    }
}