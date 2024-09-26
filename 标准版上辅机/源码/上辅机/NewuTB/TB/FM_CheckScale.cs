using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// </summary>
namespace NewuTB.TB
{
    /// <summary>
    /// 自动校秤
    /// </summary>
    public partial class FM_CheckScale : Form, ILanguageChanged
    {
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private bool startCheck;
        private BindingSource bs;   //校秤设定
        private BindingSource bs1;  //报表数据

        public FM_CheckScale()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_CheckScale_Load(object sender, EventArgs e)
        {
            List<SYS_Device> sYS_Devices = deviceRepository.GetList(0, "", "DeviceName");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("ID","ID",ColumnType.txt,false),
                new ColStruct("DeviceCode","设备"),
                new ColStruct("CheckScaleNo","校秤编号"),
                new ColStruct("DevicePartName","秤类别"),
                new ColStruct("ScaleName","秤名称"),
                new ColStruct("ScaleWeight","砝码重量"),
                new ColStruct("SetError","允许误差"),
                new ColStruct("SaveUser","用户"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cols = new ColStruct[]
            {
                new ColStruct("ID","ID",ColumnType.txt,false),
                new ColStruct("DeviceCode","设备"),
                new ColStruct("CheckScaleNo","校秤编号"),
                new ColStruct("ScaleName","秤名称"),
                new ColStruct("ScaleWeight","砝码重量"),
                new ColStruct("SetError","允许误差"),
                new ColStruct("RealWeight","实际重量"),
                new ColStruct("Result","是否合格",ColumnType.cmb,true),
                new ColStruct("SaveUser","用户"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv1.AllowUserToAddRows = false;
            dgv1.AddCols(cols);
            dgv1.ReadOnly = true;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewComboBoxColumn colum = dgv1.Columns["Result"] as DataGridViewComboBoxColumn;
            BindingSource bs2 = new BindingSource();
            var dic = new Dictionary<bool, string>
            {
                { true, "合格" },
                { false, "不合格" }
            };
            bs2.DataSource = dic;
            colum.DataSource = bs2;
            colum.ValueMember = "Key";
            colum.DisplayMember = "Value";

            bs = new BindingSource();
            dgv.DataSource = bs;

            bs1 = new BindingSource();
            dgv1.DataSource = bs1;

            SetLanguage();

            GetData();
        }

        private void GetData()
        {
            ScaleCheckSetRepository scaleRepo = new ScaleCheckSetRepository();
            string deviceid = "";
            if (cmb_DeviceID.SelectedValue != null)
            {
                deviceid = cmb_DeviceID.SelectedValue as string;
            }
            IEnumerable<ScaleCheckSet> list;
            if (deviceid == "")
            {
                list = scaleRepo.GetList();
            }
            else
            {
                list = scaleRepo.GetByDevice(NewuGlobal.DeviceCodeByID(deviceid));
            }
            bs.DataSource = list;
            dgv.Update();
        }

        /// <summary>
        /// 发送校验开始标志位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            button1.Enabled = false;
            NewuGlobal.MemMgr.Sync(1060, "0001");  //开始校验
            startCheck = true;
            Task.Run(() => MonitorCheckScaleStop());
        }

        private async Task MonitorCheckScaleStop()
        {
            while (startCheck)
            {
                try
                {
                    int stop = NewuGlobal.MemDB.GetInt(1056, 4);
                    if (stop == 1)  //结束了
                    {
                        //读取每个秤的数据,记录报表
                        ScaleCheckSetRepository scaleRepo = new ScaleCheckSetRepository();
                        RPT_AutoCheckScaleMixRepository rptRepo = new RPT_AutoCheckScaleMixRepository();
                        var list = scaleRepo.GetByDevice(NewuGlobal.SoftConfig.DeviceCode);
                        var resultList = new List<RPT_AutoCheckScale>();
                        DateTime dt = DateTime.Now;
                        bool checkOK = true;
                        for (int i = 0; i < list.Count; i++)
                        {
                            RPT_AutoCheckScale checkResult = new RPT_AutoCheckScale
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                                CheckScaleNo = list[i].CheckScaleNo,
                                ScaleName = list[i].ScaleName,
                                ScaleWeight = list[i].ScaleWeight,
                                SetError = list[i].SetError,
                                SaveTime = dt,
                                SaveUser = NewuGlobal.TB_UserInfo.UserCode
                            };
                            double checkValue = 0;
                            switch (list[i].CheckScaleNo)
                            {
                                case 1:   //炭黑秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1000, 4), NewuGlobal.SoftConfig.CarbonDigit);
                                    break;

                                case 2:   //炭黑中间秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1004, 4), NewuGlobal.SoftConfig.CarbonDigit);
                                    break;

                                case 3:   //小药秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1044, 4), NewuGlobal.SoftConfig.DrugDigit);
                                    break;

                                case 4:   //塑解剂秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1052, 4), NewuGlobal.SoftConfig.PlaDigit);
                                    break;

                                case 5:   //硅烷秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1048, 4), NewuGlobal.SoftConfig.SilaneDigit);
                                    break;

                                case 6:   //油料秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1016, 4), NewuGlobal.SoftConfig.OilDigit);
                                    break;

                                case 7:   //胶料秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1040, 4), NewuGlobal.SoftConfig.RubberDigit);
                                    break;

                                case 8:   //粉料秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1032, 4), NewuGlobal.SoftConfig.ZnoDigit);
                                    break;

                                case 9:   //粉料中间秤
                                    checkValue = FunClass.GetMemHexDec(NewuGlobal.MemDB.GetStr(1036, 4), NewuGlobal.SoftConfig.ZnoDigit);
                                    break;

                                default:
                                    break;
                            }
                            double allErr = double.Parse(list[i].SetError);
                            double set = double.Parse(list[i].ScaleWeight);
                            bool isOk = Math.Abs(checkValue - set) <= allErr;
                            if (isOk)
                            {
                                checkResult.Result = true;
                            }
                            else
                            {
                                checkResult.Result = false;
                                if (checkOK)
                                {
                                    checkOK = false;
                                }
                            }
                            checkResult.RealWeight = checkValue.ToString("0.000");
                            resultList.Add(checkResult);
                        }
                        NewuGlobal.MemMgr.Sync(1056, "0");  //结束校验
                        NewuGlobal.MemMgr.Sync(1060, "0000");  //重置开始校秤信号
                        startCheck = false;
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            bs1.DataSource = resultList;
                            dgv1.Update();
                            if (checkOK)
                            {
                                MessageBox.Show(NewuGlobal.GetRes("000805"));
                            }
                            else
                            {
                                MessageBox.Show(NewuGlobal.GetRes("000806"));
                            }
                            btnClose.Enabled = true;
                            button1.Enabled = true;
                        }));
                        rptRepo.AddList(resultList);
                    }
                    await Task.Delay(1000);
                }
                catch (Exception e)
                {
                    NewuGlobal.MemMgr.Sync(1056, "0000");  //结束校验
                    NewuGlobal.MemMgr.Sync(1060, "0000");  //重置开始校秤信号
                    startCheck = false;
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        btnClose.Enabled = true;
                        button1.Enabled = true;
                    }));
                    NewuGlobal.LogCat("FM_CheckScale").Error(e.ToString());
                }
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FM_CheckScale_Add f = new FM_CheckScale_Add
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            f.ShowDialog();
            GetData();
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
                ScaleCheckSet m = bs.Current as ScaleCheckSet;
                if (m != null)
                {
                    FM_CheckScale_Add f = new FM_CheckScale_Add(m)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    f.ShowDialog();
                    GetData();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0 && dgv.CurrentCell == null)
                {
                    return;
                }
                int rowIndex = dgv.CurrentCell.RowIndex;
                if (rowIndex >= 0)
                {
                    string id = dgv[0, rowIndex].Value.ToString();
                    string scalename = dgv[4, rowIndex].Value.ToString();
                    DialogResult isDel = MessageBox.Show("[ " + scalename + " ] " + NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (isDel == DialogResult.Yes)
                    {
                        ScaleCheckSetRepository scaleRepo = new ScaleCheckSetRepository();
                        bool isAccess = scaleRepo.Delete(id);
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
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_CheckScale").Error(ex.ToString());
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            try
            {
                SetLanguage();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_CheckScale").Error(ex.ToString());
            }
        }

        private void SetLanguage()
        {
            btnAdd.Text = NewuGlobal.GetRes("000100");
            btnEdit.Text = NewuGlobal.GetRes("000101");
            btnDel.Text = NewuGlobal.GetRes("000102");
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnQuery.Text = NewuGlobal.GetRes("000104");
            button1.Text = NewuGlobal.GetRes("000111");
            groupBox1.Text = NewuGlobal.GetRes("000189");
            groupBox2.Text = NewuGlobal.GetRes("000112");
            groupBox3.Text = NewuGlobal.GetRes("000113");
            lab_Equipment.Text = NewuGlobal.GetRes("000182") + ":";

            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000182");
            int start = 114;
            for (int i = 2; i < dgv.Columns.Count - 2; i++)
            {
                dgv.Columns[i].HeaderText = NewuGlobal.GetRes("000" + start);
                start++;
            }
            dgv.Columns[7].HeaderText = NewuGlobal.GetRes("000186");
            dgv.Columns[8].HeaderText = NewuGlobal.GetRes("000187");

            dgv1.Columns[1].HeaderText = NewuGlobal.GetRes("000182");
            dgv1.Columns[2].HeaderText = NewuGlobal.GetRes("000114");
            dgv1.Columns[3].HeaderText = NewuGlobal.GetRes("000116");
            dgv1.Columns[4].HeaderText = NewuGlobal.GetRes("000117");
            dgv1.Columns[5].HeaderText = NewuGlobal.GetRes("000118");
            dgv1.Columns[6].HeaderText = NewuGlobal.GetRes("000628");
            dgv1.Columns[7].HeaderText = NewuGlobal.GetRes("000113");
            dgv1.Columns[8].HeaderText = NewuGlobal.GetRes("000186");
            dgv1.Columns[9].HeaderText = NewuGlobal.GetRes("000187");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnDel.Padding = new Padding(0, 0, 7, 0);
                btnClose.Padding = new Padding(0, 0, 7, 0);
                button1.Size = new Size(85, 30);
                btnClose.Padding = new Padding(0, 0, 7, 0);
                btnClose.Location = new Point(400, 20);
            }
            else
            {
                btnQuery.Padding = btnDel.Padding = new Padding(0, 0, 0, 0);
                btnClose.Padding = new Padding(0, 0, 0, 0);
                button1.Size = new Size(130, 30);
                btnClose.Padding = new Padding(0, 0, 0, 0);
                btnClose.Location = new Point(440, 20);
            }
        }
    }
}