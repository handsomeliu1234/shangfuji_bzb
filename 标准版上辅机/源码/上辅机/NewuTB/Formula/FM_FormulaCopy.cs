using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.Formula
{
    public partial class FM_FormulaCopy : Form
    {
        private FormularMaterialRepository formularMaterialRepository = new FormularMaterialRepository();
        private SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();

        private NewuBLL.FormulaWeighBLL formulaWeighBll = new NewuBLL.FormulaWeighBLL();
        private FormulaWeighRepository formulaWeighRepository = new FormulaWeighRepository();

        private NewuBLL.SYS_DeviceBLL deviceBll = new NewuBLL.SYS_DeviceBLL();
        private NewuModel.FormulaMaterialMDL formulaModel = null;
        private NewuBLL.TB_UserInfoBLL userBll = new NewuBLL.TB_UserInfoBLL();

        public FM_FormulaCopy(NewuModel.FormulaMaterialMDL formulaModel)
        {
            InitializeComponent();
            this.formulaModel = formulaModel;
        }

        private void FM_FormulaCopy_Load(object sender, EventArgs e)
        {
            List<NewuModel.SYS_DeviceMDL> list = deviceBll.GetModelListAddNullRows("");
            cmb_DeviceID.DataSource = list;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            cmb_Enabled.DataSource = Newu.EnableList.GetList();
            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;

            txt_MaterialCode.Text = formulaModel.MaterialCode + "副本";
            cmb_DeviceID.SelectedValue = formulaModel.DeviceID;
            cmb_Enabled.SelectedValue = formulaModel.Enable;
        }

        private bool DataVerification()
        {
            if (txt_MaterialCode.Text.Trim() == "") { txt_MaterialCode.Text = ""; txt_MaterialCode.Focus(); MessageBox.Show("请设置新配方的名称！"); return false; }
            if (txt_VersionNo.Text.Trim() == "") { txt_VersionNo.Text = ""; txt_VersionNo.Focus(); MessageBox.Show("版本号不能为空"); return false; }
            if (cmb_DeviceID.SelectedIndex < 0) { cmb_DeviceID.Focus(); MessageBox.Show("请选择设备"); return false; }
            if (cmb_Enabled.SelectedIndex < 0) { cmb_Enabled.Focus(); MessageBox.Show("请选择是否启用！"); return false; }
            return true;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (DataVerification() == false)
            {
                return;
            }

            FormulaMaterial newFormula = new FormulaMaterial
            {
                MaterialCode = txt_MaterialCode.Text,
                VersionNo = txt_VersionNo.Text,
                MaterialDesc = txt_MaterialDesc.Text,
                TypeCodeID = formulaModel.TypeCodeID,
                MaterialFrom = "0",
                UseToMaterialID = "",
                SaveUserID = NewuGlobal.TB_UserInfo.UserID,
                SaveRealName = userBll.GetModel(NewuGlobal.TB_UserInfo.UserID).RealName
            };

            //需要根据id查找数据库
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                newFormula.DeviceID = cmb_DeviceID.SelectedValue.ToString();
                newFormula.DeviceCode = NewuGlobal.DeviceCodeByID(newFormula.DeviceID);
            }

            if (cmb_Enabled.SelectedIndex >= 0)
            {
                newFormula.Enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
            }

            newFormula.SaveTime = DateTime.Now;

            bool isSuccess = formularMaterialRepository.Add(newFormula);
            if (isSuccess == true)
            {
                List<NewuModel.FormulaWeighMDL> formulaWeighList = formulaWeighBll.GetListXF(formulaModel.MaterialID, cmb_DeviceID.SelectedValue.ToString());

                string typeCodeName = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T小药配方);
                List<FormulaWeigh> formulaWeighs = formulaWeighRepository.GetListXF(formulaModel.MaterialID, cmb_DeviceID.SelectedValue.ToString(), NewuGlobal.GetTypeCodeIDByCodeName(typeCodeName));
                for (int i = 0; i < formulaWeighs.Count; i++)
                {
                    formulaWeighs[i].MaterialID = newFormula.MaterialID;
                }

                if (formulaWeighBll.AddList(formulaWeighList))
                {
                    object obj = this.Owner;
                    if (obj != null)
                    {
                        FM_FormulaDetail fm = obj as FM_FormulaDetail;
                        fm.GetFormulaList();
                    }
                    MessageBox.Show("配方复制成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("配方复制失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}