using MultiLanguage;
using NewuCommon;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NewuRPT
{
    /// <summary>
    /// 生产效率统计
    /// </summary>
    public partial class FM_ProductionStatistics : Form, ILanguageChanged
    {
        private SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 注意使用时更新其Year属性
        /// 停起操作
        /// </summary>
        private RPT_DeviceEventRepository deviceEventRepo = new RPT_DeviceEventRepository();

        private PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private List<OrderStatistics> orderStatisticsList;
        private List<AvgOrderStatistics> allAvgList;

        /// <summary>
        /// 生产效率
        /// </summary>
        public FM_ProductionStatistics()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据导出excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = @"D:\";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                var newFile = new FileInfo(fileName);
                if (newFile.Exists)
                {
                    try
                    {
                        newFile.Delete();  // ensures we create a new workbook
                        newFile = new FileInfo(fileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                if (allAvgList == null && orderStatisticsList == null)
                {
                    MessageBox.Show("数据为空！");
                    return;
                }
                using (var package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("生产效率");
                    //添加表头
                    using (var range = worksheet.Cells["A1:O1"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    int a = orderStatisticsList.Count + 1;
                    worksheet.Cells.Style.ShrinkToFit = true;
                    worksheet.Cells[1, 1].Value = "机台";
                    worksheet.Cells[1, 2].Value = "配方";
                    worksheet.Cells[1, 3].Value = "批号";
                    worksheet.Cells[1, 4].Value = "设定车数";
                    worksheet.Cells[1, 5].Value = "完成车数";
                    worksheet.Cells[1, 6].Value = "平均炼胶";
                    worksheet.Cells[1, 7].Value = "平均配方";
                    worksheet.Cells[1, 8].Value = "平均间隔";
                    worksheet.Cells[1, 9].Value = "设定间隔";
                    worksheet.Cells[1, 10].Value = "有效时间";
                    worksheet.Cells[1, 11].Value = "下单时间";
                    worksheet.Cells[1, 12].Value = "开始时间";
                    worksheet.Cells[1, 13].Value = "结束时间";
                    worksheet.Cells[1, 14].Value = "生产时间";
                    worksheet.Cells[1, 15].Value = "生产效率";
                    worksheet.Column(2).Width = 20;//设置列宽
                    worksheet.Column(11).Width = 20;//设置列宽
                    worksheet.Column(12).Width = 20;//设置列宽
                    worksheet.Column(13).Width = 20;//设置列宽

                    int rowIndex = 1;
                    //填充数据
                    for (int i = 0; i < orderStatisticsList.Count; i++)
                    {
                        rowIndex++;
                        worksheet.Cells[rowIndex, 1].Value = orderStatisticsList[i].DeviceCode;
                        worksheet.Cells[rowIndex, 2].Value = orderStatisticsList[i].MaterialCode;
                        worksheet.Cells[rowIndex, 3].Value = orderStatisticsList[i].Lot;
                        worksheet.Cells[rowIndex, 4].Value = orderStatisticsList[i].SetBatch;
                        worksheet.Cells[rowIndex, 5].Value = orderStatisticsList[i].RealBatch;
                        worksheet.Cells[rowIndex, 6].Value = orderStatisticsList[i].AvgLianJiaoTime;
                        worksheet.Cells[rowIndex, 7].Value = orderStatisticsList[i].AvgFormulaTime;
                        worksheet.Cells[rowIndex, 8].Value = orderStatisticsList[i].AvgJianGeTime;
                        worksheet.Cells[rowIndex, 9].Value = orderStatisticsList[i].SetJianGeTime;
                        worksheet.Cells[rowIndex, 10].Value = orderStatisticsList[i].RealTime;
                        worksheet.Cells[rowIndex, 11].Value = ((DateTime)orderStatisticsList[i].SaveTime).ToString("yyyy/MM/dd HH:mm:ss");
                        if (orderStatisticsList[i].StartTime != null)
                        {
                            worksheet.Cells[rowIndex, 12].Value = ((DateTime)orderStatisticsList[i].StartTime).ToString("yyyy/MM/dd HH:mm:ss");
                        }
                        if (orderStatisticsList[i].EndTime != null)
                        {
                            worksheet.Cells[rowIndex, 13].Value = ((DateTime)orderStatisticsList[i].EndTime).ToString("yyyy/MM/dd HH:mm:ss");
                        }

                        worksheet.Cells[rowIndex, 14].Value = orderStatisticsList[i].ProduceTime;
                        worksheet.Cells[rowIndex, 15].Value = orderStatisticsList[i].ProductionEfficeTive;
                    }
                    rowIndex++;
                    if (allAvgList.Count == 1)
                    {
                        worksheet.Cells[rowIndex, 10].Value = allAvgList[0].AvgRealTime.ToString("0.00");
                        worksheet.Cells[rowIndex, 14].Value = allAvgList[0].AvgProduceTime.ToString("0.00");
                        worksheet.Cells[rowIndex, 15].Value = allAvgList[0].AvgProductionEfficeTive;
                    }

                    package.Save();
                    MessageBox.Show("导出成功!");
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);
        }

        /// <summary>
        /// 数据载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_ProductionStatistics_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);

            List<SYS_Device> list = deviceRepository.GetList("");
            cmb_DeviceName.DataSource = list;
            cmb_DeviceName.ValueMember = "DeviceID";
            cmb_DeviceName.DisplayMember = "DeviceName";

            ColStruct[] cols = new ColStruct[]
            {
                new ColStruct("DeviceCode","设备"),
                new ColStruct("MaterialCode","配方"),
                new ColStruct("Lot","批号"),
                new ColStruct("SetBatch","设定车数"),
                new ColStruct("RealBatch","实际车数"),
                new ColStruct("AvgLianJiaoTime","平均炼胶时间"),
                new ColStruct("AvgFormulaTime","平均配方时间"),
                new ColStruct("AvgJianGeTime","平均间隔时间"),
                new ColStruct("SetJianGeTime","设定间隔时间"),
                new ColStruct("RealTime","有效时间"),
                new ColStruct("SaveTime","下单时间"),
                new ColStruct("StartTime","开始时间"),
                new ColStruct("EndTime","结束时间"),
                new ColStruct("ProduceTime","生产时间"),
                new ColStruct("ProductionEfficeTive","生产效率"),
            };

            dgv1.AllowUserToAddRows = false;
            dgv1.AddCols(cols);
            dgv1.ReadOnly = true;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv1.Columns["EndTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dgv1.Columns["StartTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dgv1.Columns["EndTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            cols = new ColStruct[]
            {
                new ColStruct("AvgRealTime","平均有效时间"),
                new ColStruct("AvgProduceTime","平均生产时间"),
                new ColStruct("AvgProductionEfficeTive","平均生产效率"),
            };

            dgv2.AllowUserToAddRows = false;
            dgv2.AddCols(cols);
            dgv2.ReadOnly = true;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv2.Columns["AvgRealTime"].DefaultCellStyle.Format = "0.00";
            dgv2.Columns["AvgProduceTime"].DefaultCellStyle.Format = "0.00";
            SetLanguage();
            GetData();
        }

        /// <summary>
        ///
        /// </summary>
        private void GetData()
        {
            try
            {
                string deviceid = cmb_DeviceName.SelectedValue as string;
                if (startTime.Value.Year != endTime.Value.Year)
                {
                    endTime.Value = startTime.Value.AddYears(1).AddDays(-startTime.Value.AddYears(1).DayOfYear).AddDays(1).AddSeconds(-1);
                }
                List<PM_OrderTran> orderList = orderTranRepository.GetList(deviceid, startTime.Value, endTime.Value);

                orderStatisticsList = new List<OrderStatistics>();
                //根据orderId计算各种查询设备["作业"]记录,计算各种时间和生产效率
                int totalRealTime = 0;
                int totalProductTime = 0;
                foreach (var order in orderList)
                {
                    OrderStatistics item = new OrderStatistics
                    {
                        DeviceCode = order.DeviceName,
                        MaterialCode = order.MaterialCode,
                        Lot = order.Lot,
                        SetBatch = order.SetBatch
                    };
                    if (FunClass.VVal(order.Reserve2) != 0)
                    {
                        item.SetJianGeTime = int.Parse(order.Reserve2);
                    }
                    item.SaveTime = order.Savetime;
                    item.StartTime = order.StartTime;
                    item.EndTime = order.EndTime;
                    if (order.StartTime != null && order.EndTime != null)
                    {
                        item.ProduceTime = (int)((order.EndTime - order.StartTime).Value.TotalSeconds);
                    }

                    deviceEventRepo.TableYear = order.Savetime.Year; //important

                    List<OrderAvgData> agvdataList = deviceEventRepo.GetAvgOrderData(order.OrderID);
                    int AvgLianJiaoTime = 0;
                    int AvgFormulaTime = 0;
                    int AvgJianGeTime = 0;
                    int RealTime = 0;
                    if (agvdataList.Count == 1)
                    {
                        AvgJianGeTime = agvdataList[0].AvgJianGeTime;
                        AvgLianJiaoTime = agvdataList[0].AvgLianJiaoTime;
                        AvgFormulaTime = agvdataList[0].AvgUseTime;
                        RealTime = (AvgFormulaTime + item.SetJianGeTime) * agvdataList[0].FactNum;
                        item.RealBatch = agvdataList[0].FactNum;
                    }

                    item.AvgJianGeTime = AvgJianGeTime;
                    item.AvgLianJiaoTime = AvgLianJiaoTime;
                    item.AvgFormulaTime = AvgFormulaTime;
                    item.RealTime = RealTime;

                    if (item.ProduceTime != 0)
                    {
                        item.ProductionEfficeTive = (((decimal)RealTime / (decimal)item.ProduceTime) * 100).ToString("0.00") + "%";
                    }
                    else
                    {
                        item.ProductionEfficeTive = "0.00%";
                    }

                    totalRealTime += RealTime;
                    totalProductTime += item.ProduceTime;
                    orderStatisticsList.Add(item);
                }
                dgv1.DataSource = orderStatisticsList;

                AvgOrderStatistics totalData = new AvgOrderStatistics();
                if (orderStatisticsList.Count > 0)
                {
                    totalData.AvgRealTime = (double)totalRealTime / orderStatisticsList.Count;
                    totalData.AvgProduceTime = (double)totalProductTime / orderStatisticsList.Count;
                    totalData.AvgProductionEfficeTive = ((totalData.AvgRealTime / totalData.AvgProduceTime) * 100).ToString("0.00") + "%";
                }
                else
                {
                    totalData.AvgProductionEfficeTive = "0.00%";
                }

                allAvgList = new List<AvgOrderStatistics>
                {
                    totalData
                };
                dgv2.DataSource = allAvgList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("查询有误！");
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            groupBox1.Text = NewuGlobal.GetRes("000189");
            label1.Text = NewuGlobal.GetRes("000182") + ":";
            label2.Text = NewuGlobal.GetRes("000301") + ":";
            label3.Text = NewuGlobal.GetRes("000201") + ":";
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            btn_Close.Text = NewuGlobal.GetRes("000103");
            button2.Text = NewuGlobal.GetRes("000129");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btn_Close.Padding = new Padding(0, 0, 7, 0);
                button2.Size = new Size(76, 30);
                btn_Close.Location = new Point(1542, 24);
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btn_Close.Padding = new Padding(0, 0, 0, 0);
                button2.Size = new Size(80, 30);
                btn_Close.Location = new Point(1532, 24);
            }

            dgv1.Columns[0].HeaderText = NewuGlobal.GetRes("000182");
            dgv1.Columns[1].HeaderText = NewuGlobal.GetRes("000212");
            dgv1.Columns[2].HeaderText = NewuGlobal.GetRes("000312");
            dgv1.Columns[3].HeaderText = NewuGlobal.GetRes("000313");
            dgv1.Columns[4].HeaderText = NewuGlobal.GetRes("000370");
            dgv1.Columns[5].HeaderText = NewuGlobal.GetRes("000768");
            dgv1.Columns[6].HeaderText = NewuGlobal.GetRes("000769");
            dgv1.Columns[7].HeaderText = NewuGlobal.GetRes("000770");
            dgv1.Columns[8].HeaderText = NewuGlobal.GetRes("000771");
            dgv1.Columns[9].HeaderText = NewuGlobal.GetRes("000772");
            dgv1.Columns[10].HeaderText = NewuGlobal.GetRes("000773");
            dgv1.Columns[11].HeaderText = NewuGlobal.GetRes("000301");
            dgv1.Columns[12].HeaderText = NewuGlobal.GetRes("000302");
            dgv1.Columns[13].HeaderText = NewuGlobal.GetRes("000774");
            dgv1.Columns[14].HeaderText = NewuGlobal.GetRes("000775");

            dgv2.Columns[0].HeaderText = NewuGlobal.GetRes("000776");
            dgv2.Columns[1].HeaderText = NewuGlobal.GetRes("000777");
            dgv2.Columns[2].HeaderText = NewuGlobal.GetRes("000778");
        }
    }
}