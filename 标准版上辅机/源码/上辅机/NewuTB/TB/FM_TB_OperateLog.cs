using MultiLanguage;
using Newu;
using NewuCommon;
using OfficeOpenXml;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_OperateLog : Form, ILanguageChanged
    {
        private readonly TB_OperateLogRepository operateLogRepository = new TB_OperateLogRepository();
        private string pageIndex = "1";
        private string pageSize = "100";
        private int pageCount = 0;
        private StringBuilder strWhere = new StringBuilder();

        public FM_TB_OperateLog()
        {
            InitializeComponent();
        }

        private void FM_TB_OperateLog_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("OperateLogID","操作日志ID", ColumnType.txt,false),
                new ColStruct("LogInfo","日志信息"),
                new ColStruct("UserID","用户ID"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            SetLanguage();
            //激活onPageChanged事件
            pagerControl1.OnPageChanged += new EventHandler(PagerControl1_OnPageChanged);
            GetData();
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
            DateTime start_time = startTime.Value;
            DateTime end_time = endTime.Value;
            strWhere.Append($" SaveTime >= ''{start_time}'' and SaveTime<=''{end_time}''  ");
            if (!string.IsNullOrWhiteSpace(txt_LogInfo.Text))
            {
                strWhere.Append($" and LogInfo like N''%{txt_LogInfo.Text}%'' ");
            }
            List<TB_OperateLog> tB_OperateLogs = operateLogRepository.Paging("TB_OperateLog", "OperateLogID", "*", strWhere.ToString(), pageIndex, pageSize, "SaveTime", out pageCount);
            pagerControl1.DrawControl(pageCount);
            dgv.DataSource = tB_OperateLogs;
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
            txt_LogInfo.Text = "";
            dgv.ClearRow();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            label1.Text = NewuGlobal.GetRes("000554") + ":";
            label3.Text = NewuGlobal.GetRes("000555") + ":";
            label2.Text = NewuGlobal.GetRes("000556") + ":";
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            btnClose.Text = NewuGlobal.GetRes("000103");
            gropBox1.Text = NewuGlobal.GetRes("000438");
            btnExport.Text = NewuGlobal.GetRes("000129");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                btnExport.Size = new System.Drawing.Size(73, 30);
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                btnExport.Size = new System.Drawing.Size(87, 30);
            }
            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000559");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000560");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000561");
            pagerControl1.SetControlLanguage(NewuGlobal.SupportLanguage);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //获取数据源
                List<TB_OperateLog> tB_OperateLogs = operateLogRepository.QueryData(txt_LogInfo.Text, startTime.Value,endTime.Value);
                if (tB_OperateLogs.Count==0)
                {
                    return;
                }
                //导出文件路径
                string filePath = $@"{NewuGlobal.SoftConfig.ExportPath}:\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", new System.Globalization.CultureInfo("en-US")) + "_OperateLog.xlsx";
                //创建一个新的Excel工作簿
                ExcelPackage excelPackage = new ExcelPackage();
                //创建一个新的工作表并命名为"sheet1"
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                //增加工作表列名
                worksheet.Cells[1, 1].Value = NewuGlobal.GetRes("000559");  //日志信息
                worksheet.Cells[1, 2].Value = NewuGlobal.GetRes("000560");  //用户名称
                worksheet.Cells[1, 3].Value = NewuGlobal.GetRes("000561");  //保存时间

                //把数据写入工作表中
                for (int i = 0; i < tB_OperateLogs.Count; i++)
                {

                    worksheet.Cells[i + 2, 1].Value = tB_OperateLogs[i].LogInfo;
                    worksheet.Cells[i + 2, 2].Value = tB_OperateLogs[i].UserID;
                    worksheet.Cells[i + 2, 3].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    worksheet.Cells[i + 2, 3].Value = tB_OperateLogs[i].SaveTime;
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
                MessageBox.Show(NewuGlobal.GetRes("000842"));
            }
        }
    }
}