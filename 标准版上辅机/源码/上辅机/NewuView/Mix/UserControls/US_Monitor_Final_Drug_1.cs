using MultiLanguage;
using NewuCommon;
using Repository.GlobalConfig;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class US_Monitor_Final_Drug_1 : UserControl, ILanguageChanged
    {
        private PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
        public CSharedString ss = NewuGlobal.MemDB;

        public CSharedString SS
        {
            set
            {
                ss = value;
            }
            get
            {
                return ss;
            }
        }

        public US_Monitor_Final_Drug_1()
        {
            InitializeComponent();
        }

        private void US_MonitorBase_Load(object sender, EventArgs e)
        {
            SetControlLanguageText();
            //异步监控
            NewRefresh_UI();
            NewuGlobal.UserMonitor = this;
        }

        #region 异步轮训，更新UI界面

        private async void NewRefresh_UI()
        {
            await Task.Run(() => NewMonitorRefresh());
        }

        private async void NewMonitorRefresh()
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

            if (plc_state.ConnectTionState)
            {
                DisPlayYiBiao();
                // 胶料 画面动画
                DisPlayRubberScale();

                //动作密炼机
                ViewDisplay.MixPart(MixPart1);
            }
        }

        #endregion 异步轮训，更新UI界面

        /// OJBK 所有仪表类型监控
        private void DisPlayYiBiao()
        {
            //----------------------------------------------胶料磅秤值
            ScaleDisPlay.Scale_RFinal(YiBiaoRubber);
            //----------------------------------------------小药秤值
            ScaleDisPlay.Scale_DFinal(YiBiaoDrug);
            //----------------------------------------------密炼机相关仪表
            ScaleDisPlay.Scale_MFinal(YiBiaoTimeTime);
        }

        /// <summary>
        /// 供胶机  胶料秤 传送带 光电开关 界面显示
        /// </summary>
        private void DisPlayRubberScale()
        {
            Send_Rubber.setScaleState(SS.Getbool(696), SS.Getbool(700));
            Scale_Rubber.setScaleState(SS.Getbool(697), SS.Getbool(701));
            Scale_Drug.setScaleState(SS.Getbool(719), SS.Getbool(715));
            Send_Drug.setScaleState(SS.Getbool(718), SS.Getbool(714));

            //供胶机电机
            RubberGoJiao1.setImageTag(SS.Getbool(698));
            //RubberGoJiao2.setImageTag(SS.getbool(699));
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        protected void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            YiBiaoRubber.SetTitle(NewuGlobal.GetRes("000156"));
            YiBiaoDrug.SetTitle(NewuGlobal.GetRes("000724"));
            YiBiaoTimeTime.SetTitle(NewuGlobal.GetRes("000158"));
        }
    }
}