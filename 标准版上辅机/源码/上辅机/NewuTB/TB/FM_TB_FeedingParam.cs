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

namespace NewuTB.TB
{
    public partial class FM_TB_FeedingParam : Form, ILanguageChanged
    {
        private readonly TB_FeedingParamRepository feedingParamRepository = new TB_FeedingParamRepository();
        private List<TB_FeedingParam> tB_FeedingParams;
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();
        private readonly TB_UserInfoRepository userInfoRepository = new TB_UserInfoRepository();
        private readonly FeedingParamWriteToMem feedingParamWriteToMem = new FeedingParamWriteToMem();
        private List<SYS_TypeCode> sYS_TypeCodes;
        private int rowIndex;

        public FM_TB_FeedingParam()
        {
            InitializeComponent();
        }

        private void FM_TB_FeedingParam_Load(object sender, EventArgs e)
        {
            List<SYS_Device> sYS_Devices = deviceRepository.GetList(0, "", "DeviceName");
            cmb_DeviceID.DataSource = sYS_Devices;
            cmb_DeviceID.ValueMember = "DeviceID";
            cmb_DeviceID.DisplayMember = "DeviceName";

            List<SYS_Device> devices = new List<SYS_Device>();
            devices.AddRange(sYS_Devices);
            cmbDeviceID.DataSource = devices;
            cmbDeviceID.ValueMember = "DeviceID";
            cmbDeviceID.DisplayMember = "DeviceName";

            string typeCodeC = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T炭黑);
            string typeCodeZ = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T粉料);
            string where = string.Format("TypeCodeName in('{0}','{1}')", typeCodeC, typeCodeZ);
            cmb_TypeCodeID.DataSource = typeCodeRepository.GetList(where);
            sYS_TypeCodes = typeCodeRepository.GetList("");
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("FeedingID","加料ID",ColumnType.txt,false), //第一个参数必须对应表中字段名
                new ColStruct("BinNo","储斗编号"),
                new ColStruct("DeviceID","设备名称", ColumnType.cmb,true),
                new ColStruct("TypeCodeID","类型编码",ColumnType.cmb,true),
                new ColStruct("Big_FreqKuai","螺旋快速"),
                new ColStruct("Big_FreqZhong","螺旋低速"),
                new ColStruct("SaveUserID","保存用户",ColumnType.cmb,true)
            };

            dgv.AllowUserToAddRows = false;
            dgv.AddCols(cols);
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Columns[1].Width = 110;
            dgv.Columns[2].Width = 100;
            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewComboBoxColumn dgvColDepartment = dgv.Columns["DeviceID"] as DataGridViewComboBoxColumn;
            dgvColDepartment.DisplayMember = "DeviceName";
            dgvColDepartment.ValueMember = "DeviceID";
            dgvColDepartment.DataSource = sYS_Devices;

            List<TB_UserInfo> tB_UserInfos = userInfoRepository.GetList("");
            List<SaveUser> saveUserList = new List<SaveUser>();

            foreach (var userInfo in tB_UserInfos)
            {
                SaveUser saveUser = new SaveUser(userInfo.UserID, userInfo.RealName);
                saveUserList.Add(saveUser);
            }
            DataGridViewComboBoxColumn dgvColSaveUserID = dgv.Columns["SaveUserID"] as DataGridViewComboBoxColumn;
            dgvColSaveUserID.DisplayMember = "RealName";
            dgvColSaveUserID.ValueMember = "SaveUserID";
            dgvColSaveUserID.DataSource = saveUserList;
            SetControlLanguageText();
            GetData();
        }

        public void GetData()
        {
            try
            {
                string sqlQuery = "";
                if (cmb_DeviceID.SelectedIndex >= 0)
                {
                    string cmb_Equi = cmb_DeviceID.SelectedValue.ToString();
                    sqlQuery = " DeviceID = '" + cmb_Equi + "'";
                }
                tB_FeedingParams = feedingParamRepository.GetList(0, sqlQuery, "TypeCodeID,BinNo,DeviceID");
                dgv.DataSource = tB_FeedingParams;
                //删除最后一行时触发
                if (rowIndex >= dgv.Rows.Count)
                    rowIndex = dgv.Rows.Count - 1;

                dgv.Rows[0].Selected = false;
                dgv.Rows[rowIndex].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = rowIndex;

                tbBinNo.Text = dgv.Rows[rowIndex].Cells["BinNo"].Value.ToString();
                BigFreqKuai.Text = dgv.Rows[rowIndex].Cells["Big_FreqKuai"].Value.ToString();
                BigFreqZhong.Text = dgv.Rows[rowIndex].Cells["Big_FreqZhong"].Value.ToString();
                cmbDeviceID.SelectedValue = dgv.Rows[rowIndex].Cells["DeviceID"].Value.ToString();
                cmb_TypeCodeID.SelectedValue = dgv.Rows[rowIndex].Cells["TypeCodeID"].Value.ToString();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_FeedingParam").Error(ex.ToString());
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cmb_DeviceID.SelectedIndex = 0;
            GetData();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DataVerification())
                    return;

                int binNo = int.Parse(tbBinNo.Text);
                string typeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                string deviceID = cmbDeviceID.SelectedValue.ToString();

                TB_FeedingParam tB_FeedingParam = feedingParamRepository.GetModel(binNo, typeCodeID, deviceID);
                if (tB_FeedingParam == null)
                {
                    TB_FeedingParam feedingParam = new TB_FeedingParam
                    {
                        BinNo = binNo,
                        DeviceID = cmb_DeviceID.SelectedValue.ToString(),
                        TypeCodeID = typeCodeID,
                        Big_FreqKuai = decimal.Parse(BigFreqKuai.Text),
                        Big_FreqZhong = decimal.Parse(BigFreqZhong.Text),
                        SaveTime = DateTime.Now,
                        SaveUserID = NewuGlobal.TB_UserInfo.UserID
                    };
                    bool result = feedingParamRepository.Add(feedingParam);
                    if (result)
                    {
                        label3.Text = NewuGlobal.GetRes("000171");
                        GetData();
                    }
                    else
                        label3.Text = NewuGlobal.GetRes("000172");
                }
                else
                    label3.Text = NewuGlobal.GetRes("000322");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_FeedingParam").Error(ex.ToString());
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                    return;

                if (rowIndex >= 0)
                {
                    DialogResult dialogResult = MessageBox.Show(NewuGlobal.GetRes("000177"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        int binNo = int.Parse(dgv.Rows[rowIndex].Cells["BinNo"].Value.ToString());
                        string deviceID = dgv.Rows[rowIndex].Cells["DeviceID"].Value.ToString();
                        string typeCodeID = dgv.Rows[rowIndex].Cells["TypeCodeID"].Value.ToString();
                        TB_FeedingParam feedingParam = feedingParamRepository.GetModel(binNo, typeCodeID, deviceID);
                        if (feedingParam != null)
                        {
                            if (feedingParam.TypeCodeID.Equals(cmb_TypeCodeID.SelectedValue.ToString()))
                            {
                                UpdateFeedingParam(feedingParam);
                            }
                            else
                            {
                                binNo = int.Parse(tbBinNo.Text);
                                deviceID = cmbDeviceID.SelectedValue.ToString();
                                typeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
                                TB_FeedingParam tB_FeedingParam = feedingParamRepository.GetModel(binNo, typeCodeID, deviceID);
                                if (tB_FeedingParam != null)
                                {
                                    label3.Text = NewuGlobal.GetRes("000322");
                                    return;
                                }
                                else
                                    UpdateFeedingParam(feedingParam);
                            }
                        }
                        else
                            UpdateFeedingParam(feedingParam);
                    }
                }
                else
                {
                    label3.Text = NewuGlobal.GetRes("000066"); //请先选中一行
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_FeedingParam").Error(ex.ToString());
            }
        }

        private void UpdateFeedingParam(TB_FeedingParam feedingParam)
        {
            feedingParam.BinNo = int.Parse(tbBinNo.Text);
            feedingParam.DeviceID = cmbDeviceID.SelectedValue.ToString();
            feedingParam.TypeCodeID = cmb_TypeCodeID.SelectedValue.ToString();
            feedingParam.Big_FreqKuai = decimal.Parse(BigFreqKuai.Text);
            feedingParam.Big_FreqZhong = decimal.Parse(BigFreqZhong.Text);
            feedingParam.SaveTime = DateTime.Now;
            feedingParam.SaveUserID = NewuGlobal.TB_UserInfo.UserID;
            bool result = feedingParamRepository.Update(feedingParam);
            if (result)
            {
                label3.Text = NewuGlobal.GetRes("000171");
                GetData();
            }
            else
                label3.Text = NewuGlobal.GetRes("000172");
        }

        private bool DataVerification()
        {
            //"储罐编号不能为空且为正整数"
            if (FunClass.IsEmptyOrNumber(tbBinNo.Text.Trim()) != 2)
            {
                label3.Text = NewuGlobal.GetRes("000404");
                return false;
            }

            if (cmb_DeviceID.SelectedIndex < 0)
            {
                label3.Text = NewuGlobal.GetRes("000460") + NewuGlobal.GetRes("000162");
                return false;
            }

            if (BigFreqKuai.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFreqKuai.Text) == 0)
                {
                    label3.Text = NewuGlobal.GetRes("000404");//"螺旋变频器快速必须是数值!"
                    return false;
                }
            }

            if (BigFreqZhong.Text != string.Empty)
            {
                if (FunClass.IsEmptyOrNumber(BigFreqZhong.Text) == 0)
                {
                    label3.Text = NewuGlobal.GetRes("000404");//"螺旋变频器快速必须是数值!"
                    return false;
                }
            }

            //"材料类型不能为空！"
            if (cmb_TypeCodeID.SelectedIndex < 0)
            {
                label3.Text = NewuGlobal.GetRes("000401") + " " + NewuGlobal.GetRes("000162");
                return false;
            }

            return true;
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
                    string feedingId = dgv[0, rowIndex].Value.ToString();
                    string binNo = dgv[1, rowIndex].Value.ToString();
                    DialogResult isDel = MessageBox.Show(NewuGlobal.GetRes("000175") + " [ " + binNo + " ] ?", NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (isDel == DialogResult.Yes)
                    {
                        bool isAccess = feedingParamRepository.Delete(feedingId);
                        if (isAccess)
                        {
                            label3.Text = NewuGlobal.GetRes("000173");
                            GetData();
                        }
                        else
                        {
                            label3.Text = NewuGlobal.GetRes("000174");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(NewuGlobal.GetRes("000164"), NewuGlobal.GetRes("000578") + ex.ToString());
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtWriteToMem_Click(object sender, EventArgs e)
        {
            bool isOK = false;
            if (dgv.Rows.Count == 0)
            {
                return;
            }

            //单个下发
            if (sender == btWriteSelectedToMem)
            {
                if (rowIndex >= 0)
                {
                    string feedingId = dgv[0, rowIndex].Value.ToString();
                    isOK = feedingParamWriteToMem.WriteToMemS(feedingParamRepository.GetModel(feedingId));
                    if (isOK == true)
                    {
                        label3.Text = NewuGlobal.GetRes("000067");
                    }
                    else
                    {
                        label3.Text = NewuGlobal.GetRes("000068");
                    }
                }
                else
                {
                    label3.Text = NewuGlobal.GetRes("000066");
                    return;
                }
            }

            //全部下发
            if (sender == btWriteAllToMem)
            {
                DialogResult isAddAll = MessageBox.Show(NewuGlobal.GetRes("000069") + " [ " + cmb_DeviceID.Text + " ]PLC ?", NewuGlobal.GetRes("000170"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (isAddAll == DialogResult.Yes)
                {
                    this.Enabled = false;
                    feedingParamWriteToMem.OnWriteProgress += FeedingParamWriteToMem_onWriteProgress;

                    isOK = feedingParamWriteToMem.WriteToMem(tB_FeedingParams);

                    if (isOK == true)
                    {
                        label3.Text = NewuGlobal.GetRes("000067");
                    }
                    else
                    {
                        label3.Text = NewuGlobal.GetRes("000068");
                    }
                }
                if (isAddAll == DialogResult.No)
                {
                    label3.Text = NewuGlobal.GetRes("000070");
                }
            }
            this.Enabled = true;
        }

        private void FeedingParamWriteToMem_onWriteProgress(int total, int current)
        {
            writeProgressBar.Maximum = total;
            writeProgressBar.Value = current;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            this.Text = NewuGlobal.GetRes("000032"); //加料参数设定
            groupBox1.Text = NewuGlobal.GetRes("000458");  //
            groupBox2.Text = NewuGlobal.GetRes("000459");  //
            label6.Text = NewuGlobal.GetRes("000464") + ":";// *储斗编号
            label2.Text = NewuGlobal.GetRes("000467") + ":";// *螺旋快速
            label1.Text = NewuGlobal.GetRes("000468") + ":";// *螺旋低速
            label3.Text = NewuGlobal.GetRes("000170");

            labEquipmentTyp.Text = NewuGlobal.GetRes("000466") + ":";// *类型编码
            label10.Text = lab_Equipment.Text = NewuGlobal.GetRes("000460") + ":";// *

            /***********  常见按钮   ***********/
            btnAdd.Text = NewuGlobal.GetRes("000100"); //新增
            btnEdit.Text = NewuGlobal.GetRes("000101"); //编辑
            btnDel.Text = NewuGlobal.GetRes("000102"); //删除
            btnClose.Text = NewuGlobal.GetRes("000103"); //关闭
            btnQuery.Text = NewuGlobal.GetRes("000104");//查询
            btnReset.Text = NewuGlobal.GetRes("000105");//重置
            btWriteSelectedToMem.Text = NewuGlobal.GetRes("000461");//单项下发PLC
            btWriteAllToMem.Text = NewuGlobal.GetRes("000462");//全部下发PLC

            /***********  常见文字   ***********/

            int start = 463;
            if (dgv != null && dgv.Columns != null)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = NewuGlobal.GetRes((start + i).ToString("000000"));
                }

            DataGridViewComboBoxColumn dgvTypeCode = dgv.Columns["TypeCodeID"] as DataGridViewComboBoxColumn;
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btWriteSelectedToMem.Size = btWriteAllToMem.Size = new Size(112, 30);
                btWriteAllToMem.Location = new Point(1704, 13);
                btnClose.Padding = new Padding(0, 0, 7, 0);
                btnDel.Padding = btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                cmb_TypeCodeID.DisplayMember = "TypeCodeDesc";
                dgvTypeCode.DisplayMember = "TypeCodeDesc";
            }
            else
            {
                btWriteSelectedToMem.Size = new Size(135, 30);
                btWriteAllToMem.Size = new Size(122, 30);
                btWriteAllToMem.Location = new Point(1722, 13);
                btnDel.Padding = btnQuery.Padding = btnReset.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                cmb_TypeCodeID.DisplayMember = "TypeCodeName";
                dgvTypeCode.DisplayMember = "TypeCodeName";
            }
            cmb_TypeCodeID.ValueMember = "TypeCodeID";

            dgvTypeCode.ValueMember = "TypeCodeID";
            dgvTypeCode.DataSource = sYS_TypeCodes;
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            label3.Text = NewuGlobal.GetRes("000170");
            if (rowIndex < 0)
                rowIndex = 0;
            tbBinNo.Text = dgv.Rows[rowIndex].Cells["BinNo"].Value.ToString();
            BigFreqKuai.Text = dgv.Rows[rowIndex].Cells["Big_FreqKuai"].Value.ToString();
            BigFreqZhong.Text = dgv.Rows[rowIndex].Cells["Big_FreqZhong"].Value.ToString();
            cmbDeviceID.SelectedValue = dgv.Rows[rowIndex].Cells["DeviceID"].Value.ToString();
            cmb_TypeCodeID.SelectedValue = dgv.Rows[rowIndex].Cells["TypeCodeID"].Value.ToString();
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.Utils.TxtPreSetGcsU(e, true);
        }
    }
}