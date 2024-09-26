using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_AppEvent : Form
    {
        private List<RPT_AppEvent> list;
        private RPT_AppEventRepository appEventRepository = new RPT_AppEventRepository();
        private SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();

        public FM_RPT_AppEvent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 对下拉框赋值，并写列名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_RPT_AppEvent_Load(object sender, EventArgs e)
        {
            cmb_AppEvent.DataSource = deviceRepository.GetList("");
            cmb_AppEvent.DisplayMember = "DeviceCode";
            cmb_AppEvent.ValueMember = "DeviceCode";
            cmb_AppEvent.SelectedIndex = -1;
            string[] com = new string[5] { "用户登录", "用户登出", "程序运行开始", "程序运行结束", "交接班" };
            comboBox1.Items.AddRange(com);
            comboBox1.SelectedIndex = -1;
            ColStruct[] Cols = new ColStruct[]{
                new ColStruct ("AppEventID","事件ID",ColumnType .txt  ,false ),
                new ColStruct ("AppEventType","事件类型"),
                new ColStruct ("OrderID","订单ID",ColumnType .txt  ,false),
                new ColStruct ("DeviceCode","设备编码"),
                new ColStruct ("MaterialCode","物料编码"),
                new ColStruct ("VersionNo","版本号"),
                new ColStruct ("Lot","批号"),
                new ColStruct ("PlanQty","计划车数"),
                new ColStruct ("FactOrder","实际顺序"),
                new ColStruct ("SaveTime","保存时间"),
                new ColStruct ("UserID","用户ID",ColumnType .txt  ,false),
                new ColStruct ("SaveRealName","保存用户"),
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(Cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetLanguage();
            GetDataSet();
        }

        private void SetLanguage()
        {
            Text = NewuGlobal.GetRes("000780");
            groupBox2.Text = NewuGlobal.GetRes("000189");
            groupBox1.Text = NewuGlobal.GetRes("000553");
            label1.Text = NewuGlobal.GetRes("000182") + "：";
            label2.Text = NewuGlobal.GetRes("000781") + "：";
            label3.Text = NewuGlobal.GetRes("000301") + "：";
            label4.Text = NewuGlobal.GetRes("000302") + "：";
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

            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000781");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000346");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000347");
            dgv.Columns[5].HeaderText = NewuGlobal.GetRes("000621");
            dgv.Columns[6].HeaderText = NewuGlobal.GetRes("000623");
            dgv.Columns[7].HeaderText = NewuGlobal.GetRes("000333");
            dgv.Columns[8].HeaderText = NewuGlobal.GetRes("000541");
            dgv.Columns[9].HeaderText = NewuGlobal.GetRes("000081");
            dgv.Columns[11].HeaderText = NewuGlobal.GetRes("000186");
        }

        /// <summary>
        /// 根据条件，拼接sql语句。
        /// 获得list
        /// </summary>
        private void GetDataSet()
        {
            string sql = " 1 = 1 ";

            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            DateTime startTime = ComDate.MinDate(dateTimePicker1.Value);
            DateTime endTime = ComDate.MaxDate(dateTimePicker2.Value);

            if (cmb_AppEvent.SelectedIndex > -1)
            {
                string cmb = cmb_AppEvent.SelectedValue.ToString();
                if (cmb != "")
                {
                    sql += "and DeviceCode='" + cmb + "'";
                }
            }
            if (comboBox1.SelectedIndex > -1)
            {
                string com = comboBox1.SelectedItem.ToString();
                if (com != "")
                {
                    string com1 = "";
                    if (com == "用户登录")
                    {
                        com1 = "UserLogin";
                    }
                    if (com == "用户登出")
                    {
                        com1 = "SystemLogOut";
                    }
                    if (com == "程序运行开始")
                    {
                        com1 = "AppRun";
                    }
                    if (com == "程序运行结束")
                    {
                        com1 = "AppStop";
                    }
                    if (com == "交接班")
                    {
                        com1 = "WorkShiftChange";
                    }

                    sql += "and AppEventType='" + com1 + "'";
                }
            }
            if (startTime != null && endTime != null)
            {
                sql += "and  SaveTime >='" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and SaveTime<='" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }

            list = appEventRepository.GetList(sql);
            Listget();
        }

        /// <summary>
        /// 将list遍历并将值给dgv
        /// </summary>
        private void Listget()
        {
            foreach (var AET in list)
            {
                Object[] obj = new object[20];
                obj[0] = AET.AppEventID;
                if (AET.AppEventType == "UserLogin")
                { obj[1] = "用户登录"; }

                if (AET.AppEventType == "SystemLogOut")
                { obj[1] = "用户登出"; }

                if (AET.AppEventType == "AppRun")
                { obj[1] = "程序运行开始"; }

                if (AET.AppEventType == "AppStop")
                { obj[1] = "程序运行结束"; }

                if (AET.AppEventType == "WorkShiftChange")
                { obj[1] = "交接班"; }

                obj[2] = AET.OrderID;
                obj[3] = AET.DeviceCode;
                obj[4] = AET.MaterialCode;
                obj[5] = AET.VersionNo;
                obj[6] = AET.Lot;
                obj[7] = AET.PlanQty;
                obj[8] = AET.FactOrder;
                obj[9] = AET.SaveTime;
                obj[10] = AET.UserID;
                obj[11] = AET.SaveRealName;
                obj[12] = AET.Reserve1;
                obj[13] = AET.Reserve2;
                obj[14] = AET.Reserve3;
                obj[15] = AET.Reserve4;
                obj[16] = AET.Reserve5;

                dgv.Rows.Add(obj);
            }
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            GetDataSet();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_AppEvent.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = ComDate.MinDate(DateTime.Now);
            dateTimePicker2.Value = ComDate.MaxDate(DateTime.Now);
            dgv.Rows.Clear();
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
    }
}