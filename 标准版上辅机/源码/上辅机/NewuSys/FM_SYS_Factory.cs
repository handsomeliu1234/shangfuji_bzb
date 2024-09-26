using MultiLanguage;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_Factory : Form
    {
        private SYS_Factory factoryModel;
        private readonly SYS_FactoryRepository factoryReposity = new SYS_FactoryRepository();

        public FM_SYS_Factory()
        {
            InitializeComponent();

            splitContainer1.IsSplitterFixed = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (factoryModel == null)
            { factoryModel = new SYS_Factory(); }

            factoryModel.FactoryName = txt_FactoryName.Text;
            factoryModel.FactoryCode = txt_FactoryCode.Text;
            factoryModel.FactoryJaneSpell = txt_FactoryJaneSpell.Text;
            factoryModel.FactorySite = txt_FactorySite.Text;
            factoryModel.FactoryPhone = txt_FactoryPhone.Text;
            factoryModel.FactoryEmail = txt_FactoryEmail.Text;
            factoryModel.FactoryAddress = txt_FactoryAddress.Text;
            factoryModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
            {
                return;
            }

            bool isAccess;
            if (string.IsNullOrEmpty(factoryModel.FactoryID))
            {
                isAccess = factoryReposity.Add(factoryModel);
            }
            else
            {
                isAccess = factoryReposity.Update(factoryModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool DataVerification()
        {
            if (factoryModel.FactoryCode == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000414") + NewuGlobal.GetRes("000176"));
                return false;
            }

            if (factoryModel.FactoryName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000413") + NewuGlobal.GetRes("000176"));
                return false;
            }
            return true;
        }

        private void FM_SYS_Factory_Load(object sender, EventArgs e)
        {
            List<SYS_Factory> factoryList = factoryReposity.GetList("");

            if (factoryList.Count > 0)
            {
                factoryModel = factoryList[0];

                txt_FactoryName.Text = factoryModel.FactoryName;
                txt_FactoryCode.Text = factoryModel.FactoryCode;
                txt_FactoryJaneSpell.Text = factoryModel.FactoryJaneSpell;
                txt_FactorySite.Text = factoryModel.FactorySite;
                txt_FactoryPhone.Text = factoryModel.FactoryPhone;
                txt_FactoryEmail.Text = factoryModel.FactoryEmail;
                txt_FactoryAddress.Text = factoryModel.FactoryAddress;
            }

            SetLanguage();
        }

        private void SetLanguage()
        {
            Text = NewuGlobal.GetRes("000410");
            btnClose.Text = NewuGlobal.GetRes("000103");
            btnSave.Text = NewuGlobal.GetRes("000108");
            groupBox1.Text = NewuGlobal.GetRes("000411");
            groupBox2.Text = NewuGlobal.GetRes("000412");
            label1.Text = NewuGlobal.GetRes("000413") + ":";
            label2.Text = NewuGlobal.GetRes("000414") + ":";
            label3.Text = NewuGlobal.GetRes("000415") + ":";
            label4.Text = NewuGlobal.GetRes("000416") + ":";
            label5.Text = NewuGlobal.GetRes("000417") + ":";
            label6.Text = NewuGlobal.GetRes("000418") + ":";
            label7.Text = NewuGlobal.GetRes("000419") + ":";
            label8.Text = NewuGlobal.GetRes("000162");
            label9.Text = NewuGlobal.GetRes("000162");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }
    }
}