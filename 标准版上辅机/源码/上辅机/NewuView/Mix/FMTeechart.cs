using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class FMTeechart : Form
    {
        public double i = 0;
        public int y = 10;
        public Random random = new Random();
        public FMTeechart()
        {
            InitializeComponent();
            axTChart1.Page.MaxPointsPerPage = 20;
            axTChart1.Axis.Left.SetMinMax(0, 20);//纵坐标
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            string x = DateTime.Now.ToString("mm:ss");
            axTChart1.Series(0).Add(random.Next(10,13), x, 5);
            axTChart1.Series(1).Add(random.Next(100, 200),x , 5);
            if (i > 20) //大于20后实现左移动
                axTChart1.Axis.Bottom.Scroll(1, false); //实时曲线成功
        }
    }
}
