using Repository.GlobalConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_CheckWeightSet : Form
    {
        public FM_CheckWeightSet()
        {
            InitializeComponent();
        }

        private void FM_CheckWeightSet_Load(object sender, EventArgs e)
        {
            tb_WeightSet.Text = NewuGlobal.SoftConfig.WeightSet;
            tb_AllowError.Text = NewuGlobal.SoftConfig.AllowError;
            SetLanguage();
        }

        private void SetLanguage()
        {
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnSave.Text = NewuGlobal.GetRes("000108");
            lb_WeightSet.Text = NewuGlobal.GetRes("000117") + ":";
            lb_AllowError.Text = NewuGlobal.GetRes("000118") + ":";
            label1.Text = NewuGlobal.GetRes("000170") + ":";
            label2.Text = NewuGlobal.GetRes("000812");
            this.Text = NewuGlobal.GetRes("000826");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            NewuGlobal.SoftConfig.SetAutoCheckParam(tb_WeightSet.Text, tb_AllowError.Text);
            MessageBox.Show(NewuGlobal.GetRes("000171"));
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}