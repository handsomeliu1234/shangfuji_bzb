using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using NewuModel;
namespace NewuRPT
{
    /**
     * 定义
     * 温度 1
     * 功率 2
     * 压力 3
     * 转速 4
     * 能量 5
     * 栓位 6
     * 电压 7
     */
    public partial class FM_MixCurve_Super : Form
    {
        private bool[] CurveShow = new bool[8] { false, true, true, true, true, true, true, true };

        private CheckBox[] checkBox = new CheckBox[8];
        public int NBNum = 0;
        public const int SERIES_NUM = 7;
        public const int SERIES_NUM_MIX = SERIES_NUM * 512;
        private PM_OrderTranMDL orderTranMD = null;
        private string orderID = "a4e1cde3-b123-47f3-9da6-ecc413b1d4ca";
        private string nowBrach = "1";
        Arr[] ArrData = new Arr[SERIES_NUM_MIX];

        public FM_MixCurve_Super()
        {
            InitializeComponent();
        }
        public FM_MixCurve_Super(string orderID, string nowBrach, PM_OrderTranMDL orderTranMD)
        {
            this.orderID = orderID;
            this.nowBrach = nowBrach;
            this.orderTranMD = orderTranMD;
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            checkBox[1] = checkBox1;
            checkBox[2] = checkBox2;
            checkBox[3] = checkBox3;
            checkBox[4] = checkBox4;
            checkBox[5] = checkBox5;
            checkBox[6] = checkBox6;
            checkBox[7] = checkBox7;
        }
        private void FM_MixCurve_Super_Load(object sender, EventArgs e)
        {

            initChart();
            DrawSeries(true);
        }
        /// <summary>
        /// 初始化chart控件
        /// </summary>
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
            //title.Text = "完全OJBK";
            title.Text = orderTranMD.MaterialCode + "第 " + nowBrach + "车曲线 " + "（共" + orderTranMD.SetBatch + "车）";
            // tChart1.Header.Text = label1.Text + "（共" + orderMD.SetBatch + "车）";
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
           
            chartArea.AxisY.Title = "左";
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 200;
            chartArea.AxisY2.Title = "右";
            chartArea.AxisY2.Maximum = 1200;
            chartArea.AxisY2.Enabled = AxisEnabled.True;

