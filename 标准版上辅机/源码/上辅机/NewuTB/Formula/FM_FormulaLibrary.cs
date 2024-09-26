using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuTB.Formula
{
    public partial class FM_FormulaLibrary : Form, ILanguageChanged
    {
        // 为整改提醒保存配方加的flag点。 2018.12.6
        private bool isChange = false;

        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly FormulaWeighRepository formulaWeighRepository = new FormulaWeighRepository();
        private readonly FormulaMixRepository formulaMixRepository = new FormulaMixRepository();
        private readonly FormulaTechParamRepository formulaTechParamRepository = new FormulaTechParamRepository();

        private readonly FormulaMixFRepository formulaMixFRepository = new FormulaMixFRepository();
        private readonly FormulaTechParamFRepository formulaTechParamFRepository = new FormulaTechParamFRepository();

        private readonly SYS_TechParamFRepository techParamRepository = new SYS_TechParamFRepository();
        private readonly SYS_ActionStepRepository actionStepRepository = new SYS_ActionStepRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();

        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly TB_FormulaOperateLogRepository formulaOperateLogRepository = new TB_FormulaOperateLogRepository();
        private readonly PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private DataGridViewComboBoxColumn dgvDevicePartID;
        private DataGridViewComboBoxColumn dgvMixPartID;
        private DataGridViewComboBoxColumn dgvActionControlCode;
        private DataGridViewComboBoxColumn dgvActionControlCodeF;
        private DataGridViewComboBoxColumn dgvTechParamID;
        private DataGridViewComboBoxColumn dgvTechParamFID;
        private List<FormulaMaterial> formulaMaterials;
        private List<SYS_TechParam> sYS_TechParams;
        private List<SYS_DevicePart> sYS_DeviceParts;

        private List<FormulaWeigh> RawData = new List<FormulaWeigh>();
        private List<FormulaMix> MixData = new List<FormulaMix>();

        /// <summary>
        /// 只用作判断下密炼工艺步骤是否对应，不参与保存数据到数据库
        /// </summary>
        private List<FormulaWeigh> RawDataF = new List<FormulaWeigh>();

        private List<FormulaMixF> MixFData = new List<FormulaMixF>();

        private DataGridViewComboBoxColumn dgvWeighRubber;
        private readonly TransFromulaUtil transInstance = new TransFromulaUtil();

        private FormulaMaterial modelCopy = new FormulaMaterial();//接收添加配方界面传回来的model，用于复制配方时修改里面的值

        private List<SYS_TechParam> formulaTechParams;
        private List<SYS_TechParam> formulaTechParamFs;

        private FormulaMaterial CurrentFormulaMaterial;
        private List<FormulaMaterial> formulaList = null;
        private int formulaRowIndex;

        private string _recipeMainID = "";
        private string mixerType;
        private string upMixPartID;
        private string downMixPartID;

        public string RecipeMainID
        {
            get
            {
                return _recipeMainID;
            }
            set
            {
                _recipeMainID = value;
                dgvFormulaList.ClearSelection();
                for (int i = 0; i < dgvFormulaList.Rows.Count; i++)
                {
                    if (dgvFormulaList[1, i].Value.ToString() == _recipeMainID)
                    {
                        DataGridViewRow row = dgvFormulaList.Rows[i];
                        row.Selected = true;
                        dgvFormulaList.CurrentCell = row.Cells[2];
                        break;
                    }
                }
            }
        }

        private string _recipeDeviceID = "";

        public string RecipeDeviceID
        {
            get
            {
                return _recipeDeviceID;
            }
            set
            {
                _recipeDeviceID = value;
                cmb_DeviceID.SelectedValue = _recipeDeviceID;
            }
        }

        public FM_FormulaLibrary()
        {
            InitializeComponent();

            #region 初始化配方列表

            ColStruct[] formulaListCols = new ColStruct[]{
                new ColStruct("MaterialID","物料ID", ColumnType.txt,false),
                new ColStruct("MaterialCode","物料编码"),
                new ColStruct("VersionNo","物料版本"),
                new ColStruct("Enable","是否启用", ColumnType.chk,true)
            };

            dgvFormulaList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormulaList.AddCols(formulaListCols);
            dgvFormulaList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormulaList.AllowUserToAddRows = false;
            dgvFormulaList.MultiSelect = false;
            dgvFormulaList.ReadOnly = true;
            dgvFormulaList.Columns[2].Width = 100;

            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.DataSource = deviceRepository.GetListByDeviceType(DeviceType.T上辅机);
            if (cmb_DeviceID.Items.Count > 0)
            {
                cmb_DeviceID.SelectedIndex = 0;
            }
            radB_SY.Location = new Point(33, 155);
            radB_ZF.Location = new Point(151, 155);

            #endregion 初始化配方列表
        }

        private void FM_FormulaLibrary_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < formulaList.Count; i++)
            {
                string materialID = formulaList[i].MaterialID.ToString();
                if (materialID.Equals(NewuGlobal.Now_MaterialID))
                {
                    dgvFormulaList.Rows[i].Selected = true;
                }
            }

            cmbRuby.DisplayMember = "Name";
            cmbRuby.ValueMember = "Value";
            cmbRuby.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRuby.SelectedValue = 1;

            cmbScanner.DisplayMember = "names";
            cmbScanner.ValueMember = "values";
            cmbScanner.DropDownStyle = ComboBoxStyle.DropDownList;

            InitDgv();
            SetControlLanguageText();
            GetData();

            if (!NewuGlobal.SoftConfig.DownMixer)
                tabControl2.TabPages.Remove(tabPageStepDown);

            isChange = false;
            Authentication();
        }

        private void InitDgv()
        {
            try
            {
                //获取物料库中所有数据（配方和物料）
                formulaMaterials = formulaMaterialRepository.GetList(" Enable=1 ");

                #region 初始化称量部分

                ColStruct[] cols = new ColStruct[]
                {
                    new ColStruct("","",ColumnType.txt,false),
                    new ColStruct("WeighMaterialID","称量材料",ColumnType.cmb,true),
                    new ColStruct("DevicePartID","设备部位",ColumnType.cmb,true),
                    new ColStruct("MixPartID","密炼部位",ColumnType.cmb,true),
                    new ColStruct("DropOrder","投料顺序",ColumnType.cmb,true),
                    new ColStruct("WeighOrder","称量顺序"),
                    new ColStruct("WeighSetVal","标准值"),
                    new ColStruct("AllowError","误差值"),
                    new ColStruct("Reserve2","快称值"),
                    new ColStruct("Reserve1","提前量"),
                    new ColStruct("Rubber","供胶机",ColumnType.cmb,true),
                    new ColStruct("Scanner","扫描枪",ColumnType.chk,true)
                };

                dgvWeight.Dock = DockStyle.Fill;
                dgvWeight.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvWeight.AddCols(cols);
                dgvWeight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvWeight.MultiSelect = false;

                for (int i = 0; i < dgvWeight.Columns.Count; i++)
                {
                    dgvWeight.Columns[i].ReadOnly = true;
                }

                dgvWeight.Columns[0].Width = 50;
                dgvWeight.Columns[1].FillWeight = 150;
                dgvWeight.Columns[3].FillWeight = 100;
                dgvWeight.Columns[4].FillWeight = 70;
                DataGridViewComboBoxColumn dgvDropOrder = (DataGridViewComboBoxColumn)dgvWeight.Columns["DropOrder"];
                dgvDropOrder.DataSource = formulaWeighRepository.DropTable();
                dgvDropOrder.DisplayMember = "name";
                dgvDropOrder.ValueMember = "value";

                cmb_DropOrder.DataSource = formulaWeighRepository.DropTable();
                cmb_DropOrder.DisplayMember = "name";
                cmb_DropOrder.ValueMember = "value";

                sYS_DeviceParts = NewuGlobal.DevicePartList;
                upMixPartID = sYS_DeviceParts.Find(s => s.PartNum == 9).DevicePartID;
                downMixPartID = sYS_DeviceParts.Find(s => s.PartNum == 10).DevicePartID;

                dgvDevicePartID = (DataGridViewComboBoxColumn)dgvWeight.Columns["DevicePartID"];
                dgvDevicePartID.DataSource = sYS_DeviceParts;
                dgvDevicePartID.ValueMember = "DevicePartID";

                dgvMixPartID = (DataGridViewComboBoxColumn)dgvWeight.Columns["MixPartID"];
                dgvMixPartID.DataSource = sYS_DeviceParts;
                dgvMixPartID.ValueMember = "DevicePartID";

                cmb_DevicePart.DataSource = NewuGlobal.DevicePartList.FindAll(d => d.Enable == 1 && d.PartNum != NewuGlobal.MixerPartNum && d.PartNum != NewuGlobal.DownMixerPartNum);
                cmb_DevicePart.ValueMember = "DevicePartID";
                cmb_DevicePart.SelectedIndexChanged += Cmb_DevicePart_SelectedIndexChanged;

                cmb_Mix.DataSource = NewuGlobal.DevicePartList.FindAll(d => d.PartNum == 9 || d.PartNum == 10).OrderBy(d => d.PartNum).ToList();
                cmb_Mix.ValueMember = "DevicePartID";

                DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgvWeight.Columns["WeighMaterialID"];
                List<FormulaMaterial> weightMaterial = formulaMaterials.FindAll(e => e.DeviceID.Equals(RecipeDeviceID) || e.DeviceID.Equals(""));
                dgvWeighMaterialID.DataSource = weightMaterial;
                dgvWeighMaterialID.DisplayMember = "MaterialCode";
                dgvWeighMaterialID.ValueMember = "MaterialID";

                dgvWeighRubber = (DataGridViewComboBoxColumn)dgvWeight.Columns["Rubber"];
                dgvWeighRubber.DisplayMember = "Name";
                dgvWeighRubber.ValueMember = "Value";

                toolStripMenuItemAdd.Click += new EventHandler(MixmenuItem_Click);
                toolStripMenuItemEdit.Click += new EventHandler(MixmenuItem_Click);
                toolStripMenuItemDel.Click += new EventHandler(MixmenuItem_Click);
                toolStripMenuItemDown.Click += new EventHandler(MixmenuItem_Click);
                toolStripMenuItemUp.Click += new EventHandler(MixmenuItem_Click);
                toolStripMenuItemInsert.Click += new EventHandler(MixmenuItem_Click);

                #endregion 初始化称量部分

                #region 初始化密炼部分

                //工艺步骤所属的设备部件
                tabPageStepUp.Tag = NewuGlobal.GetDevicePartCode(DevicePartType.MixUp);
                tabPageStepDown.Tag = NewuGlobal.GetDevicePartCode(DevicePartType.MixDown);

                ColStruct[] mixCols = new ColStruct[]
                {
                    new ColStruct("StepOrder","步骤顺序",ColumnType.cmb,true),
                    new ColStruct("ActionControlCode","控制方式",ColumnType.cmb,true),
                    new ColStruct("StepTime","时间"),
                    new ColStruct("StepDesc","工艺步骤"),
                    new ColStruct("StepTemp","温度"),
                    new ColStruct("StepPower","功率"),
                    new ColStruct("StepEnergy","能量"),
                    new ColStruct("StepPress","压力"),
                    new ColStruct("StepSpeed","转速")
                };

                dgvMixUp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMixUp.AllowUserToResizeColumns = true;
                dgvMixUp.AddCols(mixCols);
                dgvMixUp.Columns[3].ReadOnly = true;
                dgvMixUp.MultiSelect = false;
                dgvMixUp.Columns[1].FillWeight = 100;
                dgvMixUp.Columns[2].FillWeight = 50;
                dgvMixUp.Columns[3].FillWeight = 270;

                List<SYS_ActionControl> sYS_ActionControls = NewuGlobal.ActionControlList;
                dgvActionControlCode = dgvMixUp.GetComboBoxColumn("ActionControlCode");
                dgvActionControlCode.DataSource = sYS_ActionControls;
                dgvActionControlCode.ValueMember = "ActionControlCode";

                DataGridViewComboBoxColumn dgvStepOrder = dgvMixUp.GetComboBoxColumn("StepOrder");
                dgvStepOrder.DataSource = actionStepRepository.GetStepOrderTable();
                dgvStepOrder.DisplayMember = "name";
                dgvStepOrder.ValueMember = "value";

                dgvMixDown.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMixDown.AllowUserToResizeColumns = true;
                dgvMixDown.AddCols(mixCols);
                dgvMixDown.Columns[3].ReadOnly = true;
                dgvMixDown.MultiSelect = false;
                dgvMixDown.Columns[1].FillWeight = 100;
                dgvMixDown.Columns[2].FillWeight = 50;
                dgvMixDown.Columns[3].FillWeight = 270;

                List<SYS_ActionControl> actionControls = new List<SYS_ActionControl>();
                actionControls.AddRange(NewuGlobal.ActionControlList);
                dgvActionControlCodeF = dgvMixDown.GetComboBoxColumn("ActionControlCode");
                dgvActionControlCodeF.DataSource = actionControls;
                dgvActionControlCodeF.ValueMember = "ActionControlCode";

                DataGridViewComboBoxColumn dgvStepOrderF = dgvMixDown.GetComboBoxColumn("StepOrder");
                dgvStepOrderF.DataSource = actionStepRepository.GetStepOrderTable();
                dgvStepOrderF.DisplayMember = "name";
                dgvStepOrderF.ValueMember = "value";

                /*
                 *todo:配置能量表小数位数
                 */
                dgvMixUp.Columns["StepTime"].DefaultCellStyle.Format = "0";
                dgvMixUp.Columns["StepTemp"].DefaultCellStyle.Format = "0.0";
                dgvMixUp.Columns["StepPower"].DefaultCellStyle.Format = "0";
                dgvMixUp.Columns["StepEnergy"].DefaultCellStyle.Format = "0.0";
                dgvMixUp.Columns["StepPress"].DefaultCellStyle.Format = "0.00";
                dgvMixUp.Columns["StepSpeed"].DefaultCellStyle.Format = "0.0";

                dgvMixDown.Columns["StepTime"].DefaultCellStyle.Format = "0";
                dgvMixDown.Columns["StepTemp"].DefaultCellStyle.Format = "0.0";
                dgvMixDown.Columns["StepPower"].DefaultCellStyle.Format = "0";
                dgvMixDown.Columns["StepEnergy"].DefaultCellStyle.Format = "0.0";
                dgvMixDown.Columns["StepPress"].DefaultCellStyle.Format = "0.00";
                dgvMixDown.Columns["StepSpeed"].DefaultCellStyle.Format = "0.0";

                #endregion 初始化密炼部分

                #region 初始化工艺参数部分

                ColStruct[] mixTechParamCols = new ColStruct[]
                {
                    new ColStruct("TechParamID","参数名称",ColumnType.cmb,true),
                    new ColStruct("Unit","单位"),
                    new ColStruct("TechParamVal","参数值")
                };

                dgvTechParamUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTechParamUp.AddCols(mixTechParamCols);
                dgvTechParamUp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTechParamUp.AllowUserToAddRows = false;
                dgvTechParamUp.MultiSelect = false;

                dgvTechParamDown.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTechParamDown.AddCols(mixTechParamCols);
                dgvTechParamDown.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTechParamDown.AllowUserToAddRows = false;
                dgvTechParamDown.MultiSelect = false;

                string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
                sYS_TechParams = techParamRepository.GetList("DevicepartID='" + devicePartId + "' and Enable = 1");
                dgvTechParamID = dgvTechParamUp.GetComboBoxColumn("TechParamID");
                dgvTechParamID.DataSource = sYS_TechParams;
                dgvTechParamID.ValueMember = "TechParamID";

                dgvTechParamUp.ReadOnly = false;
                dgvTechParamUp.Columns["TechParamID"].ReadOnly = true;
                dgvTechParamUp.Columns["Unit"].ReadOnly = true;

                string devicePartFId = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixDown));
                dgvTechParamFID = dgvTechParamDown.GetComboBoxColumn("TechParamID");
                dgvTechParamFID.DataSource = techParamRepository.GetList("DevicepartID='" + devicePartFId + "' and Enable = 1");
                dgvTechParamFID.ValueMember = "TechParamID";

                dgvTechParamDown.ReadOnly = false;
                dgvTechParamDown.Columns["TechParamID"].ReadOnly = true;
                dgvTechParamDown.Columns["Unit"].ReadOnly = true;

                #endregion 初始化工艺参数部分
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void Authentication()
        {
            try
            {
                SYS_Menu menu = Parent.Tag as SYS_Menu;
                if (menu != null)
                {
                    string menuID = menu.MenuID;
                    txt_WeighSetVal.ReadOnly = !PrivilegeAuthentication.Authentication(menuID, "txt_WeighSetVal");
                    txt_AllowError.ReadOnly = !PrivilegeAuthentication.Authentication(menuID, "txt_AllowError");
                    cmb_DevicePart.Enabled = PrivilegeAuthentication.Authentication(menuID, "cmb_DevicePart");
                    cmb_WeighMaterial.Enabled = PrivilegeAuthentication.Authentication(menuID, "cmb_WeighMaterial");
                    cmb_DropOrder.Enabled = PrivilegeAuthentication.Authentication(menuID, "cmb_DropOrder");
                    btn_Add.Enabled = PrivilegeAuthentication.Authentication(menuID, "btn_Add");
                    btn_Edit.Enabled = PrivilegeAuthentication.Authentication(menuID, "btn_Edit");
                    btn_Delete.Enabled = PrivilegeAuthentication.Authentication(menuID, "btn_Delete");
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void MixmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            string menuType = menu.Name;

            switch (menuType)
            {
                case "toolStripMenuItemAdd":
                    isChange = true;
                    if (mixerType.Equals("UpMixer"))
                        MixAdd();
                    else
                        MixFAdd();
                    break;

                case "toolStripMenuItemEdit":
                    isChange = true;
                    if (mixerType.Equals("UpMixer"))
                        MixEdit();
                    else
                        MixFEdit();
                    break;

                case "toolStripMenuItemDel":
                    isChange = true;
                    if (mixerType.Equals("UpMixer"))
                        MixDelete();
                    else
                        MixFDelete();
                    break;

                case "toolStripMenuItemInsert":
                    if (mixerType.Equals("UpMixer"))
                        MixInset();
                    else
                        MixFInset();
                    break;

                case "toolStripMenuItemUp":
                    if (mixerType.Equals("UpMixer"))
                        MixUP();
                    else
                        MixFUP();
                    break;

                case "toolStripMenuItemDown":
                    if (mixerType.Equals("UpMixer"))
                        MixDown();
                    else
                        MixFDown();
                    break;

                default:
                    break;
            }
        }

        private void GetData()
        {
            try
            {
                if (dgvFormulaList.CurrentRow == null)
                    return;

                formulaRowIndex = 0;

                for (int i = 0; i < formulaList.Count; i++)
                {
                    if (dgvFormulaList.Rows[i].Selected == true)
                    {
                        formulaRowIndex = i;
                        break;
                    }
                }

                if (dgvFormulaList.Rows.Count > 0 && formulaRowIndex >= 0)
                {
                    _recipeMainID = dgvFormulaList.Rows[formulaRowIndex].Cells["MaterialID"].Value.ToString();
                    CurrentFormulaMaterial = formulaMaterialRepository.GetModel(_recipeMainID);
                }

                List<FormulaWeigh> downMixWeighs = null;
                Task<bool> task = Task.Run(() =>
                {
                    RawData = formulaWeighRepository.GetModelListNew(0, "MaterialID='" + RecipeMainID + "'", "DevicePartID,DropOrder,WeighOrder");
                    MixData = formulaMixRepository.GetList("MaterialID='" + RecipeMainID + "' order by StepOrder");
                    downMixWeighs = RawData.FindAll(r => r.MixPartID.Equals(downMixPartID));
                    string mixDevicePart = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
                    formulaTechParams = formulaTechParamRepository.GetListJoinSysTech(RecipeMainID, RecipeDeviceID, mixDevicePart);

                    if (NewuGlobal.SoftConfig.DownMixer)
                    {
                        MixFData = formulaMixFRepository.GetList(RecipeMainID);
                        string mixFDevicePart = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixDown));
                        formulaTechParamFs = formulaTechParamFRepository.GetListJoinSysTechF(RecipeMainID, RecipeDeviceID, mixFDevicePart);
                    }
                    return true;
                });

                if (task.Result)
                {
                    DisplayData(RawData);
                    dgvMixUp.DataSource = MixData;
                    //选择配方时去除默认选中行 20231013
                    if (MixData.Count > 0)
                    {
                        dgvMixUp.Rows[0].Selected = false;
                    }

                    dgvTechParamUp.DataSource = new BindingList<SYS_TechParam>(formulaTechParams);
                    //选择配方时去除默认选中行   20231013
                    if (formulaTechParams.Count > 0)
                    {
                        dgvTechParamUp.Rows[0].Selected = false;
                    }
                    Check_match(true);

                    if (NewuGlobal.SoftConfig.DownMixer)
                    {
                        RawDataF.Clear();
                        if (downMixWeighs != null)
                            RawDataF.AddRange(downMixWeighs);
                        dgvMixDown.DataSource = MixFData;

                        if (MixFData.Count > 0)
                        {
                            dgvMixDown.Rows[0].Selected = false;
                        }
                        dgvTechParamDown.DataSource = new BindingList<SYS_TechParam>(formulaTechParamFs);
                        if (formulaTechParamFs.Count > 0)
                        {
                            dgvTechParamDown.Rows[0].Selected = false;
                        }
                        Check_match(false);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void DisplayData(List<FormulaWeigh> list)
        {
            foreach (FormulaWeigh item in list)
            {
                int digit = ScaleAccuracy.GetDigitByPartCode(item.DevicePartCode);
                item.WeighSetVal = decimal.Round(item.WeighSetVal, digit);
                item.AllowError = decimal.Round(item.AllowError, digit);

                if (item.Reserve1.Contains("."))
                {
                    switch (digit)
                    {
                        case 1:
                            item.Reserve1 = string.Format("{0:.1f}", item.Reserve1);
                            break;

                        case 2:
                            item.Reserve1 = string.Format("{0:.2f}", item.Reserve1);
                            break;

                        case 3:
                            item.Reserve1 = string.Format("{0:.3f}", item.Reserve1);
                            break;

                        default:
                            break;
                    }
                }
            }
            dgvWeight.DataSource = new BindingList<FormulaWeigh>(list);
            dgvWeight.Dock = DockStyle.Fill;
        }

        #region 称量（增删改）

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                isChange = true;

                int dropOrder = Convert.ToInt32(cmb_DropOrder.SelectedValue);
                if (!DataVerification(dropOrder, true))
                    return;

                string materialCode = dgvFormulaList.Rows[formulaRowIndex].Cells["MaterialCode"].Value.ToString();//配方名
                string devicePartID = cmb_DevicePart.SelectedValue.ToString();//称量设备部位ID
                string devicePartCode = NewuGlobal.DevicePartCodeByID(devicePartID);//称量设备部位Code
                string mixPartID = cmb_Mix.SelectedValue.ToString();//所属密炼机部位ID
                string deviceID = cmb_DeviceID.SelectedValue.ToString();
                string deviceCode = NewuGlobal.DeviceCodeByID(deviceID);
                string weighMaterialID = cmb_WeighMaterial.SelectedValue.ToString();
                string weighMaterialCode = cmb_WeighMaterial.Text;
                decimal setWeight = decimal.Parse(txt_WeighSetVal.Text);
                decimal allowError = decimal.Parse(txt_AllowError.Text);
                decimal quickValue = decimal.Parse(txt_SetKuai.Text);
                decimal tiQianValue = decimal.Parse(txt_SetTiQian.Text);

                int weightOrder = ComputeWeightOrder(dropOrder, cmb_DevicePart.SelectedValue.ToString());
                int useScanner = int.Parse(cmbScanner.SelectedValue.ToString());
                int useRuby = int.Parse(cmbRuby.SelectedValue.ToString());

                //mixPartID找到唯一识别字段PartNum
                int partNum = sYS_DeviceParts.Find(s => s.DevicePartID.Equals(mixPartID)).PartNum;

                //上密炼机
                FormulaWeigh formulaWeigh = new FormulaWeigh
                {
                    MaterialID = RecipeMainID,
                    MaterialCode = materialCode,
                    DevicePartID = devicePartID,
                    DevicePartCode = devicePartCode,
                    MixPartID = mixPartID,
                    DeviceID = deviceID,
                    DeviceCode = deviceCode,
                    WeighMaterialID = weighMaterialID,
                    WeighMaterialCode = weighMaterialCode,
                    WeighSetVal = setWeight,
                    AllowError = allowError,
                    WeighOrder = weightOrder,
                    DropOrder = dropOrder,
                    Scanner = useScanner,
                    Rubber = useRuby,
                    Reserve1 = txt_SetTiQian.Text,
                    Reserve2 = txt_SetKuai.Text,
                    Reserve3 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                RawData.Add(formulaWeigh);

                //筛选出下密炼称量数据塞入集合
                List<FormulaWeigh> downMixWeighs = RawData.FindAll(r => r.MixPartID.Equals(downMixPartID));
                RawDataF.Clear();
                RawDataF.AddRange(downMixWeighs);
                RawData = RawData.OrderBy(f => f.DevicePartID).ToList();
                dgvWeight.DataSource = new BindingList<FormulaWeigh>(RawData);

                Check_match(true);
                Check_match(false);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                isChange = true;
                int rowIndex = dgvWeight.CurrentRow.Index;
                if (rowIndex < 0 || rowIndex >= RawData.Count)
                    return;

                int dropOrder = Convert.ToInt32(cmb_DropOrder.SelectedValue);
                if (!DataVerification(dropOrder, false))
                    return;

                decimal setWeight = decimal.Parse(txt_WeighSetVal.Text);
                decimal allowError = decimal.Parse(txt_AllowError.Text);
                decimal quickValue = decimal.Parse(txt_SetKuai.Text);
                decimal tiQianValue = decimal.Parse(txt_SetTiQian.Text);

                int useScanner = int.Parse(cmbScanner.SelectedValue.ToString());
                int useRuby = int.Parse(cmbRuby.SelectedValue.ToString());

                //修改之前的物料信息
                FormulaWeigh oldFormulaWeigh = dgvWeight.CurrentRow.DataBoundItem as FormulaWeigh;
                List<FormulaWeigh> list = new List<FormulaWeigh>();
                foreach (DataGridViewRow item in dgvWeight.Rows)
                {
                    FormulaWeigh formulaWeigh = item.DataBoundItem as FormulaWeigh;
                    list.Add(formulaWeigh);
                }
                FormulaWeigh sFormulaWeigh = list.Find(l => l.WeighMaterialID.Equals(cmb_WeighMaterial.SelectedValue.ToString()));

                if (oldFormulaWeigh.WeighMaterialID.Equals(cmb_WeighMaterial.SelectedValue.ToString()))
                {
                    UpdateEditMaterial(rowIndex);
                }
                //选择物料和当前行物料不是同一种物料 && dgv中没有选择的物料
                else if (!oldFormulaWeigh.WeighMaterialID.Equals(cmb_WeighMaterial.SelectedValue.ToString()) && sFormulaWeigh == null)
                {
                    UpdateEditMaterial(rowIndex);
                }
                //选择的物料已经存在  投料顺序不同
                else if (sFormulaWeigh != null && !sFormulaWeigh.DropOrder.ToString().Equals(cmb_DropOrder.SelectedValue.ToString()))
                {
                    UpdateEditMaterial(rowIndex);
                }
                else
                {
                    lb_weightMessage.Text = NewuGlobal.GetRes("000322");
                    return;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private int ComputeWeightOrder(int dropOrder, string DevicePartID)
        {
            int order = 0;

            if (RawData.Count == 0)
            {
                return 1;
            }

            foreach (FormulaWeigh item in RawData)
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

        private void UpdateEditMaterial(int rowIndex)
        {
            RawDataF.Clear();
            string deviceID = cmb_DeviceID.SelectedValue.ToString();
            decimal setWeight = decimal.Parse(txt_WeighSetVal.Text);
            decimal allowError = decimal.Parse(txt_AllowError.Text);
            int dropOrder = Convert.ToInt32(cmb_DropOrder.SelectedValue);
            string weightMaterialID = cmb_WeighMaterial.SelectedValue.ToString();
            int weightOrder = ComputeWeightOrder(dropOrder, cmb_DevicePart.SelectedValue.ToString());

            int useScanner = int.Parse(cmbScanner.SelectedValue.ToString());
            int useRuby = int.Parse(cmbRuby.SelectedValue.ToString());

            FormulaWeigh formulaWeigh = RawData[rowIndex];
            formulaWeigh.DevicePartID = cmb_DevicePart.SelectedValue.ToString();
            formulaWeigh.MaterialID = RecipeMainID;
            formulaWeigh.MaterialCode = dgvFormulaList.Rows[formulaRowIndex].Cells["MaterialCode"].Value.ToString();
            formulaWeigh.DevicePartID = cmb_DevicePart.SelectedValue.ToString();
            formulaWeigh.DevicePartCode = NewuGlobal.DevicePartCodeByID(cmb_DevicePart.SelectedValue.ToString());
            formulaWeigh.MixPartID = cmb_Mix.SelectedValue.ToString();
            formulaWeigh.DeviceID = deviceID;
            formulaWeigh.DeviceCode = NewuGlobal.DeviceCodeByID(deviceID);
            formulaWeigh.WeighMaterialID = weightMaterialID;
            formulaWeigh.WeighMaterialCode = cmb_WeighMaterial.Text;
            formulaWeigh.WeighSetVal = setWeight;
            formulaWeigh.AllowError = allowError;
            formulaWeigh.Reserve1 = txt_SetTiQian.Text;
            formulaWeigh.Reserve2 = txt_SetKuai.Text;
            formulaWeigh.Reserve3 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            formulaWeigh.DropOrder = dropOrder;
            formulaWeigh.WeighOrder = weightOrder;
            formulaWeigh.Scanner = useScanner;
            formulaWeigh.Rubber = useRuby;
            RawDataF.Clear();
            RawDataF.AddRange(RawData.FindAll(r => r.MixPartID.Equals(downMixPartID)));

            List<FormulaWeigh> formulaWeighs = RawData.FindAll(f => f.DevicePartID.Equals(cmb_DevicePart.SelectedValue.ToString()));
            int WeightNum = 1;
            int WeightNum2 = 1;
            foreach (var item in formulaWeighs)
            {
                if (item.DropOrder == 1)
                {
                    item.WeighOrder = WeightNum;
                    WeightNum++;
                }
                else
                {
                    item.WeighOrder = WeightNum2;
                    WeightNum2++;
                }
            }
            RawData = RawData.OrderBy(f => f.DevicePartID).ToList();
            dgvWeight.DataSource = new BindingList<FormulaWeigh>(RawData);
            dgvWeight.Refresh();

            Check_match(true);
            Check_match(false);
        }

        private bool DataVerification(int dropOrder, bool flag)
        {
            if (cmb_DropOrder.SelectedIndex < 0)
            {
                lb_weightMessage.Text = (NewuGlobal.GetRes("000208") + NewuGlobal.GetRes("000132"));
                return false;
            }

            if (cmb_WeighMaterial.SelectedValue == null)
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000128");//选择的[称量部件]无对应的[称量材料]，请确认！
                return false;
            }

            if (!FunClass.VValDouble(txt_WeighSetVal.Text.Trim()))
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000264") + NewuGlobal.GetRes("000253");//设定重量数据有误请检查
                return false;
            }

            if (!FunClass.VValDouble(txt_AllowError.Text.Trim()))
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000118") + NewuGlobal.GetRes("000253"); //"允许误差数据有误请检查
                return false;
            }

            decimal setWeight = decimal.Parse(txt_WeighSetVal.Text);
            decimal allowError = decimal.Parse(txt_AllowError.Text);
            decimal quickValue = decimal.Parse(txt_SetKuai.Text);
            decimal tiQianValue = decimal.Parse(txt_SetTiQian.Text);
            if (setWeight <= 0)
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000336");//设定重量不可为零！
                return false;
            }

            if (allowError <= 0)
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000337"); //"允许误差不可为零！"
                return false;
            }

            if (quickValue - setWeight >= 0 || tiQianValue - setWeight >= 0 || allowError - setWeight >= 0)
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000338");
                return false;
            }

            FormulaWeigh existWeigh = RawData.Find(f => f.WeighMaterialID.Equals(cmb_WeighMaterial.SelectedValue.ToString()) && f.DropOrder.ToString().Equals(cmb_DropOrder.SelectedValue.ToString()));
            if (existWeigh != null && flag)
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000322");
                return false;
            }

            //"次投入物料信息未录入，不允许录入第"
            if (ComputeDropOrder(dropOrder) == false)
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000304") + (dropOrder - 1) + NewuGlobal.GetRes("000323") + dropOrder + NewuGlobal.GetRes("000324");
                return false;
            }
            return true;
        }

        private bool ComputeDropOrder(int dropOrder)
        {
            int computerOrder = 0;
            foreach (FormulaWeigh item in RawData)
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

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                isChange = true;
                int RowIndex = dgvWeight.CurrentRow.Index;
                if (dgvWeight.Rows.Count > 0)
                {
                    RawData.RemoveAt(RowIndex);
                    //获取到要排序的物料集合
                    List<FormulaWeigh> formulaWeighs = RawData.FindAll(f => f.DevicePartID.Equals(cmb_DevicePart.SelectedValue.ToString()));
                    int WeightNum = 1;
                    int WeightNum2 = 1;
                    foreach (var item in formulaWeighs)
                    {
                        if (item.DropOrder == 1)
                        {
                            item.WeighOrder = WeightNum;
                            WeightNum++;
                        }
                        else
                        {
                            item.WeighOrder = WeightNum2;
                            WeightNum2++;
                        }
                    }
                    RawDataF.Clear();
                    RawDataF.AddRange(RawData.FindAll(r => r.MixPartID.Equals(downMixPartID)));

                    dgvWeight.DataSource = new BindingList<FormulaWeigh>(RawData);
                    Cl_all_weight_Click(null, null);
                    dgvWeight.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        #endregion 称量（增删改）

        #region 密炼（增删改）

        private void MixAdd()
        {
            if (CurrentFormulaMaterial == null)
                return;
            string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(tabPageStepUp.Tag.ToString());

            FormulaMix modelMix = new FormulaMix
            {
                MaterialID = RecipeMainID,
                MaterialCode = CurrentFormulaMaterial.MaterialCode,
                StepOrder = MixData.Count + 1,
                DeviceID = RecipeDeviceID,
                DeviceCode = NewuGlobal.DeviceCodeByID(RecipeDeviceID),
                Reserve1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            FM_FormulaMix_AddStep fm = new FM_FormulaMix_AddStep(modelMix, RecipeDeviceID, devicePartId, MixData, modelMix.StepOrder);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                MixData.Add(modelMix);
                for (int i = 0; i < MixData.Count; i++)
                {
                    MixData[i].StepOrder = i + 1;
                }
                dgvMixUp.DataSource = new BindingList<FormulaMix>(MixData);
                Check_match(true);
            }
        }

        private void MixFAdd()
        {
            try
            {
                if (CurrentFormulaMaterial == null)
                    return;

                string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(tabPageStepDown.Tag.ToString());

                FormulaMixF modelMixF = new FormulaMixF
                {
                    MaterialID = RecipeMainID,
                    MaterialCode = CurrentFormulaMaterial.MaterialCode,
                    StepOrder = MixFData.Count + 1,
                    DeviceID = RecipeDeviceID,
                    DeviceCode = NewuGlobal.DeviceCodeByID(RecipeDeviceID),
                    Reserve1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                FM_FormulaMixF_AddStep fm = new FM_FormulaMixF_AddStep(modelMixF, RecipeDeviceID, devicePartId, MixFData, modelMixF.StepOrder);

                if (fm.ShowDialog() == DialogResult.OK)
                {
                    MixFData.Add(modelMixF);
                    for (int i = 0; i < MixFData.Count; i++)
                    {
                        MixFData[i].StepOrder = i + 1;
                    }
                    dgvMixDown.DataSource = new BindingList<FormulaMixF>(MixFData);
                    Check_match(false);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void MixEdit()
        {
            if (CurrentFormulaMaterial == null)
                return;
            //2018年7月31日，郄长堂修改
            int RowIndex = GetSelectedRowIndex(this.dgvMixUp);
            string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(tabPageStepUp.Tag.ToString());

            if (RowIndex < 0)
            {
                return;
            }

            FormulaMix newRow = MixData[RowIndex];
            newRow.MaterialID = RecipeMainID;
            newRow.MaterialCode = CurrentFormulaMaterial.MaterialCode;
            newRow.DeviceID = RecipeDeviceID;
            newRow.DeviceCode = NewuGlobal.DeviceCodeByID(RecipeDeviceID);
            newRow.Reserve1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            FM_FormulaMix_AddStep fm = new FM_FormulaMix_AddStep(newRow, RecipeDeviceID, devicePartId, MixData, RowIndex + 1);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < MixData.Count; i++)
                {
                    MixData[i].StepOrder = i + 1;
                }

                dgvMixUp.DataSource = new BindingList<FormulaMix>(MixData);
            }
            Check_match(true);
        }

        private void MixFEdit()
        {
            try
            {
                if (CurrentFormulaMaterial == null)
                    return;

                int RowIndex = GetSelectedRowIndex(this.dgvMixDown);
                string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(tabPageStepDown.Tag.ToString());

                if (RowIndex < 0)
                    return;

                FormulaMixF newRow = MixFData[RowIndex];
                newRow.MaterialID = RecipeMainID;
                newRow.MaterialCode = CurrentFormulaMaterial.MaterialCode;
                newRow.DeviceID = RecipeDeviceID;
                newRow.DeviceCode = NewuGlobal.DeviceCodeByID(RecipeDeviceID);
                newRow.Reserve1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                FM_FormulaMixF_AddStep fm = new FM_FormulaMixF_AddStep(newRow, RecipeDeviceID, devicePartId, MixFData, RowIndex + 1);

                if (fm.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < MixFData.Count; i++)
                    {
                        MixFData[i].StepOrder = i + 1;
                    }
                    dgvMixDown.DataSource = new BindingList<FormulaMixF>(MixFData);
                }
                Check_match(false);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void MixUP()
        {
            int RowIndex = GetSelectedRowIndex(dgvMixUp);
            if (RowIndex >= 1)
            {
                // 拷贝选中的行
                FormulaMix newRow = MixData[RowIndex];

                // 删除选中的行
                MixData.RemoveAt(RowIndex);

                // 将拷贝的行，插入到选中的上一行位置
                MixData.Insert(RowIndex - 1, newRow);

                // 选中最初选中的行
                dgvMixUp.Rows[RowIndex - 1].Selected = true;
            }

            dgvMixUp.Refresh();
        }

        private void MixFUP()
        {
            try
            {
                int RowIndex = GetSelectedRowIndex(this.dgvMixDown);
                if (RowIndex >= 1)
                {
                    // 拷贝选中的行
                    FormulaMixF newRow = MixFData[RowIndex];

                    // 删除选中的行
                    MixFData.RemoveAt(RowIndex);

                    // 将拷贝的行，插入到选中的上一行位置
                    MixFData.Insert(RowIndex - 1, newRow);

                    // 选中最初选中的行
                    dgvMixDown.Rows[RowIndex - 1].Selected = true;
                }

                dgvMixDown.Refresh();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void MixDown()
        {
            int RowIndex = GetSelectedRowIndex(this.dgvMixUp);

            if (RowIndex < dgvMixUp.Rows.Count - 1)
            {
                FormulaMix newRow = MixData[RowIndex];

                MixData.RemoveAt(RowIndex);

                MixData.Insert(RowIndex + 1, newRow);

                dgvMixUp.Rows[RowIndex + 1].Selected = true;
            }
            dgvMixUp.Refresh();
        }

        private void MixFDown()
        {
            try
            {
                int RowIndex = GetSelectedRowIndex(this.dgvMixDown);

                if (RowIndex < dgvMixDown.Rows.Count - 1)
                {
                    FormulaMixF newRow = MixFData[RowIndex];

                    MixFData.RemoveAt(RowIndex);

                    MixFData.Insert(RowIndex + 1, newRow);

                    dgvMixDown.Rows[RowIndex + 1].Selected = true;
                }
                dgvMixDown.Refresh();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void MixDelete()
        {
            int RowIndex = GetSelectedRowIndex(this.dgvMixUp);
            if (dgvMixUp.Rows.Count > 0)
            {
                MixData.RemoveAt(RowIndex);
                for (int i = 0; i < MixData.Count; i++)
                {
                    MixData[i].StepOrder = i + 1;
                }

                dgvMixUp.DataSource = new BindingList<FormulaMix>(MixData);
            }
        }

        private void MixFDelete()
        {
            try
            {
                int RowIndex = GetSelectedRowIndex(dgvMixDown);
                if (dgvMixDown.Rows.Count > 0)
                {
                    MixFData.RemoveAt(RowIndex);
                    for (int i = 0; i < MixFData.Count; i++)
                    {
                        MixFData[i].StepOrder = i + 1;
                    }
                    dgvMixDown.DataSource = new BindingList<FormulaMixF>(MixFData);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void MixInset()
        {
            //新插入一行数据
            int RowIndex = GetSelectedRowIndex(this.dgvMixUp);
            string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(tabPageStepUp.Tag.ToString());

            FormulaMix modelMix = new FormulaMix
            {
                MaterialID = RecipeMainID,
                MaterialCode = CurrentFormulaMaterial.MaterialCode,
                DeviceID = RecipeDeviceID,
                DeviceCode = NewuGlobal.DeviceCodeByID(RecipeDeviceID),
                Reserve1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            FM_FormulaMix_AddStep fm = new FM_FormulaMix_AddStep(modelMix, RecipeDeviceID, devicePartId, MixData, RowIndex + 1);

            if (fm.ShowDialog() == DialogResult.OK)
            {
                MixData.Insert(RowIndex, modelMix);
                for (int i = 0; i < MixData.Count; i++)
                {
                    MixData[i].StepOrder = i + 1;
                }
                dgvMixUp.DataSource = new BindingList<FormulaMix>(MixData);
                Check_match(true);
            }
        }

        private void MixFInset()
        {
            try
            {
                int RowIndex = GetSelectedRowIndex(dgvMixDown);
                string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(tabPageStepDown.Tag.ToString());

                FormulaMixF modelMix = new FormulaMixF
                {
                    MaterialID = RecipeMainID,
                    MaterialCode = CurrentFormulaMaterial.MaterialCode,
                    DeviceID = RecipeDeviceID,
                    DeviceCode = NewuGlobal.DeviceCodeByID(RecipeDeviceID),
                    Reserve1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                FM_FormulaMixF_AddStep fm = new FM_FormulaMixF_AddStep(modelMix, RecipeDeviceID, devicePartId, MixFData, RowIndex + 1);

                if (fm.ShowDialog() == DialogResult.OK)
                {
                    MixFData.Insert(RowIndex, modelMix);
                    for (int i = 0; i < MixFData.Count; i++)
                    {
                        MixFData[i].StepOrder = i + 1;
                    }
                    dgvMixDown.DataSource = new BindingList<FormulaMixF>(MixFData);
                    Check_match(false);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        #endregion 密炼（增删改）

        private int GetSelectedRowIndex(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                return -1;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Selected)
                {
                    return row.Index;
                }
            }
            return -1;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveFormula())
            {
                lb_weightMessage.Text = NewuGlobal.GetRes("000171");//"配方保存成功!"
                isChange = false;
            }
        }

        /// <summary>
        /// 思路 1.将获取到的sql语句添加入 添加数据中的sql 中 2.统一使用事务完成 3.更改配方报错描述 先前做法为先删 后加 当插入数据发生异常时
        /// 会导致添加失败，但之前的数据已经被删除 原代码已注释
        /// </summary>
        /// <returns></returns>
        private bool SaveFormula()
        {
            try
            {
                if (!Check_match(true))
                {
                    MessageBox.Show(NewuGlobal.GetRes("000227"));//"配方信息数据有误，请检查后再保存！"
                    return false;
                }
                if (NewuGlobal.SoftConfig.DownMixer && !Check_match(false))
                {
                    MessageBox.Show(NewuGlobal.GetRes("000227"));//"配方信息数据有误，请检查后再保存！"
                    return false;
                }

                for (int i = 0; i < dgvTechParamUp.Rows.Count; i++)
                {
                    var temp = FunClass.VDecimal(dgvTechParamUp["TechParamVal", i].Value);
                    decimal dec = 0.0M;
                    //应厂里需求，回收炭黑时间需可以设置为0，修改<=为<
                    if (temp < dec)
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000243"));//"配方工艺参数数据，不可小于零！"
                        return false;
                    }
                }
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    for (int i = 0; i < dgvTechParamDown.Rows.Count; i++)
                    {
                        var temp = FunClass.VDecimal(dgvTechParamDown["TechParamVal", i].Value);
                        decimal dec = 0.0M;
                        if (temp < dec)
                        {
                            MessageBox.Show("下密炼配方工艺参数数据，不可小于零！");
                            return false;
                        }
                    }
                }

                //保存称量数据
                formulaWeighRepository.DeleteAndInsert(RecipeMainID, RawData);
                //保存密炼数据
                formulaMixRepository.DeleteAndAddList(RecipeMainID, MixData);
                bool isDelete = formulaTechParamRepository.DeleteAll(RecipeMainID);

                //保存系统工艺参数
                List<FormulaTechParam> techParamList = new List<FormulaTechParam>();
                for (int i = 0; i < dgvTechParamUp.Rows.Count; i++)
                {
                    FormulaTechParam formulaTechParamModel = new FormulaTechParam
                    {
                        MaterialID = RecipeMainID,
                        TechParamID = dgvTechParamUp["TechParamID", i].Value.ToString(),
                        TechParamVal = FunClass.VDecimal(dgvTechParamUp["TechParamVal", i].Value.ToString()),
                        Reserve2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),//Li.Hui 20200923
                        Reserve3 = NewuGlobal.TB_UserInfo.UserCode //Li.Hui 20200923
                    };
                    techParamList.Add(formulaTechParamModel);
                }
                formulaTechParamRepository.AddList(techParamList);

                List<FormulaTechParamF> techParamFList = new List<FormulaTechParamF>();
                //下密炼数据
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    formulaMixFRepository.DeleteAndAddList(RecipeMainID, MixFData);
                    formulaTechParamFRepository.DeleteAll(RecipeMainID);

                    for (int i = 0; i < dgvTechParamDown.Rows.Count; i++)
                    {
                        FormulaTechParamF model = new FormulaTechParamF
                        {
                            MaterialID = RecipeMainID,
                            TechParamID = dgvTechParamDown["TechParamID", i].Value.ToString(),
                            TechParamVal = FunClass.VDecimal(dgvTechParamDown["TechParamVal", i].Value.ToString()),
                            Reserve2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //Li.Hui 20200923
                            Reserve3 = NewuGlobal.TB_UserInfo.UserCode //Li.Hui 20200923
                        };
                        techParamFList.Add(model);
                    }
                    formulaTechParamFRepository.AddList(techParamFList);
                }

                formulaOperateLogRepository.Add(RawData, MixData, techParamList, MixFData, techParamFList, RecipeMainID, dgvFormulaList.Rows[formulaRowIndex].Cells["MaterialCode"].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(NewuGlobal.GetRes("000253") + ex.ToString().Substring(0, 60));//"数据错误，请检查物料投入次序和称量材料是否相同 \n"
                return false;
            }
            return true;
        }

        private bool Check_match(bool isUpMixer)
        {
            int[] rawNum = new int[sYS_DeviceParts.Count - 1];
            int[] mixNum = new int[sYS_DeviceParts.Count - 1];
            List<FormulaWeigh> mixPartWeighs;
            if (isUpMixer)
                mixPartWeighs = RawData.FindAll(r => r.MixPartID.Equals(upMixPartID));
            else
                mixPartWeighs = RawData.FindAll(r => r.MixPartID.Equals(downMixPartID));

            foreach (FormulaWeigh item in mixPartWeighs)
            {
                if (item.DevicePartCode == NewuGlobal.RubberScales)
                {
                    int a = FindRawNum(item.DevicePartCode);
                    if (item.DropOrder == 2)
                    {
                        //判断二次投胶
                        rawNum[a] = 2;
                    }

                    if (item.DropOrder == 1 && item.WeighOrder == 1)
                    {
                        if (a >= 0)
                            rawNum[a]++;
                    }
                }
                else if (item.DevicePartCode == NewuGlobal.CarbonScales)
                {
                    int a = FindRawNum(item.DevicePartCode);
                    if (item.DropOrder == 2)
                    {  //判断二次投炭黑
                        rawNum[a] = 2;
                    }
                    if (item.DropOrder == 1 && item.WeighOrder == 1)
                    {
                        if (a >= 0)
                            rawNum[a]++;
                    }
                }
                else
                {
                    if (item.WeighOrder == 1)
                    {
                        int a = FindRawNum(item.DevicePartCode);
                        if (a >= 0)
                            rawNum[a]++;
                    }
                }
            }

            if (isUpMixer)
            {
                foreach (FormulaMix item in MixData)
                {
                    if (item.StepDesc.Contains("胶料") || item.StepDesc.Contains("Rubber"))
                    {
                        mixNum[NewuGlobal.RubberPartNum]++;
                    }
                    if (item.StepDesc.Contains("药") || item.StepDesc.Contains("Drug"))
                    {
                        mixNum[NewuGlobal.DrugPartNum]++;
                    }
                    if (item.StepDesc.Contains("炭黑") || item.StepDesc.Contains("Carbon"))
                    {
                        mixNum[NewuGlobal.CarbonPartNum]++;
                    }
                    if (item.StepDesc.Contains("塑") || item.StepDesc.Contains("Pla"))
                    {
                        mixNum[NewuGlobal.PlaPartNum]++;
                    }
                    if (item.StepDesc.Contains("油") || item.StepDesc.Contains("Oil"))
                    {
                        mixNum[NewuGlobal.OilPartNum]++;
                    }
                    if (item.StepDesc.Contains("硅") || item.StepDesc.Contains("Si"))
                    {
                        mixNum[NewuGlobal.SiPartNum]++;
                    }
                    if (item.StepDesc.Contains("粉") || item.StepDesc.Contains("Zno"))
                    {
                        mixNum[NewuGlobal.ZnoPartNum]++;
                    }
                    if (item.StepDesc.Contains("白炭") || item.StepDesc.Contains("White"))
                    {
                        mixNum[NewuGlobal.WCarbonPartNum]++;
                    }
                }
            }
            else
            {
                foreach (FormulaMixF item in MixFData)
                {
                    if (item.StepDesc.Contains("药2") || item.StepDesc.Contains("Drug2"))
                    {
                        mixNum[NewuGlobal.DrugPartNum2]++;
                    }

                    if (item.StepDesc.Contains("油2") || item.StepDesc.Contains("Oil2"))
                    {
                        mixNum[NewuGlobal.OilPartNum2]++;
                    }

                    if (item.StepDesc.Contains("粉料2") || item.StepDesc.Contains("Zno2"))
                    {
                        mixNum[NewuGlobal.ZnoPartNum2]++;
                    }
                }
            }

            bool ok = true;
            tv_show_cheak.Text = "";

            for (int i = 1; i < rawNum.Length; i++)
            {
                if (rawNum[i] != mixNum[i])
                {
                    ok = false;
                    tv_show_cheak.Text += FindQue(i);
                }
            }
            return ok;
        }

        private int FindRawNum(string str)
        {
            if (str.Equals(NewuGlobal.RubberScales))
            {
                return NewuGlobal.RubberPartNum;   //胶料秤
            }
            else if (str.Equals(NewuGlobal.DrugScales))
            {
                return NewuGlobal.DrugPartNum;  //小药秤
            }
            else if (str.Equals(NewuGlobal.CarbonScales))
            {
                return NewuGlobal.CarbonPartNum;   //炭黑秤
            }
            else if (str.Equals(NewuGlobal.OilScales))
            {
                return NewuGlobal.OilPartNum;   //油料秤
            }
            else if (str.Equals(NewuGlobal.SiScales))
            {
                return NewuGlobal.SiPartNum;
            }
            else if (str.Equals(NewuGlobal.ZnoScales))
            {
                return NewuGlobal.ZnoPartNum;
            }
            else if (str.Equals(NewuGlobal.PlaScales))
            {
                return NewuGlobal.PlaPartNum;  // 塑解剂秤
            }
            else if (str.Equals(NewuGlobal.WCarbonScales))
            {
                return NewuGlobal.WCarbonPartNum;
            }
            else if (str.Equals(NewuGlobal.DrugScales2))
            {
                return NewuGlobal.DrugPartNum2;  //小药秤2
            }
            else if (str.Equals(NewuGlobal.OilScales2))
            {
                return NewuGlobal.OilPartNum2;
            }
            else if (str.Equals(NewuGlobal.ZnoScales2))
            {
                return NewuGlobal.ZnoPartNum2;
            }
            else
                return 0;
        }

        private string FindQue(int i)
        {
            string ans = "";
            switch (i)
            {
                case NewuGlobal.RubberPartNum:
                    ans = NewuGlobal.GetRes("000706");
                    break;

                case NewuGlobal.DrugPartNum:
                    ans = NewuGlobal.GetRes("000707");
                    break;

                case NewuGlobal.CarbonPartNum:
                    ans = NewuGlobal.GetRes("000708");
                    break;

                case NewuGlobal.PlaPartNum:
                    ans = NewuGlobal.GetRes("000709");
                    break;

                case NewuGlobal.OilPartNum:
                    ans = NewuGlobal.GetRes("000710");
                    break;

                case NewuGlobal.SiPartNum:
                    ans = NewuGlobal.GetRes("000711");
                    break;

                case NewuGlobal.ZnoPartNum:
                    ans = NewuGlobal.GetRes("000712");
                    break;

                case NewuGlobal.WCarbonPartNum:
                    ans = NewuGlobal.GetRes("000845");
                    break;

                case NewuGlobal.ZnoPartNum2:
                    ans = NewuGlobal.GetRes("000712") + "2";
                    break;

                case NewuGlobal.OilPartNum2:
                    ans = NewuGlobal.GetRes("000710") + "2";
                    break;

                case NewuGlobal.DrugPartNum2:
                    ans = NewuGlobal.GetRes("000707") + "2";
                    break;

                default:
                    break;
            }
            ans += NewuGlobal.GetRes("000713") + " \n";
            return ans;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvMix_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && dgvMixUp.Rows.Count == 0)
            {
                Point p = MousePosition;

                p.X -= PointToScreen(this.Location).X;
                p.Y -= PointToScreen(this.Location).Y;
                contextMenuStripMix.Show(this, p);
            }
            mixerType = "UpMixer";
        }

        private void DgvMix_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            lb_weightMessage.Text = NewuGlobal.GetRes("000170");
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvMixUp.ClearSelection();
                dgvMixUp.Rows[e.RowIndex].Selected = true;

                Point p = MousePosition;

                p.X -= PointToScreen(this.Location).X;
                p.Y -= PointToScreen(this.Location).Y;
                contextMenuStripMix.Show(this, p);
            }
            mixerType = "UpMixer";
        }

        private void DgvMixDown_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvMixDown.ClearSelection();
                dgvMixDown.Rows[e.RowIndex].Selected = true;

                Point p = MousePosition;
                p.X -= PointToScreen(this.Location).X;
                p.Y -= PointToScreen(this.Location).Y;
                contextMenuStripMix.Show(this, p);
            }
            mixerType = "DownMixer";
        }

        private void DgvMixDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && dgvMixDown.Rows.Count == 0)
            {
                Point p = MousePosition;
                p.X -= PointToScreen(this.Location).X;
                p.Y -= PointToScreen(this.Location).Y;
                contextMenuStripMix.Show(this, p);
            }
            mixerType = "DownMixer";
        }

        public void GetFormulaList()
        {
            if (cmb_DeviceID.SelectedIndex < 0)
            {
                return;
            }
            string strWhere = "DeviceID='" + cmb_DeviceID.SelectedValue.ToString() + "'";

            if (txtLikeQuery.Text != "")
            {
                strWhere += " and MaterialCode like '%" + txtLikeQuery.Text + "%' ";
            }
            string _typeCodeName1 = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T母炼配方);
            string _typeCodeID1 = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName1);

            string _typeCodeName2 = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T终炼配方);
            string _typeCodeID2 = NewuGlobal.GetTypeCodeIDByCodeName(_typeCodeName2);

            strWhere += "and TypeCodeID in('" + _typeCodeID1 + "','" + _typeCodeID2 + "')";
            if (radB_SY.Checked == true)
            {
                strWhere += "and Enable = '1' ";   //显示状态
            }
            else if (radB_ZF.Checked == true)
            {
                strWhere += "and Enable = '0' ";   //显示状态
            }

            formulaList = formulaMaterialRepository.GetList(0, strWhere, " MaterialCode");
            dgvFormulaList.DataSource = formulaList;
            dgvWeight.DataSource = null;
            dgvMixUp.DataSource = null;
            dgvTechParamUp.DataSource = null;
            dgvTechParamDown.DataSource = null;
        }

        private void TxtLikeQuery_KeyUp(object sender, KeyEventArgs e)
        {
            GetFormulaList();
        }

        private void DgvFormulaList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lb_weightMessage.Text = NewuGlobal.GetRes("000170");
            //判断 前一个配方是否有变动
            if (isChange)
            {
                //"刚编辑的配方未保存,是否保存？"提示
                DialogResult ds = MessageBox.Show(NewuGlobal.GetRes("000177"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo);

                if (ds == DialogResult.Yes)
                {
                    if (SaveFormula())
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000171"));//"配方保存成功!"
                    }
                    else
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000172"));//"配方保存失败!"
                        return;
                    }
                }
            }
            isChange = false;
            string materialID = dgvFormulaList.CurrentRow.Cells["MaterialID"].Value.ToString();
            int rowIndex = dgvFormulaList.CurrentRow.Index;
            if (materialID == NewuGlobal.Now_MaterialID)
            {
                bt_SaveUpdate.Visible = true;
                progressBar1.Visible = true;
            }
            else
            {
                bt_SaveUpdate.Visible = false;
                progressBar1.Visible = false;
            }
            if (dgvFormulaList.Rows.Count > 0 && rowIndex >= 0)
            {
                if (dgvFormulaList.Rows[rowIndex].Cells[1].Value != null)
                {
                    _recipeMainID = dgvFormulaList[1, rowIndex].Value.ToString();
                    RecipeMainID = dgvFormulaList[1, rowIndex].Value.ToString();
                    CurrentFormulaMaterial = formulaMaterialRepository.GetModel(_recipeMainID);
                    GetData();
                }
            }
        }

        private void DgvFormulaList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //配方列表左键点击第三列
            if (e.Button == MouseButtons.Left && e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string v = dgvFormulaList[3, e.RowIndex].Value.ToString();
                int a = int.Parse(v);
                if (a > 0)
                {
                    //"是否修改当前配方使用状态为【作废】"    //"修改配方状态提示"
                    DialogResult rult = MessageBox.Show(NewuGlobal.GetRes("000232"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo);
                    if (rult == DialogResult.Yes)
                    {
                        string materialID = dgvFormulaList.Rows[e.RowIndex].Cells["MaterialID"].Value.ToString();
                        bool isUp = formulaMaterialRepository.UpdateSig(materialID, 0);
                        if (isUp == false)
                        {
                            //"配方状态更新失败"
                            MessageBox.Show(dgvFormulaList[3, e.RowIndex].Value.ToString() + NewuGlobal.GetRes("000242"));
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
                    //"是否修改当前配方使用状态为【使用】" //"修改配方状态提示"
                    DialogResult rult = MessageBox.Show(NewuGlobal.GetRes("000233"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo);
                    if (rult == DialogResult.Yes)
                    {
                        string materialID = dgvFormulaList.Rows[e.RowIndex].Cells["MaterialID"].Value.ToString();
                        bool isUp = formulaMaterialRepository.UpdateSig(materialID, 1);
                        if (isUp == false)
                        {
                            //"配方状态更新失败"
                            MessageBox.Show(dgvFormulaList[3, e.RowIndex].Value.ToString() + NewuGlobal.GetRes("000242"));
                            GetFormulaList();
                        }
                        else
                        {
                            GetFormulaList();
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point p = MousePosition;
                p.X -= PointToScreen(this.Location).X;
                p.Y -= PointToScreen(this.Location).Y;
                contextMenuStripFormula.Show(this, p);
            }
        }

        private void Cmb_DeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_DeviceID.SelectedIndex >= 0)
            {
                _recipeDeviceID = cmb_DeviceID.SelectedValue.ToString();
                GetFormulaList();
            }
        }

        /// <summary>
        /// 删除配方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteMaterial_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    if (orderTranRepository.GetPlanFormularList(RecipeMainID))
                    {
                        MessageBox.Show(NewuGlobal.GetRes("000272"));//"当前计划排产中含有该配方，请清理后再删！"
                        return;
                    }

                    formulaWeighRepository.DeleteAll(RecipeMainID);
                    formulaMixRepository.DeleteAll(RecipeMainID);
                    formulaTechParamRepository.DeleteAll(RecipeMainID);

                    //移除配方
                    int rowIndex = dgvFormulaList.CurrentRow.Index;
                    if (dgvFormulaList.Rows.Count > 0 && rowIndex >= 0)
                    {
                        if (dgvFormulaList.Rows[rowIndex].Cells[1].Value != null)
                        {
                            //dgvFormulaList[0, rowIndex]-->dgvFormulaList.Rows[rowIndex].Cells["MaterialID"]避免列发生变化出现问题 20240612 李辉
                            string materialID = dgvFormulaList.Rows[rowIndex].Cells["MaterialID"].Value.ToString();

                            bool isDel = formulaMaterialRepository.Delete(materialID);
                            if (isDel == false)
                            {
                                //"配方删除失败"
                                MessageBox.Show(dgvFormulaList[2, rowIndex].Value.ToString() + NewuGlobal.GetRes("000174"));
                            }
                        }
                        GetFormulaList();
                        dgvWeight.DataSource = null;
                        dgvMixUp.DataSource = null;
                        dgvTechParamUp.DataSource = null;
                        dgvTechParamDown.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000174") + ex.ToString());//"删除失败"
                }
        }

        private void BtnAddMaterial_Click(object sender, EventArgs e)
        {
            FM_FormulaMaterial_Add fm = new FM_FormulaMaterial_Add(false)
            {
                Owner = this
            };
            DialogResult dialogResult = fm.ShowDialog();
            if (dialogResult == DialogResult.OK)
                GetFormulaList();
        }

        private void BtnEditMaterial_Click(object sender, EventArgs e)
        {
            FM_FormulaMaterial_Add fm = new FM_FormulaMaterial_Add(RecipeMainID, false)
            {
                Owner = this
            };
            fm.ShowDialog();
        }

        private void BtnCopyMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                GetData();
                if (dgvFormulaList.Rows.Count < 0)
                {
                    return;
                }
                int rowIndex = formulaRowIndex;

                if (dgvFormulaList.Rows[formulaRowIndex].Cells["MaterialID"].Value == null)
                {
                    return;
                }
                string id = dgvFormulaList.Rows[rowIndex].Cells["MaterialID"].Value.ToString();
                FM_FormulaMaterial_Add fm = new FM_FormulaMaterial_Add(id, true, false)
                {
                    Owner = this
                };

                if (fm.ShowDialog() == DialogResult.OK)
                {
                    //查询刚刚成功复制的配方
                    FormulaMaterial formulaMaterial = formulaMaterialRepository.GetList(" MaterialCode=N'" + modelCopy.MaterialCode + "' and  VersionNo=N'" + modelCopy.VersionNo + "' and DeviceID='" + modelCopy.DeviceID + "' ")[0];

                    for (int i = 0; i < RawData.Count; i++)
                    {
                        RawData[i].MaterialID = formulaMaterial.MaterialID;
                        RawData[i].MaterialCode = formulaMaterial.MaterialCode;
                    }
                    for (int j = 0; j < MixData.Count; j++)
                    {
                        MixData[j].MaterialID = formulaMaterial.MaterialID;
                        MixData[j].MaterialCode = formulaMaterial.MaterialCode;
                    };

                    //下密炼
                    for (int i = 0; i < MixFData.Count; i++)
                    {
                        MixFData[i].MaterialID = formulaMaterial.MaterialID;
                        MixFData[i].MaterialCode = formulaMaterial.MaterialCode;
                    }

                    //保存称量数据
                    formulaWeighRepository.AddList(RawData);

                    //保存密炼数据
                    formulaMixRepository.AddList(MixData);
                    formulaMixFRepository.AddList(MixFData);

                    //保存系统工艺参数
                    List<FormulaTechParam> listTechTemp = new List<FormulaTechParam>();
                    foreach (var item in formulaTechParams)
                    {
                        FormulaTechParam formulaTechParamModel = new FormulaTechParam
                        {
                            MaterialID = formulaMaterial.MaterialID
                        };
                        formulaTechParamModel.TechParamID = item.TechParamID.ToString();
                        formulaTechParamModel.TechParamVal = FunClass.VDecimal(item.TechParamVal.ToString());
                        listTechTemp.Add(formulaTechParamModel);
                    }
                    formulaTechParamRepository.AddList(listTechTemp);

                    List<FormulaTechParamF> listTechFTemp = new List<FormulaTechParamF>();
                    foreach (var item in formulaTechParamFs)
                    {
                        FormulaTechParamF formulaTechParamFModel = new FormulaTechParamF
                        {
                            MaterialID = formulaMaterial.MaterialID
                        };
                        formulaTechParamFModel.TechParamID = item.TechParamID.ToString();
                        formulaTechParamFModel.TechParamVal = FunClass.VDecimal(item.TechParamVal.ToString());
                        listTechFTemp.Add(formulaTechParamFModel);
                    }
                    formulaTechParamFRepository.AddList(listTechFTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 接收添加界面传回来的model，用于复制配方时修改里面的值
        /// </summary>
        /// <param name="modelMaterialtemp"></param>
        public void GetMaterialModel(FormulaMaterial modelMaterialtemp)
        {
            modelCopy = modelMaterialtemp;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (SaveFormula())
            {
                if (StartTransfer(NewuGlobal.Now_MaterialID, NewuGlobal.Now_OrderID))
                {
                    MessageBox.Show(NewuGlobal.GetRes("000171"));//"保存下发成功！"
                    isChange = false;
                    //20211227---导致保存配方刷新失败的原因在此刷新
                    if (NewuGlobal.RubyDataChange != null)
                        NewuGlobal.RubyDataChange.RefreshData(true); //主屏
                    if (NewuGlobal.MixDataChange != null)
                        NewuGlobal.MixDataChange.RefreshData(true); //副屏

                    if (NewuGlobal.MixGridDataChange != null)
                        NewuGlobal.MixGridDataChange.RefreshData(true); //UserControl

                    progressBar1.Value = 0;
                }
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"));//"保存失败，出问题了！"
            }
        }

        /// <summary>
        /// 发送配方界面 发送配方数据到PLC
        /// </summary>
        /// <param name="MaterialID"></param>
        /// 配方ID
        /// <param name="OrderID"></param>
        /// 订单ID 更新界面数据等等
        /// <returns></returns>
        public bool StartTransfer(string MaterialID, string OrderID)
        {
            string errStr = "";
            bool ExecuteBool = false;
            progressBar1.Value = 10;

            //发送硅烷数据
            if (NewuGlobal.SoftConfig.Silane)
                ExecuteBool = transInstance.PlcSilData(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;
            progressBar1.Value = 20;

            //发送炭黑
            if (NewuGlobal.SoftConfig.Carbon)
                ExecuteBool = transInstance.PlcCarbonData(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;

            progressBar1.Value = 30;

            //发送油料
            if (NewuGlobal.SoftConfig.Oil)
                ExecuteBool = transInstance.PlcOilDataData1(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;

            progressBar1.Value = 40;

            //发送塑解剂
            if (NewuGlobal.SoftConfig.Pla)
                ExecuteBool = transInstance.PlcPlaData(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;
            progressBar1.Value = 50;

            //发送粉料
            if (NewuGlobal.SoftConfig.Zno)
                ExecuteBool = transInstance.PlcZnoData(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;

            progressBar1.Value = 60;

            //发送小药
            if (NewuGlobal.SoftConfig.Drug)
                ExecuteBool = transInstance.PlcDrugData(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;

            progressBar1.Value = 70;

            //发送恒温炼胶数据
            //ExecuteBool = transInstance.PlcHwljData(MaterialID, out errStr); if (ExecuteBool == false) goto ErrorFlag;

            // 发送胶料
            if (NewuGlobal.SoftConfig.Rubber)
                ExecuteBool = transInstance.PlcRubberData(MaterialID, out errStr);
            else
            {
                ExecuteBool = true;
            }
            if (!ExecuteBool)
                goto ErrorFlag;

            progressBar1.Value = 80;

            //上密炼系统参数 和 上密炼密炼工艺
            ExecuteBool = transInstance.PlcSysData(MaterialID, out errStr);
            if (!ExecuteBool)
                goto ErrorFlag;

            progressBar1.Value = 90;

            ExecuteBool = transInstance.PlcMixData(MaterialID, out errStr);
            if (!ExecuteBool)
                goto ErrorFlag;

            //下密炼系统参数 和 下密炼密炼工艺
            if (NewuGlobal.SoftConfig.DownMixer)
            {
                ExecuteBool = transInstance.PlcSysDataF(MaterialID, out errStr);
                if (!ExecuteBool)
                    goto ErrorFlag;

                ExecuteBool = transInstance.PlcMixFData(MaterialID, out errStr);
                if (!ExecuteBool)
                    goto ErrorFlag;
            }

            progressBar1.Value = 100;

            return true;

ErrorFlag:
            MessageBox.Show(NewuGlobal.GetRes("000209") + errStr, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            progressBar1.Value = 0;
            return false;
        }

        private void DgvTechParam_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                isChange = true;
        }

        private void Cl_all_weight_Click(object sender, EventArgs e)
        {
            decimal sum = 0M;
            //计算当前list表中的 所有重量  显示在 allWeight 中
            foreach (var a in RawData)
            {
                sum += a.WeighSetVal;
            }
            this.allWeight.Text = sum.ToString() + " Kg";
        }

        private void 自动排列步序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (var a in MixData)
            {
                a.StepOrder = i++;
            }
            dgvMixUp.DataSource = new BindingList<FormulaMix>(MixData);
        }

        private void RadB_SY_CheckedChanged(object sender, EventArgs e)
        {
            if (radB_SY.Checked == true)
            {
                GetFormulaList();
            }
        }

        private void RadB_ZF_CheckedChanged(object sender, EventArgs e)
        {
            if (radB_ZF.Checked == true)
            {
                GetFormulaList();
            }
        }

        //再执行此初始化
        private void Cmb_DevicePart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //根据 devicePartID --> typeCodeID
                //1、根据devicePartID --> DevicePartCode
                //2、根据 DevicePartCode做判断得到对应的typCodeID
                string devicePartID = cmb_DevicePart.SelectedValue.ToString();
                string devicePartCode = NewuGlobal.DevicePartCodeByID(devicePartID);
                string typeCodeID = GetTypeCodeIDByDevicepartCode(devicePartCode);
                List<FormulaMaterial> cmbMaterials = formulaMaterials.FindAll(m => (m.DeviceID == RecipeDeviceID || m.DeviceID.Equals("")) && m.TypeCodeID == typeCodeID);
                cmb_WeighMaterial.DataSource = cmbMaterials;
                cmb_WeighMaterial.ValueMember = "MaterialID";
                cmb_WeighMaterial.DisplayMember = "MaterialCode";
                if (dgvWeight.CurrentRow != null)
                    cmb_WeighMaterial.SelectedValue = dgvWeight.CurrentRow.Cells["WeighMaterialID"].Value.ToString();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private string GetTypeCodeIDByDevicepartCode(string devicePartCode)
        {
            string typeCodeID = "";

            if (devicePartCode.Equals(NewuGlobal.RubberScales))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("胶料")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.DrugScales) || devicePartCode.Equals(NewuGlobal.DrugScales2))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("药品")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.CarbonScales))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("炭黑")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.OilScales) || devicePartCode.Equals(NewuGlobal.OilScales2))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("油料")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.SiScales))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("硅烷")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.ZnoScales) || devicePartCode.Equals(NewuGlobal.ZnoScales))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("粉料")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.PlaScales))
            {
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("塑解剂")).TypeCodeID;
            }
            else if (devicePartCode.Equals(NewuGlobal.WCarbonScales))
                typeCodeID = NewuGlobal.TypeCodeList.Find(t => t.TypeCodeDesc.Equals("白炭黑")).TypeCodeID;

            return typeCodeID;
        }

        //先执行此初始化
        private void DgvWeight_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvWeight.CurrentRow.Cells["DevicePartID"].Value == null)
                    return;

                string devicePartID = sYS_DeviceParts.Find(s => s.Reserve1.Equals("胶料秤")).DevicePartID;
                string selectPartID = dgvWeight.CurrentRow.Cells["DevicePartID"].Value.ToString();

                if (!devicePartID.Equals(selectPartID))
                {
                    cmbRuby.Enabled = false;
                }
                else
                {
                    cmbRuby.Enabled = true;
                }

                cmb_DevicePart.SelectedValue = selectPartID;
                if (dgvWeight.CurrentRow.Cells["MixPartID"].Value != null)
                {
                    string mixPartID = dgvWeight.CurrentRow.Cells["MixPartID"].Value.ToString();//密炼部位ID
                    cmb_Mix.SelectedValue = mixPartID;
                }

                txt_WeighSetVal.Text = dgvWeight.CurrentRow.Cells["WeighSetVal"].Value.ToString();
                txt_AllowError.Text = dgvWeight.CurrentRow.Cells["AllowError"].Value.ToString();
                txt_SetKuai.Text = dgvWeight.CurrentRow.Cells["Reserve2"].Value.ToString();
                txt_SetTiQian.Text = dgvWeight.CurrentRow.Cells["Reserve1"].Value.ToString();
                cmb_DropOrder.SelectedValue = dgvWeight.CurrentRow.Cells["DropOrder"].Value.ToString();
                cmb_WeighMaterial.SelectedValue = dgvWeight.CurrentRow.Cells["WeighMaterialID"].Value.ToString();
                cmbRuby.SelectedValue = dgvWeight.CurrentRow.Cells["Rubber"].Value;
                cmbScanner.SelectedValue = dgvWeight.CurrentRow.Cells["Scanner"].Value.ToString();
                lb_weightMessage.Text = NewuGlobal.GetRes("000170");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaLibrary").Error(ex.ToString());
            }
        }

        private void EnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && e.KeyChar != 46)
                e.Handled = true;
        }

        //配方修改记录选项
        private void FormulaUpdate_Click(object sender, EventArgs e)
        {
            FM_FormulaOperateLog formulaOperateLog = new FM_FormulaOperateLog(RecipeMainID, RecipeDeviceID);
            formulaOperateLog.Show();
        }

        private void Cmb_DevicePart_SelectedValueChanged(object sender, EventArgs e)
        {
            string devicePartID = sYS_DeviceParts.Find(s => s.PartNum == 6).DevicePartID;
            string selectPartID = cmb_DevicePart.SelectedValue.ToString();

            if (!devicePartID.Equals(selectPartID))
            {
                cmbRuby.Text = "否";
                cmbRuby.Enabled = false;
            }
            else
            {
                cmbRuby.Enabled = true;
            }
        }

        private void DgvTechParamUp_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void DgvMixUp_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        //优化卡顿问题 方法1 20231030
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;

                return cp;
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        /// <summary>
        /// 多语言切换
        /// </summary>
        private void SetControlLanguageText()
        {
            try
            {
                /***********  专有翻译区域   ***********/
                tabControl2.TabPages["tabPageStepUp"].Text = NewuGlobal.GetRes("000221");
                MixerStepUp.Text = NewuGlobal.GetRes("000247");
                MixerParamUp.Text = NewuGlobal.GetRes("000024");
                formulaUpdate.Text = NewuGlobal.GetRes("000825");
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    tabControl2.TabPages[1].Text = NewuGlobal.GetRes("000222");
                    MixerStepDown.Text = NewuGlobal.GetRes("000247");
                    MixerParamDown.Text = NewuGlobal.GetRes("000024");
                }

                cmbRuby.DataSource = EnableListR.GetList();
                dgvWeighRubber.DataSource = EnableListR.GetList();
                cmbScanner.DataSource = EnableList.GetList();

                this.Text = NewuGlobal.GetRes("000220");  // 配方信息
                label1.Text = NewuGlobal.GetRes("000211") + ":"; //所属设备
                label2.Text = NewuGlobal.GetRes("000212") + ":"; //配方名称

                label3.Text = NewuGlobal.GetRes("000262") + ":";// *称量部件
                cmb_WeighMaterial.Location = new Point(464, 14);

                label4.Text = NewuGlobal.GetRes("000263") + ":";// *称量材料
                cmb_WeighMaterial.Location = new Point(464, 40);

                label5.Text = NewuGlobal.GetRes("000264") + ":";// *设定重量
                txt_WeighSetVal.Location = new Point(748, 14);

                label6.Text = NewuGlobal.GetRes("000267") + ":";// *允许公差
                txt_AllowError.Location = new Point(748, 40);

                label9.Text = NewuGlobal.GetRes("000265") + ":";// *快称值
                txt_WeighSetVal.Location = new Point(970, 14);

                label10.Text = NewuGlobal.GetRes("000268") + ":";// *提前量
                txt_AllowError.Location = new Point(970, 40);

                label7.Text = NewuGlobal.GetRes("000236") + ":";// *投料次序
                cmb_DropOrder.Location = new Point(1187, 14);

                label8.Text = NewuGlobal.GetRes("000844") + ":";
                allWeight.Location = new Point(1852, 36);

                label11.Text = NewuGlobal.GetRes("000184") + ":";// *启用供胶机
                cmbRuby.Location = new Point(1413, 14);

                label12.Text = NewuGlobal.GetRes("000808") + ":";// *启用供胶机
                cmbScanner.Location = new Point(1413, 40);

                lb_weightMessage.Text = NewuGlobal.GetRes("000170");

                cl_all_weight.Text = NewuGlobal.GetRes("000214");  //计算总重

                groupBox6.Text = NewuGlobal.GetRes("000216");  // 配方列表
                groupBox2.Text = NewuGlobal.GetRes("000217");   // 称量部分
                groupBox3.Text = NewuGlobal.GetRes("000218");  //  密炼工艺
                groupBox1.Text = NewuGlobal.GetRes("000219");  // 操作选项
                groupBox4.Text = NewuGlobal.GetRes("000220");  // 配方信息
                groupBox5.Text = NewuGlobal.GetRes("000273");  //搜索选项
                gBoxOprate.Text = NewuGlobal.GetRes("000358");  //提示信息

                /***********  常见按钮   ***********/
                btnSave.Text = NewuGlobal.GetRes("000108"); //保存
                btnClose.Text = NewuGlobal.GetRes("000103");//关闭
                bt_SaveUpdate.Text = NewuGlobal.GetRes("000213");

                btnAddMaterial.Text = NewuGlobal.GetRes("000223"); //新增配方
                btnEditMaterial.Text = NewuGlobal.GetRes("000224"); //编辑配方
                btnCopyMaterial.Text = NewuGlobal.GetRes("000225"); //复制配方
                btnDeleteMaterial.Text = NewuGlobal.GetRes("000226"); //删除配方

                btn_Add.Text = NewuGlobal.GetRes("000100"); //新增
                btn_Edit.Text = NewuGlobal.GetRes("000101"); //编辑
                btn_Delete.Text = NewuGlobal.GetRes("000102"); //删除

                label3.Location = new Point(332, 15);// *称量部件
                cmb_WeighMaterial.Location = new Point(464, 14);
                cmb_Mix.Location = new Point(1187, 40);

                label4.Location = new Point(332, 43);// *称量材料
                cmb_WeighMaterial.Location = new Point(464, 39);

                label5.Location = new Point(616, 15);// *设定重量
                txt_WeighSetVal.Location = new Point(748, 14);

                label6.Location = new Point(616, 43);// *允许误差
                txt_AllowError.Location = new Point(748, 39);

                label9.Location = new Point(845, 15);// *快称值
                txt_SetKuai.Location = new Point(970, 14);

                label10.Location = new Point(845, 43);// *提前量
                txt_SetTiQian.Location = new Point(970, 39);

                label7.Location = new Point(1069, 15);// *投料顺序
                label8.Location = new Point(1069, 43);// *提前量

                label11.Location = new Point(1274, 15);// *启用供胶机
                cmbRuby.Location = new Point(1413, 14);

                label12.Location = new Point(1274, 43);// *启用扫描枪

                if (NewuGlobal.SupportLanguage.Equals("1"))
                {
                    dgvActionControlCode.DisplayMember = "ActionControlNameCN";
                    dgvActionControlCodeF.DisplayMember = "ActionControlNameCN";
                    dgvTechParamID.DisplayMember = "TechParamNameCN";

                    if (NewuGlobal.SoftConfig.DownMixer)
                        dgvTechParamFID.DisplayMember = "TechParamNameCN";

                    dgvDevicePartID.DisplayMember = "Reserve1";
                    cmb_DevicePart.DisplayMember = "Reserve1";
                    cmb_Mix.DisplayMember = "Reserve1";
                    dgvMixPartID.DisplayMember = "Reserve1";
                    bt_SaveUpdate.Padding = new Padding(0, 0, 7, 0);
                    btnClose.Padding = new Padding(0, 0, 7, 0);
                    btnDeleteMaterial.Padding = new Padding(0, 0, 7, 0);
                    btn_Delete.Padding = new Padding(0, 0, 7, 0);
                    cl_all_weight.Size = new Size(93, 30);
                }
                else
                {
                    dgvActionControlCode.DisplayMember = "ActionControlNameEN";
                    dgvActionControlCodeF.DisplayMember = "ActionControlNameEN";
                    dgvTechParamID.DisplayMember = "TechParamNameEN";

                    if (NewuGlobal.SoftConfig.DownMixer)
                        dgvTechParamFID.DisplayMember = "TechParamNameEN";

                    dgvDevicePartID.DisplayMember = "DevicePartName";
                    cmb_DevicePart.DisplayMember = "DevicePartName";
                    cmb_Mix.DisplayMember = "DevicePartName";
                    dgvMixPartID.DisplayMember = "DevicePartName";
                    bt_SaveUpdate.Padding = new Padding(0, 0, 0, 0);
                    btnClose.Padding = new Padding(0, 0, 0, 0);
                    btnDeleteMaterial.Padding = new Padding(0, 0, 0, 0);
                    btn_Delete.Padding = new Padding(0, 0, 0, 0);
                    cl_all_weight.Size = new Size(117, 30);
                }

                radB_SY.Text = NewuGlobal.GetRes("000257"); //使用库
                radB_ZF.Text = NewuGlobal.GetRes("000258"); //废弃库

                /***********  常见文字提示   ***********/
                dgvWeight.Columns[1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000234");
                dgvWeight.Columns[2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000235");
                dgvWeight.Columns[3].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000844");
                dgvWeight.Columns[4].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000236");
                dgvWeight.Columns[5].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000237");
                dgvWeight.Columns[6].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000238");
                dgvWeight.Columns[7].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000239");
                dgvWeight.Columns[8].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000240");
                dgvWeight.Columns[9].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000241");
                dgvWeight.Columns[dgvWeight.Columns.Count - 1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000682");
                dgvWeight.Columns[dgvWeight.Columns.Count - 2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000184");

                LanguageDGV(dgvFormulaList, 228);  //228就是该表翻译的起始地点，不服查表
                LanguageDGV(dgvMixUp, 244);  //228就是该表翻译的起始地点，不服查表dgvTechParam
                LanguageDGV(dgvMixDown, 244);
                LanguageDGV(dgvTechParamUp, 254);  //228就是该表翻译的起始地点，不服查表
                LanguageDGV(dgvTechParamDown, 254);
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("配方库：" + ex.ToString());
            }
        }

        private void LanguageDGV(DataGridViewEx dgv, int start)
        {
            if (dgv != null && dgv.Columns != null)
                for (int i = 1; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                }

            if (dgv.Name.Equals("dgvMixUp") || dgv.Name.Equals("dgvMixDown"))
                dgv.Columns[0].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000244");
            else if (dgv.Name.Equals("dgvTechParamUp") || dgv.Name.Equals("dgvTechParamDown"))
                dgv.Columns[0].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000254");
        }

        private void MixerStepUp_Enter(object sender, EventArgs e)
        {
        }
    }
}