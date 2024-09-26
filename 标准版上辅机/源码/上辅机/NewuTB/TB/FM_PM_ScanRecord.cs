using MultiLanguage;
using Newu;
using NewuCommon;
using OfficeOpenXml;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_PM_ScanRecord : Form, ILanguageChanged
    {
        private readonly PM_ScanRecordRepository scanRecordRepository = new PM_ScanRecordRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private string pageIndex = "1";
        private string pageSize = "100";
        private int pageCount = 0;
        private string Deviceid = "";
        public FM_PM_ScanRecord()
        {
            InitializeComponent();
        }

        private void FM_PM_ScanRecord_Load(object sender, EventArgs e)
        {
            dt_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dt_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dt_start.Value = ComDate.MinDate(DateTime.Now);
            dt_end.Value = ComDate.MaxDate(DateTime.Now);

            List<SYS_Device> list = deviceRepository.GetList("");
            cmb_DeviceID.DataSource = list;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.SelectedIndex = 0;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("DeviceID","所属设备", ColumnType.cmb,true),//345
                new ColStruct("DeviceCode","设备编码"),//346
                new ColStruct("Reserve3","料仓号"),//357
                new ColStruct("TypeCodeName","物料类型名称"),//348
                new ColStruct("MaterialCode","原材料名称"),//347
                new ColStruct("MatBarcode","原材料条码"),//350
                new ColStruct("PortBarcode","扫描条码"),//349
                new ColStruct("BatchOrder","车次序号"),//351
                new ColStruct("WorkGroup","班组"),//352
                new ColStruct("WorkOrder","班次"),//353
                new ColStruct("SaveUserCode","用户编码"),//354
                new ColStruct("SaveTime","扫描时间"),//355
                new ColStruct("Reserve1","扫描结果")//358
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);

            DataGridViewComboBoxColumn dgvCmbDevice = (DataGridViewComboBoxColumn)dgv.Columns["DeviceID"];
            dgvCmbDevice.DataSource = deviceRepository.GetList("");
            dgvCmbDevice.ValueMember = "DeviceID";
            dgvCmbDevice.DisplayMember = "DeviceName";
            //激活onPageChanged事件
            pagerControl1.OnPageChanged += new EventHandler(PagerControl1_OnPageChanged);
            GetData();
            SetLanguage();

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
            try
            {
                pageIndex = pagerControl1.PageIndex.ToString();
                pageSize = pagerControl1.PageSize.ToString();
                DateTime start_time = dt_start.Value;
                DateTime end_time = dt_end.Value;
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append($" SaveTime >= ''{start_time}'' and SaveTime<=''{end_time}''  ");
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    Deviceid = cmb_DeviceID.SelectedValue.ToString();
                    strWhere.Append($" and  DeviceID=''{Deviceid}''");
                };
                List<PM_ScanRecord> pM_ScanRecords = scanRecordRepository.Paging("PM_ScanRecord", "ScanRecordID", "*", strWhere.ToString(), pageIndex, pageSize, "SaveTime", out pageCount);
                pagerControl1.DrawControl(pageCount);
                dgv.DataSource = pM_ScanRecords;
                strWhere.Remove(0, strWhere.Length);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_ScanRecord").Error(ex.ToString());
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            pagerControl1.PageIndex = 1;
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceID.SelectedIndex = -1;
            dt_start.Value = ComDate.MinDate(DateTime.Now);
            dt_end.Value = ComDate.MaxDate(DateTime.Now);
            GetData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            button1.Text = NewuGlobal.GetRes("000103");
            groupBox1.Text = NewuGlobal.GetRes("000340");
            groupBox2.Text = NewuGlobal.GetRes("000341");
            label1.Text= NewuGlobal.GetRes("000201") +":";
            label2.Text= NewuGlobal.GetRes("000476") +":";
            label3.Text= NewuGlobal.GetRes("000477") +":";
            dgv.Columns[0].HeaderText = NewuGlobal.GetRes("000345");
            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000346");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000357");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000348");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000347");
            dgv.Columns[5].HeaderText = NewuGlobal.GetRes("000350");
            dgv.Columns[6].HeaderText = NewuGlobal.GetRes("000349");
            dgv.Columns[7].HeaderText = NewuGlobal.GetRes("000351");
            dgv.Columns[8].HeaderText = NewuGlobal.GetRes("000352");
            dgv.Columns[9].HeaderText = NewuGlobal.GetRes("000353");
            dgv.Columns[10].HeaderText = NewuGlobal.GetRes("000354");
            dgv.Columns[11].HeaderText = NewuGlobal.GetRes("000355");
            dgv.Columns[12].HeaderText = NewuGlobal.GetRes("000358");
            btnExport.Text = NewuGlobal.GetRes("000129");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = button1.Padding = new Padding(0, 0, 7, 0);
                btnExport.Size = new System.Drawing.Size(73, 30);
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = button1.Padding = new Padding(0, 0, 0, 0);
                btnExport.Size = new System.Drawing.Size(87, 30);
            }
            pagerControl1.SetControlLanguage(NewuGlobal.SupportLanguage);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    Deviceid = cmb_DeviceID.SelectedValue.ToString();
                };
                //获取数据源
                List<PM_ScanRecord> pM_ScanRecord = scanRecordRepository.QueryData(Deviceid, dt_start.Value, dt_end.Value);
                if (pM_ScanRecord.Count==0)
                {
                    return;
                }
                //导出文件路径
                string filePath = $@"{NewuGlobal.SoftConfig.ExportPath}:\"+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm", new System.Globalization.CultureInfo("en-US"))+"_ScanRecord.xlsx";
                //创建一个新的Excel工作簿
                ExcelPackage excelPackage = new ExcelPackage();
                //创建一个新的工作表并命名为"sheet1"
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                //增加工作表列名
                worksheet.Cells[1, 1].Value = NewuGlobal.GetRes("000346");  //设备名称
                worksheet.Cells[1, 2].Value = NewuGlobal.GetRes("000348");  //物料类型
                worksheet.Cells[1, 3].Value = NewuGlobal.GetRes("000347");  //物料名称
                worksheet.Cells[1, 4].Value = NewuGlobal.GetRes("000349");  //物料条码
                worksheet.Cells[1, 5].Value = NewuGlobal.GetRes("000354");  //用户编码
                worksheet.Cells[1, 6].Value = NewuGlobal.GetRes("000355");  //保存时间
                worksheet.Cells[1, 7].Value = NewuGlobal.GetRes("000358");  //扫描结果

                //把数据写入工作表中
                for (int i = 0; i < pM_ScanRecord.Count; i++)
                {

                    worksheet.Cells[i + 2, 1].Value = pM_ScanRecord[i].DeviceCode;
                    worksheet.Cells[i + 2, 2].Value = pM_ScanRecord[i].TypeCodeName;
                    worksheet.Cells[i + 2, 3].Value = pM_ScanRecord[i].MaterialCode;
                    worksheet.Cells[i + 2, 4].Value = pM_ScanRecord[i].MatBarcode;
                    worksheet.Cells[i + 2, 5].Value = pM_ScanRecord[i].SaveUserCode;
                    worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    worksheet.Cells[i + 2, 6].Value = pM_ScanRecord[i].SaveTime;
                    worksheet.Cells[i + 2, 7].Value = pM_ScanRecord[i].Reserve1;
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
                NewuGlobal.LogCat("FM_PM_ScanRecord").Error(ex.ToString());
                MessageBox.Show(NewuGlobal.GetRes("000834"));
            }
        }
    }
}