            // 设置曲线下放放大 轮动条
            //chartArea.CursorX.IsUserEnabled = true;
            //chartArea.CursorX.IsUserSelectionEnabled = true;
            //chartArea.AxisX.ScaleView.Zoomable = true;
            //chartArea.AxisX.ScrollBar.IsPositionedInside = true;
            //chartArea.AxisX.ScaleView.Zoom(2, 3);
            //chartArea.AxisX.ScrollBar.Size = 10;

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
        /// <summary>
        /// 添加曲线显示
        /// </summary>
        /// <param name="is_showLegend"></param>
        MixSerise mixSerise = new MixSerise();
        private void DrawSeries(bool is_showLegend)
        {
            if (!cbSuperPosition.Checked)
            {
                chart1.Series.Clear();
            }
            else
            {
                NBNum++;
            }
            //for (int i = 0; i < chart1.Series.Count; i++)
            //{
            //    chart1.Series[i].Points.Clear();
            //}

            if (orderID == "" || nowBrach == "") return;
            if (orderTranMD == null) return;
            if (chart1.Titles.Count > 0)
                chart1.Titles[0].Text = orderTranMD.MaterialCode + "第 " + nowBrach + "车曲线 ";
            //  搞一车数据
            getData(orderID, nowBrach);
            //  显示
            for (int i = 1; i <= 7; i++)
            {
                int cc = NBNum * SERIES_NUM + i;
                Series series = this.SetSeriesStyle(mixSerise.getMixSerise(i, !cbSuperPosition.Checked));

                for (int time = 1; time <= ArrData[cc].Data.Length; time++)
                {
                    series.Points.AddXY(time, ArrData[cc].Data[time - 1]); ///@@@@@!!!!!!!!!!!!!
                }
                if (!CurveShow[i]) series.Points.Clear();
                this.chart1.Series.Add(series);
            }
        }
        private void getData(string orderID, string nowBrach)
        {
            try
            {
                NewuBLL.RPT_CurveBLL curveBll = new NewuBLL.RPT_CurveBLL(orderTranMD);
                string where = " OrderID = '" + orderID + "' and FactOrder = '" + nowBrach + "'";
                lock (ArrData)
                {
                    DataSet ds = curveBll.GetList(where);
                    DataTable dt = ds.Tables[0];
                    int allNum = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        allNum += Regex.Matches(row["RealTime"].ToString(), @"/").Count;
                    }
                    //for (int i = 0; i < ArrData.Length; i++)
                    //    ArrData[i].Redim(allNum);
                    int cnt = 0;

                    int cc = NBNum * SERIES_NUM;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int num = Regex.Matches(row["RealTime"].ToString(), @"/").Count;
                        for (int xx = 1; xx <= 7; xx++)
                            ArrData[cc + xx].Redim(num);
                        FomatData(row, num);
                        for (int j = 0; j < num; j++)
                        {
                            // ArrData[cc].Data[cnt] = times[j];
                            ArrData[cc + 1].Data[cnt] = temp[j];
                            ArrData[cc + 2].Data[cnt] = power[j];
                            ArrData[cc + 3].Data[cnt] = press[j]*100;
                            ArrData[cc + 4].Data[cnt] = speed[j];
                            ArrData[cc + 5].Data[cnt] = energy[j]*20;
                            ArrData[cc + 6].Data[cnt] = (ramPos[j]) / 3;
                            ArrData[cc + 7].Data[cnt++] = voltage[j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private double[] power;
        private double[] times;
        private double[] energy;
        private double[] press;
        private double[] speed;
        private double[] temp;
        private double[] ramPos;
        private double[] voltage;
        private int[] autoRun;
        private void FomatData(DataRow dr, int maxNum)
        {
            times = new double[maxNum + 10];
            power = new double[maxNum + 10];
            press = new double[maxNum + 10];
            energy = new double[maxNum + 10];
            speed = new double[maxNum + 10];
            temp = new double[maxNum + 10];
            ramPos = new double[maxNum + 10];
            voltage = new double[maxNum + 10];
            autoRun = new int[maxNum + 10];
            change(dr["RealTime"].ToString(), maxNum, times);
            change(dr["Power"].ToString(), maxNum, power);
            change(dr["Energy"].ToString(), maxNum, energy);
            change(dr["Press"].ToString(), maxNum, press);
            change(dr["Speed"].ToString(), maxNum, speed);
            change(dr["Temp"].ToString(), maxNum, temp);
            change(dr["Reserve1"].ToString(), maxNum, ramPos);
            change(dr["Reserve2"].ToString(), maxNum, voltage);
            change(dr["Reserve3"].ToString(), maxNum, autoRun);

        }
        private void change(string str, int num, int[] ans)
        {
            str += "/";
            int cnt = 0;
            string temp = "";
            int len = str.Length;
            for (int i = 0; i < len && cnt <= num; i++)
            {

                if (str[i] == '/')
                {
                    if (temp != "")
                    {
                        ans[cnt++] = int.Parse(temp);
                    }
                    temp = "";
                    continue;
                }
                temp += str[i];
            }
        }
        private void change(string str, int num, double[] ans)
        {
            str += "/";
            int cnt = 0;
            string temp = "";
            int len = str.Length;
            for (int i = 0; i < len && cnt <= num; i++)
            {

                if (str[i] == '/')
                {
                    if (temp != "")
                    {
                        ans[cnt++] = Double.Parse(temp);
                    }
                    temp = "";
                    continue;
                }
                temp += str[i];
            }
        }
        /// <summary>
        /// 配置曲线
        /// </summary>
        /// <param name="i"></param>
        /// <param name="is_showLegend"></param>
        /// <returns></returns>
        private Series SetSeriesStyle(SeriseParam param)
        {
            Series series = null;
            if (param.showLegend)
                series = new Series(param.field);
            else
            {
                //string str = string.Format("Ch{0}-{1}", i + 1, lineNo);
                series = new Series(string.Format("{0}-{1}", param.field, NBNum));
            }
            //Series的类型
            series.ChartType = SeriesChartType.Line;
            //Series的边框颜色
            series.BorderColor = Color.FromArgb(180, 26, 59, 105);
            //线条宽度
            series.BorderWidth = param.width;
            //线条阴影颜色
            //series.ShadowColor = Color.Black;
            //阴影宽度
            //series.ShadowOffset = 2;
            //是否显示数据说明
            series.IsVisibleInLegend = false;
            //线条上数据点上是否有数据显示
            series.IsValueShownAsLabel = false;
            //线条上的数据点标志类型
            series.MarkerStyle = MarkerStyle.None;
            //线条数据点的大小
            //series.MarkerSize = 8;
            //线条颜色
            series.Color = param.color;
            if(param.Loc == 2)
            series.YAxisType = AxisType.Secondary;//选第二纵坐标
            return series;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(nowBrach);
            if (number >= 2)
                number--;
            else
                return;
            nowBrach = number.ToString();
            DrawSeries(!cbSSuperPosiontion);
            if (cbSSuperPosiontion)
            {
                cbSuperPosition.Text = "已叠加" + NBNum + "车";
            }
            else
            {
                cbSuperPosition.Text = "叠加曲线";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(nowBrach);
            number++;
            nowBrach = number.ToString();
            DrawSeries(!cbSSuperPosiontion);
            if (cbSSuperPosiontion)
            {
                cbSuperPosition.Text = "已叠加" + NBNum + "车";
            }
            else
            {
                cbSuperPosition.Text = "叠加曲线";
            }
        }
        bool cbSSuperPosiontion = false;
        private void cbSuperPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowData.Checked == true)
            {
                MessageBox.Show("显示数据时不能叠加曲线！");
                cbSuperPosition.Checked = false;
                return;
            }

            cbSSuperPosiontion = cbSuperPosition.Checked;
            if (cbSuperPosition.Checked == true)
            {
                //ArrData = new Arr[300];
            }
            else
            {
                ArrData = new Arr[SERIES_NUM_MIX];
                NBNum = 0;
                for (int i = 0; i < ArrData.Length; i++)
                {
                    ArrData[i] = new Arr(0);
                }
            }
        }

        private void checkBox_Checked(int cnt, string str)
        {
            //不显示 温度曲线

            if (!checkBox[cnt].Checked)
            {
                CurveShow[cnt] = false;
                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    if (chart1.Series[i].Name.Contains(str))
                    {
                        chart1.Series[i].Points.Clear();
                    }
                }
            }
            else
            {
                CurveShow[cnt] = true;
                int count = 0;

                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    if (chart1.Series[i].Name.Contains(str))
                    {
                        int cc = count * SERIES_NUM + cnt;
                        for (int time = 1; time <= ArrData[cc].Data.Length; time++)
                        {
                            chart1.Series[i].Points.AddXY(time, ArrData[cc].Data[time - 1]); ///@@@@@!!!!!!!!!!!!!
                        }
                        count++;
                        //chart1.Series[i].Points.Add();
                    }
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(1, "temp");
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(2, "Power");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(3, "Press");
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(4, "Speed");
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(5, "Energy");
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(6, "Resever1");
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Checked(7, "Resever2");
        }

        private void cbShowData_CheckedChanged(object sender, EventArgs e)
        {

            foreach (Series se in chart1.Series)
            {
                se.IsValueShownAsLabel = false;
            }
            if (cbShowData.Checked == true)
                for (int i = 0; i <= 6; i++)
                {
                    int cc = NBNum * SERIES_NUM + i;
                    chart1.Series[cc].IsValueShownAsLabel = true;
                }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);
            if (result != null && result.Object != null)
            {
                // When user hits the LegendItem
                if (result.Object is LegendItem)
                {
                    // Legend item result
                    LegendItem legendItem = (LegendItem)result.Object;

                    // series item selected
                    Series selectedSeries = (Series)legendItem.Tag;

                    if (selectedSeries != null)
                    {
                        //System.Windows.Forms.DataVisualization.Charting.Utilities.
                        //System.Windows.Forms.DataVisualization.Charting.Utilities.SampleMain.MainForm mainForm = (System.Windows.Forms.DataVisualization.Charting.Utilities.SampleMain.MainForm)this.ParentForm;

                        if (selectedSeries.Enabled)
                        {
                            selectedSeries.Enabled = false;
                            legendItem.Cells[0].Image = string.Format(NewuCommon.FunClass.CurrentPath + @"\chk_unchecked.png");
                            legendItem.Cells[0].ImageTransparentColor = Color.Red;
                        }

                        else
                        {
                            selectedSeries.Enabled = true;
                            legendItem.Cells[0].Image = string.Format(NewuCommon.FunClass.CurrentPath + @"\chk_checked.png");
                            legendItem.Cells[0].ImageTransparentColor = Color.Red;
                        }
                    }
                }
            }
        }

    }
}
