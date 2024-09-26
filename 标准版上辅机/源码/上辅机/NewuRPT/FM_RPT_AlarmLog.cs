using MultiLanguage;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Dapper;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;

namespace NewuRPT
{
    public partial class FM_RPT_AlarmLog : Form, ILanguageChanged
    {
        private readonly RPT_AlarmlogRepository alarmlogRepository = new RPT_AlarmlogRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private string pageIndex = "1";
        private string pageSize = "100";
        private int pageCount = 0;
        private StringBuilder strWhere = new StringBuilder();

        public FM_RPT_AlarmLog()
        {
            InitializeComponent();
        }

        private void FM_RPT_AlarmLog_Load(object sender, EventArgs e)
        {
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);
            List<SYS_Device> list = deviceRepository.GetList("");
            cmb_DeviceName.DataSource = list;
            cmb_DeviceName.ValueMember = "DeviceID";
            cmb_DeviceName.DisplayMember = "DeviceName";
            cmb_DeviceName.SelectedIndex = 0;
            CmbGetdate();
            cmb_DevicePartName.SelectedIndex = -1;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("MaterialCode","配方名称"),
                new ColStruct("PlanQty","计划车数"),
                new ColStruct("FactOrder","实际车数"),
                new ColStruct("AlarmInfo","报警信息"),
                new ColStruct("WorkGroup","班组"),
                new ColStruct("WorkOrder","班次"),
                new ColStruct("AlarmState","报警状态"),
                new ColStruct("SaveTime","报警时间"),
            };
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AddCols(cols);
            dgv.Columns[4].Width = 90;
            dgv.Columns[5].Width = 90;
            dgv.Columns[7].Width = 200;
            SetLanguage();
            GetData();
            //激活onPageChanged事件
            pagerControl1.OnPageChanged += new EventHandler(PagerControl1_OnPageChanged);
        }
        /// <summary>
        /// 页面变化是调用绑定数据方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            //加载数据
            GetData();
        }

        private void GetData()
        {
            pageIndex = pagerControl1.PageIndex.ToString();
            pageSize = pagerControl1.PageSize.ToString();
            string start_time = startTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string end_time = endTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            strWhere.Append($" SaveTime >= ''{start_time}'' and SaveTime<= ''{end_time}'' ");
            if (!string.IsNullOrWhiteSpace(txt_AlarmInfo.Text))
            {
                strWhere.Append($" and AlarmInfo like N''%{txt_AlarmInfo.Text}%'' ");
            }
            List<RPT_AlarmLog> alarmDs = alarmlogRepository.Paging("RPT_AlarmLog", "AlarmLogID", "*", strWhere.ToString(), pageIndex, pageSize, "SaveTime", out pageCount);
            pagerControl1.DrawControl(pageCount);
            dgv.DataSource = alarmDs;
            strWhere.Remove(0, strWhere.Length);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            pagerControl1.PageIndex = 1;
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txt_AlarmInfo.Text = "";
            cmb_DeviceName.SelectedIndex = -1;
            cmb_DevicePartName.SelectedIndex = -1;
            dgv.ClearRow();
        }

        public void CmbGetdate()
        {
            string partId = "";

            if (cmb_DeviceName.SelectedIndex >= 0)
            {
                partId = cmb_DeviceName.SelectedValue.ToString();
            }

            List<SYS_DevicePart> sYS_DeviceParts = devicePartRepository.GetDevicePartListByDeviceID(partId);

            cmb_DevicePartName.ValueMember = "DevicePartCode";

            cmb_DevicePartName.DataSource = sYS_DeviceParts;
        }

        private void Bt_alarm_sum_Click(object sender, EventArgs e)
        {
            new FM_RPT_AlarmLogStat(startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")).Show();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            groupBox1.Text = NewuGlobal.GetRes("000189");
            groupBox2.Text = NewuGlobal.GetRes("000381");
            label1.Text = NewuGlobal.GetRes("000361") + ":";
            label2.Text = NewuGlobal.GetRes("000362") + ":";
            label3.Text = NewuGlobal.GetRes("000363") + ":";
            label4.Text = NewuGlobal.GetRes("000364") + ":";
            label5.Text = NewuGlobal.GetRes("000365") + ":";
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            btnClose.Text = NewuGlobal.GetRes("000103");
            bt_alarm_sum.Text = NewuGlobal.GetRes("000366");
            btnExport.Text = NewuGlobal.GetRes("000129");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                bt_alarm_sum.Size = new Size(104, 30);
                btnExport.Size = new Size(77,30);
                cmb_DevicePartName.DisplayMember = "Reserve1";
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                bt_alarm_sum.Size = new Size(156, 30);
                btnExport.Size = new Size(90, 30);
                cmb_DevicePartName.DisplayMember = "DevicePartName";
            }
            dgv.Columns[0].HeaderText = NewuGlobal.GetRes("000368");
            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000369");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000370");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000371");
            //dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000372");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000374");
            dgv.Columns[5].HeaderText = NewuGlobal.GetRes("000375");
            dgv.Columns[6].HeaderText = NewuGlobal.GetRes("000372");
            dgv.Columns[7].HeaderText = NewuGlobal.GetRes("000376");
            pagerControl1.SetControlLanguage(NewuGlobal.SupportLanguage);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //获取数据源
                List<RPT_AlarmLog> rPT_AlarmLogs = alarmlogRepository.QueryData(txt_AlarmInfo.Text, cmb_DevicePartName.SelectedValue.ToString(),startTime.Value, endTime.Value);
                if (rPT_AlarmLogs.Count == 0)
                {
                    return;
                }
                //导出文件路径
                string filePath = $@"{NewuGlobal.SoftConfig.ExportPath}:\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", new System.Globalization.CultureInfo("en-US")) + "_AlarmLog.xlsx";
                //创建一个新的Excel工作簿
                ExcelPackage excelPackage = new ExcelPackage();
                //创建一个新的工作表并命名为"sheet1"
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                //增加工作表列名
                worksheet.Cells[1, 1].Value = NewuGlobal.GetRes("000368");  //配方名称
                worksheet.Cells[1, 2].Value = NewuGlobal.GetRes("000369");  //计划车数
                worksheet.Cells[1, 3].Value = NewuGlobal.GetRes("000370");  //实际车数
                worksheet.Cells[1, 4].Value = NewuGlobal.GetRes("000371");  //报警信息
                worksheet.Cells[1, 5].Value = NewuGlobal.GetRes("000374");  //班组
                worksheet.Cells[1, 6].Value = NewuGlobal.GetRes("000375");  //班次
                worksheet.Cells[1, 7].Value = NewuGlobal.GetRes("000372");  //报警状态
                worksheet.Cells[1, 8].Value = NewuGlobal.GetRes("000376");  //保存时间

                //把数据写入工作表中
                for (int i = 0; i < rPT_AlarmLogs.Count; i++)
                {

                    worksheet.Cells[i + 2, 1].Value = rPT_AlarmLogs[i].MaterialCode;
                    worksheet.Cells[i + 2, 2].Value = rPT_AlarmLogs[i].PlanQty;
                    worksheet.Cells[i + 2, 3].Value = rPT_AlarmLogs[i].FactOrder;
                    worksheet.Cells[i + 2, 4].Value = rPT_AlarmLogs[i].AlarmInfo;
                    worksheet.Cells[i + 2, 5].Value = rPT_AlarmLogs[i].WorkGroup;
                    worksheet.Cells[i + 2, 6].Value = rPT_AlarmLogs[i].WorkOrder;
                    worksheet.Cells[i + 2, 7].Value = rPT_AlarmLogs[i].AlarmState;
                    worksheet.Cells[i + 2, 8].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    worksheet.Cells[i + 2, 8].Value = rPT_AlarmLogs[i].SaveTime;
                }

                //判断文件是否存在
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                excelPackage.SaveAs(new FileInfo(filePath));
                MessageBox.Show(NewuGlobal.GetRes("000831") + filePath);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_OperateLog").Error(ex.ToString());
                MessageBox.Show(NewuGlobal.GetRes("000832"));
            }
        }
    }
}