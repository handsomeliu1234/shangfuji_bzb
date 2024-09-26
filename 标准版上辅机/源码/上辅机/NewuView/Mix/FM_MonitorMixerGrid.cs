using System;
using System.Windows.Forms;
using System.Threading;

using Newu;
using System.Threading.Tasks;
using Repository.GlobalConfig;

namespace NewuView.Mix
{
    public partial class FM_MonitorMixerGrid : Form, IRefresh
    {
        private delegate void monitorView();

        private NewuCommon.CSharedString SS = NewuGlobal.MemDB;

        private Thread thMonitor = null;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        private NewuBLL.FormulaWeighBLL FormulaWeightBll = new NewuBLL.FormulaWeighBLL();
        private NewuBLL.FormulaMixBLL FormulaMixBll = new NewuBLL.FormulaMixBLL();

        public FM_MonitorMixerGrid()
        {
            InitializeComponent();
        }

        private void FM_MonitorMixerGrid_Load(object sender, EventArgs e)
        {
            NewuGlobal.MixGridDataChange = this;

            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            //开始异步轮训刷新
            NewRefreshUI();
        }

        private async void NewRefreshUI()
        {
            await Task.Run(() => RecycleMonitorRefresh());
        }

        private async void RecycleMonitorRefresh()
        {
            while (true)
            {
                MonitorRefresh();
                await Task.Delay(300);
            }
        }

        private void MonitorRefresh()
        {
            if (this.InvokeRequired)
            {
                Action ac = () => MonitorRefresh();
                this.Invoke(ac);
                return;
            }
            ViewDisplay.RefreshMixTechDataGridView(dgvTech);
            ViewDisplay.RefreshWeightDataGridView(dgvWeight);
            ViewDisplay.MixPartPart(mixPart1);
            DisPlayYiBiao();
        }

        private void DisPlayYiBiao()
        {
            //----------------------------------------------炭黑仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_C(YiBiaoCarbon);
            //----------------------------------------------塑解剂仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_P(YiBiaoPla);
            //----------------------------------------------硅烷仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_S(YiBiaoSil);
            //----------------------------------------------油料仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_O(YiBiaoOil);
            //----------------------------------------------小药仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_D(YiBiaoDrug);
            //----------------------------------------------胶料仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_R(YiBiaoRubber);
            //----------------------------------------------粉料仪表磅秤数值显示
            ScaleDisPlay.DeatailScale_Z(YiBiaoZon);
            //----------------------------------------------密炼机仪表数值显示
            ScaleDisPlay.Scale_M(YiBiaoPowerEnergy, YiBiaoPressSpeed, YiBiaoMixTemp, YiBiaoTimeTime);
        }

        /// <summary>
        /// 实时称量信息  todo ：2018.4.25  这是啥玩意  意义何在！
        /// </summary>
        //public void WeightRealTime()
        //{
        //    NewuGlobal.RunInfo.ScaleRealTimeRun(DevicePartType.Carbon, SS.getInt(1020, 4), SS.getInt(1016, 4), SS.getInt(404, 4));

        //    NewuGlobal.RunInfo.ScaleRealTimeRun(DevicePartType.Oil, SS.getInt(1124, 4), SS.getInt(1024, 4), SS.getInt(412, 4));

        //    NewuGlobal.RunInfo.ScaleRealTimeRun(DevicePartType.DrugMixer, SS.getInt(1140, 4), SS.getInt(1100, 4), SS.getInt(436, 4));

        //    NewuGlobal.RunInfo.ScaleRealTimeRun(DevicePartType.Rubber, SS.getInt(1128, 4), SS.getInt(1028, 4), SS.getInt(420, 4));
        //}

        private void FM_MonitorMixerGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thMonitor != null) thMonitor.Abort();
        }

        public void RefreshData()
        {
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
        }

        public void RefreshData(bool isWeight)
        {
            //Todo:实现配方变更后的  数据显示功能！！！
            Console.WriteLine("wings, 配方数据变更了@@@@");
            if (isWeight)
            {
                ViewDisplay.DisPlayTableWeight(dgvWeight);
            }
            else
            {
                ViewDisplay.DisPlayTableMix(dgvTech);
                //  tb_OrderName.Text = NewuGlobal.Now_OrderName;
            }
            // ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            // tb_OrderName.Text = NewuGlobal.Now_OrderName;
        }
    }
}