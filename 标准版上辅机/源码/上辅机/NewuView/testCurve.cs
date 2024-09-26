using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace NewuView
{
    public partial class testCurve : Form
    {
        int count = 0;
        public testCurve()
        {
            InitializeComponent();
        }

        private void testCurve_Load(object sender, EventArgs e)
        {
            initChart();

            DrawSeries(true);
        }
        private void initChart()
        {
            chart1.Series.Clear();

             #region 设置图表的属性
            //图表的背景色
            chart1.BackColor = Color.FromArgb(211, 223, 240);
            //图表背景色的渐变方式
            chart1.BackGradientStyle = GradientStyle.None;
            //图表的边框颜色、
            chart1.BorderlineColor = Color.FromArgb(26, 59, 105);
            //图表的边框线条样式
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            //图表边框线条的宽度
            chart1.BorderlineWidth = 2;
            //图表边框的皮肤
            chart1.BorderSkin.SkinStyle = BorderSkinStyle.None;
            #endregion

            #region 设置图表的Title
            Title title = new Title();
            //标题内容
            title.Text = "完全OJBK";
            //标题的字体
            title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            //标题字体颜色
            //title.ForeColor = Color.FromArgb(26, 59, 105);
            //标题阴影颜色
            //title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            //标题阴影偏移量
            //title.ShadowOffset = 3;

            chart1.Titles.Add(title);
            #endregion

            #region 设置图表区属性
            //图表区的名字
           
            //chart1.ChartAreas.Clear();
            ChartArea chartArea = chart1.ChartAreas[0];
            //ChartArea chartArea = new ChartArea("chartArea1");
            //背景色
            chartArea.BackColor = Color.White;// Color.FromArgb(64, 165, 191, 228);
            //背景渐变方式
            chartArea.BackGradientStyle = GradientStyle.None;
            //渐变和阴影的辅助背景色
            chartArea.BackSecondaryColor = Color.White;
            //边框颜色
            chartArea.BorderColor = Color.Blue;
            //边框线条宽度
            chartArea.BorderWidth = 2;
            //边框线条样式
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            //阴影颜色
            //chartArea.ShadowColor = Color.Transparent;

            //设置X轴和Y轴线条的颜色和宽度
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineWidth = 1;
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.LineWidth = 1;

            //设置X轴和Y轴的标题
            //chartArea.AxisX.Title = "time";
            //chartArea.AxisY.Title = "count";
            //chartArea.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            //chartArea.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            //设置图表区网格横纵线条的颜色和宽度
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineWidth = 1;
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineWidth = 1;          

            //chart1.ChartAreas.Add(chartArea);
            #endregion

            #region 图例及图例的位置
            Legend legend = new Legend();
            legend.Alignment = StringAlignment.Center;
            legend.Docking = Docking.Bottom;
            legend.BackColor = Color.Yellow;

            this.chart1.Legends.Add(legend);
            #endregion
        
        }
        int lineNo = 0;
        private Series SetSeriesStyle(int i, bool is_showLegend)
        {
            Series series = null;
            if (is_showLegend)
                series = new Series(string.Format("Ch{0}", i + 1));
            else
            {
                string str = string.Format("Ch{0}-{1}", i + 1, lineNo);
                series = new Series(string.Format("Ch{0}-{1}", i + 1, lineNo));
            }

            //Series的类型
            series.ChartType = SeriesChartType.Line;
            //Series的边框颜色
            series.BorderColor = Color.FromArgb(180, 26, 59, 105);
            //线条宽度
            series.BorderWidth = 3;
            //线条阴影颜色
            //series.ShadowColor = Color.Black;
            //阴影宽度
            //series.ShadowOffset = 2;
            //是否显示数据说明
            series.IsVisibleInLegend = is_showLegend;
            //线条上数据点上是否有数据显示
            series.IsValueShownAsLabel = false;
            //线条上的数据点标志类型
            series.MarkerStyle = MarkerStyle.None;
            //线条数据点的大小
            //series.MarkerSize = 8;
            //线条颜色
            switch (i)
            {
                case 0:
                    series.Color = Color.FromArgb(220, 65, 140, 240);
                    series.Color = Color.Red;
                    break;
                case 1:
                    series.Color = Color.FromArgb(220, 224, 64, 10);
                    series.Color = Color.Green;
                    break;
                case 2:
                    series.Color = Color.FromArgb(220, 120, 150, 20);
                    series.Color = Color.Yellow;
                    break;
                case 3:
                    series.Color = Color.FromArgb(220, 12, 128, 232);
                    series.Color = Color.Black;
                    break;
            }
            return series;
        }
        private void DrawSeries(bool is_showLegend)
        {
            int cnt;
            cnt = 1;
            if (!is_showLegend)
                cnt += count;
            count += 10;
            for (int i = 0; i < 4; i++)
            {

                Series series = this.SetSeriesStyle(i, is_showLegend);
                
                for (; cnt<40*(i+1)+count; )
                {
                    
                    series.Points.AddXY(cnt, count+cnt++);
                }
                this.chart1.Series.Add(series);
            }
        }
        int ooo = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            DrawSeries(false);
            button1.Text = (ooo++).ToString();
            lineNo++;
        }

        //显示隐藏曲线
        private void DisOrPlaySeries(int i){
            if (checkBox1.Checked == false)
            {
                chart1.Series["Ch1-1"].Color = Color.White;
                //chart1.Series["Ch1-1"].Points.Clear();
            }
            else
            {
                chart1.Series["Ch1-1"].Color = Color.Red;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DisOrPlaySeries(1);
        }
        //{
        //    CheckBox[] cheackBox = new CheckBox[](10);
            //if (checkBox[i].Checked)
            //{
            //    DataRow[] foundRows;
            //    string expression = "Ch = " + i;
            //    foundRows = dt.Select(expression);
            //    foreach (DataRow row in foundRows)
            //    {
            //        this.chart1.Series[i].Points.AddXY(row[0], row[2]);
            //    }
            //}
            //else
            //{
            //    this.chart1.Series[i].Points.Clear();
            //}
        
    }
}
