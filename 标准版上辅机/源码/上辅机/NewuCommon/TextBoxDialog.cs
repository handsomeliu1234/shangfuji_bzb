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
    public partial class TextBoxDialog : UserControl
    {

        public delegate void DalogClick(object sender, EventArgs e);
        public event DalogClick BtnDalogClick;

        public TextBoxDialog()
        {
            InitializeComponent();

            

        }

        public Button BtnDalog
        {
            get { return btnDalog; }
        }

        public string Values
        {
            get { return txtDalog.Text; }
            set { txtDalog.Text = value; }
        }

        public bool ReadOnly
        {
            set { txtDalog.ReadOnly=value; }
            get { return txtDalog.ReadOnly; }
        }


        private void btnDalog_Click(object sender, EventArgs e)
        {
            BtnDalogClick(sender, e);
        }

        private void TextBoxDialog_Resize(object sender, EventArgs e)
        {
            txtDalog.Width = this.Width - btnDalog.Width;
        }

       


    }
}
