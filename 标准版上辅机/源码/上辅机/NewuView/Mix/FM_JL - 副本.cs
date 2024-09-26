using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Newu;
using NewuBLL;
using NewuModel;
using NewuCommon;
using System.Text;
using Newu.Net.TCP;
using System.Net;
using MultiLanguage;

namespace NewuView.Mix
{
    public partial class FM_JL : Form, IRefresh, ScanRefresh, MultiLanguage.ILanguageChanged
    {
        private TB_BinSetingBLL tbBinSettingBll = new TB_BinSetingBLL();
        private delegate void SetTextCallback(string text,bool visible);
        CSharedString ssW = NewuGlobal.MemW;
        CSharedString ssF = NewuGlobal.MemF;
        public Random random = new Random();
        string deviceCode = "";
        bool isSingleRawOk = false, isScaleEnd = false;
        int lastWeightNum = 0;
        NewuCommon.CSharedString SS = NewuBLL.NewuGlobal.MemDB;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        CSharedString ss = NewuGlobal.MemDB;

        private List<Scanner> scannerList = new List<Scanner>();
        PM_ScanRecordBLL pm_ScanRecordBLL = new PM_ScanRecordBLL();
        private FormulaWeighBLL matBll = new FormulaWeighBLL();
        private FormulaMaterialBLL fmbBll = new FormulaMaterialBLL();
        PM_DevicePartOrderTranBLL orderDeviceBll = new PM_DevicePartOrderTranBLL();
        FormulaWeighBLL rawBll = new FormulaWeighBLL();
        List<FormulaWeighMDL> rawList = null;
       
