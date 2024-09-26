using FastReport.DataVisualization.Charting;
using FastReport.MSChart;
using NewuCommon;
using NewuTB.Utils;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Report = FastReport.Report;

namespace NewuRPT
{
    public partial class FM_RPT_BatchReportDetail : Form, IReportForm
    {
        private RPT_WeightRepository weightRepository = new RPT_WeightRepository();
        private RPT_DeviceEventRepository deviceEventRepository = new RPT_DeviceEventRepository();
        private RPT_MixStepRepository mixStepRepository = new RPT_MixStepRepository();
        private RPT_CurveRepository curveRepository = new RPT_CurveRepository();
        private PM_OrderTran orderTran;
        private string factOrder;
        private string[] seriesName = new string[8] { "温度", "功率", "压力", "转速", "能量", "栓位", "电压", "手动" };
        private string[] serieses = new string[8] { "temp", "power", "press", "speed", "energy", "ram", "voltage", "manual" };
        private Color[] colors = new Color[8] { Color.Red, Color.Green, Color.Blue, Color.DarkOrchid, Color.LightGreen, Color.Magenta, Color.Navy, Color.SandyBrown };

        private string OrderId
        {
            get;
            set;
        }

        public FM_RPT_BatchReportDetail()
        {
            InitializeComponent();
        }

        public FM_RPT_BatchReportDetail(PM_OrderTran model, string factOrder)
        {
            InitializeComponent();
            orderTran = model;
            this.factOrder = factOrder;
        }

        private void FM_RPT_BatchReport_Load(object sender, EventArgs e)
        {
            newuNavigator1.NavigatorButtonClick += new NewuNavigator.BtnClick(NewuNavigator1_NavigatorButtonClick);
            GetMaxBatch(orderTran.OrderID);

            if (orderTran == null)
                return;
            newuNavigator1.SetCurrentPageIndex(factOrder);
        }

        private void NewuNavigator1_NavigatorButtonClick(int pageIndex)
        {
            factOrder = newuNavigator1.CurrentPageIndex.ToString();
            QueryReport(orderTran);
        }

        private void GetMaxBatch(string orderId)
        {
            if (orderId == "")
                return;
            int PageCount = weightRepository.GetPageList(orderId, orderTran)[0].FactOrder;
            newuNavigator1.PageCount = PageCount;
        }

        public void QueryReport(PM_OrderTran model)
        {
            try
            {
                OrderId = model.OrderID;
                string path = AppDomain.CurrentDomain.BaseDirectory;
                //母炼
                if (!NewuGlobal.SoftConfig.IsFinalMix())
                {
                    if (NewuGlobal.SupportLanguage.Equals("1"))
                        path += @"FastReport\BatchReportDetail.frx";
                    else
                        path += @"FastReport\BatchReportDetail_EN.frx";
                }
                else
                {
                    if (NewuGlobal.SupportLanguage.Equals("1"))
                        path += @"FastReport\BatchReportDetail_Final.frx";
                    else
                        path += @"FastReport\BatchReportDetail_Final_EN.frx";
                }

                Report report = new Report();
                report.Load(path);
                report.Preview = previewControl1;

                DataTable dt_DeviceEvent = deviceEventRepository.GetDeviceEventTable("OrderID='" + model.OrderID + "' and EventType='作业'  and FactOrder = " + factOrder + "", model);
                DataTable dt_Weight = weightRepository.GetWeightTable("OrderID='" + OrderId + "' and FactOrder = " + factOrder + "", model);
                DataTable dt_formatWeight = Utils.FormatWeightValue(dt_Weight);

                DataTable dt_formatDeviceEvent = Utils.FormatDeviceEventValue(dt_DeviceEvent);
                DataTable dt_MixStep = mixStepRepository.GetMixStepTable("OrderID='" + OrderId + "' and FactOrder = " + factOrder + "", model);
                DataTable dt_formatMixStep = Utils.FormatMixStepValue(dt_MixStep);

                //设计器中绑定数据源
                report.RegisterData(dt_formatDeviceEvent, "RPT_DeviceEvent");
                report.RegisterData(dt_formatWeight, "RPT_Weight");
                report.RegisterData(dt_formatMixStep, "RPT_MixStep");
                InitCurve(report);
                report.Prepare();
                report.ShowPrepared();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_BatchReportDetail").Error(ex.ToString());
            }
        }

