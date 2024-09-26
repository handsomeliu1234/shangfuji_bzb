using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NewuCommon
{
    /// <summary>
    /// 工具栏
    /// </summary>
    public class NewuToolBar : ToolStrip
    {
        /// <summary>
        /// Login:登录 Logout:注销 Exit:退出
        /// </summary>
        public enum AppEvent
        {
            Login,
            Logout,
            Exit,
            LoginAgain
        }

        private Size BtnIconSize = new Size(60, 51);

        private SYS_MenuRepository menuRepository;

        //定义委托和事件
        public delegate void DelegateVoid(AppEvent _appEventType);

        public event DelegateVoid ExitEvent;

        /// <summary> 加载工具条数据
        //todo:添加权限判断   与TB_Privilege 进行联合查询 来控制其显示隐藏   time:2017.11.27 10.30
        // 解决状态：未解决
        /// </summary>
        public void LoadToolBar(TabControl tab)
        {
            this.AutoSize = false;
            this.Height = BtnIconSize.Height + 3;

            //解决工具栏用户角色权限问题
            menuRepository = new SYS_MenuRepository()
            {
                TabControlMain = tab
            };

            string menuID = menuRepository.GetList("Caption=N'工具条'")[0].MenuID;
            string whereStr = "ParentMenuID='" + menuID + "' and a.RoleID='" + NewuGlobal.TB_UserInfo.RoleID + "' and a.Enable='1'";
            List<SYS_Menu> sYS_Menus = menuRepository.GetLeftjoinMenu(whereStr);

            int count = sYS_Menus.Count();
            for (int i = 0; i < count; i++)
            {
                //开始分隔符
                if (sYS_Menus[i].Caption == "|")
                {
                    ToolStripSeparator split = new ToolStripSeparator();
                    this.Items.Add(split);
                    continue;
                }

                ToolStripButton btn = new ToolStripButton
                {
                    AutoSize = false,
                    Size = BtnIconSize,
                    ImageScaling = ToolStripItemImageScaling.None,
                    Name = sYS_Menus[i].Caption,
                    Text = sYS_Menus[i].Caption,
                    DisplayStyle = ToolStripItemDisplayStyle.Image,//隐藏text，不然会把标题名也显示出来。LZC
                    TextImageRelation = TextImageRelation.ImageAboveText,
                    Tag = sYS_Menus[i]
                };
                //|| sYS_Menus[i].Caption == "关于"
                if (sYS_Menus[i].Caption == "登录" || sYS_Menus[i].Caption == "注销" || sYS_Menus[i].Caption == "退出")
                {
                    btn.Click += new EventHandler(BtnClose_Click);
                }
                else
                {
                    btn.Click += new EventHandler(Btn_Click);
                }
                this.Items.Add(btn);

                if (sYS_Menus[i].MenuLogo != null)
                {
                    // 从数据库中加载图片
                    MemoryStream buf = new MemoryStream(sYS_Menus[i].MenuLogo);

                    Image menuLogo = Image.FromStream(buf).GetThumbnailImage(38, 38, () => false, IntPtr.Zero);

                    btn.Image = menuLogo;
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton btn = (ToolStripButton)sender;
                if (btn != null)
                {
                    SYS_Menu module = (SYS_Menu)btn.Tag;
                    menuRepository.LoadForm(module);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (ExitEvent != null)
            {
                ToolStripButton btn = sender as ToolStripButton;

                if (btn.Text.ToUpper() == "退出")
                {
                    ExitEvent(AppEvent.Exit);
                }
                if (btn.Text.ToUpper() == "注销")
                {
                    ExitEvent(AppEvent.Logout);
                }
                if (btn.Text.ToUpper() == "登录")
                {
                    ExitEvent(AppEvent.Login);
                }
            }
        }
    }
}