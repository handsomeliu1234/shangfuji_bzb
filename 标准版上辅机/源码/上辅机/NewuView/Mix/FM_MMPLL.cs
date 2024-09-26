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

using NewuBLL.MixBll;
namespace NewuView.Mix
{
    /// <summary>
    /// 不再维护。  2018.9.11  by wings
    /// </summary>
    public partial class FM_MMPLL : Form
    {
        public FM_MMPLL()
        {
            InitializeComponent();
            this.Text = "玲珑监控界面_废弃版本";
        }
        delegate void monitorView();

        //SaveCurve sc = null;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        NewuCommon.CSharedString SS = NewuBLL.NewuGlobal.MemDB;
        NewuCommon.CSharedString SW = NewuBLL.NewuGlobal.MemW;
        NewuCommon.CSharedString SF = NewuBLL.NewuGlobal.MemF;
        private void FM_MonitorWRJ_Load(object sender, EventArgs e)
        {

            //测试开启报表
            // sc = new SaveCurve();
            // 在界面加载储罐名称
            tb_PLCState.ReadOnly = true;
            tb_nowCar.ReadOnly = true;
            tb_setCar.ReadOnly = true;
            tb_OrderName.ReadOnly = true;
            LoadBinName();
            LoadOilName();
            scrollDisplayBar1.Width = this.Width;
            int message = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("主线程ID：  " + message);
            timer1.Interval = 300;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Interval = 2000;
            timer2.Enabled = true;
            timer2.Tick += new EventHandler(timer2_Tick);


            tb_OrderName.Text = NewuGlobal.Now_OrderName;

            SetToolTip();  //mr_step  鼠标悬停显示Label含义


            /**** 曲线 ****/
            timerRealCurve.Interval = 1000;
            timerRealCurve.Enabled = true;
            timerRealCurve.Tick += new EventHandler(timerRealCurve_Tick);
            for (int i = 1; i <= 244; i++)
            {
                chart1.Series["bg"].Points.AddXY(i, 400);
            }
            foreach (var areas in chart1.ChartAreas)
            {
                areas.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
                areas.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            }


        }
        /// <summary>
        /// 此方法 鼠标悬停显示Label含义
        /// </summary>
        private void SetToolTip()
        {
            toolTip1.SetToolTip(lblCarbonHost, "炭黑主机位");
            toolTip1.SetToolTip(lblCarbonTroubleshooting, "炭黑排错位");

        }
        #region 加载罐名
        // 加载油料罐的名称  暂时 不填入devicrID
        void LoadOilName()
        {
            string _typeCodeName = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T油料);