        private void InitCurve(Report report)
        {
            RPT_Curve rPT_Curves = curveRepository.GetModel($"OrderID='{OrderId}' and FactOrder={factOrder}", orderTran);

            if (rPT_Curves != null)
            {
                //获取到控件实例
                MSChartObject mSChartObject = report.FindObject("chartCurve") as MSChartObject;
                InitChart(mSChartObject);
                InitSeries(mSChartObject);
                decimal[] times = FormatData(rPT_Curves.RealTime.Trim().Split('/'));
                decimal[] temp = FormatData(rPT_Curves.Temp.Trim().Split('/'), NewuGlobal.SoftConfig.TempValueScale);
                decimal[] power = FormatData(rPT_Curves.Power.Trim().Split('/'), NewuGlobal.SoftConfig.PowerValueScale);
                decimal[] press = FormatData(rPT_Curves.Press.Trim().Split('/'), NewuGlobal.SoftConfig.PressValueScale);
                decimal[] speed = FormatData(rPT_Curves.Speed.Trim().Split('/'), NewuGlobal.SoftConfig.SpeedValueScale);
                decimal[] energy = FormatData(rPT_Curves.Energy.Trim().Split('/'), NewuGlobal.SoftConfig.EnergyValueScale);
                decimal[] reserve1 = FormatData(rPT_Curves.Reserve1.Trim().Split('/'), NewuGlobal.SoftConfig.RamValueScale);
                decimal[] reserve2 = FormatData(rPT_Curves.Reserve2.Trim().Split('/'), NewuGlobal.SoftConfig.VoltageValueScale);
                decimal[] reserve3 = FormatData(rPT_Curves.Reserve3.Trim().Split('/'));

                for (int i = 0; i < times.Length; i++)
                {
                    DateTime date = new DateTime(1970, 1, 1).AddSeconds(double.Parse((i).ToString()));
                    string dateTime = date.ToString("mm:ss");
                    mSChartObject.Chart.Series[0].Points.AddXY(dateTime, temp[i]);
                    mSChartObject.Chart.Series[1].Points.AddXY(dateTime, power[i]);
                    mSChartObject.Chart.Series[2].Points.AddXY(dateTime, press[i]);
                    mSChartObject.Chart.Series[3].Points.AddXY(dateTime, speed[i]);
                    mSChartObject.Chart.Series[4].Points.AddXY(dateTime, energy[i]);
                    mSChartObject.Chart.Series[5].Points.AddXY(dateTime, reserve1[i]);
                    mSChartObject.Chart.Series[6].Points.AddXY(dateTime, reserve2[i]);
                    mSChartObject.Chart.Series[7].Points.AddXY(dateTime, reserve3[i]);
                }
            }
        }

        private void InitChart(MSChartObject mSChartObject)
        {
            mSChartObject.Chart.AntiAliasing = AntiAliasingStyles.All;//抗锯齿,必开项
            mSChartObject.Chart.ChartAreas[0].AxisX.Title = "密炼时间";
            mSChartObject.Chart.ChartAreas[0].AxisX.IsMarginVisible = false;//关闭曲线横坐标从0开始绘制
            mSChartObject.Chart.ChartAreas[0].AxisX.IsMarginVisible = false;//关闭曲线横坐标从0开始绘制
            mSChartObject.Chart.ChartAreas[0].AxisX.MajorTickMark.Enabled = false; //去除X轴延长线
            mSChartObject.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;//设置当前X轴Label的双行显示格式 = 关闭，这个会影响到Y轴线条的显示
            mSChartObject.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = 5; //横坐标间隔
            mSChartObject.AlignXValues = true;
            mSChartObject.Chart.ChartAreas[0].AxisY.MajorTickMark.Enabled = false; //去除Y轴延长线

        }

        private void InitSeries(MSChartObject mSChartObject)
        {
            for (int i = 0; i < serieses.Length; i++)
            {
                Series series = new Series()
                {

                    Name = serieses[i],
                    Color = colors[i],
                    BorderWidth = 2,
                };


                if (NewuGlobal.SupportLanguage.Equals("1"))
                    series.LegendText = seriesName[i];
                else
                    series.LegendText = serieses[i];

                if (i == 7)
                    series.ChartType = SeriesChartType.StepLine;
                else
                    series.ChartType = SeriesChartType.Line;

                mSChartObject.Chart.Series.Add(series);
            }
        }

        private decimal[] FormatData(string[] data, int scaleValue = 1)
        {
            try
            {


                decimal[] dData = new decimal[data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    if (string.IsNullOrEmpty(data[i]))
                        data[i] = "0";
                    dData[i] = decimal.Parse(data[i]) / scaleValue;
                }
                return dData;
            }
            catch (Exception ex)
            {

                NewuGlobal.LogCat("FM_RPT_BatchReportDetail").Error(ex.ToString());
                return null;
            }
        }
    }
}