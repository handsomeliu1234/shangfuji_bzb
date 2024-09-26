using MultiLanguage;
using Newu;
using NewuCommon;
using NewuView.Mix;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsCaptureVideo;

namespace NewuSoft
{
    public partial class FmMain : Form, IShellForm, ILanguageChanged, IVersionSet
    {
        private readonly SYS_MenuRepository menuRepository = new SYS_MenuRepository();//系统菜单操作类
        private readonly RPT_AppEventRepository appEventRepository = new RPT_AppEventRepository();
        private Form monitorForm;
        private CaptureVideoFFpeng captureVideo;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private System.Timers.Timer timerLoseFocus = new System.Timers.Timer();
        private System.Timers.Timer timerGetFocus = new System.Timers.Timer();
        private FmLogin fmLogin;

        //不允许关闭的窗口数量
        private int NoCloseWindowsCount = 0;

        private FM_Rubber fm_JL;
        private FM_Rubber_Final fm_JL_Final;
        private TB_Role tB_Role;
        private bool isWorker;//是否为操作工登录
        private bool isLoginOut;//是否为注销状态
        private int loseFocusTime = 0;

        public FmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加的载入事件，先执行登陆窗口
        /// </summary>
        private async void FmMain_Load(object sender, EventArgs e)
        {
            try
            {
                NewuGlobal.FmMain = this;
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(new DbHelperSQL().ConnectionString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000819") + NewuGlobal.GetRes("000064"));
                        connection.Close();
                        return;
                    }
                }

                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(new DbHelperSQL(ConnType.NewuSoftData).ConnectionString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000820") + NewuGlobal.GetRes("000064"));
                        connection.Close();
                        return;
                    }
                }

                //第一步：加载登录窗口
                if (Login(true) == false)
                {
                    NewuToolBarMenu_ExitEvent(NewuToolBar.AppEvent.Exit);
                    return;
                }

                //显示样式
                ViewStyle();

                if (Debugger.IsAttached)
                {
                    NewuGlobal.bDebugFlag = false;
                }
                else
                {
                    NewuGlobal.bDebugFlag = true;
                }

                this.Text = NewuGlobal.GetRes("000119") + "_" + NewuGlobal.SoftConfig.SoftwareVersion;

                ChangeLanguage((SupportLanguageType)Enum.Parse(typeof(SupportLanguageType), NewuGlobal.SoftConfig.Language));

                if (NewuGlobal.SoftConfig.Language == "English")
                {
                    NewuGlobal.SupportLanguage = "2";
                }
                else if (NewuGlobal.SoftConfig.Language == "Chinese")
                {
                    NewuGlobal.SupportLanguage = "1";
                }
                else if (NewuGlobal.SoftConfig.Language == "Vietnamese")
                {
                    NewuGlobal.SupportLanguage = "3";
                }
                else if (NewuGlobal.SoftConfig.Language == "Thai")
                {
                    NewuGlobal.SupportLanguage = "4";
                }
                else
                {
                    NewuGlobal.SupportLanguage = "2";
                }

                //初始化窗口界面,会启动菜单中设置自动启动的菜单
                if (Screen.AllScreens.Length > 1)
                {
                    if (!NewuGlobal.SoftConfig.IsFinalMix())
                    {
                        fm_JL = new FM_Rubber();
                        fm_JL.Show();
                        fm_JL.Location = new Point(this.Width, 0);
                        fm_JL.Size = Screen.AllScreens[1].Bounds.Size;
                        fm_JL.FormBorderStyle = FormBorderStyle.None;
                        fm_JL.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        fm_JL_Final = new FM_Rubber_Final();
                        fm_JL_Final.Show();
                        fm_JL_Final.Location = new Point(this.Width, 0);
                        fm_JL_Final.Size = Screen.AllScreens[1].Bounds.Size;
                        fm_JL_Final.FormBorderStyle = FormBorderStyle.None;
                        fm_JL_Final.WindowState = FormWindowState.Maximized;
                    }
                }

