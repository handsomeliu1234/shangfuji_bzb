using System;
using NewuBLL;
using System.Threading;
namespace NewuView.Mix
{
    public partial class FM_LL_Z : Base_Monitor_Z, IRefresh
    {
        PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        public FM_LL_Z()
        {
            InitializeComponent();
            this.Text = "终练监控界面";
        }

        private void FM_LL_Z_Load(object sender, EventArgs e)
        {
            NewuGlobal.MixDataChange = this;
            SS = NewuGlobal.MemDB;
            tb_PLCState.ReadOnly = true;
            tb_nowCar.ReadOnly = true;
            tb_setCar.ReadOnly = true;
            tb_OrderName.ReadOnly = true;
            
            sonHandleEvent += new SonHandle(refrech_Tick);
            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            tb_OrderName.Text = NewuGlobal.Now_OrderName;
            plc_state.StartRun();

        }
        private void refrech_Tick()
        {
            tb_setCar.Text = SS.getInt(1116, 4).ToString();//设定车次
            tb_nowCar.Text = SS.getInt(1156, 4).ToString();//实际车次
            if (SS.getInt(1156, 4) == 0)
                tb_OrderName.Text = NewuGlobal.Now_OrderName;

            Action ScrollDisPlay = () =>
            {
                if (SS.getbool(81))
                {
                    SS.setStr(81, "0");
                    scrollDisplayBar1.setContent(NewuGlobal.AlarmInfo);
                }
                scrollDisplayBar1.run();
            };
            ScrollDisPlay();

            ViewDisplay.RefreshMixTechDataGridView(dgvTech);
            ViewDisplay.RefreshWeightDataGridView(dgvWeight);
            label1.Text = DateTime.Now.ToString();
            tb_PLCState.Text = plc_state.ConnectTionState ? "连接成功" : "连接失败";
        }
        public void RefreshData()
        {
            //Todo:实现配方变更后的  数据显示功能！！！
            Console.WriteLine("wings, 配方数据变更了@@@@");
            ViewDisplay.RefreshMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            tb_OrderName.Text = NewuGlobal.Now_OrderName;
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
                tb_OrderName.Text = NewuGlobal.Now_OrderName;
            }
            // ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            // tb_OrderName.Text = NewuGlobal.Now_OrderName;
        }
    }
}
