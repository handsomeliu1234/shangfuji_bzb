using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Newu;
using NewuBLL;
using NewuCommon;
using Microsoft.VisualBasic;
using Repository.GlobalConfig;

namespace NewuTB.Formula
{
    public partial class FM_FormulaDetail : Form
    {
        private NewuBLL.FormulaMaterialBLL formulaMaterialBll = new NewuBLL.FormulaMaterialBLL();
        private NewuBLL.SYS_DeviceBLL deviceBll = new NewuBLL.SYS_DeviceBLL();
        private NewuBLL.FormulaWeighBLL formulaWeighBll = new NewuBLL.FormulaWeighBLL();
        private NewuBLL.TB_BinSetingBLL binSetingBll = new NewuBLL.TB_BinSetingBLL();
        private NewuBLL.SYS_DevicePartBLL devicePartBll = new NewuBLL.SYS_DevicePartBLL();
        private NewuBLL.SYS_TechParamBLL techParamBll = new NewuBLL.SYS_TechParamBLL();
        private NewuBLL.FormulaTechParamBLL formulaTechParamBll = new NewuBLL.FormulaTechParamBLL();

        private NewuModel.FormulaMaterialMDL CurrentFormulaMaterial;

        private BindingList<NewuModel.FormulaWeighMDL> weighList;//作为数据源可自动刷新DataGridView

        private string deviceID = "";
        private string materialID = "";

        public FM_FormulaDetail()
        {
            InitializeComponent();
        }

        private void FM_FormulaDetail_Load(object sender, EventArgs e)
        {
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.DataSource = deviceBll.GetListByDeviceType(DeviceType.T上辅机).Tables[0];

            #region 初始化配方列表

            NewuCommon.ColStruct[] formulaListCols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("MaterialID","物料ID", Newu.ColumnType.txt,false),
                new NewuCommon.ColStruct("MaterialCode","配方名称"),
                new NewuCommon.ColStruct("VersionNo","配方版本"),
                new NewuCommon.ColStruct("Enable","是否启用",Newu.ColumnType.chk,true)
            };
            dgvFormulaList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormulaList.AddCols(formulaListCols);
            dgvFormulaList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormulaList.AllowUserToAddRows = false;
            dgvFormulaList.MultiSelect = false;
            dgvFormulaList.ReadOnly = true;

            #endregion 初始化配方列表

            #region 初始化配方详情列表

            NewuCommon.ColStruct[] cols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("WeighMaterialID","物料名称",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("DevicePartID","设备部位",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("WeighSetVal","标准重量"),
                new NewuCommon.ColStruct("AllowError","允许公差"),
                new NewuCommon.ColStruct("FormulaWeighID","ID")
            };

            dgvWeighDetail.AddCols(cols);
            dgvWeighDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWeighDetail.AllowUserToAddRows = false;
            dgvWeighDetail.ReadOnly = true;
            dgvWeighDetail.Columns[4].Visible = false;

            // 设备部位添加到ComboBoxColumn （单元格）里
            DataGridViewComboBoxColumn dgvDevicePartID = (DataGridViewComboBoxColumn)dgvWeighDetail.Columns["DevicePartID"];
            dgvDevicePartID.DataSource = devicePartBll.getDevicePartListByDeviceID(deviceID).Tables[0];
            dgvDevicePartID.DisplayMember = "DevicePartName";
            dgvDevicePartID.ValueMember = "DevicePartID";

            // 从binSet表里查
            DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgvWeighDetail.Columns["WeighMaterialID"];
            dgvWeighMaterialID.DataSource = binSetingBll.GetListJoinMaterialCode(deviceID, "").Tables[0];
            dgvWeighMaterialID.DisplayMember = "MaterialCode";
            dgvWeighMaterialID.ValueMember = "MaterialID";

            #endregion 初始化配方详情列表

            cmb_DeviceID.SelectedIndexChanged += cmb_DeviceID_SelectedIndexChanged;
            //为了确定当前DeviceID有值
            cmb_DeviceID_SelectedIndexChanged(null, null);

            #region 初始化工艺参数部分

            NewuCommon.ColStruct[] DrugTechParamCols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("TechParamID","参数名称",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("FormulaTechParamID","配方工艺参数ID",Newu.ColumnType.txt,false),
                new NewuCommon.ColStruct("Unit","单位"),
                new NewuCommon.ColStruct("TechParamVal","参数值")
            };

            dgvTechParam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTechParam.AddCols(DrugTechParamCols);
            dgvTechParam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTechParam.AllowUserToAddRows = false;
            dgvTechParam.MultiSelect = false;
            dgvTechParam.ReadOnly = false;
            dgvTechParam.Columns["TechParamID"].ReadOnly = true;
            dgvTechParam.Columns["Unit"].ReadOnly = true;

            #endregion 初始化工艺参数部分
        }

