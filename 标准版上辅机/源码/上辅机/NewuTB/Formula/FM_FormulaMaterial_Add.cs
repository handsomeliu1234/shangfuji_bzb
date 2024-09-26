using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NewuTB.Formula
{
    public partial class FM_FormulaMaterial_Add : Form
    {
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly TB_UserInfoRepository userInfoRepository = new TB_UserInfoRepository();
        private string materialID = "";
        private FormulaMaterial formulaMaterial;
        private bool isCopy = false;
        private List<SYS_TypeCode> listN = null;

        //表示入口在哪里，区分入口  维护其类别的下拉入口
        private bool isFormulaMaterial = false;

        /// <summary>
        /// 判断是新增操作还是编辑操作
        /// </summary>
        private bool isAdd;

        public FM_FormulaMaterial_Add(bool isFormulaMaterial)
        {
            InitializeComponent();
            isAdd = true;
            this.isFormulaMaterial = isFormulaMaterial;
        }

        public FM_FormulaMaterial_Add(string _MaterialID, bool isFormulaMaterial)
        {
            InitializeComponent();
            materialID = _MaterialID;
            this.isFormulaMaterial = isFormulaMaterial;
            if (string.IsNullOrEmpty(_MaterialID))
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }
        }

        /// <summary>
        /// 复制配方使用
        /// </summary>
        /// <param name="_MaterialID"></param>
        /// <param name="random"></param>
        /// <param name="isFormulaMaterial"></param>
        public FM_FormulaMaterial_Add(string _MaterialID, bool isCopy, bool isFormulaMaterial)
        {
            InitializeComponent();
            materialID = _MaterialID;
            isAdd = true;
            this.isCopy = isCopy;
            this.isFormulaMaterial = isFormulaMaterial;
        }

        private void FM_FormulaMaterial_Add_Load(object sender, EventArgs e)
        {
            listN = NewuGlobal.TypeCodeList.Where(t => t.TypeCodeName == typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T母炼配方) || t.TypeCodeName == typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T终炼配方)).ToList();

            cmb_TypeCodeID.DataSource = listN;
            cmb_TypeCodeID.ValueMember = "TypeCodeID";
            if (NewuGlobal.SupportLanguage.Equals("1"))
                cmb_TypeCodeID.DisplayMember = "TypeCodeDesc";
            else
                cmb_TypeCodeID.DisplayMember = "TypeCodeName";

            List<SYS_Device> sYS_Devices = deviceRepository.GetModelListAddNullRows("");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            cmb_Enabled.DataSource = EnableList.GetList();
            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;

            if (materialID != "")
            {
                formulaMaterial = formulaMaterialRepository.GetModel(materialID);
                if (isCopy)
                    txt_MaterialCode.Text = formulaMaterial.MaterialCode + "-副本";
                else
                    txt_MaterialCode.Text = formulaMaterial.MaterialCode;

                cmb_DeviceID.SelectedValue = formulaMaterial.DeviceID;
                txt_VersionNo.Text = formulaMaterial.VersionNo;
                txt_MaterialDesc.Text = formulaMaterial.MaterialDesc;
                cmb_TypeCodeID.SelectedValue = formulaMaterial.TypeCodeID;
                cmb_Enabled.SelectedValue = formulaMaterial.Enable;
                ckbIsHwlj.Checked = !string.IsNullOrEmpty(formulaMaterial.Reserve1) && formulaMaterial.Reserve1 != "0";
                txtHwChar.Text = formulaMaterial.Reserve2; //恒温字符
                tb_temp.Text = formulaMaterial.Reserve1;
                tbJiange.Text = formulaMaterial.Reserve3; //设定间隔时间
            }
            SetControlLanguageText();
        }

        /// <summary>
        /// 设置控件语言
        /// </summary>
        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            if (isAdd)
                this.Text = NewuGlobal.GetRes("000100");  //新增
            else
                this.Text = NewuGlobal.GetRes("000101");  //新增

            groupBox2.Text = NewuGlobal.GetRes("000195");  //物料分类
            groupBox1.Text = NewuGlobal.GetRes("000196");  //物料信息

            label8.Text = NewuGlobal.GetRes("000198") + ":"; //物料类型

            label10.Text = NewuGlobal.GetRes("000199") + ":";  // 配方名称

            label5.Text = NewuGlobal.GetRes("000200") + ":";  //版本号

            label7.Text = NewuGlobal.GetRes("000201") + ":"; // 所属设备
            label2.Text = NewuGlobal.GetRes("000202") + ":";  //物料描述

            label15.Text = NewuGlobal.GetRes("000203") + ":"; //是否启用
            lblHwChar.Text = NewuGlobal.GetRes("000204");  //恒温炼胶字符
            lblTemp.Text = NewuGlobal.GetRes("000205");  //恒温炼胶温度

            ckbIsHwlj.Text = NewuGlobal.GetRes("000207");  //恒温练胶

            label12.Text = NewuGlobal.GetRes("000065") + ":";//间隔时间

            /***********  常见按钮   ***********/
            btnSave.Text = NewuGlobal.GetRes("000108"); //保存
            btnClose.Text = NewuGlobal.GetRes("000103");//关闭
            if (NewuGlobal.SupportLanguage.Equals("1"))
                btnClose.Padding = new Padding(0, 0, 7, 0);
            else
                btnClose.Padding = new Padding(0, 0, 0, 0);

            /***********  常见文字   ***********/
            label13.Text = label6.Text = label4.Text = label3.Text = label1.Text = NewuGlobal.GetRes("000176");// *不能为空
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataVerification() == false)
                {
                    return;
                }

                if (formulaMaterial == null)
                {
                    formulaMaterial = new FormulaMaterial();
                }
                formulaMaterial.MaterialCode = txt_MaterialCode.Text;
                formulaMaterial.VersionNo = txt_VersionNo.Text;
                formulaMaterial.MaterialDesc = txt_MaterialDesc.Text;
                List<string> barCode = new List<string>();
                formulaMaterial.BarCode = string.Join(",", barCode);
                formulaMaterial.TypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                formulaMaterial.MaterialFrom = "0";
                formulaMaterial.UseToMaterialID = "";
                if (tbJiange.Visible)
                {
                    if (NewuCommon.FunClass.VVal(tbJiange.Text) == 0)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000191"));  //"配方间隔时间必须为数字"
                        return;
                    }
                    else
                    {
                        formulaMaterial.Reserve3 = tbJiange.Text;
                    }
                }
                //选中则是恒温炼胶配方
                if (ckbIsHwlj.Checked)
                {
                    formulaMaterial.Reserve1 = tb_temp.Text;
                    if (!string.IsNullOrEmpty(txtHwChar.Text) && !string.IsNullOrEmpty(tb_temp.Text))
                    {
                        //安全验证
                        Match regex = Regex.Match(txtHwChar.Text, @"^[A-Z\d+./]{10}$");
                        if (!regex.Success)
                        {
                            MessageBox.Show(NewuGlobal.GetRes("000192"));//"恒温字符必须是10位字符(包含大写字母和数字)！"
                            return;
                        }

                        formulaMaterial.Reserve2 = txtHwChar.Text;
                    }
                    else
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000193")); //"恒温炼胶配方必须填写恒温字符和温度！"
                        return;
                    }
                }
                else
                {
                    formulaMaterial.Reserve1 = "0";//不选则默认为母练配方
                    formulaMaterial.Reserve2 = "";
                }

                formulaMaterial.SaveUserID = NewuGlobal.TB_UserInfo.UserID;
                //需要根据id查找数据库
                if (formulaMaterial.SaveUserID != null && formulaMaterial.SaveUserID != "")
                    formulaMaterial.SaveRealName = userInfoRepository.GetModel(formulaMaterial.SaveUserID).RealName;

                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    formulaMaterial.DeviceID = cmb_DeviceID.SelectedValue.ToString();
                    formulaMaterial.DeviceCode = NewuGlobal.DeviceCodeByID(formulaMaterial.DeviceID);
                }

                if (cmb_TypeCodeID.SelectedIndex >= 0)
                {
                    formulaMaterial.TypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                }

                if (cmb_Enabled.SelectedIndex >= 0)
                {
                    formulaMaterial.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
                }
                formulaMaterial.SaveTime = DateTime.Now;

                bool isAccess = false;
                if (isAdd)
                {
                    isAccess = formulaMaterialRepository.Add(formulaMaterial);
                }
                else
                {
                    isAccess = formulaMaterialRepository.Update(formulaMaterial);
                }

                if (isAccess == true)
                {
                    object obj = Owner;
                    if (Owner.Name == "FM_FormulaLibrary")
                    {
                        TurntoMix(obj);
                    }
                    else if (Owner.Name == "FM_FormulaMaterial")
                    {
                        RefreshGrid(obj);
                    }
                    MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("UNIQUE KEY"))
                {
                    MessageBox.Show(NewuGlobal.GetRes("000294"));
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshGrid(object obj)
        {
            if (obj != null)
            {
                FM_FormulaMaterial fm = obj as FM_FormulaMaterial;
                fm.GetData();
            }
        }

        private void TurntoMix(object obj)
        {
            if (obj != null)
            {
                FM_FormulaLibrary fm = obj as FM_FormulaLibrary;
                fm.GetFormulaList();
                fm.GetMaterialModel(formulaMaterial);
            }
        }

        private bool DataVerification()
        {
            //"请选择物料类型！"
            if (cmb_TypeCodeID.SelectedIndex < 0)
            {
                cmb_TypeCodeID.Focus();
                MessageBox.Show(NewuGlobal.GetRes("000208") + " " + NewuGlobal.GetRes("000387"));
                return false;
            }
            //"物料编号不能为空！"
            if (txt_MaterialCode.Text.Trim() == "")
            {
                txt_MaterialCode.Text = "";
                txt_MaterialCode.Focus();
                MessageBox.Show(NewuGlobal.GetRes("000388") + " " + NewuGlobal.GetRes("000162"));
                return false;
            }
            //"版本号不能为空"
            if (txt_VersionNo.Text.Trim() == "")
            {
                txt_VersionNo.Text = "";
                txt_VersionNo.Focus();
                MessageBox.Show(NewuGlobal.GetRes("000200") + " " + NewuGlobal.GetRes("000162"));
                return false;
            }
            //"请选择设备"
            if (cmb_DeviceID.SelectedIndex < 0)
            {
                cmb_DeviceID.Focus();
                MessageBox.Show(NewuGlobal.GetRes("000208") + " " + NewuGlobal.GetRes("000182"));
                return false;
            }
            //"请选择是否启用！"
            if (cmb_Enabled.SelectedIndex < 0)
            {
                cmb_Enabled.Focus();
                MessageBox.Show(NewuGlobal.GetRes("000208") + " " + NewuGlobal.GetRes("000188"));
                return false;
            }
            return true;
        }

        private void Cmb_TypeCodeID_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cmb_TypeCodeID.Text.Contains("母炼") || cmb_TypeCodeID.Text.Contains(typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T母炼配方))))
            {
                ckbIsHwlj.Visible = false;
                lblHwChar.Visible = false;
                txtHwChar.Visible = false;
                tb_temp.Visible = false;
                lblTemp.Visible = false;
            }
            else
            {
                ckbIsHwlj.Visible = true;
                lblHwChar.Visible = true;
                txtHwChar.Visible = true;
                tb_temp.Visible = true;
                lblTemp.Visible = true;
            }
        }
    }
}