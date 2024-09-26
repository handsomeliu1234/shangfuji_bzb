using MultiLanguage;
using NewuCommon;
using Repository.GlobalConfig;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    /// <summary>
    /// 界面监控
    /// </summary>
    public partial class FM_Monitor2 : Form, IRefresh, ILanguageChanged
    {
        private PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        private CSharedString SS = NewuGlobal.MemDB;

        public FM_Monitor2()
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
                tableLayoutPanel2.Controls.Add(us, 0, 0);
            }
            InitCurve();
        }

        private void FM_Monitor_Load(object sender, EventArgs e)
        {
            NewuGlobal.MixDataChange = this;
            label9.Text = NewuGlobal.SoftConfig.DeviceCode;
            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            scrollingText1.ScrollText = NewuGlobal.GetRes("000714");
            ContinuedOrder.GetInstance();
            lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            lb_nextOrderName.Text = NewuGlobal.Next_OrderName;

            plc_state.StartRun();

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
                NewuGlobal.LogCat("FM_Monitor").Error(ex.ToString());
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

                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningCarbon.Auto);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningRubber.Auto);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningOil.Auto);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningSilane.Auto);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningZno.Auto);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningDrug.Auto);

                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningCarbon.OverTolerance);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningRubber.OverTolerance);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningOil.OverTolerance);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningSilane.OverTolerance);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningZno.OverTolerance);
                    ViewDisplay.RefreshScaleStatus(p_carbonStatus, (int)MixerDigitalMiningDrug.OverTolerance);

                    lb_setBatch.Text = SS.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4).ToString();//设定车次;
                    lb_nowBatch.Text = SS.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4).ToString();//实际车次
                }

                lb_datatime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
                lb_nextOrderName.Text = NewuGlobal.Next_OrderName; // 下一配方
                lb_PLCState.Text = plc_state.ConnectTionState ? NewuGlobal.GetRes("000063") : NewuGlobal.GetRes("000064");
                lb_PLCState.ForeColor = plc_state.ConnectTionState ? Color.Black : Color.Red;
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

            lb_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            lb_nextOrderName.Text = NewuGlobal.Next_OrderName;
        }

        public void RefreshData(bool isWeight)
        {
            if (isWeight)
            {
                ViewDisplay.DisPlayTableWeight(dgvWeight);
                ViewDisplay.DisPlayTableMix(dgvTech);
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

        private void InitCurve()
        {
            for (int i = 1; i <= 122; i++)
            {
                chart1.Series["bg"].Points.AddXY(i, 400);
            }
            foreach (var areas in chart1.ChartAreas)
            {
                areas.AxisX.MajorGrid.LineColor = Color.Transparent;
                areas.AxisY.MajorGrid.LineColor = Color.Transparent;
            }
            Task.Run(() => SetChartVaulues());
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
                AddPoint();
            }
            if (SS.Getbool((int)MixerDigitalMiningMixer.BelowRamOpen) && mixIsRun)
            {
                mixIsRun = false;
                cnt = 0;
            }
        }

        private void AddPoint()
        {
            chart1.Series["温度.Temp"].Points.AddXY(++cnt, (1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Temp, 4)) / ScaleAccuracy.digitTemp);
            chart1.Series["功率.Power"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Power, 4) / 4);
            chart1.Series["压力.Press"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Press, 4));
            chart1.Series["转速.Speed"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Speed, 4));
            chart1.Series["能量.Energy"].Points.AddXY(cnt, (1.0 * SS.GetInt((int)MixerAnalogMiningMixer.Energy, 4) * 10) / ScaleAccuracy.digitEnergy);
            chart1.Series["电压.Voltage"].Points.AddXY(cnt, SS.GetInt((int)MixerAnalogMiningMixer.Voltage, 4));
            chart1.Series["栓位.Ram"].Points.AddXY(cnt, SS.GetHex((int)MixerAnalogMiningMixer.RealTimeRam, 4) / 10);
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
            label4.Text = NewuGlobal.GetRes("000125") + ":";// *
            label5.Text = NewuGlobal.GetRes("000126") + ":";// *
            label6.Text = NewuGlobal.GetRes("000127") + ":";// *

            lb_carbonStatus.Text = NewuGlobal.GetRes("000814");
            lb_rubberStatus.Text = NewuGlobal.GetRes("000814");
            lb_oilStatus.Text = NewuGlobal.GetRes("000814");
            lb_siStatus.Text = NewuGlobal.GetRes("000814");
            lb_znoStatus.Text = NewuGlobal.GetRes("000814");
            lb_drugStatus.Text = NewuGlobal.GetRes("000814");

            lb_carbonWeight.Text = NewuGlobal.GetRes("000815");
            lb_rubberWeight.Text = NewuGlobal.GetRes("000815");
            lb_oilWeight.Text = NewuGlobal.GetRes("000815");
            lb_siWeight.Text = NewuGlobal.GetRes("000815");
            lb_znoWeight.Text = NewuGlobal.GetRes("000815");
            lb_drugWeight.Text = NewuGlobal.GetRes("000815");
            /***********  常见按钮   ***********/
            LanguageDGV(dgvWeight, 130);
            LanguageDGV(dgvTech, 138);
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
    }
}