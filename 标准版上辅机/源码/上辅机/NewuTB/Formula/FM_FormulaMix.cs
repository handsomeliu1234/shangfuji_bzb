using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newu;
using NewuCommon;
using NewuBLL;

namespace NewuTB.Formula
{
    public partial class FM_FormulaMix : Form
    {
        NewuBLL.FormulaWeighBLL formulaWeighBll = new NewuBLL.FormulaWeighBLL();
        NewuBLL.FormulaMixBLL formulaMixBll = new NewuBLL.FormulaMixBLL();
        NewuBLL.FormulaMaterialBLL formulaMaterialBll = new NewuBLL.FormulaMaterialBLL();
        NewuBLL.FormulaTechParamBLL formulaTechParamBll = new NewuBLL.FormulaTechParamBLL();

        NewuBLL.SYS_ActionControlBLL actionControl = new NewuBLL.SYS_ActionControlBLL();
        NewuBLL.SYS_ActionStepBLL actionStepBll = new NewuBLL.SYS_ActionStepBLL();
        NewuBLL.SYS_DeviceBLL deviceBll = new SYS_DeviceBLL();
        NewuBLL.SYS_DevicePartBLL devicePartBll = new NewuBLL.SYS_DevicePartBLL();
        NewuBLL.TB_BinSetingBLL binSetingBll = new NewuBLL.TB_BinSetingBLL();
        NewuBLL.SYS_TypeCodeBLL typeCodeBll = new NewuBLL.SYS_TypeCodeBLL();
        NewuBLL.SYS_TechParamBLL techParamBll = new NewuBLL.SYS_TechParamBLL();


        List<NewuModel.FormulaWeighMDL>[] RawData = new List<NewuModel.FormulaWeighMDL>[4];
        List<NewuModel.FormulaMixMDL> MixData = new List<NewuModel.FormulaMixMDL>();

        //Name为材料类型，Tag为称量部件类型
        TabPage[] RawTabPages = new TabPage[]{
                new TabPage("胶料"){ Name="Rubber", Tag=NewuGlobal.GetDevicePartCode(DevicePartType.Rubber)},
                new TabPage("炭黑"){ Name="Carbon", Tag=NewuGlobal.GetDevicePartCode(DevicePartType.Carbon)},
                new TabPage("油料"){ Name="Oil", Tag=NewuGlobal.GetDevicePartCode(DevicePartType.Oil)},
                new TabPage("药品"){ Name="Drug", Tag=NewuGlobal.GetDevicePartCode(DevicePartType.DrugMixer)}
        };

        TabPage TabPageAdd = new TabPage("  •••");
        DataGridViewEx dgv_Rubber = new DataGridViewEx() { Name = "dgv_Rubber" };

        private NewuModel.FormulaMaterialMDL CurrentFormulaMaterial;

        private string _recipeMainID = "";
        public string recipeMainID
        {
            get { return _recipeMainID; }
            set
            {
                _recipeMainID = value;
                dgvFormulaList.ClearSelection();
                for (int i = 0; i < dgvFormulaList.Rows.Count; i++)
                {
                    if (dgvFormulaList[0, i].Value.ToString() == _recipeMainID)
                    {
                        DataGridViewRow row = dgvFormulaList.Rows[i];
                        row.Selected = true;
                        dgvFormulaList.CurrentCell = row.Cells[1];
                        break;
                    }
                }
            }
        }

        private string _recipeDeviceID = "";
        public string recipeDeviceID
        {
            get { return _recipeDeviceID; }
            set
            {
                _recipeDeviceID = value;
                cmb_DeviceID.SelectedValue = _recipeDeviceID;
            }
        }



