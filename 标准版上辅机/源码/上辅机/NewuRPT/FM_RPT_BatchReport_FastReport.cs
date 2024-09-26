using FastReport;
using NewuTB.Utils;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_BatchReport_FastReport : Form
    {
        private PM_OrderTran orderTran;
        private PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private RPT_WeightRepository weightRepository;
        private RPT_DeviceEventRepository deviceEventRepository = new RPT_DeviceEventRepository();

        private string OrderId
        {
            get;
            set;
        }

        public FM_RPT_BatchReport_FastReport()
        {
            InitializeComponent();
        }

        public FM_RPT_BatchReport_FastReport(PM_OrderTran model)
        {
            InitializeComponent();
            orderTran = model;
        }

        public void QueryReport(PM_OrderTran model)
        {
            try
            {
                if (model.OrderID == OrderId)
                    return;
                OrderId = model.OrderID;
                weightRepository = new RPT_WeightRepository();
                Report report = new Report();  //实例化
                string path = AppDomain.CurrentDomain.BaseDirectory;

                //母炼
                if (NewuGlobal.SupportLanguage.Equals("1"))
                    path += @"FastReport\EachReport.frx";
                else
                    path += @"FastReport\EachReport_EN.frx";
                report.Load(path);   //载入报表文件模板
                //填充数据
                List<RPT_Weight> listRawWeight = weightRepository.GetList(0, "OrderID='" + OrderId + "'", "SetBinNo", model);

                //计算实际总重量
                double sumActWeight = 0;
                for (int i = 0, count = listRawWeight.Count; i < count; i++)
                {
                    sumActWeight += Convert.ToDouble(listRawWeight[i].ActWeight);
                }
                DataTable orderTran = orderTranRepository.GetListTable(OrderId);
                DataTable rPT_Weights = weightRepository.GetBatchReportTable(OrderId, model);
                DataTable dt_formatWeightTable = Utils.FormatExecValue(rPT_Weights);

                report.SetParameterValue("OrderId", OrderId);
                report.RegisterData(orderTran, "PM_OrderTranTable");
                report.RegisterData(dt_formatWeightTable, "RPT_WeightTable");
                report.Preview = previewControl1;   //设置报表Fastreport报表 Preview控件
                report.Prepare();     //准备
                report.ShowPrepared();//显示
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_BatchReport_FastReport").Error(ex.ToString());
            }
        }

        private void FM_RPT_BatchReport_FastReport_Load(object sender, EventArgs e)
        {
            if (orderTran == null)
                return;
            QueryReport(orderTran);
        }
    }
}