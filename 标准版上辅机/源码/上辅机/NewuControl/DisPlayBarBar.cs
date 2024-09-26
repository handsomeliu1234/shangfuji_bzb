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
    public partial class DisPlayBarBar : UserControl
    {
        StringBuilder strBuder  = null;
        public DisPlayBarBar()
        {
            InitializeComponent();
        }
        public void setContent(string str)
        {
            alarm_content.Text = str;
            this.strBuder = new StringBuilder(str);
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
            if (strBuder == null || strBuder.ToString().Trim().Length <28) return;
            char a = strBuder[0];
            strBuder = strBuder.Remove(0, 1);
            strBuder = strBuder.Append(a);
           // strBuder = strBuder.Insert(0, a);
            alarm_content.Text = strBuder.ToString();
        }
    }
}
