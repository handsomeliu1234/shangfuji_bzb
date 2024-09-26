using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_TypeCode_Add : Form
    {
        private string TypeCodeID = "";
        private SYS_TypeCode typeCodeModel = new SYS_TypeCode();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 判断是新增操作还是编辑操作
        /// </summary>
        private bool isAdd;

        public FM_SYS_TypeCode_Add()
        {
            InitializeComponent();
            isAdd = true;
        }

        public FM_SYS_TypeCode_Add(string _TypeCodeID)
        {
            InitializeComponent();
            TypeCodeID = _TypeCodeID;
            isAdd = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (typeCodeModel == null)
                    typeCodeModel = new SYS_TypeCode();
                typeCodeModel.TypeCodeName = txt_TypeCodeName.Text;
                typeCodeModel.TypeCodeDesc = txt_TypeCodeDesc.Text;
                typeCodeModel.TypeCodeSpell = txt_TypeCodeSpell.Text;

                typeCodeModel.SaveTime = DateTime.Now;

                if (cmb_Enabled.SelectedIndex >= 0)
                {
                    typeCodeModel.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue);
                }
                else
                {
                    typeCodeModel.Enable = -1;
                }
                if (DataVerification() == false)
                    return;
                bool isAccess;
                if (isAdd == false)
                {
                    isAccess = typeCodeRepository.Update(typeCodeModel);
                }
                else
                {
                    isAccess = typeCodeRepository.Add(typeCodeModel);
                }

                if (isAccess == true)
                {
                    MessageBox.Show("信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (string.IsNullOrEmpty(typeCodeModel.TypeCodeID))
                    {
                        ClearControl();
                    }
                    RefreshGrid();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("信息保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        private void ClearControl()
        {
            typeCodeModel.TypeCodeID = "";
            typeCodeModel.TypeCodeName = "";
            typeCodeModel.TypeCodeDesc = "";
            typeCodeModel.TypeCodeSpell = "";
            typeCodeModel.ParentTypeCodeDataSet = "";

            typeCodeModel.ParentTypeCodeID = "";
            cmb_Enabled.SelectedIndex = -1;
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_TypeCode fm = obj as FM_SYS_TypeCode;
                fm.GetData();
            }
        }

        private bool DataVerification()
        {
            if (typeCodeModel.TypeCodeID == "")
            {
                MessageBox.Show("类型编码ID 不能为空！");
                return false;
            }
            if (typeCodeModel.TypeCodeName == "")
            {
                MessageBox.Show("类型编码名称 不能为空！");
                return false;
            }

            return true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FM_SYS_TypeCode_Add_Load(object sender, EventArgs e)
        {
            try
            {
                cmb_Enabled.DisplayMember = "names";
                cmb_Enabled.ValueMember = "values";
                cmb_Enabled.DataSource = Newu.EnableList.GetList();
                cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb_Enabled.SelectedIndex = 0;
                if (TypeCodeID != "")
                {
                    typeCodeModel = typeCodeRepository.GetModel(TypeCodeID);
                    txt_TypeCodeName.Text = typeCodeModel.TypeCodeName;
                    txt_TypeCodeDesc.Text = typeCodeModel.TypeCodeDesc;
                    txt_TypeCodeSpell.Text = typeCodeModel.TypeCodeSpell;
                    cmb_Enabled.SelectedValue = typeCodeModel.Enable;
                }

                SetLanguage();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        private void SetLanguage()
        {
            groupBox3.Text = NewuGlobal.GetRes("000412");
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            label1.Text = NewuGlobal.GetRes("000764") + "：";
            label4.Text = NewuGlobal.GetRes("000765") + "：";
            label6.Text = NewuGlobal.GetRes("000766") + "：";
            label5.Text = NewuGlobal.GetRes("000162");
            label7.Text = NewuGlobal.GetRes("000188") + ":";//是否启用
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