using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewuBLL;
using Repository.GlobalConfig;

namespace NewuTB.Formula
{
    public partial class FM_FormulaDetail_Add : Form
    {
        private NewuBLL.TB_BinSetingBLL bllBin = new NewuBLL.TB_BinSetingBLL();
        private NewuBLL.FormulaWeighBLL bllWeigh = new FormulaWeighBLL();
        private NewuBLL.SYS_DevicePartBLL bllDevicePart = new SYS_DevicePartBLL();

        private NewuModel.FormulaWeighMDL modelWeigh;

        private NewuModel.FormulaMaterialMDL modelMaterial;
        private bool isAdd;

        public FM_FormulaDetail_Add(NewuModel.FormulaMaterialMDL _modelMater)
        {
            //新增
            InitializeComponent();
            this.Text = "配方" + "\"" + _modelMater.MaterialCode + "\"" + "新增称重物料";
            modelMaterial = _modelMater;
            isAdd = true;
        }

        public FM_FormulaDetail_Add(NewuModel.FormulaMaterialMDL _modelMater, NewuModel.FormulaWeighMDL _modelWeigh)
        {
            //编辑
            InitializeComponent();
            this.Text = "配方" + "\"" + _modelMater.MaterialCode + "\"" + "编辑称重物料";
            modelMaterial = _modelMater;
            modelWeigh = _modelWeigh;
            isAdd = false;
        }

        private void FM_FormulaDetail_Add_Load(object sender, EventArgs e)
        {
            DataSet ds = bllDevicePart.getDevicePartListByDeviceID(modelMaterial.DeviceID);
            cobDevicePart.DataSource = ds.Tables[0];
            cobDevicePart.ValueMember = "DevicePartID";
            cobDevicePart.DisplayMember = "DevicePartName";
            cobDevicePart.SelectedIndex = -1;

            DataSet binList = bllBin.GetListJoinMaterialCode(modelMaterial.DeviceID, NewuGlobal.GetTypeCodeIDByCodeName("Drug"));
            cobWeighMaterial.DataSource = binList.Tables[0];
            cobWeighMaterial.ValueMember = "MaterialID";
            cobWeighMaterial.DisplayMember = "MaterialCode";
            cobWeighMaterial.SelectedIndex = -1;

            if (!isAdd)     //编辑时会调用
            {
                cobDevicePart.SelectedValue = modelWeigh.DevicePartID;
                cobWeighMaterial.SelectedValue = modelWeigh.WeighMaterialID;
                txtAllowError.Text = modelWeigh.AllowError.ToString();
                txtWeighSetVal.Text = modelWeigh.WeighSetVal.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataVerification() == false) { return; }

            if (isAdd)
            {
                modelWeigh = new NewuModel.FormulaWeighMDL();
            }
            //配方id和名称
            modelWeigh.MaterialID = modelMaterial.MaterialID;
            modelWeigh.MaterialCode = modelMaterial.MaterialCode;

            //设定称重物料ID和名称
            modelWeigh.WeighMaterialID = cobWeighMaterial.SelectedValue.ToString();
            modelWeigh.WeighMaterialCode = cobWeighMaterial.Text.ToString().Trim();

            //设定设备部件
            modelWeigh.DevicePartID = cobDevicePart.SelectedValue.ToString();
            modelWeigh.DevicePartCode = bllDevicePart.getDevicePartListByDeviceID(modelMaterial.DeviceID).Tables[0].Rows[0]["DevicePartCode"].ToString();

            //设定所属设备
            modelWeigh.DeviceID = modelMaterial.DeviceID;
            modelWeigh.DeviceCode = modelMaterial.DeviceCode;

            //称量序号和投入顺序为0
            modelWeigh.WeighOrder = 0;
            modelWeigh.DropOrder = 0;

            if (txtAllowError.Text.Trim() != "")
            {
                modelWeigh.AllowError = decimal.Parse(txtAllowError.Text.Trim());
            }

            if (txtWeighSetVal.Text.Trim() != "")
            {
                modelWeigh.WeighSetVal = decimal.Parse(txtWeighSetVal.Text.Trim());
            }

            refreshGrid(modelWeigh);
            clearControl();
        }

        private void refreshGrid(NewuModel.FormulaWeighMDL modelWeigh)
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_FormulaDetail fm = obj as FM_FormulaDetail;
                fm.addRowDetail(modelWeigh, isAdd);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool dataVerification()
        {
            if (cobWeighMaterial.SelectedIndex < 0)
            {
                MessageBox.Show("请选择物料名称"); return false;
            }
            if (cobDevicePart.SelectedIndex < 0)
            {
                MessageBox.Show("请选择设备部位"); return false;
            }
            //if (NewuCommon.FunClass.isEmptyOrNumber(txtWeighSetVal.Text.Trim()) == 0)
            //{
            //    labWeighSetVal.Text = "标准重量应为数字"; return false;
            //}
            //if (NewuCommon.FunClass.isEmptyOrNumber(txtAllowError.Text.Trim()) == 0)
            //{
            //    labAllowError.Text = "允许公差应为数字"; return false;
            //}
            return true;
        }

        private void clearControl()
        {
            txtWeighSetVal.Text = "";
            txtAllowError.Text = "";
        }
    }
}