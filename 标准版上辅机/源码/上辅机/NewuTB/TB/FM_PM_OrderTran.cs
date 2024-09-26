using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_PM_OrderTran : Form, ILanguageChanged
    {
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();

        public FM_PM_OrderTran()
        {
            InitializeComponent();
        }

        private void FM_PM_OrderTran_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);

            cmb_DeviceID.DataSource = deviceRepository.GetList("");
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.ValueMember = "DeviceID";

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("OrderID","订单ID", ColumnType.txt,false),
                new ColStruct("DeviceID","设备ID", ColumnType.txt,false),
                new ColStruct("DeviceName","设备名称", ColumnType.txt,true),
                new ColStruct("MaterialID","配方ID",ColumnType.txt,false),
                new ColStruct("MaterialCode","配方名称", ColumnType.txt,true),
                new ColStruct("VersionNo","配方版本"),
                new ColStruct("SerialNumber","优先级"),
                new ColStruct("Lot","批号"),
                new ColStruct("SetBatch","设定车数"),
                new ColStruct("IsStart","启动状态",ColumnType.cmb,true),
                new ColStruct("StartUserID","启动用户",ColumnType.txt,false),
                new ColStruct("StartUserCode","用户编码"),
                new ColStruct("WorkGroup","班组"),
                new ColStruct("WorkOrder","班次"),
                new ColStruct("Savetime","保存时间"),
                new ColStruct("StartTime","启动时间"),
                new ColStruct("EndTime","结束时间")
            };
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmb_isStart.DataSource = orderTranRepository.GetOrderStartState();
            cmb_isStart.DisplayMember = "names";
            cmb_isStart.ValueMember = "values";
            cmb_isStart.SelectedIndex = 0;

            DataGridViewComboBoxColumn dgvIsStart = (DataGridViewComboBoxColumn)dgv.Columns["IsStart"];
            dgvIsStart.ValueMember = "values";
            dgvIsStart.DisplayMember = "names";
            dgvIsStart.DataSource = cmb_isStart.DataSource;

            GetData();
            SetControlLanguageText();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FM_PM_OrderTran_Add fm = new FM_PM_OrderTran_Add
            {
                Owner = this
            };
            fm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);
            cmb_isStart.SelectedIndex = 0;
            GetData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
                return;

            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex < 0)
                return;

            string orderId = dgv["OrderID", rowIndex].Value.ToString();
            int isStart = (int)dgv["IsStart", rowIndex].Value;
            if (isStart == 2 || isStart == 1)
            {
                MessageBox.Show("不能删除已关闭和已启用的订单,因为报表会用到该订单", NewuGlobal.GetRes("000170"));
                return;
            }
            DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (isDel == DialogResult.Yes)
            {
                try
                {
                    PM_OrderTran orderTranModel = orderTranRepository.GetModel(orderId);
                    orderTranModel.IsDelete = 1;
                    bool isAccess = orderTranRepository.Update(orderTranModel);
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void GetData()
        {
            string where = "1=1";
            where += " and Savetime>='" + ComDate.MinDate(startTime.Value) + "' ";
            where += " and Savetime<='" + ComDate.MaxDate(endTime.Value) + "'";
            where += " and IsStart= " + cmb_isStart.SelectedValue;
            where += " and  IsDelete = 0";
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                where += " and DeviceID='" + cmb_DeviceID.SelectedValue.ToString() + "' ";
            }
            dgv.DataSource = orderTranRepository.GetList(0, where, "Savetime desc,SerialNumber desc");
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/

            groupBox2.Text = NewuGlobal.GetRes("000298");  //查询条件
            groupBox1.Text = NewuGlobal.GetRes("000299");  //订单信息列表
            label1.Text = NewuGlobal.GetRes("000300") + ":";// *设备名称
            label3.Text = NewuGlobal.GetRes("000301") + ":";// *开始时间
            label2.Text = NewuGlobal.GetRes("000302") + ":";// *结束时间
            label4.Text = NewuGlobal.GetRes("000303") + ":";// *启动状态
            /***********  常见按钮   ***********/
            btnAdd.Text = NewuGlobal.GetRes("000100"); //新增
            button1.Text = NewuGlobal.GetRes("000102"); //删除
            btnClose.Text = NewuGlobal.GetRes("000103");//关闭
            btnQuery.Text = NewuGlobal.GetRes("000104"); //查询
            btnReset.Text = NewuGlobal.GetRes("000105"); //重置
            this.Text = NewuGlobal.GetRes("000299"); //订单信息
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnQuery.Padding = new Padding(0, 0, 7, 0);
                button1.Padding = new Padding(0, 0, 7, 0);
                btnClose.Padding = new Padding(0, 0, 7, 0);
                btnReset.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnQuery.Padding = new Padding(0, 0, 0, 0);
                button1.Padding = new Padding(0, 0, 0, 0);
                btnClose.Padding = new Padding(0, 0, 0, 0);
                btnReset.Padding = new Padding(0, 0, 0, 0);
            }
            int start = 305;
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.GetRes((start + i).ToString("000000"));
                }

            /***********  常见文字   ***********/
        }
    }
}