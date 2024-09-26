using Newtonsoft.Json;
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
    public partial class FM_FormulaOperateLog : Form
    {
        private string materialID = "";
        private string deviceID = "";
        private readonly SYS_DevicePartRepository devicePartRepository = new SYS_DevicePartRepository();
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly SYS_ActionControlRepository actionControlRepository = new SYS_ActionControlRepository();
        private readonly SYS_ActionStepRepository actionStepRepository = new SYS_ActionStepRepository();
        private readonly SYS_TechParamFRepository techParamRepository = new SYS_TechParamFRepository();
        private readonly TB_FormulaOperateLogRepository formulaOperateLogRepository = new TB_FormulaOperateLogRepository();

        private DataGridViewComboBoxColumn dgvDevicePartID;
        private DataGridViewComboBoxColumn dgvActionControlCode;
        private DataGridViewComboBoxColumn dgvActionControlCodeF;
        private DataGridViewComboBoxColumn dgvTechParamID;
        private DataGridViewComboBoxColumn dgvTechParamFID;

        private List<SYS_DevicePart> sYS_DeviceParts;
        private List<FormulaMaterial> formulaMaterials;
        private List<SYS_ActionControl> sYS_ActionControls;
        private List<SYS_TechParam> sYS_TechParams;
        private List<SYS_TechParam> sYS_TechParamsDown;

        private List<TB_FormulaOperateLog> tB_FormulaOperateLogs;

        public FM_FormulaOperateLog(string recipeMainID, string recipeDeviceID)
        {
            InitializeComponent();
            materialID = recipeMainID;
            deviceID = recipeDeviceID;
            //获取物料库中所有数据（配方和物料）
            formulaMaterials = formulaMaterialRepository.GetList("");
        }

        private void FM_FormulaOperateLog_Load(object sender, EventArgs e)
        {
            InitView();
            GetData();

            if (!NewuGlobal.SoftConfig.DownMixer)
                tabControl2.TabPages.Remove(tabPageStepDown);
            SetControlLanguage();
        }

        private void InitView()
        {
            try
            {
                #region 配方列表

                ColStruct[] formulaListCols = new ColStruct[]
                {
                    new ColStruct("MaterialCode","配方名称"),
                    new ColStruct("SaveTime","保存时间")
                };

                dgv_formulaList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv_formulaList.AddCols(formulaListCols);
                dgv_formulaList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_formulaList.AllowUserToAddRows = false;
                dgv_formulaList.MultiSelect = false;
                dgv_formulaList.ReadOnly = true;
                dgv_formulaList.Columns["SaveTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgv_formulaList.Columns["SaveTime"].FillWeight = 150;

                #endregion 配方列表

                #region 称量部分

                ColStruct[] cols = new ColStruct[]
                {
                    new ColStruct("WeighMaterialID","称量材料",ColumnType.cmb,true),
                    new ColStruct("DevicePartID","设备部位",ColumnType.cmb,true),
                    new ColStruct("DropOrder","投料顺序"),
                    new ColStruct("WeighOrder","称量顺序"),
                    new ColStruct("WeighSetVal","标准值"),
                    new ColStruct("AllowError","误差值"),
                    new ColStruct("Reserve2","快称值"),
                    new ColStruct("Reserve1","提前量"),
                    new ColStruct("Rubber","供胶机",ColumnType.cmb,true),
                    new ColStruct("Scanner","扫描枪",ColumnType.chk,true)
                };

                dgv_FormulaWeight.Dock = DockStyle.Fill;
                dgv_FormulaWeight.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv_FormulaWeight.AddCols(cols);
                dgv_FormulaWeight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_FormulaWeight.MultiSelect = false;
                dgv_FormulaWeight.ReadOnly = true;

                sYS_DeviceParts = devicePartRepository.GetDevicePartList();
                dgvDevicePartID = (DataGridViewComboBoxColumn)dgv_FormulaWeight.Columns["DevicePartID"];
                dgvDevicePartID.DataSource = sYS_DeviceParts;
                dgvDevicePartID.ValueMember = "DevicePartID";
                dgvDevicePartID.DisplayMember = "Reserve1";

                DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgv_FormulaWeight.Columns["WeighMaterialID"];
                List<FormulaMaterial> weightMaterial = formulaMaterials.FindAll(e => e.DeviceID.Equals(deviceID) || e.DeviceID.Equals(""));
                dgvWeighMaterialID.DataSource = weightMaterial;
                dgvWeighMaterialID.DisplayMember = "MaterialCode";
                dgvWeighMaterialID.ValueMember = "MaterialID";

                DataGridViewComboBoxColumn dgvWeighRubber = (DataGridViewComboBoxColumn)dgv_FormulaWeight.Columns["Rubber"];
                dgvWeighRubber.DataSource = EnableListR.GetList();
                dgvWeighRubber.DisplayMember = "Name";
                dgvWeighRubber.ValueMember = "Value";

                #endregion 称量部分

                #region 工艺步骤

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
                dgvMixUp.MultiSelect = false;
                dgvMixUp.Columns[1].FillWeight = 100;
                dgvMixUp.Columns[2].FillWeight = 50;
                dgvMixUp.Columns[3].FillWeight = 270;
                dgvMixUp.ReadOnly = true;

                sYS_ActionControls = actionControlRepository.GetList("DeviceID='" + deviceID + "' or DeviceID=''");
                dgvActionControlCode = dgvMixUp.GetComboBoxColumn("ActionControlCode");
                dgvActionControlCode.DataSource = sYS_ActionControls;
                dgvActionControlCode.DisplayMember = "ActionControlNameCN";
                dgvActionControlCode.ValueMember = "ActionControlCode";

                DataGridViewComboBoxColumn dgvStepOrder = dgvMixUp.GetComboBoxColumn("StepOrder");
                dgvStepOrder.DataSource = actionStepRepository.GetStepOrderTable();
                dgvStepOrder.DisplayMember = "name";
                dgvStepOrder.ValueMember = "value";
                /*
                 *todo:配置能量表小数位数
                 */
                dgvMixUp.Columns["StepTime"].DefaultCellStyle.Format = "0";
                dgvMixUp.Columns["StepTemp"].DefaultCellStyle.Format = "0.0";
                dgvMixUp.Columns["StepPower"].DefaultCellStyle.Format = "0";
                dgvMixUp.Columns["StepEnergy"].DefaultCellStyle.Format = "0.0";
                dgvMixUp.Columns["StepPress"].DefaultCellStyle.Format = "0.00";
                dgvMixUp.Columns["StepSpeed"].DefaultCellStyle.Format = "0.0";

                #endregion 工艺步骤

                #region 系统参数

                ColStruct[] mixTechParamCols = new ColStruct[]
                {
                    new ColStruct("TechParamID","参数名称",ColumnType.cmb,true),
                    new ColStruct("Reserve1","单位"),
                    new ColStruct("TechParamVal","参数值")
                };

                dgvTechParamUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTechParamUp.AddCols(mixTechParamCols);
                dgvTechParamUp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTechParamUp.AllowUserToAddRows = false;
                dgvTechParamUp.MultiSelect = false;
                dgvTechParamUp.ReadOnly = true;

                string devicePartId = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
                sYS_TechParams = techParamRepository.GetList($"DeviceID='{deviceID}' and DevicePartID='{devicePartId}'");
                dgvTechParamID = dgvTechParamUp.GetComboBoxColumn("TechParamID");
                dgvTechParamID.DataSource = sYS_TechParams;
                dgvTechParamID.ValueMember = "TechParamID";
                dgvTechParamID.DisplayMember = "TechParamNameCN";

                #endregion 系统参数

                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    dgvMixDown.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvMixDown.AllowUserToResizeColumns = true;
                    dgvMixDown.AddCols(mixCols);
                    dgvMixDown.Columns[3].ReadOnly = true;
                    dgvMixDown.MultiSelect = false;
                    dgvMixDown.Columns[1].FillWeight = 100;
                    dgvMixDown.Columns[2].FillWeight = 50;
                    dgvMixDown.Columns[3].FillWeight = 270;
                    dgvMixDown.ReadOnly = true;

                    List<SYS_ActionControl> actionControls = new List<SYS_ActionControl>();
                    actionControls.AddRange(NewuGlobal.ActionControlList);
                    dgvActionControlCodeF = dgvMixDown.GetComboBoxColumn("ActionControlCode");
                    dgvActionControlCodeF.DataSource = actionControls;
                    dgvActionControlCodeF.DisplayMember = "ActionControlNameCN";
                    dgvActionControlCodeF.ValueMember = "ActionControlCode";

                    DataGridViewComboBoxColumn dgvStepOrderF = dgvMixDown.GetComboBoxColumn("StepOrder");
                    dgvStepOrderF.DataSource = actionStepRepository.GetStepOrderTable();
                    dgvStepOrderF.DisplayMember = "name";
                    dgvStepOrderF.ValueMember = "value";
                    dgvTechParamDown.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvTechParamDown.AddCols(mixTechParamCols);
                    dgvTechParamDown.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvTechParamDown.AllowUserToAddRows = false;
                    dgvTechParamDown.MultiSelect = false;

                    dgvMixDown.Columns["StepTime"].DefaultCellStyle.Format = "0";
                    dgvMixDown.Columns["StepTemp"].DefaultCellStyle.Format = "0.0";
                    dgvMixDown.Columns["StepPower"].DefaultCellStyle.Format = "0";
                    dgvMixDown.Columns["StepEnergy"].DefaultCellStyle.Format = "0.0";
                    dgvMixDown.Columns["StepPress"].DefaultCellStyle.Format = "0.00";
                    dgvMixDown.Columns["StepSpeed"].DefaultCellStyle.Format = "0.0";

                    string devicePartFId = NewuGlobal.GetDevicePartIDByPartCode(NewuGlobal.GetDevicePartCode(DevicePartType.MixDown));
                    sYS_TechParamsDown = techParamRepository.GetList($"DeviceID='{deviceID}' and DevicepartID='{devicePartFId}'");
                    dgvTechParamFID = dgvTechParamDown.GetComboBoxColumn("TechParamID");
                    dgvTechParamFID.DataSource = sYS_TechParamsDown;
                    dgvTechParamFID.ValueMember = "TechParamID";
                    dgvTechParamFID.DisplayMember = "TechParamNameCN";

                    dgvTechParamDown.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaOperateLog").Error(ex.ToString());
            }
        }

        private void GetData()
        {
            try
            {
                dt_start.Value = dt_start.Value.Date;
                DateTime st = DateTime.Now.Date;
                DateTime et = DateTime.Now.AddDays(1).Date.AddSeconds(-1);

                tB_FormulaOperateLogs = formulaOperateLogRepository.GetList(materialID, st, et);
                dgv_formulaList.DataSource = tB_FormulaOperateLogs;
                dgv_formulaList.Rows[0].Selected = false;
                dgv_formulaList.CellClick += Dgv_formulaList_CellClick;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaOperateLog").Error(ex.ToString());
            }
        }

        private void Dgv_formulaList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex == -1)
                    return;
                TB_FormulaOperateLog formulaOperateLog = tB_FormulaOperateLogs[rowIndex];
                string strWeight = formulaOperateLog.FormulaWeight;
                string strMixStep = formulaOperateLog.FormulaMixStep;
                string strTechParam = formulaOperateLog.FormulaTechParam;
                string strMixStepF = formulaOperateLog.FormulaMixStepF;
                string strTechParamF = formulaOperateLog.FormulaTechParamF;
                List<FormulaWeigh> weights = JsonConvert.DeserializeObject<List<FormulaWeigh>>(strWeight);

                List<FormulaMix> mixs = JsonConvert.DeserializeObject<List<FormulaMix>>(strMixStep);
                List<FormulaTechParam> techParams = JsonConvert.DeserializeObject<List<FormulaTechParam>>(strTechParam);
                dgv_FormulaWeight.DataSource = weights;

                foreach (var item in techParams)
                {
                    item.Reserve1 = sYS_TechParams.Find(f => f.TechParamID.Equals(item.TechParamID)).Unit;
                }
                dgvMixUp.DataSource = mixs;
                dgvTechParamUp.DataSource = techParams;

                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    List<FormulaMixF> mixFs = JsonConvert.DeserializeObject<List<FormulaMixF>>(strMixStepF);
                    List<FormulaTechParamF> techParamFs = JsonConvert.DeserializeObject<List<FormulaTechParamF>>(strTechParamF);
                    foreach (var item in techParamFs)
                    {
                        item.Reserve1 = sYS_TechParamsDown.Find(f => f.TechParamID.Equals(item.TechParamID)).Unit;
                    }

                    dgvMixDown.DataSource = mixFs;
                    dgvTechParamDown.DataSource = techParamFs;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaOperateLog").Error(ex.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime st = dt_start.Value;
            DateTime et = dt_end.Value;
            tB_FormulaOperateLogs = formulaOperateLogRepository.GetList(materialID, st, et);
            dgv_formulaList.DataSource = tB_FormulaOperateLogs;
        }

        private void SetControlLanguage()
        {
            try
            {
                tabControl2.TabPages["tabPageStepUp"].Text = NewuGlobal.GetRes("000221");
                MixerStepUp.Text = NewuGlobal.GetRes("000247");
                MixerParamUp.Text = NewuGlobal.GetRes("000024");
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    tabControl2.TabPages[1].Text = NewuGlobal.GetRes("000222");
                    MixerStepDown.Text = NewuGlobal.GetRes("000247");
                    MixerParamDown.Text = NewuGlobal.GetRes("000024");
                }

                groupBox1.Text = NewuGlobal.GetRes("000219");  // 操作选项
                label1.Text = NewuGlobal.GetRes("000301") + ":";//开始时间
                label2.Text = NewuGlobal.GetRes("000302") + ":";//结束时间
                button1.Text = NewuGlobal.GetRes("000104");//查询
                groupBox2.Text = NewuGlobal.GetRes("000480");//历史配方
                grb_weight.Text = NewuGlobal.GetRes("000217");//称量部分
                groupBox3.Text = NewuGlobal.GetRes("000218");//密炼工艺
                dgv_formulaList.Columns[0].HeaderText = NewuGlobal.GetRes("000199");
                dgv_formulaList.Columns[1].HeaderText = NewuGlobal.GetRes("000081");

                if (NewuGlobal.SupportLanguage.Equals("1"))
                {
                    dgvActionControlCode.DisplayMember = "ActionControlNameCN";
                    dgvTechParamID.DisplayMember = "TechParamNameCN";
                    dgvDevicePartID.DisplayMember = "Reserve1";

                    if (NewuGlobal.SoftConfig.DownMixer)
                    {
                        dgvActionControlCodeF.DisplayMember = "ActionControlNameCN";
                        dgvTechParamFID.DisplayMember = "TechParamNameCN";
                    }
                }
                else
                {
                    dgvActionControlCode.DisplayMember = "ActionControlNameEN";
                    dgvTechParamID.DisplayMember = "TechParamNameEN";
                    dgvDevicePartID.DisplayMember = "DevicePartName";

                    if (NewuGlobal.SoftConfig.DownMixer)
                    {
                        dgvActionControlCodeF.DisplayMember = "ActionControlNameEN";
                        dgvTechParamFID.DisplayMember = "TechParamNameEN";
                    }
                }

                LanguageDGV(dgv_FormulaWeight, 233);  //228就是该表翻译的起始地点，不服查表
                LanguageDGV(dgvMixUp, 244);  //228就是该表翻译的起始地点，不服查表dgvTechParam
                LanguageDGV(dgvTechParamUp, 254);  //228就是该表翻译的起始地点，不服查表
              
                if (NewuGlobal.SoftConfig.DownMixer)
                {
                    LanguageDGV(dgvMixDown, 244);
                    LanguageDGV(dgvTechParamDown, 254);
                }
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
                    if (dgv.Name.Equals("dgv_FormulaWeight"))
                        dgv.Columns[i - 1].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                    else
                        dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                }

            if (dgv.Name.Equals("dgvMixUp") || dgv.Name.Equals("dgvMixDown"))
                dgv.Columns[0].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000244");
            else if (dgv.Name.Equals("dgvTechParamUp") || dgv.Name.Equals("dgvTechParamDown"))
                dgv.Columns[0].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000254");
            else if (dgv.Name.Equals("dgv_FormulaWeight"))
            {
                dgv.Columns[dgv.Columns.Count - 1].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000682");
                dgv.Columns[dgv.Columns.Count - 2].HeaderText = NewuGlobal.LanguagResourceManager.GetString("000184");
            }
        }
    }
}