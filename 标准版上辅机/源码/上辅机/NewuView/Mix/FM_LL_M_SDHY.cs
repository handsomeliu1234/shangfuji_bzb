using NewuBLL;
using System;
using System.Text;
using MultiLanguage;

namespace NewuView.Mix
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FM_LL_M_SDHY : Base_Monitor_M_SDHY, IRefresh, MultiLanguage.ILanguageChanged
    {
        PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        public FM_LL_M_SDHY()
        {
            InitializeComponent();
        }

        private void FM_LL_M_SDHY_Load(object sender, EventArgs e)
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
            SetControlLanguageText();
        }

        private new void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            label2.Text = NewuBLL.NewuGlobal.GetRes("000123");// *
            label7.Text = NewuBLL.NewuGlobal.GetRes("000124");// *
            label8.Text = NewuBLL.NewuGlobal.GetRes("000125");// *
            label5.Text = NewuBLL.NewuGlobal.GetRes("000126");// *
            label6.Text = NewuBLL.NewuGlobal.GetRes("000127");// *
            /***********  常见按钮   ***********/

            LanguageDGV(dgvWeight, 130);
            LanguageDGV(dgvTech, 138);

        }

        private void LanguageDGV(NewuCommon.DataGridViewEx dgv, int start)
        {
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            base.SetControlLanguageText();
            SetControlLanguageText();
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
            tb_PLCState.Text = plc_state.ConnectTionState ? NewuBLL.NewuGlobal.GetRes("000063") : NewuBLL.NewuGlobal.GetRes("000064");// "连接成功" : "连接失败";
            ViewDisplay.RefreshMixTechDataGridView(dgvTech);
            ViewDisplay.RefreshWeightDataGridView(dgvWeight);

            ////下面代码为通讯程序新加功能
            //StringBuilder sb = new StringBuilder();
            //float value = 0f;
            //NewuGlobal.MemMgr.Read<float>(1320, 4, out value);
            //sb.Append("挤出转速:" + value.ToString("0.000") + ",");

            //NewuGlobal.MemMgr.Read<float>(1324, 4, out value);
            //sb.Append("压片转速:" + value.ToString("0.000") + ",");

            //NewuGlobal.MemMgr.Read<float>(1328, 4, out value);
            //sb.Append("压力:" + value.ToString("0.000") + ",");

            //NewuGlobal.MemMgr.Read<float>(1332, 4, out value);
            //sb.Append("温度:" + value.ToString("0.000") + ",");

            //NewuGlobal.MemMgr.Read<float>(1336, 4, out value);
            //sb.Append("辊距:" + value.ToString("0.000") + ",");

            //NewuGlobal.MemMgr.Read<float>(1340, 4, out value);
            //sb.Append("挤出电流:" + value.ToString("0.000") + ",");

            //NewuGlobal.MemMgr.Read<float>(1344, 4, out value);
            //sb.Append("压片电流:" + value.ToString("0.000"));
            //labMixDown.Text = sb.ToString();
        }

        void ScrollDisPlay()
        {
            if (SS.getbool(81))
            {
                SS.setStr(81, "0");
                scrollingText1.ScrollText = string.Join(" ", NewuGlobal.AlarmInfo);
                //scrollDisplayBar1.setContent(NewuGlobal.AlarmInfo);
            }
            //scrollDisplayBar1.run();
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





