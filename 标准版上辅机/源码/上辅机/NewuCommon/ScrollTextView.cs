using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class ScrollTextView : UserControl
    {
        bool flag = true;
        int a = 0;
        public ScrollTextView()
        {
            InitializeComponent();
            timer.Interval = 280;
            timer.Enabled = true;
          //  textView.Font = Bold;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (flag) textView.ForeColor = Color.Red;
            else textView.ForeColor = Color.Yellow;
            a++;
            if (a == 2)
            {
                flag = !flag;
                a = 0;
            }
            try
            {
                textView.Text = textView.Text.Substring(1) + textView.Text.Substring(0, 1);
            }
            catch (Exception ex)
            {
                // null think
            }
        }
        public void setText(string str)
        {
            timer.Enabled = false;
            textView.Text = str;
            timer.Enabled = true;
        }

    }
}
