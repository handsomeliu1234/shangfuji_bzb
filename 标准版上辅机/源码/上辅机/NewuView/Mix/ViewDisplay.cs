using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuView
{
    /// <summary>
    /// 主屏界面
    /// </summary>
    public static class ViewDisplay
    {
        private static CSharedString ss = NewuGlobal.MemDB;
        private static Dictionary<int, string> myDictionary = null;
        public static List<FormulaWeigh> rawLists;
        public static List<MixerWeightShow> weightList;
        public static List<MixerShow> mixTechLists;
        public static List<MixerShow> mixTechDownLists;
        private static FormulaWeighRepository formulaWeighRepository = new FormulaWeighRepository();
        private static FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private static FormulaMixRepository formulaMixRepository = new FormulaMixRepository();
        private static FormulaMixFRepository formulaMixFRepository = new FormulaMixFRepository();
        public static int nowSelect = 0;

        public static Dictionary<int, string> MyDictionary
        {
            get
            {
                if (myDictionary == null)
                {
                    Init();
                }
                return myDictionary;
            }
            set
            {
                myDictionary = value;
            }
        }

        private static void Init()
        {
            myDictionary = new Dictionary<int, string>
            {
                { (int)MixerNetWeight.Carbon, NewuGlobal.DevicePartCodeByID(DevicePartType.Carbon) },
                { (int)MixerNetWeight.Oil, NewuGlobal.DevicePartCodeByID(DevicePartType.Oil) },
                { (int)MixerNetWeight.Zno, NewuGlobal.DevicePartCodeByID(DevicePartType.Zno) },
                { (int)MixerNetWeight.Plasticizer, NewuGlobal.DevicePartCodeByID(DevicePartType.Plasticizer) },
                { (int)MixerNetWeight.Silane, NewuGlobal.DevicePartCodeByID(DevicePartType.Silane) },
                { (int)MixerNetWeight.Rubber, NewuGlobal.DevicePartCodeByID(DevicePartType.Rubber) },
                { (int)MixerNetWeight.DrugMixer, NewuGlobal.DevicePartCodeByID(DevicePartType.DrugMixer) }
            };
        }

        /// <summary>
        ///密炼机实例监控
        /// </summary>
        /// <param name="MixPart1">密炼机部件实例</param>
        public static void MixPart(MixPart Part_Mix)
        {
            //密炼机运行
            Part_Mix.SetMixRun(ss.Getbool((int)MixerDigitalMiningMixer.Running));

            //上顶栓高、中、低位
            Part_Mix.SetMixShuanLocation(ss.Getbool((int)MixerDigitalMiningMixer.RamHigh), ss.Getbool((int)MixerDigitalMiningMixer.RamMid), ss.Getbool((int)MixerDigitalMiningMixer.RamLow));

            //加料门开、关
            Part_Mix.SetMixInDoor(ss.Getbool((int)MixerDigitalMiningMixer.FeedingDoorOpen), ss.Getbool((int)MixerDigitalMiningMixer.FeedingDoorClose));

            //下顶栓开
            Part_Mix.SetMixOutDoor(ss.Getbool((int)MixerDigitalMiningMixer.BelowRamOpen), ss.Getbool((int)MixerDigitalMiningMixer.BelowRamClose));
        }

        public static void MixPartF(DownMixPart mixDownPart)
        {
            //密炼机运行
            mixDownPart.SetMixRun(ss.Getbool((int)MixerDigitalMiningMixerDown.Running));

            //下顶栓开
            mixDownPart.SetMixOutDoor(ss.Getbool((int)MixerDigitalMiningMixerDown.BelowRamOpen), ss.Getbool((int)MixerDigitalMiningMixerDown.BelowRamClose));
        }

        /**********   主界面和胶料界面  称量解耦  *************/
        /** 使用主界面两个表格，方式
         * 1.在界面上去添加两个dgv
         * 2.实现IRefresh接口  在load时 将自身的引用转入Gloabl
         *
         *   在Load 中调用
         *  ViewDisplay.InitWeightDataGridView(dgvWeight);
            ViewDisplay.InitMixTechDataGridView(dgvTech);
            ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
         * 3.配方更换，使用接口刷新 调用
             ViewDisplay.DisPlayTable(dgvTech, dgvWeight);
         * 4.使用Time轮询监控调用
         *       ViewDisplay.RefreshMixTechDataGridView(dgvTech);
                ViewDisplay.RefreshWeightDataGridView(dgvTech);
         * 5. OJBK。
         *
         */

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
                new ColStruct("PlcStepTemp","温度(实)"),
                new ColStruct("StepPower","功率(设)"),
                new ColStruct("PlcStepPower","功率(实)"),
                new ColStruct("StepEnergy","能量(设)"),
                new ColStruct("PlcStepEnergy","能量(实)"),
                new ColStruct("StepPress","压力(设)"),
                new ColStruct("PlcStepPress","压力(实)"),
                new ColStruct("StepSpeed","转速(设)"),
                new ColStruct("PlcStepSpeed","转速(实)")
            };

            DataGridViewHelper helper = new DataGridViewHelper(dgv);
            helper.Headers.Add(new DataGridViewHelper.TopHeader(2, 2, "000635"));
            helper.Headers.Add(new DataGridViewHelper.TopHeader(5, 2, "000636"));
            helper.Headers.Add(new DataGridViewHelper.TopHeader(7, 2, "000637"));
            helper.Headers.Add(new DataGridViewHelper.TopHeader(9, 2, "000638"));
            helper.Headers.Add(new DataGridViewHelper.TopHeader(11, 2, "000639"));
            helper.Headers.Add(new DataGridViewHelper.TopHeader(13, 2, "000288"));

            dgv.AllowUserToResizeColumns = true;
            dgv.ReadOnly = true;
            dgv.AddCols(mixCols);
            dgv.Columns[0].FillWeight = 40;
            dgv.Columns[1].FillWeight = 200;
            dgv.Columns[4].FillWeight = 250;
            dgv.Enabled = false;
            dgv.AllowUserToAddRows = false;
            dgv.ScrollBars = ScrollBars.None;
            dgv.MultiSelect = false;

            if (dgv.Name.Equals("dgvTech"))
                dgv.DataSource = mixTechLists;
            else
                dgv.DataSource = mixTechDownLists;

            DataGridViewComboBoxColumn dgvActionControlCode = dgv.GetComboBoxColumn("ActionControlCode");
            dgvActionControlCode.DataSource = new SYS_ActionControlRepository().GetList("DeviceID = '" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID = ''");

            //切换语言 20230509 李辉
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
            dgv.Columns["StepPower"].DefaultCellStyle.Format = "0";
            dgv.Columns["PlcStepPower"].DefaultCellStyle.Format = "0";

            dgv.Columns["StepEnergy"].DefaultCellStyle.Format = "0.0";
            dgv.Columns["PlcStepEnergy"].DefaultCellStyle.Format = "0.0";

            dgv.Columns["StepPress"].DefaultCellStyle.Format = "0.00";
            dgv.Columns["PlcStepPress"].DefaultCellStyle.Format = "0.00";
            dgv.Columns["StepSpeed"].DefaultCellStyle.Format = "0.0";
            dgv.Columns["PlcStepSpeed"].DefaultCellStyle.Format = "0";

            dgv.Columns[3].DefaultCellStyle.ForeColor = Color.Red;
            dgv.Columns[6].DefaultCellStyle.ForeColor = Color.Red;
            dgv.Columns[8].DefaultCellStyle.ForeColor = Color.Red;
            dgv.Columns[10].DefaultCellStyle.ForeColor = Color.Red;
            dgv.Columns[12].DefaultCellStyle.ForeColor = Color.Red;
            dgv.Columns[14].DefaultCellStyle.ForeColor = Color.Red;

            #endregion 密炼Grid初始化

            return;
        }

        /// <summary>
        /// 初始化称量表格
        /// </summary>
        /// <param name="dgv"></param>
        public static void InitWeightDataGridView(DataGridViewEx dgv)
        {
            #region 称量Grid初始化

            ColStruct[] cols = new ColStruct[]{
                new ColStruct("DevicePartID","设备部位",ColumnType.cmb,true),
                new ColStruct("WeighMaterialID","称量材料",ColumnType.cmb,true),
                new ColStruct("DropOrder","投料顺序",ColumnType.cmb,true),
                new ColStruct("WeighOrder","称量顺序"),
                new ColStruct("WeighSetVal","标准值"),
                new ColStruct("RealWeight","实际值"),
                new ColStruct("AllowError","误差值")
            };

            dgv.AddCols(cols);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns[1].FillWeight = 200;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.Enabled = false;
            dgv.ScrollBars = ScrollBars.None;
            //dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSeaGreen;
            //dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            try
            {
                DataGridViewComboBoxColumn dgvDropOrder = (DataGridViewComboBoxColumn)dgv.Columns["DropOrder"];
                dgvDropOrder.DataSource = formulaWeighRepository.DropTable();
                dgvDropOrder.DisplayMember = "name";
                dgvDropOrder.ValueMember = "value";

                DataGridViewComboBoxColumn dgvDevicePartID = (DataGridViewComboBoxColumn)dgv.Columns["DevicePartID"];
                dgvDevicePartID.DataSource = NewuGlobal.DevicePartList;
                //添加中英文判断 20230509 李辉 是否需要切换语言就刷新,有待商榷
                if (NewuGlobal.SupportLanguage.Equals("1"))
                    dgvDevicePartID.DisplayMember = "Reserve1";
                else
                    dgvDevicePartID.DisplayMember = "DevicePartName";

                dgvDevicePartID.ValueMember = "DevicePartID";

                DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgv.Columns["WeighMaterialID"];
                dgvWeighMaterialID.DataSource = formulaMaterialRepository.GetList("DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID=''");
                dgvWeighMaterialID.DisplayMember = "MaterialCode";
                dgvWeighMaterialID.ValueMember = "MaterialID";
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewDisplay").Error(ex.ToString());
            }

            #endregion 称量Grid初始化
        }

        /// <summary>
        /// 解决新增物料 被添加到新增配方中 直接发送PLC执行 导致dgv表格弹框报错
        /// </summary>
        /// <param name="dgv"></param>
        private static void IsRefresh(DataGridViewEx dgv)
        {
            DataGridViewComboBoxColumn dgvWeighMaterialID = (DataGridViewComboBoxColumn)dgv.Columns["WeighMaterialID"];
            dgvWeighMaterialID.DataSource = formulaMaterialRepository.GetList("DeviceID='" + NewuGlobal.SoftConfig.DeviceID + "' or DeviceID=''");
            dgvWeighMaterialID.DisplayMember = "MaterialCode";
            dgvWeighMaterialID.ValueMember = "MaterialID";
        }

        /// <summary>
        /// 新的配方发送成功，更新两个表中的数据
        /// </summary>
        /// <param name="dgvTech"></param>
        /// <param name="dgvWeight"></param>
        public static void DisPlayTable(DataGridViewEx dgvTech, DataGridViewEx dgvWeight)
        {
            if (dgvTech == null || dgvWeight == null)
                return;
            NewuGlobal.RunInfo.InitData();
            IsRefresh(dgvWeight);
            DisPlayMixTechCurrent(dgvTech);
            DisPlayWeightCurent(dgvWeight);
        }

        public static void DisPlayTableWeight(DataGridViewEx dgv)
        {
            if (dgv == null)
                return;
            NewuGlobal.RunInfo.InitData();
            IsRefresh(dgv);
            DisPlayWeightCurent(dgv);
        }

        public static void DisPlayTableMix(DataGridViewEx dgv)
        {
            if (dgv == null)
                return;

            NewuGlobal.RunInfo.InitData();
            DisPlayMixTechCurrent(dgv);
        }

        private static void DisPlayMixTechCurrent(DataGridViewEx dgv)
        {
            if (dgv.InvokeRequired)
            {
                dgv.BeginInvoke(new Action(() =>
                {
                    GetMixTech(dgv);
                }));
            }
            else
            {
                GetMixTech(dgv);
            }
        }

        /// <summary>
        /// 密炼工艺步骤数据刷新
        /// </summary>
        /// <param name="dgv"></param>
        public static void GetMixTech(DataGridViewEx dgv)
        {
            if (dgv.Name.Equals("dgvTech"))
            {
                List<FormulaMix> lists = formulaMixRepository.GetMixDetail(NewuGlobal.Now_MaterialID);

                mixTechLists = new List<MixerShow>();

                int i = 0;
                foreach (FormulaMix item in lists)
                {
                    if (item.StepDesc == "开卸料门")
                    {
                        NewuGlobal.OpenDropDoorIndex = i;
                    }
                    i++;
                    mixTechLists.Add(InitChangeMixMDLToShowMDL(item));
                }
                dgv.DataSource = mixTechLists;
            }
            else
            {
                List<FormulaMixF> lists = formulaMixFRepository.GetMixDetail(NewuGlobal.Now_MaterialID);

                mixTechDownLists = new List<MixerShow>();

                for (int i = 0; i < lists.Count; i++)
                {
                    if (lists[i].StepDesc == "开卸料门")
                    {
                        NewuGlobal.OpenDropDoorIndexF = i;
                    }
                    mixTechDownLists.Add(InitMixMDLToShowMDL(lists[i]));
                }
                dgv.DataSource = mixTechDownLists;
            }
        }

        private static MixerShow InitChangeMixMDLToShowMDL(FormulaMix temp)
        {
            MixerShow newItem = new MixerShow
            {
                StepOrder = temp.StepOrder,
                StepDesc = temp.StepDesc,
                StepTime = temp.StepTime,
                StepTemp = temp.StepTemp,
                StepPower = temp.StepPower,
                StepEnergy = temp.StepEnergy,
                StepPress = temp.StepPress,
                StepSpeed = temp.StepSpeed,
                KeepTime = temp.KeepTime,
                ActionControlCode = temp.ActionControlCode
            };

            return newItem;
        }

        private static MixerShow InitMixMDLToShowMDL(FormulaMixF temp)
        {
            MixerShow newItem = new MixerShow
            {
                StepOrder = temp.StepOrder,
                StepDesc = temp.StepDesc,
                StepTime = temp.StepTime,
                StepTemp = temp.StepTemp,
                StepPower = temp.StepPower,
                StepEnergy = temp.StepEnergy,
                StepPress = temp.StepPress,
                StepSpeed = temp.StepSpeed,
                KeepTime = temp.KeepTime,
                ActionControlCode = temp.ActionControlCode
            };

            return newItem;
        }

        private static void DisPlayWeightCurent(DataGridViewEx dgv)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new Action(() =>
                {
                    GetWeight(dgv);
                }));
            }
            else
            {
                GetWeight(dgv);
            }
        }

        public static void GetWeight(DataGridViewEx dgv)
        {
            string whereStr = "MaterialID='" + NewuGlobal.Now_MaterialID + "'";
            rawLists = new FormulaWeighRepository().GetModelListNew(0, whereStr, "DevicePartID,DropOrder,WeighOrder");
            weightList = new List<MixerWeightShow>();

            MixerWeightShow InitChangeWeightMDLToShowMDL(FormulaWeigh temp)
            {
                MixerWeightShow newItem = new MixerWeightShow
                {
                    DevicePartID = temp.DevicePartID,
                    WeighMaterialID = temp.WeighMaterialID,
                    DropOrder = temp.DropOrder,
                    WeighOrder = temp.WeighOrder,
                    DevicePartCode = temp.DevicePartCode,
                    WeighSetVal = decimal.Round(temp.WeighSetVal, ScaleAccuracy.GetDigitByPartCode(temp.DevicePartCode)),
                    AllowError = decimal.Round(temp.AllowError, ScaleAccuracy.GetDigitByPartCode(temp.DevicePartCode))
                };
                return newItem;
            }

            foreach (FormulaWeigh item in rawLists)
            {
                weightList.Add(InitChangeWeightMDLToShowMDL(item));
            }

            dgv.DataSource = weightList;
        }

        private static bool allIsZERO = false;

        /// <summary>
        /// 密炼工艺表格 监控
        /// </summary>
        /// <param name="dgv"></param>
        public static void RefreshMixTechDataGridView(DataGridView dgv)
        {
            if (mixTechLists == null)
                return;

            // 根据内存地址点 更新数据
            int cnt = mixTechLists.Count - 1;
            bool flag = true;
            for (int i = AddressConst.MixerTechStart + mixTechLists.Count - 1; i >= AddressConst.MixerTechStart; i--, cnt--)
            {
                if (ss.Getbool(i))
                {
                    flag = false;
                    dgv.Rows[cnt].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                    nowSelect = cnt;
                    dgv.InvalidateRow(cnt);
                    allIsZERO = true;
                    break;
                }
            }

            if (flag)  //全为零
            {
                dgv.ClearSelection();
                if (allIsZERO == true)
                    ClearTechData(dgv, mixTechLists);
            }

            cnt = mixTechLists.Count - 1;
            for (int i = AddressConst.MixerTechEnd + mixTechLists.Count - 1; i >= AddressConst.MixerTechEnd; i--, cnt--)
            {
                if (ss.Getbool(i))
                {
                    string a = ss.GetInt((int)MixerAnalogMiningMixer.Time, 4).ToString();  //分步时间
                    if (a == "0")
                    {
                        continue;
                    }

                    mixTechLists[cnt].PlcStepTime = a;
                    mixTechLists[cnt].PlcStepTemp = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixer.Temp, 4)) / ScaleAccuracy.digitTemp).ToString();
                    mixTechLists[cnt].PlcSteppower = ss.GetInt((int)MixerAnalogMiningMixer.Power, 4).ToString();
                    mixTechLists[cnt].PlcStepEnergy = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixer.Energy, 4)) / ScaleAccuracy.digitEnergy).ToString();
                    mixTechLists[cnt].PlcStepPress = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixer.Press, 4)) / ScaleAccuracy.digitPress).ToString();
                    mixTechLists[cnt].PlcStepSpeed = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixer.Speed, 4)) / ScaleAccuracy.digitSpeed).ToString();
                    dgv.InvalidateRow(cnt);
                    break;
                }
            }
        }

        public static void ClearTechData(DataGridView dgv, List<MixerShow> techList)
        {
            allIsZERO = false;
            int cnt = 0;
            foreach (MixerShow item in techList)
            {
                item.PlcStepTime = "";
                item.PlcStepTime = "";
                item.PlcStepPress = "";
                item.PlcSteppower = "";
                item.PlcStepSpeed = "";
                item.PlcStepEnergy = "";
                item.PlcStepTemp = "";
                dgv.InvalidateRow(cnt++);
            }
        }

        /// <summary>
        /// 下密炼工艺表格 监控
        /// </summary>
        /// <param name="dgv"></param>
        public static void RefreshMixTechDataGridViewF(DataGridView dgvTechDown)
        {
            if (mixTechDownLists == null)
                return;

            // 根据内存地址点 更新数据
            int cnt = mixTechDownLists.Count - 1;
            bool flag = true;
            for (int i = AddressConst.MixerTechDownStart + mixTechDownLists.Count - 1; i >= AddressConst.MixerTechDownStart; i--, cnt--)
            {
                if (ss.Getbool(i))
                {
                    flag = false;
                    dgvTechDown.Rows[cnt].Selected = true;
                    dgvTechDown.FirstDisplayedScrollingRowIndex = cnt;   // din 定位到当前选中行
                    nowSelect = cnt;
                    dgvTechDown.InvalidateRow(cnt);
                    allIsZERO = true;
                    break;
                }
            }

            if (flag)  //全为零
            {
                dgvTechDown.ClearSelection();
                if (allIsZERO == true)
                    ClearTechData(dgvTechDown, mixTechDownLists);
            }

            cnt = mixTechDownLists.Count - 1;
            for (int i = AddressConst.MixerTechDownEnd + mixTechDownLists.Count - 1; i >= AddressConst.MixerTechDownEnd; i--, cnt--)
            {
                if (ss.Getbool(i))
                {
                    string a = ss.GetInt((int)MixerAnalogMiningMixerDown.Time, 4).ToString();  //分步时间
                    if (a == "0")
                    {
                        continue;
                    }

                    mixTechDownLists[cnt].PlcStepTime = a;
                    mixTechDownLists[cnt].PlcStepTemp = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixerDown.Temp, 4)) / ScaleAccuracy.digitTemp).ToString();
                    mixTechDownLists[cnt].PlcSteppower = ss.GetInt((int)MixerAnalogMiningMixerDown.Power, 4).ToString();
                    mixTechDownLists[cnt].PlcStepEnergy = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixerDown.Energy, 4)) / ScaleAccuracy.digitEnergy).ToString();
                    mixTechDownLists[cnt].PlcStepPress = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixerDown.Press, 4)) / ScaleAccuracy.digitPress).ToString();
                    mixTechDownLists[cnt].PlcStepSpeed = ((1.0 * ss.GetInt((int)MixerAnalogMiningMixerDown.Speed, 4)) / ScaleAccuracy.digitSpeed).ToString();
                    dgvTechDown.InvalidateRow(cnt);
                    break;
                }
            }
        }

        /// <summary>
        /// 称量物品 监控
        /// </summary>
        /// <param name="dgv"></param>
        public static void RefreshWeightDataGridView(DataGridView dgv)
        {
            if (dgv.RowCount > 0)
                dgv.Rows[0].Selected = false;

            if (weightList == null)
                return;

            for (int i = (int)MixerNetWeight.Carbon; i <= (int)MixerNetWeight.SilaneSerialNum; i += 8)
            {
                if (MyDictionary.ContainsKey(i))
                {
                    //更新表中该数据  拿到DevicePartCodeID 去更新表中数据
                    UpDataWeightShowMDL(dgv, i, myDictionary[i], weightList);
                }
            }
        }

        private static void UpDataWeightShowMDL(DataGridView dgv, int weightPint, string devicePartID, List<MixerWeightShow> list)
        {
            try
            {
                int xx = ss.GetHex(weightPint + 4, 4);
                int left = xx / 10 % 10;
                int right = xx % 10;
                int cnt = 0;
                foreach (MixerWeightShow item in list)
                {
                    if (item.DevicePartID == devicePartID && left == item.DropOrder && right == item.WeighOrder)
                    {
                        item.RealWeight = decimal.Parse((1.0 * ss.GetHex(weightPint, 4) / Math.Pow(10, ScaleAccuracy.GetDigitByPartCode(item.DevicePartCode))).ToString());
                        item.RealWeight = decimal.Round(item.RealWeight, ScaleAccuracy.GetDigitByPartCode(item.DevicePartCode));
                        dgv.Rows[cnt].DefaultCellStyle.ForeColor = Color.White;
                        dgv.Rows[cnt].DefaultCellStyle.BackColor = Color.FromArgb(28, 59, 118);
                        dgv.InvalidateRow(cnt);
                    }
                    else
                    {
                        if (item.DevicePartID == devicePartID && dgv.Rows[cnt].DefaultCellStyle.ForeColor == Color.White)
                        {
                            if (cnt % 2 == 0)
                            {
                                dgv.Rows[cnt].DefaultCellStyle.BackColor = dgv.AlternatingRowsDefaultCellStyle.BackColor;
                            }
                            else
                            {
                                dgv.Rows[cnt].DefaultCellStyle.BackColor = dgv.RowsDefaultCellStyle.BackColor;
                            }
                            dgv.Rows[cnt].DefaultCellStyle.ForeColor = Color.Black;
                            dgv.InvalidateRow(cnt);
                        }
                    }
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewDisplay").Error(ex.ToString());
            }
        }

        public static void RefreshScaleStatus(NewuPicAngle pic, int address)
        {
            try
            {
                if (NewuGlobal.MemDB.Getbool(address))
                    pic.NewuPicTypeStyle = NewuPicType.Background;
                else
                    pic.NewuPicTypeStyle = NewuPicType.Foreground;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ViewDisplay").Error(ex.ToString());
            }
        }
    }
}