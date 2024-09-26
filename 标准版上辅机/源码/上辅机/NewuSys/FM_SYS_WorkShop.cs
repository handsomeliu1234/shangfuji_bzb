using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_WorkShop : Form, ILanguageChanged
    {
        private readonly SYS_WorkShopRepository workShopRepository = new SYS_WorkShopRepository();
        private readonly SYS_FactoryRepository factoryRepository = new SYS_FactoryRepository();

        public FM_SYS_WorkShop()
        {
            InitializeComponent();
        }

        private void FM_SYS_WorkShop_Load(object sender, EventArgs e)
        {
            List<SYS_Factory> list = factoryRepository.GetList("");
            cmb_FactoryID.DataSource = list;
            cmb_FactoryID.ValueMember = "FactoryID";
            cmb_FactoryID.DisplayMember = "FactoryName";

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("WorkshopID","车间ID", ColumnType.txt,false),
                new ColStruct("FactoryID","所属工厂",ColumnType.cmb,true),
                new ColStruct("ShopName","车间名称"),
                new ColStruct("WorkshopCode","车间编号"),
                new ColStruct("WorkshopJaneSpell","工厂简拼"),
                new ColStruct("SaveTime","保存时间")
            };

            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AddCols(cols);
            dgv.GridColor = SystemColors.ControlDark;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewComboBoxColumn factoryComBox = dgv.Columns["FactoryID"] as DataGridViewComboBoxColumn;
            factoryComBox.DataSource = list;
            factoryComBox.DisplayMember = "FactoryName";
            factoryComBox.ValueMember = "FactoryID";

            GetData();
            SetLanguage();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SYS_WorkShop sYS_WorkShop = new SYS_WorkShop();
            ExcuteData(sYS_WorkShop, true);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, dgv.CurrentCell.RowIndex].Value.ToString();
                SYS_WorkShop sYS_WorkShop = workShopRepository.GetModel(id);
                ExcuteData(sYS_WorkShop, false);
            }
        }

        private void ExcuteData(SYS_WorkShop workShopModel, bool flag)
        {
            try
            {
                workShopModel.ShopName = txt_ShopName.Text.Trim();
                workShopModel.WorkshopCode = txt_WorkshopCode.Text.Trim();
                workShopModel.WorkshopJaneSpell = txt_WorkshopJaneSpell.Text.Trim();
                if (cmb_FactoryID.SelectedIndex >= 0)
                    workShopModel.FactoryID = cmb_FactoryID.SelectedValue.ToString();
                else
                    workShopModel.FactoryID = "";

                workShopModel.SaveTime = DateTime.Now;

                if (!DataVerification(workShopModel))
                    return;

                bool result;

                if (flag)
                    result = workShopRepository.Add(workShopModel);
                else
                    result = workShopRepository.Update(workShopModel);
                if (result)
                {
                    hint.Text = NewuGlobal.GetRes("000171");
                    GetData();
                }
                else
                    hint.Text = NewuGlobal.GetRes("000172");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_WorkShop").Error(ex.ToString());
            }
        }

        private bool DataVerification(SYS_WorkShop workShopModel)
        {
            if (workShopModel.WorkshopCode == "")
            {
                hint.Text = NewuGlobal.GetRes("000426") + NewuGlobal.GetRes("000176");
                return false;
            }
            if (workShopModel.ShopName == "")
            {
                hint.Text = NewuGlobal.GetRes("000431") + NewuGlobal.GetRes("000176");
                return false;
            }
            if (workShopModel.FactoryID == "")
            {
                hint.Text = NewuGlobal.GetRes("000434") + NewuGlobal.GetRes("000176");
                return false;
            }
            return true;
        }

        public void GetData()
        {
            List<SYS_WorkShop> sYS_WorkShops = workShopRepository.GetList("");

            dgv.DataSource = sYS_WorkShops;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
                return;

            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, rowIndex].Value.ToString();
                string workShopName = dgv[2, rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show("[ " + workShopName + " ] " + NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = workShopRepository.Delete(id);
                    if (isAccess)
                    {
                        hint.Text = NewuGlobal.GetRes("000173");
                        GetData();
                    }
                    else
                    {
                        hint.Text = NewuGlobal.GetRes("000174");
                    }
                }
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            groupBox1.Text = NewuGlobal.GetRes("000219");
            btnAdd.Text = NewuGlobal.GetRes("000100"); //新增
            btnEdit.Text = NewuGlobal.GetRes("000101"); //编辑
            btnDel.Text = NewuGlobal.GetRes("000102"); //删除
            btnClose.Text = NewuGlobal.GetRes("000103"); //关闭
            label1.Text = NewuGlobal.GetRes("000425") + ":";
            label2.Text = NewuGlobal.GetRes("000426") + ":";
            label3.Text = NewuGlobal.GetRes("000427") + ":";
            label4.Text = NewuGlobal.GetRes("000424") + ":";
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
            }
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.GetRes((423 + i).ToString("000000"));
                }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");
            txt_ShopName.Text = dgv.CurrentRow.Cells["ShopName"].Value.ToString();
            txt_WorkshopCode.Text = dgv.CurrentRow.Cells["WorkshopCode"].Value.ToString();
            txt_WorkshopJaneSpell.Text = dgv.CurrentRow.Cells["WorkshopJaneSpell"].Value.ToString();
            cmb_FactoryID.SelectedValue = dgv.CurrentRow.Cells["FactoryID"].Value.ToString();
        }
    }
}