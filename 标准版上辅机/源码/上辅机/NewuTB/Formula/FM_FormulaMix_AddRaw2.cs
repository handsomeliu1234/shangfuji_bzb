using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.Formula
{
    public partial class FM_FormulaMix_AddRaw2 : Form
    {
        private readonly FormulaWeighRepository formulaWeighRepository = new FormulaWeighRepository();
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly TB_BinSettingRepository binSettingRepository = new TB_BinSettingRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();

        private FormulaWeigh weightModel = null;

        private string recipeDeviceID
        {
            get; set;
        }

        private string DevicePartID
        {
            get; set;
        }

        private string DevicePart
        {
            get; set;
        }

        private bool isEdit = false;
        private List<FormulaWeigh> list;

        /// <summary>
        /// </summary>
        /// <param name="model">称量单笔数据模型</param>
        /// <param name="_deviceId">所属机台ID</param>
        /// <param name="_typeCodeName">材料类型编码名称</param>
        /// <param name="_devicePartCode">设备部件编码</param>
        public FM_FormulaMix_AddRaw2(FormulaWeigh model, string _deviceId, string _typeCodeName, string _devicePartId, List<FormulaWeigh> _list)
        {
            InitializeComponent();

            weightModel = model;
            recipeDeviceID = _deviceId;
            DevicePartID = _devicePartId;
            list = _list;
        }

        public FM_FormulaMix_AddRaw2(FormulaWeigh model, string _deviceId, List<FormulaWeigh> _list, bool _IsEdit)
        {
            InitializeComponent();

            weightModel = model;
            recipeDeviceID = _deviceId;
            list = _list;
            isEdit = _IsEdit;
            SetControlLanguageText();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //"请选择投料次序"
            if (cmb_DropOrder.SelectedIndex < 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000322"));
                return;
            }

            int dropOrder = Convert.ToInt32(cmb_DropOrder.SelectedValue);
            //"次投入物料信息未录入，不允许录入第"
            if (ComputeDropOrder(dropOrder) == false)
            {
                string msg = NewuGlobal.GetRes("000304") + (dropOrder - 1) + NewuGlobal.GetRes("000323") + dropOrder + NewuGlobal.GetRes("000324");
                MessageBox.Show(msg, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            weightModel.MaterialCode = txt_MaterialName.Text;

            weightModel.DevicePartID = cmb_DevicePart.SelectedValue.ToString();
            weightModel.DevicePartCode = NewuGlobal.DevicePartCodeByID(weightModel.DevicePartID);

            weightModel.DeviceID = cmb_Device.SelectedValue.ToString();
            weightModel.DeviceCode = NewuGlobal.DeviceCodeByID(weightModel.DeviceID);

            if (cmb_WeighMaterial.SelectedValue != null)
            {
                weightModel.WeighMaterialID = cmb_WeighMaterial.SelectedValue.ToString();
                weightModel.WeighMaterialCode = cmb_WeighMaterial.Text;
            }
            else
            {
                string msg = NewuGlobal.GetRes("000128");//选择的[称量部件]无对应的[称量材料]，请确认！
                MessageBox.Show(msg, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            weightModel.WeighSetVal = FunClass.VDecimal(txt_WeighSetVal.Text);
            weightModel.AllowError = FunClass.VDecimal(txt_AllowError.Text);
            weightModel.Reserve1 = FunClass.VDecimal(txt_SetTiQian.Text).ToString();
            weightModel.Reserve2 = FunClass.VDecimal(txt_SetKuai.Text).ToString();
            weightModel.DropOrder = dropOrder;
            if (weightModel.WeighSetVal == 0)
            {
                string msg = NewuGlobal.GetRes("000336");//设定重量不可为零！
                MessageBox.Show(msg, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (weightModel.AllowError == 0)
            {
                string msg = NewuGlobal.GetRes("000337"); //"允许误差不可为零！"
                MessageBox.Show(msg, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (weightModel.WeighSetVal <= FunClass.VDecimal(txt_SetTiQian.Text) || weightModel.WeighSetVal <= FunClass.VDecimal(txt_SetKuai.Text) || weightModel.WeighSetVal <= weightModel.AllowError)
            {
                string msg = NewuGlobal.GetRes("000338");//"快称值和提前量 误差不得大于设定重量！！"
                MessageBox.Show(msg, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!isEdit)
            {
                weightModel.WeighOrder = 0;//必须先将称量顺序情况，然后重新计算
                weightModel.WeighOrder = ComputeWeightOrder(weightModel.DropOrder, weightModel.DevicePartID);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 根据物料类型添加对应物料
        /// </summary>
        private void AddWeighMaterialData()
        {
            string TypeCodeName = TypeCodeNameByDevicePartCode(cmb_DevicePart.Text);
            DevicePartID = NewuGlobal.GetDevicePartIDByPartCode(DevicePart);

            #region 根据类型编码查找物料

            List<FormulaMaterial> formulaMaterials;
            if (TypeCodeName == "Rubber")
            {
                TypeCodeEnum[] enumArr = new TypeCodeEnum[1]
                {
                   TypeCodeEnum.T胶料
                };

                formulaMaterials = formulaMaterialRepository.GetMaterialListByDeviceAndTypeCode(recipeDeviceID, enumArr);
                cmb_WeighMaterial.DataSource = formulaMaterials;
            }
            else if (TypeCodeName == "Drug")
            {
                TypeCodeEnum[] enumArr = new TypeCodeEnum[1]
                {
                   TypeCodeEnum.T药品
                };
                formulaMaterials = formulaMaterialRepository.GetMaterialListByDeviceAndTypeCode(recipeDeviceID, enumArr);
                cmb_WeighMaterial.DataSource = formulaMaterials;
            }
            else if (TypeCodeName == "Plasticizer")
            {
                TypeCodeEnum[] enumArr = new TypeCodeEnum[1]
                {
                   TypeCodeEnum.T塑解剂
                };
                formulaMaterials = formulaMaterialRepository.GetMaterialListByDeviceAndTypeCode(recipeDeviceID, enumArr);
                cmb_WeighMaterial.DataSource = formulaMaterials;
            }
            else if (TypeCodeName == "Silane")
            {
                TypeCodeEnum[] enumArr = new TypeCodeEnum[1]
                {
                   TypeCodeEnum.T硅烷
                };
                formulaMaterials = formulaMaterialRepository.GetMaterialListByDeviceAndTypeCode(recipeDeviceID, enumArr);
                cmb_WeighMaterial.DataSource = formulaMaterials;
            }
            else if (TypeCodeName == "Oil")
            {
                TypeCodeEnum[] enumArr = new TypeCodeEnum[1]
                {
                   TypeCodeEnum.T油料
                };
                formulaMaterials = formulaMaterialRepository.GetMaterialListByDeviceAndTypeCode(recipeDeviceID, enumArr);
                cmb_WeighMaterial.DataSource = formulaMaterials;
            }
            else if (TypeCodeName == "Carbon")
            {
                List<TB_BinSeting> tB_BinSetings = binSettingRepository.GetListJoinMaterialCode(recipeDeviceID, NewuGlobal.GetTypeCodeIDByCodeName(TypeCodeName));
                cmb_WeighMaterial.DataSource = tB_BinSetings;
            }
            else if (TypeCodeName == "Zno")
            {
                List<TB_BinSeting> tB_BinSetings = binSettingRepository.GetListJoinMaterialCode(recipeDeviceID, NewuGlobal.GetTypeCodeIDByCodeName(TypeCodeName));
                cmb_WeighMaterial.DataSource = tB_BinSetings;
            }
            else
            {
                List<TB_BinSeting> tB_BinSetings = binSettingRepository.GetListJoinMaterialCode(recipeDeviceID, NewuGlobal.GetTypeCodeIDByCodeName(TypeCodeName));
                cmb_WeighMaterial.DataSource = tB_BinSetings;
            }

            cmb_WeighMaterial.ValueMember = "MaterialID";
            cmb_WeighMaterial.DisplayMember = "MaterialCode";

            #endregion 根据类型编码查找物料
        }

        /// <summary>
        /// 将称量部件转换成物料类型
        /// </summary>
        /// <param name="DevicePartCode"></param>
        /// <returns></returns>
        private string TypeCodeNameByDevicePartCode(string DevicePartCode)
        {
            string TypeCodeName;
            switch (DevicePartCode)
            {
                case "OilScales":
                    TypeCodeName = "Oil";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.Oil);
                    break;

                case "MixDrugScales":
                    TypeCodeName = "Drug";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.DrugMixer);
                    break;

                case "MixRubberScales":
                    TypeCodeName = "Rubber";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.Rubber);
                    break;

                case "CarbonScales":
                    TypeCodeName = "Carbon";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.Carbon);
                    break;

                case "ZnoScales":
                    TypeCodeName = "Zno";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.Zno);
                    break;

                case "PlasticScales":
                    TypeCodeName = "Plasticizer";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.Plasticizer);
                    break;

                case "SilaneScales":
                    TypeCodeName = "Silane";
                    DevicePart = NewuGlobal.GetDevicePartCode(DevicePartType.Silane);
                    break;

                default:
                    TypeCodeName = "";
                    break;
            }
            return TypeCodeName;
        }

        private void FM_FormulaMix_Add_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;

            cmb_Device.DataSource = deviceRepository.GetList("");
            cmb_Device.ValueMember = "DeviceID";
            cmb_Device.DisplayMember = "DeviceName";

            cmb_DevicePart.DataSource = NewuGlobal.DevicePartList;
            cmb_DevicePart.ValueMember = "DevicePartID";
            cmb_DevicePart.DisplayMember = "DevicePartName";

            if (cmb_WeighMaterial.SelectedIndex >= 0)
            {
                cmb_WeighMaterial.SelectedIndex = 0;
            }

            cmb_DropOrder.DataSource = formulaWeighRepository.DropTable();
            cmb_DropOrder.DisplayMember = "name";
            cmb_DropOrder.ValueMember = "value";
            if (!isEdit)
            {
                AddWeighMaterialData();
            }

            InitTextBox();
            InitPage();
        }

        /// <summary>
        /// 初始化文本框（公差、快称、提前量）
        /// </summary>
        private void InitTextBox()
        {
            if (cmb_WeighMaterial.Items.Count <= 0)
                return;

            string strDevicePart;
            if (cmb_WeighMaterial.SelectedValue is string)
            {
                strDevicePart = cmb_WeighMaterial.SelectedValue.ToString();
            }
            else
            {
                TB_BinSeting selectedValue = (TB_BinSeting)cmb_WeighMaterial.SelectedValue;
                strDevicePart = selectedValue.MaterialID;
            }

            string strVeighSetVal = txt_WeighSetVal.Text;
            double weighSetVal;

            if (strVeighSetVal == "")
            {
                txt_WeighSetVal.Text = "0";
                weighSetVal = 0;
            }
            else
            {
                weighSetVal = double.Parse(strVeighSetVal);
            }

            List<TB_BinSeting> tB_BinSetings = binSettingRepository.GetModelList(" MaterialID='" + strDevicePart + "'");

            if (tB_BinSetings != null && tB_BinSetings.Count == 1)
            {
                txt_AllowError.Text = tB_BinSetings[0].PreSetWuUp.ToString();
                double temp = weighSetVal - double.Parse(tB_BinSetings[0].PreSetKuai.ToString());
                if (temp >= 0)
                {
                    txt_SetKuai.Text = temp.ToString();
                }
                else
                {
                    txt_SetKuai.Text = "0";
                }
                txt_SetTiQian.Text = tB_BinSetings[0].PreSetTiQian.ToString();
            }
            else
            {
                txt_AllowError.Text = "0";
                txt_SetKuai.Text = "0";
                txt_SetTiQian.Text = "0";
            }
        }

        private void InitPage()
        {
            try
            {
                //设定信息组
                if (weightModel.DevicePartID != null)
                    cmb_DevicePart.SelectedValue = weightModel.DevicePartID;

                if (weightModel.WeighMaterialID != null)
                    cmb_WeighMaterial.SelectedValue = weightModel.WeighMaterialID;

                if (weightModel == null)
                {
                    cmb_DropOrder.SelectedValue = 0;
                }

                txt_WeighSetVal.Text = weightModel.WeighSetVal.ToString();
                txt_AllowError.Text = weightModel.AllowError.ToString();

                if (weightModel.Reserve1 != null)
                    txt_SetTiQian.Text = weightModel.Reserve1.ToString();

                if (weightModel.Reserve2 != null)
                    txt_SetKuai.Text = weightModel.Reserve2.ToString();

                //默认信息组
                FormulaMaterial formulaMaterial = formulaMaterialRepository.GetModel(weightModel.MaterialID);

                if (formulaMaterial == null)
                    return;

                txt_MaterialName.Text = formulaMaterial.MaterialCode;
                txt_MaterialDesc.Text = formulaMaterial.MaterialDesc;
                cmb_Device.SelectedValue = recipeDeviceID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private int ComputeWeightOrder(int dropOrder, string DevicePartID)
        {
            int order = 0;

            if (list.Count == 0)
            {
                return 1;
            }

            foreach (FormulaWeigh item in list)
            {
                if (item.DevicePartID == DevicePartID)
                {
                    if (Convert.ToInt32(item.DropOrder) == dropOrder)
                    {
                        order++;
                    }
                }
            }

            return order + 1;
        }

        //验证投料次数输入是否正确
        private bool ComputeDropOrder(int dropOrder)
        {
            int computerOrder = 0;
            foreach (FormulaWeigh item in list)
            {
                if (Convert.ToInt32(item.DropOrder) > computerOrder)
                {
                    computerOrder = Convert.ToInt32(item.DropOrder);
                }
            }
            computerOrder++;

            if (dropOrder > computerOrder)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Cmb_DevicePart_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddWeighMaterialData();
        }

        private void Cmb_WeighMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化文本框
            try
            {
                InitTextBox();
            }
            catch (Exception)
            {
            }
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/

            this.Text = NewuGlobal.GetRes("000259");  //设定信息

            groupBox2.Text = NewuGlobal.GetRes("000260");  //设定信息
            groupBox1.Text = NewuGlobal.GetRes("000261");  //配方信息

            label3.Text = NewuGlobal.GetRes("000262");// *称量部件
            label4.Text = NewuGlobal.GetRes("000263");// *称量材料
            label5.Text = NewuGlobal.GetRes("000264");// *设定重量
            label9.Text = NewuGlobal.GetRes("000265");// *快称值
            label7.Text = NewuGlobal.GetRes("000266");// *投料次序
            label6.Text = NewuGlobal.GetRes("000267");// *允许公差
            label10.Text = NewuGlobal.GetRes("000268");// *提前量
            label1.Text = NewuGlobal.GetRes("000269");// *配方编号
            label2.Text = NewuGlobal.GetRes("000270");// *所属设备
            label8.Text = NewuGlobal.GetRes("000271");// *配方描述

            /***********  常见按钮   ***********/
            btnOk.Text = NewuGlobal.GetRes("000108"); //确定
            btnClose.Text = NewuGlobal.GetRes("000103");//关闭
            if (NewuGlobal.SupportLanguage.Equals("1"))
                btnClose.Padding = new Padding(0, 0, 7, 0);
            else
                btnClose.Padding = new Padding(0, 0, 0, 0);

            /***********  常见文字   ***********/
        }
    }
}