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
    public partial class FM_SYS_TypeCode : Form, ILanguageChanged
    {
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();

        public FM_SYS_TypeCode()
        {
            InitializeComponent();
        }

        private void FM_SYS_TypeCode_Load(object sender, EventArgs e)
        {
            InitView();
            GetData();
        }

        private void InitView()
        {
            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DataSource = EnableList.GetList();
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Enabled.SelectedIndex = 0;

            ColStruct[] colStructs = new ColStruct[]
            {
                new ColStruct("TypeCodeName","类型编码名称",ColumnType.txt,true),
                new ColStruct("TypeCodeDesc","类型编码描述",ColumnType.txt,true),
                new ColStruct("SaveTime","保存时间",ColumnType.txt,true),
                new ColStruct("TypeCodeID","类型编码",ColumnType.txt,true),
                new ColStruct("Enable","是否启用",ColumnType.chk,true),
            };
            dgv.AddCols(colStructs);
            dgv.Columns["SaveTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dgv.GridColor = SystemColors.ControlDark;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void GetData()
        {
            List<SYS_TypeCode> list = typeCodeRepository.GetList("");
            dgv.DataSource = list;
            SetLanguage();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SYS_TypeCode sYS_TypeCode = new SYS_TypeCode();
            ExcuteData(sYS_TypeCode, true);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            string typecodeId = dgv.CurrentRow.Cells["TypeCodeID"].Value.ToString();

            if (!string.IsNullOrEmpty(typecodeId))
            {
                SYS_TypeCode sYS_TypeCode = typeCodeRepository.GetModel(typecodeId);
                ExcuteData(sYS_TypeCode, false);
            }
        }

        private void ExcuteData(SYS_TypeCode typeCodeModel, bool flag)
        {
            try
            {
                typeCodeModel.TypeCodeName = txt_TypeCodeName.Text;
                typeCodeModel.TypeCodeDesc = txt_TypeCodeDesc.Text;
                typeCodeModel.TypeCodeSpell = txt_TypeCodeSpell.Text;
                typeCodeModel.SaveTime = DateTime.Now;
                if (cmb_Enabled.SelectedIndex >= 0)
                    typeCodeModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue);
                else
                    typeCodeModel.Enable = -1;
                if (!DataVerification(typeCodeModel))
                    return;
                bool result;
                if (flag)
                    result = typeCodeRepository.Add(typeCodeModel);
                else
                    result = typeCodeRepository.Update(typeCodeModel);

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
                NewuGlobal.LogCat("FM_SYS_TypeCode").Error(ex.ToString());
            }
        }

        private bool DataVerification(SYS_TypeCode typeCodeModel)
        {
            if (typeCodeModel.TypeCodeID == "")
            {
                hint.Text = NewuGlobal.GetRes("000016") + "ID" + NewuGlobal.GetRes("000162");
                return false;
            }
            if (typeCodeModel.TypeCodeName == "")
            {
                hint.Text = NewuGlobal.GetRes("000764") + NewuGlobal.GetRes("000162");
                return false;
            }

            return true;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            string typecodeId = dgv.CurrentRow.Cells["TypeCodeID"].Value.ToString();
            DialogResult diaResult = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (diaResult == DialogResult.Cancel)
                return;

            bool isDel = typeCodeRepository.DeleteList(typecodeId);
            if (isDel)
            {
                MessageBox.Show(NewuGlobal.GetRes("000173"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000174"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            hint.Text= NewuGlobal.GetRes("000170");
            btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");
            groupBox1.Text = NewuGlobal.LanguagResourceManager.GetString("000438");
            dgv.Columns[0].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000764");
            dgv.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000765");
            dgv.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000081");
            dgv.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000466");
            dgv.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000188");
            label1.Text = NewuGlobal.LanguagResourceManager.GetString("000764") + ":";
            label4.Text = NewuGlobal.LanguagResourceManager.GetString("000765") + ":";
            label6.Text = NewuGlobal.LanguagResourceManager.GetString("000466") + ":";
            label7.Text = NewuGlobal.LanguagResourceManager.GetString("000188") + ":";
            label2.Text = NewuGlobal.GetRes("000170") + ":" + NewuGlobal.GetRes("000827");
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

            txt_TypeCodeName.Text = dgv.CurrentRow.Cells["TypeCodeName"].Value.ToString();
            txt_TypeCodeDesc.Text = dgv.CurrentRow.Cells["TypeCodeDesc"].Value.ToString();
            string typeCodeID = dgv.CurrentRow.Cells["TypeCodeID"].Value.ToString();
            SYS_TypeCode sYS_TypeCode = typeCodeRepository.GetModel(typeCodeID);
            if (sYS_TypeCode != null)
                txt_TypeCodeSpell.Text = sYS_TypeCode.TypeCodeSpell;

            cmb_Enabled.SelectedValue = dgv.CurrentRow.Cells["Enable"].Value.ToString();
        }
    }
}