                timer.Elapsed += Timer1_Tick;
                timer.Interval = 1000;
                timer.Enabled = true;

                timerGetFocus.Elapsed += TimerGetFocus_Elapsed;
                timerGetFocus.Interval = 1000;

                timerLoseFocus.Elapsed += TimerLoseFocus_Elapsed;
                timerLoseFocus.Interval = 1000;

                await FormShow();
                await Task.Delay(1000);
                monitorForm.Activate();

                //打开通讯程序
                StartNewuCommo();
                SaveRawMix saveRawMix = new SaveRawMix();//数据回采
                SaveCurve saveCurve = new SaveCurve();
                SaveAlarmUtil.GetInstance();  //报警信息  采集
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FmMain").Error(ex.ToString());
            }
        }

        private void StartNewuCommo()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = AppDomain.CurrentDomain.BaseDirectory + @"\newuComm\NewuCommCSharp.exe",
                WindowStyle = ProcessWindowStyle.Minimized
            };
            Process.Start(startInfo);
        }

        /// <summary>
        /// </summary>
        private async Task FormShow()
        {
            try
            {
                string strWhere = "AutoShow is not null and AutoShow = 1  order by MenuOrder desc";//1表示自动启动
                List<SYS_Menu> menuList = menuRepository.GetList(strWhere);

                if (menuList.Count == 0)
                    return;

                NoCloseWindowsCount = menuList.Count;  //不允许关闭的窗口数量为自动打开的窗体数量

                SYS_Menu module = menuList[0];
                if (module.ASSEMBLY != "" && module.NameSpaceAndClass != "")
                {
                    object obj = null;
                    Type type = Type.GetType(module.NameSpaceAndClass + "," + module.ASSEMBLY);
                    if (type == null)
                    {
                        MessageBox.Show("未找到该程序集中的类,请检查:" + module.NameSpaceAndClass);
                        return;
                    }

                    obj = Activator.CreateInstance(type);

                    if (obj != null && obj is Form)
                    {
                        monitorForm = obj as Form;
                        //存储权限信息,方便窗体中的按钮权限验证
                        monitorForm.Tag = module;//model就是SYS_Menu中的一条数据,后续可以根据其ID查询其中的按钮或者其他控件
                        monitorForm.TopMost = false;
                        monitorForm.TopLevel = false;
                        monitorForm.Dock = DockStyle.Fill;
                        IndexTabPage.Controls.Add(monitorForm);
                        monitorForm.Left = 0;
                        monitorForm.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        monitorForm.Height = IndexTabPage.Height;
                        monitorForm.WindowState = FormWindowState.Maximized;
                        monitorForm.ShowInTaskbar = false;
                        monitorForm.Show();
                        NewuGlobal.FmMonitor = monitorForm;
                        monitorForm.Deactivate += MonitorForm_Deactivate;
                        monitorForm.Activated += MonitorForm_Activated;

                        await Task.Delay(1000);

                        captureVideo = new CaptureVideoFFpeng("HistoryVideo", 1, 30);
                        captureVideo.Start(this.monitorForm.Handle, this.monitorForm);
                        NewuGlobal.CaptureVideo = captureVideo;
                        monitorForm.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FmMain").Error(ex.ToString());
            }
        }

        public bool Login(bool IsFirstLoad)
        {
            try
            {
                if (IsFirstLoad)
                    this.Hide();

                fmLogin = new FmLogin();
                if (IsFirstLoad)
                {
                    fmLogin.ControlBox = false;
                }

                if (fmLogin.ShowDialog() == DialogResult.OK)
                {
                    if (IsFirstLoad)
                        this.Show();

                    toolStripStatusUser.Text = NewuGlobal.GetRes("000608") + ": " + NewuGlobal.TB_UserInfo.UserCode;
                    VersionSet();
                    //第二步：载入菜单、工具条
                    LoadMen();

                    ChangeLanguage((SupportLanguageType)Enum.Parse(typeof(SupportLanguageType), NewuGlobal.SoftConfig.Language));
                    if (!this.timerGetFocus.Enabled)
                    {
                        this.timerGetFocus.Enabled = true;
                    }
                    if (!this.timerLoseFocus.Enabled)
                    {
                        this.timerGetFocus.Enabled = true;
                    }
                    tB_Role = NewuGlobal.TB_Roles.Find(r => r.RoleID.Equals(NewuGlobal.TB_UserInfo.RoleID));

                    if (tB_Role != null && tB_Role.RoleName.Contains("操作"))
                        isWorker = false;
                    else
                        isWorker = true;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FmMain").Error(ex.ToString());
                return false;
            }
        }

        private void LoadMen()
        {
            //加载菜单  修改是否显示属性显示窗口
            string whereStr = "a.roleid='" + NewuGlobal.TB_UserInfo.RoleID + "' and a.Enable='1' and b.Caption<>N'工具条' and b.ControlType = '1'";
            menuStripSys.Items.Clear();
            GetData(whereStr);

            //加载工具条
            newuToolBarMenu.Items.Clear();
            newuToolBarMenu.LoadToolBar(tabControl1);
            newuToolBarMenu.ExitEvent -= new NewuToolBar.DelegateVoid(NewuToolBarMenu_ExitEvent);
            newuToolBarMenu.ExitEvent += new NewuToolBar.DelegateVoid(NewuToolBarMenu_ExitEvent);
        }

        public void GetData(string where)
        {
            try
            {
                List<SYS_Menu> sYS_Menus = menuRepository.GetLeftjoinMenu(where);
                if (sYS_Menus != null)
                {
                    menuRepository.TabControlMain = tabControl1;

                    //菜单节点
                    menuRepository.MenuTree = menuStripSys;
                    menuRepository.LoaderMenu(null, "", sYS_Menus);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FmMain").Error(ex.ToString());
            }
        }

        private async void NewuToolBarMenu_ExitEvent(NewuToolBar.AppEvent _appEventType)
        {
            switch (_appEventType)
            {
                case NewuToolBar.AppEvent.Login:
                    if (Login(false))
                    {
                        //添加切换账户关闭除首页外的所有tabControl----20230613容亚松
                        int count = 0;
                        tabControl1.SelectedIndex = count;
                        await Task.Delay(500);

                        foreach (TabPage page in tabControl1.TabPages)
                        {
                            count++;
                            if (count <= NoCloseWindowsCount)
                                continue;
                            CloseForm(page, null);
                        }
                        tB_Role = NewuGlobal.TB_Roles.Find(r => r.RoleID.Equals(NewuGlobal.TB_UserInfo.RoleID));

                        if (tB_Role != null && tB_Role.RoleName.Contains("操作"))
                            isWorker = false;
                        else
                            isWorker = true;
                        appEventRepository.Add(AppEventType.UserLogin);
                        isLoginOut = false;
                    }
                    break;

                case NewuToolBar.AppEvent.Logout:

                    //关闭已经打开的所有窗体
                    int cnt = 0;
                    tabControl1.SelectedIndex = cnt;
                    await Task.Delay(500);
                    foreach (TabPage page in tabControl1.TabPages)
                    {
                        //保留一个 主界面不关闭
                        cnt++;
                        if (cnt <= NoCloseWindowsCount)
                            continue;
                        CloseForm(page, null);
                    }

                    foreach (ToolStripItem item in newuToolBarMenu.Items)
                    {
                        if (item.ToolTipText != NewuGlobal.GetRes("000010") && item.ToolTipText != NewuGlobal.GetRes("000012"))
                        {
                            item.Enabled = false;
                        }
                    }
                    foreach (ToolStripItem item in menuStripSys.Items)
                    {
                        item.Enabled = false;
                    }

                    appEventRepository.Add(AppEventType.SystemLogOut);

                    break;

                case NewuToolBar.AppEvent.Exit:

                    if (MessageBox.Show(NewuGlobal.GetRes("000178"), "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        appEventRepository.Add(AppEventType.AppStop);
                        try
                        {
                            Environment.Exit(0);
                        }
                        catch (Exception ex)
                        {
                            NewuGlobal.LogCat("FmMain").Error(ex.ToString());
                            Environment.Exit(0);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public void ViewStyle()
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void TabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    if (tabControl1.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        tabControl1.SelectedTab = tabControl1.TabPages[i];
                        break;
                    }
                }
                if (tabControl1.SelectedIndex < NoCloseWindowsCount)
                {
                    return;
                }

                Point p = e.Location;
                p.X = p.X;
                p.Y = tabControl1.Location.Y + menuStripSys.Height + newuToolBarMenu.Height;
                contextMenuStrip1.Show(this, p);
            }
        }

        //在选项卡上关闭窗口操作
        private void MenuItemClosePage_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage page = tabControl1.SelectedTab;
                if (tabControl1.SelectedIndex < NoCloseWindowsCount)
                {
                    return;
                }
                ClosePage(page);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Main").Error(ex.ToString());
            }
        }

        /// <summary>
        ///切换用户关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CloseForm(object sender, MouseEventArgs e)
        {
            try
            {
                TabPage page = sender as TabPage;
                ClosePage(page);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Main").Error(ex.ToString());
            }
        }

        private void ClosePage(TabPage page)
        {
            foreach (Control item in page.Controls)
            {
                if (item is Form)
                {
                    Form fm = item as Form;
                    fm.Close();
                    fm.Dispose();
                }
            }
        }

        //双击关闭选项卡
        private void TabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedIndex < NoCloseWindowsCount)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    if (tabControl1.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        CloseForm(tabControl1.TabPages[i], null);
                        break;
                    }
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Action ac = new Action(GetTime);
                BeginInvoke(ac);
            }
            else
            {
                GetTime();
            }
        }

        private void GetTime()
        {
            string time = DateTime.Now.ToLocalTime().ToString();
            this.toolStripStatusTime.Text = "Time:" + " " + time;
        }

        public Form GetFormByClassName(string formName)
        {
            foreach (TabPage tp in this.tabControl1.TabPages)
            {
                foreach (Control ct in tp.Controls)
                {
                    if (ct is Form)
                    {
                        Type t = ct.GetType();
                        if (formName == t.FullName)
                        {
                            return ct as Form;
                        }
                    }
                }
            }
            return null;
        }

        public TabControl GetTabControl()
        {
            return this.tabControl1;
        }

        #region 语言切换

        /// <summary>
        /// 语言切换
        /// </summary>
        /// <param name="language"></param>
        public void LanguageChanged(SupportLanguageType language)
        {
            ChangeLanguage(language);
        }

        /// <summary>
        /// 语言切换具体方法
        /// </summary>
        /// <param name="languageCode"></param>
        private void ChangeLanguage(SupportLanguageType language)
        {
            //软件名称  修改
            this.Text = NewuGlobal.GetRes("000119") + "_" + NewuGlobal.SoftConfig.SoftwareVersion;

            //菜单
            foreach (ToolStripMenuItem item in this.menuStripSys.Items)
            {
                SwitchMenuLanguage(item);
            }

            //工具栏
            foreach (ToolStripItem item in this.newuToolBarMenu.Items)
            {
                if (item is ToolStripButton)
                {
                    SYS_Menu sysMenu = item.Tag as SYS_Menu;
                    if (sysMenu != null)
                    {
                        if (sysMenu.Reserve1 != "")
                        {
                            //item.Text 看情况使用
                            item.ToolTipText = NewuGlobal.LanguagResourceManager.GetString(sysMenu.Reserve1);
                        }
                    }
                }
            }

            //page页
            foreach (TabPage tp in tabControl1.TabPages)
            {
                SYS_Menu sysMenu = tp.Tag as SYS_Menu;
                if (sysMenu != null)
                {
                    if (sysMenu.Reserve1 != "")
                    {
                        tp.Text = NewuGlobal.LanguagResourceManager.GetString(sysMenu.Reserve1);
                    }
                }
                else
                {
                    //首页
                    tp.Text = NewuGlobal.LanguagResourceManager.GetString("000122");
                }
            }
            //当前page页内子窗体  SelectedTab
            foreach (TabPage a in tabControl1.TabPages)
            {
                foreach (Control ct in a.Controls)
                {
                    if (ct is ILanguageChanged)
                    {
                        ILanguageChanged f = ct as ILanguageChanged;
                        f.LanguageChanged(language);
                    }
                }
            }
            toolStripStatusUser.Text = NewuGlobal.GetRes("000608") + ": " + NewuGlobal.TB_UserInfo.UserCode;
            VersionSet();
        }

        /// <summary>
        /// 递归遍历循环菜单翻译
        /// </summary>
        private void SwitchMenuLanguage(ToolStripMenuItem item)
        {
            if (item == null)
            {
                return;
            }
            //此处赋值是在NewuBLL.NewuToolBarBLL中的LoadToolBar方法中的GetLeftjoinMenu方法中 lzc20200701
            SYS_Menu sysMenu = item.Tag as SYS_Menu;
            if (sysMenu != null)
            {
                if (sysMenu.Reserve1 != "")
                {
                    item.Text = NewuGlobal.LanguagResourceManager.GetString(sysMenu.Reserve1);
                }
            }
            //根据资源id获取语言
            if (item.HasDropDownItems)
            {
                foreach (ToolStripItem it in item.DropDownItems)
                {
                    if (it is ToolStripMenuItem)  //还有分隔符菜单必须判断否则出错
                    {
                        SwitchMenuLanguage(it as ToolStripMenuItem);
                    }
                }
            }
        }

        #endregion 语言切换

        private void FmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //F7 切换
            if (e.KeyCode == Keys.F7)
            {
                ChangeScreep();
            }
        }

        public void ChangeScreep()
        {
            if (Screen.AllScreens.Length > 1)
            {
                if (!NewuGlobal.SoftConfig.IsFinalMix())
                {
                    if (fm_JL != null)
                    {
                        if (fm_JL.Location.X > 10)
                        {
                            ChangePositionScreen(new Point(0, 0), new Point(this.Width, 0));
                        }
                        else
                        {
                            ChangePositionScreen(new Point(this.Width, 0), new Point(0, 0));
                        }
                    }
                }
                else
                {
                    if (fm_JL_Final != null)
                    {
                        if (fm_JL_Final.Location.X > 10)
                        {
                            ChangePositionScreen(new Point(0, 0), new Point(this.Width, 0));
                        }
                        else
                        {
                            ChangePositionScreen(new Point(this.Width, 0), new Point(0, 0));
                        }
                    }
                }
            }
        }

        private void ChangePositionScreen(Point pointJL, Point pointMain)
        {
            if (!NewuGlobal.SoftConfig.IsFinalMix())
            {
                fm_JL.FormBorderStyle = FormBorderStyle.Sizable;
                fm_JL.WindowState = FormWindowState.Normal;
                fm_JL.Location = pointJL;
                fm_JL.WindowState = FormWindowState.Maximized;
                fm_JL.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                fm_JL_Final.FormBorderStyle = FormBorderStyle.Sizable;
                fm_JL_Final.WindowState = FormWindowState.Normal;
                fm_JL_Final.Location = pointJL;
                fm_JL_Final.WindowState = FormWindowState.Maximized;
                fm_JL_Final.FormBorderStyle = FormBorderStyle.None;
            }

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.Location = pointMain;
            this.WindowState = FormWindowState.Maximized;
        }

        private void MonitorForm_Deactivate(object sender, EventArgs e)
        {
            NewuGlobal.MonitorShowed = false;
        }

        private void MonitorForm_Activated(object sender, EventArgs e)
        {
            if (!NewuGlobal.MonitorShowed)
            {
                NewuGlobal.MonitorShowed = true;
            }
        }

        public void VersionSet()
        {
            //机台版本展示
            if (NewuGlobal.SoftConfig.VersionID == "1")
            {
                toolStripStatusVersion.Text = NewuGlobal.GetRes("000801") + ": " + NewuGlobal.GetRes("000797");
            }
            else
            {
                toolStripStatusVersion.Text = NewuGlobal.GetRes("000801") + ": " + NewuGlobal.GetRes("000798");
            }
        }

        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //tabControl1 背景色
            Rectangle rectangle = new Rectangle(0, 0, tabControl1.Width, tabControl1.Height);
            SolidBrush BackBrushTabControl = new SolidBrush(Color.Wheat);
            e.Graphics.FillRectangle(BackBrushTabControl, rectangle);

            //标签背景填充颜色
            SolidBrush BackBrush = new SolidBrush(Color.SteelBlue);
            SolidBrush BackBrushSelect = new SolidBrush(Color.Brown);

            //标签文字填充颜色
            SolidBrush FrontBrush = new SolidBrush(Color.White);
            StringFormat StringF = new StringFormat
            {
                //设置文字对齐方式
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //获取标签头工作区域
                Rectangle rec = tabControl1.GetTabRect(i);
                //绘制标签头背景颜色
                e.Graphics.FillRectangle(BackBrush, rec);

                //绘制标签头文字
                if (i == tabControl1.SelectedIndex)
                {
                    //绘制标签头背景颜色
                    e.Graphics.FillRectangle(BackBrushSelect, rec);
                    e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("黑体", 10.5f, FontStyle.Bold), FrontBrush, rec, StringF);
                }
                else
                    e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("黑体", 10.5f), FrontBrush, rec, StringF);
            }
        }

        private void FmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //是否取消close操作
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        #region 监控鼠标键盘输入状态

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;

            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        private static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo))
            {
                NewuGlobal.LogCat("FmMain").Info("GetLastInputInfo failed");
            }

            return Environment.TickCount - vLastInputInfo.dwTime;
        }

        #endregion 监控鼠标键盘输入状态

        /// <summary>
        /// 窗体获取焦点
        /// 开启监控鼠标键盘未操作的间隔时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FmMain_Activated(object sender, EventArgs e)
        {
            timerLoseFocus.Enabled = false;
            loseFocusTime = 0;

            if (!this.timerGetFocus.Enabled)
            {
                this.timerGetFocus.Enabled = true;
            }
        }

        private void TimerGetFocus_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //非操作工账号登录
            if (isWorker)
            {
                long unUsetime = GetLastInputTime();
                if (unUsetime > 1000 * 60 * NewuGlobal.SoftConfig.UnUseTime && !isLoginOut)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            NewuToolBarMenu_ExitEvent(NewuToolBar.AppEvent.Logout);
                            isLoginOut = true;
                        }));
                    }
                    else
                    {
                        NewuToolBarMenu_ExitEvent(NewuToolBar.AppEvent.Logout);
                        isLoginOut = true;
                    }
                }
            }
        }

        /// <summary>
        /// 窗体失去焦点
        /// 开启监控失去焦点时长的线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FmMain_Deactivate(object sender, EventArgs e)
        {
            timerGetFocus.Enabled = false;
            if (!this.timerLoseFocus.Enabled)
            {
                this.timerLoseFocus.Enabled = true;
            }
        }

        private void TimerLoseFocus_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            loseFocusTime++;
            if (loseFocusTime > 60 * NewuGlobal.SoftConfig.UnUseTime && !isLoginOut && isWorker)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        NewuToolBarMenu_ExitEvent(NewuToolBar.AppEvent.Logout);
                        isLoginOut = true;
                    }));
                }
                else
                {
                    NewuToolBarMenu_ExitEvent(NewuToolBar.AppEvent.Logout);
                    isLoginOut = true;
                }
            }
        }
    }
}