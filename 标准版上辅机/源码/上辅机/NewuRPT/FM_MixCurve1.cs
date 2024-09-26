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

using NewuModel;
namespace NewuRPT
{
    public partial class FM_MixCurve : Form
    {
        private bool isOver = false;
        private PM_OrderTranMDL orderMD = null;
        private string orderID = "a4e1cde3-b123-47f3-9da6-ecc413b1d4ca";
        private string brach = "1";
        Arr[] ArrData = new Arr[14];

        TTMixSerise TT类型 = new TTMixSerise();

        public FM_MixCurve()
        {
            InitializeComponent();
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
        }
        public FM_MixCurve(string left, string right, PM_OrderTranMDL orderMD)
        {
            InitializeComponent();
            orderID = left;
            brach = right; 
            this.orderMD = orderMD;
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
        }
        private void FM_MixCurve_Load(object sender, EventArgs e)
        {
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.WindowState = FormWindowState.Maximized;
            InitTee();

            //return;

            //textBox1.Left = -100;
            //addMenu();

            Start();
        }
        void Start()
        {

            for (int i = 0; i < tChart1.Series.Count; i++)
            {
                tChart1.Series[i].Clear();
            }

            if (orderID == "" || brach == "") return;
            if (orderMD == null) return;
            label1.Text = orderMD.MaterialCode + "第 " + brach + "车曲线 ";
            tChart1.Header.Text = label1.Text + "（共" + orderMD.SetBatch + "车）";
            GetData(orderID, brach);
            GetFlag();
            FullData();

            //SetBottomMinMax(dtp1.Value);


        }
        void GetFlag()
        {
            int index = TT类型.T背景.no;
            tChart1.Series[index].Clear();

            index = TT类型.M备注.no;
            tChart1.Series[index].Clear();



            bool flag = false;
            bool showFlag = false;
            double xVal1 = 0.0, xVal2 = 0.0;
            if (autoRun == null)
                return;
            if (autoRun.Length >=1)
                autoRun[autoRun.Length - 1 - 10] = 0;
            for (int i = 0; i < autoRun.Length - 10; i++)
            {
                if (autoRun[i] == 1 && !flag)
                {
                    flag = true;
                    xVal1 = times[i];

                }
                else if (flag && autoRun[i] == 0)
                {
                    flag = false;
                    xVal2 = times[i];
                    showFlag = true;
                }
                if (showFlag)
                {
                    double xMid = (xVal1 + xVal2) / 2;
                    tChart1.Series[TT类型.T背景.no].Add(xVal1, 300, "1");
                    tChart1.Series[TT类型.T背景.no].Add(xVal2, -300, "1");
                    tChart1.Series[TT类型.M备注.no].Add(xMid, 140, "混炼手动");
                    showFlag = false;
                }
            }

        }
        int flag = 0;
        void FullData()
        {

            //数据量
            tLabNum.Text = "数据量：" + ArrData[0].Data.Length.ToString() + " 笔";

            int IT时间 = 0;
            try
            {
               // if (!isOver)
               //     this.area1 = this.save;

                int index = 0;
                //index = TT类型.T背景.no;
                //tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);

                index = TT类型.T温度.no;
                tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);


                index = TT类型.L压力.no;
                if (!isOver)
                    tChart1.Series[index].Clear();
                // tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);
                for (int i = 0; i < ArrData[IT时间].Data.Length; i++)
                {
                    tChart1.Series[index].Add(ArrData[IT时间].Data[i], ArrData[index].Data[i]);
                }

                index = TT类型.L转速.no;
                if (!isOver)
                    tChart1.Series[index].Clear();
                tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);

                index = TT类型.L功率.no;
                if (!isOver)
                    tChart1.Series[index].Clear();
                tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);

