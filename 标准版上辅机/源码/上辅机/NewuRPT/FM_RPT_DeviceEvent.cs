using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Repository;
using System;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_DeviceEvent : Form, ILanguageChanged
    {
        private readonly RPT_DeviceEventRepository deviceEventRepository = new RPT_DeviceEventRepository();

        public FM_RPT_DeviceEvent()
        {
            InitializeComponent();
        }

        private void FM_RPT_DeviceEvent_Load(object sender, EventArgs e)
        {
            cmb_DeviceEvent.DataSource = deviceEventRepository.GetList("", startTime.Value);
            cmb_DeviceEvent.DisplayMember = "DeviceCode";
            cmb_DeviceEvent.ValueMember = "DeviceEventID";
            cmb_DeviceEvent.SelectedIndex = -1;

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("DeviceEventID","设备事件ID", ColumnType.txt,false),
                new ColStruct("DeviceCode","设备编号"),
                new ColStruct("EventType","事件类型"),
                new ColStruct("MaterialCode","物料编码"),
                new ColStruct("StartTime","开始时间"),
                new ColStruct("EndTime","结束时间"),
                new ColStruct("UseTime","消耗时间"),
                new ColStruct("PmMode","生产模式"),
                new ColStruct("Reserve1","停机原因"),
            };

            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            GetData();
        }

        public void GetData()
        {
            string sql = " 1=1 ";

            if (cmb_DeviceEvent.SelectedIndex >= 0)
            {
                string cmb_deviceEvent = cmb_DeviceEvent.SelectedValue.ToString();
                if (cmb_deviceEvent != "")
                {
                    sql += " and DeviceEventID='" + cmb_deviceEvent + "'";
                }
            }
            if (text_UseTime.Text.Trim() != "")
            {
                string useTime = text_UseTime.Text.ToString();
                sql += " and UseTime >= '" + useTime + "'";
            }
            dgv.DataSource = deviceEventRepository.GetList(sql, startTime.Value);
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceEvent.SelectedIndex = -1;
            text_UseTime.Text = "";
        }

        private void Text_UseTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

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
            groupBox2.Text = NewuGlobal.GetRes("000340");
            label1.Text = NewuGlobal.GetRes("000182");
            label2.Text = NewuGlobal.GetRes("000301");
            label3.Text = NewuGlobal.GetRes("000302");
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
        }
    }
}