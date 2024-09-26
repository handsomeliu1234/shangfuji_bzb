using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newu;
using NewuBLL;

namespace NewuTB.Formula
{
    public partial class FM_FormulaMix_AddRaw : Form
    {
        NewuBLL.FormulaWeighBLL formulaWeight = new FormulaWeighBLL();
        NewuBLL.FormulaMaterialBLL materialBll = new NewuBLL.FormulaMaterialBLL();
        NewuBLL.SYS_DeviceBLL deviceBll = new NewuBLL.SYS_DeviceBLL();
        NewuBLL.SYS_DevicePartBLL devicePartBll = new NewuBLL.SYS_DevicePartBLL();
        NewuBLL.SYS_TypeCodeBLL typeCodeBll = new NewuBLL.SYS_TypeCodeBLL();
        NewuBLL.TB_BinSetingBLL binSetingBll = new NewuBLL.TB_BinSetingBLL();
        
        NewuModel.FormulaWeighMDL weightModel = null;
        string recipeDeviceID { get; set; }
        string TypeCodeName { get; set; }
        string DevicePartID { get; set; }
        
        List<NewuModel.FormulaWeighMDL> List = null;
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="model"> 称量单笔数据模型</param>
        /// <param name="_deviceId">所属机台ID</param>
        /// <param name="_typeCodeName">材料类型编码名称</param>
        /// <param name="_devicePartCode">设备部件编码</param>
        public FM_FormulaMix_AddRaw(NewuModel.FormulaWeighMDL model, string _deviceId, string _typeCodeName, string _devicePartId,List<NewuModel.FormulaWeighMDL> _list)
        {
            InitializeComponent();

            weightModel = model;
            recipeDeviceID = _deviceId;
            DevicePartID = _devicePartId;
            TypeCodeName = _typeCodeName;
            List = _list;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int dropOrder = Convert.ToInt32(cmb_DropOrder.SelectedValue);
            if (ComputeDropOrder(dropOrder) == false)
            {
                string msg = "第" + (dropOrder - 1) + "次投入物料信息未录入，不允许录入第" + dropOrder + "次投入物料";
                MessageBox.Show(msg, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            weightModel.WeighOrder = 0;//必须先将称量顺序情况，然后重新计算

            weightModel.MaterialCode = txt_MaterialName.Text;
            weightModel.DevicePartID = DevicePartID;
            weightModel.DevicePartCode = NewuBLL.NewuGlobal.DevicePartCodeByID(weightModel.DevicePartID);

            weightModel.WeighMaterialID = cmb_WeighMaterial.SelectedValue.ToString();
            weightModel.WeighMaterialCode = cmb_WeighMaterial.Text;
            

            weightModel.DeviceID = cmb_Device.SelectedValue.ToString();
            weightModel.DeviceCode = NewuBLL.NewuGlobal.DeviceCodeByID(weightModel.DeviceID);

            weightModel.WeighSetVal =NewuCommon.FunClass.vVal( txt_WeighSetVal.Text);
            weightModel.AllowError =NewuCommon.FunClass.vDecimal(txt_AllowError.Text);
            weightModel.DropOrder=dropOrder;
            weightModel.WeighOrder = ComputeWeightOrder(weightModel.DropOrder);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FM_FormulaMix_Add_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;

            cmb_Device.DataSource = deviceBll.GetAllList().Tables[0];
            cmb_Device.ValueMember = "DeviceID";
            cmb_Device.DisplayMember = "DeviceName";

            cmb_DevicePart.DataSource = devicePartBll.GetAllList().Tables[0];
            cmb_DevicePart.ValueMember = "DevicePartID";
            cmb_DevicePart.DisplayMember = "DevicePartName";

            


            #region 根据类型编码查找物料
            DataSet ds=null;
            if (TypeCodeName == "Rubber")
            {
                NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum[] enumArr = new NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum[2]{
                    NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T胶料,
                    NewuBLL.SYS_TypeCodeBLL.TypeCodeEnum.T母炼配方
                };
                ds= materialBll.GetMaterialListByDeviceAndTypeCode(recipeDeviceID, enumArr);
            }
            else
            {
                ds = binSetingBll.GetListJoinMaterialCode(recipeDeviceID, NewuGlobal.GetTypeCodeIDByCodeName(TypeCodeName));
                
            }
            cmb_WeighMaterial.DataSource = ds.Tables[0];
            cmb_WeighMaterial.ValueMember = "MaterialID";
            cmb_WeighMaterial.DisplayMember = "MaterialCode";
            #endregion


            
            cmb_DropOrder.DataSource = formulaWeight.DropTable();
            cmb_DropOrder.DisplayMember = "name";
            cmb_DropOrder.ValueMember = "value";

            InitPage();
        }

        void InitPage()
        {

            try
            {

                //设定信息组
                cmb_WeighMaterial.SelectedValue = weightModel.WeighMaterialID;
                cmb_DropOrder.SelectedValue = weightModel.DropOrder;
                txt_WeighSetVal.Text = weightModel.WeighSetVal.ToString();
                txt_AllowError.Text = weightModel.AllowError.ToString();


                //默认信息组
                NewuModel.FormulaMaterialMDL formulaModel = materialBll.GetModel(weightModel.MaterialID);
                txt_MaterialName.Text = formulaModel.MaterialCode;
                txt_MaterialDesc.Text = formulaModel.MaterialDesc;
                cmb_Device.SelectedValue = recipeDeviceID;
                cmb_DevicePart.SelectedValue = DevicePartID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        int ComputeWeightOrder(int dropOrder)
        {
            int order = 0;
            if (List.Count == 0) { return 1; }
            foreach (NewuModel.FormulaWeighMDL item in List)
            {
                if (Convert.ToInt32( item.DropOrder) == dropOrder)
                {
                    order++;
                }
            }
            return order+1;
        }


        //验证投料次数输入是否正确
        bool ComputeDropOrder(int dropOrder)
        {
            int computerOrder = 0;
            foreach (NewuModel.FormulaWeighMDL item in List)
            {
                if (Convert.ToInt32( item.DropOrder) > computerOrder)
                {
                    computerOrder =Convert.ToInt32( item.DropOrder);
                }
            }
            computerOrder = computerOrder + 1;

            if (dropOrder > computerOrder)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
