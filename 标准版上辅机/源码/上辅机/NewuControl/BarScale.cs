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
    public partial class BarScale : UserControl
    {
        private double errorWeight {get; set; }
        private double unitLength { get; set; }
        private double maxWeight { get; set; }
        private double minWeight { get; set; }
        private double setWeight { get; set; }
        private double unitWeight { get; set; }
        private double barLength { get;set;}
        private double nowWeight = 0;
        public BarScale()
        {

            InitializeComponent();
           // this.bar1.Width = this.label1.Location.X - 200;
           // this.bar2.Width = this.label1.Location.Y - 200;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="setWeight">物料设定重量</param>
        /// <param name="errorWeight">物料允许误差</param>

        public void setBarScaleWidth(int formWidth,int labelWidth)
        {
            this.bar1.Width = formWidth - labelWidth - 50;
            this.bar2.Width = formWidth - labelWidth - 100;
            this.barLength = (int)bar1.Width / 100 * 100;
            setNowWeight(nowWeight);
        }
        public void setMessage(double setWeight,double errorWeight)
        {
          //  this.bar1.Width = aaa - 200;
           // InitializeComponent();
            this.setWeight = setWeight;
            this.errorWeight = errorWeight;
            this.maxWeight = setWeight + errorWeight;
            this.minWeight = setWeight - errorWeight > 0 ? setWeight - errorWeight : 0;
            this.unitWeight = setWeight / 5 + 1;
            this.barLength = (int)bar1.Width / 100 * 100;
            this.unitLength = barLength / (setWeight / unitWeight);
            setNowWeight(0.0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowWeight">从PLC实时监控到的重量数据</param>
        public void setNowWeight(double nowWeight)
        {
            this.nowWeight = nowWeight;
            label1.Text = nowWeight.ToString();
            double nowLength = (nowWeight / setWeight) * barLength;
            drawBar1(Color.SkyBlue,nowLength);

            
            Graphics ggg = bar2.CreateGraphics();
            ggg.Clear(Color.White);
            label2.Text = "----";
            if (nowWeight < (minWeight - errorWeight))
                return;
            Color color;
            if (nowWeight < minWeight)
                color = Color.Yellow;
            else if (nowWeight <= maxWeight)
                color = Color.SkyBlue;
            else
                color = Color.Red;
            double temp = nowWeight - setWeight;
            label2.Text = temp > 0.0 ? " + " + temp.ToString("f2") : temp.ToString("f2"); 
            double len = (nowWeight - minWeight + errorWeight) / (errorWeight) * (barLength / 4);

            drawBar2(color , len , ggg);
            
        }
        public void drawBar2(Color color, double length,Graphics g)
        {
            Brush brush = new SolidBrush(color);//填充的颜色
            Point p = new Point(bar1.Location.X, bar1.Location.Y);
            Size s = new Size((int)length, bar2.Height - 2 * bar1.Location.Y);
            Rectangle r = new Rectangle(p, s);
            g.FillRectangle(brush, r);
            Pen myPen = new Pen(Brushes.Red);
            for (int i = 1; i <= 3; i++)
            {
                double bb = bar2.Location.X + i * barLength / 4 ;
                Point x = new Point((int)bb, bar1.Location.Y);
                Point y = new Point((int)bb, bar2.Height - bar1.Location.Y);
                myPen.Width = 2;
                if (i == 2)
                {
                    myPen.Width = 5;
                }
                g.DrawLine(myPen, x, y);
            }

        }
        public void drawBar1(Color color,double length)
        {
            Graphics g = bar1.CreateGraphics();
            g.Clear(Color.White);
            Brush brush = new SolidBrush(color);//填充的颜色
            Point p = new Point(bar1.Location.X, bar1.Location.Y);
           // Point p = new Point(0,0);
            Size s = new Size((int)length, bar1.Height - 2 * bar1.Location.Y);
            Rectangle r = new Rectangle(p, s);
            g.FillRectangle(brush, r);

            Pen myPen = new Pen(Brushes.Red);
            for (int i = 1; i <= 5; i++)
            {
                double bb = bar1.Location.X + i * barLength / 5;
                Point x = new Point((int)bb, bar1.Location.Y);
                Point y = new Point((int)bb, bar1.Height -  bar1.Location.Y);
                myPen.Width = 2;
                if (i == 5)
                {
                    myPen.Width = 5;
                }
                g.DrawLine(myPen, x, y);
            }
        }
    }
}
