using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuView.Mix
{
    /// <summary>
    /// 胶料界面
    /// </summary>
    public class ViewHelper
    {
        private static CSharedString ss = NewuGlobal.MemDB;
        public static List<MixerShow> mixerechList;
        public static List<MixerShow> mixerechDownList;
        private static bool allIsZERO = false;
        private static readonly FormulaMixRepository formulaMixRepository = new FormulaMixRepository();
        private static FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private static readonly FormulaMixFRepository formulaMixFRepository = new FormulaMixFRepository();

        public static void InitWeightDataGridView(DataGridViewEx dgv)
        {
            ColStruct[] cols = new ColStruct[]
               {
                    new ColStruct("Reserve4","投料次序"),
                    new ColStruct("WeighOrder","称量次序",ColumnType.txt,false),
                    new ColStruct("WeighMaterialID","物料名称", ColumnType.cmb,true),
                    new ColStruct("DevicePartID","称量部件",ColumnType.txt,false),
                    new ColStruct("WeighSetVal","标准重量"),
                    new ColStruct("WeighActVal","实际重量"),
                    new ColStruct("AllowError","公差"),
                    new ColStruct("Reserve3","扫描",ColumnType.txt,true),
                    new ColStruct("Scanner","是否启用",ColumnType.txt,true),
               };

            dgv.VisibleOrderNumber = false;
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ScrollBars = ScrollBars.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 50;
            dgv.RowTemplate.MinimumHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("黑体", 15, FontStyle.Bold);
            dgv.Columns[0].FillWeight = 90;
            dgv.Columns[2].FillWeight = 200;
            dgv.Columns[4].FillWeight = 95;
            dgv.Columns[5].FillWeight = 95;
            dgv.Columns[6].FillWeight = 90;
            dgv.Columns[7].FillWeight = 50;
            dgv.Columns[8].FillWeight = 90;

            DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgv.Columns["WeighMaterialID"];
            dgvWeighMaterialID.DataSource = formulaMaterialRepository.GetList("DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID=''");
            dgvWeighMaterialID.DisplayMember = "MaterialCode";
            dgvWeighMaterialID.ValueMember = "MaterialID";
        }

        /// <summary>
        /// 初始化密炼表格
        /// </summary>
        /// <param name="dgv"></param>
        public static void InitMixTechDataGridView(DataGridViewEx dgv)
        {
            #region 密炼Grid初始化

            ColStruct[] mixCols = new ColStruct[]{
                new ColStruct("StepOrder","No"),
                new ColStruct("ActionControlCode","控制方式",ColumnType.cmb,true),
                new ColStruct("StepTime","时间(设)"),
                new ColStruct("PlcStepTime","时间(实)"),
                new ColStruct("StepDesc","工艺步骤"),
                new ColStruct("StepTemp","温度(设)"),
                new ColStruct("PlcStepTemp","温度(实)")
            };

            dgv.AllowUserToResizeColumns = true;
            dgv.ReadOnly = true;
            dgv.AddCols(mixCols);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns[0].FillWeight = 50;
            dgv.Columns[1].FillWeight = 200;
            dgv.Columns[4].FillWeight = 230;
            dgv.Enabled = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.ColumnHeadersHeight = 50;
            dgv.RowTemplate.MinimumHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("黑体", 15, FontStyle.Bold);

            if (dgv.Name.Equals("dgvEXMixer"))
                dgv.DataSource = mixerechList;
            else
                dgv.DataSource = mixerechDownList;

            DataGridViewComboBoxColumn dgvActionControlCode = dgv.GetComboBoxColumn("ActionControlCode");

            dgvActionControlCode.DataSource = new SYS_ActionControlRepository().GetList("DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID=''");

            if (NewuGlobal.SupportLanguage.Equals("1"))
                dgvActionControlCode.DisplayMember = "ActionControlNameCN";
            else
                dgvActionControlCode.DisplayMember = "ActionControlNameEN";

            dgvActionControlCode.ValueMember = "ActionControlCode";
            /*
             *todo:监控界面 配置能量表小数位数
             */
            dgv.Columns["StepTime"].DefaultCellStyle.Format = "0";
            dgv.Columns["PlcStepTime"].DefaultCellStyle.Format = "0";
            dgv.Columns["StepTemp"].DefaultCellStyle.Format = "0.0";
            dgv.Columns["PlcStepTemp"].DefaultCellStyle.Format = "0.0";
            dgv.Columns[3].DefaultCellStyle.ForeColor = Color.Red;
            dgv.Columns[6].DefaultCellStyle.ForeColor = Color.Red;

            #endregion 密炼Grid初始化

            return;
        }

        /// <summary>
        /// 清空密炼数据
        /// </summary>
        /// <param name="dgv">DataGridView控件名称</param>
        public static void ClearTechData(DataGridView dgv, List<MixerShow> techList)
        {
            int cnt = 0;
            foreach (MixerShow item in techList)
            {
                item.PlcStepTime = "";
                item.PlcStepTime = "";
                item.PlcStepTemp = "";
                dgv.InvalidateRow(cnt++);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="dgvTech"></param>
        public static void DisPlayTable(DataGridViewEx dgvTech)
        {
            if (dgvTech == null)
                return;
            NewuGlobal.RunInfo.InitData();
            DisPlayMixTechCurrent(dgvTech);
        }

        /// <summary>
        /// 密炼工艺数据
        /// </summary>
        /// <param name="dgv"></param>
        private static void DisPlayMixTechCurrent(DataGridViewEx dgv)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new Action(() =>
                {
                    GetMixTech(dgv);
                }));
            }
            else
            {
                GetMixTech(dgv);
            }
        }

        public static void GetMixTech(DataGridViewEx dgv)
        {
            if (dgv.Name.Equals("dgvEXMixer"))
            {
                List<FormulaMix> list = formulaMixRepository.GetMixDetail(NewuGlobal.RunInfo.OrderMixModel.MaterialID);

                mixerechList = new List<MixerShow>();
                foreach (FormulaMix item in list)
                {
                    mixerechList.Add(InitChangeMixMDLToShowMDL(item));
                }
                dgv.DataSource = mixerechList;
            }
            else
            {
                List<FormulaMixF> list = formulaMixFRepository.GetMixDetail(NewuGlobal.RunInfo.OrderMixModel.MaterialID);

                mixerechDownList = new List<MixerShow>();
                foreach (FormulaMixF item in list)
                {
                    mixerechDownList.Add(InitMixMDLToShowMDL(item));
                }
                dgv.DataSource = mixerechDownList;
            }
        }

        private static MixerShow InitChangeMixMDLToShowMDL(FormulaMix item)
        {
            MixerShow newItem = new MixerShow
            {
                StepOrder = item.StepOrder,
                StepDesc = item.StepDesc,
                StepTime = item.StepTime,
                StepTemp = item.StepTemp,
                StepPower = item.StepPower,
                StepEnergy = item.StepEnergy,
                StepPress = item.StepPress,
                StepSpeed = item.StepSpeed,
                KeepTime = item.KeepTime,
                ActionControlCode = item.ActionControlCode
            };
            return newItem;
        }

        private static MixerShow InitMixMDLToShowMDL(FormulaMixF item)
        {
            MixerShow newItem = new MixerShow
            {
                StepOrder = item.StepOrder,
                StepDesc = item.StepDesc,
                StepTime = item.StepTime,
                StepTemp = item.StepTemp,
                StepPower = item.StepPower,
                StepEnergy = item.StepEnergy,
                StepPress = item.StepPress,
                StepSpeed = item.StepSpeed,
                KeepTime = item.KeepTime,
                ActionControlCode = item.ActionControlCode
            };
            return newItem;
        }

        /// <summary>
        /// 密炼工艺表格 监控
        /// </summary>
        /// <param name="dgv"></param>
        public static void RefreshMixTechDataGridView(DataGridView dgv)
        {
            try
            {
                if (mixerechList == null)
                    return;

                // 根据内存地址点 更新数据
                int cnt = mixerechList.Count - 1;
                bool flag = true;
                for (int i = AddressConst.MixerTechStart + mixerechList.Count - 1; i >= AddressConst.MixerTechStart; i--, cnt--)
                {
                    if (ss.Getbool(i))
                    {
                        flag = false;
                        dgv.Rows[cnt].Selected = true;
                        dgv.FirstDisplayedScrollingRowIndex = cnt;
                        dgv.InvalidateRow(cnt);
                        allIsZERO = true;
                        break;
                    }
                }
                if (flag)
                {
                    dgv.ClearSelection();
                    if (allIsZERO == true)
                        ClearTechData(dgv, mixerechList);
                }
                cnt = mixerechList.Count - 1;
                for (int i = AddressConst.MixerTechEnd + mixerechList.Count - 1; i >= AddressConst.MixerTechEnd; i--, cnt--)
                {
                    if (ss.Getbool(i))
                    {
                        string a = ss.GetInt((int)MixerAnalogMiningMixer.Time, 4).ToString();
                        if (a == "0")
                        {
                            continue;
                        }
                        mixerechList[cnt].PlcStepTime = a;
                        mixerechList[cnt].PlcStepTemp = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixer.Temp, 4)) / ScaleAccuracy.digitTemp).ToString();

                        dgv.InvalidateRow(cnt);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                NewuGlobal.LogCat("ViewHelper").Error(e.ToString());
            }
        }

        /// <summary>
        /// 密炼工艺表格 监控
        /// </summary>
        /// <param name="dgv"></param>
        public static void RefreshMixTechDataGridViewF(DataGridView dgv)
        {
            try
            {
                if (mixerechDownList == null)
                    return;

                // 根据内存地址点 更新数据
                int cnt = mixerechDownList.Count - 1;
                bool flag = true;
                for (int i = AddressConst.MixerTechDownStart + mixerechDownList.Count - 1; i >= AddressConst.MixerTechDownStart; i--, cnt--)
                {
                    if (ss.Getbool(i))
                    {
                        flag = false;
                        dgv.Rows[cnt].Selected = true;
                        dgv.FirstDisplayedScrollingRowIndex = cnt;
                        dgv.InvalidateRow(cnt);
                        allIsZERO = true;
                        break;
                    }
                }
                if (flag)
                {
                    dgv.ClearSelection();
                    if (allIsZERO == true)
                        ClearTechData(dgv, mixerechDownList);
                }
                cnt = mixerechDownList.Count - 1;
                for (int i = AddressConst.MixerTechDownEnd + mixerechDownList.Count - 1; i >= AddressConst.MixerTechDownEnd; i--, cnt--)
                {
                    if (ss.Getbool(i))
                    {
                        string a = ss.GetInt((int)MixerAnalogMiningMixerDown.Time, 4).ToString();
                        if (a == "0")
                        {
                            continue;
                        }
                        mixerechDownList[cnt].PlcStepTime = a;
                        mixerechDownList[cnt].PlcStepTemp = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixerDown.Temp, 4)) / ScaleAccuracy.digitTemp).ToString();

                        dgv.InvalidateRow(cnt);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                NewuGlobal.LogCat("ViewHelper").Error(e.ToString());
            }
        }

        public static void ClearWeightData(DataGridViewEx dgv, List<View_FormulaWeigh> list)
        {
            int cnt = 0;
            if (list == null)
                return;
            foreach (View_FormulaWeigh item in list)
            {
                item.WeighActVal = "";
                dgv.InvalidateRow(cnt++);
            }
        }

        public static void LanguageDGV(DataGridViewEx dgv, int start)
        {
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible == true)
                    {
                        dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                    }
                }
            if (dgv.Name.Equals("dgvExWeight"))
                dgv.Columns["Scanner"].HeaderText = NewuGlobal.GetRes("000821");
        }

        /// <summary>
        /// 解决新增物料 被添加到新增配方中 直接发送PLC执行 导致dgv表格弹框报错
        /// </summary>
        /// <param name="dgv"></param>
        public static void IsRefresh(DataGridViewEx dgv)
        {
            DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgv.Columns["WeighMaterialID"];
            dgvWeighMaterialID.DataSource = formulaMaterialRepository.GetList("DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID=''");
            dgvWeighMaterialID.DisplayMember = "MaterialCode";
            dgvWeighMaterialID.ValueMember = "MaterialID";
        }
    }
}