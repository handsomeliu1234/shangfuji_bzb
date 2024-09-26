using HZH_Controls.Controls;
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
    public partial class FM_Monitor : Form, IRefresh, ILanguageChanged
    {
        private PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        private CSharedString SS = NewuGlobal.MemDB;
        private RPT_WeightRepository rPT_WeightRepository = new RPT_WeightRepository();

        public FM_Monitor()
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
            panel3.Margin = new Padding(0, 2, 0, 0);
            if (!NewuGlobal.SoftConfig.DownMixer)
            {
                tabControlExt1.TabPages.Remove(tabPage2);
                tabControlExt2.TabPages.Remove(tabPage4);
                tabControlExt3.TabPages.Remove(tabPage6);

                for (int i = 0; i < tabControlExt1.TabPages.Count; i++)
                {
                    tabControlExt1.TabPages[i].Text = "";
                    tabControlExt2.TabPages[i].Text = "";
                    tabControlExt3.TabPages[i].Text = "";
                }
                panel3.Margin = new Padding(0, 4, 0, 0);
                tabControlExt1.HeadSelectedBorderColor = Color.FromArgb(152, 202, 240);
                tabControlExt2.HeadSelectedBorderColor = Color.FromArgb(152, 202, 240);
                tabControlExt3.HeadSelectedBorderColor = Color.FromArgb(152, 202, 240);
                tabControlExt1.ItemSize = tabControlExt2.ItemSize = tabControlExt3.ItemSize = new Size(0, 1);
                tabControlExt1.SizeMode = tabControlExt2.SizeMode = tabControlExt3.SizeMode = TabSizeMode.Fixed;
            }
            InitCurve();
        }

        private void FM_Monitor_Load(object sender, EventArgs e)
        {
            NewuGlobal.MixDataChange = this;
            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                ViewDisplay.InitMixTechDataGridView(dgvTechDown);
                ViewDisplay.DisPlayTableMix(dgvTechDown);
            }

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
                    ViewDisplay.RefreshMixTechDataGridView(dgvTech);
                    ViewDisplay.RefreshWeightDataGridView(dgvWeight);
                    if (NewuGlobal.SoftConfig.DownMixer)
                        ViewDisplay.RefreshMixTechDataGridViewF(dgvTechDown);

                    lb_setBatch.Text = SS.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4).ToString();//设定车次;
                    lb_nowBatch.Text = SS.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4).ToString();//实际车次

                    ScaleDisPlay.MixerLocation(p_highLocation, p_midLocation, p_lowLocation, p_addDoor, p_unLoadDoor, p_mixerStatus);

                    ScaleDisPlay.Scale_M(lb_press, lb_speed, lb_power, lb_energy, lb_temp, lb_factbatch, p_run, p_mixerStatus, p_alarm);

                    if (NewuGlobal.SoftConfig.DownMixer)
                        ScaleDisPlay.Scale_M_D(lbD_press, lbD_speed, lbD_power, lbD_energy, lbD_temp, lbD_factbatch, p_runD, p_alarmD, p_unLoadingD, p_mixerStatusD);
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
            ViewDisplay.RefreshMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            ViewDisplay.InitWeightDataGridView(dgvWeight);
            if (NewuGlobal.SoftConfig.DownMixer)
                ViewDisplay.RefreshMixTechDataGridViewF(dgvTechDown);

            lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            lb_nextOrderName.Text = NewuGlobal.Next_OrderName;
        }

        public void RefreshData(bool isWeight)
        {
            if (isWeight)
            {
                ViewDisplay.DisPlayTableWeight(dgvWeight);
                ViewDisplay.DisPlayTableMix(dgvTech);
                ViewDisplay.DisPlayTableMix(dgvTechDown);
                lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            }
            else
            {
                ViewDisplay.DisPlayTableMix(dgvTech);
                lb_nextOrderName.Text = NewuGlobal.Next_OrderName;
            }
        }

        #endregion 接口

        #region 实时曲线 代码块

        private int cnt = 0;
        private bool mixIsRun = false;
        private int cntD = 0;
        private bool mixDIsRun = false;

        private void InitCurve()
        {
            for (int i = 1; i <= 122; i++)
            {
                chart1.Series["bg"].Points.AddXY(i, 400);
                if (NewuGlobal.SoftConfig.DownMixer)
                    chart2.Series["bg"].Points.AddXY(i, 400);
            }

            for (int i = 0; i < chart1.ChartAreas.Count; i++)
            {
                chart1.ChartAreas[i].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas[i].AxisY.MajorGrid.LineColor = Color.Transparent;
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    chart2.ChartAreas[i].AxisX.MajorGrid.LineColor = Color.Transparent;
                    chart2.ChartAreas[i].AxisY.MajorGrid.LineColor = Color.Transparent;
                }
            }
            Task.Run(() => SetChartVaulues());

            if (NewuGlobal.SoftConfig.DownMixer)
                Task.Run(() => SetChart2Vaulues());
        }

        private async void SetChartVaulues()
        {
            try
            {
                while (true)
                {
                    if (IsHandleCreated)
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new EventHandler(delegate
                            {
                                SetChart();
                            }));
                        }
                        else
                        {
                            SetChart();
                        }
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Monitor").Error(ex.ToString());
            }
        }

        public void SetChart()
        {
            if (SS.Getbool((int)MixerDigitalMiningMixer.CurveStart) && mixIsRun == false)
            {
                mixIsRun = true;

                foreach (var series in chart1.Series)
                {
                    if (series.Name != "bg")
                        series.Points.Clear();
                }
                foreach (var areas in chart1.ChartAreas)
                {
                    areas.AxisX.MajorGrid.LineColor = Color.Transparent;
                    areas.AxisY.MajorGrid.LineColor = Color.Transparent;
                }
            }
            if (mixIsRun)
            {
                AddPoint(1);
            }
            if (SS.Getbool((int)MixerDigitalMiningMixer.BelowRamOpen) && mixIsRun)
            {
                mixIsRun = false;
                cnt = 0;
            }
        }

        private void AddPoint(int flag)
        {
            if (flag == 1)
            {
                chart1.Series["温度.Temp"].Points.AddXY(++cnt, 1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Temp, 4) / NewuGlobal.SoftConfig.TempValueScale);
                chart1.Series["功率.Power"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Power, 4) / NewuGlobal.SoftConfig.PowerValueScale);
                chart1.Series["压力.Press"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Press, 4) / NewuGlobal.SoftConfig.PressValueScale);
                chart1.Series["转速.Speed"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Speed, 4) / NewuGlobal.SoftConfig.SpeedValueScale);
                chart1.Series["能量.Energy"].Points.AddXY(cnt, 1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Energy, 4) / NewuGlobal.SoftConfig.EnergyValueScale);
                chart1.Series["电压.Voltage"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Voltage, 4) / NewuGlobal.SoftConfig.VoltageValueScale);
                chart1.Series["栓位.Ram"].Points.AddXY(cnt, SS.GetHex((int)MixerAnalogMiningMixer.RealTimeRam, 4) / NewuGlobal.SoftConfig.RamValueScale);
            }
            else
            {
                chart2.Series["温度.Temp"].Points.AddXY(++cntD, 1.0 * NewuGlobal.MemDB.GetInt((int)MixerAnalogMiningMixerDown.Temp, 4) / NewuGlobal.SoftConfig.TempValueScale);
                chart2.Series["功率.Power"].Points.AddXY(cntD, NewuGlobal.MemDB.GetInt((int)MixerAnalogMiningMixerDown.Power, 4) / NewuGlobal.SoftConfig.PowerValueScale);
                chart2.Series["压力.Press"].Points.AddXY(cntD, NewuGlobal.MemDB.GetInt((int)MixerAnalogMiningMixerDown.Press, 4) / NewuGlobal.SoftConfig.PressValueScale);
                chart2.Series["转速.Speed"].Points.AddXY(cntD, NewuGlobal.MemDB.GetInt((int)MixerAnalogMiningMixerDown.Speed, 4) / NewuGlobal.SoftConfig.SpeedValueScale);
                chart2.Series["能量.Energy"].Points.AddXY(cntD, 1.0 * NewuGlobal.MemDB.GetInt((int)MixerAnalogMiningMixerDown.Energy, 4) / NewuGlobal.SoftConfig.EnergyValueScale);
                chart2.Series["电压.Voltage"].Points.AddXY(cntD, NewuGlobal.MemDB.GetInt((int)MixerAnalogMiningMixerDown.Voltage, 4) / NewuGlobal.SoftConfig.VoltageValueScale);
            }
        }

        private async void SetChart2Vaulues()
        {
            try
            {
                while (true)
                {
                    if (IsHandleCreated)
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new EventHandler(delegate
                            {
                                SetChart2();
                            }));
                        }
                        else
                        {
                            SetChart2();
                        }
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Monitor").Error(ex.ToString());
            }
        }

        public void SetChart2()
        {
            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixerDown.CurveStart) && mixDIsRun == false)
            {
                mixDIsRun = true;

                foreach (var series in chart2.Series)
                {
                    if (series.Name != "bg")
                        series.Points.Clear();
                }
                foreach (var areas in chart2.ChartAreas)
                {
                    areas.AxisX.MajorGrid.LineColor = Color.Transparent;
                    areas.AxisY.MajorGrid.LineColor = Color.Transparent;
                }
            }
            if (mixDIsRun)
            {
                AddPoint(2);
            }
            if (NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningMixerDown.BelowRamOpen) && mixDIsRun)
            {
                mixDIsRun = false;
                cntD = 0;
            }
        }

        #endregion 实时曲线 代码块

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

            groupBox1.Text = groupBox2.Text = NewuGlobal.GetRes("000157");
            hslPanelText1.Text = NewuGlobal.GetRes("000660") + ":";
            tabPage1.Text = NewuGlobal.GetRes("000221");
            tabPage2.Text = NewuGlobal.GetRes("000222");
            tabPage3.Text = NewuGlobal.GetRes("000221");
            tabPage4.Text = NewuGlobal.GetRes("000222");
            tabPage5.Text = NewuGlobal.GetRes("000221");
            tabPage6.Text = NewuGlobal.GetRes("000222");

            label_press.Text = labelD_press.Text = NewuGlobal.GetRes("000639") + ":";
            label_speed.Text = labelD_speed.Text = NewuGlobal.GetRes("000640") + ":";
            label_power.Text = labelD_power.Text = NewuGlobal.GetRes("000637") + ":";
            label_energy.Text = labelD_energy.Text = NewuGlobal.GetRes("000638") + ":";
            label_temp.Text = labelD_temp.Text = NewuGlobal.GetRes("000636") + ":";
            label_factbatch.Text = labelD_factbatch.Text = NewuGlobal.GetRes("000575") + ":";
            label_mixerStatus.Text = labelD_mixerStatus.Text = NewuGlobal.GetRes("000684") + ":";
            label_run.Text = labelD_run.Text = NewuGlobal.GetRes("000303") + ":";
            label_alarm.Text = labelD_alarm.Text = NewuGlobal.GetRes("000818") + ":";
            label_feeding.Text = NewuGlobal.GetRes("000816") + ":";
            label_unLoading.Text = labelD_unloading.Text = NewuGlobal.GetRes("000817") + ":";
            /***********  常见按钮   ***********/
            LanguageDGV(dgvWeight, 130);
            LanguageDGV(dgvTech, 138);
            if (NewuGlobal.SoftConfig.DownMixer)
                LanguageDGV(dgvTechDown, 138);
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

        private void DgvWeight_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void DgvTech_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void DgvTech_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
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