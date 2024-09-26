using Repository.GlobalConfig;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewuView
{
    public partial class FM_About : Form
    {
        private DateTime AppRunTime = DateTime.Now;

        public FM_About()
        {
            InitializeComponent();
            button1.Text = NewuGlobal.GetRes("000854");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new FM_Authorization().Show();
        }
    }
}