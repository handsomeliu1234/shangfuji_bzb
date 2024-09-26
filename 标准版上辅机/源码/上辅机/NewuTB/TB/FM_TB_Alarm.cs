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
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_Alarm : Form, ILanguageChanged
    {
        private readonly TB_AlarmRepository alarmRepository = new TB_AlarmRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private int rowIndex;
        private List<TB_Alarm> list = null;
        private List<SYS_DevicePart> sYS_DeviceParts;

        public FM_TB_Alarm()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FM_TB_Alarm_Add fm = new FM_TB_Alarm_Add
            {
                Owner = this
            };
            fm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FM_TB_Alarm_Load(object sender, EventArgs e)
        {
            List<SYS_Device> Devicelist = deviceRepository.GetList("");
            cmb_DeviceID.DataSource = Devicelist;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.SelectedIndex = -1;
            cmb_DevicePartID.DataSource = null;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("AlarmID","报警ID", ColumnType.txt,false),
                new ColStruct("DeviceID",NewuGlobal.GetRes("000361"), ColumnType.cmb,true),
                new ColStruct("DevicePartID",NewuGlobal.GetRes("000362"), ColumnType.cmb,true),
                new ColStruct("AlarmInfo",NewuGlobal.GetRes("000365")),
                new ColStruct("MemoryAddr",NewuGlobal.GetRes("000747")),
                new ColStruct("TagAddress",NewuGlobal.GetRes("000748")),
                new ColStruct("IsDisplay",NewuGlobal.GetRes("000749"),ColumnType.chk,true)
            };

            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewComboBoxColumn dgvCmbDevice = (DataGridViewComboBoxColumn)dgv.Columns["DeviceID"];
            dgvCmbDevice.DataSource = deviceRepository.GetList("");
            dgvCmbDevice.ValueMember = "DeviceID";
            dgvCmbDevice.DisplayMember = "DeviceName";
            SetLanguage();

            GetData();
        }

        public void GetData()
        {
            try
            {
                string sql = " 1=1 ";

                if (cmb_DevicePartID.SelectedIndex >= 0)
                {
                    string cmb_devicepart = cmb_DevicePartID.SelectedValue.ToString();
                    if (cmb_devicepart != "")
                    {
                        sql += " and DevicePartID='" + cmb_devicepart + "'";
                    }
                }
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    string cmb_device = cmb_DeviceID.SelectedValue.ToString();
                    sql += " and DeviceID = '" + cmb_device + "'";
                }
                sql += " order by MemoryAddr ";
                list = alarmRepository.GetModelList(sql);
                dgv.DataSource = new BindingSource { DataSource = list };

                if (rowIndex < 0)
                    rowIndex = 0;

                //删除最后一行时触发
                if (rowIndex >= dgv.Rows.Count)
                    rowIndex = dgv.Rows.Count - 1;

                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = false;
                    dgv.Rows[rowIndex].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = rowIndex;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_Alarm").Error(ex.ToString());
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, dgv.CurrentCell.RowIndex].Value.ToString();
                FM_TB_Alarm_Add fm = new FM_TB_Alarm_Add(id)
                {
                    Owner = this
                };
                fm.ShowDialog();
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, rowIndex].Value.ToString();
                string AlarmName = dgv[3, rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170") + "    " + AlarmName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = alarmRepository.Delete(id);
                    if (isAccess)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000173"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000174"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceID.SelectedIndex = -1;
            cmb_DevicePartID.DataSource = null;
        }

        private void Cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string partId = "";

            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                partId = cmb_DeviceID.SelectedValue.ToString();
            }
            sYS_DeviceParts = devicePartRepository.GetDevicePartListByDeviceID(partId);

            cmb_DevicePartID.ValueMember = "DevicePartID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
                cmb_DevicePartID.DisplayMember = "Reserve1";
            else
                cmb_DevicePartID.DisplayMember = "DevicePartName";
            cmb_DevicePartID.DataSource = sYS_DeviceParts;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (list == null)
                return;
            string str = textBox1.Text.ToString();
            string content = textBox2.Text.ToString();
            TB_Alarm temp = null;
            foreach (var item in list)
            {
                if (item.TagAddress == str)
                {
                    item.AlarmInfo = content;
                    temp = item;
                    break;
                }
            }
            if (temp != null)
            {
                alarmRepository.Update(temp);
                MessageBox.Show(NewuGlobal.GetRes("000171"));
            }

            CSharedString SS = NewuGlobal.MemDB;
            SS.SetStr(85, "1");
            dgv.DataSource = new BindingList<TB_Alarm>(list);
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = "";
                if (cmb_DeviceID.SelectedValue == null)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000208") + NewuGlobal.GetRes("000182"));
                    return;
                }
                else
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        strPath = dialog.FileName;
                        if (!string.IsNullOrEmpty(strPath))
                        {
                            FileInfo existingFile = new FileInfo(strPath);

                            ExcelPackage package = new ExcelPackage(existingFile);

                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];//选定 指定页
                            string deviceID = cmb_DeviceID.SelectedValue.ToString();
                            int rows = worksheet.Dimension.End.Row;
                            int count = 0;
                            if (rows > 0)
                            {
                                Task<bool> task = Task.Run<bool>(() =>
                                 {
                                     alarmRepository.DeleteByDeviceID(deviceID);
                                     for (int i = 2; i < rows; i++)
                                     {
                                         TB_Alarm model = new TB_Alarm
                                         {
                                             DeviceID = deviceID,
                                             DevicePartID = sYS_DeviceParts.Find(s => s.Reserve1.Equals(worksheet.Cells[i, 1].Value.ToString())).DevicePartID,
                                             AlarmInfo = worksheet.Cells[i, 2].Value.ToString(),
                                             MemoryAddr = int.Parse(worksheet.Cells[i, 3].Value.ToString()),
                                             TagAddress = worksheet.Cells[i, 4].Value.ToString(),
                                             IsDisplay = int.Parse(worksheet.Cells[i, 5].Value.ToString()),
                                             SaveTime = DateTime.Now
                                         };

                                         alarmRepository.Add(model);
                                         count++;
                                     }
                                     if (count > 0)
                                         return true;
                                     else
                                         return false;
                                 });
                                if (task.Result)
                                {
                                    MessageBox.Show(NewuGlobal.GetRes("000378"));
                                    GetData();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_Alarm").Error(ex.ToString());
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000361");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000362");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000365");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000747");
            dgv.Columns[5].HeaderText = NewuGlobal.GetRes("000748");
            dgv.Columns[6].HeaderText = NewuGlobal.GetRes("000749");
        }

        private void SetLanguage()
        {
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnRefresh.Size = new Size(115, 30);

                btnImport.Size = new Size(100, 30);
                btnImport.Location = new Point(850, 15);

                btnDel.Padding = new Padding(0, 0, 7, 0);
                btnClose.Padding = new Padding(0, 0, 7, 0);

                label1.Size = new Size(65, 12);
                label1.Location = new Point(25, 29);
                label1.Text = NewuGlobal.GetRes("000182") + "：";

                label2.Text = NewuGlobal.GetRes("000130") + "：";
                cmb_DevicePartID.Location = new Point(380, 29);
                btnQuery.Location = new Point(577, 24);
                btnReset.Location = new Point(673, 24);
                btnQuery.Padding = new Padding(0, 0, 7, 0);
                btnReset.Padding = new Padding(0, 0, 7, 0);

                btnExport.Size = new Size(105, 30);
                btnExport.Location = new Point(977, 15);

                btnDownLoad.Size = new Size(94, 30);
                btnDownLoad.Location = new Point(1104, 15);
            }
            else
            {
                btnRefresh.Size = new Size(151, 30);
                btnRefresh.Location = new Point(709, 15);

                btnImport.Size = new Size(125, 30);
                btnImport.Location = new Point(879, 15);

                btnDel.Padding = new Padding(0, 0, 0, 0);
                btnClose.Padding = new Padding(0, 0, 0, 0);

                label1.Size = new Size(47, 12);
                label1.Location = new Point(34, 29);
                label1.Text = NewuGlobal.GetRes("000182") + ":";

                label2.Text = NewuGlobal.GetRes("000130") + ":";
                cmb_DevicePartID.Location = new Point(400, 29);
                btnQuery.Location = new Point(597, 24);
                btnReset.Location = new Point(693, 24);
                btnQuery.Padding = new Padding(0, 0, 0, 0);
                btnReset.Padding = new Padding(0, 0, 0, 0);

                btnExport.Size = new Size(120, 30);
                btnExport.Location = new Point(1019, 15);

                btnDownLoad.Size = new Size(156, 30);
                btnDownLoad.Location = new Point(1157, 15);
            }

            btnAdd.Text = NewuGlobal.GetRes("000100");
            btnEdit.Text = NewuGlobal.GetRes("000101");
            btnDel.Text = NewuGlobal.GetRes("000102");
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnRefresh.Text = NewuGlobal.GetRes("000744");
            btnImport.Text = NewuGlobal.GetRes("000745");
            groupBox1.Text = NewuGlobal.GetRes("000458");
            groupBox2.Text = NewuGlobal.GetRes("000746");
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            btnExport.Text = NewuGlobal.GetRes("000828");
            btnDownLoad.Text = NewuGlobal.GetRes("000829");

            DataGridViewComboBoxColumn dgvCmbDevicePart = (DataGridViewComboBoxColumn)dgv.Columns["DevicePartID"];
            dgvCmbDevicePart.DataSource = devicePartRepository.GetList("");
            dgvCmbDevicePart.ValueMember = "DevicePartID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
                dgvCmbDevicePart.DisplayMember = "Reserve1";
            else
                dgvCmbDevicePart.DisplayMember = "DevicePartName";
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void BtnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = AppDomain.CurrentDomain.BaseDirectory + @"\AlarmTemplate.xlsx";
                string targetDirectory = $@"{NewuGlobal.SoftConfig.ExportPath}:\";
                string targetFile = targetDirectory + Path.GetFileName(sourceFile);
                File.Copy(sourceFile, targetFile, true);
                MessageBox.Show(NewuGlobal.GetRes("000833") + targetFile);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_Alarm").Error(ex.ToString());
                MessageBox.Show(NewuGlobal.GetRes("000830"));
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //导出文件路径
                string filePath = $@"{NewuGlobal.SoftConfig.ExportPath}:\AlarmTemplate.xlsx";
                //创建一个新的Excel工作簿
                ExcelPackage excelPackage = new ExcelPackage();
                //创建一个新的工作表并命名为"sheet1"
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                //增加工作表列名
                worksheet.Cells[1, 1].Value = NewuGlobal.GetRes("000362");  //设备部件名称
                worksheet.Cells[1, 2].Value = NewuGlobal.GetRes("000365");  //报警信息
                worksheet.Cells[1, 3].Value = NewuGlobal.GetRes("000747");  //内存地址
                worksheet.Cells[1, 4].Value = NewuGlobal.GetRes("000748");  //标签地址
                worksheet.Cells[1, 5].Value = NewuGlobal.GetRes("000749");  //是否显示
                                                                            //把数据写入工作表中
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 2; j < dgv.Columns.Count; j++)
                    {
                        if (j == 2)
                        {
                            //dgv.Rows[i].Cells[j].Value =
                            worksheet.Cells[i + 2, 1].Value = devicePartRepository.GetList("").FindLast(list =>
                            {
                                if (list.DevicePartID == dgv.Rows[i].Cells[2].Value.ToString())
                                {
                                    return true;
                                }
                                return false;
                            }).Reserve1;
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j - 1].Value = dgv.Rows[i].Cells[j].Value;
                        }
                    }
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
                NewuGlobal.LogCat("FM_TB_Alarm").Error(ex.ToString());
                MessageBox.Show(NewuGlobal.GetRes("000832"));
            }
        }
    }
}