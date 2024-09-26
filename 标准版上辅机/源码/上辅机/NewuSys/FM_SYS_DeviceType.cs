using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_DeviceType : Form, ILanguageChanged
    {
        private readonly SYS_DeviceTypeRepository deviceTypeRepository = new SYS_DeviceTypeRepository();

        public FM_SYS_DeviceType()
        {
            InitializeComponent();
        }

        private void FM_SYS_DeviceType_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            List<SYS_DeviceType> sYS_DeviceTypes = deviceTypeRepository.GetList("");

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("DeviceTypeID","设备类型ID", ColumnType.txt,false),
                new ColStruct("DeviceTypeName","设备类型名称"),
                new ColStruct("DeviceTypeCode","设备类型编码"),
                new ColStruct("DeviceTypeJaneSpell","设备类型简拼"),
                new ColStruct("SaveTime","保存时间")
            };
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;

            dgv.AddCols(cols);
            dgv.DataSource = sYS_DeviceTypes;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetLanguage();
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btnadd_Click(object sender, EventArgs e)
        {
            SYS_DeviceType sYS_DeviceType = new SYS_DeviceType();
            ExcuteData(sYS_DeviceType, true);
        }

        private void Btnedit_Click(object sender, EventArgs e)
        {
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, dgv.CurrentCell.RowIndex].Value.ToString();
                SYS_DeviceType sYS_DeviceType = deviceTypeRepository.GetModel("DeviceTypeID = '" + id + "'");
                ExcuteData(sYS_DeviceType, false);
            }
        }

        private void ExcuteData(SYS_DeviceType deviceTypeModel, bool flag)
        {
            try
            {
                deviceTypeModel.DeviceTypeName = txt_DeviceTypeName.Text.Trim();
                deviceTypeModel.DeviceTypeCode = txt_DeviceTypeCode.Text.Trim();
                deviceTypeModel.DeviceTypeJaneSpell = txt_DeviceTypeJaneSpell.Text.Trim();
                deviceTypeModel.SaveTime = DateTime.Now;

                if (!DataVerification(deviceTypeModel))
                    return;
                bool resutl;
                if (flag)
                    resutl = deviceTypeRepository.Add(deviceTypeModel);
                else
                    resutl = deviceTypeRepository.Update(deviceTypeModel);

                if (resutl)
                {
                    hint.Text = NewuGlobal.GetRes("000171");
                    GetData();
                }
                else
                    hint.Text = NewuGlobal.GetRes("000172");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_DeviceType").Error(ex.ToString());
            }
        }

        private bool DataVerification(SYS_DeviceType deviceTypeModel)
        {
            if (deviceTypeModel.DeviceTypeName == "")
            {
                hint.Text = NewuGlobal.GetRes("000756") + NewuGlobal.GetRes("000162");
                return false;
            }
            if (deviceTypeModel.DeviceTypeCode == "")
            {
                hint.Text = NewuGlobal.GetRes("000757") + NewuGlobal.GetRes("000162");
                return false;
            }
            return true;
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string id = dgv[0, rowIndex].Value.ToString();
                DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isDel == DialogResult.Yes)
                {
                    bool isAccess = deviceTypeRepository.Delete(id);
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
            label1.Text = NewuGlobal.GetRes("000756") + ":";
            label2.Text = NewuGlobal.GetRes("000757") + ":";
            label3.Text = NewuGlobal.GetRes("000758") + ":";
            hint.Text = NewuGlobal.GetRes("000170");

            this.btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            this.btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            this.btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");

            groupBox2.Text = NewuGlobal.LanguagResourceManager.GetString("000219");
            groupBox1.Text = NewuGlobal.LanguagResourceManager.GetString("000438");

            dgv.Columns[1].HeaderText = NewuGlobal.GetRes("000756");
            dgv.Columns[2].HeaderText = NewuGlobal.GetRes("000757");
            dgv.Columns[3].HeaderText = NewuGlobal.GetRes("000758");
            dgv.Columns[4].HeaderText = NewuGlobal.GetRes("000081");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = NewuGlobal.GetRes("000170");
            txt_DeviceTypeName.Text = dgv.CurrentRow.Cells["DeviceTypeName"].Value.ToString();
            txt_DeviceTypeCode.Text = dgv.CurrentRow.Cells["DeviceTypeCode"].Value.ToString();
            txt_DeviceTypeJaneSpell.Text = dgv.CurrentRow.Cells["DeviceTypeJaneSpell"].Value.ToString();
        }
    }
}