        //tododown: 翻屏信号  应编写初始化逻辑
        int[] scanRecord = new int[100];
        #region 按F7切换屏幕
        public delegate void ScreemChange();
        public ScreemChange scc = null;
        private void FM_JL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                if (scc != null)
                {
                    scc();
                }
            }
        }
        #endregion

        public FM_JL()
        {
            InitializeComponent();

            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
        }
        void MonitorRefresh()
        {
            if (this.InvokeRequired)
            {
                Action d = () => MonitorRefresh();
                this.Invoke(d);
                return;
            }
            try
            {
                ScrollBarDisPlay();
                disView();
                ViewHelper.RefreshMixTechDataGridView(dgvMlgy);
                labNowName.Text = NewuGlobal.Now_OrderName;
                labNextName.Text = NewuGlobal.Next_OrderName;
            }
            catch (Exception e)
            {
                FunClass.WriteLogFile("副屏:" + e.Message + Environment.NewLine + e.TargetSite.ToString() + Environment.NewLine + e.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        #region 条码扫描程序
        /// <summary>
        /// 
        /// 开启扫描程序服务
        /// </summary>
        AsyncTCPServer tcpServer = null;
        public void StartMonitorScanner()
        {
            try
            {
                tcpServer = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), 4001, 10);
                //可以不用注册连接事件
                tcpServer.ClientConnected += tcpServer_ClientConnected;
                tcpServer.DataReceived += tcpServer_DataReceived;
                tcpServer.Start();
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }


        private void SetText(string text,bool visible)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
             // 如果它们不相同则返回true
            if (this.lb_Scan.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text,visible });
            }
            else
            {
                this.lb_Scan.Text = text;
                this.lb_Scan.Visible = visible;
            }
        }


        private void tcpServer_DataReceived(object sender, AsyncEventArgs e)
        {
            try
            {
                if (NewuGlobal.SoftConfig.RubScanner || NewuGlobal.SoftConfig.DrugScanner || NewuGlobal.SoftConfig.OilScanner || NewuGlobal.SoftConfig.ZnOScanner || NewuGlobal.SoftConfig.CarBonScanner)
                {
                    int weightNum = ss.getHex(452, 4);
                    int weightLeft = weightNum / 10 % 10;
                    int weightRight = weightNum % 10;
                    int weightNumD = ss.getHex(460, 4);
                    int weightLeftD = weightNumD / 10 % 10;
                    int weightRightD = weightNumD % 10;
                    string tempStr = System.Text.Encoding.ASCII.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                    string MaterialC = null;
                    int WeighOrder = 0;
                    int WeighOrderD = 0;
                    for (int i = 0; i < scannerList.Count; i++)
                    {
                        if (scannerList[i].ScannerIP.ToString() == e._state.TcpClient.Client.RemoteEndPoint.ToString())
                        {
                            NewuBLL.WorkType workType = getBarCodeBin(tempStr);
                            var temp = NewuGlobal.GetTypeNameByTypeCodeID(workType.TypeCodeID);
                            if (temp == "" || temp == null)
                            {
                                string ScanText = NewuGlobal.GetRes("000718") + tempStr;
                                SetText(ScanText, true);
                                break;
                            }
                            else
                            {
                                if (NewuGlobal.GetTypeNameByTypeCodeID(workType.TypeCodeID) == new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T胶料))
                                {
                                    var tempR = new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T胶料);
                                    //string strWhere = "BarCode like'" + "%" + tempStr + "%" + "'";
                                    string strWhere = "BarCode in ('" + tempStr + "')";
                                    List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);

                                    if (formularMaList.Count == 1)
                                    {   //集合中取一条数据不用foreach循环直接第0个集合。sxh
                                        MaterialC = formularMaList[0].MaterialCode;
                                    }
                                    else if (formularMaList.Count == 0)
                                    {
                                        string ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                        SetText(ScanText, true);
                                        break;
                                    }
                                    else
                                    {
                                        MaterialC = formularMaList[0].MaterialCode;
                                        // foreach (var item in formularMaList)
                                        //{
                                        //    MaterialCode = item.MaterialCode;
                                        //}
                                    }

                                    string strWhere1 = "WeighMaterialCode='" + MaterialC + "' and MaterialCode = '" + NewuGlobal.Now_OrderName + "'";
                                    List<FormulaWeighMDL> rawList = matBll.GetModelList(strWhere1);
                                    if (rawList.Count == 1)
                                    {
                                        WeighOrder = rawList[0].WeighOrder;
                                    }
                                    else if (formularMaList.Count == 0)
                                    {
                                        string ScanText = NewuGlobal.GetRes("000717") + tempStr; //配方条码错误
                                        SetText(ScanText, true);
                                        break;
                                    }
                                    else
                                    {
                                        WeighOrder = rawList[0].WeighOrder;
                                    }

                                    if (temp == tempR && WeighOrder != 0)
                                    {
                                        if (weightRight == 1 && weightLeft == 1 && WeighOrder == 1)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr; //"条码正确："
                                            SetText(ScanText, true);
                                            //扫描成功后发送信号电气开始称量且把扫描成功信号电气必须置0
                                            NewuGlobal.MemMgr.Sync(29940, "0001");
                                            //胶料逻辑  调用接口  跟新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "胶料", ScanText);

                                        }
                                        else if ((weightRight == 2 || weightLeft == 2) && WeighOrder == 2)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                            NewuGlobal.MemMgr.Sync(29940, "0001");
                                            //胶料逻辑  调用接口  更新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "胶料", ScanText);


                                        }
                                        else if ((weightRight == 3 || weightLeft == 3) && WeighOrder == 3)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                            NewuGlobal.MemMgr.Sync(29940, "0001");
                                            //胶料逻辑  调用接口  更新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "胶料", ScanText);
                                        }
                                        else if ((weightRight == 4 || weightLeft == 4) && WeighOrder == 4)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                            NewuGlobal.MemMgr.Sync(29940, "0001");
                                            //胶料逻辑  调用接口  更新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "胶料", ScanText);
                                        }
                                        else if ((weightRight == 5 || weightLeft == 5) && WeighOrder == 5)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                            NewuGlobal.MemMgr.Sync(29940, "0001");
                                            //胶料逻辑  调用接口  更新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "胶料", ScanText);
                                        }
                                        else
                                        {
                                            string ScanText = NewuGlobal.GetRes("000717") + tempStr;//"条码不匹配：" 
                                            SetText(ScanText, true);
                                            break;
                                        }
                                    }
                                    else if ((temp != tempR || WeighOrder == 0))
                                    {
                                        string ScanText = NewuGlobal.GetRes("000719") + tempStr; //"称量序号错误："
                                        SetText(ScanText, true);
                                        break;
                                    }
                                }//药品 扫描条码
                                else if (temp == new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T药品))
                                {
                                    var tempD = new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T药品);
                                    //string strWhere = "BarCode like'" + "%" + tempStr + "%" + "'";
                                    string strWhere = "BarCode in ('" + tempStr + "')";
                                    List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);

                                    if (formularMaList.Count == 1)
                                    {   //集合中取一条数据不用foreach循环直接第0个集合。sxh
                                        MaterialC = formularMaList[0].MaterialCode;
                                    }
                                    else if (formularMaList.Count == 0)
                                    {
                                        string ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                        SetText(ScanText, true);
                                        break;
                                    }
                                    else
                                    {
                                        MaterialC = formularMaList[0].MaterialCode;
                                    }

                                    string strWhere1 = "WeighMaterialCode='" + MaterialC + "' and MaterialCode = '" + NewuGlobal.Now_OrderName + "'";
                                    List<FormulaWeighMDL> rawList = matBll.GetModelList(strWhere1);
                                    if (rawList.Count == 1)
                                    {
                                        WeighOrderD = rawList[0].WeighOrder;
                                    }
                                    else if (formularMaList.Count == 0)
                                    {
                                        string ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                        SetText(ScanText, true);
                                        break;
                                    }
                                    else
                                    {
                                        WeighOrderD = rawList[0].WeighOrder;
                                    }

                                    if (temp == tempD && WeighOrderD != 0)
                                    {
                                        if (weightRightD == 1 && weightLeftD == 1 && WeighOrderD == 1)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                            //扫描成功后发送信号电气开始称量且把扫描成功信号置0
                                            NewuGlobal.MemMgr.Sync(29948, "0001");
                                            //胶料逻辑  调用接口  跟新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "药品", ScanText);

                                        }
                                        else if ((weightRightD == 2 || weightLeftD == 2) && WeighOrderD == 2)
                                        {
                                            string ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                            //扫描成功后发送信号电气开始称量且把扫描成功信号置0
                                            NewuGlobal.MemMgr.Sync(29948, "0001");
                                            //胶料逻辑  调用接口  跟新胶料界面装填
                                            NewuGlobal.RubyScan.ScanRefresh(workType, tempStr, "药品", ScanText);
                                        }
                                        else
                                        {
                                            string ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                            SetText(ScanText, true);
                                            break;
                                        }
                                    }
                                    else if ((temp != tempD || WeighOrderD == 0))
                                    {
                                        string ScanText = NewuGlobal.GetRes("000719") + tempStr; //称量序号错误：
                                        SetText(ScanText, true);
                                        break;
                                    }
                                }//粉料 开启其储斗 投料口
                                else if (temp == new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T粉料))
                                {
                                    string ScanText = "";
                                    //根据条码值在储斗参数中查询到储斗号,没有就代表扫描错误,记录错误日志,打开投料门，记录日志
                                    TB_BinSetingBLL tbBinsetting = new TB_BinSetingBLL();

                                    //模糊查询
                                    int binNo = tbBinsetting.GetBinNoByBarCode(tempStr, NewuGlobal.SoftConfig.DeviceID);
                                    if (binNo != 0)
                                    {
                                        //扫描成功后发送斗号给电气开门
                                        //打开解包斗
                                        bool flag = NewuGlobal.MemMgr.Sync(29964, binNo.ToString("0000"));
                                        if (flag)
                                        {
                                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                        }
                                    }
                                    else
                                    {
                                        ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                        SetText(ScanText, true);
                                        break;
                                    }
                                    pm_ScanRecordBLL.AddDataByWorkUp(workType, tempStr, "粉料", ScanText, binNo);
                                }//油料 开启投料口  
                                else if (temp == new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T油料))
                                {
                                    string ScanText = "";
                                    //根据条码值在储斗参数中查询到储斗号,没有就代表扫描错误,记录错误日志,打开投料门，记录日志
                                    TB_BinSetingBLL tbBinsetting = new TB_BinSetingBLL();

                                    //模糊查询
                                    int binNo = tbBinsetting.GetBinNoByBarCode(tempStr, NewuGlobal.SoftConfig.DeviceID);
                                    if (binNo != 0)
                                    {
                                        //扫描成功后发送斗号给电气开门
                                        //打开解包斗
                                        bool flag = NewuGlobal.MemMgr.Sync(29956, binNo.ToString("0000"));
                                        if (flag)
                                        {
                                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                        }
                                    }
                                    else
                                    {
                                        ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                        SetText(ScanText, true);
                                        break;
                                    }
                                    pm_ScanRecordBLL.AddDataByWorkUp(workType, tempStr, "油料", ScanText, binNo);
                                }//炭黑 开启投料口  根据 workType.BinNo
                                else if (temp == new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T白炭黑)
                                            || temp == new SYS_TypeCodeBLL().GetTypeCodeNameByEnum(SYS_TypeCodeBLL.TypeCodeEnum.T炭黑))
                                {
                                    string ScanText = "";
                                    //根据条码值在储斗参数中查询到储斗号,没有就代表扫描错误,记录错误日志,打开投料门，记录日志
                                    TB_BinSetingBLL tbBinsetting = new TB_BinSetingBLL();

                                    //模糊查询
                                    int binNo = tbBinsetting.GetBinNoByBarCode(tempStr, NewuGlobal.SoftConfig.DeviceID);
                                    if (binNo != 0)
                                    {
                                        //扫描成功后发送斗号给电气开门
                                        //打开解包斗
                                        bool flag = NewuGlobal.MemMgr.Sync(29976, binNo.ToString("0000"));
                                        if (flag)
                                        {
                                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                                            SetText(ScanText, true);
                                        }
                                    }
                                    else
                                    {
                                        ScanText = NewuGlobal.GetRes("000717") + tempStr;
                                        SetText(ScanText, true);
                                        break;
                                    }

                                    pm_ScanRecordBLL.AddDataByWorkUp(workType, tempStr, "炭黑", ScanText, binNo);
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
            
        }

        public List<NewuModel.FormulaMaterialMDL> _listFormulaMaterial = null;
        public List<NewuModel.FormulaMaterialMDL> ListFormulaMaterial
        {
            get
            {
                _listFormulaMaterial = new NewuBLL.FormulaMaterialBLL().GetModelList("");
                return _listFormulaMaterial;
            }
        }

        private List<NewuModel.TB_BinSetingMDL> _listBinSeting = null;
        public List<NewuModel.TB_BinSetingMDL> ListBinSeting
        {
            get
            {
                if (_listBinSeting == null)
                {
                    _listBinSeting = new NewuBLL.TB_BinSetingBLL().GetModelList("");
                }
                return _listBinSeting;
            }
        }

        private int findBinNo(string materialID)
        {
            int binNo = 1;
            foreach (var item in ListBinSeting)
            {
                if (item.MaterialID == materialID)
                {
                    binNo = item.BinNo;
                }
            }
            return binNo;
        }

        private NewuBLL.WorkType getBarCodeBin(string barCode)
        {
            NewuBLL.WorkType workType = new WorkType();
            foreach (var item in ListFormulaMaterial)
            {
                //if (item.BarCode.Contains(barCode))
                if (item.BarCode.Equals(barCode))
                {
                    workType.TypeCodeID = item.TypeCodeID;
                    workType.BinNo = findBinNo(item.MaterialID);
                    workType.MaterialID = item.MaterialID;
                    workType.MaterialCode = item.MaterialCode;
                    workType.DeviceID = item.DeviceID;
                    workType.DeviceCode = item.DeviceCode;
                    workType.PortBarcode = item.BarCode;
                    workType.STime = item.SaveTime;
                }
            }
            return workType;
        }
 
        public class Scanner
        {
            private EndPoint scannerIP;

            public EndPoint ScannerIP
            {
                get { return scannerIP; }
                set { scannerIP = value; }
            }
            private int? binNo;

            public int? BinNo
            {
                get { return binNo; }
                set { binNo = value; }
            }

            private string materialBarCode;

            public string MaterialBarCode
            {
                get { return materialBarCode; }
                set { materialBarCode = value; }
            }

            public Scanner(EndPoint scannerIP, int? binNo, string materialBarCode)
            {
                this.scannerIP = scannerIP;
                this.binNo = binNo;
                this.materialBarCode = materialBarCode;
            }
        }


        private void tcpServer_ClientConnected(object sender, AsyncEventArgs e)
        {
            Scanner scanner = new Scanner(e._state.TcpClient.Client.RemoteEndPoint, null, null);
            bool isExist = false;
            foreach (var item in scannerList)
            {
                if (item.ScannerIP.ToString() == e._state.TcpClient.Client.RemoteEndPoint.ToString())
                {
                    isExist = true;
                    break;
                }
            }
            if (isExist == false)
            {
                scannerList.Add(scanner);
            }
        }

        #region 停止监控扫描枪
        public void StopMonitorScanner()
        {
            tcpServer.Dispose();
        }
        #endregion


    #endregion





        public void timer1_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToString();
            //Thread thMonitor = new Thread(new ThreadStart(MonitorRefresh));
            Thread thMonitor = new Thread(MonitorRefresh);
            thMonitor.IsBackground = true;
            thMonitor.Start();

        }
        void ScrollBarDisPlay()
        {
            if (SS.getbool(84))
            {
                SS.setStr(84, "0");
                ////alarm_content.Text = NewuGlobal.AlarmInfo;
                ///之前老的滚动条
                //disPlayBarBar1.setContent(NewuGlobal.AlarmInfo);
                scrollingText1.ScrollText = string.Join(" ", NewuGlobal.AlarmInfo);
            }
            //disPlayBarBar1.run();
        }
        int DropDownflag = 0;

        void disView()
        {
            try
            {
                //YiBiaoTimeTime.NewuYiBiaoValue = SS.getInt(86, 4).ToString();  //时时间，(分步)
                txtUser.Text = NewuGlobal.SysUser.RealName;
                txtLineNo.Text = deviceCode;
                int Index = dgvMat.RowCount - 1;
                int weightNo = ss.getHex(452, 4);
                if (weightNo == 0)
                {
                    dgvMat.Rows[Index].Selected = false;
                    labScaleState.Text = NewuGlobal.GetRes("000703");
                }


                if (ss.getbool(696) && ss.getbool(697) && DropDownflag == 0) { DropDownflag = 1; ClearWeightData(dgvMat); return; }
                DropDownflag = 0;
                txtBatchSet.Text = ss.getInt(1104, 4).ToString();//1104胶料设定车数
                txtSetMiLian.Text = ss.getInt(1128, 4).ToString();//1128 油料实际车数
                txtBatchReal_R.Text = ss.getInt(1144, 4).ToString();//1144胶秤车数

                tbTemp.Text = (1.0 * ss.getInt(1176, 4) / ScaleAccuracy.digitTemp).ToString(); //温度
                tbPress.Text = (1.0 * ss.getInt(1184, 4) / ScaleAccuracy.digitPress).ToString();  //压力
                tbSpeed.Text = (1.0 * ss.getInt(1188, 4) / ScaleAccuracy.digitSpeed).ToString();  //转速
                tbEnergy.Text = (1.0 * ss.getInt(1192, 4) / ScaleAccuracy.digitEnergy).ToString();  //能量  
                double carbonVal = FunClass.GetMemHexDec(SS.getStr(1000, 4), ScaleAccuracy.getDigit(DevicePartType.Carbon));
                double oilVal = FunClass.GetMemHexDec(SS.getStr(1016, 4), ScaleAccuracy.getDigit(DevicePartType.Oil));
                double rubberVal = FunClass.GetMemHexDec(SS.getStr(1040, 4), ScaleAccuracy.getDigit(DevicePartType.Rubber));
                tbCarbon.Text = Convert.ToString(carbonVal);
                tbOil.Text = Convert.ToString(oilVal);
                tbRub.Text = Convert.ToString(rubberVal);
                if (!NewuGlobal.SoftConfig.IsFinalMix())
                {
                    txtBatchReal_P.Text = ss.getInt(1120, 4).ToString();//1120   炭黑车数

                    txtBatchReal_D.Text = ss.getInt(1148, 4).ToString();//1148  小药车数
                }
                txtMiLian.Text = ss.getInt(1156, 4).ToString();
                //1200---胶料双屏重量   16进制
                labScaleReal.Text = ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, ss.getHex(1200, 4));
                //labScaleReal.Text = ScaleAccuracy.agreeWeightShow(DevicePartType.DrugMixer, ss.getHex(1200, 4));
                //1040---胶秤重量 16进制
                //txtScale.Text = ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, ss.getHex(1040, 4));
                txtScale.Text = NewuBLL.NewuGlobal.SysUser.WorkOrder.ToString();
                //这种写法为抛出异常,当电脑重启,会报空指针异常,并未被捕获.
                //lblMixingTime.Text = Int32.Parse(ss.getStr(86, 4)).ToString();
                if (SS.getbool(838))
                {
                    lblMixingTime.Text = SS.getInt(86, 4).ToString();
                }
                else
                {
                    lblMixingTime.Text = "0";
                }
                if (ss.getStr(703, 1) != "1")//胶料秤翻屏信号
                {
                    if (ss.getStr(702, 1) == "1")//胶秤双屏正负号
                    {
                        labWeightFlag.Text = "-";
                    }
                    else
                    {
                        labWeightFlag.Text = "+";
                    }
                }

                // 解决 dgv中没有数据时，会报空指针异常的错误 2018.4.2 11:34
                if (dgvMat.Rows.Count < 1) return;
                int scaleIndex = dgvMat.CurrentRow.Index;
                if (ss.getStr(703, 1) == "1" && isSingleRawOk == false)//703胶料秤翻屏信号
                {
                    isSingleRawOk = true;
                    //labScaleState.Text = "称量完毕!等待运胶";
                    labScaleState.Text = NewuGlobal.GetRes("000703");
                    if (ss.getHex(1104, 4) <= ss.getInt(1144, 4) && scaleIndex == dgvMat.Rows.Count - 1)//1104胶料设定车数，1028胶秤车数
                    {
                        //labScaleState.Text = "本配方称量完毕!等待下一配方!";
                        labScaleState.Text = NewuGlobal.GetRes("000703");
                        dgvMat.ClearSelection();
                        isScaleEnd = true;
                    }
                    else
                    {
                        isScaleEnd = false;
                    }
                }
                else
                {
                    if (ss.getStr(703, 1) == "0" && isSingleRawOk == true)//703胶料秤翻屏信号
                    {
                        //labScaleState.Text = "称量中...";
                        isSingleRawOk = false;
                    }
                    int weightNum_R = ss.getHex(452, 4);
                    if (weightNum_R != 0)
                    {
                        //labScaleState.Text = "正在称" + (weightNum_R / 10 % 10) + " - " + (weightNum_R % 10) + "种胶...";
                        labScaleState.Text = NewuGlobal.GetRes("000702") + (weightNum_R / 10 % 10) + " - " + (weightNum_R % 10);
                    }

                }

                if (isScaleEnd == false)
                {
                    if (rawList == null) return;
                    #region 胶料表格秤逻辑
                    int weightNum = ss.getHex(452, 4);
                    int weightLeft = weightNum / 10 % 10;
                    int weightRight = weightNum % 10;
                    int cnt = 0;
                    foreach (FormulaWeighMDL item in rawList)
                    {
                        if (NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Rubber) == item.DevicePartCode)
                        {
                            if (SS.getbool(651))
                            {
                                dgvMat.Rows[cnt].Cells["Reserve3"].Value = ""; //称好以后清空扫描结果，准备下次扫描。
                                scanRecord[cnt] = 0;
                                if (item.DropOrder == weightLeft && item.WeighOrder == weightRight
                                && NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Rubber) == item.DevicePartCode)
                                {
                                    dgvMat.Rows[cnt].Selected = true;
                                    dgvMat.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                                    if (ss.getbool(654))
                                        item.Reserve5 = "-" + ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
                                    else
                                        item.Reserve5 = ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
                                    dgvMat.InvalidateRow(cnt);
                                    break;
                                }
                            }
                            else
                            {
                                if (item.DropOrder == weightLeft && item.WeighOrder == weightRight
                                && NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Rubber) == item.DevicePartCode)
                                {
                                    dgvMat.Rows[cnt].Selected = true;
                                    dgvMat.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                                    if (ss.getbool(654))
                                        item.Reserve5 = "-" + ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
                                    else
                                        item.Reserve5 = ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
                                    dgvMat.InvalidateRow(cnt);
                                    break;
                                }
                            }
                        }
                        cnt++;
                    }
                    if (weightNum != lastWeightNum)
                    {
                        lastWeightNum = weightNum;
                    }
                    #endregion
                    //胶料净重值  
                    //scaleBar2.Value = ScaleAccuracy.agreeWeightShowDouble(DevicePartType.Rubber, ss.getHex(448, 4));
                    if (NewuGlobal.SoftConfig.IsFinalMix())
                        return;
                    #region 小药表格秤逻辑
                    weightNum = ss.getHex(460, 4);
                    weightLeft = weightNum / 10 % 10;
                    weightRight = weightNum % 10;
                    cnt = 0;
                    foreach (FormulaWeighMDL item in rawList)
                    {
                        if (NewuGlobal.GetDevicePartCode(Newu.DevicePartType.DrugMixer) == item.DevicePartCode)
                        {
                            if (SS.getbool(823))
                            {
                                dgvMat.Rows[cnt].DefaultCellStyle.ForeColor = Color.Green;
                                dgvMat.Rows[cnt].Cells["Reserve3"].Value = ""; //称好以后清空扫描结果，准备下次扫描。
                                scanRecord[cnt] = 0;
                                dgvMat.InvalidateRow(cnt);
                                //break;    //去除break,当两种以上药品时两种同时变绿
                            }
                            else if (item.DropOrder == weightLeft && item.WeighOrder == weightRight)
                            {
                                item.Reserve5 = ScaleAccuracy.agreeWeightShow(DevicePartType.DrugMixer, ss.getHex(456, 4));
                                dgvMat.Rows[cnt].DefaultCellStyle.ForeColor = Color.Red;
                                dgvMat.InvalidateRow(cnt);
                                break;
                            }
                            else
                            {
                                dgvMat.Rows[cnt].DefaultCellStyle.ForeColor = Color.Black;
                                dgvMat.InvalidateRow(cnt);
                            }
                        }
                        cnt++;
                    }
                    #endregion
                    #region 塑解剂表格秤逻辑
                    weightNum = ss.getHex(444, 4);
                    weightLeft = weightNum / 10 % 10;
                    weightRight = weightNum % 10;
                    cnt = 0;
                    foreach (FormulaWeighMDL item in rawList)
                    {
                        if (NewuGlobal.GetDevicePartCode(Newu.DevicePartType.Plasticizer) == item.DevicePartCode)
                        {
                            if (SS.getbool(822))
                            {
                                dgvMat.Rows[cnt].DefaultCellStyle.ForeColor = Color.Green;
                                dgvMat.InvalidateRow(cnt);
                                break;
                            }
                            else if (item.DropOrder == weightLeft && item.WeighOrder == weightRight)
                            {
                                item.Reserve5 = ScaleAccuracy.agreeWeightShow(DevicePartType.Plasticizer, ss.getHex(440, 4));
                                dgvMat.Rows[cnt].DefaultCellStyle.ForeColor = Color.Red;
                                dgvMat.InvalidateRow(cnt);
                                break;
                            }
                            else
                            {
                                dgvMat.Rows[cnt].DefaultCellStyle.ForeColor = Color.Black;
                                dgvMat.InvalidateRow(cnt);
                            }
                        }
                        cnt++;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
            
        }
        public void ClearWeightData(DataGridView dgv)
        {
            int cnt = 0;
            if (rawList == null) return;
            foreach (FormulaWeighMDL item in rawList)
            {
                item.Reserve5 = "";
                dgv.InvalidateRow(cnt++);
            }
        }

        private void FM_JL_Load(object sender, EventArgs e)
        {
            if (NewuGlobal.RubyDataChange == null)
                NewuGlobal.RubyDataChange = this;
            if (NewuGlobal.RubyScan == null)
                NewuGlobal.RubyScan = this;
            deviceCode = NewuGlobal.SoftConfig.DeviceCode;
            //displayBar1.Width = this.Width;
            #region 控件样式
            NewuCommon.ColStruct[] cols = null;
            if (NewuGlobal.SoftConfig.IsFinalMix())
            {
                txtBatchReal_P.Visible = false;

                txtBatchReal_D.Visible = false;
                lb_d.Visible = false;
                lb_Scan.Visible = false;
                lb_p.Visible = false;
                cols = new NewuCommon.ColStruct[] {
                new NewuCommon.ColStruct("Reserve4","投料次序"),
                new NewuCommon.ColStruct("WeighOrder","称量次序",ColumnType.txt,false),
                new NewuCommon.ColStruct("WeighMaterialCode","物料名称", ColumnType.txt,true),
                new NewuCommon.ColStruct("DevicePartID","称量部件", ColumnType.txt,false),
                new NewuCommon.ColStruct("WeighSetVal","标准重量"),
                new NewuCommon.ColStruct("Reserve5","实际重量"),
                new NewuCommon.ColStruct("AllowError","公差"),
                new NewuCommon.ColStruct("Reserve3","扫描",ColumnType.txt,true)
            };
            }
            else
            {
                cols = new NewuCommon.ColStruct[] {
                new NewuCommon.ColStruct("Reserve4","投料次序"),
                new NewuCommon.ColStruct("WeighOrder","称量次序",ColumnType.txt,false),
                new NewuCommon.ColStruct("WeighMaterialCode","物料名称", ColumnType.txt,true),
                new NewuCommon.ColStruct("DevicePartID","称量部件", ColumnType.txt,false),
                new NewuCommon.ColStruct("WeighSetVal","标准重量"),
                new NewuCommon.ColStruct("Reserve5","实际重量"),
                new NewuCommon.ColStruct("AllowError","公差"),
                new NewuCommon.ColStruct("Reserve3","扫描",ColumnType.txt,true)
            };
            }
            dgvMat.VisibleOrderNumber = false;
            dgvMat.AllowUserToAddRows = false;
            dgvMat.AddCols(cols);
            dgvMat.ReadOnly = true;
            dgvMat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvMat.Enabled = false;


            dgvMat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMat.MultiSelect = false;
            dgvMat.ScrollBars = ScrollBars.None;
            dgvMat.EnableHeadersVisualStyles = false;
            dgvMat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvMat.ColumnHeadersHeight = 35;
            dgvMat.RowTemplate.MinimumHeight = 45;
            //dgvMat.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dgvMat.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvMat.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvMat.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;

            dgvMat.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Bold);
            dgvMat.RowsDefaultCellStyle.Font = new Font("宋体", 20, FontStyle.Bold);
            // dgvMat.Columns[0].FillWeight = 120;
            dgvMat.Columns[0].FillWeight = 50;
            dgvMat.Columns[2].FillWeight = 260;
            dgvMat.Columns[4].FillWeight = 95;
            dgvMat.Columns[5].FillWeight = 95;
            dgvMat.Columns[6].FillWeight = 50;
            dgvMat.Columns[7].FillWeight = 50;
            #endregion
            getQuXian();
            ViewHelper.InitMixTechDataGridView(dgvMlgy);
            ViewHelper.DisPlayTable(dgvMlgy);
            getData();
            //label5.Text = "配方时间:";

            NewuBLL.NewuGlobal.FmJL = this;
            //启动扫描程序
            StartMonitorScanner();
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            label9.Text = NewuBLL.NewuGlobal.GetRes("000674");//*炼胶配方
            label8.Text = NewuBLL.NewuGlobal.GetRes("000701");//*下个炼胶配方

            label2.Text = NewuBLL.NewuGlobal.GetRes("000692");//*炼胶设定车数
            label3.Text = NewuBLL.NewuGlobal.GetRes("000693");//*当前胶料车数

            lb_d.Text = NewuBLL.NewuGlobal.GetRes("000672");//*当前小药车数
            lb_p.Text = NewuBLL.NewuGlobal.GetRes("000695");//*当前炭黑车数
            label4.Text = NewuBLL.NewuGlobal.GetRes("000696");//*当前密炼车数
            label6.Text = NewuBLL.NewuGlobal.GetRes("000694");//*当前油料车数
            label5.Text = NewuBLL.NewuGlobal.GetRes("000697");//*配方时间

            labScaleState.Text = NewuBLL.NewuGlobal.GetRes("000703");
            //bt_C.Text = NewuBLL.NewuGlobal.GetRes("000698");//*炭黑
            //bt_O.Text = NewuBLL.NewuGlobal.GetRes("000699");//*油料
            //bt_Door.Text = NewuBLL.NewuGlobal.GetRes("000700");//*卸料
            // disPlayBarBar1.SetTitle(NewuBLL.NewuGlobal.GetRes("000701"));//*设备正常

            LanguageDGV(dgvMat, 675);
            LanguageDGV(dgvMlgy, 683);

        }
        private void LanguageDGV(NewuCommon.DataGridViewEx dgv, int start)
        {
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible == true)
                    {
                        dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                    }
                }
        }
        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }


        #region 曲线图表
        ///// <summary>
        ///// 曲线方法
        ///// </summary>
        //private void getQuXian()
        //{
        //    System.Windows.Forms.Timer timerRealCurve = new System.Windows.Forms.Timer();
        //    /**** 曲线 ****/
        //    timerRealCurve.Interval = 1000;
        //    timerRealCurve.Enabled = true;
        //    timerRealCurve.Tick += new EventHandler(timerRealCurve_Tick);
        //    for (int i = 1; i <= 82; i++)
        //    {
        //        chart1.Series["bg"].Points.AddXY(i, 200);
        //    }
        //    foreach (var areas in chart1.ChartAreas)
        //    {
        //        areas.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
        //        areas.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
        //    }
        //}

        //int cnt = 0;
        //bool mixIsRun = false;
        ///// <summary>
        ///// 曲线时间事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void timerRealCurve_Tick(object sender, EventArgs e)
        //{

        //    if (SS.getbool(838) && mixIsRun == false)
        //    {
        //        mixIsRun = true;

        //        foreach (var series in chart1.Series)
        //        {
        //            if (series.Name != "bg")
        //                series.Points.Clear();
        //        }
        //        foreach (var areas in chart1.ChartAreas)
        //        {
        //            areas.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
        //            areas.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
        //        }
        //    }
        //    if (mixIsRun)
        //    {
        //        addPoint();
        //    }
        //    if (SS.getbool(678) && mixIsRun)
        //    {
        //        mixIsRun = false;
        //        cnt = 0;
        //    }
        //}

        //private void addPoint()
        //{

        //    chart1.Series["温度"].Points.AddXY(++cnt, (1.0 * SS.getInt(1176, 4)) / ScaleAccuracy.digitTemp);
        //    chart1.Series["功率"].Points.AddXY(cnt, SS.getInt(1180, 4) / 4);
        //    chart1.Series["压力"].Points.AddXY(cnt, SS.getInt(1184, 4) / ScaleAccuracy.digitPress * 100);
        //    chart1.Series["转速"].Points.AddXY(cnt, SS.getInt(1188, 4));
        //    chart1.Series["能量"].Points.AddXY(cnt, (1.0 * SS.getInt(1192, 4) * 10) / ScaleAccuracy.digitEnergy);
        //    chart1.Series["电压"].Points.AddXY(cnt, SS.getInt(1208, 4));
        //    chart1.Series["栓位"].Points.AddXY(cnt, SS.getHex(1072, 4) / 10);
        //} 
        #endregion

        #region 修改的曲线图表 2019-10-30
        /// <summary>
        /// 曲线方法
        /// </summary>
        private void getQuXian()
        {
            System.Windows.Forms.Timer timerRealCurve = new System.Windows.Forms.Timer();
            /**** 曲线 ****/
            timerRealCurve.Interval = 1000;
            timerRealCurve.Enabled = true;
            timerRealCurve.Tick += new EventHandler(timerRealCurve_Tick);
            axTChart1.Axis.Left.SetMinMax(0, 500);//左纵坐标
            axTChart1.Axis.Right.SetMinMax(0, 4000);//右纵坐标
            axTChart1.Axis.Left.GridPen.Visible = false;
            axTChart1.Axis.Bottom.GridPen.Visible = false;
            axTChart1.Legend.Show();
        }

        int cnt = 0;
        bool mixIsRun = false;
        /// <summary>
        /// 曲线时间事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRealCurve_Tick(object sender, EventArgs e)
        {
            try
            {
                if (SS.getbool(838) && mixIsRun == false)
                {
                    mixIsRun = true;

                }
                if (mixIsRun)
                {
                    cnt++;
                    addPoint();
                }
                if (SS.getbool(678) && mixIsRun)
                {
                    mixIsRun = false;
                    //axTChart1.Series(0).Clear();
                    //axTChart1.Series(1).Clear();
                    //axTChart1.Series(2).Clear();
                    //axTChart1.Series(3).Clear();
                    //axTChart1.Series(4).Clear();
                    //axTChart1.Series(5).Clear();
                    //axTChart1.Series(6).Clear();
                }
                if (!SS.getbool(838))
                {
                    cnt = 0;
                    axTChart1.Series(0).Clear();
                    axTChart1.Series(1).Clear();
                    axTChart1.Series(2).Clear();
                    axTChart1.Series(3).Clear();
                    axTChart1.Series(4).Clear();
                    axTChart1.Series(5).Clear();
                    axTChart1.Series(6).Clear();
                }
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile("副屏曲线:" + ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }


        /// <summary>
        /// liu.zhicheng 双坐标曲线
        /// </summary>
        private void addPoint()
        {
            //温度
            //axTChart1.Series(0).AddXY(cnt, (1.0 * SS.getInt(1176, 4)) / ScaleAccuracy.digitTemp, "", 0);
            ////功率
            //axTChart1.Series(1).AddXY(cnt, SS.getInt(1180, 4), "", 0);
            //压力
            axTChart1.Series(2).AddXY(cnt, SS.getInt(1184, 4) / ScaleAccuracy.digitPress * 100, "", 0);
            //转速
            axTChart1.Series(3).AddXY(cnt,(1.0* SS.getInt(1188, 4)) / ScaleAccuracy.digitTemp, "", 0);
            //能量
            axTChart1.Series(4).AddXY(cnt, (1.0 * SS.getInt(1192, 4)), "", 0);
            //电压
            axTChart1.Series(5).AddXY(cnt, SS.getInt(1208, 4), "", 0);
            //栓位
            axTChart1.Series(6).AddXY(cnt, SS.getHex(1072, 4) / 10, "", 0);

            axTChart1.Series(0).AddXY(cnt, random.Next(1, 200), "", 50);
            axTChart1.Series(1).AddXY(cnt, random.Next(500, 2000), "", 0);

        }
        #endregion



        void getData()
        {
            string RubberpartCode = NewuGlobal.GetDevicePartCode(DevicePartType.Rubber);//部件编码
            string RubberpartID = NewuGlobal.GetDevicePartIDByPartCode(RubberpartCode);//部件ID

            string strWhere = "DevicePartID='" + RubberpartID + "' and DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "'";
            List<PM_DevicePartOrderTranMDL> orderlist = orderDeviceBll.GetModelList(strWhere);
            if (orderlist.Count != 1)
            {
                labScaleState.Text = NewuBLL.NewuGlobal.GetRes("000705") + orderlist.Count;
                return;
            }


            strWhere = "MaterialID='" + orderlist[0].MaterialID + "' and " +
                    "DevicePartID='" + RubberpartID + "'";
            rawList = rawBll.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
            foreach (FormulaWeighMDL item in rawList)
            {
                item.Reserve4 ="胶"+ item.DropOrder + "-" + item.WeighOrder;
                item.WeighSetVal = ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, item.WeighSetVal);
                item.AllowError = ScaleAccuracy.agreeWeightShow(DevicePartType.Rubber, item.AllowError);
            }
            /**
             * 
             * 添加其塑解剂秤 和 小药秤的数据查询 添加在rawList中
             */
            if (!NewuGlobal.SoftConfig.IsFinalMix())
            {
                string DurgPartCode = NewuGlobal.GetDevicePartCode(DevicePartType.DrugMixer);//部件编码
                string DurgPartID = NewuGlobal.GetDevicePartIDByPartCode(DurgPartCode);//部件ID
                strWhere = "MaterialID='" + orderlist[0].MaterialID + "' and " +
                         "DevicePartID='" + DurgPartID + "'";
                List<FormulaWeighMDL> temp = null;
                temp = rawBll.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
                foreach (FormulaWeighMDL item in temp)
                {
                    item.Reserve4 ="辅"+ item.DropOrder + "-" + item.WeighOrder;
                    item.WeighSetVal = ScaleAccuracy.agreeWeightShow(DevicePartType.DrugMixer, item.WeighSetVal);
                    item.AllowError = ScaleAccuracy.agreeWeightShow(DevicePartType.DrugMixer, item.AllowError);
                }
                if (temp != null)
                    rawList.AddRange(temp);
            }

            if (!NewuGlobal.SoftConfig.IsFinalMix())
            {
                string PlaPartCode = NewuGlobal.GetDevicePartCode(DevicePartType.Plasticizer);//部件编码
                string PlaPartID = NewuGlobal.GetDevicePartIDByPartCode(PlaPartCode);//部件ID
                strWhere = "MaterialID='" + orderlist[0].MaterialID + "' and " +
                         "DevicePartID='" + PlaPartID + "'";
                List<FormulaWeighMDL> temp = null;
                temp = rawBll.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
                foreach (FormulaWeighMDL item in temp)
                {
                    item.Reserve4 = item.DropOrder + "-" + item.WeighOrder;
                    item.WeighSetVal = ScaleAccuracy.agreeWeightShow(DevicePartType.Plasticizer, item.WeighSetVal);
                    item.AllowError = ScaleAccuracy.agreeWeightShow(DevicePartType.Plasticizer, item.AllowError);
                }
                if (temp != null)
                    rawList.AddRange(temp);
            }

            dgvMat.DataSource = rawList;
            this.dgvMat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;

            //tv_NowName.Text = NewuGlobal.Now_Weight_OrderName;
            //tv_NowMixName.Text = NewuGlobal.Now_OrderName;

            labNowName.Text = NewuGlobal.Now_OrderName;
            labNextName.Text = NewuGlobal.Next_OrderName;
        }

        /// <summary>
        /// 发送配方完毕后，会以接口的形式  通知该界面更新
        /// </summary>
        public void RefreshData()
        {
            ViewHelper.DisPlayTable(dgvMlgy);
            getData();
            //labNowName.Text = NewuGlobal.Now_OrderName;
            //labNextName.Text = NewuGlobal.Next_OrderName;
        }

        public void RefreshData(bool isWeight)
        {
            RefreshData();
            //// 称量  is 
            //if (isWeight)
            //{
            //    getData();
            //    tv_NowName.Text = NewuGlobal.Now_Weight_OrderName;
            //}
            //else
            //{
            //    ViewHelper.DisPlayTable(dgvMlgy);
            //    tv_NowMixName.Text = NewuGlobal.Now_OrderName;
            //}
            //labNowName.Text = NewuGlobal.Now_OrderName;
            //labNextName.Text = NewuGlobal.Next_OrderName;
        }

        
        private void cheackSendOK()
        {
            int cnt = 0;
            foreach (var item in rawList)
            {
                if (item.Reserve4[3].ToString() == flagTimers.ToString() && scanRecord[cnt] == 0)
                {
                    return;
                }
                cnt++;
            }
            // todo: 发送扫描完毕  记录   ==  胶料的云库那个信号
            return;
        }


        /// <summary>
        /// 更新物料扫描记录
        /// </summary>
        int flagTimers = 0;
        private void Timer2_Tick(object sender, EventArgs e)
        {
            //用于判断屏幕是否假死
            lblTime.Text = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
        }

        private void lb_energy_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="work"></param>
        /// <param name="barCode"></param>
        /// <param name="type"></param>
        public void ScanRefresh(WorkType work, string barCode, string type,string scaninfo)
        {
            int cnt = 0;
            foreach (var item in rawList)
            {
                if (work.MaterialID == item.WeighMaterialID && scanRecord[cnt] == 0)
                {
                    scanRecord[cnt] = 1;
                    item.Reserve3 = "ok";
                    dgvMat.InvalidateRow(cnt);
                    //cheackSendOK();
                    if (type == "胶料")
                    {
                        work.che = SS.getInt(1144, 4);
                    }
                    else if (type == "药品")
                    {
                        work.che = SS.getInt(1148, 4);
                    }
                    new NewuBLL.PM_ScanRecordBLL().AddDataByWorkUp(work, barCode, type, scaninfo,0);
                    flagTimers = Int32.Parse(item.Reserve4[3].ToString());
                    break;
                }
                cnt++;
            }
        }
    }
}
