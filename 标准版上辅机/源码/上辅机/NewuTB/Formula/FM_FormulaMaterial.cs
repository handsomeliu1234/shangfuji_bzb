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
using System.Text;
using System.Windows.Forms;

namespace NewuTB.Formula
{
    public partial class FM_FormulaMaterial : Form, ILanguageChanged
    {
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly TB_OperateLogRepository operateLogRepository = new TB_OperateLogRepository();
        private List<SYS_TypeCode> listN = null;
        private List<FormulaMaterial> formulaMaterials;
        private int index;

        private string materialCode;

        public FM_FormulaMaterial()

        {
            InitializeComponent();
        }

        private void FM_FormulaMaterial_Load(object sender, EventArgs e)
        {
            listN = NewuGlobal.TypeCodeList.Where(t => t.TypeCodeName != typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T母炼配方) && t.TypeCodeName != typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T终炼配方) && (t.Enable == 1)).ToList();
            cmb_TypeCodeID.DataSource = listN;
            cmb_TypeCodeID.ValueMember = "TypeCodeID";

            List<SYS_TypeCode> typeCodeList = new List<SYS_TypeCode>();
            typeCodeList.AddRange(listN);
            cmbTypeCode.DataSource = typeCodeList;
            cmbTypeCode.ValueMember = "TypeCodeID";

            cmb_Enabled.DataSource = EnableList.GetList();
            cmb_Enabled.DisplayMember = "names";
            cmb_Enabled.ValueMember = "values";
            cmb_Enabled.DropDownStyle = ComboBoxStyle.DropDownList;

            List<SYS_Device> sYS_Devices = deviceRepository.GetModelListAddNullRows("");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";
            cmb_DeviceID.SelectedIndex = 0;

            List<SYS_Device> devices = new List<SYS_Device>();
            devices.AddRange(sYS_Devices);
            cmbDeviceID.DataSource = devices;
            cmbDeviceID.ValueMember = "DeviceID";
            cmbDeviceID.DisplayMember = "DeviceName";
            cmbDeviceID.SelectedIndex = 0;

            ColStruct[] cols = new ColStruct[]
            {
                new ColStruct("MaterialID","物料ID", ColumnType.txt,false),
                new ColStruct("MaterialCode","物料编号"),
                new ColStruct("DeviceID","所属设备", ColumnType.cmb,true),
                new ColStruct("TypeCodeID","物料类型", ColumnType.cmb,true),
                new ColStruct("Reserve5","是否使用供胶机", ColumnType.chk,false),
                new ColStruct("BarCode","条码"),
                new ColStruct("SaveRealName","保存用户"),
                new ColStruct("SaveTime","保存时间"),
                //Enable列予以展示
                new ColStruct("Enable","是否启用", ColumnType.chk,true)
            };

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns[0].Width = 50;
            DataGridViewComboBoxColumn dgvCmbDevice = (DataGridViewComboBoxColumn)dgv.Columns["DeviceID"];
            dgvCmbDevice.DataSource = deviceRepository.GetModelListAddNullRows("");
            dgvCmbDevice.ValueMember = "DeviceID";
            dgvCmbDevice.DisplayMember = "DeviceName";

