using System;
using System.Windows.Forms;
using NewuBLL;
using System.Threading.Tasks;
namespace NewuView.Mix
{
    public partial class Base_Monitor_Z : Form
    {
        public NewuCommon.CSharedString ss = NewuBLL.NewuGlobal.MemDB;
        public NewuCommon.CSharedString SS
        {
            set { ss = value; ViewDisplay.Init(value); ScaleDisPlay.Init(value); }
            get { return ss; }
        }
        public Base_Monitor_Z()
        {
            InitializeComponent();
        }
        private void Base_Monitor_Z_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                NewRefresh_UI();
                /**** 曲线 ****/
                InitCurve();
            }
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
        void MonitorRefresh()
        {
            if (this.InvokeRequired)
            {
                Action refresh = () => MonitorRefresh();
                this.Invoke(refresh);
                return;
            }
            //匿名委托开始
            if (sonHandleEvent != null)
                sonHandleEvent();
            DisPlayYiBiao();
            DisPlayRubberScale();
            ViewDisplay.MixPartPart(Part_Mix);
        }
        #endregion
        /// 所有仪表类型监控
        void DisPlayYiBiao()
        {
            ScaleDisPlay.Scale_R(YB_Rubber);
            ScaleDisPlay.Scale_M(YiBiaoPowerEnergy, YiBiaoPressSpeed, YiBiaoMixTemp, YiBiaoTimeTime);
        }
        /// 胶料磅秤
        void DisPlayRubberScale()
        {
            Send_Rubber.setScaleState(SS.getbool(696), SS.getbool(700));
            Scale_Rubber.setScaleState(SS.getbool(697), SS.getbool(701));
            //供胶机1电机
            RubberGoJiao1.setImageTag(SS.getbool(698));
            //供胶机2电机
            RubberGoJiao2.setImageTag(SS.getbool(699));
        }
        #region  实时曲线 代码块
        int cnt = 0;
        bool mixIsRun = false;

        System.Windows.Forms.Timer timerRealCurve = new System.Windows.Forms.Timer();
        private void InitCurve()
        {
            timerRealCurve.Interval = 1000;
            timerRealCurve.Enabled = true;
            timerRealCurve.Tick += new EventHandler(timerRealCurve_Tick);
            for (int i = 1; i <= 82; i++)
            {
                chart1.Series["bg"].Points.AddXY(i, 200);
            }
            foreach (var areas in chart1.ChartAreas)
            {
                areas.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                areas.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            }
        }
        void timerRealCurve_Tick(object sender, EventArgs e)
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
            if (SS.getbool(678) && mixIsRun)
            {
                mixIsRun = false;
                cnt = 0;
            }
        }
        private void addPoint()
        {

            chart1.Series["温度"].Points.AddXY(++cnt, (1.0 * SS.getInt(1176, 4)) / ScaleAccuracy.digitTemp);
            chart1.Series["功率"].Points.AddXY(cnt, SS.getInt(1180, 4) / 2.0);
            chart1.Series["压力"].Points.AddXY(cnt, SS.getInt(1184, 4) / ScaleAccuracy.digitPress * 10000);
            chart1.Series["转速"].Points.AddXY(cnt, SS.getInt(1188, 4));
            chart1.Series["能量"].Points.AddXY(cnt, (1.0 * SS.getInt(1192, 4) * 50) / ScaleAccuracy.digitEnergy);
            chart1.Series["电压"].Points.AddXY(cnt, SS.getInt(1208, 4));
            chart1.Series["栓位"].Points.AddXY(cnt, SS.getHex(1072, 4) / 10);

        }
        #endregion
    }
}
