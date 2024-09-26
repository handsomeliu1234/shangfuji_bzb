using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MultiLanguage;
using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;

namespace NewuSys
{
    public partial class FM_SYS_WorkShop_Add : Form
    {
        private SYS_WorkShop workShopModel;

        private SYS_FactoryRepository factoryRepository = new SYS_FactoryRepository();
        private readonly SYS_WorkShopRepository workShopRepository = new SYS_WorkShopRepository();

        private string workshopID = "";

        public FM_SYS_WorkShop_Add()
        {
            InitializeComponent();
        }

        public FM_SYS_WorkShop_Add(string _WorkshopID)
        {
            InitializeComponent();
            workshopID = _WorkshopID;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (workShopModel == null)
                workShopModel = new SYS_WorkShop();

            workShopModel.ShopName = txt_ShopName.Text.Trim();
            workShopModel.WorkshopCode = txt_WorkshopCode.Text.Trim();
            workShopModel.WorkshopJaneSpell = txt_WorkshopJaneSpell.Text.Trim();
            if (cmb_FactoryID.SelectedIndex >= 0)
            {
                workShopModel.FactoryID = cmb_FactoryID.SelectedValue.ToString();
            }
            else
            {
                workShopModel.FactoryID = "";
            }
            workShopModel.SaveTime = DateTime.Now;

            if (DataVerification() == false)
                return;

            bool isAccess;
            if (string.IsNullOrEmpty(workShopModel.WorkshopID))
            {
                isAccess = workShopRepository.Add(workShopModel);
            }
            else
            {
                isAccess = workShopRepository.Update(workShopModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show("工厂信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(workShopModel.WorkshopID))
                    ClearControl();
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("工厂信息保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_WorkShop fm = obj as FM_SYS_WorkShop;
                fm.GetData();
            }
        }

        private void FM_SYS_WorkShop_Add_Load(object sender, EventArgs e)
        {
            SetLanguageChanged();
            List<SYS_Factory> list = factoryRepository.GetList("");
            cmb_FactoryID.DataSource = list;
            cmb_FactoryID.ValueMember = "FactoryID";
            cmb_FactoryID.DisplayMember = "FactoryName";

            if (workshopID != "")
            {
                workShopModel = workShopRepository.GetModel(workshopID);

                txt_ShopName.Text = workShopModel.ShopName;
                txt_WorkshopCode.Text = workShopModel.WorkshopCode;
                txt_WorkshopJaneSpell.Text = workShopModel.WorkshopJaneSpell;
                cmb_FactoryID.SelectedValue = workShopModel.FactoryID;
            }
        }

        private void ClearControl()
        {
            txt_ShopName.Text = "";
            txt_WorkshopCode.Text = "";
            txt_WorkshopJaneSpell.Text = "";
            cmb_FactoryID.SelectedIndex = -1;
        }

        private bool DataVerification()
        {
            if (workShopModel.WorkshopCode == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000426") + NewuGlobal.GetRes("000176"));
                return false;
            }
            if (workShopModel.ShopName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000431") + NewuGlobal.GetRes("000176"));
                return false;
            }
            if (workShopModel.FactoryID == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000434") + NewuGlobal.GetRes("000176"));
                return false;
            }
            return true;
        }

        public void SetLanguageChanged()
        {
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            label5.Text = NewuGlobal.GetRes("000162");
            label6.Text = NewuGlobal.GetRes("000162");
            label8.Text = NewuGlobal.GetRes("000162");

            label1.Text = NewuGlobal.GetRes("000425") + "：";
            label2.Text = NewuGlobal.GetRes("000426") + "：";
            label3.Text = NewuGlobal.GetRes("000427") + "：";
            label4.Text = NewuGlobal.GetRes("000424") + "：";
            groupBox1.Text = NewuGlobal.GetRes("000430");
            this.Text = NewuGlobal.GetRes("000429");
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