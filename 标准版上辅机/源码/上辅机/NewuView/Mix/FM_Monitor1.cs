using MultiLanguage;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Repository;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    /// <summary>
    /// 界面监控
    /// </summary>
    public partial class FM_Monitor1 : Form, IRefresh, ILanguageChanged
    {
        private PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        private CSharedString SS = NewuGlobal.MemDB;
        private RPT_WeightRepository rPT_WeightRepository = new RPT_WeightRepository();

        public FM_Monitor1()
        {
            InitializeComponent();
            InitView();
        }

        /// <summary>
        /// 初始化界面 添加控件
        /// </summary>
        private void InitView()
        {
            string viewType = "." + NewuGlobal.SoftConfig.MonitorView;
            UserControl us = GetType().Assembly.CreateInstance(GetType().Namespace + viewType) as UserControl;
            if (us != null)
            {
                us.Dock = DockStyle.Fill;
                panel6.Controls.Add(us);
            }
        }

        private void FM_Monitor_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterWidth = 16;//窗体中设置不起作用

            NewuGlobal.MixDataChange = this;
            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.DisPlayTableWeight(dgvWeight);
            scrollingText1.ScrollText = NewuGlobal.GetRes("000714");
            ContinuedOrder.GetInstance();
            lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            lb_nextOrderName.Text = NewuGlobal.Next_OrderName; // 下一配方
            plc_state.StartRun();

            lb_device.Text = NewuGlobal.SoftConfig.DeviceCode;

            SaveAlarmUtil.GetInstance().MonitorAlarm += FM_Monitor_LogAlarm;
            SaveAlarmUtil.GetInstance().PrepareData(true);

            SetControlLanguageText();
            Task.Run(() => Monitor());
        }

        private void FM_Monitor_LogAlarm(string alarmInfo)
        {
            try
            {
                if (scrollingText1.InvokeRequired)
                    BeginInvoke(new Action<string>(FM_Monitor_LogAlarm), alarmInfo);
                else
                    scrollingText1.ScrollText = alarmInfo;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Monitor").Error(message: ex.ToString());
            }
        }

        private async void Monitor()
        {
            while (true)
            {
                if (InvokeRequired)
                {
                    Action ac = new Action(DataRefresh);
                    this.Invoke(ac);
                }
                else
                {
                    DataRefresh();
                }
                await Task.Delay(300);
            }
        }

        private void DataRefresh()
        {
            try
            {
                if (plc_state.ConnectTionState)
                {
                    ViewDisplay.RefreshWeightDataGridView(dgvWeight);

                    lb_setBatch.Text = SS.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4).ToString();//设定车次;
                    lb_nowBatch.Text = SS.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4).ToString();//实际车次
                }

                lb_user.Text = NewuGlobal.TB_UserInfo.UserCode;
                lb_datatime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
                lb_nextOrderName.Text = NewuGlobal.Next_OrderName; // 下一配方

                if (NewuGlobal.SupportLanguage.Equals("1"))
                {
                    lb_PLCState.Text = plc_state.ConnectTionState ? NewuGlobal.LanguagResourceManager.GetString("000063", NewuGlobal.ZhCNCulture) : NewuGlobal.LanguagResourceManager.GetString("000064", NewuGlobal.ZhCNCulture);
                }
                else
                {
                    lb_PLCState.Text = plc_state.ConnectTionState ? NewuGlobal.LanguagResourceManager.GetString("000063", NewuGlobal.EnUSCulture) : NewuGlobal.LanguagResourceManager.GetString("000064", NewuGlobal.EnUSCulture);
                }

                lb_PLCState.ForeColor = plc_state.ConnectTionState ? Color.Green : Color.Red;

                //删除日志文件
                if (DateTime.Now.ToString("HH:mm:ss") == NewuGlobal.SoftConfig.DeleteFileLogTime)
                {
                    DelFiles(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\");
                    DelFiles(AppDomain.CurrentDomain.BaseDirectory + "\\HistoryVideo\\");
                }

                if (NewuGlobal.SoftConfig.DBCleanEnable)
                {
                    DBTableClear();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Monitor").Error(ex.ToString());
            }
        }

        #region 接口

        public void RefreshData()
        {
            ViewDisplay.DisPlayTableWeight(dgvWeight);
            ViewDisplay.InitWeightDataGridView(dgvWeight);

            lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            lb_nextOrderName.Text = NewuGlobal.Next_OrderName;
        }

        public void RefreshData(bool isWeight)
        {
            if (isWeight)
            {
                ViewDisplay.DisPlayTableWeight(dgvWeight);

                lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            }
            else
            {
                lb_nextOrderName.Text = NewuGlobal.Next_OrderName;
            }
        }

        #endregion 接口

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            label2.Text = NewuGlobal.GetRes("000123") + ":";// *
            label3.Text = NewuGlobal.GetRes("000124") + ":";// *
            label4.Text = NewuGlobal.GetRes("000125") + ":";
            label5.Text = NewuGlobal.GetRes("000126");// *
            label6.Text = NewuGlobal.GetRes("000127");// *
            label_device.Text = NewuGlobal.GetRes("000300") + ":";
            label_user.Text = NewuGlobal.GetRes("000089") + ":";

            /***********  常见按钮   ***********/
            LanguageDGV(dgvWeight, 130);
        }

        private void LanguageDGV(DataGridViewEx dgv, int start)
        {
            if (dgv != null && dgv.Columns != null && dgv.Name.Equals("dgvTech"))
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (i >= 2 && i < 4)
                    {
                        if (i % 2 == 0)
                            dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000809");
                        else
                            dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000810");
                    }
                    else if (i > 4)
                    {
                        if (i % 2 != 0)
                            dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000809");
                        else
                            dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000810");
                    }
                    else
                        dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                }
            }
            else
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                }
            }
        }

        #region 定时删除文件夹数据

        public void DelFiles(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return;
                }
                var dyInfo = new DirectoryInfo(path);
                foreach (var feInfo in dyInfo.GetDirectories())
                {
                    if (feInfo.LastWriteTime < DateTime.Now.AddDays(double.Parse("-" + NewuGlobal.SoftConfig.DeleteFileLogDays)))
                    {   //删除文件夹及子目录文件
                        feInfo.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Monitor").Error(ex.ToString());
            }
        }

        #endregion 定时删除文件夹数据

        #region 定时清理数据表数据

        public void DBTableClear()
        {
            rPT_WeightRepository.DeleteDataTable(NewuGlobal.SoftConfig.DBCleanYear);
        }

        #endregion 定时清理数据表数据
    }
}