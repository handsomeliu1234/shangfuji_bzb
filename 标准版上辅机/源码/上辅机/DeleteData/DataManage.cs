using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DeleteData
{
    public partial class DataManage : Form
    {
        private fmNewuSoft newuSoft = new fmNewuSoft();
        private fmNewuSoftData newuSoftData = new fmNewuSoftData();
        private XmlHelper help = new XmlHelper();
        public ConnDbInfo db_INFO = new ConnDbInfo();
        //private TB_OperateLogRepository operateLogRepository = new TB_OperateLogRepository();
        private string tableName;
        private string sumCount;

        private string intervalCount;
        private string databaseType = null; //数据库名
        private string timesubsegment = ""; //时间字段
        private string dataBase;
        private int flag = 0; //flag=1代表sysrdBtn选中，flag=2代表rptrdBtn选中

        private static string dirPath = Access.directoryPath; //存放删除数据日志的文件夹路径
        private string filePath = dirPath + "Record.mdb"; //文件路径
        private string datatableName = "DeletaDataRecord"; //数据表名
        private int flag2 = 0;// 为了保证 先选目录

        //private List<TB_OperateLog> listLog = new List<TB_OperateLog>();

        public DataManage()
        {
            InitializeComponent();

            //init();
            //getData();
        }

        #region 往数据库记录日志(暂时启用)，日志记录到access数据库
        /*
        private void init()
        {
            ColStruct[] mixCols = new ColStruct[]{
                new ColStruct("Reserve1","序号"),
                new ColStruct("Reserve2","备份文件名称"),
                new ColStruct("Reserve3","备份文件路径"),
                new ColStruct("UserID","备份人员"),
                new ColStruct("SaveTime","备份日期"),
            };

            dgv.AllowUserToResizeColumns = true;
            dgv.ReadOnly = true;
            dgv.AddCols(mixCols);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns[0].FillWeight = 40;
            dgv.Columns[1].FillWeight = 200;
            // dgv.Columns[4].FillWeight = 230;
            // dgv.Enabled = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            // dgv.ColumnHeadersHeight = 35;
            // dgv.RowTemplate.MinimumHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            // dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15, FontStyle.Bold);
            // dgv.RowsDefaultCellStyle.Font = new Font("宋体", 20, FontStyle.Bold);
            dgv.DataSource = listLog;
        }

        private void getData()
        {
            listLog = operateLogRepository.QueryBackupOrRestoreData();
            if (listLog != null)
            {
                int i = 1;
                foreach (var log in listLog)
                {
                    // 根据|吧 login取出来，并赋予值给界面显示slj
                    TB_OperateLog lpgOperate = log as TB_OperateLog;
                    string info = lpgOperate.LogInfo.ToString().Trim();
                    if (info.Contains('|'))
                    {
                        string[] info1 = info.Split('|');
                        if (info1.Length > 3)
                        {
                            log.Reserve1 = i.ToString();
                            i++;
                            log.Reserve2 = info1[2];
                            log.Reserve3 = info1[1];
                        }
                        else
                        {
                            MessageBox.Show("操作日志保存错误");
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作日志保存错误");
                    }
                }
            }

            dgv.Refresh();
            dgv.DataSource = listLog;
        }
        */
        #endregion

        private void DataManage_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00"));
            endTime.Value = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:59"));
            //btnShinkLog.Enabled = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("请确认所选择的时间区间是否有问题,再进行删除!!!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                int time = 0;
                string dirPath = Access.directoryPath; //存放删除数据日志的文件夹路径
                string filePath = dirPath + "Record.mdb"; //文件路径
                string datatableName = "DeletaDataRecord"; //数据表名
                Access.PathStr = filePath;
                if (maindbBtn.Checked == true)
                {
                    tableName = cmbTableList.SelectedValue.ToString();
                    time = newuSoft.DeleteSysData(startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                        endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), tableName, txtIntervalCount.Text.ToString(),
                        "SaveTime");
                    txtTime.Text = time.ToString();
                    databaseType = "NewuAutomation";
                }

                if (rptdbBtn.Checked == true)
                {
                    tableName = cmbTableList.SelectedValue.ToString();
                    //根据表名匹配对应的时间字段
                    timesubsegment = GetTimeSubsegment(tableName.Remove(0, 5));
                    time = newuSoftData.DeleteRprData(startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                        endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), tableName, txtIntervalCount.Text.ToString(),
                        timesubsegment);
                    txtTime.Text = time.ToString();
                    databaseType = "NewuSoftData";
                }

                if (time > 0)
                {
                    Access.CreateAccessTable(dirPath, filePath, datatableName);
                    string connection = Access.ConnectionString;
                    OleDbConnection accessConnection = new OleDbConnection(connection);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("DatabaseType", databaseType);
                    data.Add("TableName", tableName);
                    data.Add("StartTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("EndTime", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("TableDataSum", txtSumCount.Text.ToString());
                    data.Add("IntervalCount", txtIntervalCount.Text.ToString());
                    data.Add("ConsumTime", txtTime.Text.ToString());
                    data.Add("SaveTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Access.AddDataToAccess(data, datatableName, accessConnection);

                    MessageBox.Show("删除成功", "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("删除失败，请检查所选择信息是否正确", "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        #region 控件事件

        private void maindbBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (maindbBtn.Checked == true && rptdbBtn.Checked == false)
            {
                DbHelperSQL dbhelp = new DbHelperSQL(ConnType.NewuAutomation);
                List<string> tableList = dbhelp.GetTableName();
                cmbTableList.DataSource = tableList;
                txtTime.Text = "";
                //btnShinkLog.Enabled = false;
            }
        }

        private void rptdbBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (rptdbBtn.Checked == true && maindbBtn.Checked == false)
            {
                DbHelperSQL dbhelp = new DbHelperSQL(ConnType.NewuSoftData);
                List<string> tableList = dbhelp.GetTableName();
                cmbTableList.DataSource = tableList;
                txtTime.Text = "";
                //btnShinkLog.Enabled = true;
            }
        }

        private void cmbTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (maindbBtn.Checked == true)
                {
                    flag = 1;
                    tableName = cmbTableList.SelectedValue.ToString();
                    SetTxtSumCount(tableName, flag);
                    SetTxtIntervalCount(tableName, flag, "SaveTime");
                }

                if (rptdbBtn.Checked == true)
                {
                    flag = 2;
                    tableName = cmbTableList.SelectedValue.ToString();
                    SetTxtSumCount(tableName, flag);
                    SetTxtIntervalCount(tableName, flag, GetTimeSubsegment(tableName.Remove(0, 5)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //NewuGlobal.LogCat("Database").Error(ex.ToString());
            }
        }

        private void startTime_ValueChanged(object sender, EventArgs e)
        {
            if (maindbBtn.Checked == true)
            {
                flag = 1;
                tableName = cmbTableList.SelectedValue.ToString();
                SetTxtIntervalCount(tableName, flag, "SaveTime");
            }

            if (rptdbBtn.Checked == true)
            {
                flag = 2;
                tableName = cmbTableList.SelectedValue.ToString();
                SetTxtIntervalCount(tableName, flag, GetTimeSubsegment(tableName.Remove(0, 5)));
            }
        }

        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            if (maindbBtn.Checked == true)
            {
                flag = 1;
                tableName = cmbTableList.SelectedValue.ToString();
                SetTxtIntervalCount(tableName, flag, "SaveTime");
            }

            if (rptdbBtn.Checked == true)
            {
                flag = 2;
                tableName = cmbTableList.SelectedValue.ToString();
                SetTxtIntervalCount(tableName, flag, GetTimeSubsegment(tableName.Remove(0, 5)));
            }
        }

        #endregion 控件事件

        #region 基本方法

        /// <summary>
        /// 给txtSumCount赋值
        /// </summary>
        /// <param name="tableName">数据库名称</param>
        private void SetTxtSumCount(string tableName, int flag)
        {
            if (flag == 1)
            {
                sumCount = newuSoft.GetSysDataSumCount(tableName);
                txtSumCount.Text = sumCount;
            }
            else if (flag == 2)
            {
                sumCount = newuSoftData.GetRptDataSumCount(tableName);
                txtSumCount.Text = sumCount;
            }
        }

        /// <summary>
        /// 给txtIntervalCount赋值
        /// </summary>
        /// <param name="tableName">数据库名称</param>
        private void SetTxtIntervalCount(string tableName, int flag, string timesubsegment)
        {
            if (flag == 1)
            {
                intervalCount = newuSoft.GetSysDataIntervalCount(startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), tableName, timesubsegment);
                txtIntervalCount.Text = intervalCount;
            }
            else if (flag == 2)
            {
                intervalCount = newuSoftData.GetRptDataIntervalCount(startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), tableName, timesubsegment);
                txtIntervalCount.Text = intervalCount;
            }
        }

        #endregion 基本方法

        private void btnShinkLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (newuSoftData.ShrinkLog(Global.SoftConfig.ReportDbName))
                {
                    MessageBox.Show("收缩成功");
                }
                else
                {
                    MessageBox.Show("收缩失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetTimeSubsegment(string tablename)
        {
            switch (tablename)
            {
                case "RPT_Curve":
                    timesubsegment = "CreateTime";
                    break;

                case "RPT_DeviceEvent":
                    timesubsegment = "StartTime";
                    break;

                case "RPT_MixStep":
                    timesubsegment = "SaveTime";
                    break;

                case "RPT_Weight":
                    timesubsegment = "SaveTime";
                    break;

                default:
                    timesubsegment = "SaveTime";
                    break;
            }

            return timesubsegment;
        }

        private void buttonBackUp_Click(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                flag2++;
                MessageBox.Show("请确定是否已经选了保存路径");
                return;
            }

            if (textBoxFileName == null)
            {
                MessageBox.Show("请选择备份的路径");
                return;
            }
            else
            {
                if (!radioButton6.Checked && radioButton5.Checked)
                {
                    DbHelperSQL dbhelp = new DbHelperSQL(ConnType.NewuSoftData);
                    DateTime startTime = DateTime.Now;
                    // string path = textBoxCatalog.Text.ToString().Trim() + $"\\{dataBase}.bak";
                    string path = textBoxFileName.Text;
                    dbhelp.BackUpDataBase(folderBrowserDialog1, dataBase, path);
                    DateTime endTime = DateTime.Now;
                    string fileName = $"{ dataBase }_{DateTime.Now.ToString("M") }.bak";

                    string messageLog = "备份到" + "|" + textBoxCatalog.Text.ToString().Trim() + "|" + fileName + "|" + DateTime.Now;
                    //operateLogRepository.SaveAppLog(messageLog, AppLogType.BackUp);//发送成功后记录日志
                    Access.PathStr = filePath;
                    Access.CreateAccessTable(dirPath, filePath, datatableName);
                    string connection = Access.ConnectionString;
                    OleDbConnection accessConnection = new OleDbConnection(connection);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("DatabaseType", "备份成功");
                    data.Add("TableName", $"{dataBase}");
                    data.Add("StartTime", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("TableDataSum", 0);
                    data.Add("IntervalCount", 0);
                    data.Add("ConsumTime", (endTime - startTime).ToString());
                    data.Add("SaveTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Access.AddDataToAccess(data, datatableName, accessConnection);

                    //getData();
                    MessageBox.Show("备份成功");
                    flag2--;
                }
                else if (radioButton6.Checked && !radioButton5.Checked)
                {
                    DbHelperSQL dbhelp = new DbHelperSQL(ConnType.NewuAutomation);
                    DateTime startTime = DateTime.Now;
                    string path = textBoxFileName.Text;
                    dbhelp.BackUpDataBase(folderBrowserDialog1, dataBase, path);
                    DateTime endTime = DateTime.Now;
                    string fileName = $"{ dataBase }_{DateTime.Now.ToString("M") }.bak";

                    string messageLog = "备份到" + "|" + textBoxCatalog.Text.ToString().Trim() + "|" + fileName + "|" + DateTime.Now;
                    //operateLogRepository.SaveAppLog(messageLog, AppLogType.BackUp);//发送成功后记录日志
                    Access.PathStr = filePath;
                    Access.CreateAccessTable(dirPath, filePath, datatableName);
                    string connection = Access.ConnectionString;
                    OleDbConnection accessConnection = new OleDbConnection(connection);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("DatabaseType", "备份成功");
                    data.Add("TableName", $"{dataBase}");
                    data.Add("StartTime", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("TableDataSum", 0);
                    data.Add("IntervalCount", 0);
                    data.Add("ConsumTime", (endTime - startTime).ToString());
                    data.Add("SaveTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Access.AddDataToAccess(data, datatableName, accessConnection);
                    flag2--;
                    //getData();
                    MessageBox.Show("备份成功");
                }
                else
                {
                    MessageBox.Show("请先选择主数据还是报表数据库");
                    return;
                }
            }
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (!radioButton2.Checked && radioButton1.Checked)
            {
                // textBoxFileName_MouseDoubleClick(null, null);
                if (textBox1.Text.ToString()=="")
                {
                    MessageBox.Show("先选择还原的数据库文件");
                    return;

                }
                DbHelperSQL dbhelp = new DbHelperSQL(ConnType.master);
                System.Xml.XmlNode node = help.ReadXmlNode(Application.StartupPath.ToString() + "\\SoftConfig.xml",
                    "Config//NewuSoftData");

                string Database = node.Attributes["DB"].Value.ToString();
                // dbhelp.CreatDataBase(Database);
                // DbHelperSQL dbhelp1 = new DbHelperSQL(ConnType.NewuSoftData);
                string path = textBox1.Text;
                DateTime startTime = DateTime.Now;
                dbhelp.RestoreUpDataBase(folderBrowserDialog1, Database, path);
                DateTime endTime = DateTime.Now;

                // string fileName = $"{ dataBase }_{DateTime.Now.ToString("M") }.bak";
                string fileName = $"{ textBox1.Text }";

                string messageLog = "还原到" + "|" + textBoxCatalog.Text.ToString().Trim() + "|" + fileName + "|" + DateTime.Now;
                //数据库丢失无法进主窗体 进行还原功能。故取消想数据库保存日志，向acess数据库保存日志
                //operateLogRepository.SaveAppLog(messageLog, AppLogType.Restore);//发送成功后记录日志
                Access.PathStr = filePath;
                Access.CreateAccessTable(dirPath, filePath, datatableName);
                string connection = Access.ConnectionString;
                OleDbConnection accessConnection = new OleDbConnection(connection);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("DatabaseType", "还原成功");
                data.Add("TableName", $"{Database}");
                data.Add("StartTime", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                data.Add("EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                data.Add("TableDataSum", 0);
                data.Add("IntervalCount", 0);
                data.Add("ConsumTime", (endTime - startTime).ToString());
                data.Add("SaveTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                Access.AddDataToAccess(data, datatableName, accessConnection);

                //getData();
                MessageBox.Show("还原成功");
            }
            else if (radioButton2.Checked && !radioButton1.Checked)
            {
                // textBoxFileName_MouseDoubleClick(null, null);
                if (textBox1.Text.ToString() == "")
                {
                    MessageBox.Show("先选择还原的数据库文件");
                    return;
                }
                DbHelperSQL dbhelp = new DbHelperSQL(ConnType.master);
                System.Xml.XmlNode node = help.ReadXmlNode(Application.StartupPath.ToString() + "\\SoftConfig.xml",
                    "Config//NewuAutomation");

                string Database = node.Attributes["DB"].Value.ToString();
                // dbhelp.CreatDataBase(Database);
                // DbHelperSQL dbhelp1 = new DbHelperSQL(ConnType.NewuAutomation);
                DateTime startTime = DateTime.Now;
                string path = textBox1.Text;
                dbhelp.RestoreUpDataBase(folderBrowserDialog1, Database, path);
                DateTime endTime = DateTime.Now;
                string fileName = $"{ textBox1.Text}";
                string messageLog = "还原到" + "|" + textBoxCatalog.Text.ToString().Trim() + "|" + fileName + "|" + DateTime.Now;
                //数据库丢失无法进主窗体 进行还原功能。故取消想数据库保存日志，向acess数据库保存日志
                // operateLogRepository.SaveAppLog(messageLog, AppLogType.Restore);//发送成功后记录日志
                Access.PathStr = filePath;
                Access.CreateAccessTable(dirPath, filePath, datatableName);
                string connection = Access.ConnectionString;
                OleDbConnection accessConnection = new OleDbConnection(connection);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("DatabaseType", "还原成功");
                data.Add("TableName", $"{Database}");
                data.Add("StartTime", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                data.Add("EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                data.Add("TableDataSum", 0);
                data.Add("IntervalCount", 0);
                data.Add("ConsumTime", (endTime - startTime).ToString());
                data.Add("SaveTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                Access.AddDataToAccess(data, datatableName, accessConnection);

                //getData();
                MessageBox.Show("还原成功");
            }
            else
            {
                MessageBox.Show("请先选择主数据还是报表数据库");
                return;
            }
        }

        /// <summary>
        /// 选择目录和文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!radioButton6.Checked && radioButton5.Checked)
            {
                folderBrowserDialog1.ShowDialog();
                string fileName = folderBrowserDialog1.SelectedPath;
                textBoxCatalog.Text = fileName;
                DbHelperSQL dbhelp = new DbHelperSQL(ConnType.NewuSoftData);
                System.Xml.XmlNode node = help.ReadXmlNode(Application.StartupPath.ToString() + "\\SoftConfig.xml",
                    "Config//NewuSoftData");

                dataBase = node.Attributes["DB"].Value.ToString();

                string time = DateTime.Now.ToString("M");
                if (!File.Exists($"{fileName}\\{dataBase}_{time}.bak"))
                {
                    File.Create($"{fileName}\\{dataBase}_{time}.bak").Close();
                }

                textBoxFileName.Text = $"{fileName}\\{dataBase}_{time}.bak";
                string path = fileName + $"\\{dataBase}_{time}.bak";
            }
            else if (radioButton6.Checked && !radioButton5.Checked)
            {
                folderBrowserDialog1.ShowDialog();
                string fileName = folderBrowserDialog1.SelectedPath;
                textBoxCatalog.Text = fileName;
                DbHelperSQL dbhelp = new DbHelperSQL(ConnType.master);
                System.Xml.XmlNode node = help.ReadXmlNode(Application.StartupPath.ToString() + "\\SoftConfig.xml",
                    "Config//NewuAutomation");

                dataBase = node.Attributes["DB"].Value.ToString();

                string time = DateTime.Now.ToString("M");
                if (!File.Exists($"{fileName}\\{dataBase}_{time}.bak"))
                {
                    File.Create($"{fileName}\\{dataBase}_{time}.bak").Close();
                }

                string path = fileName + $"\\{dataBase}_{time}.bak";
                textBoxFileName.Text = $"{fileName}\\{dataBase}_{time}.bak";
            }
            else
            {
                MessageBox.Show("请先选择主数据还是报表数据库");
                return;
            }
        }

        private void textBoxFileName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = string.Empty;
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Files (*.bak)|*.bak" //如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            };
            var result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFileName.Text = openFileDialog.FileName;
                textBox1.Text = openFileDialog.FileName;
            }
        }

        private void buttonSelectSqlFile_Click(object sender, EventArgs e)
        {
            textBoxFileName_MouseDoubleClick(null, null);
        }
    }
}