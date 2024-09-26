using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Newu;
using NewuBLL;
using NewuControl;
using NewuCommon;
using NewuModel;
using NewuBLL.MixBll;
using Repository.GlobalConfig;

namespace NewuView.Mix
{
    /// <summary>
    /// 不再维护。  2018.9.11  by wings
    /// </summary>
    public partial class FM_MonitorFinalMixer : Form, IRefresh
    {
        // 2.100
        /****   上面 继续看 *****/
        private int plcCnt = 0;
        private int plcMMP = 0;
        private bool isTestPLC = false;

        private delegate void monitorView();

        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        private NewuCommon.CSharedString SS = NewuGlobal.MemDB;
        private NewuCommon.CSharedString SW = NewuGlobal.MemW;
        private NewuCommon.CSharedString SF = NewuGlobal.MemF;
        public static int temp = 0;

        public FM_MonitorFinalMixer()
        {
            InitializeComponent();
        }

        private void FM_MonitorWRJ_Load(object sender, EventArgs e)
        {
            NewuGlobal.MixDataChange = this;
            //  timer 刷新 有待验证
            int message = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("主线程ID：  " + message);
            timer1.Interval = 200;
            timer1.Enabled = true;

            timer2.Interval = 2000;
            timer2.Enabled = true;
            // scrollDisplayBar1.Width = this.Width;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            // 初始化 两个DataGridView   dgvWeight & dgvTech
            ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);

            OrderName.Text = NewuGlobal.Now_OrderName;

            scrollDisplayBar1.Width = this.Width;

            /**** 曲线 ****/
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

        #region 实时曲线 代码块

        private int cnt = 0;
        private bool mixIsRun = false;

        private System.Windows.Forms.Timer timerRealCurve = new System.Windows.Forms.Timer();

        private void timerRealCurve_Tick(object sender, EventArgs e)
        {
            //开始线程刷新
            // xx.Add(++cnt);
            // l2.Add(new Random().Next(100));
            // chart1.Series["压力"].Points.DataBindXY(xx, l2);
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
                //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                //chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                // chart1.Series["bg"].Points.AddXY(cnt, new Random().Next(400));
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
            chart1.Series["压力"].Points.AddXY(cnt, SS.getInt(1184, 4) / ScaleAccuracy.digitPress * 100);
            chart1.Series["转速"].Points.AddXY(cnt, SS.getInt(1188, 4));
            chart1.Series["能量"].Points.AddXY(cnt, (1.0 * SS.getInt(1192, 4) * 50) / ScaleAccuracy.digitEnergy);
            //chart1.Series["电压"].Points.AddXY(cnt, SS.getInt(1208, 4));
            chart1.Series["栓位"].Points.AddXY(cnt, SS.getHex(1072, 4) / 10);
        }

        #endregion 实时曲线 代码块

        private void timer1_Tick(object sender, EventArgs e)
        {
            //开始线程刷新
            //    Console.WriteLine(temp + " timer thread is" + Thread.CurrentThread);
            Thread thMonitor = new Thread(new ThreadStart(MonitorRefresh));
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Thread thMonitor = new Thread(new ThreadStart(changePLCConnect));
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }

        private void PLCConnectState()
        {
            if (!isTestPLC)
            {
                isTestPLC = true;
                SW.setStr(9176, "1");
                SF.setStr(9176, "1");
                if (SF.SleepFlag(9176, 1, "0"))
                {
                    tv_PLCState.Text = "通讯正常";
                }
                else
                {
                    tv_PLCState.Text = "连接错误";
                    //return false;
                }
                isTestPLC = false;
            }
        }

        private void changePLCConnect()
        {
            monitorView test2 = delegate
            {
                PLCConnectState();
            };
            if (this.IsDisposed == false)
            {
                this.Invoke(test2);
            }
        }