        public FM_FormulaMix()
        {
            InitializeComponent();

            addMenu();

            #region 初始化配方列表


            NewuCommon.ColStruct[] formulaListCols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("MaterialID","物料ID", Newu.ColumnType.txt,false),
                new NewuCommon.ColStruct("MaterialCode","物料编码"),
                new NewuCommon.ColStruct("VersionNo","物料版本")
            };
            dgvFormulaList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormulaList.AddCols(formulaListCols);
            dgvFormulaList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormulaList.AllowUserToAddRows = false;
            dgvFormulaList.MultiSelect = false;
            dgvFormulaList.ReadOnly = true;

            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.DataSource = deviceBll.GetListByDeviceType(DeviceType.T上辅机).Tables[0];
            if (cmb_DeviceID.Items.Count > 0) { cmb_DeviceID.SelectedIndex = 0; }

            #endregion


        }



        #region 添加和删除Tab页

        private ContextMenuStrip contextMenuStrip1 = null;

        void addMenu()
        {

            contextMenuStrip1 = new ContextMenuStrip();


            ToolStripMenuItem[] itemAddSub = new ToolStripMenuItem[]{
               new ToolStripMenuItem("炭黑"){ Tag="Carbon"},
               new ToolStripMenuItem("油料"){ Tag="Oil"},
               new ToolStripMenuItem("药品"){ Tag="Drug"}
            };
            for (int i = 0; i < itemAddSub.Length; i++)
            {
                ToolStripMenuItem menuSub1 = new ToolStripMenuItem("✚ 新增");
                ToolStripMenuItem menuSub2 = new ToolStripMenuItem("✚ 删除");
                itemAddSub[i].DropDownItems.Add(menuSub1);
                itemAddSub[i].DropDownItems.Add(menuSub2);
                menuSub1.Click += new EventHandler(FM_FormulaMix_Click);
                menuSub2.Click += new EventHandler(FM_FormulaMix_Click);
            }


            contextMenuStrip1.Items.AddRange(itemAddSub);
        }



        void FM_FormulaMix_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ToolStripMenuItem parent = menuItem.OwnerItem as ToolStripMenuItem;

            string findTypeCode = parent.Tag.ToString();