        internal void GetFormulaList()
        {
            if (cmb_DeviceID.SelectedIndex < 0) { return; }

            string strWhere = "DeviceID='" + cmb_DeviceID.SelectedValue.ToString() + "'";
            //配方信息  不选择终练或者母练信息  全部呈现
            //strWhere += " and TypeCodeID='" + NewuGlobal.GetTypeCodeIDByCodeName("XfFormula") + "' ";

            if (txtLikeQuery.Text != "")
            {
                strWhere += " and MaterialCode like '%" + txtLikeQuery.Text + "%' ";
            }
            if (radB_SY.Checked == true)
            {
                strWhere += "and Enable = '1' ";   //显示状态
            }
            else if (radB_ZF.Checked == true)
            {
                strWhere += "and Enable = '0' ";   //显示状态
            }
            //strWhere += "and Enable=1";
            dgvFormulaList.DataSource = formulaMaterialBll.GetList(strWhere).Tables[0];
            dgvWeighDetail.DataSource = null;
            //先禁止右边的按钮的操作
            btnAddDetail.Enabled = false;
            btnDeleteDetail.Enabled = false;
            btnEditDetail.Enabled = false;
        }

        private void getData()
        {
            try
            {
                List<NewuModel.FormulaWeighMDL> list = formulaWeighBll.GetListXF(materialID, cmb_DeviceID.SelectedValue.ToString());
                weighList = new BindingList<NewuModel.FormulaWeighMDL>(list);
                dgvWeighDetail.DataSource = weighList;

                //获取工艺系统参数
                // dgvTechParam.DataSource = null;
                string DevicePartID = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.DrugXf));
                DataGridViewComboBoxColumn dgvTechParamID = dgvTechParam.GetComboBoxColumn("TechParamID");
                // devicePartID列绑定数据源
                dgvTechParamID.DataSource = formulaTechParamBll.GetListJoinSysTech(materialID, deviceID, DevicePartID);
                dgvTechParamID.DisplayMember = "TechParamNameCN";
                dgvTechParamID.ValueMember = "TechParamID";
                //dataGridView绑定
                dgvTechParam.DataSource = dgvTechParamID.DataSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            bool isAdd = true;
            bool isDel = formulaWeighBll.DeleteAll(materialID);

            for (int i = 0; i < weighList.Count; i++)
            {
                try
                {
                    NewuModel.FormulaWeighMDL model = weighList[i];
                    //model.WeighMaterialCode = dgv.Rows[i].Cells[""].Value.ToString();

                    if (model.WeighMaterialID == null || model.DevicePartID == null || model.AllowError.ToString().Trim() == "" || model.WeighSetVal.ToString().Trim() == "")
                    {
                        MessageBox.Show("第" + (i + 1) + "行数据不能为空");
                        isAdd = false;
                        continue;
                    }
                    bool add = formulaWeighBll.Add(model);
                    if (add == false)
                    {
                        MessageBox.Show("第" + (i + 1) + "行数据保存失败");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.ToString().Contains("UNIQUE KEY constraint"))
                    {
                        MessageBox.Show("数据有重复，请检查");
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    isAdd = false;
                }
            }

            //保存系统工艺参数
            if (dgvTechParam.Rows.Count == 1)
            {
                bool flag = false;
                NewuModel.FormulaTechParamMDL formulaTechParamModel = new NewuModel.FormulaTechParamMDL();
                formulaTechParamModel.MaterialID = materialID;
                formulaTechParamModel.FormulaTechParamID = dgvTechParam["FormulaTechParamID", 0].Value.ToString();
                formulaTechParamModel.TechParamID = dgvTechParam["TechParamID", 0].Value.ToString();
                formulaTechParamModel.TechParamVal = FunClass.vDecimal(dgvTechParam["TechParamVal", 0].Value.ToString());
                if (formulaTechParamBll.Exists(formulaTechParamModel.FormulaTechParamID))
                {
                    flag = formulaTechParamBll.Update(formulaTechParamModel);
                }
                else
                {
                    flag = formulaTechParamBll.Add(formulaTechParamModel);
                }
                if (flag)
                {
                    MessageBox.Show("配方工艺参数保存成功");
                }
                else
                {
                    MessageBox.Show("配方工艺参数保存失败");
                }
            }
            else
            {
                MessageBox.Show("该配方设定了多个配方工艺参数，配方工艺参数保存失败");
            }
            if (isAdd == true)
            {
                MessageBox.Show("配方材料重量保存成功");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            bool isDelete = false;
            int rowIndex = dgvFormulaList.CurrentRow.Index;
            if (dgvFormulaList.Rows.Count > 0 && rowIndex >= 0)
            {
                isDelete = formulaWeighBll.DeleteAll(materialID);
                if (isDelete)
                {
                    MessageBox.Show("配方物料数据删除成功");
                }
                isDelete = formulaTechParamBll.DeleteAll(materialID);

                if (isDelete)
                {
                    MessageBox.Show("该配方总误差数据删除成功");
                }
                dgvFormulaList_CellClick(null, null);
            }
            else
            {
                if (dgvFormulaList.Rows.Count == 0)
                {
                    MessageBox.Show("没有配方");
                }
                else
                {
                    MessageBox.Show("请点击一个配方!");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFormulaList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dgvFormulaList.CurrentRow.Index;
            if (dgvFormulaList.Rows.Count > 0 && rowIndex >= 0)
            {
                materialID = dgvFormulaList["MaterialID", rowIndex].Value.ToString();
                CurrentFormulaMaterial = formulaMaterialBll.GetModel(materialID);
                getData();
            }
            btnAddDetail.Enabled = true;
            btnDeleteDetail.Enabled = true;
            btnEditDetail.Enabled = true;
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            FM_FormulaDetail_Add fm = new FM_FormulaDetail_Add(CurrentFormulaMaterial);
            fm.Owner = this;
            fm.ShowDialog();
        }

        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvWeighDetail.CurrentRow.Index;
            if (rowIndex >= 0)
            {
                FM_FormulaDetail_Add fm = new FM_FormulaDetail_Add(CurrentFormulaMaterial, weighList[rowIndex]);
                fm.Owner = this;
                fm.ShowDialog();
            }
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvWeighDetail.CurrentRow.Index;
            if (rowIndex >= 0)
            {
                weighList.RemoveAt(rowIndex);
            }
        }

        public void addRowDetail(NewuModel.FormulaWeighMDL modelweigh, bool isAddOrUpdate)
        {
            bool isExist = false;
            int count = 0;
            foreach (var i in weighList)
            {
                if (i.WeighMaterialID == modelweigh.WeighMaterialID)
                {
                    isExist = true;
                    break;
                }
                count++;
            }
            if (!isExist && isAddOrUpdate) //不存在和添加
            {
                weighList.Add(modelweigh);//添加
            }
            else if (isExist && !isAddOrUpdate)  //存在和更新
            {
                weighList.RemoveAt(count);
                weighList.Insert(count, modelweigh);
            }
            else if (isExist && isAddOrUpdate) //存在和添加
            {
                MessageBox.Show("物料-" + modelweigh.WeighMaterialCode + "-已存在");
            }
        }

        private void txtLikeQuery_TextChanged(object sender, EventArgs e)
        {
            GetFormulaList();
        }

        private void cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                deviceID = cmb_DeviceID.SelectedValue.ToString();
                GetFormulaList();
            }
        }

