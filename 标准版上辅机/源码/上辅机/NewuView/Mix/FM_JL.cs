using MultiLanguage;
using Newu;
using Newu.Net.TCP;
using NewuBLL;
using NewuBLL.MixBll;
using NewuCommon;
using NewuModel;
using Repository.GlobalConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class FM_JL : Form, IRefresh, ScanRefresh, ILanguageChanged
    {
        private delegate void SetTextCallback(string text, bool visible, Color color);

        public Random random = new Random();
        private string deviceCode = "";
        private bool isSingleRawOk = false, isScaleEnd = false;
        private int lastWeightNum = 0;
        private int selectIndex = 0;
        private string tempStr = "";
        private string mName = "";
        private CSharedString SS = NewuGlobal.MemDB;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private MemoryNotify[] changeSignal;  //扫描正确信号
        private TB_BinSetingBLL tbSeting = new TB_BinSetingBLL();
        private CSharedString ss = NewuGlobal.MemDB;
        private PM_ScanRecordBLL scanRecordBll = new PM_ScanRecordBLL();
        private List<Scanner> scannerList = new List<Scanner>();
        private PM_ScanRecordBLL pm_ScanRecordBLL = new PM_ScanRecordBLL();
        private FormulaWeighBLL matBll = new FormulaWeighBLL();
        private FormulaMaterialBLL fmbBll = new FormulaMaterialBLL();
        private PM_DevicePartOrderTranBLL orderDeviceBll = new PM_DevicePartOrderTranBLL();
        private FormulaWeighBLL rawBll = new FormulaWeighBLL();
        private List<FormulaWeighMDL> rawList = null;
        private NewuBLL.RPT_BarcodeRecordBLL BarcodeRecord;

        private RPT_BarcodeRecordBLL BarRecord = new RPT_BarcodeRecordBLL(new PM_OrderTranMDL()
        {
            Savetime = DateTime.Now,
        });

        //tododown: 翻屏信号  应编写初始化逻辑
        private int[] scanRecord = new int[100];

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

        #endregion 按F7切换屏幕

        public FM_JL()
        {
            InitializeComponent();

            timer1.Interval = 500;
            timer1.Tick += Timer1_Tick;
            timer1.Enabled = true;
        }

        public void Timer1_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToString();
            Thread thMonitor = new Thread(MonitorRefresh)
            {
                IsBackground = true
            };
            thMonitor.Start();
        }

        private void MonitorRefresh()
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
                DisView();
                ViewHelper.RefreshMixTechDataGridView(dgvMlgy);
                labNowName.Text = NewuGlobal.Now_OrderName;
                labNextName.Text = NewuGlobal.Next_OrderName;
            }
            catch (Exception e)
            {
                FunClass.WriteLogFile("副屏:" + e.Message + Environment.NewLine + e.TargetSite.ToString() + Environment.NewLine + e.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        private void ScrollBarDisPlay()
        {
            scrollingText1.ScrollText = string.Join(" ", NewuGlobal.AlarmInfo);
        }

        private void DisView()
        {
            try
            {
                txtUser.Text = NewuGlobal.SysUser.RealName;
                txtLineNo.Text = deviceCode;
                int Index = dgvMat.RowCount - 1;
                int weightNo = ss.getHex(452, 4);
                if (weightNo == 0)
                {
                    dgvMat.Rows[Index].Selected = false;
                    labScaleState.Text = NewuGlobal.GetRes("000703");
                }

                //胶料称自动
                if (NewuGlobal.SoftConfig.ManualWeight)
                {
                    if (SS.getbool(648))
                    {
                        labelRubberWeight.Visible = false;
                    }
                    else //胶料称手动
                    {
                        labelRubberWeight.Visible = true;

                        if (SS.getbool(650))
                        {
                            labelRubberWeight.Text = "- " + ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(1040, 4));
                        }
                        else
                        {
                            labelRubberWeight.Text = "+ " + ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(1040, 4));
                        }
                    }
                }

                if (ss.getbool(696) && ss.getbool(697) && DropDownflag == 0) { DropDownflag = 1; ClearWeightData(dgvMat); return; }
                DropDownflag = 0;
                tbTemp.Text = (1.0 * ss.getInt(1176, 4) / ScaleAccuracy.digitTemp).ToString(); //温度
                tbPress.Text = (1.0 * ss.getInt(1184, 4) / ScaleAccuracy.digitPress).ToString();  //压力
                tbSpeed.Text = (1.0 * ss.getInt(1188, 4) / ScaleAccuracy.digitSpeed).ToString();  //转速
                tbEnergy.Text = (1.0 * ss.getInt(1192, 4) / ScaleAccuracy.digitEnergy).ToString();  //能量
                double carbonVal = FunClass.GetMemHexDec(SS.getStr(1000, 4), ScaleAccuracy.GetDigit(DevicePartType.Carbon));
                double carbonVal2 = FunClass.GetMemHexDec(SS.getStr(1004, 4), ScaleAccuracy.GetDigit(DevicePartType.Carbon));
                double oilVal = FunClass.GetMemHexDec(SS.getStr(1016, 4), ScaleAccuracy.GetDigit(DevicePartType.Oil));
                double rubberVal = FunClass.GetMemHexDec(SS.getStr(1040, 4), ScaleAccuracy.GetDigit(DevicePartType.Rubber));
                double drugVal = FunClass.GetMemHexDec(SS.getStr(1044, 4), ScaleAccuracy.GetDigit(DevicePartType.DrugMixer));
                double siVal = FunClass.GetMemHexDec(SS.getStr(1048, 4), ScaleAccuracy.GetDigit(DevicePartType.Silane));
                double znoVal = FunClass.GetMemHexDec(SS.getStr(1032, 4), ScaleAccuracy.GetDigit(DevicePartType.Zon));
                tbRub.Text = Convert.ToString(rubberVal);
                tb_Drug.Text = Convert.ToString(drugVal);
                if (!NewuGlobal.SoftConfig.IsFinalMix())
                {
                    txtBatchReal_C.Text = ss.getInt(1120, 4).ToString();//1120   炭黑车数
                    txtBatchReal_O.Text = ss.getInt(1128, 4).ToString();//1128   油料车数
                    txtBatchReal_Si.Text = ss.getInt(1152, 4).ToString();//1152   硅烷车数
                    txtBatchReal_Zno.Text = ss.getInt(1136, 4).ToString();//1136   Zno车数

                    tbCarbon.Text = Convert.ToString(carbonVal);
                    tbCarbon2.Text = Convert.ToString(carbonVal2);
                    tbOil.Text = Convert.ToString(oilVal);
                    tbSi.Text = Convert.ToString(siVal);
                    tbZno.Text = Convert.ToString(znoVal);
                }
                txtBatchSet.Text = ss.getInt(1104, 4).ToString();//1104炼胶设定车数
                txtBatchReal_R.Text = ss.getInt(1144, 4).ToString();//1144胶秤车数
                txtBatchReal_D.Text = ss.getInt(1148, 4).ToString();//1148  小药车数
                txtMiLian.Text = ss.getInt(1156, 4).ToString();
                //1200---胶料双屏重量   16进制
                labScaleReal.Text = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(1200, 4));
                txtScale.Text = NewuGlobal.SysUser.WorkOrder.ToString();
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
                                    selectIndex = cnt;
                                    dgvMat.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                                    if (ss.getbool(654))
                                        item.Reserve5 = "-" + ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
                                    else
                                        item.Reserve5 = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
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
                                    selectIndex = cnt;
                                    dgvMat.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                                    if (ss.getbool(654))
                                        item.Reserve5 = "-" + ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
                                    else
                                        item.Reserve5 = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.getHex(448, 4));
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

                    #endregion 胶料表格秤逻辑

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
                                item.Reserve5 = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, ss.getHex(456, 4));
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

                    #endregion 小药表格秤逻辑

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
                                item.Reserve5 = ScaleAccuracy.AgreeWeightShow(DevicePartType.Plasticizer, ss.getHex(440, 4));
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

                    #endregion 塑解剂表格秤逻辑
                }
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\Exception" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        #region 条码扫描程序

        /// <summary>
        ///
        /// 开启扫描程序服务
        /// </summary>
        private AsyncTCPServer tcpServer = null;

        private AsyncTCPServer tcpServerC = null;
        private AsyncTCPServer tcpServerO = null;

        public void StartMonitorScanner()
        {
            try
            {
                tcpServer = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), 4001, 10);
                //可以不用注册连接事件
                tcpServer.ClientConnected += tcpServer_ClientConnected;
                tcpServer.DataReceived += TcpServer_DataReceived;
                tcpServer.Start();
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + "胶料条码扫描程序出错" + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\ScanRubber" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        public void StartMonitorScannerCarbon()
        {
            try
            {
                tcpServerC = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), 4002, 10);
                //可以不用注册连接事件
                tcpServerC.ClientConnected += tcpServer_ClientConnectedC;
                tcpServerC.DataReceived += TcpServer_DataReceivedC;
                tcpServerC.Start();
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + "炭黑条码扫描程序出错" + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\ScanCarbon" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        public void StartMonitorScannerOil()
        {
            try
            {
                tcpServerO = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), 4003, 10);
                //可以不用注册连接事件
                tcpServerO.ClientConnected += tcpServer_ClientConnectedO;
                tcpServerO.DataReceived += TcpServer_DataReceivedO;
                tcpServerO.Start();
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + "油料条码扫描程序出错" + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\ScanOil" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        private void TcpServer_DataReceived(object sender, AsyncEventArgs e)
        {
            try
            {
                if (NewuGlobal.SoftConfig.RubScanner)
                {
                    int weightNum = ss.getHex(452, 4);
                    int weightLeft = weightNum / 10 % 10;
                    int weightRight = weightNum % 10;
                    string matCode = "";
                    string strWhere = " 1=1 ";
                    string ScanText = "";
                    tempStr = System.Text.Encoding.ASCII.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                    BarcodeRecord = new RPT_BarcodeRecordBLL(NewuGlobal.RunInfo.OrderRawModel);

                    if (weightLeft == 1 && weightRight == 1)
                    {
                        matCode = dgvMat.Rows[selectIndex].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名

                        //matCode = dgvMat.Rows[0].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        mName = matCode;
                        strWhere = strWhere + "and BarCode in ('" + tempStr + "') and MaterialCode ='" + matCode + "'";
                        List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);
                        if (formularMaList.Count > 0)
                        {
                            ScanText = NewuGlobal.GetRes("000716") + tempStr; //"条码正确："
                            SetText(ScanText, true, Color.Green);
                            //扫描成功后，发送扫描正确信号给电气，开始称量后电气置0
                            NewuGlobal.MemMgr.Sync(29944, "0001");
                            saveBarcode();
                            PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                            scannerRecord.PortBarcode = "0";
                            scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                            scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                            scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                            scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                            scannerRecord.SaveTime = DateTime.Now;
                            scannerRecord.TypeCodeName = "胶料";
                            scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                            scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                            scannerRecord.MatBarcode = tempStr;
                            scannerRecord.Reserve1 = "条码正确！";
                            scanRecordBll.Add(scannerRecord);
                        }
                        else
                        {
                            ScanText = NewuGlobal.GetRes("000717") + tempStr; //"条码错误："
                            SetText(ScanText, true, Color.Red);
                            NewuGlobal.MemMgr.Sync(29944, "0000");
                        }
                    }

                    if (weightLeft == 2 || weightRight == 2)
                    {
                        matCode = dgvMat.Rows[selectIndex].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        //matCode = dgvMat.Rows[1].Cells["WeighMaterialCode"].Value.ToString();
                        mName = matCode;
                        strWhere = strWhere + "and BarCode in ('" + tempStr + "') and MaterialCode ='" + matCode + "'";
                        List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);
                        if (formularMaList.Count > 0)
                        {
                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                            SetText(ScanText, true, Color.Green);
                            NewuGlobal.MemMgr.Sync(29944, "0001");
                            saveBarcode();
                            PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                            scannerRecord.PortBarcode = "0";
                            scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                            scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                            scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                            scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                            scannerRecord.SaveTime = DateTime.Now;
                            scannerRecord.TypeCodeName = "胶料";
                            scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                            scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                            scannerRecord.MatBarcode = tempStr;
                            scannerRecord.Reserve1 = "条码正确！";
                            scanRecordBll.Add(scannerRecord);
                        }
                        else
                        {
                            ScanText = NewuGlobal.GetRes("000717") + tempStr;
                            SetText(ScanText, true, Color.Red);
                            NewuGlobal.MemMgr.Sync(29944, "0000");
                        }
                    }
                    if (weightLeft == 3 || weightRight == 3)
                    {
                        matCode = dgvMat.Rows[selectIndex].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        //matCode = dgvMat.Rows[2].Cells["WeighMaterialCode"].Value.ToString();
                        mName = matCode;
                        strWhere = strWhere + "and BarCode in ('" + tempStr + "') and MaterialCode ='" + matCode + "'";
                        List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);
                        if (formularMaList.Count > 0)
                        {
                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                            SetText(ScanText, true, Color.Green);
                            NewuGlobal.MemMgr.Sync(29944, "0001");
                            saveBarcode();
                            PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                            scannerRecord.PortBarcode = "0";
                            scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                            scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                            scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                            scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                            scannerRecord.SaveTime = DateTime.Now;
                            scannerRecord.TypeCodeName = "胶料";
                            scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                            scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                            scannerRecord.MatBarcode = tempStr;
                            scannerRecord.Reserve1 = "条码正确！";
                            scanRecordBll.Add(scannerRecord);
                        }
                        else
                        {
                            ScanText = NewuGlobal.GetRes("000717") + tempStr;
                            SetText(ScanText, true, Color.Red);
                            NewuGlobal.MemMgr.Sync(29944, "0000");
                        }
                    }
                    if (weightLeft == 4 || weightRight == 4)
                    {
                        matCode = dgvMat.Rows[selectIndex].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        //matCode = dgvMat.Rows[3].Cells["WeighMaterialCode"].Value.ToString();
                        mName = matCode;
                        strWhere = strWhere + "and BarCode in ('" + tempStr + "') and MaterialCode ='" + matCode + "'";
                        List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);
                        if (formularMaList.Count > 0)
                        {
                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                            SetText(ScanText, true, Color.Green);
                            NewuGlobal.MemMgr.Sync(29944, "0001");
                            saveBarcode();
                            PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                            scannerRecord.PortBarcode = "0";
                            scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                            scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                            scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                            scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                            scannerRecord.SaveTime = DateTime.Now;
                            scannerRecord.TypeCodeName = "胶料";
                            scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                            scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                            scannerRecord.MatBarcode = tempStr;
                            scannerRecord.Reserve1 = "条码正确！";
                            scanRecordBll.Add(scannerRecord);
                        }
                        else
                        {
                            ScanText = NewuGlobal.GetRes("000717") + tempStr;
                            SetText(ScanText, true, Color.Red);
                            NewuGlobal.MemMgr.Sync(29944, "0000");
                        }
                    }
                    if (weightLeft == 5 || weightRight == 5)
                    {
                        matCode = dgvMat.Rows[selectIndex].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        //matCode = dgvMat.Rows[4].Cells["WeighMaterialCode"].Value.ToString();
                        mName = matCode;
                        strWhere = strWhere + "and BarCode in ('" + tempStr + "') and MaterialCode ='" + matCode + "'";
                        List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);
                        if (formularMaList.Count > 0)
                        {
                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                            SetText(ScanText, true, Color.Green);
                            NewuGlobal.MemMgr.Sync(29944, "0001");
                            saveBarcode();
                            PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                            scannerRecord.PortBarcode = "0";
                            scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                            scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                            scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                            scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                            scannerRecord.SaveTime = DateTime.Now;
                            scannerRecord.TypeCodeName = "胶料";
                            scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                            scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                            scannerRecord.MatBarcode = tempStr;
                            scannerRecord.Reserve1 = "条码正确！";
                            scanRecordBll.Add(scannerRecord);
                        }
                        else
                        {
                            ScanText = NewuGlobal.GetRes("000717") + tempStr; //"条码错误："
                            SetText(ScanText, true, Color.Red);
                            //扫描成功后，发送扫描正确信号给电气，开始称量后电气置0
                            NewuGlobal.MemMgr.Sync(29944, "0000");
                        }
                    }
                    if (weightLeft == 6 || weightRight == 6)
                    {
                        matCode = dgvMat.Rows[selectIndex].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        //matCode = dgvMat.Rows[5].Cells["WeighMaterialCode"].Value.ToString();//显示的物料名
                        mName = matCode;
                        strWhere = strWhere + "and BarCode in ('" + tempStr + "') and MaterialCode ='" + matCode + "'";
                        List<FormulaMaterialMDL> formularMaList = fmbBll.GetModelList(strWhere);
                        if (formularMaList.Count > 0)
                        {
                            ScanText = NewuGlobal.GetRes("000716") + tempStr;
                            SetText(ScanText, true, Color.Green);
                            NewuGlobal.MemMgr.Sync(29944, "0001");
                            saveBarcode();

                            PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                            scannerRecord.PortBarcode = "0";
                            scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                            scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                            scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                            scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                            scannerRecord.SaveTime = DateTime.Now;
                            scannerRecord.TypeCodeName = "胶料";
                            scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                            scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                            scannerRecord.MatBarcode = tempStr;
                            scannerRecord.Reserve1 = "条码正确！";
                            scanRecordBll.Add(scannerRecord);
                        }
                        else
                        {
                            ScanText = NewuGlobal.GetRes("000717") + tempStr; //"条码错误："
                            SetText(ScanText, true, Color.Red);
                            //扫描成功后，发送扫描正确信号给电气，开始称量后电气置0
                            NewuGlobal.MemMgr.Sync(29944, "0000");
                        }
                    }
                }
                else
                {   //不使用胶料扫描直接给电气发扫描正确的信号
                    NewuGlobal.MemMgr.Sync(29940, "0000");
                    NewuGlobal.MemMgr.Sync(29944, "0001");
                }
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\ScanRubber" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        private void TcpServer_DataReceivedC(object sender, AsyncEventArgs e)
        {
            try
            {
                string tempStr = System.Text.Encoding.ASCII.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                BarcodeRecord = new NewuBLL.RPT_BarcodeRecordBLL(NewuGlobal.RunInfo.OrderRawModel);
                if (NewuGlobal.SoftConfig.CarBonScanner)
                {
                    if (getBinNo(tempStr) != 0)
                    {
                        int binNo = getBinNo(tempStr);
                        NewuGlobal.MemMgr.Sync(29976, binNo.ToString("0000"));
                        bool isHaveBarcode = BarRecord.IsExists(NewuGlobal.Now_OrderID, NewuGlobal.GetDevicePartCode(DevicePartType.Carbon), "炭黑");
                        //条码记录表中有条码就更新条码和保存时间
                        if (isHaveBarcode)
                        {
                            BarRecord.UpdateBarcode(tempStr, DateTime.Now, NewuGlobal.Now_OrderID, "炭黑");
                        }
                        //没有条码记录就插入条码全部信息
                        else
                        {
                            RPT_BarcodeRecordMDL barcode = new RPT_BarcodeRecordMDL()
                            {
                                OrderID = NewuGlobal.Now_OrderID,
                                DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                                MaterialCode = "炭黑",
                                TypeCodeName = NewuGlobal.GetDevicePartCode(DevicePartType.Carbon),
                                WeighMaterialCode = NewuGlobal.Now_OrderName,
                                SaveTime = DateTime.Now,
                                Reserve1 = tempStr
                            };
                            BarcodeRecord.Add(barcode);
                        }

                        PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                        scannerRecord.PortBarcode = "0";
                        scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                        scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                        scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                        scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                        scannerRecord.SaveTime = DateTime.Now;
                        scannerRecord.TypeCodeName = "炭黑";
                        scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                        scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                        scannerRecord.MatBarcode = tempStr;
                        scannerRecord.Reserve1 = "条码正确！";
                        scanRecordBll.Add(scannerRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\ScanCarbon" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        private void TcpServer_DataReceivedO(object sender, AsyncEventArgs e)
        {
            try
            {
                string tempStr = System.Text.Encoding.ASCII.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                BarcodeRecord = new NewuBLL.RPT_BarcodeRecordBLL(NewuGlobal.RunInfo.OrderRawModel);
                if (NewuGlobal.SoftConfig.OilScanner)
                {
                    if (getBinNo(tempStr) != 0)
                    {
                        int binNo = getBinNo(tempStr);
                        NewuGlobal.MemMgr.Sync(29960, binNo.ToString("0000"));
                        bool isHaveBarcode = BarRecord.IsExists(NewuGlobal.Now_OrderID, NewuGlobal.GetDevicePartCode(DevicePartType.Oil), "油料");
                        //条码记录表中有条码就更新条码和保存时间
                        if (isHaveBarcode)
                        {
                            BarRecord.UpdateBarcode(tempStr, DateTime.Now, NewuGlobal.Now_OrderID, "油料");
                        }
                        //没有条码记录就插入条码全部信息
                        else
                        {
                            RPT_BarcodeRecordMDL barcode = new RPT_BarcodeRecordMDL()
                            {
                                OrderID = NewuGlobal.Now_OrderID,
                                DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                                MaterialCode = "油料",
                                TypeCodeName = NewuGlobal.GetDevicePartCode(DevicePartType.Oil),
                                WeighMaterialCode = NewuGlobal.Now_OrderName,
                                SaveTime = DateTime.Now,
                                Reserve1 = tempStr
                            };
                            BarcodeRecord.Add(barcode);
                        }

                        PM_ScanRecordMDL scannerRecord = new PM_ScanRecordMDL();
                        scannerRecord.PortBarcode = "0";
                        scannerRecord.DeviceID = NewuGlobal.SoftConfig.DeviceID;
                        scannerRecord.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
                        scannerRecord.OrderID = NewuGlobal.Now_OrderID;
                        scannerRecord.TypeCodeName = "油料";
                        scannerRecord.MaterialCode = NewuGlobal.Now_OrderName;
                        scannerRecord.SaveTime = DateTime.Now;
                        scannerRecord.SaveUserCode = NewuGlobal.SysUser.UserCode;
                        scannerRecord.SaveUserID = NewuGlobal.SysUser.UserID;
                        scannerRecord.MatBarcode = tempStr;
                        scannerRecord.Reserve1 = "条码正确！";
                        scanRecordBll.Add(scannerRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                FunClass.WriteLogFile(ex.Message + Environment.NewLine + ex.TargetSite.ToString() + Environment.NewLine + ex.StackTrace, "Exception\\ScanOil" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            }
        }

        private void saveBarcode()
        {
            //判断条码记录表中是否有此条码
            bool isHaveBarcode = BarRecord.IsExists(NewuGlobal.Now_OrderID, NewuGlobal.GetDevicePartCode(DevicePartType.Rubber), mName);
            //条码记录表中有条码就更新条码和保存时间
            if (isHaveBarcode)
            {
                BarRecord.UpdateBarcode(tempStr, DateTime.Now, NewuGlobal.Now_OrderID, mName);
            }
            //没有条码记录就插入条码全部信息
            else
            {
                RPT_BarcodeRecordMDL barcode = new RPT_BarcodeRecordMDL()
                {
                    OrderID = NewuGlobal.Now_OrderID,
                    DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                    MaterialCode = mName,
                    TypeCodeName = NewuGlobal.GetDevicePartCode(DevicePartType.Rubber),
                    WeighMaterialCode = NewuGlobal.Now_OrderName,
                    SaveTime = DateTime.Now,
                    Reserve1 = tempStr
                };
                BarcodeRecord.Add(barcode);
            }
        }

        private void SetText(string text, bool visible, Color color)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.lb_Scan.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, visible, color });
            }
            else
            {
                this.lb_Scan.Text = text;
                this.lb_Scan.Visible = visible;
                this.lb_Scan.ForeColor = color;
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
            FunClass.WriteLogFile("胶料扫描枪启动成功", "Log\\ScanRubber" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
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

        private void tcpServer_ClientConnectedC(object sender, AsyncEventArgs e)
        {
            FunClass.WriteLogFile("炭黑扫描枪建立连接", "Log\\ScanCarbon" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }

        private void tcpServer_ClientConnectedO(object sender, AsyncEventArgs e)
        {
            FunClass.WriteLogFile("油料扫描枪建立连接", "Log\\ScanOil" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }

        #endregion 条码扫描程序

        private int DropDownflag = 0;

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

            #region 控件样式

            ColStruct[] cols = null;
            if (NewuGlobal.SoftConfig.IsFinalMix())
            {
                txtBatchReal_C.Visible = false;
                txtBatchReal_O.Visible = false;
                txtBatchReal_Si.Visible = false;
                txtBatchReal_Zno.Visible = false;
                tbCarbon.Visible = false;
                tbCarbon2.Visible = false;
                tbOil.Visible = false;
                tbSi.Visible = false;
                tbZno.Visible = false;

                lb_Scan.Visible = false;
                lb_Carbon.Visible = false;
                lb_Carbon2.Visible = false;
                lb_Oil.Visible = false;
                lb_Silana.Visible = false;
                lb_Zon.Visible = false;
                lb_c.Visible = false;
                lb_o.Visible = false;
                lb_si.Visible = false;
                lb_z.Visible = false;

                cols = new ColStruct[] {
                new ColStruct("Reserve4","投料次序"),
                new ColStruct("WeighOrder","称量次序",ColumnType.txt,false),
                new ColStruct("WeighMaterialCode","物料名称", ColumnType.txt,true),
                new ColStruct("DevicePartID","称量部件", ColumnType.txt,false),
                new ColStruct("WeighSetVal","标准重量"),
                new ColStruct("Reserve5","实际重量"),
                new ColStruct("AllowError","公差"),
                new ColStruct("Reserve3","扫描",ColumnType.txt,false)
                };
            }
            else
            {
                cols = new ColStruct[] {
                new ColStruct("Reserve4","投料次序"),
                new ColStruct("WeighOrder","称量次序",ColumnType.txt,false),
                new ColStruct("WeighMaterialCode","物料名称", ColumnType.txt,true),
                new ColStruct("DevicePartID","称量部件", ColumnType.txt,false),
                new ColStruct("WeighSetVal","标准重量"),
                new ColStruct("Reserve5","实际重量"),
                new ColStruct("AllowError","公差"),
                new ColStruct("Reserve3","扫描",ColumnType.txt,false)
                };
            }
            dgvMat.VisibleOrderNumber = false;
            dgvMat.AllowUserToAddRows = false;
            dgvMat.AddCols(cols);
            dgvMat.ReadOnly = true;
            dgvMat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvMat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMat.MultiSelect = false;
            dgvMat.ScrollBars = ScrollBars.None;
            dgvMat.EnableHeadersVisualStyles = false;
            dgvMat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvMat.ColumnHeadersHeight = 35;
            dgvMat.RowTemplate.MinimumHeight = 45;
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

            #endregion 控件样式

            ViewHelper.InitMixTechDataGridView(dgvMlgy);
            ViewHelper.DisPlayTable(dgvMlgy);
            GetData();

            NewuGlobal.FmJL = this;
            //启动扫描程序
            StartMonitorScanner();
            //StartMonitorScannerCarbon();
            //StartMonitorScannerOil();

            SetControlLanguageText();
            InitChangeSignal(); //初始化称量值变化信号
            StartMonitorChangeSignal();

            for (int i = 0; i < changeSignal.Length; i++)
            {
                changeSignal[i].PropertyChanged += ChangeStartSignal_PropertyChanged;
            }
        }

        #region 根据条码获取斗号

        private int getBinNo(string barCode)
        {
            int BinNo = 0;
            string strWhere = "Reserve1='" + barCode + "' ";
            List<TB_BinSetingMDL> rawList = tbSeting.GetModelList(strWhere);
            if (rawList.Count > 0)
            {
                BinNo = rawList[0].BinNo;
            }
            else
            {
                BinNo = 0;
            }
            return BinNo;
        }

        #endregion 根据条码获取斗号

        private void InitChangeSignal()
        {
            changeSignal = new MemoryNotify[1];  //就一个
            changeSignal[0] = new MemoryNotify
            {
                Address = 452,
                Number = 0
            };
        }

        public bool IsMonitorChangeSignal
        {
            get;
            set;
        }

        public async Task StartMonitorChangeSignal()
        {
            if (IsMonitorChangeSignal)
                return;

            IsMonitorChangeSignal = true;
            while (IsMonitorChangeSignal)
            {
                //配方发送信号最好用for循环
                for (int i = 0; i < changeSignal.Length; i++)
                {
                    if (IsMonitorChangeSignal)
                    {
                        changeSignal[i].Value = NewuGlobal.MemMgr.GetSharedMemIntValue(changeSignal[i].Address, 4);
                    }
                    else
                    {
                        return;
                    }
                }
                await Task.Delay(100);
            }
        }

        private void ChangeStartSignal_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //清空扫描信息
            SetText("", true, Color.DarkGray);
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            label9.Text = NewuGlobal.GetRes("000674");//*炼胶配方
            label8.Text = NewuGlobal.GetRes("000701");//*下个炼胶配方

            label2.Text = NewuGlobal.GetRes("000692");//*炼胶设定车数
            label3.Text = NewuGlobal.GetRes("000693");//*当前胶料车数

            lb_d.Text = NewuGlobal.GetRes("000672");//*当前小药车数
            lb_c.Text = NewuGlobal.GetRes("000695");//*当前炭黑车数
            label4.Text = NewuGlobal.GetRes("000696");//*当前密炼车数
            lb_o.Text = NewuGlobal.GetRes("000694");//*当前油料车数
            lb_si.Text = NewuGlobal.GetRes("000727");//*当前硅烷车数
            lb_z.Text = NewuGlobal.GetRes("000728");//*当前ZnO车数
            label5.Text = NewuGlobal.GetRes("000697");//*配方时间

            labScaleState.Text = NewuGlobal.GetRes("000703");
            //bt_C.Text = NewuGlobal.GetRes("000698");//*炭黑
            //bt_O.Text = NewuGlobal.GetRes("000699");//*油料
            //bt_Door.Text = NewuGlobal.GetRes("000700");//*卸料
            // disPlayBarBar1.SetTitle(NewuGlobal.GetRes("000701"));//*设备正常

            lb_temp.Text = NewuGlobal.GetRes("000636");  //温度
            lb_press.Text = NewuGlobal.GetRes("000639"); //压力
            lb_speed.Text = NewuGlobal.GetRes("000640"); //转速
            lb_energy.Text = NewuGlobal.GetRes("000638");//能量
            lb_Drug.Text = NewuGlobal.GetRes("000724");//药品秤
            lb_Silana.Text = NewuGlobal.GetRes("000725");//硅烷秤
            lb_Zon.Text = NewuGlobal.GetRes("000726");//ZnO秤
            lb_Carbon.Text = NewuGlobal.GetRes("000154");//炭黑秤
            lb_Carbon2.Text = NewuGlobal.GetRes("000155");//炭黑中间斗秤
            lb_Oil.Text = NewuGlobal.GetRes("000153");//油料秤
            lb_Rub.Text = NewuGlobal.GetRes("000156");//胶料秤

            LanguageDGV(dgvMat, 675);
            LanguageDGV(dgvMlgy, 683);
        }

        private void LanguageDGV(DataGridViewEx dgv, int start)
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

        private void GetData()
        {
            string RubberpartCode = NewuGlobal.GetDevicePartCode(DevicePartType.Rubber);//部件编码
            string RubberpartID = NewuGlobal.GetDevicePartIDByPartCode(RubberpartCode);//部件ID

            string strWhere = "DevicePartID='" + RubberpartID + "' and DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "'";
            List<PM_DevicePartOrderTranMDL> orderlist = orderDeviceBll.GetModelList(strWhere);
            if (orderlist.Count != 1)
            {
                labScaleState.Text = NewuGlobal.GetRes("000705") + orderlist.Count;
                return;
            }

            strWhere = "MaterialID='" + orderlist[0].MaterialID + "' and " +
                    "DevicePartID='" + RubberpartID + "'";
            rawList = rawBll.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
            foreach (FormulaWeighMDL item in rawList)
            {
                item.Reserve4 = "R" + item.DropOrder + "-" + item.WeighOrder;
                item.WeighSetVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, item.WeighSetVal);
                item.AllowError = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, item.AllowError);
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

                List<FormulaWeighMDL> temp = rawBll.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");

                foreach (FormulaWeighMDL item in temp)
                {
                    item.Reserve4 = "D" + item.DropOrder + "-" + item.WeighOrder;
                    item.WeighSetVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, item.WeighSetVal);
                    item.AllowError = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, item.AllowError);
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
                List<FormulaWeighMDL> temp = rawBll.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
                foreach (FormulaWeighMDL item in temp)
                {
                    item.Reserve4 = item.DropOrder + "-" + item.WeighOrder;
                    item.WeighSetVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Plasticizer, item.WeighSetVal);
                    item.AllowError = ScaleAccuracy.AgreeWeightShow(DevicePartType.Plasticizer, item.AllowError);
                }
                if (temp != null)
                    rawList.AddRange(temp);
            }

            dgvMat.DataSource = rawList;
            this.dgvMat.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            labNowName.Text = NewuGlobal.Now_OrderName;
            labNextName.Text = NewuGlobal.Next_OrderName;
        }

        /// <summary>
        /// 发送配方完毕后，会以接口的形式  通知该界面更新
        /// </summary>
        public void RefreshData()
        {
            ViewHelper.DisPlayTable(dgvMlgy);
            GetData();
        }

        public void RefreshData(bool isWeight)
        {
            RefreshData();
        }

        private void CheackSendOK()
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
        private int flagTimers = 0;

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //用于判断屏幕是否假死
            lblTime.Text = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="work"></param>
        /// <param name="barCode"></param>
        /// <param name="type"></param>
        public void ScanRefresh(Repository.Model.WorkType work, string barCode, string type, string scaninfo)
        {
            int cnt = 0;
            foreach (var item in rawList)
            {
                if (work.MaterialID == item.WeighMaterialID && scanRecord[cnt] == 0)
                {
                    scanRecord[cnt] = 1;
                    item.Reserve3 = "ok";
                    dgvMat.InvalidateRow(cnt);
                    if (type == "胶料")
                    {
                        work.che = SS.getInt(1144, 4);
                    }
                    else if (type == "药品")
                    {
                        work.che = SS.getInt(1148, 4);
                    }
                    new PM_ScanRecordBLL().AddDataByWorkUp(work, barCode, type, scaninfo, 0);
                    flagTimers = int.Parse(item.Reserve4[3].ToString());
                    break;
                }
                cnt++;
            }
        }
    }
}