            SetControlLanguageText();
            GetData();
        }

        private void SetControlLanguageText()
        {
            DataGridViewComboBoxColumn dgvCmbTypeCode = (DataGridViewComboBoxColumn)dgv.Columns["TypeCodeID"];
            dgvCmbTypeCode.DataSource = typeCodeRepository.GetList("");
            dgvCmbTypeCode.ValueMember = "TypeCodeID";

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                cmbTypeCode.DisplayMember = "TypeCodeDesc";
                cmb_TypeCodeID.DisplayMember = "TypeCodeDesc";
                dgvCmbTypeCode.DisplayMember = "TypeCodeDesc";
                cmbDeviceID.Location = new Point(87, 19);
                cmb_DeviceID.Location = new Point(87, 30);

                label8.Location = new Point(238, 22);
                label2.Location = new Point(238, 34);
                cmb_TypeCodeID.Location = new Point(321, 19);
                cmbTypeCode.Location = new Point(321, 30);

                label10.Location = new Point(503, 22);
                txt_MaterialCode.Location = new Point(586, 16);
                label15.Location = new Point(779, 22);
                cmb_Enabled.Location = new Point(862, 19);
                label9.Location = new Point(1055, 22);
                txt_BarCode.Location = new Point(1136, 19);
                btnAdd.Location = new Point(1329, 15);
                btnEdit.Location = new Point(1434, 15);
                btnDel.Location = new Point(1539, 15);
                btnQuery.Location = new Point(466, 23);
                btnReset.Location = new Point(560, 23);
                btnClose.Location = new Point(654, 23);
                label5.Location = new Point(749, 30);
            }
            else
            {
                cmbTypeCode.DisplayMember = "TypeCodeName";
                cmb_TypeCodeID.DisplayMember = "TypeCodeName";
                dgvCmbTypeCode.DisplayMember = "TypeCodeName";
                cmbDeviceID.Location = new Point(87, 19);
                cmb_DeviceID.Location = new Point(87, 30);

                label8.Location = new Point(237, 22);
                label2.Location = new Point(237, 34);
                cmb_TypeCodeID.Location = new Point(355, 19);
                cmbTypeCode.Location = new Point(355, 30);

                label10.Location = new Point(537, 22);
                txt_MaterialCode.Location = new Point(655, 16);
                label15.Location = new Point(847, 22);
                cmb_Enabled.Location = new Point(916, 19);
                label9.Location = new Point(1107, 22);
                txt_BarCode.Location = new Point(1246, 19);
                btnAdd.Location = new Point(1439, 15);
                btnEdit.Location = new Point(1544, 15);
                btnDel.Location = new Point(1649, 15);
                btnQuery.Location = new Point(500, 23);
                btnReset.Location = new Point(594, 23);
                btnClose.Location = new Point(688, 23);
                label5.Location = new Point(782, 30);
            }

            Text = NewuGlobal.LanguagResourceManager.GetString("000196");   //物料信息
            label3.Text = label1.Text = NewuGlobal.LanguagResourceManager.GetString("000182") + ":";   //所属设备
            label5.Text = NewuGlobal.GetRes("000170");
            label8.Text = label2.Text = NewuGlobal.LanguagResourceManager.GetString("000183") + ":";    //物料类型
            label9.Text = NewuGlobal.GetRes("000350") + ":";
            label10.Text = NewuGlobal.LanguagResourceManager.GetString("000181") + ":";
            label15.Text = NewuGlobal.LanguagResourceManager.GetString("000188") + ":";
            btnQuery.Text = NewuGlobal.LanguagResourceManager.GetString("000104");
            btnReset.Text = NewuGlobal.LanguagResourceManager.GetString("000105");
            btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");

            btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000100");
            btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            cmbTypeCode.SelectedIndex = -1;

            SetButtonStyle(btnClose);
            SetButtonStyle(btnDel);
            SetButtonStyle(btnQuery);
            SetButtonStyle(btnReset);

            this.groupBox1.Text = NewuGlobal.LanguagResourceManager.GetString("000189");  //查询条件
            int start = 180;
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.LanguagResourceManager.GetString((start + i).ToString("000000"));
                }
        }

        private void SetButtonStyle(Button btn)
        {
            if (NewuGlobal.SupportLanguage.Equals("1"))
                btn.Padding = new Padding(0, 0, 7, 0);
            else
                btn.Padding = new Padding(0, 0, 0, 0);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormulaMaterial existFormulaMaterial = formulaMaterials.Find(f => f.MaterialCode.Equals(txt_MaterialCode.Text.Trim()) && f.TypeCodeID.Equals(cmb_TypeCodeID.SelectedValue.ToString()) && f.DeviceID.Equals(cmbDeviceID.SelectedValue));
                if (existFormulaMaterial != null)
                {
                    label5.Text = NewuGlobal.GetRes("000322");
                    return;
                }

                string deviceID = cmbDeviceID.SelectedValue.ToString();
                string typeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                materialCode = txt_MaterialCode.Text;
                int enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
                string versionNo = "1";
                string deviceCode = NewuGlobal.DeviceCodeByID(deviceID);
                string materialForm = "0";
                string barCode = txt_BarCode.Text;
                string realName = NewuGlobal.TB_UserInfo.RealName;
                string userID = NewuGlobal.TB_UserInfo.UserID;
                string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                FormulaMaterial formulaMaterial = new FormulaMaterial
                {
                    MaterialCode = materialCode,
                    VersionNo = versionNo,
                    DeviceID = deviceID,
                    DeviceCode = deviceCode,
                    TypeCodeID = typeCodeID,
                    MaterialFrom = materialForm,
                    BarCode = barCode,
                    SaveRealName = realName,
                    Enable = enable,
                    SaveUserID = userID,
                    SaveTime = DateTime.Parse(saveTime),
                };
                bool result = formulaMaterialRepository.Add(formulaMaterial);
                if (result)
                {
                    StringBuilder strBuilder = new StringBuilder();
                    string typeCode;
                    if (NewuGlobal.SupportLanguage.Equals("1"))
                        typeCode = listN.Find(t => t.TypeCodeID.Equals(formulaMaterial.TypeCodeID)).TypeCodeDesc;
                    else
                        typeCode = listN.Find(t => t.TypeCodeID.Equals(formulaMaterial.TypeCodeID)).TypeCodeName;

                    strBuilder.AppendFormat("{0}{1}：{2}，{3}：{4}，{5}：{6}", NewuGlobal.GetRes("000100"), NewuGlobal.GetRes("000181"), formulaMaterial.MaterialCode, NewuGlobal.GetRes("000183"), typeCode, NewuGlobal.GetRes("000182"), deviceCode);
                    TB_OperateLog operateLog = new TB_OperateLog
                    {
                        DeviceID = deviceID,
                        LogInfo = strBuilder.ToString(),
                        LogType = AppLogType.Add.ToString(),
                        UserID = NewuGlobal.TB_UserInfo.UserCode,
                        SaveTime = DateTime.Now
                    };
                    operateLogRepository.Add(operateLog);
                    label5.Text = NewuGlobal.GetRes("000171");
                }
                else
                    label5.Text = NewuGlobal.GetRes("000172");

                GetData();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaMaterial").Error(ex.ToString());
            }
        }

        public void GetData()
        {
            try
            {
                string sqlStr = " 1=1 ";
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    sqlStr += " and (DeviceID='" + cmb_DeviceID.SelectedValue.ToString() + "' or DeviceID='') ";//空字符串时适用所有机台
                }

                if (cmbTypeCode.SelectedIndex >= 0)
                {
                    sqlStr += " and TypeCodeID='" + cmbTypeCode.SelectedValue.ToString() + "' ";
                }

                string masterPolymer = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T母炼配方);
                string masterID = NewuGlobal.GetTypeCodeIDByCodeName(masterPolymer);
                string finalPolymer = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T终炼配方);
                string finalID = NewuGlobal.GetTypeCodeIDByCodeName(finalPolymer);

                sqlStr += " and TypeCodeID not in ('" + masterID + "', '" + finalID + "')";

                formulaMaterials = formulaMaterialRepository.GetList(0, sqlStr, " TypeCodeID asc, MaterialCode asc,SaveTime desc ");
                dgv.DataSource = formulaMaterials;

                //删除最后一行时触发
                if (index >= dgv.Rows.Count)
                    index = dgv.Rows.Count - 1;

                dgv.Rows[0].Selected = false;
                dgv.Rows[index].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = index;
                DataGridViewRow dataGridViewRow = dgv.Rows[index];

                cmb_TypeCodeID.SelectedValue = dataGridViewRow.Cells["TypeCodeID"].Value.ToString();
                cmbDeviceID.SelectedValue = dataGridViewRow.Cells["DeviceID"].Value.ToString();
                txt_MaterialCode.Text = dataGridViewRow.Cells["MaterialCode"].Value.ToString();
                if (dataGridViewRow.Cells["BarCode"].Value != null)
                    txt_BarCode.Text = dataGridViewRow.Cells["BarCode"].Value.ToString();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaMaterial").Error(ex.ToString());
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
                return;
            string dgv_MaterialCode = dgv.Rows[index].Cells["MaterialCode"].Value.ToString();
            string dgv_DeviceID = dgv.Rows[index].Cells["DeviceID"].Value.ToString();
            string dgv_TypeCodeID = dgv.Rows[index].Cells["TypeCodeID"].Value.ToString();
            if (!dgv_MaterialCode.Equals(txt_MaterialCode.Text.Trim()) || !dgv_DeviceID.Equals(cmbDeviceID.SelectedValue.ToString().Trim()) || !dgv_TypeCodeID.Equals(cmb_TypeCodeID.SelectedValue.ToString().Trim()))
            {
                FormulaMaterial existFormulaMaterial = formulaMaterials.Find(f => f.MaterialCode.Equals(txt_MaterialCode.Text.Trim()) && f.TypeCodeID.Equals(cmb_TypeCodeID.SelectedValue.ToString()) && f.DeviceID.Equals(cmbDeviceID.SelectedValue));

                if (existFormulaMaterial != null)
                {
                    label5.Text = NewuGlobal.GetRes("000322");
                    return;
                }
            }

            DialogResult dialogResult = MessageBox.Show(NewuGlobal.GetRes("000177"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(txt_MaterialCode.Text))
            {
                int rowIndex = index;
                if (rowIndex >= 0)
                {
                    string materialID = dgv.Rows[index].Cells["MaterialID"].Value.ToString();
                    FormulaMaterial oldFormulaMaterial = formulaMaterials.Find(f => f.MaterialID.Equals(materialID));

                    FormulaMaterial formulaMaterial = formulaMaterialRepository.GetModel(materialID);

                    string typeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                    this.materialCode = txt_MaterialCode.Text;
                    int enable = Convert.ToInt32(cmb_Enabled.SelectedValue.ToString());
                    string deviceID = cmbDeviceID.SelectedValue.ToString();
                    string deviceCode = NewuGlobal.DeviceCodeByID(deviceID);
                    string barCode = txt_BarCode.Text;
                    string realName = NewuGlobal.TB_UserInfo.RealName;
                    string userID = NewuGlobal.TB_UserInfo.UserID;
                    string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    formulaMaterial.MaterialID = materialID;
                    formulaMaterial.MaterialCode = this.materialCode;
                    formulaMaterial.DeviceID = deviceID;
                    formulaMaterial.DeviceCode = deviceCode;
                    formulaMaterial.TypeCodeID = typeCodeID;
                    formulaMaterial.BarCode = barCode;
                    formulaMaterial.VersionNo = "1";
                    formulaMaterial.SaveRealName = realName;
                    formulaMaterial.Enable = enable;
                    formulaMaterial.SaveUserID = userID;
                    formulaMaterial.SaveTime = DateTime.Parse(saveTime);
                    bool result = formulaMaterialRepository.Update(formulaMaterial);
                    if (result)
                    {
                        StringBuilder strBuilder = new StringBuilder();
                        strBuilder.AppendFormat("{0}：{1}，{2}：{3} 修改内容：", NewuGlobal.GetRes("000181"), oldFormulaMaterial.MaterialCode, NewuGlobal.GetRes("000182"), oldFormulaMaterial.DeviceCode);
                        string oldTypeCode;
                        string typeCode;
                        if (NewuGlobal.SupportLanguage.Equals("1"))
                        {
                            oldTypeCode = listN.Find(t => t.TypeCodeID.Equals(oldFormulaMaterial.TypeCodeID)).TypeCodeDesc;
                            typeCode = listN.Find(t => t.TypeCodeID.Equals(formulaMaterial.TypeCodeID)).TypeCodeDesc;
                        }
                        else
                        {
                            oldTypeCode = listN.Find(t => t.TypeCodeID.Equals(oldFormulaMaterial.TypeCodeID)).TypeCodeName;
                            typeCode = listN.Find(t => t.TypeCodeID.Equals(formulaMaterial.TypeCodeID)).TypeCodeName;
                        }

                        foreach (var oldItem in oldFormulaMaterial.GetType().GetProperties())
                        {
                            foreach (var item in formulaMaterial.GetType().GetProperties())
                            {
                                if (item.Name == oldItem.Name && item.Name != "SaveTime" && item.Name != "SaveRealName")
                                {
                                    if (oldItem.GetValue(oldFormulaMaterial) != null && item.GetValue(formulaMaterial) != null)
                                    {
                                        if (!oldItem.GetValue(oldFormulaMaterial).ToString().Equals(item.GetValue(formulaMaterial).ToString()))
                                        {
                                            string title = GetTitle(oldItem.Name);
                                            if (oldItem.Name.Equals("TypeCodeID"))
                                                strBuilder.AppendFormat("[{0}] 由 [{1}] 修改为 [{2}]； ", title, oldTypeCode, typeCode);
                                            else
                                                strBuilder.AppendFormat("[{0}] 由 [{1}] 修改为 [{2}]； ", title, oldItem.GetValue(oldFormulaMaterial), item.GetValue(formulaMaterial));
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
                        label5.Text = NewuGlobal.GetRes("000171");
                    }
                    else
                        label5.Text = NewuGlobal.GetRes("000172");

                    GetData();
                }
            }
            else if (dialogResult == DialogResult.Cancel)
                return;
            else
                label5.Text = NewuGlobal.GetRes("000181") + NewuGlobal.GetRes("000162");
        }

        private string GetTitle(string name)
        {
            string title = "";
            switch (name)
            {
                case "MaterialCode":
                    title = NewuGlobal.GetRes("000181");
                    break;

                case "DeviceID":
                    title = NewuGlobal.GetRes("000182");
                    break;

                case "TypeCodeID":
                    title = NewuGlobal.GetRes("000183");
                    break;

                case "Reserve5":
                    title = NewuGlobal.GetRes("000184");
                    break;

                case "BarCode":
                    title = NewuGlobal.GetRes("000185");
                    break;

                case "Enable":
                    title = NewuGlobal.GetRes("000188");
                    break;

                default:
                    break;
            }
            return title;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
                return;

            int rowIndex = index;
            if (rowIndex >= 0)
            {
                string formulaName = dgv.Rows[index].Cells["MaterialCode"].Value.ToString();
                DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175") + " [ " + formulaName + " ]?", NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                FormulaMaterial formulaMaterial = formulaMaterialRepository.GetModel(dgv.Rows[index].Cells["MaterialID"].Value.ToString());

                if (isDel == DialogResult.Yes)
                {
                    try
                    {
                        formulaMaterial.Enable = 0;
                        bool isSccess = formulaMaterialRepository.Delete(formulaMaterial.MaterialID);
                        if (isSccess)
                        {
                            StringBuilder strBuilder = new StringBuilder();
                            strBuilder.AppendFormat("{0}{1}：{2}，{3}：{4}", NewuGlobal.GetRes("000102"), NewuGlobal.GetRes("000181"), formulaMaterial.MaterialCode, NewuGlobal.GetRes("000182"), formulaMaterial.DeviceCode);
                            TB_OperateLog operateLog = new TB_OperateLog
                            {
                                DeviceID = cmbDeviceID.SelectedValue.ToString(),
                                LogInfo = strBuilder.ToString(),
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
                            label5.Text = NewuGlobal.GetRes("000174");//"删除失败!,请先在配方库中清空配方！"
                        }
                    }
                    catch (Exception ex)
                    {
                        NewuGlobal.LogCat("FM_FormulaMaterial").Error(ex.ToString());
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceID.SelectedIndex = -1;
            cmbTypeCode.SelectedIndex = -1;
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                label5.Text = "提示";
                txt_MaterialCode.Text = dgv.CurrentRow.Cells["MaterialCode"].Value.ToString();
                cmb_Enabled.SelectedValue = dgv.CurrentRow.Cells["Enable"].Value.ToString();
                cmb_TypeCodeID.SelectedValue = dgv.CurrentRow.Cells["TypeCodeID"].Value.ToString();
                cmbDeviceID.SelectedValue = dgv.CurrentRow.Cells["DeviceID"].Value.ToString();
                if (dgv.CurrentRow.Cells["BarCode"].Value != null)
                    txt_BarCode.Text = dgv.CurrentRow.Cells["BarCode"].Value.ToString();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_FormulaMaterial").Error(ex.ToString());
            }
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }
    }
}