        private void btCopyFormula_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvFormulaList.CurrentRow.Index;
            if (rowIndex >= 0)
            {
                FM_FormulaCopy fm = new FM_FormulaCopy(CurrentFormulaMaterial);
                fm.Owner = this;
                fm.ShowDialog();
            }
        }

        private void radB_SY_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radB_ZF_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void dgvFormulaList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //配方列表左键点击第三列
            if (e.Button == MouseButtons.Left && e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string v = dgvFormulaList[3, e.RowIndex].Value.ToString();
                int a = Int32.Parse(v);
                if (a > 0)
                {
                    DialogResult rult = MessageBox.Show("是否修改当前配方使用状态为【作废】", "修改配方状态提示", MessageBoxButtons.YesNo);
                    if (rult == DialogResult.Yes)
                    {
                        string MetID1 = dgvFormulaList[0, e.RowIndex].Value.ToString();
                        string StrSql1 = "UPDATE dbo.FormulaMaterial SET Enable='0' WHERE MaterialID='" + MetID1 + "'";
                        bool isUp = formulaMaterialBll.UpdateSig(StrSql1);
                        if (isUp == false)
                        {
                            MessageBox.Show(dgvFormulaList[2, e.RowIndex].Value.ToString() + "配方状态更新失败");
                            GetFormulaList();
                        }
                        else
                        {
                            GetFormulaList();
                        }
                    }
                }
                else if (a == 0)
                {
                    DialogResult rult = MessageBox.Show("是否修改当前配方使用状态为【使用】", "修改配方状态提示", MessageBoxButtons.YesNo);
                    if (rult == DialogResult.Yes)
                    {
                        string MetID1 = dgvFormulaList[0, e.RowIndex].Value.ToString();
                        string StrSql1 = "UPDATE dbo.FormulaMaterial SET Enable='1' WHERE MaterialID='" + MetID1 + "'";
                        bool isUp = formulaMaterialBll.UpdateSig(StrSql1);
                        if (isUp == false)
                        {
                            MessageBox.Show(dgvFormulaList[2, e.RowIndex].Value.ToString() + "配方状态更新失败");
                            GetFormulaList();
                        }
                        else
                        {
                            GetFormulaList();
                        }
                    }
                }
            }
        }
    }
}