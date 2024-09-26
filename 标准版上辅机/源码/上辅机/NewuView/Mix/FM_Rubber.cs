using MultiLanguage;
using Newu;
using Newu.Net.TCP;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuView.Mix
{
    public partial class FM_Rubber : Form, IRefresh, ScanRefresh, ILanguageChanged
    {
        private bool isSingleRawOk = false, isScaleEnd = false;
        private int selectIndex = 0;
        private int DropDownflag = 0;
        private List<View_FormulaWeigh> rawList;
        private CSharedString ss = NewuGlobal.MemDB;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private PM_DevicePartOrderTranRepository devicePartOrderTranRepository = new PM_DevicePartOrderTranRepository();
        private ViewFormulaWeighRepository viewFormulaWeighRepository = new ViewFormulaWeighRepository();
        private FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private int[] scanRecord = new int[100];
        private MemoryNotify[] changeSignal;  //扫描正确信号
        private RPT_BarcodeRecordRepository barcodeRecordRepository = new RPT_BarcodeRecordRepository();
        private PM_ScanRecordRepository scanRecordRepository = new PM_ScanRecordRepository();
        private TB_BinSettingRepository binSettingRepository = new TB_BinSettingRepository();

        public FM_Rubber()
        {
            InitializeComponent();
            timer.Interval = 500;
            timer.Elapsed += Timer_Tick;
            timer.Enabled = true;
        }

        private void FM_Rubber_Load(object sender, EventArgs e)
        {
            InitView();
        }

        private async void InitView()
        {
            try
            {
                if (!NewuGlobal.SoftConfig.IsShowPartBatch)
                {
                    splitContainer1.Panel2Collapsed = true;
                }

                if (NewuGlobal.RubyDataChange == null)
                    NewuGlobal.RubyDataChange = this;

                if (NewuGlobal.RubyScan == null)
                    NewuGlobal.RubyScan = this;

                scrollingText1.ScrollText = NewuGlobal.GetRes("000714");

                ViewHelper.InitWeightDataGridView(dgvExWeight);
                ViewHelper.InitMixTechDataGridView(dgvEXMixer);
                ViewHelper.DisPlayTable(dgvEXMixer);

                splitContainer2.Panel2Collapsed = true;
                txtMixerDown.Visible = false;
                txtMixerTimeDown.Visible = false;
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    txtMixerDown.Visible = true;
                    txtMixerTimeDown.Visible = true;
                    splitContainer2.Panel2Collapsed = false;
                    ViewHelper.InitMixTechDataGridView(dgvEXMixerDown);
                    ViewHelper.DisPlayTable(dgvEXMixerDown);
                }

                GetData();
                SaveAlarmUtil.GetInstance().MonitorAlarm += FM_Rubber_MonitorAlarm;

                NewuGlobal.FmJL = this;
                //启动扫描程序
                StartMonitorScanner();

                SetControlLanguage();

                InitChangeSignal();

                await StartMonitorChangeSignal();

                for (int i = 0; i < changeSignal.Length; i++)
                {
                    changeSignal[i].PropertyChanged += ChangeStartSignal_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void FM_Rubber_MonitorAlarm(string alarmInfo)
        {
            try
            {
                if (scrollingText1.InvokeRequired)
                    BeginInvoke(new Action<string>(FM_Rubber_MonitorAlarm), alarmInfo);
                else
                    scrollingText1.ScrollText = alarmInfo;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void GetData()
        {
            try
            {
                string rubberPartCode = NewuGlobal.GetDevicePartCode(DevicePartType.Rubber);//部件编码
                string rubberPartID = NewuGlobal.GetDevicePartIDByPartCode(rubberPartCode);//部件ID
                string strWhere = "DevicePartID = '" + rubberPartID + "' and DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "'";

                List<PM_DevicePartOrderTran> devicePartOrderTrans = devicePartOrderTranRepository.GetModelList(strWhere);
                if (devicePartOrderTrans == null || devicePartOrderTrans.Count <= 0)
                    return;
                strWhere = "MaterialID='" + devicePartOrderTrans[0].MaterialID + "' and DevicePartID='" + rubberPartID + "'";
                rawList = viewFormulaWeighRepository.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
                foreach (View_FormulaWeigh item in rawList)
                {
                    item.Reserve4 = "R" + item.DropOrder + "-" + item.WeighOrder;
                    item.WeighSetVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, item.WeighSetVal);
                    item.AllowError = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, item.AllowError);
                }

                if (!NewuGlobal.SoftConfig.IsFinalMix())
                {
                    List<View_FormulaWeigh> drugFormulaWeighs = GetWeightData(DevicePartType.DrugMixer, devicePartOrderTrans);
                    foreach (var item in drugFormulaWeighs)
                    {
                        item.Reserve4 = "D" + item.DropOrder + "-" + item.WeighOrder;
                        item.WeighSetVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, item.WeighSetVal);
                        item.AllowError = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, item.AllowError);
                    }

                    if (drugFormulaWeighs != null && drugFormulaWeighs.Count > 0)
                    {
                        rawList.AddRange(drugFormulaWeighs);
                    }

                    List<View_FormulaWeigh> plaFormulaWeighs = GetWeightData(DevicePartType.Plasticizer, devicePartOrderTrans);
                    foreach (var item in plaFormulaWeighs)
                    {
                        item.Reserve4 = item.DropOrder + "-" + item.WeighOrder;
                        item.WeighSetVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Plasticizer, item.WeighSetVal);
                        item.AllowError = ScaleAccuracy.AgreeWeightShow(DevicePartType.Plasticizer, item.AllowError);
                    }

                    if (plaFormulaWeighs != null && plaFormulaWeighs.Count > 0)
                    {
                        rawList.AddRange(plaFormulaWeighs);
                    }
                }
                BindingWeighMaterialIDData();
                if (dgvExWeight.InvokeRequired)
                {
                    dgvExWeight.Invoke(new Action(() => dgvExWeight.DataSource = rawList));
                }
                else
                {
                    dgvExWeight.DataSource = rawList;
                }

                labNowName.Text = NewuGlobal.Now_OrderName;
                labNextName.Text = NewuGlobal.Next_OrderName;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 根据部件获取称量信息
        /// </summary>
        /// <param name="devicePartType"></param>
        /// <param name="devicePartOrderTran"></param>
        /// <returns></returns>
        private List<View_FormulaWeigh> GetWeightData(DevicePartType devicePartType, List<PM_DevicePartOrderTran> devicePartOrderTran)
        {
            try
            {
                string devicePartCode = NewuGlobal.GetDevicePartCode(devicePartType);
                string devicePartID = NewuGlobal.GetDevicePartIDByPartCode(devicePartCode);
                string strWhere = "MaterialID='" + devicePartOrderTran[0].MaterialID + "' and DevicePartID='" + devicePartID + "'";
                List<View_FormulaWeigh> formulaWeighs = viewFormulaWeighRepository.GetModelList(0, strWhere, "DropOrder,WeighOrder asc");
                return formulaWeighs;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
                return null;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MonitorRefresh();
        }

        private void MonitorRefresh()
        {
            try
            {
                if (InvokeRequired)
                {
                    Action d = () => MonitorRefresh();
                    Invoke(d);
                    return;
                }
                else
                {
                    DisView();
                    ViewHelper.RefreshMixTechDataGridView(dgvEXMixer);
                    if (NewuGlobal.SoftConfig.DownMixer)
                        ViewHelper.RefreshMixTechDataGridViewF(dgvEXMixerDown);
                    labNowName.Text = NewuGlobal.Now_OrderName;
                    labNextName.Text = NewuGlobal.Next_OrderName;
                    txtTime.Text = DateTime.Now.ToString();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void DisView()
        {
            try
            {
                //人工选择手动称量显示此区域，功能是否启用通过配置文件实现
                RefreshWeightMode();
                TextRefresh();

                if (dgvExWeight.RowCount > 0)
                {
                    int weightNo = ss.GetHex((int)MixerNetWeight.RubberSerialNum, 4);
                    if (weightNo == 0)
                    {
                        int index = dgvExWeight.RowCount - 1;
                        dgvExWeight.Rows[index].Selected = false;
                        labScaleState.Text = NewuGlobal.GetRes("000703");
                    }

                    //投胶动作
                    if (ss.Getbool((int)MixerDigitalMiningRubber.FeedingBeltMotor) && ss.Getbool((int)MixerDigitalMiningRubber.RubberScaleMotor) && DropDownflag == 0)
                    {
                        DropDownflag = 1;
                        ViewHelper.ClearWeightData(dgvExWeight, rawList);
                        return;
                    }

                    if (dgvExWeight.Rows.Count < 1)
                        return;

                    int scaleIndex = dgvExWeight.CurrentRow.Index;
                    if (ss.GetStr((int)MixerDigitalMiningRubber.RubberFilpScreenSignal, 1) == "1" && isSingleRawOk == false)//703胶料秤翻屏信号
                    {
                        isSingleRawOk = true;
                        labScaleState.Text = NewuGlobal.GetRes("000703");
                        if (ss.GetHex((int)MixerAnalogMiningSetBatch.Rubber, 4) <= ss.GetInt((int)MixerAnalogMiningActBatch.Rubber, 4) && scaleIndex == dgvExWeight.Rows.Count - 1)//1104胶料设定车数，1028胶秤车数
                        {
                            labScaleState.Text = NewuGlobal.GetRes("000703");
                            dgvExWeight.ClearSelection();
                            isScaleEnd = true;
                        }
                        else
                        {
                            isScaleEnd = false;
                        }
                    }
                    else
                    {
                        if (ss.GetStr((int)MixerDigitalMiningRubber.RubberFilpScreenSignal, 1) == "0" && isSingleRawOk == true)//703胶料秤翻屏信号
                        {
                            isSingleRawOk = false;
                        }

                        int weightNum_R = ss.GetHex((int)MixerNetWeight.RubberSerialNum, 4);
                        if (weightNum_R != 0)
                        {
                            labScaleState.Text = NewuGlobal.GetRes("000702") + (weightNum_R / 10 % 10) + " - " + (weightNum_R % 10);
                        }
                    }

                    if (!isScaleEnd)
                    {
                        if (rawList == null)
                            return;

                        #region 胶料表格秤逻辑

                        int weightNum = ss.GetHex((int)MixerNetWeight.RubberSerialNum, 4);
                        int weightLeft = weightNum / 10 % 10;
                        int weightRight = weightNum % 10;
                        int cnt = 0;
                        foreach (View_FormulaWeigh item in rawList)
                        {
                            if (NewuGlobal.GetDevicePartCode(DevicePartType.Rubber) == item.DevicePartCode)
                            {
                                //称好信号
                                if (ss.Getbool((int)MixerDigitalMiningRubber.WeightingOK))
                                {
                                    dgvExWeight.Rows[cnt].Cells["Reserve3"].Value = ""; //称好以后清空扫描结果，准备下次扫描。
                                    scanRecord[cnt] = 0;
                                }

                                if (item.DropOrder == weightLeft && item.WeighOrder == weightRight)
                                {
                                    dgvExWeight.Rows[cnt].Selected = true;
                                    selectIndex = cnt;
                                    dgvExWeight.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                                    if (NewuGlobal.SoftConfig.RubScanner)
                                    {
                                        if (dgvExWeight.CurrentRow.Cells["Reserve3"].Value == null)
                                        {
                                            return;
                                        }
                                        if (dgvExWeight.CurrentRow.Cells["Reserve3"].Value.ToString().Equals("正确"))
                                        {
                                            item.WeighActVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.GetHex((int)MixerNetWeight.Rubber, 4));
                                        }
                                        else
                                        {
                                            lb_Scan.Text = "请扫描胶料条码";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (ss.Getbool((int)MixerDigitalMiningRubber.MidSign))
                                            item.WeighActVal = "-" + ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.GetHex((int)MixerNetWeight.Rubber, 4));
                                        else
                                            item.WeighActVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.GetHex((int)MixerNetWeight.Rubber, 4));
                                    }
                                    dgvExWeight.InvalidateRow(cnt);
                                    break;
                                }
                            }
                            cnt++;
                        }

                        #endregion 胶料表格秤逻辑

                        #region 小药表格秤逻辑

                        if (NewuGlobal.SoftConfig.Drug)
                        {
                            weightNum = ss.GetHex((int)MixerNetWeight.DrugMixerSerialNum, 4);
                            weightLeft = weightNum / 10 % 10;
                            weightRight = weightNum % 10;
                            cnt = 0;
                            foreach (View_FormulaWeigh item in rawList)
                            {
                                if (NewuGlobal.GetDevicePartCode(DevicePartType.DrugMixer) == item.DevicePartCode)
                                {
                                    if (ss.Getbool((int)MixerDigitalMiningDrug.WeightingOKDualScreen))
                                    {
                                        dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Green;
                                        dgvExWeight.Rows[cnt].Cells["Reserve3"].Value = ""; //称好以后清空扫描结果，准备下次扫描。
                                        scanRecord[cnt] = 0;
                                    }
                                    else if (item.DropOrder == weightLeft && item.WeighOrder == weightRight)
                                    {
                                        if (NewuGlobal.SoftConfig.DrugScanner)
                                        {
                                            if (dgvExWeight.CurrentRow.Cells["Reserve3"].Value == null)
                                            {
                                                return;
                                            }
                                            if (dgvExWeight.CurrentRow.Cells["Reserve3"].Value.ToString().Equals("正确"))
                                            {
                                                item.WeighActVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, ss.GetHex((int)MixerNetWeight.DrugMixer, 4));
                                                dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                lb_Scan.Text = "请扫描药品条码";
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            item.WeighActVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.DrugMixer, ss.GetHex((int)MixerNetWeight.DrugMixer, 4));
                                            dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Red;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    dgvExWeight.InvalidateRow(cnt);
                                }
                                cnt++;
                            }
                        }

                        #endregion 小药表格秤逻辑

                        #region 塑解剂表格秤逻辑

                        if (NewuGlobal.SoftConfig.Pla)
                        {
                            weightNum = ss.GetHex((int)MixerNetWeight.PlasticizerSerialNum, 4);
                            weightLeft = weightNum / 10 % 10;
                            weightRight = weightNum % 10;
                            cnt = 0;
                            foreach (View_FormulaWeigh item in rawList)
                            {
                                if (NewuGlobal.GetDevicePartCode(DevicePartType.Plasticizer) == item.DevicePartCode)
                                {
                                    if (ss.Getbool((int)MixerDigitalMiningPlasticizer.WeightingOKDualScreen))
                                    {
                                        dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Green;
                                        break;
                                    }
                                    else if (item.DropOrder == weightLeft && item.WeighOrder == weightRight)
                                    {
                                        item.WeighActVal = ScaleAccuracy.AgreeWeightShow(DevicePartType.Plasticizer, ss.GetHex((int)MixerNetWeight.Plasticizer, 4));
                                        dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Red;
                                        break;
                                    }
                                    else
                                    {
                                        dgvExWeight.Rows[cnt].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    dgvExWeight.InvalidateRow(cnt);
                                }
                                cnt++;
                            }
                        }

                        #endregion 塑解剂表格秤逻辑
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 人工手动称量时显示此区域 功能是否开启通过配置文件决定
        /// </summary>
        private void RefreshWeightMode()
        {
            if (NewuGlobal.SoftConfig.ManualWeight)
            {
                if (ss.Getbool((int)MixerDigitalMiningRubber.Auto))
                {
                    labelRubberWeight.Visible = false;
                    label10.Visible = false;
                }
                else
                {
                    labelRubberWeight.Visible = true;
                    label10.Visible = true;
                    //胶料+-号
                    if (ss.Getbool((int)MixerDigitalMiningRubber.Sign))
                    {
                        label10.Text = "-";
                        labelRubberWeight.Text = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.GetHex((int)MixerAnalogMiningWeight.Rubber, 4));
                    }
                    else
                    {
                        label10.Text = "+";
                        labelRubberWeight.Text = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.GetHex((int)MixerAnalogMiningWeight.Rubber, 4));
                    }
                }
            }
        }

        /// <summary>
        /// 文本刷新区域
        /// </summary>
        private void TextRefresh()
        {
            DropDownflag = 0;
            txtCarbon.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Carbon, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Carbon, 4).ToString();
            txtOil.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Oil, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Oil, 4).ToString();
            txtZno.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Zno, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Zno, 4).ToString();
            txtDrug.Text = ss.GetInt((int)MixerAnalogMiningActBatch.DrugMixer, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.DrugMixer, 4).ToString();
            txtPla.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Plasticizer, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Plasticizer, 4).ToString();//塑解剂车数
            txtSi.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Silane, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Silane, 4).ToString();
            txtMiLian.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Mixer, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Mixer, 4).ToString();

            txtMixerDown.Text = ss.GetInt((int)MixerAnalogMiningActBatch.DownMixer, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningMixerDown.SetBatch, 4).ToString();

            txtRubber.Text = ss.GetInt((int)MixerAnalogMiningActBatch.Rubber, 4).ToString() + "/" + ss.GetInt((int)MixerAnalogMiningSetBatch.Rubber, 4).ToString();//胶秤车数

            labelScaleReal.Text = ScaleAccuracy.AgreeWeightShow(DevicePartType.Rubber, ss.GetHex((int)MixerAnalogMiningMixer.DualScreenWeight, size: 4));//16进制

            if (ss.Getbool((int)MixerDigitalMiningMixer.CurveStart))
            {
                lblMixingTime.Text = ss.GetInt(AddressConst.MixerTime, 4).ToString();
            }
            else
            {
                lblMixingTime.Text = "0";
            }

            if (ss.Getbool((int)MixerDigitalMiningMixerDown.CurveStart))
            {
                txtMixerTimeDown.Text = ss.GetInt(AddressConst.MixerFTime, 4).ToString();
            }
            else
            {
                txtMixerTimeDown.Text = "0";
            }

            if (ss.Getbool((int)MixerDigitalMiningRubber.Auto))
            {
                lb_RubberStatue.Text = NewuGlobal.LanguagResourceManager.GetString("000731");
                lb_RubberStatue.ForeColor = Color.Green;
            }
            else
            {
                lb_RubberStatue.Text = NewuGlobal.LanguagResourceManager.GetString("000732");
                lb_RubberStatue.ForeColor = Color.Red;
            }

            if (ss.Getbool((int)MixerDigitalMiningMixer.Auto))
            {
                lb_MixerStatue.Text = NewuGlobal.LanguagResourceManager.GetString("000733");
                lb_MixerStatue.ForeColor = Color.Green;
            }
            else
            {
                lb_MixerStatue.Text = NewuGlobal.LanguagResourceManager.GetString("000734");
                lb_MixerStatue.ForeColor = Color.Red;
            }

            if (ss.GetStr((int)MixerDigitalMiningRubber.RubberFilpScreenSignal, 1) != "1")//胶料秤翻屏信号
            {
                if (ss.GetStr((int)MixerDigitalMiningRubber.RubberDualScreenSign, 1) == "1")//胶秤双屏正负号
                {
                    labWeightFlag.Text = "-";
                }
                else
                {
                    labWeightFlag.Text = "+";
                }
            }
        }

        private void StartMonitorScanner()
        {
            try
            {
                if (NewuGlobal.SoftConfig.RubScanner)
                {
                    AsyncTCPServer tCPServerR = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), int.Parse(NewuGlobal.SoftConfig.ListenerPort_R), 10);
                    tCPServerR.ClientConnected += TcpServer_ClientConnected_R;
                    tCPServerR.DataReceived += TcpServer_DataReceived_R;
                    tCPServerR.Start();
                }

                if (NewuGlobal.SoftConfig.DrugScanner)
                {
                    AsyncTCPServer tCPServerD = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), int.Parse(NewuGlobal.SoftConfig.ListenerPort_D), 10);
                    tCPServerD.ClientConnected += TCPServerD_ClientConnected;
                    tCPServerD.DataReceived += TCPServerD_DataReceived;
                }

                if (NewuGlobal.SoftConfig.CarBonScanner)
                {
                    AsyncTCPServer tCPServerC = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), int.Parse(NewuGlobal.SoftConfig.ListenerPort_C), 10);
                    tCPServerC.ClientConnected += TCPServerC_ClientConnected;
                    tCPServerC.DataReceived += TCPServerC_DataReceived;
                }

                if (NewuGlobal.SoftConfig.ZnOScanner)
                {
                    AsyncTCPServer tCPServerZnO = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), int.Parse(NewuGlobal.SoftConfig.ListenerPort_ZnO), 10);
                    tCPServerZnO.ClientConnected += TCPServerZnO_ClientConnected;
                    tCPServerZnO.DataReceived += TCPServerZnO_DataReceived;
                }

                if (NewuGlobal.SoftConfig.OilScanner)
                {
                    AsyncTCPServer tCPServerO = new AsyncTCPServer(IPAddress.Parse(NewuGlobal.SoftConfig.PCIP), int.Parse(NewuGlobal.SoftConfig.ListenerPort_O), 10);
                    tCPServerO.ClientConnected += TCPServerO_ClientConnected;
                    tCPServerO.DataReceived += TCPServerO_DataReceived;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void TcpServer_ClientConnected_R(object sender, AsyncEventArgs e)
        {
            StartScannerResult("胶料");
        }

        private void TCPServerD_ClientConnected(object sender, AsyncEventArgs e)
        {
            StartScannerResult("药品");
        }

        private void TCPServerC_ClientConnected(object sender, AsyncEventArgs e)
        {
            StartScannerResult("炭黑");
        }

        private void TCPServerZnO_ClientConnected(object sender, AsyncEventArgs e)
        {
            StartScannerResult("粉料");
        }

        private void TCPServerO_ClientConnected(object sender, AsyncEventArgs e)
        {
            StartScannerResult("油料");
        }

        private void StartScannerResult(string scannerType)
        {
            try
            {
                NewuGlobal.LogCat("FM_Rubber").Info(scannerType + "扫描枪启动成功");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 胶料扫描枪接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpServer_DataReceived_R(object sender, AsyncEventArgs e)
        {
            try
            {
                string barcodeStr = Encoding.UTF8.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                GetScannerBarcode((int)MixerNetWeight.RubberSerialNum, barcodeStr, "胶料", (int)MixerDigitalMiningScanner.RubberScanOK);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 药品扫描枪接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPServerD_DataReceived(object sender, AsyncEventArgs e)
        {
            try
            {
                string barcodeStr = Encoding.UTF8.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                GetScannerBarcode((int)MixerNetWeight.DrugMixerSerialNum, barcodeStr, "药品", (int)MixerDigitalMiningScanner.DrugScanOK);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 胶料、药品扫描器获取条码执行逻辑
        /// </summary>
        /// <param name="weightNoSingal"></param>
        /// <param name="barcodeStr"></param>
        /// <param name="materialType"></param>
        /// <param name="scannerResultSinal"></param>
        public void GetScannerBarcode(int weightNoSingal, string barcodeStr, string materialType, int scannerResultSinal)
        {
            try
            {
                int weightNo = ss.GetHex(weightNoSingal, 4);//称量序号
                int weightLeft = weightNo / 10;
                int weightRight = weightNo % 10;
                string scanText;

                RPT_BarcodeRecordRepository barcodeRecordRepository = new RPT_BarcodeRecordRepository();
                string materialCode = dgvExWeight.Rows[selectIndex].Cells["WeighMaterialID"].Value.ToString();//物料名
                string strWhere = "BarCode in ('" + barcodeStr + "') and MaterialID ='" + materialCode + "'";
                List<FormulaMaterial> formulaMaterials = formulaMaterialRepository.GetList(strWhere);

                if (formulaMaterials.Count > 0)
                {
                    scanText = NewuGlobal.GetRes("000716") + barcodeStr;
                    SetText(scanText, true, Color.Green);
                    NewuGlobal.MemMgr.Sync(scannerResultSinal, "0001");//扫描成功后，发送扫描正确信号给电气，开始称量后电气置0
                    SaveBarcode(materialCode, barcodeStr);
                    PM_ScanRecord scanRecord = new PM_ScanRecord()
                    {
                        PortBarcode = "0",
                        DeviceID = NewuGlobal.SoftConfig.DeviceID,
                        DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                        OrderID = NewuGlobal.Now_OrderID,
                        MaterialCode = NewuGlobal.Now_OrderName,
                        SaveTime = DateTime.Now,
                        TypeCodeName = materialType,
                        SaveUserCode = NewuGlobal.TB_UserInfo.UserCode,
                        SaveUserID = NewuGlobal.TB_UserInfo.UserID,
                        MatBarcode = barcodeStr,
                        Is_Read = 0,
                        VersionID = int.Parse(NewuGlobal.SoftConfig.VersionID)
                    };

                    scanRecordRepository.Add(scanRecord);
                    dgvExWeight.Rows[selectIndex].Cells["Reserve3"].Value = "正确";
                }
                else
                {
                    scanText = NewuGlobal.GetRes("000717") + barcodeStr;
                    SetText(scanText, true, Color.Red);
                    NewuGlobal.MemMgr.Sync(scannerResultSinal, "0000");
                    dgvExWeight.Rows[selectIndex].Cells["Reserve3"].Value = "错误";
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void SaveBarcode(string materialCode, string barcodeStr)
        {
            try
            {
                bool isExist = barcodeRecordRepository.IsExists(NewuGlobal.Now_OrderID, NewuGlobal.GetDevicePartCode(DevicePartType.Rubber), materialCode);
                if (isExist)
                    barcodeRecordRepository.UpdateBarcode(barcodeStr, DateTime.Now, NewuGlobal.Now_OrderID, materialCode);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 炭黑扫描枪接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPServerC_DataReceived(object sender, AsyncEventArgs e)
        {
            try
            {
                string barCodeStr = Encoding.UTF8.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                GetScannerBarcode(barCodeStr, "炭黑", (int)MixerDigitalMiningScanner.CarbonBin);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 粉料扫描枪接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPServerZnO_DataReceived(object sender, AsyncEventArgs e)
        {
            try
            {
                string barCodeStr = Encoding.UTF8.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                GetScannerBarcode(barCodeStr, "粉料", (int)MixerDigitalMiningScanner.ZnoBin);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void TCPServerO_DataReceived(object sender, AsyncEventArgs e)
        {
            try
            {
                string barCodeStr = Encoding.UTF8.GetString(e._state.Buffer, 0, e._state.ReceviedDataLength).Trim();
                GetScannerBarcode(barCodeStr, "油料", (int)MixerDigitalMiningScanner.OilBin);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private void GetScannerBarcode(string barcodeStr, string materialType, int scannerResultSinal)
        {
            try
            {
                barcodeRecordRepository.TableYear = NewuGlobal.RunInfo.OrderRawModel.Savetime.Year;
                int binNo = GetBinNo(barcodeStr);
                if (binNo != 0)
                {
                    NewuGlobal.MemMgr.Sync(scannerResultSinal, binNo.ToString("0000"));
                    bool isExist = barcodeRecordRepository.IsExists(NewuGlobal.Now_OrderID, NewuGlobal.GetDevicePartCode(DevicePartType.Carbon), materialType);
                    //条码记录表中有条码就更新条码和保存时间
                    if (isExist)
                    {
                        barcodeRecordRepository.UpdateBarcode(barcodeStr, DateTime.Now, NewuGlobal.Now_OrderID, materialType);
                    }
                    //没有条码记录就插入条码全部信息
                    else
                    {
                        RPT_BarcodeRecord barcodeRecord = new RPT_BarcodeRecord()
                        {
                            OrderID = NewuGlobal.Now_OrderID,
                            DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                            MaterialCode = materialType,
                            TypeCodeName = NewuGlobal.GetDevicePartCode(DevicePartType.Carbon),
                            WeighMaterialCode = NewuGlobal.Now_OrderName,
                            SaveTime = DateTime.Now,
                            Reserve1 = barcodeStr
                        };
                        barcodeRecordRepository.Add(barcodeRecord);
                    }

                    PM_ScanRecord scannerRecord = new PM_ScanRecord
                    {
                        PortBarcode = "0",
                        DeviceID = NewuGlobal.SoftConfig.DeviceID,
                        DeviceCode = NewuGlobal.SoftConfig.DeviceCode,
                        OrderID = NewuGlobal.Now_OrderID,
                        MaterialCode = NewuGlobal.Now_OrderName,
                        SaveTime = DateTime.Now,
                        TypeCodeName = materialType,
                        SaveUserCode = NewuGlobal.TB_UserInfo.UserCode,
                        SaveUserID = NewuGlobal.TB_UserInfo.UserID,
                        MatBarcode = barcodeStr,
                        Reserve1 = "条码正确！",
                        Is_Read = 0,
                        VersionID = int.Parse(NewuGlobal.SoftConfig.VersionID)
                    };

                    scanRecordRepository.Add(scannerRecord);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        private int GetBinNo(string barCode)
        {
            string strWhere = "Reserve1='" + barCode + "' ";
            List<TB_BinSeting> binSettingList = binSettingRepository.GetModelList(strWhere);
            int BinNo;
            if (binSettingList.Count > 0)
            {
                BinNo = binSettingList[0].BinNo;
            }
            else
            {
                BinNo = 0;
            }
            return BinNo;
        }

        private void InitChangeSignal()
        {
            changeSignal = new MemoryNotify[1];  //就一个
            changeSignal[0] = new MemoryNotify
            {
                Address = (int)MixerNetWeight.RubberSerialNum,
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
                await Task.Delay(500);
            }
        }

        private void ChangeStartSignal_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //清空扫描信息
            SetText("", true, Color.DarkGray);
        }

        private void SetText(string text, bool visible, Color color)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID 如果它们不相同则返回true
            if (lb_Scan.InvokeRequired)
            {
                Action d = () => SetText(text, visible, color);
                this.Invoke(d);
            }
            else
            {
                this.lb_Scan.Text = text;
                this.lb_Scan.Visible = visible;
                this.lb_Scan.ForeColor = color;
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguage();
        }

        private void SetControlLanguage()
        {
            try
            {
                labelTime.Text = NewuGlobal.GetRes("000723");//
                label_Curren.Text = NewuGlobal.GetRes("000674") + ":";//*炼胶配方
                label_Next.Text = NewuGlobal.GetRes("000701") + ":";//*下个炼胶配方
                labScaleState.Text = NewuGlobal.GetRes("000703");//称量完成

                label_Carbon.Text = NewuGlobal.GetRes("000695");
                label_Oil.Text = NewuGlobal.GetRes("000694");
                label_Zno.Text = NewuGlobal.GetRes("000728");
                label_Drug.Text = NewuGlobal.GetRes("000672");
                label_Si.Text = NewuGlobal.GetRes("000727");
                label_Pla.Text = NewuGlobal.GetRes("000709");
                label_Rubber.Text = NewuGlobal.GetRes("000693");//*胶料车数

                label_Mixer.Text = NewuGlobal.GetRes("000696");
                labelMixTime.Text = NewuGlobal.GetRes("000697");//*配方时间

                label_MixerDown.Visible = false;
                lb_MixerTimeDown.Visible = false;

                ViewHelper.LanguageDGV(dgvExWeight, 675);
                ViewHelper.LanguageDGV(dgvEXMixer, 683);
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    label_MixerDown.Visible = true;
                    lb_MixerTimeDown.Visible = true;
                    label_MixerDown.Text = NewuGlobal.GetRes("000222");//下密炼车数
                    lb_MixerTimeDown.Text = NewuGlobal.GetRes("000246") + "(Down)";//下密炼配方时间
                    ViewHelper.LanguageDGV(dgvEXMixerDown, 683);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_Rubber").Error(ex.ToString());
            }
        }

        public void RefreshData()
        {
            ViewHelper.DisPlayTable(dgvEXMixer);
            ViewHelper.IsRefresh(dgvExWeight);
            if (NewuGlobal.SoftConfig.DownMixer)
                ViewHelper.DisPlayTable(dgvEXMixerDown);
            GetData();
        }

        public void RefreshData(bool isWeight)
        {
            RefreshData();
        }

        public void ScanRefresh(WorkType work, string barCode, string type, string scaninfo)
        {
            throw new NotImplementedException();
        }

        #region 给dgvMat的WeighMaterialID字段绑定数据

        private void BindingWeighMaterialIDData()
        {
            DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgvExWeight.Columns["WeighMaterialID"];
            dgvWeighMaterialID.DataSource = formulaMaterialRepository.GetList("DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID=''");
            dgvWeighMaterialID.DisplayMember = "MaterialCode";
            dgvWeighMaterialID.ValueMember = "MaterialID";
        }

        #endregion 给dgvMat的WeighMaterialID字段绑定数据
    }
}