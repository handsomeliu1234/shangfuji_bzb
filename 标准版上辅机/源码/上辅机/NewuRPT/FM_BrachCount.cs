using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_BrachCount : Form
    {
        NewuBLL.FormulaMaterialBLL formulaMaterialBll = new NewuBLL.FormulaMaterialBLL();
        private int cnt = 0;
        public FM_BrachCount()
        {
            InitializeComponent();
        }

        private void FM_BrachCount_Load(object sender, EventArgs e)
        {
            InitDGV();
        }
        private void InitDGV()
        {
            NewuCommon.ColStruct[] cols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("MaterialCode","配方名称"),
                new NewuCommon.ColStruct("MaterialCount","生产车数")
            };

            //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            startTime.Value = NewuCommon.ComDate.MinDate(new DateTime(2019, 1, 1, 0, 0, 0));
            GetData();
        }
        private void GetData()
        {
            DateTime start_time = startTime.Value;
            DateTime end_time = endTime.Value;

            string strSQL = " InitTime >= '" + NewuCommon.ComDate.MinDate(start_time) + "' and EndTime <= '" + NewuCommon.ComDate.MaxDate(end_time) + "'";



            DataSet ds = formulaMaterialBll.GetMaterialCount(start_time, strSQL);


            if (ds.Tables.Count == 0)
            {
                if (cnt > 0)
                    MessageBox.Show("当前选择的时间段，未查询到数据！");
                cnt++;
                return;
            }
            dgv.DataSource = ds.Tables[0];

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
