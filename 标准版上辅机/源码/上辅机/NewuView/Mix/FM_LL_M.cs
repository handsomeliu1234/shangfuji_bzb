using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NewuBLL;
using System.Threading;

namespace NewuView.Mix
{
    public partial class FM_LL_M : Base_Monitor_M
    {
        PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        public FM_LL_M()
        {
            InitializeComponent();
            this.Text = "母练监控界面";
        }

        private void FM_LL_M_Load(object sender, EventArgs e)
        {
            tb_PLCState.ReadOnly = true;
            tb_nowCar.ReadOnly = true;
            tb_setCar.ReadOnly = true;
            tb_Weight_OrderName.ReadOnly = true;
            tb_Weight_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            tb_Mix_OrderName.ReadOnly = true;
            tb_Mix_OrderName.Text = NewuGlobal.Now_OrderName;
            //refresh_UI_Timer.Tick += new EventHandler(refrech_Tick);
            sonHandleEvent += new SonHandle(MonitorRefresh);
            plc_state.StartRun();


            //启动报警监控
            NewuBLL.MixBll.SaveAlarmBLL.GetInstance().LogAlarm += base.monitorLog;

        }
        private void MonitorRefresh()
        {
            tb_setCar.Text = SS.getInt(1116, 4).ToString();//设定车次;
            tb_nowCar.Text = SS.getInt(1156, 4).ToString();//实际车次
            tb_Weight_OrderName.Text = NewuGlobal.Now_Weight_OrderName;
            tb_Mix_OrderName.Text = NewuGlobal.Now_OrderName;
            label1.Text = DateTime.Now.ToString();
            ScrollDisPlay();
            tb_PLCState.Text = plc_state.ConnectTionState ? "连接成功" : "连接失败";
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

    }
}
