using MultiLanguage;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuRPT
{
    /// <summary>
    /// 称量合格率统计
    /// </summary>
    public partial class FM_RPT_WeightPASSRateNew : Form, ILanguageChanged
    {
        /// <summary>
        /// 注意使用时更新其Year属性 订单操作
        /// </summary>
        private readonly PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();

        private readonly RPT_WeightRepository weightRepository = new RPT_WeightRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();

        /// <summary>
        /// </summary>
        public FM_RPT_WeightPASSRateNew()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_RPT_WeightPASSRate_Load(object sender, EventArgs e)
        {
            //时间初始化
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);

            List<SYS_Device> list = deviceRepository.GetList("");
            cmb_DeviceName.DataSource = list;
            cmb_DeviceName.ValueMember = "DeviceID";
            cmb_DeviceName.DisplayMember = "DeviceName";

            ColStruct[] cols = new ColStruct[]
            {
                new ColStruct("DeviceCode","设备"),
                new ColStruct("MaterialCode","配方"),
                new ColStruct("Lot","批号"),
                new ColStruct("SetMaterialCode","物料名称"),
                new ColStruct("PlanQty","设定车数"),
                new ColStruct("FactOrder","实际车数"),
                new ColStruct("Reserve1","合格车数"),
                new ColStruct("Reserve2","合格率"),
                new ColStruct("Reserve3","开始时间"),
                new ColStruct("Reserve4","结束时间"),
            };

            dgv1.AllowUserToAddRows = false;
            dgv1.AddCols(cols);
            dgv1.ReadOnly = true;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cols = new ColStruct[]
            {
                new ColStruct("TypeCodeName","物料类型"),
                new ColStruct("SetMaterialCode","物料名称"),
                new ColStruct("PlanQty","车数"),
                new ColStruct("FactOrder","合格车数"),
                new ColStruct("Reserve1","合格率"),
            };

            dgv2.AllowUserToAddRows = false;
            dgv2.AddCols(cols);
            dgv2.ReadOnly = true;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            GetData();
            SetLanguage();
        }

        /// <summary>
        /// </summary>
        private async void GetData()
        {
            try
            {
                string deviceid = cmb_DeviceName.SelectedValue as string;
                if (startTime.Value.Year != endTime.Value.Year)
                {
                    endTime.Value = startTime.Value.AddYears(1).AddDays(-startTime.Value.AddYears(1).DayOfYear).AddDays(1).AddSeconds(-1);
                }

                List<PM_OrderTran> pM_OrderTrans = orderTranRepository.GetList(startTime.Value, endTime.Value);
                List<RPT_Weight> rPT_Weights = null;
                List<RPT_Weight> yieldWeights = null;

                List<RPT_Weight> typeCodeList = null;
                List<RPT_Weight> yieldTypeCodeList = null;

                int count = await Task.Run(() =>
                {
                    rPT_Weights = weightRepository.GetList(startTime.Value, endTime.Value);
                    return rPT_Weights.Count;
                });

                await Task.Run(() =>
                 {
                     yieldWeights = weightRepository.GetYieldList(startTime.Value, endTime.Value);
                 });

                int counts = await Task.Run(() =>
                 {
                     typeCodeList = weightRepository.GetListByTypeCodeName(startTime.Value, endTime.Value);
                     return typeCodeList.Count;
                 });

                await Task.Run(() =>
                 {
                     yieldTypeCodeList = weightRepository.GetYieldListByTypeCodeName(startTime.Value, endTime.Value);
                 });

                if (count > 0)
                {
                    foreach (var item in rPT_Weights)
                    {
                        foreach (var yeild in yieldWeights)
                        {
                            if (item.Lot.Equals(yeild.Lot) && item.SetMaterialCode.Equals(yeild.SetMaterialCode))
                            {
                                item.Reserve1 = yeild.Reserve1;//临时作为合格的称量总个数统计变量
                                double yeildBatch = double.Parse(yeild.Reserve1);
                                double result = yeildBatch / double.Parse((item.FactOrder).ToString()) * 100;
                                item.Reserve2 = result.ToString("f2") + "%";
                            }
                        }
                    }

                    foreach (var item in rPT_Weights)
                    {
                        foreach (var orderTran in pM_OrderTrans)
                        {
                            if (item.OrderID.Equals(orderTran.OrderID))
                            {
                                item.Reserve3 = orderTran.StartTime?.ToString("yyyy-MM-dd HH:mm:ss");
                                item.Reserve4 = orderTran.EndTime?.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    }
                    dgv1.DataSource = new BindingSource { DataSource = rPT_Weights };

                    if (counts > 0)
                    {
                        foreach (var item in typeCodeList)
                        {
                            foreach (var model in yieldTypeCodeList)
                            {
                                if (item.SetMaterialCode.Equals(model.SetMaterialCode))
                                {
                                    item.FactOrder = model.FactOrder;
                                    double rate = double.Parse(item.FactOrder.ToString()) / double.Parse(item.PlanQty.ToString());
                                    item.Reserve1 = (rate * 100).ToString("f2") + "%";
                                }
                            }
                        }
                    }
                    dgv2.DataSource = new BindingSource { DataSource = typeCodeList };
                }
                else
                    MessageBox.Show(NewuGlobal.GetRes("000666") + NewuGlobal.GetRes("000137"));
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_RPT_WeightPASSRateNew").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            groupBox1.Text = NewuGlobal.GetRes("000340");
            label1.Text = NewuGlobal.GetRes("000182")+":";
            label2.Text = NewuGlobal.GetRes("000301")+":";
            label3.Text = NewuGlobal.GetRes("000302")+":";
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            btnClose.Text = NewuGlobal.GetRes("000103");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
            }

            dgv1.Columns[0].HeaderText = NewuGlobal.GetRes("000182");
            dgv1.Columns[1].HeaderText = NewuGlobal.GetRes("000212");
            dgv1.Columns[2].HeaderText = NewuGlobal.GetRes("000312");
            dgv1.Columns[3].HeaderText = NewuGlobal.GetRes("000181");
            dgv1.Columns[4].HeaderText = NewuGlobal.GetRes("000313");
            dgv1.Columns[5].HeaderText = NewuGlobal.GetRes("000370");
            dgv1.Columns[6].HeaderText = NewuGlobal.GetRes("000782");
            dgv1.Columns[7].HeaderText = NewuGlobal.GetRes("000783");
            dgv1.Columns[8].HeaderText = NewuGlobal.GetRes("000301");
            dgv1.Columns[9].HeaderText = NewuGlobal.GetRes("000302");

            dgv2.Columns[0].HeaderText = NewuGlobal.GetRes("000183");
            dgv2.Columns[1].HeaderText = NewuGlobal.GetRes("000181");
            dgv2.Columns[2].HeaderText = NewuGlobal.GetRes("000313");
            dgv2.Columns[3].HeaderText = NewuGlobal.GetRes("000782");
            dgv2.Columns[4].HeaderText = NewuGlobal.GetRes("000783");
        }
    }
}