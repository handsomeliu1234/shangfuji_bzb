using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NewuControl;
using NewuBLL;
using System.Threading.Tasks;
namespace NewuView.Mix
{
    public partial class Base_Monitor_M_SDHY : Form
    {
        public NewuCommon.CSharedString ss = NewuBLL.NewuGlobal.MemDB;
        public NewuCommon.CSharedString SS
        {
            set { ss = value; ViewDisplay.Init(value); ScaleDisPlay.Init(value); }
            get { return ss; }
        }
        public Base_Monitor_M_SDHY()
        {
            InitializeComponent();
        }

        public void Base_Monitor_M_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ViewDisplay.Init(SS);
                ScaleDisPlay.Init(SS);
                //加载储罐的名字
                LoadBinName();
                LoadOilName();
                //异步监控 
                NewRefresh_UI();
                /**** 初始化曲线参数 ****/
                InitCurve();
            }
        }

        protected void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            YiBiaoOil1.SetTitle(NewuBLL.NewuGlobal.GetRes("000153"));
            YiBiaoCarbon.SetTitle(NewuBLL.NewuGlobal.GetRes("000154"));
            //YiBiaoCarbonMid.SetTitle(NewuBLL.NewuGlobal.GetRes("000155"));
            YiBiaoRubber.SetTitle(NewuBLL.NewuGlobal.GetRes("000156"));
            YiBiaoMixTemp.SetTitle(NewuBLL.NewuGlobal.GetRes("000157"));
            YiBiaoTimeTime.SetTitle(NewuBLL.NewuGlobal.GetRes("000158"));
            YiBiaoPressSpeed.SetTitle(NewuBLL.NewuGlobal.GetRes("000159"));
            YiBiaoPowerEnergy.SetTitle(NewuBLL.NewuGlobal.GetRes("000160"));
            xfLog1.SetTitle(NewuBLL.NewuGlobal.GetRes("000161"));

        }
        #region 异步轮训，更新UI界面
        //定义事件， 子界面需轮训时，往事件中 添加  自动轮训
        public delegate void SonHandle();
        public event SonHandle sonHandleEvent;
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

        private void refrech_UI_Tick(object sender, EventArgs e)
        {
            //开始线程刷新
            Thread thMonitor = new Thread(new ThreadStart(MonitorRefresh));
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }
        private void MonitorRefresh()
        {
            if (this.InvokeRequired)
            {
                Action ac = () => MonitorRefresh();
                this.Invoke(ac);
                return;
            }
            if (sonHandleEvent != null)
            {
                sonHandleEvent();
            }
            DisPlayYiBiao();
            DisPlayCarbonBin();
            DisPlayCarbonScale();
            DisPlayOilScale();


            // 胶料 画面动画
            DisPlayRubberScale();
            //动作密炼机
            ViewDisplay.MixPartPart(MixPart1);
        }
        #endregion
        #region 加载罐名
        // 加载油料罐的名称  暂时 不填入devicrID
        public void LoadOilName()
        {
            string _typeCodeName = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T油料);

            string _typeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName);
            DataSet ds = new NewuBLL.TB_BinSetingBLL().GetListJoinMaterialCode("", _typeCodeID);
            int cnt = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (cnt > 4) break;
                OilTank cb = this.Controls["oilTank" + cnt++] as OilTank;
                if (cb == null) continue;
                cb.NewuLabText = (cnt - 1) + "#" + "\n" + row["MaterialCode"].ToString();
            }

        }
        // 加载储罐的名称
        public void LoadBinName()
        {
            string _typeCodeNameTH = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T炭黑);
            string _typeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeNameTH);
            string _typeCodeNameBTH = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T白炭黑);
            string _typeCodeIDB = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeNameBTH);
            DataSet ds = new NewuBLL.TB_BinSetingBLL().GetListJoinMaterialCodeIn("", _typeCodeID, _typeCodeIDB);

            //ds.Merge(dsBTH);
            int cnt = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (cnt > 10) break;
                CarbonBin cb = this.Controls["carbonBin0" + cnt++] as CarbonBin;
                if (cb == null) continue;
                cb.NewuLabText = "  " + (cnt - 1) + "#" + "\n" + row["MaterialCode"].ToString();
            }
        }
        #endregion
        #region  实时曲线 代码块
        int cnt = 0;
        bool mixIsRun = false;

        System.Windows.Forms.Timer timerRealCurve = new System.Windows.Forms.Timer();
        private void InitCurve()
        {
            timerRealCurve.Interval = 1000;
            timerRealCurve.Enabled = true;
            timerRealCurve.Tick += new EventHandler(timerRealCurve_Tick);
            for (int i = 1; i <= 122; i++)
            {
                chart1.Series["bg"].Points.AddXY(i, 400);
            }
            foreach (var areas in chart1.ChartAreas)
            {
                areas.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                areas.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            }
        }
        private void timerRealCurve_Tick(object sender, EventArgs e)
        {
            if (SS.getbool(838) && mixIsRun == false)
            {
                mixIsRun = true;

                foreach (var series in chart1.Series)
                {
                    if (series.Name != "bg")
                        series.Points.Clear();
                }
                foreach (var areas in chart1.ChartAreas)
                {
                    areas.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                    areas.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                }
            }
            if (mixIsRun)
            {
                addPoint();
            }
            if (SS.getbool(838)==false && mixIsRun)
            {
                mixIsRun = false;
                cnt = 0;
            }
        }
        private void addPoint()
        {
            chart1.Series["温度"].Points.AddXY(++cnt, (1.0 * SS.getInt(1176, 4)) / ScaleAccuracy.digitTemp);
            chart1.Series["功率"].Points.AddXY(cnt, SS.getInt(1180, 4) / 4);
            chart1.Series["压力"].Points.AddXY(cnt, SS.getInt(1184, 4) / ScaleAccuracy.digitPress * 100);
            chart1.Series["转速"].Points.AddXY(cnt, (1.0 * SS.getInt(1188, 4))/ ScaleAccuracy.digitSpeed);
            chart1.Series["能量"].Points.AddXY(cnt, (1.0 * SS.getInt(1192, 4) * 10) / ScaleAccuracy.digitEnergy);
            chart1.Series["电压"].Points.AddXY(cnt, SS.getInt(1208, 4));
            chart1.Series["栓位"].Points.AddXY(cnt, SS.getHex(1072, 4) / 10);
        }
        #endregion
        /// <summary>
        /// 炭黑储罐以上监控 对应点核对核对正确   西门子PLC300
        /// </summary>
        void DisPlayCarbonBin()
        {
            // 炭黑 1 - 6 斗
            int startIndex = 37000;
            const int MAX_Posion = 7;
            try
            {
                for (int cnt = 1; cnt <= 6; cnt++)
                {
                    CarbonBin cb = this.Controls["carbonBin0" + cnt] as CarbonBin;
                    cb.NewuSet料位(SS.getInt(startIndex + (cnt - 1) * MAX_Posion, 4));
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        /// <summary>
        /// 炭黑 下料界面显示 
        /// </summary>
        void DisPlayCarbonScale()
        {
            // 加1 - 6 号炭黑 下炭管的颜色控制
            int StartPos = 704;
            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    NewuPicAngle pc = this.Controls["PipeCarbon" + i + j] as NewuPicAngle;
                    if (pc == null) break;
                    pc.setImageTag(SS.getbool(StartPos + i - 1));
                }
            }

            //炭黑称好
            CarbonScaleBin.setImageTag(SS.getbool(603));


            //炭黑排错位
            if (SS.getbool(726))
            {
                lblCarbonTroubleshooting.BackColor = Color.Lime; //炭黑排错位光电
                lblCarbonHost.BackColor = SystemColors.ControlLightLight;
            }
            else
            {
                lblCarbonTroubleshooting.BackColor = SystemColors.ControlLightLight; //炭黑排错位光电
                lblCarbonHost.BackColor = Color.Lime; //主机位
            }
            //投炭黑
            bool To = SS.getbool(721); //投炭黑
            bool Ro = SS.getbool(726);  //炭黑排错位
            FaCarbonScaleMid.setImageTag(To);
            PipeCarbonScaleMid01.setImageTag(To);
            PipeCarbonScaleMid03.setImageTag(To && !Ro);
            PipeCarbonScaleMid04.setImageTag(To && !Ro);
            PipeCarbonScaleMid05.setImageTag(To && !Ro);
            PipeCarbonScaleMid02.setImageTag(To && Ro);
        }

        /// OJBK 所有仪表类型监控
        void DisPlayYiBiao()
        {
            //----------------------------------------------炭黑磅秤值
            ScaleDisPlay.Scale_C(YiBiaoCarbon);
            //----------------------------------------------油料1磅秤值
            ScaleDisPlay.Scale_O(YiBiaoOil1);

            //----------------------------------------------胶料磅秤值
            ScaleDisPlay.Scale_R(YiBiaoRubber);
            //----------------------------------------------密炼机相关仪表
            ScaleDisPlay.Scale_M(YiBiaoPowerEnergy, YiBiaoPressSpeed, YiBiaoMixTemp, YiBiaoTimeTime);

        }
        /// <summary>
        /// 油料磅秤计量称量
        /// </summary>
        void DisPlayOilScale()
        {
            // 加1 - 4 号油料 下油管 和 泄油阀 的颜色控制
            int StartPos = 784;
            for (int i = 1; i <= 4; i++)
            {
                NewuPicAngle fa = this.Controls["FaOil0" + i] as NewuPicAngle;
                if (fa == null) continue;
                fa.setImageTag(SS.getbool(StartPos + i - 1));
                for (int j = 1; j <= 5; j++)
                {
                    NewuPicAngle pi = this.Controls["PipeOil" + i + j] as NewuPicAngle;
                    if (pi == null) break;
                    pi.setImageTag(SS.getbool(StartPos + i - 1));
                }
            }

            //1号油秤，称好
            OilScaleBin01.setImageTag(SS.getbool(619));
            //1号油秤中间斗，油料到位
            OilScaleMidBin01.setImageTag(SS.getbool(621));
            //1号油秤向中间斗卸油 （阀门，管）
            PipeOilScale01.setImageTag(SS.getbool(792));
            //faOilScale.setImageTag(SS.getbool(792));
            //1号油秤向密炼机注油 （阀门  两个管 电机）
            bool tempBool = SS.getbool(793);
            //faOilScaleMid.setImageTag(tempBool);
            PipeOilScaleMid11.setImageTag(tempBool);
            PipeOilScaleMid12.setImageTag(tempBool);
            MotorOil01.setImageTag(tempBool);

        }

        /// <summary>
        /// 供胶机  胶料秤 传送带 光电开关 界面显示 
        /// </summary>
        void DisPlayRubberScale()
        {
            Scale_Rubber.setScaleState(SS.getbool(697), SS.getbool(701));
        }
        protected virtual void monitorLog(int level, string msg)
        {
            switch (level)
            {
                case 1:
                    xfLog1.LogError(msg);
                    break;
                case 2:
                    xfLog1.LogMessage(msg);
                    break;
                default:
                    break;
            }
        }
        protected virtual void monitorLog(string time, string msg)
        {
            xfLog1.LogHistory(time, msg);

        }
    }
}
