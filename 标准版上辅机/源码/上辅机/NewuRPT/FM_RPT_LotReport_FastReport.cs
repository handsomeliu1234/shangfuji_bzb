using NewuTB.Utils;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_LotReport_FastReport : Form
    {
        private RPT_WeightRepository weightRepository;
        private RPT_MixStepRepository mixStepRepository;
        private RPT_DeviceEventRepository deviceEventRepository;
        private PM_OrderTran orderTran;
        private string OrderId = "";

        public FM_RPT_LotReport_FastReport()
        {
            InitializeComponent();
        }

        public FM_RPT_LotReport_FastReport(PM_OrderTran model)
        {
            InitializeComponent();
            weightRepository = new RPT_WeightRepository();
            mixStepRepository = new RPT_MixStepRepository();
            deviceEventRepository = new RPT_DeviceEventRepository();
            orderTran = model;
        }

        private void FM_RPT_LotReport_FastReport_Load(object sender, EventArgs e)
        {
            QueryReport(orderTran);
        }

        public void QueryReport(PM_OrderTran model)
        {
            try
            {
                OrderId = model.OrderID;
                FastReport.Report report = new FastReport.Report();  //实例化
                string path = AppDomain.CurrentDomain.BaseDirectory;
                //母炼
                if (NewuGlobal.SoftConfig.MixType == 0)
                {
                    if (NewuGlobal.SupportLanguage.Equals("1"))
                        path += @"FastReport\BatchReportDetailAll.frx";
                    else
                        path += @"FastReport\BatchReportDetailAll_EN.frx";
                }
                else
                {
                    if (NewuGlobal.SupportLanguage.Equals("1"))
                        path += @"FastReport\BatchReportDetailAll_Final.frx";
                    else
                        path += @"FastReport\BatchReportDetailAll_Final_EN.frx";
                }

                report.Load(path);   //载入报表文件模板

                DataTable rPT_DeviceEvents = deviceEventRepository.GetDeviceEventTable("OrderID='" + OrderId + "' and EventType='作业'", model);
                DataTable rPT_Weight = weightRepository.GetWeightTable("OrderID='" + OrderId + "'", model);
                DataTable rPT_MixStep = mixStepRepository.GetMixStepTable("OrderID='" + OrderId + "'", model);

                DataTable dt_formatDeviceEvent = Utils.FormatDeviceEventValue(rPT_DeviceEvents);
                DataTable dt_formatWeight = Utils.FormatWeightValue(rPT_Weight);
                DataTable dt_formatMixStep = Utils.FormatMixStepValue(rPT_MixStep);

                report.RegisterData(dt_formatDeviceEvent, "RPT_DeviceEvent");
                report.RegisterData(dt_formatWeight, "RPT_Weight");
                report.RegisterData(dt_formatMixStep, "RPT_MixStep");
                report.Preview = previewControl1;   //设置报表Fastreport报表 Preview控件
                report.Prepare();     //准备
                report.ShowPrepared();//显示
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("批量车报表显示").Error(ex.ToString());
            }
        }
    }
}