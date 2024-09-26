using MultiLanguage;
using Newu;
using NewuCommon;
using Repository;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_AutoCheck : Form, ILanguageChanged
    {
        private SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private BindingSource bs;   //校秤设定

        public FM_RPT_AutoCheck()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            RPT_AutoCheckScaleMixRepository rptRepo = new RPT_AutoCheckScaleMixRepository();
            string devicecode = "";
            if (cmb_DeviceName.SelectedIndex != -1)
            {
                devicecode = NewuGlobal.DeviceCodeByID(cmb_DeviceName.SelectedValue as string);
            }
            bs.DataSource = rptRepo.GetList(devicecode, startTime.Value, endTime.Value);
            dgv.Update();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceName.SelectedIndex = -1;
        }

        private void FM_RPT_AutoCheck_Load(object sender, EventArgs e)
        {
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
                new ColStruct("ID","ID",ColumnType.txt,false),
                new ColStruct("DeviceCode","设备"),
                new ColStruct("CheckScaleNo","校秤编号"),
                new ColStruct("ScaleName","秤名称"),
                new ColStruct("ScaleWeight","砝码重量"),
                new ColStruct("SetError","允许误差"),
                new ColStruct("RealWeight","实际重量"),
                new ColStruct("Result","是否合格", ColumnType.cmb,true),
                new ColStruct("SaveUser","用户"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewComboBoxColumn colum = dgv.Columns["Result"] as DataGridViewComboBoxColumn;
            BindingSource bs2 = new BindingSource();
            var dic = new Dictionary<bool, string>
            {
                { true, "合格" },
                { false, "不合格" }
            };
            bs2.DataSource = dic;
            colum.DataSource = bs2;
            colum.ValueMember = "Key";
            colum.DisplayMember = "Value";

            bs = new BindingSource();
            dgv.DataSource = bs;
            SetLanguage();
            GetData();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            groupBox1.Text = NewuGlobal.GetRes("000189");
            label1.Text = NewuGlobal.GetRes("000182") + ":";
            label2.Text = NewuGlobal.GetRes("000301") + ":";
            label3.Text = NewuGlobal.GetRes("000302") + ":";
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
            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000307");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000114");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000116");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000117");
            dgv.Columns[5].HeaderText = NewuGlobal.GetRes("000118");
            dgv.Columns[6].HeaderText = NewuGlobal.GetRes("000628");
            dgv.Columns[7].HeaderText = NewuGlobal.GetRes("000113");
            dgv.Columns[8].HeaderText = NewuGlobal.GetRes("000186");
            dgv.Columns[9].HeaderText = NewuGlobal.GetRes("000187");
        }
    }
}