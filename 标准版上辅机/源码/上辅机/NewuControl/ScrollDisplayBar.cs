using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class ScrollDisplayBar : UserControl
    {
        StringBuilder strBuder = null;
        public ScrollDisplayBar()
        {
            InitializeComponent();
        }
        public void setContent(string str)
        {
            alarm_content.Text = str;
            strBuder = new StringBuilder(str);
        }
        private int maxNum = 45;
        public int MaxNum
        {
            set { maxNum = value; }
        }
        //public void run()
        //{
        //    if (alarm_content.Left < this.Width)
        //    {
        //        alarm_content.Left = alarm_content.Left + 30;
        //    }
        //    else if (alarm_content.Left > -this.Width)
        //    {
        //        alarm_content.Left = -alarm_content.Width;
        //    }
        //}
        public void run()
        {
            if (strBuder == null || strBuder.Length < maxNum) return;
            char a = strBuder[0];
            strBuder = strBuder.Remove(0, 1);
            strBuder = strBuder.Append(a);
            alarm_content.Text = strBuder.ToString();
        }
    }
}
