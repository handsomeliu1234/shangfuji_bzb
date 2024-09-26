using NewuBLL;
using System;
using System.Text;

namespace NewuView.Mix
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FM_LL_M_QG : Base_Monitor_M_QG, IRefresh
    {
        PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        public FM_LL_M_QG()
        {
            InitializeComponent();
        }

        private void FM_LL_M_QG_Load(object sender, EventArgs e)
        {
            NewuGlobal.MixDataChange = this;
            
            //refresh_UI_Timer.Tick += new EventHandler(refrech_Tick);
            sonHandleEvent += new SonHandle(MonitorRefresh);
            plc_state.StartRun();
            //启动报警监控
            NewuBLL.MixBll.SaveAlarmBLL.GetInstance().LogAlarm += base.monitorLog;

            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            //tb_Weight_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            tbNextName.Text = NewuGlobal.Now_OrderName;
            tbNextName.Text = NewuGlobal.Next_OrderName;
        }
        private void MonitorRefresh()
        {
            tb_setCar.Text = SS.getInt(1116, 4).ToString();//设定车次;
            tb_nowCar.Text = SS.getInt(1156, 4).ToString();//实际车次
            //tb_Weight_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            //tb_Mix_OrderName.Text = NewuGlobal.Now_OrderName;
            tbNowName.Text = NewuGlobal.Now_OrderName;
            tbNextName.Text = NewuGlobal.Next_OrderName;

            label1.Text = DateTime.Now.ToString();
            ScrollDisPlay();
            tb_PLCState.Text = plc_state.ConnectTionState ? "连接成功" : "连接失败";
            ViewDisplay.RefreshMixTechDataGridView(dgvTech);
            ViewDisplay.RefreshWeightDataGridView(dgvWeight);

            //下面代码为通讯程序新加功能
            StringBuilder sb = new StringBuilder();
            float value = 0f;
            NewuGlobal.MemMgr.Read<float>(1320, 4, out value);
            sb.Append("挤出转速:" + value.ToString("0.000") + ",");

            NewuGlobal.MemMgr.Read<float>(1324, 4, out value);
            sb.Append("压片转速:" + value.ToString("0.000") + ",");

            NewuGlobal.MemMgr.Read<float>(1328, 4, out value);
            sb.Append("压力:" + value.ToString("0.000") + ",");

            NewuGlobal.MemMgr.Read<float>(1332, 4, out value);
            sb.Append("温度:" + value.ToString("0.000") + ",");

            NewuGlobal.MemMgr.Read<float>(1336, 4, out value);
            sb.Append("辊距:" + value.ToString("0.000") + ",");

            NewuGlobal.MemMgr.Read<float>(1340, 4, out value);
            sb.Append("挤出电流:" + value.ToString("0.000") + ",");

            NewuGlobal.MemMgr.Read<float>(1344, 4, out value);
            sb.Append("压片电流:" + value.ToString("0.000"));
            labMixDown.Text = sb.ToString();
        }

        void ScrollDisPlay()
        {
            if (SS.getbool(81))
            {
                SS.setStr(81, "0");
                scrollDisplayBar1.setContent(NewuGlobal.AlarmInfo);
            }
            scrollDisplayBar1.run();
        }


        public void RefreshData()
        {
            //Todo:实现配方变更后的  数据显示功能！
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            //tb_Weight_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            //tb_Mix_OrderName.Text = NewuGlobal.Now_OrderName;
            tbNowName.Text = NewuGlobal.Now_OrderName;
            tbNextName.Text = NewuGlobal.Next_OrderName;
        }
        public void RefreshData(bool isWeight)
        {
            //Todo:实现配方变更后的  数据显示功能！
            //Console.WriteLine("wings, 配方数据变更了@@@@");
            //if (isWeight)
            //{
            //    ViewDisplay.DisPlayTableWeight(dgvWeight);
            //    tb_Weight_OrderName.Text = NewuGlobal.Now_Weight_OrderName;

            //}
            //else
            //{
            //    ViewDisplay.DisPlayTableMix(dgvTech);
            //    tb_Mix_OrderName.Text = NewuGlobal.Now_OrderName;
            //}
            ViewDisplay.DisPlayTableWeight(dgvWeight);
            ViewDisplay.DisPlayTableMix(dgvTech);
            tbNowName.Text = NewuGlobal.Now_OrderName;
            tbNextName.Text = NewuGlobal.Next_OrderName;
            // ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            // tb_OrderName.Text = NewuGlobal.Now_OrderName;
        }

    }
};