            string _typeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName);
            DataSet ds = new NewuBLL.TB_BinSetingBLL().GetListJoinMaterialCode("", _typeCodeID);
            int cnt = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (cnt > 3) break;
                OilTank cb = this.Controls["oilTank" + cnt++] as OilTank;
                cb.NewuLabText = row["MaterialCode"].ToString();
            }

        }
        // 加载储罐的名称
        void LoadBinName()
        {

            //string _typeCodeNameTH = new NewuBLL.SYS_TypeCode().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCode.TypeCodeEnum.T炭黑);

            //string _typeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeNameTH);

            //DataSet ds = new NewuBLL.TB_BinSeting().GetListJoinMaterialCode("", _typeCodeID);

            //string _typeCodeNameBTH = new NewuBLL.SYS_TypeCode().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCode.TypeCodeEnum.T白炭黑);

            //string _typeCodeIDB = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeNameBTH);

            //DataSet dsBTH = new NewuBLL.TB_BinSeting().GetListJoinMaterialCode("", _typeCodeIDB);
            string _typeCodeNameTH = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T炭黑);
            string _typeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeNameTH);
            string _typeCodeNameBTH = new NewuBLL.SYS_TypeCodeBLL().GetTypeCodeNameByEnum(NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T白炭黑);
            string _typeCodeIDB = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeNameBTH);
            DataSet ds = new NewuBLL.TB_BinSetingBLL().GetListJoinMaterialCodeIn("", _typeCodeID, _typeCodeIDB);

            //ds.Merge(dsBTH);
            int cnt = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (cnt > 8) break;
                CarbonBin cb = this.Controls["carbonBin0" + cnt++] as CarbonBin;
                cb.NewuLabText = row["MaterialCode"].ToString();
            }
        }
        #endregion
        void timer1_Tick(object sender, EventArgs e)
        {
            //开始线程刷新
            Thread thMonitor = new Thread(new ThreadStart(MonitorRefresh));
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }

        bool isTestPLC = false;
        void timer2_Tick(object sender, EventArgs e)
        {
            Thread thMonitor = new Thread(new ThreadStart(changePLCConnect));
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }
        void changePLCConnect()
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
        private void PLCConnectState()
        {
            if (!isTestPLC)
            {
                isTestPLC = true;
                SW.setStr(9176, "1");
                SF.setStr(9176, "1");
                if (SF.SleepFlag(9176, 1, "0"))
                {
                    SS.setStr(832, "1");
                    tb_PLCState.Text = "通讯正常";
                }
                else
                {
                    SS.setStr(832, "0");
                    tb_PLCState.Text = "连接错误";
                    //return false;
                }
                isTestPLC = false;
            }

        }

        #region  实时曲线 代码块
        int cnt = 0;
        bool mixIsRun = false;

        System.Windows.Forms.Timer timerRealCurve = new System.Windows.Forms.Timer();
        void timerRealCurve_Tick(object sender, EventArgs e)
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
            chart1.Series["功率"].Points.AddXY(cnt, SS.getInt(1180, 4) / 4);
            chart1.Series["压力"].Points.AddXY(cnt, SS.getInt(1184, 4) / ScaleAccuracy.digitPress * 100);
            chart1.Series["转速"].Points.AddXY(cnt, SS.getInt(1188, 4));
            chart1.Series["能量"].Points.AddXY(cnt, (1.0 * SS.getInt(1192, 4) * 10) / ScaleAccuracy.digitEnergy);
            chart1.Series["电压"].Points.AddXY(cnt, SS.getInt(1208, 4));

            //--------上顶栓高中低位
            //if (SS.getStr(672, 1) == "1")
            //{
            //    chart1.Series["栓位"].Points.AddXY(cnt, 300);  //高位
            //}
            //else if (SS.getStr(674, 1) == "1")
            //{
            //    chart1.Series["栓位"].Points.AddXY(cnt, 100);  //低位
            //}
            //else
            //{
            //    chart1.Series["栓位"].Points.AddXY(cnt, 200); //中位
            //}
            chart1.Series["栓位"].Points.AddXY(cnt, SS.getHex(1072,4)/10 );
        }
        #endregion

        void MonitorRefresh()
        {
            //匿名委托开始
            monitorView test2 = delegate
            {

               // string str = DateTime.Now.ToString();
               // Console.WriteLine("num :" + cnt + "    " + str);
                DisPlayYiBiao();

                DisPlayCarbonBin();

                DisPlayCarbonScale();
                
                DisPlayOilScale();
                //回收罐
                DisPlayReCycCarbonBin();
                // 塑解剂 画面
                DisPlaySilScale();
                // 胶料 画面动画
                DisPlayRubberScale();
                //左上角时间
                label1.Text = DateTime.Now.ToString();
                //动作密炼机
                ViewDisplay.MixPartPart(MixPart1);
                // 显示报警信息
                ScrollDisPlay();

              //  str = DateTime.Now.ToString();
              //  Console.WriteLine("num :" + cnt + "    " + str);

            };
            //  int message = Thread.CurrentThread.ManagedThreadId;
            // Console.WriteLine(message);
            if (this.IsDisposed == false)
            {
                //  var a = DateTime.Now ;
                // Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " winsg 当前开始时间：" + DateTime.Now.ToString("hh:mm:ss:fff"));
                this.Invoke(test2);
                //    var b = DateTime.Now;
                //  Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " winsg 当前结束时间：" + DateTime.Now.ToString("hh:mm:ss:fff"));
                //  Console.WriteLine("-----时间差" + (b-a).ToString() );
            }

        }

        void ScrollDisPlay()
        {

            if (SS.getbool(81))
            {
                SS.setStr(81, "0");
                scrollDisplayBar1.setContent(NewuGlobal.AlarmInfo);
            }
            if (SS.getbool(80))
            {
                SS.setStr(80, "0");
                string str = NewuGlobal.AlarmInfo;
                StringBuilder stt = new StringBuilder(str);
                string sttr = "";
                tb_show_Alarm.Text = "";
                for (int i = 0; i < stt.Length; i++)
                {
                    if (stt[i] == ' ')
                    {
                        
                        tb_show_Alarm.Text += sttr + "\r\n";
                        sttr = "";
                    }
                    else
                    {
                        sttr = sttr + stt[i];
                    } 
                    
                }
                //tb_show_Alarm.Text = sttr.ToString();
            }
            scrollDisplayBar1.run();
        }
        #region OJBK
        /// <summary>
        /// 炭黑储罐以上监控 对应点核对核对正确   西门子PLC300
        /// </summary>
        void DisPlayCarbonBin()
        {
            // Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " 当前开始时间：" + DateTime.Now.ToString("hh:mm:ss:fff"));

            ReCycBin1.NewuSet料位(SS.getbool(37063), SS.getbool(37064), SS.getbool(37065), SS.getbool(37066));
            carbonBin01.NewuSet料位(SS.getbool(37000), SS.getbool(37001), SS.getbool(37002), SS.getbool(37003));
            carbonBin02.NewuSet料位(SS.getbool(37007), SS.getbool(37008), SS.getbool(37009), SS.getbool(37010));

            carbonBin03.NewuSet料位(SS.getbool(37014), SS.getbool(37015), SS.getbool(37016), SS.getbool(37017));
            carbonBin04.NewuSet料位(SS.getbool(37021), SS.getbool(37022), SS.getbool(37023), SS.getbool(37024));
            carbonBin05.NewuSet料位(SS.getbool(37028), SS.getbool(37029), SS.getbool(37030), SS.getbool(37031));
            carbonBin06.NewuSet料位(SS.getbool(37035), SS.getbool(37036), SS.getbool(37037), SS.getbool(37038));
            carbonBin07.NewuSet料位(SS.getbool(37042), SS.getbool(37043), SS.getbool(37044), SS.getbool(37045));
            carbonBin08.NewuSet料位(SS.getbool(37049), SS.getbool(37050), SS.getbool(37051), SS.getbool(37052));

        }
        void DisPlayReCycCarbonBin()
        {
            if (SS.getbool(716))
            {
                ReCycPipe11.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                ReCycPipe12.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                ReCycPipe13.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                ReCycPipe11.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                ReCycPipe12.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                ReCycPipe13.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
        }
        /// <summary>炭黑磅秤计量监控
        /// 
        /// </summary>
        void DisPlayCarbonScale()
        {
            //加1号炭黑
            if (SS.getbool(704))
            {
                PipeCarbon11.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon12.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon13.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon14.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon15.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon11.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon12.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon13.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon14.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon15.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            //加2号炭黑
            if (SS.getbool(705))
            {
                PipeCarbon21.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon22.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon23.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon24.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon25.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon21.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon22.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon23.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon24.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon25.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //加3号炭黑
            if (SS.getbool(706))
            {
                PipeCarbon31.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon32.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon33.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon34.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon35.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon31.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon32.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon33.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon34.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon35.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //加4号炭黑
            if (SS.getbool(707))
            {
                PipeCarbon41.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon41.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


            //加5号炭黑
            if (SS.getbool(708))
            {
                PipeCarbon51.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon51.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


            //加6号炭黑
            if (SS.getbool(709))
            {
                PipeCarbon61.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon62.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon63.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon64.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon65.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon61.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon62.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon63.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon64.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon65.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


            //加7号炭黑
            if (SS.getbool(710))
            {
                PipeCarbon71.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon72.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon73.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon74.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon75.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon71.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon72.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon73.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon74.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon75.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }



            //加8号炭黑
            if (SS.getbool(711))
            {
                PipeCarbon81.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon82.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon83.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon84.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbon85.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbon81.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon82.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon83.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon84.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbon85.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


            //炭黑称好
            if (SS.getbool(603))
            {
                CarbonScaleBin.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                CarbonScaleBin.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //炭黑中间斗有料
            if (SS.getbool(605))
            {
                CarbonScaleMidBin.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                CarbonScaleMidBin.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //卸炭黑
            if (SS.getbool(720))
            {
                FaCarbonScale.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbonScale.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeCarbonScale.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                FaCarbonScale.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
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
            if (SS.getbool(721))
            {
                FaCarbonScaleMid.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeCarbonScaleMid01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                if (SS.getbool(726))
                {
                    PipeCarbonScaleMid02.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                    PipeCarbonScaleMid03.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                    PipeCarbonScaleMid04.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                    PipeCarbonScaleMid05.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                }
                else
                {
                    PipeCarbonScaleMid03.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                    PipeCarbonScaleMid04.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                    PipeCarbonScaleMid05.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                }

            }
            else
            {
                FaCarbonScaleMid.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbonScaleMid01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbonScaleMid02.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbonScaleMid03.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbonScaleMid04.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeCarbonScaleMid05.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


        }

        #endregion
        /// OJBK 所有仪表类型监控
        void DisPlayYiBiao()
        {
            tb_setCar.Text = SS.getInt(1116, 4).ToString();//设定车次;
            tb_nowCar.Text = SS.getInt(1156, 4).ToString();//实际车次
            if (SS.getInt(1156, 4) == 0)
                tb_OrderName.Text = NewuGlobal.Now_OrderName;

            //----------------------------------------------炭黑磅秤值
            ScaleDisPlay.Scale_C(YiBiaoCarbon);
            //----------------------------------------------炭黑磅秤中间斗
            ScaleDisPlay.Scale_C_MID(YiBiaoCarbonMid);
            //----------------------------------------------油料1磅秤值
            ScaleDisPlay.Scale_O(YiBiaoOil1);
            //----------------------------------------------硅烷1磅秤值
            ScaleDisPlay.Scale_S(YiBiaoSil1);
            //----------------------------------------------小药秤值
            ScaleDisPlay.Scale_D(YiBiaoDrug);
            //----------------------------------------------胶料磅秤值
            ScaleDisPlay.Scale_R(YiBiaoRubber);
            //----------------------------------------------密炼机相关仪表
            ScaleDisPlay.Scale_M(YiBiaoPowerEnergy, YiBiaoPressSpeed, YiBiaoMixTemp, YiBiaoTimeTime);

            //----------------------------------------------塑解剂仪表值
            ScaleDisPlay.DeatailScale_P(PlasticizerYiBiao);
        }

        void DisPlaySilScale()
        {
            if (SS.getbool(824))
            {
                faSil01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeSil11.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                faSil01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeSil11.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            if (SS.getbool(825))
            {
                faSilScale.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeSilScale.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                faSilScale.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeSilScale.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            if (SS.getbool(826))
            {
                PipeSilScaleMid11.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeSilScaleMid12.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                MotorSil.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                faSilScaleMid.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeSilScaleMid11.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeSilScaleMid12.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                MotorSil.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                faSilScaleMid.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            //硅烷 中间斗有料
            if (SS.getbool(669))
            {
                SilScaleMidBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                
            }
            else
            {
                SilScaleMidBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                
            }
            //硅烷称称好
            if (SS.getbool(667))
            {
                SilScaleBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                SilScaleBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
        }
        /// <summary>油料磅秤计量称量
        /// 
        /// </summary>
        void DisPlayOilScale()
        {

            //加1号油
            if (SS.getbool(784))
            {
                FaOil01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil11.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil12.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil13.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil14.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil15.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                FaOil01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil11.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil12.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil13.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil14.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil15.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //加2号油
            if (SS.getbool(785))
            {
                FaOil02.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil21.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                FaOil02.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil21.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //加3号油
            if (SS.getbool(786))
            {
                FaOil03.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil31.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil32.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil33.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil34.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOil35.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                FaOil03.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil31.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil32.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil33.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil34.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOil35.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


            //1号油秤称好
            if (SS.getbool(619))
            {
                OilScaleBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                OilScaleBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //1号油秤中间斗油料
            if (SS.getbool(621))
            {
                OilScaleMidBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                faOilScaleMid.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                OilScaleMidBin01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                faOilScaleMid.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }

            //1号油秤卸油
            if (SS.getbool(792))
            {
                PipeOilScale01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                faOilScale.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeOilScale01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                faOilScale.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            //1号油秤注油
            if (SS.getbool(793))
            {
                PipeOilScaleMid11.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                PipeOilScaleMid12.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
                MotorOil01.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                PipeOilScaleMid11.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                PipeOilScaleMid12.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
                MotorOil01.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }


        }


        /// <summary>胶料磅秤计量
        /// 
        /// </summary>
        void DisPlayRubberScale()
        {

            RubberConveyor.setScaleState(SS.getbool(696));
            RubberScale.setScaleState(SS.getbool(697));
            DrugScale.setScaleState(SS.getbool(719));
            PlasticizerScale.setScaleState(SS.getbool(718));
            ////胶料投料带电机运行
            //if (SS.getbool(696))
            //{
            //    RubberConveyor.NewuPicDisplayStyle = NewuControl.RubberScale.NewuPicStyle.RubberConveyorRun;
            //}
            //else
            //{
            //    RubberConveyor.NewuPicDisplayStyle = NewuControl.RubberScale.NewuPicStyle.RubberConveyorStop;
            //}
            ////胶料秤电机运行
            //if (SS.getbool(697))
            //{
            //    RubberScale.NewuPicDisplayStyle = NewuControl.RubberScale.NewuPicStyle.RubberConveyorRun;
            //}
            //else
            //{
            //    RubberScale.NewuPicDisplayStyle = NewuControl.RubberScale.NewuPicStyle.RubberConveyorStop;
            //}
            //供胶机1电机
            if (SS.getbool(698))
            {
                RubberGoJiao1.NewuPicTypeStyle = NewuControl.NewuPicType.Background;
            }
            else
            {
                RubberGoJiao1.NewuPicTypeStyle = NewuControl.NewuPicType.Foreground;
            }
            //供胶机2电机
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
                RubberConveyorSensor.Visible = true;
            }
            else
            {
                RubberConveyorSensor.Visible = false;
            }
            //胶料秤光电开关
            if (SS.getbool(701))
            {
                RubberScaleSensor.Visible = true;
            }
            else
            {
                RubberScaleSensor.Visible = false;
            }
            //小药秤光电开关
            if (SS.getbool(715))
            {
                DrugScaleSensor.Visible = true;
            }
            else
            {
                DrugScaleSensor.Visible = false;
            }
            //塑解剂光电开关
            if (SS.getbool(714))
            {
                PlasticizerScaleCensor.Visible = true;
            }
            else
            {
                PlasticizerScaleCensor.Visible = false;
            }
        }
    }
}
