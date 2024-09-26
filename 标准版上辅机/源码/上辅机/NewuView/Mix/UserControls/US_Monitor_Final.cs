using MultiLanguage;
using NewuTB.Utils;
using Repository.GlobalConfig;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class US_Monitor_Final : UserControl, ILanguageChanged
    {
        private readonly PLC_Connection_State plcConnectionState = PLC_Connection_State.GetInstance();

        public US_Monitor_Final()
        {
            InitializeComponent();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void US_Monitor_Final_Load(object sender, EventArgs e)
        {
            SetControlLanguageText();
            RefreshUI();
            NewuGlobal.UserMonitor = this;
        }

        private async void RefreshUI()
        {
            try
            {
                await Task.Run(() => NewMonitorReresh());
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_Monitor_Final").Error(ex.ToString());
            }
        }

        private async void NewMonitorReresh()
        {
            try
            {
                while (true)
                {
                    MonitorRefresh();
                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_Monitor_Final").Error(ex.ToString());
            }
        }

        private void MonitorRefresh()
        {
            try
            {
                if (InvokeRequired)
                {
                    Action ac = () => MonitorRefresh();
                    Invoke(ac);
                    return;
                }
                if (plcConnectionState.ConnectTionState)
                {
                    DisPlayYiBiao();

                    // 胶料 画面动画
                    DisPlayRubberScale();

                    //动作密炼机
                    ViewDisplay.MixPart(mixPart1);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_Monitor_Final").Error(ex.ToString());
            }
        }

        private void DisPlayYiBiao()
        {
            try
            {
                //胶料磅秤值
                ScaleDisPlay.Scale_R(YiBiaoRubber);

                //密炼机相关仪表
                ScaleDisPlay.Scale_MFinal(YiBiaoTimeTime);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_Monitor_Final").Error(ex.ToString());
            }
        }

        private void DisPlayRubberScale()
        {
            try
            {
                Send_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeedingBeltPE));
                Scale_Rubber.setScaleState(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScaleMotor), NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.RubberScalePE));
                RubberGoJiao1.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeederMotor1));
                RubberGoJiao2.setImageTag(NewuGlobal.MemDB.Getbool((int)MixerDigitalMiningRubber.FeederMotor2));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("US_Monitor_Final").Error(ex.ToString());
            }
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            YiBiaoRubber.SetTitle(NewuGlobal.GetRes("000156"));
            YiBiaoTimeTime.SetTitle(NewuGlobal.GetRes("000158"));
        }
    }
}