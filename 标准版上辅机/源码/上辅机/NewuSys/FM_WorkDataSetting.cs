using Repository.GlobalConfig;
using System;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_WorkDataSetting : Form
    {
        public FM_WorkDataSetting()
        {
            InitializeComponent();
        }

        private void FM_WorkDataSetting_Load(object sender, EventArgs e)
        {
            lb_GroupSet.Text = NewuGlobal.LanguagResourceManager.GetString("000317") + ":";
            lb_OrderSet.Text = NewuGlobal.LanguagResourceManager.GetString("000318") + ":";
            tb_GroupSet.Text = NewuGlobal.SoftConfig.WorkGroupSet;
            tb_OrderSet.Text = NewuGlobal.SoftConfig.WorkOrderSet;
            label1.Text = NewuGlobal.LanguagResourceManager.GetString("000170") + ":";
            label2.Text = NewuGlobal.LanguagResourceManager.GetString("000812");
            btnSave.Text = NewuGlobal.LanguagResourceManager.GetString("000108");
            btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_GroupSet.Text) && !string.IsNullOrEmpty(tb_OrderSet.Text))
            {
                NewuGlobal.SoftConfig.SetWorkSetting(tb_GroupSet.Text, tb_OrderSet.Text);
                MessageBox.Show(NewuGlobal.LanguagResourceManager.GetString("000171"));
            }
            else
                MessageBox.Show(NewuGlobal.LanguagResourceManager.GetString("000172"));
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}