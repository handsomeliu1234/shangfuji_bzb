using MultiLanguage;
using Newu;
using NewuCommon;
using NewuView;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_PM_OrderTran : Form, ILanguageChanged
    {
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private RPT_WeightRepository weightRepository;
        private RPT_MixStepRepository mixStepRepository;
        private PM_OrderTran orderTran;

        public FM_PM_OrderTran()
        {
            InitializeComponent();
        }

        private void FM_PM_OrderTran_Load(object sender, EventArgs e)
        {
            orderDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            weightDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);

            //设备名称
            cmb_Device.DataSource = deviceRepository.GetList("");
            cmb_Device.DisplayMember = "DeviceName";
            cmb_Device.ValueMember = "DeviceID";
            cmb_Device.SelectedIndex = -1;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("OrderID","订单ID", ColumnType.txt,false),
                new ColStruct("DeviceID","所属设备", ColumnType.txt,false),
                new ColStruct("DeviceName","设备名称", ColumnType.txt,true),
                new ColStruct("MaterialID","物料",ColumnType.txt,false),
                new ColStruct("MaterialCode","物料编码", ColumnType.txt,true),
                new ColStruct("VersionNo","配方版本"),
                new ColStruct("FormulaHostoryID","版本序号",ColumnType.txt,false),
                new ColStruct("OrderFrom","订单来源"),
                new ColStruct("SerialNumber","序号"),
                new ColStruct("Lot","批号"),
                new ColStruct("SetBatch","设定车数"),
                new ColStruct("IsStart","是否启动",ColumnType.chk,true),
                new ColStruct("IsDelete","是否删除",ColumnType.chk,true),
                new ColStruct("StartUserID","启动用户",ColumnType.txt,false),
                new ColStruct("StartUserCode","用户编码"),
                new ColStruct("WorkGroup","班组"),
                new ColStruct("WorkOrder","班次"),
                new ColStruct("Savetime","保存时间"),
                new ColStruct("StartTime","启动时间"),
                new ColStruct("EndTime","结束时间")
            };
            orderDgv.AllowUserToAddRows = false;
            orderDgv.ReadOnly = true;
            orderDgv.AddCols(cols);
            orderDgv.Columns["Savetime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            orderDgv.Columns["StartTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            orderDgv.Columns["EndTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            orderDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            orderDgv.Columns["WorkGroup"].Width = 50;
            orderDgv.Columns["WorkOrder"].Width = 50;

            ColStruct[] cols1 = new ColStruct[]{
                new ColStruct("OrderID","订单ID", ColumnType.txt,false),
                new ColStruct("MaterialCode","配方名称", ColumnType.txt,true),
                new ColStruct("FactOrder","生产车数", ColumnType.txt,true)
            };

            mixBatchDgv.AllowUserToAddRows = false;
            mixBatchDgv.ReadOnly = true;
            mixBatchDgv.AddCols(cols1);

            ColStruct[] cols2 = new ColStruct[]{
                new ColStruct("MaterialCode","配方名称", ColumnType.txt,false),
                new ColStruct("TypeCodeName","物料类型"),
                new ColStruct("SetMaterialCode","物料编码"),
                new ColStruct("VersionNo","版本号"),
                new ColStruct("OrderID","订单ID", ColumnType.txt,false),
                new ColStruct("Lot","批号"),
                new ColStruct("PlanQty","计划车数"),
                new ColStruct("DropOrder","投料顺序"),
                new ColStruct("WeightOrder","称量顺序"),
                new ColStruct("SetWeight","设定重量"),
                new ColStruct("AllowError","允许误差"),
                new ColStruct("ActWeight","实际重量"),
                new ColStruct("ActError","实际误差"),
                new ColStruct("SaveTime","保存时间"),
            };

            weightDgv.AllowUserToAddRows = false;
            weightDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            weightDgv.ReadOnly = true;
            weightDgv.AddCols(cols2);
            weightDgv.Columns["SaveTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            weightDgv.Columns["SaveTime"].Width = 130;

            ColStruct[] cols3 = new ColStruct[]{
                new ColStruct("MaterialCode","配方名称", ColumnType.txt,false),
                new ColStruct("VersionNo","版本号"),
                new ColStruct("StepName","工艺步骤"),
                new ColStruct("ActionControlName","控制方式"),
                new ColStruct("OrderID","订单ID", ColumnType.txt,false),
                new ColStruct("Lot","批号"),
                new ColStruct("ActTime","时间"),
                new ColStruct("ActTemp","温度"),
                new ColStruct("ActPower","功率"),
                new ColStruct("ActEnergy","能量"),
                new ColStruct("ActPress","压力"),
                new ColStruct("ActSpeed","转速"),
                new ColStruct("SaveTime","保存时间"),
            };

            mixStepDgv.AllowUserToAddRows = false;
            mixStepDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mixStepDgv.ReadOnly = true;
            mixStepDgv.AddCols(cols3);
            mixStepDgv.Columns["ActTime"].DefaultCellStyle.Format = "0";
            mixStepDgv.Columns["ActTemp"].DefaultCellStyle.Format = "0";
            mixStepDgv.Columns["ActPower"].DefaultCellStyle.Format = "0";
            mixStepDgv.Columns["ActEnergy"].DefaultCellStyle.Format = "0.0";
            mixStepDgv.Columns["ActPress"].DefaultCellStyle.Format = "0.0";
            mixStepDgv.Columns["ActSpeed"].DefaultCellStyle.Format = "0";
            mixStepDgv.Columns["SaveTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            SetControlLanguageText();
            if (Authorization.JudgeAuthorization())
            {
                btnQuery.Enabled = true;
                return;
            }
            else
            {
                btnQuery.Enabled = false;
                new FM_About().ShowDialog();
            }
            GetData();
        }

        private void GetData()
        {
            try
            {
                string where = "1=1";
                where += " and Savetime>='" + ComDate.MinDate(startTime.Value) + "' ";
                where += " and Savetime<='" + ComDate.MaxDate(endTime.Value) + "'";
                where += " and IsDelete=0 ";
                if (cmb_Device.SelectedIndex >= 0)
                {
                    where += " and DeviceID='" + cmb_Device.SelectedValue.ToString() + "' ";
                }

                where += "order by StartTime ";
                List<PM_OrderTran> list = orderTranRepository.GetList(where);
                orderDgv.DataSource = list;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_Device.SelectedIndex = -1;
            txtFormula.Text = "";
        }

        private void Alldgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GetMix();
        }

        private void GetMix()
        {
            try
            {
                if (orderDgv.CurrentCell == null)
                    return;
                int row = orderDgv.CurrentCell.RowIndex;
                string orderId = orderDgv.Rows[row].Cells[0].Value.ToString();

                orderTran = orderTranRepository.GetModel(orderId);
                weightRepository = new RPT_WeightRepository();
                mixStepRepository = new RPT_MixStepRepository();

                List<RPT_Weight> rPT_Weights = weightRepository.GetList(orderId, orderTran);
                if (rPT_Weights != null)
                    mixBatchDgv.DataSource = rPT_Weights;

                weightDgv.DataSource = null;
                mixStepDgv.DataSource = null;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void MixBatchDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GetWeigh();
            GetMix2();
        }

        private void GetWeigh()
        {
            try
            {
                if (mixBatchDgv.CurrentCell == null)
                    return;
                int row = mixBatchDgv.CurrentCell.RowIndex;
                string OrderId = mixBatchDgv.Rows[row].Cells[0].Value.ToString();
                int FactOrder = Convert.ToInt32(mixBatchDgv.Rows[row].Cells[2].Value);
                List<RPT_Weight> rPT_Weights = weightRepository.GetList(OrderId, FactOrder, orderTran);
                DisplayData(rPT_Weights);
                weightDgv.DataSource = rPT_Weights;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void DisplayData(List<RPT_Weight> list)
        {
            foreach (RPT_Weight item in list)
            {
                if (item.TypeCodeName == "胶料")
                {
                    item.SetWeight = decimal.Round(item.SetWeight, NewuGlobal.SoftConfig.RubberDigit);
                    item.ActWeight = decimal.Round(item.ActWeight, NewuGlobal.SoftConfig.RubberDigit);
                    item.AllowError = decimal.Round(item.AllowError, NewuGlobal.SoftConfig.RubberDigit);
                    item.ActError = decimal.Round(item.ActError, NewuGlobal.SoftConfig.RubberDigit);
                }
                else if (item.TypeCodeName == "炭黑")
                {
                    item.SetWeight = decimal.Round(item.SetWeight, NewuGlobal.SoftConfig.CarbonDigit);
                    item.ActWeight = decimal.Round(item.ActWeight, NewuGlobal.SoftConfig.CarbonDigit);
                    item.AllowError = decimal.Round(item.AllowError, NewuGlobal.SoftConfig.CarbonDigit);
                    item.ActError = decimal.Round(item.ActError, NewuGlobal.SoftConfig.CarbonDigit);
                }
                else if (item.TypeCodeName == "粉料")
                {
                    item.SetWeight = decimal.Round(item.SetWeight, NewuGlobal.SoftConfig.ZnoDigit);
                    item.ActWeight = decimal.Round(item.ActWeight, NewuGlobal.SoftConfig.ZnoDigit);
                    item.AllowError = decimal.Round(item.AllowError, NewuGlobal.SoftConfig.ZnoDigit);
                    item.ActError = decimal.Round(item.ActError, NewuGlobal.SoftConfig.ZnoDigit);
                }
                else if (item.TypeCodeName == "油料")
                {
                    item.SetWeight = decimal.Round(item.SetWeight, NewuGlobal.SoftConfig.OilDigit);
                    item.ActWeight = decimal.Round(item.ActWeight, NewuGlobal.SoftConfig.OilDigit);
                    item.AllowError = decimal.Round(item.AllowError, NewuGlobal.SoftConfig.OilDigit);
                    item.ActError = decimal.Round(item.ActError, NewuGlobal.SoftConfig.OilDigit);
                }
                else if (item.TypeCodeName == "药品")
                {
                    item.SetWeight = decimal.Round(item.SetWeight, NewuGlobal.SoftConfig.DrugDigit);
                    item.ActWeight = decimal.Round(item.ActWeight, NewuGlobal.SoftConfig.DrugDigit);
                    item.AllowError = decimal.Round(item.AllowError, NewuGlobal.SoftConfig.DrugDigit);
                    item.ActError = decimal.Round(item.ActError, NewuGlobal.SoftConfig.DrugDigit);
                }
            }
        }

        private void GetMix2()
        {
            try
            {
                if (mixBatchDgv.CurrentCell == null)
                    return;

                int row = mixBatchDgv.CurrentCell.RowIndex;
                string OrderId = mixBatchDgv.Rows[row].Cells[0].Value.ToString();
                int FactOrder = Convert.ToInt32(mixBatchDgv.Rows[row].Cells[2].Value);

                string Strwhere = "OrderID = '" + OrderId + "' and FactOrder=" + FactOrder + "order by StepOrder";
                List<RPT_MixStep> rPT_MixSteps = mixStepRepository.GetList(Strwhere, orderTran);
                mixStepDgv.DataSource = rPT_MixSteps;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void BtnReportC_Click(object sender, EventArgs e)
        {
            try
            {
                if (mixBatchDgv.SelectedRows.Count <= 0 && mixBatchDgv.Rows.Count <= 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000644"));
                    return;
                }

                int row = mixBatchDgv.CurrentCell.RowIndex;
                string FactOrder = mixBatchDgv.Rows[index: row].Cells[index: 2].Value.ToString();

                FM_RPT_BatchReportDetail fm = new FM_RPT_BatchReportDetail(orderTran, FactOrder)
                {
                    Owner = this
                };
                fm.ShowDialog();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void Btn_Report_Click(object sender, EventArgs e)
        {
            try
            {
                //"请选择订单后，再查看批报表!"
                if (orderDgv.SelectedRows.Count <= 0 && orderDgv.Rows.Count <= 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000645"));
                    return;
                }
                if (mixBatchDgv.SelectedRows.Count <= 0 && mixBatchDgv.Rows.Count <= 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000645"));
                    return;
                }
                FM_RPT_BatchReport_FastReport fastReport = new FM_RPT_BatchReport_FastReport(orderTran);
                fastReport.ShowDialog();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                // tododown: 正版授权
                if (Authorization.JudgeAuthorization() == false)
                {
                    //到期 无法查看 "该软件未经正版授权，暂关闭此功能，请联系设备供应商获取正版支持！"
                    MessageBox.Show(NewuGlobal.GetRes("000646"));
                    new FM_About().Show();
                    return;
                }

                //"请选择订单车数后，再查看曲线！"
                if (orderDgv.SelectedRows.Count <= 0 && orderDgv.Rows.Count <= 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000645"));
                    return;
                }

                //"请选择密炼车次后，再查看曲线！"
                if (mixBatchDgv.SelectedRows.Count <= 0 && mixBatchDgv.Rows.Count <= 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000645"));
                    return;
                }

                int row = mixBatchDgv.CurrentCell.RowIndex;
                string OrderId = mixBatchDgv.Rows[row].Cells[0].Value.ToString();
                string FactOrder = mixBatchDgv.Rows[row].Cells[2].Value.ToString();
                FM_MixCurve_Super_Super fm = new FM_MixCurve_Super_Super(OrderId, FactOrder, orderTran)
                {
                    Owner = this
                };
                fm.Show();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFormula_KeyUp(object sender, KeyEventArgs e)
        {
            GetFormulaList();
        }

        private void GetFormulaList()
        {
            try
            {
                string where = "1=1";
                where += " and Savetime>='" + ComDate.MinDate(startTime.Value) + "' ";
                where += " and Savetime<='" + ComDate.MaxDate(endTime.Value) + "'";
                if (txtFormula.Text != "")
                {
                    where += " and MaterialCode like '%" + txtFormula.Text + "%' ";
                }
                where += " and IsDelete=0 ";
                if (cmb_Device.SelectedIndex >= 0)
                {
                    where += " and DeviceID='" + cmb_Device.SelectedValue.ToString() + "' ";
                }
                where += "order by StartTime ";
                List<PM_OrderTran> list = orderTranRepository.GetList(where);
                orderDgv.DataSource = list;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran").Error(ex.ToString());
            }
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/

            groupBox1.Text = NewuGlobal.GetRes("000581");  //
            groupBox2.Text = NewuGlobal.GetRes("000582");  //
            groupBox3.Text = NewuGlobal.GetRes("000583");  //
            groupBox4.Text = NewuGlobal.GetRes("000584");  //
            groupBox6.Text = NewuGlobal.GetRes("000585");  //
            groupBox5.Text = NewuGlobal.GetRes("000586");  //

            label1.Text = NewuGlobal.GetRes("000588") + ":";// *
            label2.Text = NewuGlobal.GetRes("000589") + ":";// *
            label5.Text = NewuGlobal.GetRes("000590") + ":";// *
            label3.Text = NewuGlobal.GetRes("000201") + ":";
            //label1.Text = NewuGlobal.GetRes("000361") + "：";

            btn_Weight.Text = NewuGlobal.GetRes("000593");
            btnCar.Text = NewuGlobal.GetRes("000594");
            btn_Curve.Text = NewuGlobal.GetRes("000595");
            btnCarLot.Text = NewuGlobal.GetRes("000596");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnCarLot.Size = btn_Weight.Size = btnCar.Size = btn_Curve.Size = new Size(120, 30);
                btnCarLot.TextAlign = ContentAlignment.MiddleCenter;
                btn_Weight.Padding = btnCar.Padding = new Padding(0, 0, 0, 0);
                btn_Curve.Padding = new Padding(0, 0, 13, 0);
                btnQuery.Padding = btnReset.Padding = btn_Close.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnCarLot.Size = btn_Weight.Size = btnCar.Size = btn_Curve.Size = new Size(135, 30);
                btnCarLot.TextAlign = ContentAlignment.MiddleRight;
                btnCar.Padding = new Padding(5, 0, 0, 0);
                btn_Weight.Padding = new Padding(5, 0, 5, 0);
                btn_Curve.Padding = new Padding(5, 0, 10, 0);
                btnQuery.Padding = btnReset.Padding = btn_Close.Padding = new Padding(0, 0, 0, 0);
            }

            /***********  常见按钮   ***********/

            btnQuery.Text = NewuGlobal.GetRes("000104");//查询
            btnReset.Text = NewuGlobal.GetRes("000105");//重置
            btn_Close.Text = NewuGlobal.GetRes("000103");// 关闭

            orderDgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000300");//设备名称
            orderDgv.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000347");//物料编码
            orderDgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000230");//配方版本
            orderDgv.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000356");//订单来源
            orderDgv.Columns[8].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000330");//序号
            orderDgv.Columns[9].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000312");//批号
            orderDgv.Columns[10].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000604");//设定车数
            orderDgv.Columns[11].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000606");//是否启动
            orderDgv.Columns[12].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000607");//是否删除
            orderDgv.Columns[14].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000316");//用户编码
            orderDgv.Columns[15].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000317");//班组
            orderDgv.Columns[16].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000318");//班次
            orderDgv.Columns[17].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000319");//保存时间
            orderDgv.Columns[18].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000320");//启动时间
            orderDgv.Columns[19].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000321");//结束时间

            mixBatchDgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000616");//配方名称
            mixBatchDgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000617");//生产车数

            weightDgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000619");//物料类型
            weightDgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000620");//物料编码
            weightDgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000621");//版本号
            weightDgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000623");//批号
            weightDgv.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000333");//计划车数
            weightDgv.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000624");//投料顺序
            weightDgv.Columns[8].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000625");//称量顺序
            weightDgv.Columns[9].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000626");//设定重量
            weightDgv.Columns[10].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000627");//允许误差
            weightDgv.Columns[11].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000628");//实际重量
            weightDgv.Columns[12].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000629");//实际误差
            weightDgv.Columns[13].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000641");//保存时间

            mixStepDgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000621");//版本号
            mixStepDgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000633");//工艺步骤
            mixStepDgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000634");//控制方式
            mixStepDgv.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000312");//批号
            mixStepDgv.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000635");//时间
            mixStepDgv.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000636");//温度
            mixStepDgv.Columns[8].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000637");//功率
            mixStepDgv.Columns[9].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000638");//能量
            mixStepDgv.Columns[10].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000639");//压力
            mixStepDgv.Columns[11].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000640");//转速
            mixStepDgv.Columns[12].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000641");//保存时间
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void BtnCarLot_Click(object sender, EventArgs e)
        {
            if (mixBatchDgv.SelectedRows.Count <= 0 && mixBatchDgv.Rows.Count <= 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000644"));
                return;
            }

            FM_RPT_LotReport_FastReport fM_RPT_LotReport_FastReport = new FM_RPT_LotReport_FastReport(orderTran)
            {
                Owner = this
            };
            fM_RPT_LotReport_FastReport.ShowDialog();
        }
    }
}