        private void MonitorRefresh()
        {
            //匿名委托开始
            monitorView test2 = delegate
            {
                DisPlayYiBiao();
                DisPlayRubberScale();
                //overPLCState();
                ViewDisplay.MixPartPart(Part_Mix);
                ViewDisplay.RefreshMixTechDataGridView(dgvTech);
                ViewDisplay.RefreshWeightDataGridView(dgvWeight);
                label1.Text = DateTime.Now.ToString();
            };
            if (this.IsDisposed == false)
            {
                this.Invoke(test2);
            }
        }

        /// 所有仪表类型监控
        private void DisPlayYiBiao()
        {
            if (SS.getbool(83))
            {
                SS.setStr(83, "0");
                scrollDisplayBar1.setContent(NewuGlobal.AlarmInfo);
            }
            scrollDisplayBar1.run();
            tb_setCar.Text = SS.getInt(1116, 4).ToString();//设定车次;
            tb_nowCar.Text = SS.getInt(1156, 4).ToString();//实际车次
            ScaleDisPlay.Scale_R(YB_Rubber);
            ScaleDisPlay.Scale_M(YiBiaoPowerEnergy, YiBiaoPressSpeed, YiBiaoMixTemp, YiBiaoTimeTime);
            // YiBiaoPowerEnergy.NewuYiBiaoValue = SS.getInt(1180, 4).ToString();   //功率
            // YiBiaoPowerEnergy.NewuYiBiaoValue2 = SS.getInt(1192, 4).ToString();  //能量

            // YB_PressSpeed.NewuYiBiaoValue = SS.getInt(1184, 4).ToString();
            // YB_PressSpeed.NewuYiBiaoValue2 = SS.getInt(1188, 4).ToString();

            // //YB_MixTemp.NewuScaleValue = SS.getStr(1176, 4);//温度

            //YB_MixTemp.NewuScaleValue = FunClass.GetMemStrDec(SS.getStr(1176, 4), 0).ToString();//温度
            // YB_MixTemp.NewuScaleBatch = SS.getInt(1156, 4).ToString();//车次
            // YB_MixTemp.NewuScaleAuto = SS.getbool(681);//密炼自动
            // YB_MixTemp.NewuScaleStartRun = SS.getbool(682);//密炼运行
            // YB_MixTemp.NewuScaleOverAlarm = SS.getbool(683);

            // YB_MixTime.NewuYiBiaoValue = SS.getInt(1196, 4).ToString(); // 时间分布

            // YB_drug.NewuYiBiaoValue = SS.getInt(1044,4).ToString();
        }

        //private void overPLCState()
        //{
        //    if (SS.getbool(832))
        //    {
        //        plcMMP++;
        //    }
        //    else
        //    {
        //        plcMMP = 0;
        //        tv_PLCState.Text = "PLC连接正常";
        //    }
        //    if (plcMMP >= 4)
        //    {
        //        plcMMP = 10;
        //        tv_PLCState.Text = "连接异常";
        //    }

        //}
        /// 胶料磅秤
        private void DisPlayRubberScale()
        {
            send_rubber.setScaleState(SS.getbool(696));
            Scale_Rubber.setScaleState(SS.getbool(697));

            //供胶机1电机
            if (SS.getbool(698))
            {
                RubberGoJiao1.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                RubberGoJiao1.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            //供胶机1电机
            if (SS.getbool(699))
            {
                RubberGoJiao2.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                RubberGoJiao2.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //投料带光电开关
            if (SS.getbool(700))
            {
                Sensor_DrugScale.Visible = true;
            }
            else
            {
                Sensor_DrugScale.Visible = false;
            }
            //胶料秤光电开关
            if (SS.getbool(701))
            {
                Senso_RubberScale.Visible = true;
            }
            else
            {
                Senso_RubberScale.Visible = false;
            }
        }

        public void RefreshData()
        {
            //Todo:实现配方变更后的  数据显示功能！！！
            Console.WriteLine("wings, 配方数据变更了@@@@");
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            OrderName.Text = NewuGlobal.Now_OrderName;
            //NewuModel.PM_OrderTranMDL
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
                //tb_OrderName.Text = NewuGlobal.Now_OrderName;
            }
            // ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
            // tb_OrderName.Text = NewuGlobal.Now_OrderName;
        }
    }
}