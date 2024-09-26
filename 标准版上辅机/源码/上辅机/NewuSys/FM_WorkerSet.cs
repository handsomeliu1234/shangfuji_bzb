using MultiLanguage;
using Repository.GlobalConfig;
using System;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_WorkerSet : Form, ILanguageChanged
    {
        public FM_WorkerSet()
        {
            InitializeComponent();
        }

        private void FM_WorkerSet_Load(object sender, EventArgs e)
        {
            cmb_workGroup.SelectedText = NewuGlobal.SoftConfig.WorkGroup;
            cmb_workOrder.SelectedText = NewuGlobal.SoftConfig.WorkOrder;

            this.Text = NewuGlobal.GetRes("000317") + NewuGlobal.GetRes("000809");
            string workGroupSet = NewuGlobal.SoftConfig.WorkGroupSet;
            string workOrderSet = NewuGlobal.SoftConfig.WorkOrderSet;
            string[] strGroupSet = workGroupSet.Split('/');
            string[] strOrderSet = workOrderSet.Split('/');
            cmb_workGroup.DataSource = strGroupSet;
            cmb_workOrder.DataSource = strOrderSet;
            SetLanguage();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            NewuGlobal.SoftConfig.SetWorker(cmb_workGroup.Text, cmb_workOrder.Text);
            MessageBox.Show(NewuGlobal.GetRes("000171"));//保存成功
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            Text = NewuGlobal.GetRes("000824");
            label1.Text = NewuGlobal.GetRes("000317") + ":";
            label2.Text = NewuGlobal.GetRes("000318") + ":";
            btn_Save.Text = NewuGlobal.GetRes("000108");
        }
    }
}