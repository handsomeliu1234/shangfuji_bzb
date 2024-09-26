using System;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class NewuNavigator : UserControl
    {
        public delegate void BtnClick(int pageIndex);

        public event BtnClick NavigatorButtonClick;

        public NewuNavigator()
        {
            InitializeComponent();

            bindingNavigatorMoveFirstItem.Click += new EventHandler(BindingNavigatorMoveFirstItem_Click);
            bindingNavigatorMovePreviousItem.Click += new EventHandler(BindingNavigatorMovePreviousItem_Click);
            bindingNavigatorMoveNextItem.Click += new EventHandler(BindingNavigatorMoveNextItem_Click);
            bindingNavigatorMoveLastItem.Click += new EventHandler(BindingNavigatorMoveLastItem_Click);
            bindingNavigatorPositionItem.KeyPress += new KeyPressEventHandler(BindingNavigatorPositionItem_KeyPress);

        }

        private void BindingNavigatorPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                NavigatorButtonClick(CurrentPageIndex);
            }
        }

        private void BindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            if (NavigatorButtonClick != null)
            {
                CurrentPageIndex = PageCount;
                NavigatorButtonClick(CurrentPageIndex);
            }
        }

        private void BindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            if (NavigatorButtonClick != null)
            {
                if (CurrentPageIndex < PageCount)
                    CurrentPageIndex++;
                NavigatorButtonClick(CurrentPageIndex);
            }
        }

        private void BindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            if (NavigatorButtonClick != null)
            {
                if (CurrentPageIndex > 1)
                {
                    CurrentPageIndex--;
                    bindingNavigatorMovePreviousItem.Enabled = true;
                    bindingNavigatorMoveFirstItem.Enabled = true;
                }
                else
                {
                    CurrentPageIndex = 1;
                    bindingNavigatorMovePreviousItem.Enabled = false;
                    bindingNavigatorMoveFirstItem.Enabled = false;
                }
                NavigatorButtonClick(CurrentPageIndex);
            }
        }

        private void BindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            if (NavigatorButtonClick != null)
            {
                CurrentPageIndex = 1;
                NavigatorButtonClick(CurrentPageIndex);
            }
        }

        private int _CurrentPageIndex = 0;

        public int CurrentPageIndex
        {
            get
            {
                try
                {
                    bindingNavigatorPositionItem.Text = _CurrentPageIndex.ToString();
                    return _CurrentPageIndex;
                }
                catch
                {
                    return _CurrentPageIndex;
                }
            }
            set
            {
                _CurrentPageIndex = value;
            }
        }

        private int _pageCount = 0;

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                if (_pageCount == value)
                    return;

                _pageCount = value;

                string[] dd = new string[_pageCount];

                BindingSource source = new BindingSource();
                source.DataSource = dd;

                bindingNavigator1.BindingSource = source;
            }
        }

        public void SetCurrentPageIndex(string x)
        {
            bindingNavigatorPositionItem.Text = x;
            CurrentPageIndex = int.Parse(bindingNavigatorPositionItem.Text);
            if (CurrentPageIndex > 1)
            {
                bindingNavigatorMovePreviousItem.Enabled = true;
                bindingNavigatorMoveFirstItem.Enabled = true;
            }

            if (NavigatorButtonClick != null)
            {
                NavigatorButtonClick(CurrentPageIndex);
            }
        }
    }
}