            if (menuItem.Text.IndexOf("新增") >= 0)
            {
                addType(findTypeCode, parent.Tag.ToString());
            }
            else
            {
                DelType(findTypeCode, parent.Tag.ToString());
            }

        }

        void DelType(string findType, string pageText)
        {
            TabPage page = findTabPage(findType);
            if (page != null)
            {

                page.Parent = null;
            }
            else
            {
                MessageBox.Show("不存在 [" + pageText + "] 类型");
            }
        }


        void addType(string findType, string pageText)
        {
            int index = tabControl1.SelectedIndex;
            TabPage page = findTabPage(findType);
            if (page != null)
            {
                MessageBox.Show("已存在 [" + pageText + "] 类型");
            }
            else
            {
                page = findTabPageFromArrByTypeCode(findType);
                if (page != null)
                {
                    tabControl1.TabPages.Insert(index + 1, page);
                }
            }
        }

        TabPage findTabPageFromArrByTypeCode(string typeCode)
        {
            TabPage findPage = null;
            for (int i = 0; i < RawTabPages.Length; i++)
            {
                TabPage pageItem = RawTabPages[i];

                if (pageItem.Name == typeCode)
                {
                    findPage = pageItem;
                    break;
                }
            }


            return findPage;
        }

        TabPage findTabPage(string findType)
        {
            TabPage findPage = null;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                TabPage pageItem = tabControl1.TabPages[i];

                if (pageItem.Name == findType)
                {
                    findPage = pageItem;
                    break;
                }
            }


            return findPage;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.Count - 1)
            {
                e.Cancel = true;
                Point p = MousePosition;

                p.X = p.X - PointToScreen(this.Location).X;
                p.Y = p.Y - PointToScreen(this.Location).Y;
                contextMenuStrip1.Show(this, p);
            }
        }

        #endregion


        private void FM_FormulaMix_Load(object sender, EventArgs e)
        {



            #region 初始化称量部分

            NewuCommon.ColStruct[] cols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("WeighMaterialID","称量材料",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("DevicePartID","设备部位",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("DropOrder","投料顺序",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("WeighOrder","称量顺序"),
                new NewuCommon.ColStruct("WeighSetVal","标准值"),
                new NewuCommon.ColStruct("AllowError","误差值")
            };

            RawTabPages[0].Parent = tabControl1;
            TabPageAdd.Parent = tabControl1;

            dgv_Rubber.Parent = RawTabPages[0];
            dgv_Rubber.Dock = DockStyle.Fill;
            dgv_Rubber.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Rubber.AddCols(cols);
            dgv_Rubber.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Rubber.AllowUserToAddRows = false;
            dgv_Rubber.MultiSelect = false;
            dgv_Rubber.ReadOnly = true;


            for (int i = 0; i < RawData.Length; i++)
            {
                RawData[i] = new List<NewuModel.FormulaWeighMDL>();
            }

            DataGridViewComboBoxColumn dgvDropOrder = (DataGridViewComboBoxColumn)dgv_Rubber.Columns["DropOrder"];
            dgvDropOrder.DataSource = formulaWeighBll.DropTable();
            dgvDropOrder.DisplayMember = "name";
            dgvDropOrder.ValueMember = "value";

            DataGridViewComboBoxColumn dgvDevicePartID = (DataGridViewComboBoxColumn)dgv_Rubber.Columns["DevicePartID"];
            dgvDevicePartID.DataSource = devicePartBll.getDevicePartListByDeviceID("").Tables[0];
            dgvDevicePartID.DisplayMember = "DevicePartName";
            dgvDevicePartID.ValueMember = "DevicePartID";



            DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgv_Rubber.Columns["WeighMaterialID"];
            dgvWeighMaterialID.DataSource = formulaMaterialBll.GetList("DeviceID='" + recipeDeviceID + "' or DeviceID=''").Tables[0];
            dgvWeighMaterialID.DisplayMember = "MaterialCode";
            dgvWeighMaterialID.ValueMember = "MaterialID";



            toolStripMenuItemAdd.Click += new EventHandler(MixmenuItem_Click);
            toolStripMenuItemEdit.Click += new EventHandler(MixmenuItem_Click);
            toolStripMenuItemDel.Click += new EventHandler(MixmenuItem_Click);
            toolStripMenuItemDown.Click += new EventHandler(MixmenuItem_Click);
            toolStripMenuItemUp.Click += new EventHandler(MixmenuItem_Click);
            toolStripMenuItemInsert.Click += new EventHandler(MixmenuItem_Click);


            #endregion

            #region 初始化密炼部分

            //工艺步骤所属的设备部件
            tabPageStepUp.Tag = NewuGlobal.GetDevicePartCode(DevicePartType.MixUp);

            NewuCommon.ColStruct[] mixCols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("StepOrder","步骤顺序",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("ActionControlCode","控制方式",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("StepTime","时间"),
                new NewuCommon.ColStruct("StepTemp","温度"),
                new NewuCommon.ColStruct("StepPower","功率"),
                new NewuCommon.ColStruct("StepEnergy","能量"),
                new NewuCommon.ColStruct("StepDesc","工艺步骤"),
                new NewuCommon.ColStruct("StepPress","压力"),
                new NewuCommon.ColStruct("StepSpeed","转速")
                //new NewuCommon.ColStruct("KeepTime","保持时间"),
            };
            dgvMix.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMix.AllowUserToResizeColumns = true;
            dgvMix.AddCols(mixCols);
            dgvMix.AllowUserToAddRows = false;
            dgvMix.MultiSelect = false;
            dgvMix.ReadOnly = true;

            DataGridViewComboBoxColumn dgvActionControlCode = dgvMix.GetComboBoxColumn("ActionControlCode");
            dgvActionControlCode.DataSource = actionControl.GetList("DeviceID='" + recipeDeviceID + "' or DeviceID=''").Tables[0];
            dgvActionControlCode.DisplayMember = "ActionControlNameCN";
            dgvActionControlCode.ValueMember = "ActionControlCode";

            DataGridViewComboBoxColumn dgvStepOrder = dgvMix.GetComboBoxColumn("StepOrder");
            dgvStepOrder.DataSource = actionStepBll.GetStepOrderTable();
            dgvStepOrder.DisplayMember = "name";
            dgvStepOrder.ValueMember = "value";

            #endregion


            #region 初始化工艺参数部分

            NewuCommon.ColStruct[] mixTechParamCols = new NewuCommon.ColStruct[]{
                new NewuCommon.ColStruct("TechParamID","参数名称",Newu.ColumnType.cmb,true),
                new NewuCommon.ColStruct("Unit","单位"),
                new NewuCommon.ColStruct("TechParamVal","参数值")
            };
            dgvTechParam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTechParam.AddCols(mixTechParamCols);
            dgvTechParam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTechParam.AllowUserToAddRows = false;
            dgvTechParam.MultiSelect = false;
            dgvTechParam.ReadOnly = false;
            dgvTechParam.Columns["TechParamID"].ReadOnly = true;
            dgvTechParam.Columns["Unit"].ReadOnly = true;

            string devicePartID = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
            DataGridViewComboBoxColumn dgvTechParamID = dgvTechParam.GetComboBoxColumn("TechParamID");
            dgvTechParamID.DataSource = techParamBll.GetList(recipeDeviceID, devicePartID).Tables[0];
            dgvTechParamID.DisplayMember = "TechParamNameCN";
            dgvTechParamID.ValueMember = "TechParamID";

            #endregion



        }



        void MixmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            string menuType = menu.Name;

            switch (menuType)
            {
                case "toolStripMenuItemAdd":
                    MixAdd();
                    break;
                case "toolStripMenuItemEdit":
                    MixEdit();
                    break;
                case "toolStripMenuItemDel":
                    MixDelete();
                    break;
                case "toolStripMenuItemInsert":
                    MixInset();
                    break;
                case "toolStripMenuItemUp":
                    MixUP();
                    break;
                case "toolStripMenuItemDown":
                    MixDown();
                    break;
                default:
                    break;
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            int PageIndex = 0;
            for (int i = 0; i < RawTabPages.Length; i++)
            {
                if (tabControl1.SelectedTab.Tag == RawTabPages[i].Tag)
                {
                    PageIndex = i;
                }
            }

            dgv_Rubber.DataSource = new BindingList<NewuModel.FormulaWeighMDL>(RawData[PageIndex]);
            dgv_Rubber.Parent = tabControl1.SelectedTab;
            dgv_Rubber.Dock = DockStyle.Fill;
        }

        private void getData()
        {

            //获取称量数据
            string[] devicePartId = new string[]{
                NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.Rubber)),
                NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.Carbon)),
                NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.Oil)),
                NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.DrugMixer))
            };

            formulaWeighBll.GetWeightGroup(recipeMainID, devicePartId, RawData);
            //RawData[0] = formulaWeighBll.GetModelList("MaterialID='" + recipeMainID + "' and DevicePartID='" + devicePartId[0] + "'");
            //RawData[1] = formulaWeighBll.GetModelList("MaterialID='" + recipeMainID + "' and DevicePartID='" + devicePartId[1] + "'");
            //RawData[2] = formulaWeighBll.GetModelList("MaterialID='" + recipeMainID + "' and DevicePartID='" + devicePartId[2] + "'");
            //RawData[3] = formulaWeighBll.GetModelList("MaterialID='" + recipeMainID + "' and DevicePartID='" + devicePartId[3] + "'");


            //for (int i = 2; i < tabControl1.TabPages.Count; i++)
            //{
            //    //if (i > 0)
            //    //    if (i > 0 && i < tabControl1.TabPages.Count - 1)
            //    //    {
            //    tabControl1.TabPages.RemoveAt(1);
            //            i--;
            //        //}
            //}

            while (tabControl1.TabPages.Count > 2)
            {
                try
                {
                    tabControl1.TabPages.RemoveAt(1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            for (int i = 1; i < RawData.Length; i++)
            {
                if (RawData[i].Count > 0)
                {
                    tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, RawTabPages[i]);
                }
            }
            if (tabControl1.TabPages.Count >= 2) { tabControl1_Selected(null, null); }

            //获取密炼数据
            MixData = formulaMixBll.GetModelList("MaterialID='" + recipeMainID + "' order by StepOrder");
            dgvMix.DataSource = MixData;

            //获取工艺系统参数
            string mixDevicePart = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
            dgvTechParam.DataSource = formulaTechParamBll.GetListJoinSysTech(recipeMainID, recipeDeviceID, mixDevicePart);

        }

        #region 称量（增删改）

        private void btn_Add_Click(object sender, EventArgs e)
        {


            int PageIndex = tabControl1.SelectedIndex;
            TabPage selectPage = tabControl1.SelectedTab;
            string devicePartId = NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(selectPage.Tag.ToString());

            NewuModel.FormulaWeighMDL modelWeight = new NewuModel.FormulaWeighMDL();
            modelWeight.MaterialID = recipeMainID;

            FM_FormulaMix_AddRaw fm = new FM_FormulaMix_AddRaw(modelWeight, recipeDeviceID, selectPage.Name, devicePartId, RawData[PageIndex]);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                RawData[PageIndex].Add(modelWeight);
                dgv_Rubber.DataSource = new BindingList<NewuModel.FormulaWeighMDL>(RawData[PageIndex]);

            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {


            TabPage selectPage = tabControl1.SelectedTab;
            int PageIndex = tabControl1.SelectedIndex;
            int RowIndex = GetSelectedRowIndex(this.dgv_Rubber);
            string devicePartId = NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(selectPage.Tag.ToString());

            if (RowIndex < 0 || RawData[PageIndex].Count <= RowIndex) { return; }

            NewuModel.FormulaWeighMDL newRow = RawData[PageIndex][RowIndex];

            FM_FormulaMix_AddRaw fm = new FM_FormulaMix_AddRaw(newRow, recipeDeviceID, selectPage.Name, devicePartId, RawData[PageIndex]);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                dgv_Rubber.DataSource = new BindingList<NewuModel.FormulaWeighMDL>(RawData[PageIndex]);
            }

        }

        private void btn_Up_Click(object sender, EventArgs e)
        {

            int PageIndex = tabControl1.SelectedIndex;
            int RowIndex = GetSelectedRowIndex(this.dgv_Rubber);
            if (RowIndex >= 1)
            {
                // 拷贝选中的行  
                NewuModel.FormulaWeighMDL newRow = RawData[PageIndex][RowIndex];

                // 删除选中的行  
                RawData[PageIndex].RemoveAt(RowIndex);

                // 将拷贝的行，插入到选中的上一行位置  
                RawData[PageIndex].Insert(RowIndex - 1, newRow);

                // 选中最初选中的行  
                dgv_Rubber.Rows[RowIndex - 1].Selected = true;
            }

            dgv_Rubber.Refresh();

        }

        private void btn_Down_Click(object sender, EventArgs e)
        {

            int PageIndex = tabControl1.SelectedIndex;
            int RowIndex = GetSelectedRowIndex(this.dgv_Rubber);

            if (RowIndex < dgv_Rubber.Rows.Count - 1)
            {

                NewuModel.FormulaWeighMDL newRow = RawData[PageIndex][RowIndex];

                RawData[PageIndex].RemoveAt(RowIndex);

                RawData[PageIndex].Insert(RowIndex + 1, newRow);

                dgv_Rubber.Rows[RowIndex + 1].Selected = true;
            }
            dgv_Rubber.Refresh();
        }

        private int GetSelectedRowIndex(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                return 0;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Selected)
                {
                    return row.Index;
                }
            }
            return 0;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {

            int PageIndex = tabControl1.SelectedIndex;
            int RowIndex = GetSelectedRowIndex(this.dgv_Rubber);
            if (dgv_Rubber.Rows.Count > 0)
            {

                RawData[PageIndex].RemoveAt(RowIndex);
                dgv_Rubber.DataSource = new BindingList<NewuModel.FormulaWeighMDL>(RawData[PageIndex]);

            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {

            int PageIndex = tabControl1.SelectedIndex;
            int RowIndex = GetSelectedRowIndex(this.dgv_Rubber);
            TabPage selectPage = tabControl1.SelectedTab;
            string devicePartId = NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(selectPage.Tag.ToString());

            NewuModel.FormulaWeighMDL modelWeight = new NewuModel.FormulaWeighMDL();
            modelWeight.MaterialID = recipeMainID;

            FM_FormulaMix_AddRaw fm = new FM_FormulaMix_AddRaw(modelWeight, recipeDeviceID, selectPage.Name, devicePartId, RawData[PageIndex]);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                RawData[PageIndex].Insert(RowIndex, modelWeight);
                dgv_Rubber.DataSource = new BindingList<NewuModel.FormulaWeighMDL>(RawData[PageIndex]);
            }
        }

        #endregion


        #region 密炼（增删改）

        private void MixAdd()
        {
            if (CurrentFormulaMaterial == null) return;

            string devicePartId = NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(tabPageStepUp.Tag.ToString());

            NewuModel.FormulaMixMDL modelMix = new NewuModel.FormulaMixMDL();
            modelMix.MaterialID = recipeMainID;
            modelMix.MaterialCode = CurrentFormulaMaterial.MaterialCode;
            modelMix.DeviceID = recipeDeviceID;
            modelMix.DeviceCode = NewuBLL.NewuGlobal.DeviceCodeByID(recipeDeviceID);

            FM_FormulaMix_AddStep fm = new FM_FormulaMix_AddStep(modelMix, recipeDeviceID, devicePartId, MixData);

            if (fm.ShowDialog() == DialogResult.OK)
            {

                MixData.Add(modelMix);
                dgvMix.DataSource = new BindingList<NewuModel.FormulaMixMDL>(MixData);


            }
        }

        private void MixEdit()
        {
            if (CurrentFormulaMaterial == null) return;

            int RowIndex = GetSelectedRowIndex(this.dgvMix);
            string devicePartId = NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(tabPageStepUp.Tag.ToString());

            if (RowIndex < 0) { return; }


            NewuModel.FormulaMixMDL newRow = MixData[RowIndex];
            newRow.MaterialID = recipeMainID;
            newRow.MaterialCode = CurrentFormulaMaterial.MaterialCode;
            newRow.DeviceID = recipeDeviceID;
            newRow.DeviceCode = NewuBLL.NewuGlobal.DeviceCodeByID(recipeDeviceID);

            FM_FormulaMix_AddStep fm = new FM_FormulaMix_AddStep(newRow, recipeDeviceID, devicePartId, MixData);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                dgv_Rubber.DataSource = new BindingList<NewuModel.FormulaMixMDL>(MixData);
            }

        }

        private void MixUP()
        {

            int RowIndex = GetSelectedRowIndex(this.dgvMix);
            if (RowIndex >= 1)
            {
                // 拷贝选中的行  
                NewuModel.FormulaMixMDL newRow = MixData[RowIndex];

                // 删除选中的行  
                MixData.RemoveAt(RowIndex);

                // 将拷贝的行，插入到选中的上一行位置  
                MixData.Insert(RowIndex - 1, newRow);

                // 选中最初选中的行  
                dgvMix.Rows[RowIndex - 1].Selected = true;
            }

            dgvMix.Refresh();

        }

        private void MixDown()
        {

            int RowIndex = GetSelectedRowIndex(this.dgvMix);

            if (RowIndex < dgvMix.Rows.Count - 1)
            {

                NewuModel.FormulaMixMDL newRow = MixData[RowIndex];

                MixData.RemoveAt(RowIndex);

                MixData.Insert(RowIndex + 1, newRow);

                dgvMix.Rows[RowIndex + 1].Selected = true;
            }
            dgvMix.Refresh();
        }

        private void MixDelete()
        {
            int RowIndex = GetSelectedRowIndex(this.dgvMix);
            if (dgvMix.Rows.Count > 0)
            {

                MixData.RemoveAt(RowIndex);
                dgvMix.DataSource = new BindingList<NewuModel.FormulaMixMDL>(MixData);
            }
        }

        private void MixInset()
        {
            int RowIndex = GetSelectedRowIndex(this.dgvMix);
            string devicePartId = NewuBLL.NewuGlobal.GetDevicePartIDByPartCode(tabPageStepUp.Tag.ToString());

            NewuModel.FormulaMixMDL modelMix = new NewuModel.FormulaMixMDL();
            modelMix.MaterialID = recipeMainID;

            FM_FormulaMix_AddStep fm = new FM_FormulaMix_AddStep(modelMix, recipeDeviceID, devicePartId, MixData);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                MixData.Insert(RowIndex, modelMix);
                dgvMix.DataSource = MixData;
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {

            bool isAdd = true;
            bool isDelete = true;
            try
            {

                isDelete = formulaWeighBll.DeleteAll(recipeMainID);
                isDelete = formulaMixBll.DeleteAll(recipeMainID);
                isDelete = formulaTechParamBll.DeleteAll(recipeMainID);

                //保存称量数据
                for (int i = 0; i < RawData.Length; i++)
                {
                    int j = 0;
                    List<NewuModel.FormulaWeighMDL> _list = RawData[i];

                    for (j = 0; j < _list.Count; j++)
                    {
                        isAdd = formulaWeighBll.Add(_list[j]);
                        if (isAdd == false) { break; }
                    }

                    if (j < _list.Count) { break; }


                }

                //保存密炼数据
                for (int i = 0; i < MixData.Count; i++)
                {
                    formulaMixBll.Add(MixData[i]);
                }

                //保存系统工艺参数
                for (int i = 0; i < dgvTechParam.Rows.Count; i++)
                {
                    NewuModel.FormulaTechParamMDL formulaTechParamModel = new NewuModel.FormulaTechParamMDL();
                    formulaTechParamModel.MaterialID = recipeMainID;
                    formulaTechParamModel.TechParamID = dgvTechParam["TechParamID", i].Value.ToString();
                    formulaTechParamModel.TechParamVal = FunClass.vDecimal(dgvTechParam["TechParamVal", i].Value.ToString());
                    formulaTechParamBll.Add(formulaTechParamModel);
                }


            }
            catch (Exception)
            {

                MessageBox.Show("数据错误，请检查");
                return;
            }
            if (isAdd == true)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dgvMix_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && dgvMix.Rows.Count == 0)
            {
                Point p = MousePosition;

                p.X = p.X - PointToScreen(this.Location).X;
                p.Y = p.Y - PointToScreen(this.Location).Y;
                contextMenuStripMix.Show(this, p);
            }
        }

        private void dgvMix_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvMix.ClearSelection();
                dgvMix.Rows[e.RowIndex].Selected = true;

                Point p = MousePosition;

                p.X = p.X - PointToScreen(this.Location).X;
                p.Y = p.Y - PointToScreen(this.Location).Y;
                contextMenuStripMix.Show(this, p);
            }
        }


        void GetFormulaList()
        {
            if (cmb_DeviceID.SelectedIndex < 0) { return; }
            string strWhere = "DeviceID='" + cmb_DeviceID.SelectedValue.ToString() + "'";

            if (txtLikeQuery.Text != "")
            {
                strWhere += " and MaterialCode like '%" + txtLikeQuery.Text + "%' ";
            }

            dgvFormulaList.DataSource = formulaMaterialBll.GetList(strWhere).Tables[0];
        }

        private void txtLikeQuery_KeyUp(object sender, KeyEventArgs e)
        {
            GetFormulaList();
        }



        private void dgvFormulaList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = dgvFormulaList.CurrentRow.Index;
                if (dgvFormulaList.Rows.Count > 0 && rowIndex >= 0)
                {
                    _recipeMainID = dgvFormulaList[0, rowIndex].Value.ToString();
                    CurrentFormulaMaterial = formulaMaterialBll.GetModel(_recipeMainID);

                    getData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                _recipeDeviceID = cmb_DeviceID.SelectedValue.ToString();
                GetFormulaList();
            }
        }





    }
}
