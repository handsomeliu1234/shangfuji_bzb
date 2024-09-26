using Newu;
using NewuCommon;
using NewuTB.TB;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewuView
{
    public partial class FM_FormulaTransfer : Form
    {
        private SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private TB_FeedingParamRepository feedingParamRepository = new TB_FeedingParamRepository();
        private FeedingParamWriteToMem feedingParamWriteToMem = new FeedingParamWriteToMem();
        private CSharedString ss = NewuGlobal.MemDB;
        private ContinuedOrder coutinuedOrder = ContinuedOrder.GetInstance();
        private TB_OperateLogRepository operateLogRepository = new TB_OperateLogRepository();

        public FM_FormulaTransfer()
        {
            InitializeComponent();
        }

        private void FM_FormulaTransfer_Load(object sender, EventArgs e)
        {
            List<SYS_Device> sYS_Devices = deviceRepository.GetList("");
            cmb_DeviceID.DataSource = sYS_Devices;

            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.ValueMember = "DeviceID";

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("OrderID","订单ID", ColumnType.txt,false),
                new ColStruct("DeviceID","设备ID", ColumnType.txt,false),
                new ColStruct("MaterialID","配方ID",ColumnType.txt,false),
                new ColStruct("MaterialCode","配方名称", ColumnType.txt,true),
                new ColStruct("SerialNumber","顺序"),
                new ColStruct("Lot","批号"),
                new ColStruct("SetBatch","设定车数"),
                new ColStruct("StartUserCode","用户编码"),
                new ColStruct("WorkGroup","班组"),
                new ColStruct("WorkOrder","班次"),
                new ColStruct("Savetime","保存时间"),
            };

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns["Savetime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            dgv.Columns["WorkGroup"].Width = 80;
            dgv.Columns["WorkOrder"].Width = 80;
            dgv.Columns["Savetime"].Width = 150;

            coutinuedOrder.OnSentFormulaProgress += CoutinuedOrder_OnSentFormulaProgress;
            coutinuedOrder.IsSerise = NewuGlobal.SoftConfig.IsContinue;

            GetData();
            Authentication();
            // 检索当前是否正在进行连续配方的状态  给予button 按钮不可使用 显示  且修改text
            SetControlLanguageText();
            MyRefresh();
        }

        private void Authentication()
        {
            try
            {
                SYS_Menu menu = this.Tag as SYS_Menu;
                if (menu != null)
                {
                    string menuID = menu.MenuID;
                    btnAdd.Enabled = PrivilegeAuthentication.Authentication(menuID, "btnAdd");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            this.Text = NewuGlobal.GetRes("000527");

            groupBox4.Text = NewuGlobal.GetRes("000528");
            groupBox3.Text = NewuGlobal.GetRes("000529");
            groupBox2.Text = NewuGlobal.GetRes("000530");

            btnRefresh.Text = NewuGlobal.GetRes("000531");
            label5.Text = NewuGlobal.GetRes("000532") + ":";

            btnSend.Text = NewuGlobal.GetRes("000533");

            btnContinue.Text = NewuGlobal.GetRes("000534");

            /***********  常见按钮   ***********/
            btnAdd.Text = NewuGlobal.GetRes("000100"); //新增
            btnUpdate.Text = NewuGlobal.GetRes("000101"); //编辑
            btnDelete.Text = NewuGlobal.GetRes("000102"); //删除

            btnClose.Text = NewuGlobal.GetRes("000103"); //关闭
            btnMoveUp.Text = NewuGlobal.GetRes("000109"); //上移
            btnMoveDown.Text = NewuGlobal.GetRes("000110"); //下移

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnRefresh.Padding = new Padding(0, 0, 7, 0);
                btnRefresh.Size = new Size(76, 30);

                btnSend.Size = new Size(90, 30);

                btnContinue.Size = new Size(123, 30);
                btnContinue.Location = new Point(496, 26);

                btnDelete.Padding = new Padding(0, 0, 7, 0);

                btnClose.Location = new Point(645, 26);
                btnClose.Padding = new Padding(0, 0, 7, 0);

                btnMoveUp.Location = new Point(460, 41);
                btnMoveUp.Padding = new Padding(0, 0, 7, 0);
                btnMoveUp.Size = new Size(76, 30);

                btnMoveDown.Location = new Point(550, 41);
                btnMoveDown.Size = new Size(76, 30);
            }
            else
            {
                btnRefresh.Padding = new Padding(0, 0, 0, 0);
                btnRefresh.Size = new Size(85, 30);

                btnSend.Size = new Size(110, 30);

                btnContinue.Size = new Size(145, 30);
                btnContinue.Location = new Point(495, 26);

                btnDelete.Padding = new Padding(0, 0, 0, 0);

                btnClose.Location = new Point(655, 26);
                btnClose.Padding = new Padding(0, 0, 2, 0);

                btnMoveUp.Location = new Point(480, 41);
                btnMoveUp.Padding = new Padding(0, 0, 17, 0);
                btnMoveUp.Size = new Size(105, 30);

                btnMoveDown.Location = new Point(605, 41);
                btnMoveDown.Size = new Size(105, 30);
            }

            int start = 537;
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.GetRes((start + i).ToString("000000"));
                }
        }

        private void GetData()
        {
            try
            {
                string where = " 1=1 and IsStart = 0 and IsDelete = 0 ";
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    where += " and DeviceID='" + cmb_DeviceID.SelectedValue.ToString() + "' ";
                }
                List<PM_OrderTran> pM_OrderTrans = orderTranRepository.GetList(0, where, "SerialNumber asc, Savetime desc");

                dgv.DataSource = pM_OrderTrans;
                dgv.ClearSelection();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = -1;
                if (dgv.CurrentCell != null)
                {
                    rowIndex = dgv.CurrentCell.RowIndex;
                }
                if (rowIndex > 0)
                {
                    string id = dgv["OrderID", dgv.CurrentCell.RowIndex].Value.ToString();
                    FM_PM_OrderTran_Add fm = new FM_PM_OrderTran_Add
                    {
                        nextOrderId = id,
                        Owner = this
                    };
                    fm.ShowDialog();
                }
                else
                {
                    FM_PM_OrderTran_Add fm = new FM_PM_OrderTran_Add
                    {
                        Owner = this
                    };
                    fm.ShowDialog();
                }
                GetData();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        private void BtUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0 && dgv.CurrentCell == null)
                {
                    return;
                }

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string id = dgv["OrderID", dgv.CurrentCell.RowIndex].Value.ToString();
                    //更新或者是编辑时把ID传入构造函数
                    FM_PM_OrderTran_Add fm = new FM_PM_OrderTran_Add(id)
                    {
                        Owner = this
                    };
                    fm.ShowDialog();
                }
                GetData();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0 && dgv.CurrentCell == null)
                {
                    return;
                }

                int rowIndex = dgv.CurrentCell.RowIndex;
                if (rowIndex < 0)
                    return;

                string orderId = dgv["OrderID", rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = false;
                    try
                    {
                        PM_OrderTran pM_OrderTran = orderTranRepository.GetModel(orderId);

                        pM_OrderTran.IsDelete = 1;
                        isAccess = orderTranRepository.Update(pM_OrderTran);
                        if (isAccess)
                        {
                            orderTranRepository.UpdateAllOrderAfterDelete(pM_OrderTran.SerialNumber);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (isAccess)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000173"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000174"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                GetData();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                btnSend.Enabled = false;
                btnClose.Enabled = false;

                #region 自动校秤

                if (NewuGlobal.SoftConfig.AutoScaleCheck)
                {
                    DateTime dt = DateTime.Now;
                    //8-16 中
                    //16-24 晚
                    //24-8 早
                    if (dt.Hour >= 0 && dt.Hour < 8)
                    {
                        dt = DateTime.Parse(dt.ToString("yyyy-MM-dd 00:00:00"));
                    }
                    else if (dt.Hour >= 8 && dt.Hour < 16)
                    {
                        dt = DateTime.Parse(dt.ToString("yyyy-MM-dd 08:00:00"));
                    }
                    else
                    {
                        dt = DateTime.Parse(dt.ToString("yyyy-MM-dd 16:00:00"));
                    }

                    RPT_AutoCheckScaleMixRepository rptRepo = new RPT_AutoCheckScaleMixRepository();
                    ScaleCheckSetRepository scaleChecckRepo = new ScaleCheckSetRepository();
                    var list = scaleChecckRepo.GetByDevice(NewuGlobal.SoftConfig.DeviceCode);
                    bool scaleChecked = false;
                    if (list != null)
                    {
                        scaleChecked = rptRepo.IsCheckScale(NewuGlobal.SoftConfig.DeviceCode, dt, list.Count);
                    }
                    if (!scaleChecked)
                    {
                        MessageBox.Show("按照规定,换班时间必须校秤,请校秤!");
                        btnSend.Enabled = true;
                        btnClose.Enabled = true;
                        return;
                    }
                }

                #endregion 自动校秤

                if (dgv.Rows.Count <= 0 || dgv.CurrentCell == null || dgv.CurrentCell.RowIndex < 0)
                {
                    btnSend.Enabled = true;
                    btnClose.Enabled = true;
                    return;
                }

                int rowIndex = 0;  // 仅仅按照顺序发送
                string orderID = dgv["OrderID", rowIndex].Value.ToString();  // 此值传下，拆分成订单信息

                // 先清空连续配方的标志点  防止本次配方发送完毕  该点有数值  造成的直接下发连续配方
                NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningMixer.WeightContinueRecipe, "0000");
                NewuGlobal.MemMgr.Sync((int)MixerAnalogMiningMixer.MixerContinueRecipe, "0000");

                PM_OrderTran pM_OrderTran = orderTranRepository.GetModel(orderID);

                pbTransState.Maximum = 100;

                //断电的时候此点为0，发送给PLC成功后，PLC置成1。
                if (!ss.Getbool(AddressConst.SendFeedingParam))
                {
                    //发送储斗快慢速
                    List<TB_FeedingParam> tB_FeedingParams = feedingParamRepository.GetModelList("");
                    feedingParamWriteToMem.WriteToMemTransfer(tB_FeedingParams);
                }

                if (coutinuedOrder.SendOrder(pM_OrderTran))
                {
                    MessageBox.Show(NewuGlobal.GetRes("000168"));//"配方发送成功！"
                    Button2_Click(null, null);
                }

                GetData();
                btnSend.Enabled = true;
                btnClose.Enabled = true;
                if (NewuGlobal.SoftConfig.IsContinue)
                    coutinuedOrder.StartContinueOrder(); //开始连续配方

                MyRefresh();
                pM_OrderTran.Reserve5 = "";
                string orderLog = "配方" + pM_OrderTran.MaterialCode + "发送成功" + ",设定：" + pM_OrderTran.SetBatch + "车";
                operateLogRepository.SaveAppLog(orderLog, AppLogType.SendOrderLog);//发送成功后记录日志
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CoutinuedOrder_OnSentFormulaProgress(object sender, FormulaTranEventArgs e)
        {
            try
            {
                if (txtLog.IsDisposed)
                    return;

                if (txtLog.InvokeRequired)
                {
                    EventHandler<FormulaTranEventArgs> d = new EventHandler<FormulaTranEventArgs>(this.CoutinuedOrder_OnSentFormulaProgress);
                    Invoke(d, sender, e);
                }
                else
                {
                    pbTransState.Value = (sender as ContinuedOrder).ProgressValue;
                    string info = e.Message + Environment.NewLine;
                    txtLog.AppendText(info);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            coutinuedOrder.StopContinueOrder();
            MyRefresh();
        }

        private void MyRefresh()
        {
            btnSend.Enabled = !coutinuedOrder.IsSerise;
            btnContinue.Enabled = coutinuedOrder.IsSerise;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            GetData();
            MyRefresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            coutinuedOrder.OnSentFormulaProgress -= CoutinuedOrder_OnSentFormulaProgress;
            base.OnClosing(e);
        }

        /// <summary>
        /// 上移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentRow == null)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000208")); //"请在列表中选中一个配方!"
                    return;
                }
                if (dgv.CurrentRow.Index == 0 || dgv.RowCount == 0)
                {
                    return;
                }

                PM_OrderTran previousorder = orderTranRepository.GetModel(dgv.Rows[dgv.CurrentRow.Index - 1].Cells["OrderID"].Value.ToString());
                PM_OrderTran noworder = orderTranRepository.GetModel(dgv.Rows[dgv.CurrentRow.Index].Cells["OrderID"].Value.ToString());

                int selectionIdx = dgv.CurrentRow.Index - 1;

                int tempSerialNum = previousorder.SerialNumber;

                previousorder.SerialNumber = noworder.SerialNumber;

                noworder.SerialNumber = tempSerialNum;

                orderTranRepository.Update(previousorder);
                orderTranRepository.Update(noworder);

                GetData();
                dgv.Rows[selectionIdx].Selected = true;
                dgv.CurrentCell = dgv.Rows[selectionIdx].Cells["Lot"];
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 下移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentRow == null)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000208"));//"请在列表中选中一个配方!"
                    return;
                }
                if (dgv.CurrentRow.Index == dgv.RowCount - 1 || dgv.RowCount == 0)
                {
                    return;
                }

                PM_OrderTran nextorder = orderTranRepository.GetModel(dgv.Rows[dgv.CurrentRow.Index + 1].Cells["OrderID"].Value.ToString());
                PM_OrderTran noworder = orderTranRepository.GetModel(dgv.CurrentRow.Cells["OrderID"].Value.ToString());

                int selectionIdx = dgv.CurrentRow.Index + 1;

                int tempSerialNum = nextorder.SerialNumber;

                nextorder.SerialNumber = noworder.SerialNumber;

                noworder.SerialNumber = tempSerialNum;

                orderTranRepository.Update(nextorder);
                orderTranRepository.Update(noworder);

                GetData();
                dgv.Rows[selectionIdx].Selected = true;
                dgv.CurrentCell = dgv.Rows[selectionIdx].Cells["Lot"];
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaTransfer").Error(ex.ToString());
            }
        }
    }
}