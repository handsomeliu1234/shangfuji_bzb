using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_DataAnalysis : Form
    {
        private DbHelperSQL sqlClass = new DbHelperSQL(ConnType.NewuSoftData);
        private List<DataD> list = new List<DataD>();

        private string[] type = new string[] { "日生产效率", "月生产效率" };

        public FM_DataAnalysis()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            list.Clear();
            tChart1.Series.Clear();

            string getType = comboBox1.Text.ToString();//获取选择的类型

            string sDate = dateTimePicker1.Value.ToString("yyyyMMdd");//开始日期
            string eDate = dateTimePicker2.Value.ToString("yyyyMMdd");//结束日期

            string yMonth = sDate.Substring(0, 4);//上个月的yyyyMM

            string strSql;
            if (getType.Equals("日生产效率"))
            {
                tChart1.Header.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd") + "日生产效率";
                strSql = "SELECT [MaterialCode] ,[EndTime],[WorkGroup],[WorkOrder]  from  [" + yMonth + "_RPT_DeviceEvent]  where  EndTime>='" + sDate + "' and EndTime<='" + sDate + " 23:59:59' and EventType = '作业' order by EndTime";
                int[] Cnt = new int[30];  // 小时
                int[] Class = new int[4];  //早中晚  三班

                // 根据上述拼接的字符串 查询数据
                DataSet ds = sqlClass.Query(strSql);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Cnt[GetHour(dt.Rows[i][1].ToString())]++;
                            Class[GetClass(dt.Rows[i][3].ToString())]++;
                        }
                    }
                    for (int i = 1; i <= 24; i++)
                    {
                        DataD dataD = new DataD
                        {
                            Dt = new DateTime(1970, 1, 1).AddHours(i),
                            Count = Cnt[i]
                        };
                        list.Add(dataD);
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        DataD dataD = new DataD
                        {
                            Dt = new DateTime(1970, 1, 1).AddHours(24 + i),
                            Count = Class[i]
                        };
                        list.Add(dataD);
                    }
                }
                DrawSerise(list, true);
                //底部坐标显式
                tChart1.Axes.Bottom.Grid.Visible = false;//纵向网格线消失
                tChart1.Axes.Bottom.Visible = true;
                tChart1.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value;
                tChart1.Axes.Bottom.Labels.DateTimeFormat = "HH";
            }
            else if (getType.Equals("月生产效率"))
            {
                tChart1.Header.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " ~ " + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "月生产效率";

                strSql = "SELECT CONVERT(VARCHAR(10),EndTime,120) AS 日期小时,COUNT(*)AS 记录 from  [" + yMonth + "_RPT_DeviceEvent]  where  EndTime>='" + sDate + "' and EndTime<='" + eDate + " 23:59:59' and EventType = '作业' GROUP BY CONVERT(VARCHAR(10),EndTime,120)";

                // 根据上述拼接的字符串 查询数据
                DataSet ds = sqlClass.Query(strSql);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataD dataD = new DataD
                            {
                                Dt = DateTime.Parse(dt.Rows[i][0].ToString()),
                                Count = int.Parse(dt.Rows[i][1].ToString())
                            };
                            list.Add(dataD);
                        }
                    }
                }
                DrawSerise(list, false);
                //底部坐标显式
                tChart1.Axes.Bottom.Grid.Visible = false;//纵向网格线消失
                tChart1.Axes.Bottom.Visible = true;
                tChart1.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value;
                tChart1.Axes.Bottom.Labels.DateTimeFormat = "yyyy-MM-dd";
            }
            else
            {
            }
        }

        private int GetHour(string str)
        {
            DateTime dt = DateTime.Parse(str);
            return dt.Hour;
        }

        private int GetClass(string str)
        {
            switch (str)
            {
                case "早":
                    return 1;

                case "中":
                    return 2;

                case "晚":
                    return 3;
            }
            return 0;
        }

        private void FM_DataAnalysis_Load(object sender, EventArgs e)
        {
            InitChart();
            foreach (var t in type)
            {
                comboBox1.Items.Add(t);
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(type[0]);
            }

            NewuCommon.ColStruct[] mixCols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("dt","时间"),
                new NewuCommon.ColStruct("Count","车数")
            };
            //dgvTech.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            dgv.AllowUserToResizeColumns = true;
            dgv.ReadOnly = true;
            dgv.AddCols(mixCols);
        }

        private void DrawSerise(List<DataD> list, bool isShow)
        {
            try
            {
                for (int i = 1; i <= 1; i++)
                {
                    // Steema.TeeChart.Styles.FastLine series = this.GetSeries(mixSerise.getMixSerise(i, !cbSuperPosition.Checked));

                    Steema.TeeChart.Styles.Bar3D series = this.GetSeries();
                    for (int a = 0; a < list.Count; a++)
                    {
                        DateTime datetime = Convert.ToDateTime(list[a].Dt);
                        DateTime date;
                        if (isShow)
                            date = new DateTime(1970, 1, 1).AddSeconds(a);
                        else
                            date = list[a].Dt;
                        series.Add(datetime, list[a].Count);
                    }

                    this.tChart1.Series.Add(series);
                }
                //dataGridViewEx1.DataSource = list;
                //dataGridView 绑定数据源
                if (isShow)
                {
                    List<DataDD> ld = new List<DataDD>();
                    for (int i = 0; i < 27; i++)
                    {
                        DataDD dd = new DataDD
                        {
                            Dt = (i + 1).ToString()
                        };
                        if (i == 24)
                            dd.Dt = "早";
                        else if (i == 25)
                            dd.Dt = "中";
                        else if (i == 26)
                            dd.Dt = "晚";

                        dd.Count = list[i].Count;
                        ld.Add(dd);
                    }
                    DataDD ddd = new DataDD
                    {
                        Dt = "总",
                        Count = list[26].Count + list[25].Count + list[24].Count
                    };
                    ld.Add(ddd);
                    dgv.DataSource = new BindingList<DataDD>(ld);
                }
                else
                {
                    dgv.DataSource = new BindingList<DataD>(list);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private Steema.TeeChart.Styles.Bar3D GetSeries()
        {
            Steema.TeeChart.Styles.Bar3D line = new Steema.TeeChart.Styles.Bar3D
            {
                ShowInLegend = false//显示图例
            };
            line.XValues.DateTime = true;
            line.HorizAxis = Steema.TeeChart.Styles.HorizontalAxis.Bottom;
            line.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left;
            return line;
        }

        private void InitChart()
        {
            try
            {
                #region 显示Teechart样式

                tChart1.Header.Text = "生产数据分析";
                tChart1.Header.Font.Size = 20;

                tChart1.Dock = DockStyle.Fill;
                tChart1.Aspect.View3D = false;
                tChart1.Walls.View3D = false;
                //图例
                tChart1.Legend.Visible = true;
                tChart1.Legend.Title.Pen.Visible = true;
                tChart1.Legend.Alignment = Steema.TeeChart.LegendAlignments.Right;
                tChart1.Legend.CheckBoxes = true;
                tChart1.Axes.Left.SetMinMax(0, 60);
                //tChart1.Legend.CustomPosition = true;
                tChart1.Axes.Left.Increment = 10;
                tChart1.Axes.Left.AutomaticMinimum = true;
                tChart1.Axes.Left.AutomaticMaximum = true;
                tChart1.Axes.Left.Automatic = true;//自动设置纵坐标
                tChart1.Axes.Left.Title.Angle = 0;
                tChart1.Axes.Left.Title.Text = "生产车数";
                tChart1.Axes.Left.Title.Font.Size = 12;
                tChart1.Axes.Left.Title.Font.Color = Color.Blue;

                tChart1.Axes.Right.Labels.Visible = true;
                tChart1.Axes.Right.Increment = 0.1;
                // tChart1.Axes.Right.SetMinMax(0, 1400);
                tChart1.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.DashDot;
                tChart1.Axes.Right.Title.Angle = 0;
                tChart1.Axes.Right.Title.Text = "";
                tChart1.Axes.Right.Title.Font.Size = 12;
                tChart1.Axes.Right.Title.Font.Color = Color.BlueViolet;

                tChart1.Panel.MarginTop = 2;
                tChart1.Panel.MarginBottom = 3;//指定下边框
                tChart1.Panel.MarginLeft = 1;//指定左边框

                #endregion 显示Teechart样式

                //底部坐标显式
                tChart1.Axes.Bottom.Grid.Visible = false;//纵向网格线消失
                tChart1.Axes.Bottom.Visible = true;
                tChart1.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value;
                tChart1.Axes.Bottom.Labels.DateTimeFormat = "HH";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    internal class DataD
    {
        public int Count { get; set; }
        public DateTime Dt { get; set; }
    }

    internal class DataDD
    {
        public int Count { get; set; }
        public string Dt { get; set; }
    }
}