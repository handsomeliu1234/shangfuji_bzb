using MultiLanguage;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_MaterialStatistics : Form, ILanguageChanged
    {
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private readonly RPT_WeightRepository weightRepository = new RPT_WeightRepository();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();

        public FM_MaterialStatistics()
        {
            InitializeComponent();
        }

        private void FM_MaterialStatistics_Load(object sender, EventArgs e)
        {
            InitTime();
            InitDevice();
            InitDgv();
            SetLanguage();
        }

        private void InitDevice()
        {
            string _typeCodeName1 = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T母炼配方);
            string _typeCodeID1 = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName1);
            string _typeCodeName2 = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T终炼配方);
            string _typeCodeID2 = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName2);
            string strWhere = " TypeCodeID in('" + _typeCodeID1 + "','" + _typeCodeID2 + "')";

            cmb_Formula.DataSource = formulaMaterialRepository.GetList(0, strWhere, " MaterialCode");
            cmb_Formula.DisplayMember = "MaterialCode";
            cmb_Formula.ValueMember = "MaterialID";
            cmb_Formula.SelectedIndex = -1;
        }

        private void InitTime()
        {
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            startTime.Value = ComDate.MinDate(DateTime.Now);
            endTime.Value = ComDate.MaxDate(DateTime.Now);
        }

        private void InitDgv()
        {
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("TypeCodeName","物料类型"),
                new ColStruct("SetMaterialCode","物料名称"),
                new ColStruct("SetWeight","设定重量"),
                new ColStruct("ActWeight","称量重量")
            };

            dgg.AllowUserToAddRows = false;
            dgg.AddCols(cols);
            dgg.ReadOnly = true;
            dgg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void GetData()
        {
            try
            {
                DateTime start_time = startTime.Value;
                DateTime end_time = endTime.Value;

                string strSQL = " SaveTime >= '" + ComDate.MinDate(start_time) + "' and SaveTime <= '" + ComDate.MaxDate(end_time) + "'";
                if (cmb_Formula.Text != "")
                {
                    strSQL += " and MaterialCode like '%" + cmb_Formula.Text + "%' ";
                }
                if (cmbTypeCode.SelectedIndex >= 0)
                {
                    string str = cmbTypeCode.SelectedValue.ToString();
                    strSQL += " and DevicePartCode = '" + str + "' ";
                }

                List<RPT_Weight> rPT_Weights = weightRepository.GetMaterialStatistics(start_time, strSQL);

                if (rPT_Weights == null || rPT_Weights.Count == 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000578")+ NewuGlobal.GetRes("000137"));
                    return;
                }
                dgg.DataSource = rPT_Weights;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_MaterialStatistics").Error(ex.ToString());
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmbTypeCode.SelectedIndex = -1;
            cmb_Formula.SelectedIndex = -1;
            dgg.ClearRow();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            label1.Text = NewuGlobal.GetRes("000301") + ":";
            label2.Text = NewuGlobal.GetRes("000302") + ":";
            label3.Text = NewuGlobal.GetRes("000678") + ":";
            label4.Text = NewuGlobal.GetRes("000212") + ":";
            btnQuery.Text = NewuGlobal.GetRes("000104");
            btnReset.Text = NewuGlobal.GetRes("000105");
            btn_Close.Text = NewuGlobal.GetRes("000103");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btn_Close.Padding = btnReset.Padding = btnQuery.Padding = new Padding(0, 0, 7, 0);
            }
            else
                btn_Close.Padding = btnReset.Padding = btnQuery.Padding = new Padding(0, 0, 0, 0);
            dgg.Columns[0].HeaderText = NewuGlobal.GetRes("000183");
            dgg.Columns[1].HeaderText = NewuGlobal.GetRes("000181");
            dgg.Columns[2].HeaderText = NewuGlobal.GetRes("000264");
            dgg.Columns[3].HeaderText = NewuGlobal.GetRes("000628");

            List<SYS_DevicePart> list = new List<SYS_DevicePart>();
            //母练
            if (!NewuGlobal.SoftConfig.IsFinalMix())
            {
                list.AddRange(devicePartRepository.GetList("DevicePartCode not in('UpMix_001')"));
            }
            else
            {
                list.AddRange(devicePartRepository.GetList("DevicePartCode in('MixRubberScales_001') "));
            }

            cmbTypeCode.DataSource = list;
            if (NewuGlobal.SupportLanguage.Equals("1"))
                cmbTypeCode.DisplayMember = "Reserve1";
            else
                cmbTypeCode.DisplayMember = "DevicePartName";

            cmbTypeCode.ValueMember = "DevicePartCode";
            cmbTypeCode.SelectedIndex = -1;
        }
    }
}