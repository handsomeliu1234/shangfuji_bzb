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
using NewuBLL;
namespace NewuView
{

    public partial class FM_MixCurve_Super_Super : Form
    {
        public int NBNum = 0;
        private PM_OrderTranMDL orderTranMD = null;
        private string orderID = "";
        private string nowBrach = "";
        public const int SERIES_NUM = 9;
        public const int SERIES_NUM_MIX = SERIES_NUM * 512;
        Arr[] ArrData = new Arr[SERIES_NUM_MIX];
        MixSerise mixSerise = new MixSerise();

        public FM_MixCurve_Super_Super()
        {
            InitializeComponent();
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
        }
        public FM_MixCurve_Super_Super(string orderID, string nowBrach, PM_OrderTranMDL orderTranMD)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.nowBrach = nowBrach;
            this.orderTranMD = orderTranMD;
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
            if (NewuBLL.NewuGlobal.SoftConfig.IsFinalMix())
            {
                RX = 1400;
                LX = 200;
            }
            else
            {
                RX = 2200;
                LX = 200;
            }
        }
        private void FM_MixCurve_Super_Super_Load(object sender, EventArgs e)
        {
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.WindowState = FormWindowState.Maximized;
            //InitChart();
            //Start();
            InitCheakBox();
            InitChart();
            DrawSerise();
        }
        CheckBox[] cbb = new CheckBox[9];
        private void InitCheakBox()
        {
        }
        private void DrawSerise()
        {
            //数据量
            //tLabNum.Text = "数据量：" + ArrData[0].Data.Length.ToString() + " 笔";

            if (!cbSuperPosition.Checked)
            {
                tChart1.Series.Clear();
            }
            else
            {
                NBNum++;
            }
            if (orderID == "" || nowBrach == "") return;
            if (orderTranMD == null) return;
            tChart1.Header.Text = orderTranMD.MaterialCode + "第 " + nowBrach + "车曲线 ";
            //  搞一车数据
            GetData(orderID, nowBrach);
            //  将七条曲线添加在图表中 （温度，功率，压力，转速，能量，栓位，电压）
            //  ps: 0中存得是时间  不需要添加曲线到图表中   8，9为显示其是否手动状态，
            for (int i = 1; i <= 8; i++)
            {
                int cc = NBNum * SERIES_NUM + i;

                Steema.TeeChart.Styles.FastLine series = this.GetSeries(mixSerise.getMixSerise(i, !cbSuperPosition.Checked));

                for (int time = 1; time <= ArrData[cc].Data.Length; time++)
                {
                    DateTime date = new DateTime(1970, 1, 1).AddSeconds(time);
                    series.Add(date.ToOADate(), ArrData[cc].Data[time - 1]); ///@@@@@!!!!!!!!!!!!!
                }
                if (!getIsCheacked(i))
                {
                    series.Clear();
                }
                this.tChart1.Series.Add(series);
            }

        }
        private bool getIsCheacked(int i)
        {
            CheckBox cb = this.panel1.Controls["checkBox" + i] as CheckBox;
            return cb.Checked;
        }
        #region   获取数据
        void GetData(string orderID, string brach)
        {


            try
            {
                NewuBLL.RPT_CurveBLL curveBll = new NewuBLL.RPT_CurveBLL(orderTranMD);

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

                    int cnt = 0;
                    //当前第几条数据
                    int cc = NBNum * SERIES_NUM;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int num = Regex.Matches(row["RealTime"].ToString(), @"/").Count;
                        for (int xx = 0; xx < SERIES_NUM; xx++)
                        {
                            ArrData[cc + xx].Redim(allNum);
                            // ArrData[cc + xx].Redim(num+10);
                            Array.Clear(ArrData[xx].Data, 0, ArrData[xx].Data.Length);
                        }
                        FomatData(row, num);
                        for (int j = 0; j < num; j++)
                        {
                            ArrData[cc].Data[cnt] = times[j];
                            ArrData[cc + 1].Data[cnt] = temp[j];
                            ArrData[cc + 2].Data[cnt] = power[j];
                            ArrData[cc + 3].Data[cnt] = press[j] * 100;
                            ArrData[cc + 4].Data[cnt] = speed[j];
                            ArrData[cc + 5].Data[cnt] = energy[j] * 2;
                            ArrData[cc + 6].Data[cnt] = (ramPos[j]) / 3;
                            ArrData[cc + 7].Data[cnt] = voltage[j];
                            ArrData[cc + 8].Data[cnt++] = autoRun[j] * 200 - 40;
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
        #endregion
        void InitChart()
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
                tChart1.Axes.Left.SetMinMax(0, LX);
                tChart1.Axes.Left.Title.Angle = 0;
                tChart1.Axes.Left.Title.Text = "温\r\n度\r\n标\r\n尺\r\n℃";
                tChart1.Axes.Left.Title.Text += "压\r\n力\r\n标\r\n尺\r\nMpa";
                tChart1.Axes.Left.Title.Text += "转\r\n速\r\n标\r\n尺\r\nr";
                tChart1.Axes.Left.Title.Font.Size = 12;
                tChart1.Axes.Left.Title.Font.Color = Color.Blue;

                tChart1.Axes.Right.Labels.Visible = true;
                tChart1.Axes.Right.Increment = 0.1;
                tChart1.Axes.Right.SetMinMax(0, RX);
                tChart1.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.DashDot;
                tChart1.Axes.Right.Title.Angle = 0;
                tChart1.Axes.Right.Title.Text = "功\r\n率\r\n标\r\n尺\r\nkW";
                tChart1.Axes.Right.Title.Font.Size = 12;
                tChart1.Axes.Right.Title.Font.Color = Color.Blue;


                tChart1.Panel.MarginTop = 2;
                tChart1.Panel.MarginBottom = 3;//指定下边框
                tChart1.Panel.MarginLeft = 1;//指定左边框

                #endregion

                //底部坐标显式
                tChart1.Axes.Bottom.Visible = true;
                tChart1.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value;
                tChart1.Axes.Bottom.Labels.DateTimeFormat = "m:ss";
                cursorTool1.Active = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        Steema.TeeChart.Styles.FastLine GetSeries(SeriseParam param)
        {
            Steema.TeeChart.Styles.FastLine line = new Steema.TeeChart.Styles.FastLine();
            line.ShowInLegend = param.showLegend;//显示图例
            line.LinePen.Color = param.color;
            line.Color = param.color;

            line.LinePen.Width = param.width;

            line.XValues.DateTime = true;
            line.HorizAxis = Steema.TeeChart.Styles.HorizontalAxis.Bottom;

            if (param.Loc == 1)
            {
                line.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left;
            }
            else
            {
                line.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right;
            }

            line.LabelMember = param.field;

            return line;

        }

        private void tChart1_BeforeDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g)
        {
            int cc = SERIES_NUM * NBNum;
            if (cursorTool1.Active == false) { return; }


            double val = cursorTool1.XValue;
            if (val > 0)
            {
                int PosKey = finKey(val);
                if (PosKey >= ArrData[cc + 1].Data.Length)
                    PosKey = ArrData[cc + 1].Data.Length;
                PosKey--;
                //string temp = "时间:" + DateTime.FromOADate(ArrData[IT时间].Data[PosKey]).ToString("HH:mm:ss");
                string DataShow = "时间:" + DateTime.FromOADate(ArrData[cc].Data[PosKey]).ToString("HH:mm:ss") + "\r\n" +
                "温度:" + ArrData[cc + 1].Data[PosKey].ToString() + "\r\n" +
                "功率:" + (ArrData[cc + 2].Data[PosKey]).ToString() + "\r\n" +
                "能量:" + (ArrData[cc + 5].Data[PosKey] / 2.0).ToString() + "\r\n" +
                "压力:" + (ArrData[cc + 3].Data[PosKey] / 100.0).ToString() + "\r\n" +
                "转速:" + ArrData[cc + 4].Data[PosKey].ToString() + "\r\n" +
                    // "栓位:" + strS + "\r\n" +
                "电压:" + ArrData[cc + 7].Data[PosKey].ToString() + "\r\n";
                tv_DataShow.Text = DataShow;
                //"运行时间" + DateTime.FromOADate(ArrData[IT时间].Data[PosKey] - ArrData[IT时间].Data[0]).ToString("HH:mm:ss");

            }
        }
        double[] arr = null;
        int finKey(double tagVal)
        {
            if (arr == null)
            {
                if (NewuBLL.NewuGlobal.SoftConfig.IsFinalMix())
                {
                    arr = new double[180];
                    for (int i = 0; i < 180; i++)
                    {
                        arr[i] = new DateTime(1970, 1, 1).AddSeconds(i).ToOADate();
                    }
                }
                else
                {
                    arr = new double[360];
                    for (int i = 0; i < 360; i++)
                    {
                        arr[i] = new DateTime(1970, 1, 1).AddSeconds(i).ToOADate();
                    }
                }
            }
            int st = 0;
            int ed = arr.Length - 1;
            int rst = 0;
            if (ed == 0) { return 0; }
            if (ed > arr.Length)
            {
                ed = arr.Length;
            }

            while (rst == 0)
            {
                int mid = (st + ed) / 2;
                double temp = arr[mid];

                if (temp == tagVal) { rst = mid; break; }
                if (ed - st == 1)
                {
                    if (arr[st] == tagVal)
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
            int number = Int32.Parse(nowBrach);
            if (number >= 2)
                number--;
            else
                return;
            nowBrach = number.ToString();
            DrawSerise();
        }
        Steema.TeeChart.Styles.Area save = null;
        private void btnBack_Click(object sender, EventArgs e)
        {
            // save = this.area1;
            int number = Int32.Parse(nowBrach);
            //            if (number < orderMD.SetBatch)  //todo  change bug
            number++;
            //else
            //    return;
            nowBrach = number.ToString();

            //Start();
            DrawSerise();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打印逻辑，先存入制定目录，调用打印机打印该图片 即可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_print_Click(object sender, EventArgs e)
        {
            //根据订单Id获取此订单的实体
            var orderBLL = new PM_OrderTranBLL();
            var orderTran = orderBLL.GetModel(orderID);
            //把图片保存到订单启动时间路径
            string orderDatePath = Convert.ToDateTime(orderTran.StartTime).ToString(@"yyyy\/MM\/dd\/") + Convert.ToDateTime(orderTran.StartTime).ToString("yyyy-MM-dd-HH-mm-ss");
            string url1 = "C:/newu/image/" + orderDatePath;
            //除去曲线名称中带有歧义的字符
            string url2 = tChart1.Header.Text.ToString().Replace("/", "-").Replace(" ", "") + ".jpg";
            if (System.IO.Directory.Exists(url1) == false)
            {
                System.IO.Directory.CreateDirectory(url1);
            }
            tChart1.Export.Image.JPEG.Save(url1 + "/" + url2);
            MessageBox.Show("打印成功,请到文件夹"+url1+"里查看！");
            /*
            string url1 = "C:\\newu\\image";
            string url2 = tChart1.Header.Text.ToString() + ".jpg";
            string urlAim = "";
            foreach (var c in url2) {
                if (c != '/')
                {
                    urlAim += c;
                }
                else
                {
                    urlAim += '-';
                }
            }

            if (System.IO.Directory.Exists(url1) == false)
            {
                System.IO.Directory.CreateDirectory(url1);
            }
            tChart1.Export.Image.JPEG.Save(url1 + "\\" + urlAim);
             * */
        }
        private void cbSuperPosition_CheckedChanged(object sender, EventArgs e)
        {
            //cbSSuperPosiontion = cbSuperPosition.Checked;
            if (cbSuperPosition.Checked == true)
            {
                //ArrData = new Arr[300];
            }
            else
            {
                chkCur.Checked = false;
                ArrData = new Arr[SERIES_NUM_MIX];
                NBNum = 0;
                for (int i = 0; i < ArrData.Length; i++)
                {
                    ArrData[i] = new Arr(0);
                }
            }
        }

        private void chkCur_CheckedChanged(object sender, EventArgs e)
        {
            cursorTool1.Active = chkCur.Checked;
        }

        private void checkBoxClick_showSerise(SeriseParam seriseParam, bool isCheack)
        {
            string str = seriseParam.field;
            if (!isCheack)
            {
                foreach (Steema.TeeChart.Styles.Series series in tChart1.Series)
                {
                    if (series.LabelMember == str)
                    {
                        series.Clear();
                    }
                }
            }
            else
            {
                int count = 0;
                foreach (Steema.TeeChart.Styles.Series series in tChart1.Series)
                {
                    if (series.LabelMember == str)
                    {
                        int cc = count * SERIES_NUM + +seriseParam.No;
                        for (int time = 1; time <= ArrData[cc].Data.Length; time++)
                        {
                            DateTime date = new DateTime(1970, 1, 1).AddSeconds(time);
                            series.Add(date.ToOADate(), ArrData[cc].Data[time - 1]);
                        }
                        count++;
                    }
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(1), checkBox1.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(2), checkBox2.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            checkBoxClick_showSerise(mixSerise.getMixSerise(3), checkBox3.Checked);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(4), checkBox4.Checked);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(5), checkBox5.Checked);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(6), checkBox6.Checked);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(7), checkBox7.Checked);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxClick_showSerise(mixSerise.getMixSerise(8), checkBox8.Checked);
        }
        int LX = 200;
        int RX = 1400;
        private void add_x_Click(object sender, EventArgs e)
        {
            LX += 200;
            RX += 200;
            tChart1.Axes.Left.SetMinMax(0, LX);
            tChart1.Axes.Right.SetMinMax(0, RX);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LX >= 50)
                LX -= 25;
            if (RX >= 150)
                RX -= 25;
            tChart1.Axes.Left.SetMinMax(0, LX);
            tChart1.Axes.Right.SetMinMax(0, RX);

        }

    }
}
