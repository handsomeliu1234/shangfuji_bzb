using System.Windows.Forms;

namespace NewuCommon
{
    /// <summary>
    /// TabPage选项页面管理类
    /// </summary>
    public class TabPageMgr
    {
        public enum Location
        {
            NormalCenter,
            Full
        }

        public bool IsExitTagPage(TabControl tabControl1, string pageName)
        {
            foreach (TabPage item in tabControl1.TabPages)
            {
                if (item.Name == pageName)
                {
                    tabControl1.SelectedTab = item;
                    return true;
                }
            }
            return false;
        }

        public TabPage AddPage(TabControl tabControl1, string pageName, Form fm, Location location)
        {
            bool isExit = IsExitTagPage(tabControl1, pageName);
            if (isExit == true)
            {
                return null;
            }

            TabPage page = new TabPage
            {
                Name = pageName,
                Text = pageName
            };
            tabControl1.TabPages.Add(page);

            //开始更改fm样式
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;

            switch (location)
            {
                case Location.NormalCenter:

                    fm.Left = (page.Width - fm.Width) / 2;
                    fm.Top = (page.Height - fm.Height) / 2;

                    break;

                case Location.Full:

                    fm.Left = 0;
                    fm.Top = 0;
                    fm.WindowState = FormWindowState.Normal;
                    fm.Dock = DockStyle.Fill;

                    break;

                default:
                    break;
            }

            page.Controls.Add(fm);
            tabControl1.SelectedTab = page;

            return page;
        }

        public TabPage AddPage(TabControl tabControl1, string pageName, Form fm)
        {
            if (fm.WindowState == FormWindowState.Maximized)
            {
                return AddPage(tabControl1, pageName, fm, Location.Full);
            }
            else
            {
                return AddPage(tabControl1, pageName, fm, Location.NormalCenter);
            }
        }
    }
}