                index = TT类型.L能量.no;
                if (!isOver)
                    tChart1.Series[index].Clear();
                tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);

                index = TT类型.L栓位.no;
                if (!isOver)
                    tChart1.Series[index].Clear();
                tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);

                index = TT类型.L电压.no;
                if (!isOver)
                    tChart1.Series[index].Clear();
                tChart1.Series[index].Add(ArrData[IT时间].Data, ArrData[index].Data);






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void GetData(string orderID, string brach)
        {


            try
            {
                List<double> doubleX = new List<double>();
                List<double> doubleY = new List<double>();


                NewuBLL.RPT_CurveBLL curveBll = new NewuBLL.RPT_CurveBLL(orderMD);

                string where = " OrderID = '" + orderID + "' and FactOrder = '" + brach + "'";

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
                    if (allNum == 0)
                    {
                        autoRun = null;
                    }
                    for (int i = 0; i <= 10; i++)
                        ArrData[i].Redim(allNum);
                    int cnt = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int num = Regex.Matches(row["RealTime"].ToString(), @"/").Count;
                        FomatData(row, num);
                        for (int j = 0; j < num; j++)
                        {
                            ArrData[0].Data[cnt] = times[j];

                            int index = TT类型.L功率.no;
                            string field = TT类型.L功率.field;
                            ArrData[index].Data[cnt] = power[j];
                            index = TT类型.L能量.no;
                            ArrData[index].Data[cnt] = energy[j] * 10.0;
                            index = TT类型.L压力.no;
                            ArrData[index].Data[cnt] = press[j] * 100.0;
                            index = TT类型.L转速.no;
                            ArrData[index].Data[cnt] = speed[j];
                            index = TT类型.T温度.no;
                            ArrData[index].Data[cnt] = temp[j];
                            index = TT类型.L栓位.no;
                            ArrData[index].Data[cnt] = (ramPos[j]) / 4;
                            index = TT类型.L电压.no;
                            ArrData[index].Data[cnt++] = voltage[j];
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
        void InitTee()
        {

            try
            {



                #region 显示Teechart样式


                tChart1.Header.Text = "密炼曲线看板";
                tChart1.Header.Font.Size = 20;



                tChart1.Parent = splitContainer1.Panel2;
                tChart1.Dock = DockStyle.Fill;
                tChart1.Aspect.View3D = false;
                tChart1.Walls.View3D = false;
                //图例
                tChart1.Legend.Visible = true;
                tChart1.Legend.Title.Pen.Visible = true;
                tChart1.Legend.Alignment = Steema.TeeChart.LegendAlignments.Right;
                tChart1.Legend.CheckBoxes = true;
                //tChart1.Legend.CustomPosition = true;

                tChart1.Axes.Left.Increment = 10;
                tChart1.Axes.Left.SetMinMax(0, 180);
                tChart1.Axes.Left.Title.Angle = 0;
                tChart1.Axes.Left.Title.Text = "温\r\n度\r\n标\r\n尺\r\n℃";
                tChart1.Axes.Left.Title.Text += "压\r\n力\r\n标\r\n尺\r\nMpa";
                tChart1.Axes.Left.Title.Text += "转\r\n速\r\n标\r\n尺\r\nr";
                tChart1.Axes.Left.Title.Font.Size = 12;
                tChart1.Axes.Left.Title.Font.Color = Color.Blue;

                tChart1.Axes.Right.Labels.Visible = true;
                tChart1.Axes.Right.Increment = 0.1;
                tChart1.Axes.Right.SetMinMax(0, 20);
                tChart1.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.DashDot;
                tChart1.Axes.Right.Title.Angle = 0;
                tChart1.Axes.Right.Title.Text = "功\r\n率\r\n标\r\n尺\r\nkW";
                tChart1.Axes.Right.Title.Font.Size = 12;
                tChart1.Axes.Right.Title.Font.Color = Color.Blue;


                tChart1.Panel.MarginTop = 2;
                tChart1.Panel.MarginBottom = 3;//指定下边框
                tChart1.Panel.MarginLeft = 1;//指定左边框

                #endregion


                #region 添加曲线



                //在设计视图下，已经添加2条曲线
                //AddSeries(TT类型.T背景);
                //AddSeries(TT类型.T温度);

                //左曲线（6条）
                AddSeries(TT类型.L功率);
                AddSeries(TT类型.L能量);
                AddSeries(TT类型.L压力);
                AddSeries(TT类型.L转速);
                AddSeries(TT类型.L栓位);
                AddSeries(TT类型.L电压);

                //此处进行手工设定坐标，很奇怪
                tChart1.Series[TT类型.L功率.no].VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right;

                tChart1.Series[TT类型.L压力.no].VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left;
                tChart1.Series[TT类型.L转速.no].VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left;
                tChart1.Series[TT类型.L栓位.no].VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right;

                tChart1.Series[TT类型.L电压.no].VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right;

                //备注（1条）
                Steema.TeeChart.Styles.Points point = new Steema.TeeChart.Styles.Points();
                point.Marks.Visible = true;
                point.Color = Color.Blue;
                point.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.DownTriangle;
                point.Marks.ArrowLength = 5;
                point.Marks.Font.Name = "宋体";
                point.Marks.ShapeStyle = Steema.TeeChart.Drawing.TextShapeStyle.RoundRectangle;
                point.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Label;
                point.Marks.TextFormat = Steema.TeeChart.Drawing.TextFormat.Normal;


                point.Marks.Font.Size = 9;
                tChart1.AllowDrop = false;
                tChart1.Series.Add(point);




                #endregion

                setLenged();

                //底部坐标显式为时间
                tChart1.Series[0].HorizAxis = Steema.TeeChart.Styles.HorizontalAxis.Bottom;
                tChart1.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value;
                tChart1.Axes.Bottom.Labels.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tChart1.Axes.Bottom.Labels.MultiLine = true;
                // tChart1.Axes.Bottom.Labels.Chart.Width=2;
                cursorTool1.Active = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void AddSeries(Serise属性 S)
        {
            Steema.TeeChart.Styles.FastLine line = new Steema.TeeChart.Styles.FastLine();
            line.ShowInLegend = true;//显示图例
            line.LinePen.Color = S.color;


            line.LinePen.Width = 2;

            line.XValues.DateTime = true;
            line.HorizAxis = Steema.TeeChart.Styles.HorizontalAxis.Bottom;

            if (S.axis == Serise属性.Axis.Left)
            {
                line.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left;
            }
            else
            {
                line.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right;
            }

            tChart1.Series.Add(line);

        }
        void setLenged()
        {
            tChart1.Series[TT类型.T背景.no].Title = "背景";
            tChart1.Series[TT类型.T温度.no].Title = "温度";
            tChart1.Series[TT类型.L功率.no].Title = "功率";
            tChart1.Series[TT类型.L能量.no].Title = "能量";
            tChart1.Series[TT类型.L压力.no].Title = "压力";
            tChart1.Series[TT类型.L转速.no].Title = "转速";
            tChart1.Series[TT类型.L栓位.no].Title = "栓位";
            tChart1.Series[TT类型.L电压.no].Title = "电压";
            tChart1.Series[TT类型.M备注.no].Title = "备注";

            tChart1.Series[tChart1.Series.Count - 1].Title = "信息备注";
        }


        private void tChart1_BeforeDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g)
        {

            if (cursorTool1.Active == false) { return; }

            if (ArrData[TT类型.T温度.no].Data.Length < 10) { return; }

            double val = cursorTool1.XValue;



            if (val > 0)
            {

                int IT时间 = 0;
                int PosKey = finKey(val, ArrData);
                string temp = "时间:" + DateTime.FromOADate(ArrData[IT时间].Data[PosKey]).ToString("HH:mm:ss");
                string strS = "";

                if (ArrData[TT类型.L栓位.no].Data[PosKey].ToString() == "100")
                {
                    strS = "低";
                }
                else if (ArrData[TT类型.L栓位.no].Data[PosKey].ToString() == "200")
                {
                    strS = "中";
                }
                else
                {
                    strS = "高";
                }
                annotation1.Text = string.Format("{0,-45}", temp) + "\r\n" +
                "温度:" + ArrData[TT类型.T温度.no].Data[PosKey].ToString() + "\r\n" +
                "功率:" + (ArrData[TT类型.L功率.no].Data[PosKey]).ToString() + "\r\n" +
                "能量:" + (ArrData[TT类型.L能量.no].Data[PosKey] / 10.0).ToString() + "\r\n" +
                "压力:" + (ArrData[TT类型.L压力.no].Data[PosKey] / 100.0).ToString() + "\r\n" +
                "转速:" + ArrData[TT类型.L转速.no].Data[PosKey].ToString() + "\r\n" +
                "栓位:" + strS + "\r\n" +
                "电压:" + ArrData[TT类型.L电压.no].Data[PosKey].ToString() + "\r\n" +
                "运行时间" + DateTime.FromOADate(ArrData[IT时间].Data[PosKey] - ArrData[IT时间].Data[0]).ToString("HH:mm:ss");

            }
        }

        int finKey(double tagVal, Arr[] arr)
        {
            int st = 0;
            int ed = arr[0].Data.Length - 1;
            int rst = 0;
            if (ed == 0) { return 0; }

            while (rst == 0)
            {
                int mid = (st + ed) / 2;
                double temp = arr[0].Data[mid];

                if (temp == tagVal) { rst = mid; break; }
                if (ed - st == 1)
                {
                    if (ArrData[0].Data[st] == tagVal)
                    {
                        rst = st;
                        break;
                    }
                    else
                    {
                        rst = ed;
                        break;
                    }
                }
                else
                {

                    if (tagVal > temp)
                    {
                        st = mid;
                    }
                    else
                    {
                        if (tagVal < temp)
                        {
                            ed = mid;
                        }
                    }
                }
            }

            return rst;

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(brach);
            if (number >= 2)
                number--;
            else
                return;
            brach = number.ToString();
            Start();
        }
        Steema.TeeChart.Styles.Area save = null;
        private void btnBack_Click(object sender, EventArgs e)
        {
            // save = this.area1;
            int number = Int32.Parse(brach);
//            if (number < orderMD.SetBatch)  //todo  change bug
                number++;
            //else
            //    return;
            brach = number.ToString();

            Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkCur_CheckedChanged_1(object sender, EventArgs e)
        {
            cursorTool1.Active = chkCur.Checked;
        }
        /// <summary>
        /// 打印逻辑，先存入制定目录，调用打印机打印该图片 即可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_print_Click(object sender, EventArgs e)
        {
            string url1 = "C:\\newu\\image";
            string url2 = tChart1.Header.Text.ToString() + ".jpg";
            if (System.IO.Directory.Exists(url1) == false)
            {
                System.IO.Directory.CreateDirectory(url1);
            }
            tChart1.Export.Image.JPEG.Save(url1 + "\\" + url2);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // isOver = !isOver;
        }
    }
}
