using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using Steema.TeeChart.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_MixCurve_Super_Super : Form
    {
        public int NBNum = 0;
        private PM_OrderTran orderTran;
        private string orderID = "";
        private string nowBrach = "";
        public const int SERIES_NUM = 12;
        public const int SERIES_NUM_MIX = SERIES_NUM * 512;
        private Arr[] ArrData = new Arr[SERIES_NUM_MIX];
        private MixSerise mixSerise = new MixSerise();
        private double[] arr = null;
        private double[] power;
        private double[] times;
        private double[] energy;
        private double[] press;
        private double[] speed;
        private double[] temp;
        private double[] ramPos;
        private double[] voltage;
        private int[] autoRun;
        private int[] PCU1;
        private int[] PCU2;
        private int[] PCU3;

        /// <summary>
        ///
        /// </summary>
        public FM_MixCurve_Super_Super()
        {
            InitializeComponent();
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
        }

        /// <summary>
        /// 曲线
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="nowBrach"></param>
        /// <param name="orderTranMD"></param>
        public FM_MixCurve_Super_Super(string orderID, string nowBrach, PM_OrderTran orderTranMD)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.nowBrach = nowBrach;
            this.orderTran = orderTranMD;
            for (int i = 0; i < ArrData.Length; i++)
            {
                ArrData[i] = new Arr(0);
            }
            if (NewuGlobal.SoftConfig.IsFinalMix())
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
            this.WindowState = FormWindowState.Maximized;
            InitChart();
            DrawSerise();
            SetControlLanguageText();
        }

        private void DrawSerise()
        {
            if (!cbSuperPosition.Checked)
            {
                tChart1.Series.Clear();
            }
            else
            {
                NBNum++;
            }

            if (orderID == "" || nowBrach == "")
                return;
            if (orderTran == null)
                return;

            tChart1.Header.Text = orderTran.MaterialCode + "第 " + nowBrach + "车曲线 ";

            //  搞一车数据
            GetData(orderID, nowBrach);

            //  将七条曲线添加在图表中 （温度，功率，压力，转速，能量，栓位，电压）
            //  ps: 0中存得是时间  不需要添加曲线到图表中   8，9为显示其是否手动状态，
            for (int i = 1; i <= 11; i++)
            {
                int cc = NBNum * SERIES_NUM + i;

                FastLine series = this.GetSeries(mixSerise.getMixSerise(i, !cbSuperPosition.Checked));
                if (i == 6)
                    series.Stairs = true;

                for (int time = 1; time <= ArrData[cc].Data.Length; time++)
                {
                    DateTime date = new DateTime(1970, 1, 1).AddSeconds(time);
                    series.Add(date.ToOADate(), ArrData[cc].Data[time - 1]);
                }
                if (!GetIsCheacked(i))
                {
                    series.Clear();
                }
                this.tChart1.Series.Add(series);
            }
        }

        private bool GetIsCheacked(int i)
        {
            CheckBox cb = panel1.Controls["checkBox" + i] as CheckBox;
            if (cb == null)
                return false;
            return cb.Checked;
        }

        #region 获取数据

        private void GetData(string orderID, string brach)
        {
            try
            {
                RPT_CurveRepository curveRepository = new RPT_CurveRepository();

                string where = " OrderID = '" + orderID + "' and FactOrder = '" + brach + "'";

                lock (ArrData)
                {
                    RPT_Curve rPT_Curve = curveRepository.GetModel(where, orderTran);
                    int allNum = 0;
                    allNum += Regex.Matches(rPT_Curve.RealTime.ToString(), @"/").Count;

                    int cnt = 0;
                    //当前第几条数据
                    int cc = NBNum * SERIES_NUM;
                    int num = Regex.Matches(rPT_Curve.RealTime.ToString(), @"/").Count;
                    for (int xx = 0; xx < SERIES_NUM; xx++)
                    {
                        ArrData[cc + xx].Redim(allNum);
                        Array.Clear(ArrData[xx].Data, 0, ArrData[xx].Data.Length);
                    }
                    FomatData(rPT_Curve, num);
                    for (int j = 0; j < num; j++)
                    {
                        ArrData[cc].Data[cnt] = times[j];
                        ArrData[cc + 1].Data[cnt] = temp[j];
                        ArrData[cc + 2].Data[cnt] = power[j];
                        ArrData[cc + 3].Data[cnt] = press[j] * 10;
                        ArrData[cc + 4].Data[cnt] = speed[j];
                        ArrData[cc + 5].Data[cnt] = energy[j];
                        ArrData[cc + 6].Data[cnt] = (ramPos[j]) / 3;
                        ArrData[cc + 7].Data[cnt] = voltage[j];
                        ArrData[cc + 8].Data[cnt] = autoRun[j] * 200 - 40;
                        ArrData[cc + 9].Data[cnt] = PCU1[j];
                        ArrData[cc + 10].Data[cnt] = PCU2[j];
                        ArrData[cc + 11].Data[cnt++] = PCU3[j];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FomatData(RPT_Curve rPT_Curve, int maxNum)
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
            PCU1 = new int[maxNum + 10];
            PCU2 = new int[maxNum + 10];
            PCU3 = new int[maxNum + 10];
            Change(rPT_Curve.RealTime.ToString(), maxNum, times);
            Change(rPT_Curve.Power.ToString(), maxNum, power);
            Change(rPT_Curve.Energy.ToString(), maxNum, energy);
            Change(rPT_Curve.Press.ToString(), maxNum, press);
            Change(rPT_Curve.Speed.ToString(), maxNum, speed);
            Change(rPT_Curve.Temp.ToString(), maxNum, temp);
            Change(rPT_Curve.Reserve1.ToString(), maxNum, ramPos);
            Change(rPT_Curve.Reserve2.ToString(), maxNum, voltage);
            Change(rPT_Curve.Reserve3.ToString(), maxNum, autoRun);
            //Change(rPT_Curve.Reserve4.ToString(), maxNum, PCU1, PCU2, PCU3);
        }

        private void Change(string str, int num, int[] pcu1, int[] pcu2, int[] pcu3)
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
                        int ttt = int.Parse(temp);
                        pcu1[cnt] = ttt / 10000;
                        pcu2[cnt] = ttt / 100 % 100;
                        pcu3[cnt++] = ttt % 100;
                    }
                    temp = "";
                    continue;
                }
                temp += str[i];
            }
        }

        private void Change(string str, int num, int[] ans)
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

        private void Change(string str, int num, double[] ans)
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
                        ans[cnt++] = double.Parse(temp);
                    }
                    temp = "";
                    continue;
                }
                temp += str[i];
            }
        }

        #endregion 获取数据

        private void InitChart()
        {
            try
            {
                #region 显示Teechart样式

                tChart1.Header.Text = "密炼曲线看板";
                tChart1.Header.Font.Size = 20;

                tChart1.Parent = panel4;
                tChart1.Dock = DockStyle.Fill;
                tChart1.Aspect.View3D = false;
                tChart1.Walls.View3D = false;
                //图例
                tChart1.Legend.Visible = true;
                tChart1.Legend.Title.Pen.Visible = true;
                tChart1.Legend.Alignment = Steema.TeeChart.LegendAlignments.Right;
                tChart1.Legend.CheckBoxes = true;

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

                #endregion 显示Teechart样式

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

        private FastLine GetSeries(SeriseParam param)
        {
            FastLine line = new FastLine
            {
                ShowInLegend = param.showLegend//显示图例
            };
            line.LinePen.Color = param.color;
            line.Color = param.color;

            line.LinePen.Width = param.width;

            line.XValues.DateTime = true;
            line.HorizAxis = HorizontalAxis.Bottom;

            if (param.Loc == 1)
            {
                line.VertAxis = VerticalAxis.Left;
            }
            else
            {
                line.VertAxis = VerticalAxis.Right;
            }

            line.LabelMember = param.field;

            return line;
        }

        private void TChart1_BeforeDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g)
        {
            int cc = SERIES_NUM * NBNum;
            if (cursorTool1.Active == false)
                return;

            double val = cursorTool1.XValue;
            if (val > 0)
            {
                int PosKey = FinKey(val);
                if (PosKey >= ArrData[cc + 1].Data.Length)
                    PosKey = ArrData[cc + 1].Data.Length;
                PosKey--;

                if (PosKey < 0)
                    return;

                string DataShow = NewuGlobal.GetRes("000685") + ":" + DateTime.FromOADate(ArrData[cc].Data[PosKey]).ToString("HH:mm:ss") + "\r\n" +
                NewuGlobal.GetRes("000655") + ":" + ArrData[cc + 1].Data[PosKey].ToString() + "\r\n" +
                NewuGlobal.GetRes("000656") + ":" + (ArrData[cc + 2].Data[PosKey]).ToString() + "\r\n" +
                NewuGlobal.GetRes("000659") + ":" + (ArrData[cc + 5].Data[PosKey]).ToString() + "\r\n" +
                NewuGlobal.GetRes("000657") + ":" + (ArrData[cc + 3].Data[PosKey] / 10).ToString() + "\r\n" +
                NewuGlobal.GetRes("000658") + ":" + ArrData[cc + 4].Data[PosKey].ToString() + "\r\n" +

                NewuGlobal.GetRes("000661") + ":" + ArrData[cc + 7].Data[PosKey].ToString() + "\r\n" +
                "PCU1:" + ArrData[cc + 9].Data[PosKey].ToString() + "\r\n" +
                "PCU2:" + ArrData[cc + 10].Data[PosKey].ToString() + "\r\n" +
                "PCU3:" + ArrData[cc + 11].Data[PosKey].ToString() + "\r\n";
                tv_DataShow.Text = DataShow;
            }
        }

        private int FinKey(double tagVal)
        {
            if (arr == null)
            {
                if (NewuGlobal.SoftConfig.IsFinalMix())
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
            if (ed == 0)
            {
                return 0;
            }
            if (ed > arr.Length)
            {
                ed = arr.Length;
            }

            while (rst == 0)
            {
                int mid = (st + ed) / 2;
                double temp = arr[mid];

                if (temp == tagVal)
                {
                    rst = mid;
                    break;
                }
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

        private void BtnPre_Click(object sender, EventArgs e)
        {
            int number = int.Parse(nowBrach);
            if (number >= 2)
                number--;
            else
                return;
            nowBrach = number.ToString();
            DrawSerise();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            int number = int.Parse(nowBrach);
            if (number < orderTran.SetBatch)  //todo  change bug
            {
                number++;
            }
            else
            {
                return;
            }

            nowBrach = number.ToString();

            DrawSerise();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打印逻辑，先存入制定目录，调用打印机打印该图片 即可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_print_Click(object sender, EventArgs e)
        {
            //根据订单Id获取此订单的实体
            PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
            PM_OrderTran pM_OrderTran = orderTranRepository.GetModel(orderID);
            //把图片保存到订单启动时间路径
            string orderDatePath = Convert.ToDateTime(pM_OrderTran.StartTime).ToString(@"yyyy\/MM\/dd\/") + Convert.ToDateTime(pM_OrderTran.StartTime).ToString("yyyy-MM-dd-HH-mm-ss");
            string url1 = "C:/newu/image/" + orderDatePath;
            //除去曲线名称中带有歧义的字符
            string url2 = tChart1.Header.Text.ToString().Replace("/", "-").Replace(" ", "") + ".jpg";
            if (System.IO.Directory.Exists(url1) == false)
            {
                System.IO.Directory.CreateDirectory(url1);
            }
            tChart1.Export.Image.JPEG.Save(url1 + "/" + url2);
            MessageBox.Show("打印成功,请到文件夹" + url1 + "里查看！");
        }

        private void CbSuperPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSuperPosition.Checked)
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

        private void ChkCur_CheckedChanged(object sender, EventArgs e)
        {
            cursorTool1.Active = chkCur.Checked;
        }

        private void CheckBoxClick_showSerise(SeriseParam seriseParam, bool isCheack)
        {
            string str = seriseParam.field;
            if (!isCheack)
            {
                foreach (Series series in tChart1.Series)
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
                foreach (Series series in tChart1.Series)
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

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(1), checkBox1.Checked);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(2), checkBox2.Checked);
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(3), checkBox3.Checked);
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(4), checkBox4.Checked);
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(5), checkBox5.Checked);
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(6), checkBox6.Checked);
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(7), checkBox7.Checked);
        }

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(8), checkBox8.Checked);
        }

        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(9), checkBox9.Checked);
        }

        private void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(10), checkBox10.Checked);
        }

        private void CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxClick_showSerise(mixSerise.getMixSerise(11), checkBox11.Checked);
        }

        private int LX = 200;
        private int RX = 1400;

        private void Add_x_Click(object sender, EventArgs e)
        {
            LX += 200;
            RX += 200;
            tChart1.Axes.Left.SetMinMax(0, LX);
            tChart1.Axes.Right.SetMinMax(0, RX);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (LX >= 50)
                LX -= 25;
            if (RX >= 150)
                RX -= 25;
            tChart1.Axes.Left.SetMinMax(0, LX);
            tChart1.Axes.Right.SetMinMax(0, RX);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FM_RPT_BatchReportDetail fm = new FM_RPT_BatchReportDetail(orderTran, nowBrach)
            {
                Owner = this
            };
            fm.ShowDialog();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            this.Text = NewuGlobal.GetRes("000649");// *

            btnPre.Text = NewuGlobal.GetRes("000650");// *
            btnBack.Text = NewuGlobal.GetRes("000651");// *
            chkCur.Text = NewuGlobal.GetRes("000652");// *
            cbSuperPosition.Text = NewuGlobal.GetRes("000653");// *
            lab.Text = NewuGlobal.GetRes("000654");// *
            checkBox1.Text = NewuGlobal.GetRes("000655") + " ❤";// *
            checkBox2.Text = NewuGlobal.GetRes("000656") + " ❤";// *
            checkBox3.Text = NewuGlobal.GetRes("000657") + " ❤";// *
            checkBox4.Text = NewuGlobal.GetRes("000658") + " ❤";// *
            checkBox5.Text = NewuGlobal.GetRes("000659") + " ❤";// *
            checkBox6.Text = NewuGlobal.GetRes("000660") + " ❤";// *
            checkBox7.Text = NewuGlobal.GetRes("000661") + " ❤";// *
            checkBox8.Text = NewuGlobal.GetRes("000662") + " ❤";// *

            tv_DataShow.Text = NewuGlobal.GetRes("000666") + "：";// *数据
            button3.Text = NewuGlobal.GetRes("000671");// *该车报表
            add_x.Text = NewuGlobal.GetRes("000669");// *X轴增加
            button2.Text = NewuGlobal.GetRes("000670");// *X轴减少
            bt_print.Text = NewuGlobal.GetRes("000668");// *打印
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnPre.Size = btnBack.Size = new Size(72, 30);
                add_x.Size = new Size(72, 30);
                add_x.Location = new Point(14, 20);
                button2.Size = new Size(72, 30);
                button2.Location = new Point(101, 20);
                bt_print.Size = new Size(72, 30);
                bt_print.Location = new Point(189, 20);
                button3.Size = new Size(117, 30);
                button3.Location = new Point(277, 20);
            }
            else
            {
                btnPre.Size = btnBack.Size = new Size(90, 30);
                add_x.Size = new Size(105, 30);
                add_x.Location = new Point(14, 20);
                button2.Size = new Size(113, 30);
                button2.Location = new Point(134, 20);
                bt_print.Size = new Size(72, 30);
                bt_print.Location = new Point(263, 20);
                button3.Size = new Size(100, 30);
                button3.Location = new Point(351, 20);

                checkBox1.Location = new Point(1, 29);
                checkBox2.Location = new Point(1, 51);
                checkBox3.Location = new Point(1, 73);
                checkBox4.Location = new Point(1, 95);
                checkBox5.Location = new Point(1, 117);
                checkBox6.Location = new Point(1, 139);
                checkBox7.Location = new Point(1, 161);
                checkBox8.Location = new Point(1, 183);
                checkBox9.Location = new Point(1, 205);
                checkBox10.Location = new Point(1, 227);
                checkBox11.Location = new Point(1, 249);
            }
        }
    }
}