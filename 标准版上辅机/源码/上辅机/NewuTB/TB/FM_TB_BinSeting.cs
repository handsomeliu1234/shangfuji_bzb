using MultiLanguage;
using Newu;
using NewuCommon;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_BinSeting : Form, ILanguageChanged
    {
        private readonly TB_BinSettingRepository binSettingRepository = new TB_BinSettingRepository();
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly TB_OperateLogRepository operateLogRepository = new TB_OperateLogRepository();
        private List<FormulaMaterial> materialList;
        private List<SYS_TypeCode> sYS_TypeCodes;
        private List<SYS_Device> sYS_Devices;
        private List<TB_BinSeting> binSetings;
        private int rowIndex;
        private string deviceID;

        public FM_TB_BinSeting()
        {
            InitializeComponent();
        }

        private void FM_TB_BinSeting_Load(object sender, EventArgs e)
        {
            InitView();
            SetControlLanguageText();
            GetData();
        }

        private void InitView()
        {
            try
            {
                if (NewuGlobal.SoftConfig.UseLowerLimit)
                {
                    txtPreSetGcsD.Enabled = true;
                }
                else
                {
                    txtPreSetGcsD.Enabled = false;
                    txtPreSetGcsD.BackColor = SystemColors.ButtonHighlight;
                }

                sYS_TypeCodes = NewuGlobal.TypeCodeList.Where(t => t.TypeCodeName == "Carbon" || t.TypeCodeName == "Oil" || t.TypeCodeName == "Zno" || t.TypeCodeName == "Silane").ToList();

                sYS_Devices = NewuGlobal.DeviceList;
                cmb_DeviceID.DataSource = sYS_Devices;
                cmb_DeviceID.ValueMember = "DeviceID";
                cmb_DeviceID.DisplayMember = "DeviceName";

                cmbDeviceID.DataSource = NewuGlobal.DeviceList;
                cmbDeviceID.ValueMember = "DeviceID";
                cmbDeviceID.DisplayMember = "DeviceName";

                ColStruct[] cols = new ColStruct[]
                {
                    new ColStruct("",""),
                    new ColStruct("DeviceID","设备ID", ColumnType.cmb,true),
                    new ColStruct("BinID","储罐ID",ColumnType.txt,false),
                    new ColStruct("BinNo","储罐编号"),
                    new ColStruct("TypeCodeID","材料类型",ColumnType.cmb,true),
                    new ColStruct("MaterialID","材料", ColumnType.cmb,true),
                    new ColStruct("Reserve1","物料条码"),
                    new ColStruct("PreSetKuai","预设快称"),
                    new ColStruct("PreSetTiQian","预设提前"),
                    new ColStruct("PreSetWuUp","预设公差上限"),
                    new ColStruct("PreSetWuDown","预设公差下限"),
                    new ColStruct("SaveUserID","保存用户", ColumnType.cmb,true)
                };

                dgv.AllowUserToAddRows = false;
                dgv.AddCols(cols);
                dgv.ReadOnly = true;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.Columns[0].Width = 50;
                dgv.Columns[3].Width = 100;

                DataGridViewComboBoxColumn dgvColDepartment = dgv.Columns["DeviceID"] as DataGridViewComboBoxColumn;
                dgvColDepartment.DisplayMember = "DeviceName";
                dgvColDepartment.ValueMember = "DeviceID";
                dgvColDepartment.DataSource = sYS_Devices;

                materialList = formulaMaterialRepository.GetList("");
                DataGridViewComboBoxColumn dgvColMaterialID = dgv.Columns["MaterialID"] as DataGridViewComboBoxColumn;
                dgvColMaterialID.ValueMember = "MaterialID";
                dgvColMaterialID.DisplayMember = "MaterialCode";
                dgvColMaterialID.DataSource = materialList;

                List<SaveUser> saveUserList = new List<SaveUser>();
                foreach (var userInfo in NewuGlobal.UserInfoList)
                {
                    SaveUser saveUser = new SaveUser(userInfo.UserID, userInfo.RealName);
                    saveUserList.Add(saveUser);
                }

                DataGridViewComboBoxColumn dgvColSaveUserID = dgv.Columns["SaveUserID"] as DataGridViewComboBoxColumn;
                dgvColSaveUserID.DisplayMember = "RealName";
                dgvColSaveUserID.ValueMember = "SaveUserID";
                dgvColSaveUserID.DataSource = saveUserList;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_BinSeting").Error(ex.ToString());
            }
        }

        public void GetData()
        {
            try
            {
                RefreshDGVdata();
                string where = "1=1";
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    string cmb_Equi = cmb_DeviceID.SelectedValue.ToString();
                    where = " DeviceID = '" + cmb_Equi + "'";
                }

                if (cmbTypeCode.SelectedIndex >= 0)
                {
                    where += "and TypeCodeID='" + cmbTypeCode.SelectedValue.ToString() + "' ";
                }

                binSetings = binSettingRepository.GetList(0, where, "DeviceID,TypeCodeID,BinNo");
                if (binSetings != null)
                {
                    dgv.DataSource = binSetings;

                    //删除最后一行时触发
                    if (rowIndex >= dgv.Rows.Count)
                        rowIndex = dgv.Rows.Count - 1;

                    dgv.Rows[0].Selected = false;
                    dgv.Rows[rowIndex].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = rowIndex;

                    txt_BinNo.Text = dgv.Rows[rowIndex].Cells["BinNo"].Value.ToString();
                    cmbDeviceID.SelectedValue = dgv.Rows[rowIndex].Cells["DeviceID"].Value.ToString();
                    cmb_TypeCodeID.SelectedValue = dgv.Rows[rowIndex].Cells["TypeCodeID"].Value.ToString();
                    cmb_MaterialID.SelectedValue = dgv.Rows[rowIndex].Cells["MaterialID"].Value.ToString();
                    cmb_BarCode.Text = dgv.Rows[rowIndex].Cells["Reserve1"].Value.ToString();
                    txtPreSetKuai.Text = dgv.Rows[rowIndex].Cells["PreSetKuai"].Value.ToString();
                    txtPreSetTiqian.Text = dgv.Rows[rowIndex].Cells["PreSetTiQian"].Value.ToString();
                    txtPreSetGcsU.Text = dgv.Rows[rowIndex].Cells["PreSetWuUp"].Value.ToString();
                    txtPreSetGcsD.Text = dgv.Rows[rowIndex].Cells["PreSetWuDown"].Value.ToString();
                }
                else
                    label5.Text = NewuGlobal.GetRes("000387") + NewuGlobal.GetRes("000253");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_BinSeting").Error(ex.ToString());
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            label5.Text = NewuGlobal.GetRes("000170");
            GetData();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                {
                    return;
                }

                if (rowIndex >= 0)
                {
                    string id = dgv[2, rowIndex].Value.ToString();
                    string binNo = dgv[3, rowIndex].Value.ToString();
                    /*                    string id = dgv[1, rowIndex].Value.ToString();
                                        string binNo = dgv[2, rowIndex].Value.ToString();*/
                    DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175") + " [ " + binNo + " ] ?", NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (isDel == DialogResult.Yes)
                    {
                        bool isAccess = binSettingRepository.Delete(id);
                        if (isAccess)
                        {
                            TB_BinSeting binSeting = dgv.CurrentRow.DataBoundItem as TB_BinSeting;
                            string strBinSeting = "删除了储罐：" + binSeting.BinNo + "，物料：" + materialList.Find(f => f.MaterialID.Equals(binSeting.MaterialID)).MaterialCode;
                            TB_OperateLog operateLog = new TB_OperateLog
                            {
                                DeviceID = deviceID,
                                LogInfo = strBinSeting,
                                LogType = AppLogType.Delete.ToString(),
                                UserID = NewuGlobal.TB_UserInfo.UserCode,
                                SaveTime = DateTime.Now
                            };
                            operateLogRepository.Add(operateLog);
                            label5.Text = NewuGlobal.GetRes("000173");
                            GetData();
                        }
                        else
                        {
                            label5.Text = NewuGlobal.GetRes("000174");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_BinSeting").Error(ex.ToString());
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DataVerification())
                    return;

                int binNo = int.Parse(txt_BinNo.Text);
                string deviceID = cmb_DeviceID.SelectedValue.ToString();
                string typeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                string materialID = cmb_MaterialID.SelectedValue.ToString();
                TB_BinSeting binSeting = new TB_BinSeting
                {
                    BinNo = binNo,
                    PreSetKuai = decimal.Parse(txtPreSetKuai.Text),
                    PreSetTiQian = decimal.Parse(txtPreSetTiqian.Text),
                    PreSetWuUp = decimal.Parse(txtPreSetGcsU.Text),
                    PreSetWuDown = decimal.Parse(txtPreSetGcsD.Text),
                    DeviceID = deviceID,
                    TypeCodeID = typeCodeID,
                    MaterialID = materialID,
                    Reserve1 = cmb_BarCode.SelectedItem == null ? "" : cmb_BarCode.SelectedItem.ToString(),
                    SaveTime = DateTime.Now,
                    SaveUserID = NewuGlobal.TB_UserInfo.UserID
                };
                TB_BinSeting tB_BinSeting = binSettingRepository.GetModel(binNo, materialID, deviceID, typeCodeID);
                if (tB_BinSeting != null)
                    label5.Text = NewuGlobal.GetRes("000322");
                else
                {
                    bool result = binSettingRepository.Add(binSeting, out string message);
                    label5.Text = message;
                    if (result)
                    {
                        GetData();
                        StringBuilder strBuilder = new StringBuilder();
                        string materialCode = materialList.Find(m => m.MaterialID.Equals(materialID)).MaterialCode;
                        string typeCode;
                        string deviceCode = sYS_Devices.Find(s => s.DeviceID.Equals(deviceID)).DeviceCode;
                        if (NewuGlobal.SupportLanguage.Equals("1"))
                            typeCode = sYS_TypeCodes.Find(s => s.TypeCodeID.Equals(typeCodeID)).TypeCodeDesc;
                        else
                            typeCode = sYS_TypeCodes.Find(s => s.TypeCodeID.Equals(typeCodeID)).TypeCodeName;

                        strBuilder.AppendFormat("{0}{1}：{2}，{3}：{4}，{5}：{6}，{7}：{8}", NewuGlobal.GetRes("000100"), NewuGlobal.GetRes("000386"), binNo, NewuGlobal.GetRes("000388"), materialCode, NewuGlobal.GetRes("000387"), typeCode, NewuGlobal.GetRes("000382"), deviceCode);

                        TB_OperateLog operateLog = new TB_OperateLog
                        {
                            DeviceID = deviceID,
                            LogInfo = strBuilder.ToString(),
                            LogType = AppLogType.Add.ToString(),
                            UserID = NewuGlobal.TB_UserInfo.UserCode,
                            SaveTime = DateTime.Now
                        };
                        operateLogRepository.Add(operateLog);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_BinSeting").Error(ex.ToString());
            }
        }

        private bool DataVerification()
        {
            //"储罐编号不能为空且为正整数"
            if (FunClass.IsEmptyOrNumber(txt_BinNo.Text.Trim()) != 2)
            {
                MessageBox.Show(NewuGlobal.GetRes("000404"));
                return false;
            }

            if (cmb_DeviceID.SelectedIndex < 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000182") + NewuGlobal.GetRes("000162"));
                return false;
            }

            //"预设快称不是数字"
            if (!FunClass.VValDouble(txtPreSetKuai.Text.Trim()))
            {
                MessageBox.Show(NewuGlobal.GetRes("000405"));
                return false;
            }

            //"预设提前不是数字"
            if (!FunClass.VValDouble(txtPreSetTiqian.Text.Trim()))
            {
                MessageBox.Show(NewuGlobal.GetRes("000406"));
                return false;
            }

            //"预设公差不是数字"
            if (!FunClass.VValDouble(txtPreSetGcsU.Text.Trim()) || !FunClass.VValDouble(txtPreSetGcsU.Text.Trim()))
            {
                MessageBox.Show(NewuGlobal.GetRes("000407"));
                return false;
            }

            //"材料类型不能为空！"
            if (cmb_TypeCodeID.SelectedIndex < 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000401") + " " + NewuGlobal.GetRes("000162"));
                return false;
            }

            //"材料不能为空！"
            if (cmb_MaterialID.SelectedIndex < 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000408"));
                return false;
            }

            return true;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }

            if (!DataVerification())
                return;

            RefreshDGVdata();
            DialogResult dialogResult = MessageBox.Show(NewuGlobal.GetRes("000177"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                if (rowIndex >= 0)
                {
                    //dgv选择的储斗对象
                    int binNo = int.Parse(dgv.Rows[rowIndex].Cells["BinNo"].Value.ToString());
                    string deviceID = dgv.Rows[rowIndex].Cells["DeviceID"].Value.ToString();
                    string typeCodeID = dgv.Rows[rowIndex].Cells["TypeCodeID"].Value.ToString();//选择行的物料类型
                    string materialID = dgv.Rows[rowIndex].Cells["MaterialID"].Value.ToString();

                    int getBinNo = int.Parse(txt_BinNo.Text);
                    string getDeviceID = cmbDeviceID.SelectedValue.ToString();
                    string getTypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                    string getMaterialID = cmb_MaterialID.SelectedValue.ToString();

                    TB_BinSeting binsetting = binSettingRepository.GetModel(binNo, deviceID, typeCodeID);

                    //编辑区域选择的物料在储斗中是否已存在
                    TB_BinSeting existBin = binSetings.Find(b => b.MaterialID.Equals(getMaterialID));

                    //编辑区域和选择的储斗是同一个对象
                    if (binNo == getBinNo && deviceID.Equals(getDeviceID) && typeCodeID.Equals(getTypeCodeID))
                    {
                        if (!materialID.Equals(getMaterialID))
                        {
                            if (existBin != null)
                            {
                                label5.Text = NewuGlobal.GetRes("000322");
                                return;
                            }
                            else
                                UpdateBinsetting(binsetting);
                        }
                        else
                            UpdateBinsetting(binsetting);
                    }
                    else
                    {
                        //编辑区域获取到储斗参数--->判断编辑的储斗是否已存在
                        TB_BinSeting tB_BinSeting = binSettingRepository.GetModel(getBinNo, getDeviceID, getTypeCodeID);
                        if (tB_BinSeting != null)
                        {
                            label5.Text = NewuGlobal.GetRes("000322");
                            return;
                        }
                        else
                        {
                            if (existBin != null)
                            {
                                label5.Text = NewuGlobal.GetRes("000322");
                                return;
                            }
                            else
                                UpdateBinsetting(binsetting);
                        }
                    }
                }
                else
                    label5.Text = NewuGlobal.GetRes("000066");//"请先选中一行"
            }
        }

        private void UpdateBinsetting(TB_BinSeting binsetting)
        {
            try
            {
                TB_BinSeting oldBinSeting = new TB_BinSeting
                {
                    BinID = binsetting.BinID,
                    BinNo = binsetting.BinNo,
                    DeviceID = binsetting.DeviceID,
                    MaterialID = binsetting.MaterialID,
                    TypeCodeID = binsetting.TypeCodeID,
                    PreSetKuai = binsetting.PreSetKuai,
                    PreSetZhong = binsetting.PreSetZhong,
                    PreSetTiQian = binsetting.PreSetTiQian,
                    PreSetWuUp = binsetting.PreSetWuUp,
                    PreSetWuDown = binsetting.PreSetWuDown,
                    FrequenceUp = binsetting.FrequenceUp,
                    FrequenceMid = binsetting.FrequenceMid,
                    FrequenceDown = binsetting.FrequenceDown,
                    SaveUserID = binsetting.SaveUserID,
                    SaveTime = binsetting.SaveTime,
                    Reserve1 = binsetting.Reserve1
                };

                Convert.ToDecimal(txtPreSetKuai.Text).ToString("F3");
                binsetting.BinNo = int.Parse(txt_BinNo.Text);
                binsetting.DeviceID = cmbDeviceID.SelectedValue.ToString();
                binsetting.MaterialID = cmb_MaterialID.SelectedValue.ToString();
                binsetting.TypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                binsetting.Reserve1 = cmb_BarCode.SelectedItem == null ? "" : cmb_BarCode.SelectedItem.ToString();
                binsetting.PreSetKuai = Convert.ToDecimal(decimal.Parse(txtPreSetKuai.Text).ToString("F3"));
                binsetting.PreSetTiQian = Convert.ToDecimal(decimal.Parse(txtPreSetTiqian.Text).ToString("F3"));
                binsetting.PreSetWuUp = Convert.ToDecimal(decimal.Parse(txtPreSetGcsU.Text).ToString("F3"));
                binsetting.PreSetWuDown = Convert.ToDecimal(decimal.Parse(txtPreSetGcsD.Text).ToString("F3"));
                binsetting.SaveTime = DateTime.Now;
                bool result = binSettingRepository.Update(binsetting);

                if (result)
                {
                    StringBuilder strBuilder = new StringBuilder();
                    PropertyInfo[] oldPropertyInfo = oldBinSeting.GetType().GetProperties();
                    PropertyInfo[] propertyInfo = binsetting.GetType().GetProperties();
                    string oldMaterialCode = materialList.Find(f => f.MaterialID.Equals(oldBinSeting.MaterialID)).MaterialCode;
                    string materialCode = materialList.Find(f => f.MaterialID.Equals(binsetting.MaterialID)).MaterialCode;

                    strBuilder.AppendFormat("储罐编号：[{0}]，物料：[{1}] 修改内容：", oldBinSeting.BinNo, oldMaterialCode);
                    //修改前的model对应数据
                    foreach (PropertyInfo p in oldPropertyInfo)
                    {
                        foreach (PropertyInfo pi in propertyInfo)
                        {
                            //属性名相同
                            if (p.Name.Equals(pi.Name) && p.Name != "SaveTime")
                            {
                                if (p.GetValue(oldBinSeting) != null && pi.GetValue(binsetting) != null)
                                {
                                    if (!p.GetValue(oldBinSeting).ToString().Equals(pi.GetValue(binsetting).ToString()))
                                    {
                                        string title = GetTitle(p.Name);
                                        if (pi.Name.Equals("MaterialID"))
                                            strBuilder.AppendFormat("[{0}] 由 [{1}] 修改为 [{2}]； ", title, oldMaterialCode, materialCode);
                                        else
                                            strBuilder.AppendFormat("[{0}] 由 [{1}] 修改为 [{2}]； ", title, p.GetValue(oldBinSeting), pi.GetValue(binsetting));
                                    }
                                }
                            }
                        }
                    }
                    TB_OperateLog operateLog = new TB_OperateLog
                    {
                        DeviceID = deviceID,
                        LogInfo = strBuilder.ToString(),
                        LogType = AppLogType.Update.ToString(),
                        UserID = NewuGlobal.TB_UserInfo.UserCode,
                        SaveTime = DateTime.Now
                    };
                    operateLogRepository.Add(operateLog);
                    GetData();
                    label5.Text = NewuGlobal.GetRes("000171");
                }
                else
                    label5.Text = NewuGlobal.GetRes("000172");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_BinSeting").Error(ex.ToString());
            }
        }

        private string GetTitle(string name)
        {
            string title = "";
            switch (name)
            {
                case "BinNo":
                    title = NewuGlobal.GetRes("000386");
                    break;

                case "DeviceID":
                    title = NewuGlobal.GetRes("000384");
                    break;

                case "MaterialID":
                    title = NewuGlobal.GetRes("000388");
                    break;

                case "PreSetKuai":
                    title = NewuGlobal.GetRes("000390");
                    break;

                case "PreSetTiQian":
                    title = NewuGlobal.GetRes("000391");
                    break;

                case "PreSetWuUp":
                    title = NewuGlobal.GetRes("000392");
                    break;

                case "PreSetWuDown":
                    title = NewuGlobal.GetRes("000403");
                    break;

                case "Reserve1":
                    title = NewuGlobal.GetRes("000389");
                    break;

                default:
                    break;
            }
            return title;
        }

        private void RefreshDGVdata()
        {
            List<FormulaMaterial> materList = formulaMaterialRepository.GetList("");
            DataGridViewComboBoxColumn dgvColMaterialID = dgv.Columns["MaterialID"] as DataGridViewComboBoxColumn;
            dgvColMaterialID.ValueMember = "MaterialID";
            dgvColMaterialID.DisplayMember = "MaterialCode";
            dgvColMaterialID.DataSource = materList;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceID.SelectedIndex = 0;
            cmbTypeCode.SelectedIndex = -1;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            this.Text = NewuGlobal.GetRes("000379");       //  储斗参数管理
            groupBox1.Text = NewuGlobal.GetRes("000380");  //  查询条件
            groupBox2.Text = NewuGlobal.GetRes("000381");  //  信息列表
            lab_Equipment.Text = NewuGlobal.GetRes("000382") + ":";// 所属设备
            label7.Text = NewuGlobal.GetRes("000387") + ":";
            label5.Text = NewuGlobal.GetRes("000170");

            lb_BinNo.Text = NewuGlobal.GetRes("000396") + ":";
            lb_Device.Text = NewuGlobal.GetRes("000382") + ":";
            lb_EquipmentType.Text = NewuGlobal.GetRes("000401") + ":";
            lb_MaterialName.Text = NewuGlobal.GetRes("000402") + ":";
            lb_Barcode.Text = NewuGlobal.GetRes("000389") + ":";
            lb_PreQuick.Text = NewuGlobal.GetRes("000390") + ":";
            lb_PreTiQian.Text = NewuGlobal.GetRes("000391") + ":";
            lb_PreAllowUp.Text = NewuGlobal.GetRes("000392") + ":";
            lb_PreAllowDown.Text = NewuGlobal.GetRes("000403") + ":";

            /***********  常见按钮   ***********/
            btnAdd.Text = NewuGlobal.GetRes("000100"); //新增
            btnEdit.Text = NewuGlobal.GetRes("000101"); //编辑
            btnDel.Text = NewuGlobal.GetRes("000102"); //删除
            btnClose.Text = NewuGlobal.GetRes("000103"); //关闭

            btnReset.Text = NewuGlobal.GetRes("000105");//重置
            btnQuery.Text = NewuGlobal.GetRes("000104");//查询
            /***********  常见文字   ***********/

            int start = 383;
            if (dgv != null && dgv.Columns != null)
                for (int i = 1; i < dgv.Columns.Count - 2; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.GetRes((start + i).ToString("000000"));
                }
            dgv.Columns[dgv.Columns.Count - 2].HeaderText = NewuGlobal.GetRes("000403");
            dgv.Columns[dgv.Columns.Count - 1].HeaderText = NewuGlobal.GetRes("000393");

            cmb_TypeCodeID.DataSource = sYS_TypeCodes;
            cmb_TypeCodeID.ValueMember = "TypeCodeID";

            cmbTypeCode.DataSource = sYS_TypeCodes;
            cmbTypeCode.ValueMember = "TypeCodeID";

            DataGridViewComboBoxColumn dgvTypeCode = dgv.Columns["TypeCodeID"] as DataGridViewComboBoxColumn;
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                cmb_TypeCodeID.DisplayMember = "TypeCodeDesc";
                cmbTypeCode.DisplayMember = "TypeCodeDesc";
                dgvTypeCode.DisplayMember = "TypeCodeDesc";
                btnDel.Padding = btnClose.Padding = btnQuery.Padding = btnReset.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                cmb_TypeCodeID.DisplayMember = "TypeCodeName";
                cmbTypeCode.DisplayMember = "TypeCodeName";
                dgvTypeCode.DisplayMember = "TypeCodeName";
                btnDel.Padding = btnClose.Padding = btnQuery.Padding = btnReset.Padding = new Padding(0, 0, 0, 0);
            }

            dgvTypeCode.ValueMember = "TypeCodeID";
            dgvTypeCode.DataSource = sYS_TypeCodes;
            cmbTypeCode.SelectedIndex = -1;
        }

        private void Cmb_TypeCodeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaterial();
        }

        private void CmbDeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaterial();
        }

        private void GetMaterial()
        {
            if (cmb_TypeCodeID.SelectedIndex >= 0 && cmb_DeviceID.SelectedIndex >= 0)
            {
                deviceID = cmb_DeviceID.SelectedValue.ToString();
                string typeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                cmb_MaterialID.DataSource = formulaMaterialRepository.GetListByDeviceIDandTypeCodeID(0, deviceID, typeCodeID, "MaterialCode");
                cmb_MaterialID.ValueMember = "MaterialID";
                cmb_MaterialID.DisplayMember = "MaterialCode";
            }
        }

        private void Cmb_MaterialID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_TypeCodeID.SelectedIndex >= 0 && cmb_DeviceID.SelectedIndex >= 0)
                {
                    deviceID = cmb_DeviceID.SelectedValue.ToString();
                    FormulaMaterial formulaMaterialModel = formulaMaterialRepository.GetModel(cmb_MaterialID.SelectedValue.ToString());
                    cmb_BarCode.Items.Clear();
                    if (formulaMaterialModel != null && !string.IsNullOrEmpty(formulaMaterialModel.BarCode))
                    {
                        string[] barCodeValue = formulaMaterialModel.BarCode.Split(',');
                        cmb_BarCode.Items.AddRange(barCodeValue);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_BinSeting").Error(ex.ToString());
            }
        }

        /// <summary>
        /// 不启用储斗参数下限上限设定的值就是下限设定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPreSetGcsU_TextChanged(object sender, EventArgs e)
        {
            if (!NewuGlobal.SoftConfig.UseLowerLimit)
                txtPreSetGcsD.Text = txtPreSetGcsU.Text;
        }

        private void TxtPreSetGcsU_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.Utils.TxtPreSetGcsU(e, true);
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            label5.Text = NewuGlobal.GetRes("000170");
            txt_BinNo.Text = dgv.CurrentRow.Cells["BinNo"].Value.ToString();
            cmbDeviceID.SelectedValue = dgv.CurrentRow.Cells["DeviceID"].Value.ToString();
            cmb_TypeCodeID.SelectedValue = dgv.CurrentRow.Cells["TypeCodeID"].Value.ToString();
            cmb_MaterialID.SelectedValue = materialList.Find(a => a.MaterialID.Equals(dgv.CurrentRow.Cells["MaterialID"].Value.ToString())).MaterialID;
            cmb_BarCode.Text = dgv.CurrentRow.Cells["Reserve1"].Value.ToString();
            txtPreSetKuai.Text = dgv.CurrentRow.Cells["PreSetKuai"].Value.ToString();
            txtPreSetTiqian.Text = dgv.CurrentRow.Cells["PreSetTiQian"].Value.ToString();
            txtPreSetGcsU.Text = dgv.CurrentRow.Cells["PreSetWuUp"].Value.ToString();
            txtPreSetGcsD.Text = dgv.CurrentRow.Cells["PreSetWuDown"].Value.ToString();
        }
    }
}