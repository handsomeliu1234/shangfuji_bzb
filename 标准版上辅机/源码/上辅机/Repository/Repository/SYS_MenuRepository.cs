using Dapper;
using Newu;
using NewuCommon;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Repository.Repository
{
    public class SYS_MenuRepository : BaseDAL<SYS_Menu>
    {
        private TabPageMgr TbPage = new TabPageMgr();

        public SYS_MenuRepository()
        {
        }

        public bool Add(SYS_Menu menuModel)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"insert into SYS_Menu(
                                                        MenuID,
                                                        Caption,
                                                        ControlType,
                                                        ParentMenuID,
                                                        ASSEMBLY,
                                                        NameSpaceAndClass,
                                                        ShowDialog,
                                                        ContainerForm,
                                                        ControlName,
                                                        ControlText,
                                                        ToolTip,
                                                        AutoShow,
                                                        SaveTime,
                                                        MenuOrder,
                                                        MenuLogo,
                                                        Reserve1,
                                                        Reserve2,
                                                        Reserve3,
                                                        Reserve4,
                                                        Reserve5)
                                                    Values(
                                                        NEWID(),
                                                        @Caption,
                                                        @ControlType,
                                                        @ParentMenuID,
                                                        @ASSEMBLY,
                                                        @NameSpaceAndClass,
                                                        @ShowDialog,
                                                        @ContainerForm,
                                                        @ControlName,
                                                        @ControlText,
                                                        @ToolTip,
                                                        @AutoShow,
                                                        @SaveTime,
                                                        @MenuOrder,
                                                        @MenuLogo,
                                                        @Reserve1,
                                                        @Reserve2,
                                                        @Reserve3,
                                                        @Reserve4,
                                                        @Reserve5)");
                    int effecttRow = dbConnection.Execute(sqlStr, menuModel);
                    if (effecttRow > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_MenuRepository").Error(ex.ToString());
                return false;
            }
        }

        public bool DeleteList(string MenuIDlist)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("delete from SYS_Menu ");
                    sqlStr.Append(" where MenuID in (" + MenuIDlist + ")  ");
                    int rows = dbConnection.Execute(sqlStr.ToString());
                    if (rows != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_MenuRepository").Error(ex.ToString());
                return false;
            }
        }

        public SYS_Menu GetModel(string menuID)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"select  top 1 MenuID, Caption, ControlType, ParentMenuID, ASSEMBLY, NameSpaceAndClass, ShowDialog, ContainerForm, ControlName, ControlText, ToolTip, AutoShow, SaveTime, MenuOrder, MenuLogo, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 from SYS_Menu where MenuID = @MenuID");
                    List<SYS_Menu> sYSMenu = dbConnection.Query<SYS_Menu>(sqlStr, new
                    {
                        MenuID = menuID
                    }).ToList();
                    if (sYSMenu.Count > 0)
                    {
                        return sYSMenu[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_MenuRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public List<SYS_Menu> GetList(string strWhere)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append(@"select MenuID, Caption, ControlType, ParentMenuID, ASSEMBLY, NameSpaceAndClass, ShowDialog, ContainerForm, ControlName, ControlText, ToolTip, AutoShow, SaveTime, MenuOrder, MenuLogo, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_Menu ");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    List<SYS_Menu> sYSMenu = dbConnection.Query<SYS_Menu>(sqlStr.ToString()).ToList();
                    if (sYSMenu.Count > 0)
                    {
                        return sYSMenu;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_MenuRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public List<SYS_Menu> GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select ");
                    if (Top > 0)
                    {
                        sqlStr.Append(" top " + Top.ToString());
                    }
                    sqlStr.Append(@" MenuID, Caption, ControlType, ParentMenuID, ASSEMBLY, NameSpaceAndClass, ShowDialog, ContainerForm, ControlName, ControlText, ToolTip, AutoShow, SaveTime, MenuOrder, MenuLogo, Reserve1, Reserve2, Reserve3, Reserve4, Reserve5 FROM SYS_Menu");
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlStr.Append(" where " + strWhere);
                    }
                    sqlStr.Append(" order by " + filedOrder);
                    List<SYS_Menu> sYSMenu = dbConnection.Query<SYS_Menu>(sqlStr.ToString()).ToList();
                    if (sYSMenu.Count > 0)
                    {
                        return sYSMenu;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_MenuRepository").Error(ex.ToString());
                throw ex;
            }
        }

        public List<SYS_Menu> GetLeftjoinMenu(string strWhere)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string sqlStr = string.Format(@"SELECT b.* FROM [TB_Privilege] a left join sys_menu b on a.menuid = b.menuid where " + strWhere + " order by b.MenuOrder");
                    List<SYS_Menu> menuList = connection.Query<SYS_Menu>(sqlStr).AsList();
                    return menuList;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("SYS_MenuRepository").Error(ex.ToString());
                return null;
            }
        }

        private TreeView _NodeTree;

        public TreeView NodeTree
        {
            get
            {
                return _NodeTree;
            }
            set
            {
                _NodeTree = value;
            }
        }

        /// <summary>
        /// 载入Node菜单
        /// </summary>
        /// <param name="node"></param>
        /// <param name="pid"></param>
        /// <param name="list"></param>
        public void LoaderNodes(TreeNode node, string pid, List<SYS_Menu> list)
        {
            foreach (var item in list)
            {
                if (item.ParentMenuID == pid)
                {
                    if (!string.IsNullOrEmpty(item.Reserve1))
                    {
                        item.Caption = NewuGlobal.LanguagResourceManager.GetString(item.Reserve1);
                    }
                    TreeNode P = new TreeNode(item.Caption)
                    {
                        Tag = item,
                        ToolTipText = item.ToolTip,
                        Name = item.MenuID
                    };

                    if (pid == "")
                    {
                        NodeTree.Nodes.Add(P);

                        LoaderNodes(P, item.MenuID, list);
                    }
                    else
                    {
                        node.Nodes.Add(P);
                        LoaderNodes(P, item.MenuID, list);
                    }
                }
            }
        }

        public MenuStrip MenuTree
        {
            get;
            set;
        }

        public TabControl TabControlMain
        {
            get;
            set;
        }

        /// <summary>
        /// 载入Menu菜单
        /// </summary>
        /// <param name="node"></param>
        /// <param name="pid"></param>
        /// <param name="list"></param>
        public void LoaderMenu(ToolStripMenuItem node, string pid, List<SYS_Menu> list)
        {
            foreach (SYS_Menu item in list)
            {
                ToolStripMenuItem P = null;
                if (item.ParentMenuID != pid)
                    continue;

                if (item.Caption == "|")
                {
                    ToolStripSeparator split = new ToolStripSeparator();
                    node.DropDownItems.Add(split);
                }
                else
                {
                    P = new ToolStripMenuItem(item.Caption)
                    {
                        Tag = item,
                        ToolTipText = item.ToolTip,
                        Name = item.MenuID,
                        Image= CreateSampleBitmap(item.MenuLogo)
                    };
                    P.Click += new EventHandler(MenuItem_Click);

                    if (pid == "")
                    {
                        MenuTree.Items.Add(P);

                        LoaderMenu(P, item.MenuID, list);
                    }
                    else
                    {
                        node.DropDownItems.Add(P);
                        LoaderMenu(P, item.MenuID, list);
                    }
                }
            }
        }

        // 创建菜单图标
        internal Image CreateSampleBitmap(byte[] imageLogo)
        {
            Image sampleBitmap = null;
            if (imageLogo!=null)
            {
                MemoryStream buf = new MemoryStream(imageLogo);
                sampleBitmap = Image.FromStream(buf).GetThumbnailImage(16, 16, () => false, IntPtr.Zero);
            }
            return sampleBitmap;
        }
        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _item = (ToolStripMenuItem)sender;

            if (_item != null)
            {
                SYS_Menu module = (SYS_Menu)_item.Tag;
                LoadForm(module);
            }
        }

        public void LoadForm(SYS_Menu module)
        {
            string name = NewuGlobal.LanguagResourceManager.GetString(module.Reserve1);
            bool isExit = TbPage.IsExitTagPage(TabControlMain, module.Caption);

            if (module.ASSEMBLY != "" && module.NameSpaceAndClass != "" && isExit == false)
            {
                object obj = null;
                Type type = Type.GetType(module.NameSpaceAndClass + "," + module.ASSEMBLY);
                if (type == null)
                    return;

                if (module.ControlText != "")
                {
                    object[] objParams = ReflectionParam(module.ControlText);
                    obj = Activator.CreateInstance(type, objParams);
                }
                else
                {
                    obj = Activator.CreateInstance(type);
                }

                if (obj != null && obj is Form)
                {
                    Form fm = obj as Form;
                    fm.Tag = module;
                    switch (module.ShowDialog)
                    {
                        case 0:
                            fm.WindowState = FormWindowState.Maximized;
                            break;

                        case 1:
                            fm.ShowDialog();
                            return;

                        case 2:
                            fm.WindowState = FormWindowState.Normal;
                            break;
                    }

                    //如果不是模态窗口，则新增Page页容器
                    TabPage page = TbPage.AddPage(TabControlMain, name, fm);
                    if (page != null)
                    {
                        page.Tag = module;
                        page.BackColor = fm.BackColor;
                        TabControlMain.SelectedTab = page;
                        page.ControlRemoved += new ControlEventHandler(Page_ControlRemoved);
                        fm.Show();
                    }
                }
            }
        }

        private object[] ReflectionParam(string paramStr)
        {
            List<Item<string, string>> list = new List<Item<string, string>>();

            string[] arr = paramStr.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                Item<string, string> _item = new Item<string, string>();

                string[] dic = arr[i].Split('=');
                if (dic.Length == 2)
                {
                    _item.DisplayName = dic[0];
                    _item.Value = dic[1];
                }
                list.Add(_item);
            }

            object[] ojb = { list };

            return ojb;
        }

        private void Page_ControlRemoved(object sender, ControlEventArgs e)
        {
            TabPage page = sender as TabPage;
            if (TabControlMain.Contains(page))
            {
                if (TabControlMain.SelectedIndex > 1)
                    TabControlMain.SelectedIndex = TabControlMain.SelectedIndex - 1;
                TabControlMain.TabPages.Remove(page);
            }
        }
    }
}