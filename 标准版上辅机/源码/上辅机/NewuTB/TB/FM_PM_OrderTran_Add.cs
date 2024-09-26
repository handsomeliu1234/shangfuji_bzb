using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_PM_OrderTran_Add : Form
    {
        private PM_OrderTran OrderTranModel = new PM_OrderTran();
        private readonly FormulaMaterialRepository formulaMaterialRepository = new FormulaMaterialRepository();
        private readonly SYS_DeviceRepository deviceRepository = new SYS_DeviceRepository();
        private readonly PM_OrderTranRepository orderTranRepository = new PM_OrderTranRepository();
        private List<FormulaMaterial> materiallist;

        /// <summary>
        /// 插入时当前行的订单id,确定
        /// </summary>
        public string nextOrderId;

        private string orderID = "";

        public FM_PM_OrderTran_Add()
        {
            InitializeComponent();
        }

        public FM_PM_OrderTran_Add(string _OrderID)
        {
            orderID = _OrderID;
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
        * 新建订单窗口载入时录入信息
        */

        private void FM_PM_OrderTran_Add_Load(object sender, EventArgs e)
        {
            string where = "TypeCodeID in(" + FormulaTypeIDS() + ") ";  //字符串拼接，前两个引号为一对，后两个引号为一对
            if (cmb_DeviceName.SelectedIndex >= 0)
            {
                where += "and  DeviceID='" + cmb_DeviceName.SelectedValue.ToString() + "'";
            }
            materiallist = formulaMaterialRepository.GetList(where);
            cmb_MaterialCode.DataSource = materiallist;
            cmb_MaterialCode.ValueMember = "MaterialID";      //显示的字段对应的值为物料ID
            cmb_MaterialCode.DisplayMember = "MaterialCode";  //显示的字段为配方编码
            cmb_MaterialCode.SelectedIndex = 0;              //设置选项为空

            List<SYS_Device> Devicelist = deviceRepository.GetList("");
            cmb_DeviceName.DataSource = Devicelist;
            cmb_DeviceName.ValueMember = "DeviceID";
            cmb_DeviceName.DisplayMember = "DeviceName";

            cmb_DeviceName.SelectedIndexChanged += new EventHandler(this.Cmb_DeviceName_SelectedIndexChanged);

            cmb_DeviceName.SelectedIndex = 0;
            if (orderID != "")  //编辑时
            {
                OrderTranModel = orderTranRepository.GetModel(orderID);
                cmb_DeviceName.SelectedValue = OrderTranModel.DeviceID;   //触发cmb_DeviceName_SelectedIndexChanged
                cmb_MaterialCode.SelectedValue = OrderTranModel.MaterialID;
                txt_SerialNumber.Text = OrderTranModel.SerialNumber.ToString();
                txt_Lot.Text = OrderTranModel.Lot;
                txt_SetBatch.Text = OrderTranModel.SetBatch.ToString();
            }
            else
            {
                if (nextOrderId == null)
                {
                    int no = orderTranRepository.GetMaxPlanSerialNumber(DateTime.Now) + 1;
                    txt_SerialNumber.Text = no.ToString();
                    txt_Lot.Text = DateTime.Now.ToString("yyMMdd") + no.ToString("000");
                    txt_SerialNumber.Enabled = false;
                    txt_Lot.Enabled = false;
                }
                else
                {
                    PM_OrderTran order = orderTranRepository.GetModel(nextOrderId);
                    txt_SerialNumber.Text = order.SerialNumber.ToString();
                    txt_Lot.Text = DateTime.Now.ToString("yyMMdd") + order.SerialNumber.ToString("000");
                    txt_SerialNumber.Enabled = false;
                    txt_Lot.Enabled = false;
                }
            }

            Cmb_DeviceName_SelectedIndexChanged(this, new EventArgs());
            SetControlLanguageText();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            OrderTranModel.VersionNo = lblVersionNo.Text;
            if (DataVerification() == false)
            {
                return;
            }

            if (OrderTranModel == null)
            {
                OrderTranModel = new PM_OrderTran();
            }
            if (cmb_DeviceName.SelectedIndex >= 0)
            {
                OrderTranModel.DeviceID = cmb_DeviceName.SelectedValue.ToString();
                OrderTranModel.DeviceName = cmb_DeviceName.Text.ToString();
            }
            if (cmb_MaterialCode.SelectedIndex >= 0)
            {
                OrderTranModel.MaterialID = cmb_MaterialCode.SelectedValue.ToString();
                OrderTranModel.MaterialCode = cmb_MaterialCode.Text.ToString();
            }

            OrderTranModel.IsDelete = 0;

            if (txt_SerialNumber.Text != "")
            {
                OrderTranModel.SerialNumber = Convert.ToInt16(txt_SerialNumber.Text);
            }

            OrderTranModel.Lot = txt_Lot.Text;
            if (txt_SetBatch.Text != "")
            {
                OrderTranModel.SetBatch = Convert.ToInt32(txt_SetBatch.Text);
            }

            OrderTranModel.OrderFrom = "0";
            OrderTranModel.StartUserID = NewuGlobal.TB_UserInfo.UserID;
            OrderTranModel.StartUserCode = NewuGlobal.TB_UserInfo.UserCode;
            OrderTranModel.WorkGroup = NewuGlobal.SoftConfig.WorkGroup;
            OrderTranModel.WorkOrder = NewuGlobal.SoftConfig.WorkOrder;

            OrderTranModel.Savetime = DateTime.Now;

            bool isAccess;
            if (string.IsNullOrEmpty(OrderTranModel.OrderID))
            {
                if (nextOrderId != null) //将插入后的订单serialNumber +1;
                {
                    orderTranRepository.UpdateAllOrderSerial(OrderTranModel.SerialNumber);
                }
                isAccess = orderTranRepository.Add(OrderTranModel);
            }
            else
            {
                isAccess = orderTranRepository.Update(OrderTranModel);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
                this.Close();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_PM_OrderTran fm = obj as FM_PM_OrderTran;
                if (fm != null)
                    fm.GetData();
            }
        }

        private bool DataVerification()
        {
            if (OrderTranModel.VersionNo == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000230") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (OrderTranModel.OrderFrom == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000356") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (txt_SerialNumber.Text == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000602") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (txt_SetBatch.Text == "" || int.Parse(txt_SetBatch.Text) <= 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000604") + NewuGlobal.GetRes("000167"));
                return false;
            }
            if (OrderTranModel.Lot == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000603") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }

        private void Cmb_DeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_DeviceName.Items.Count == 0)
                return;

            string where = "TypeCodeID in(" + FormulaTypeIDS() + ") and ";  //字符串拼接，前两个引号为一对，后两个引号为一对
            if (cmb_DeviceName.SelectedIndex >= 0)
            {
                where += "DeviceID='" + cmb_DeviceName.SelectedValue.ToString() + "'";
            }

            where += "and Enable = 1 order by MaterialCode ";
            List<FormulaMaterial> Materiallist = formulaMaterialRepository.GetList(where);
            cmb_MaterialCode.DataSource = Materiallist;
            cmb_MaterialCode.ValueMember = "MaterialID";      //显示的字段对应的值为物料ID
            cmb_MaterialCode.DisplayMember = "MaterialCode";  //显示的字段为配方编码
            cmb_MaterialCode.SelectedIndex = -1;              //设置选项为空

            this.cmb_MaterialCode.SelectedIndexChanged += new EventHandler(this.Cmb_MaterialCode_SelectedIndexChanged);

            if (orderID != null && OrderTranModel != null && orderID != "")
            {
                cmb_MaterialCode.SelectedValue = OrderTranModel.MaterialID;
            }
        }

        private void Cmb_MaterialCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormulaMaterial fm = cmb_MaterialCode.SelectedItem as FormulaMaterial;
            if (fm != null)
            {
                lblVersionNo.Text = fm.VersionNo;
            }
        }

        private string _formulaTypeStr = "";

        /// <summary>
        /// </summary>
        /// <returns>NewuModel.SYS_TypeCode类型的中的所有“配方类型实例”的TypeCodeID</returns>
        private string FormulaTypeIDS()
        {
            if (_formulaTypeStr != "")
                return _formulaTypeStr;

            List<SYS_TypeCode> sYS_TypeCodes = new SYS_TypeCodeRepository().GetList("TypeCodeName in ('MasterPolymer','FinalPolymer')");
            foreach (var item in sYS_TypeCodes)
            {
                _formulaTypeStr += "'" + item.TypeCodeID + "',";
            }
            if (_formulaTypeStr != "")
            {
                _formulaTypeStr = _formulaTypeStr.Substring(0, _formulaTypeStr.Length - 1); //去掉最后的逗号
            }

            return _formulaTypeStr;
        }

        #region 简易数字软键盘

        /// <summary>
        /// 简易数字软键盘
        /// </summary>
        /// <returns></returns>
        private void KeyB1_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("1");
        }

        private void KeyB2_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("2");
        }

        private void KeyB3_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("3");
        }

        private void KeyB4_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("4");
        }

        private void KeyB5_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("5");
        }

        private void KeyB6_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("6");
        }

        private void KeyB7_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("7");
        }

        private void KeyB8_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("8");
        }

        private void KeyB9_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("9");
        }

        private void KeyB0_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("0");
        }

        private void KeyB_delete_Click(object sender, EventArgs e)
        {
            txt_SetBatch.Focus();

            SendKeys.Send("{BACKSPACE}");
        }

        #endregion 简易数字软键盘

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            this.Text = NewuGlobal.GetRes("000325");

            groupBox2.Text = NewuGlobal.GetRes("000326");  //订单主要信息
            groupBox1.Text = NewuGlobal.GetRes("000327");  //其他数据

            label1.Text = NewuGlobal.GetRes("000328") + ":";// *设备名称
            label2.Text = NewuGlobal.GetRes("000329") + ":";// *配方名
            label4.Text = NewuGlobal.GetRes("000330") + ":";// *版本序号
            label13.Text = NewuGlobal.GetRes("000331") + ":";// *序号
            label12.Text = NewuGlobal.GetRes("000332") + ":";// *批号
            label11.Text = NewuGlobal.GetRes("000333") + ":";// *计划车数
            label18.Text = NewuGlobal.GetRes("000167");// *正整数非空

            /***********  常见按钮   ***********/
            btnSave.Text = NewuGlobal.GetRes("000108"); //保存
            btnClose.Text = NewuGlobal.GetRes("000103");//关闭
            buttonDelete.Text = NewuGlobal.GetRes("000102");

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnClose.Padding = new Padding(0, 0, 0, 0);
            }

            /***********  常见文字   ***********/
        }

        private void Cmb_MaterialCode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmb_MaterialCode.Text) || e.KeyCode == Keys.Back)
                    return;

                string where = "TypeCodeID in(" + FormulaTypeIDS() + ") and ";  //字符串拼接，前两个引号为一对，后两个引号为一对
                if (cmb_DeviceName.SelectedIndex >= 0)
                {
                    where += "DeviceID='" + cmb_DeviceName.SelectedValue.ToString() + "'";
                }
                if (cmb_MaterialCode.Text != "")
                {
                    where += " and MaterialCode like '%" + cmb_MaterialCode.Text + "%' ";
                }

                where += "and Enable = 1 order by MaterialCode ";
                List<FormulaMaterial> list = formulaMaterialRepository.GetList(where);
                if (list.Count == 0)
                {
                    return;
                }
                cmb_MaterialCode.DataSource = list;
                cmb_MaterialCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_MaterialCode.ValueMember = "MaterialID";      //显示的字段对应的值为物料ID
                cmb_MaterialCode.DisplayMember = "MaterialCode";  //显示的字段为配方编码
                cmb_MaterialCode.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_PM_OrderTran_Add").Error(ex.ToString());
            }
        }

        private void Txt_SetBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.Utils.TxtPreSetGcsU(e, false);
        }
    }
}