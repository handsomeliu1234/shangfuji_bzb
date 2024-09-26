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
    public partial class FM_FormulaMixF_AddStep : Form
    {
        private readonly SYS_ActionControlRepository actionControlRepository = new SYS_ActionControlRepository();
        private readonly SYS_ActionStepRepository actionStepRepository = new SYS_ActionStepRepository();
        private string controlCode;
        private string DevicePartID;
        private string DeviceID;
        private int StepOrder;
        private FormulaMixF MixFModel;
        private List<FormulaMixF> MixFList;

        public FM_FormulaMixF_AddStep(FormulaMixF model, string _deviceId, string _devicePartId, List<FormulaMixF> _list, int _stepOrder1)
        {
            InitializeComponent();

            MixFModel = model;
            controlCode = MixFModel.ActionControlCode;
            DevicePartID = _devicePartId;
            DeviceID = _deviceId;
            MixFList = _list;
            StepOrder = _stepOrder1;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            int stepOrder = Convert.ToInt32(cmb_StepOrder.SelectedValue);

            string msg = ComputeDropOrder(stepOrder);
            if (msg != "")
            {
                MessageBox.Show(msg, NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Verification() == false)
            {
                return;
            }
            MixFModel.DevicePartID = DevicePartID;
            MixFModel.DevicePartCode = NewuGlobal.DevicePartCodeByID(DevicePartID);

            MixFModel.StepOrder = stepOrder;
            MixFModel.StepCode = GetStepSumBit(out string stepStr);
            MixFModel.StepDesc = stepStr;
            MixFModel.ActionControlCode = cmb_ActionControlCode.SelectedValue.ToString();

            List<SYS_ActionControl> actionControlList = actionControlRepository.GetList(" ActionControlCode='" + MixFModel.ActionControlCode + "'");

            if (actionControlList[0].ActionControlNameCN.Contains("时间"))
            {
                if (FunClass.VVal(txt_StepTime.Text) == 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000290") + " 【" + NewuGlobal.GetRes("000476") + "】", NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (actionControlList[0].ActionControlNameCN.Contains("温度"))
            {
                if (FunClass.VVal(txt_StepTemp.Text) == 0)
                {
                    //"控制方式为【温度】必须输入！", "信息"
                    MessageBox.Show(NewuGlobal.GetRes("000290") + " 【" + NewuGlobal.GetRes("000636") + "】", NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (actionControlList[0].ActionControlNameCN.Contains("能量"))
            {
                if (FunClass.VVal(txt_StepEnergy.Text) == 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000290") + " 【" + NewuGlobal.GetRes("000638") + "】", NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (actionControlList[0].ActionControlNameCN == "功率")
            {
                if (FunClass.VVal(txt_StepPower.Text) == 0)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000290") + " 【" + NewuGlobal.GetRes("000637") + "】", NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (FunClass.VVal(cmb_StepSpeed.Text) == 0 || FunClass.VVal(cmb_StepSpeed.Text) > 60)
            {   //"【转速】必须大于0，小数60！", "信息"
                MessageBox.Show("0 < " + NewuGlobal.GetRes("000640") + "< 60", NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FunClass.VVal(cmb_StepPress.Text) == 0 || FunClass.VVal(cmb_StepPress.Text) > 6)
            {   //"【压力】必须大于0，小数6！", "信息"
                MessageBox.Show("0 < " + NewuGlobal.GetRes("000639") + "< 6", NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MixFModel.StepTime = FunClass.VVal(txt_StepTime.Text);
            MixFModel.StepTemp = FunClass.VDecimal(txt_StepTemp.Text);
            MixFModel.StepPower = FunClass.VVal(txt_StepPower.Text);
            MixFModel.StepEnergy = FunClass.VDecimal(txt_StepEnergy.Text);
            MixFModel.StepPress = FunClass.VDecimal(cmb_StepPress.Text);
            MixFModel.StepSpeed = FunClass.VDecimal(cmb_StepSpeed.Text);
            MixFModel.KeepTime = FunClass.VVal(txt_KeepTime.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private int GetStepSumBit(out string stepStr)
        {
            stepStr = "";

            CheckedListBox checkList = null;
            if (radioButtonDrop.Checked)
            {
                checkList = checkListDrop;
            }
            if (radioButtonMixUp.Checked)
            {
                checkList = checkListMixUp;
            }

            int subBit = 0;

            foreach (object obj in checkList.CheckedItems)
            {
                Item<int, string> item = (Item<int, string>)obj;

                subBit += Convert.ToInt32(Math.Pow(2, item.Value - 1));
                stepStr += item.DisplayName + ",";
            }

            if (stepStr != "")
            {
                stepStr = stepStr.Substring(0, stepStr.Length - 1);
            }
            return subBit;
        }

        private void FM_FormulaMixF_AddStep_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;

            radioButtonMixUp.Click += new EventHandler(RadioButton_Click);
            radioButtonDrop.Click += new EventHandler(RadioButton_Click);
            RadioButton_Click(radioButtonDrop, null);

            cmb_StepOrder.DataSource = actionStepRepository.GetStepOrderTable();
            cmb_StepOrder.DisplayMember = "name";
            cmb_StepOrder.ValueMember = "value";
            cmb_StepOrder.SelectedIndex = StepOrder;

            cmb_ActionControlCode.DataSource = NewuGlobal.ActionControlList;

            if (NewuGlobal.SupportLanguage != "1" || NewuGlobal.SupportLanguage == null)
            {
                cmb_ActionControlCode.DisplayMember = "ActionControlNameEN";
                cmb_ActionControlCode.ValueMember = "ActionControlCode";
            }
            else
            {
                cmb_ActionControlCode.DisplayMember = "ActionControlNameCN";
                cmb_ActionControlCode.ValueMember = "ActionControlCode";
            }

            GetMixStepToCheckListBox();

            InitPage();
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            /***********  专有翻译区域   ***********/
            radioButtonDrop.Text = NewuGlobal.GetRes("000277");  //投料步骤
            radioButtonMixUp.Text = NewuGlobal.GetRes("000157");  //密炼机
            this.Text = NewuGlobal.GetRes("000275");  //配方工艺数据

            groupBox2.Text = NewuGlobal.GetRes("000163");  //密炼数据设定
            groupBox1.Text = NewuGlobal.GetRes("000247");  //工艺步骤

            label7.Text = NewuGlobal.GetRes("000244")+":";// 步骤顺序
            label9.Text = NewuGlobal.GetRes("000246") + ":";// 时间
            label14.Text = NewuGlobal.GetRes("000249") + ":";// 功率
            label18.Text = NewuGlobal.GetRes("000251") + ":";// 压力
            label22.Text = NewuGlobal.GetRes("000284") + ":";// 保持时间
            label6.Text = NewuGlobal.GetRes("000285") + ":";// 动作控制方式
            label12.Text = NewuGlobal.GetRes("000286") + ":";// 温度
            label16.Text = NewuGlobal.GetRes("000287") + ":";// 能量
            label20.Text = NewuGlobal.GetRes("000288") + ":";// 转速

            label10.Text = "*s";
            label13.Text = "*kW";
            //label17.Text = "*kPa";
            label17.Text = "*kg";
            label21.Text = "*s";
            label11.Text = "*℃";
            label15.Text = "*kJ";
            label19.Text = "*r";
            /***********  常见按钮   ***********/
            btnOk.Text = NewuGlobal.GetRes("000108"); //确定
            btnClose.Text = NewuGlobal.GetRes("000103");//关闭
            if (NewuGlobal.SupportLanguage.Equals("1"))
                btnClose.Padding = new Padding(0, 0, 7, 0);
            else
                btnClose.Padding = new Padding(0, 0, 0, 0);

            /***********  常见文字   ***********/
        }

        private void InitPage()
        {
            cmb_StepOrder.SelectedValue = StepOrder;//MixModel.StepOrder
            if (MixFModel.ActionControlCode != null)
                cmb_ActionControlCode.SelectedValue = MixFModel.ActionControlCode;
            txt_StepTime.Text = MixFModel.StepTime.ToString();
            txt_StepTemp.Text = MixFModel.StepTemp.ToString();
            txt_StepPower.Text = MixFModel.StepPower.ToString();
            txt_StepEnergy.Text = MixFModel.StepEnergy.ToString();
            cmb_StepPress.Text = MixFModel.StepPress.ToString();
            cmb_StepSpeed.Text = MixFModel.StepSpeed.ToString();
            txt_KeepTime.Text = MixFModel.KeepTime.ToString();

            CheckedListBox[] boxs = new CheckedListBox[]{
                checkListDrop,
                checkListMixUp
            };

            actionStepRepository.FindCheckListItemByMixStepCode(MixFModel.StepCode, boxs, MixFModel.StepDesc);

            if (checkListDrop.CheckedItems.Count > 0)
            {
                radioButtonDrop.Checked = true;
                RadioButton_Click(radioButtonDrop, null);
            }

            if (checkListMixUp.CheckedItems.Count > 0)
            {
                radioButtonMixUp.Checked = true;
                RadioButton_Click(radioButtonMixUp, null);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (controlCode != cmb_ActionControlCode.SelectedValue.ToString())
            {
                MixFModel.ActionControlCode = controlCode;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool Verification()
        {
            CheckedListBox checkList = null;
            if (radioButtonDrop.Checked)
            {
                checkList = checkListDrop;
            }
            if (radioButtonMixUp.Checked)
            {
                checkList = checkListMixUp;
            }

            if (checkList.CheckedItems.Count == 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000274"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"工艺步骤必须要进行选择！"
                return false;
            }
            if (cmb_ActionControlCode.SelectedIndex < 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000295"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"工艺步骤中的【控制方式】要求必须录入！"
                return false;
            }

            if (cmb_StepOrder.SelectedIndex < 0)
            {
                MessageBox.Show(NewuGlobal.GetRes("000296"), NewuGlobal.GetRes("000170"), MessageBoxButtons.OK, MessageBoxIcon.Information);//"详细数据中的【步骤顺序】要求必须录入！"
                return false;
            }

            // 如果为投料工艺步 radioButtonDrop 则该步骤就Ok
            if (radioButtonDrop.Checked)
            {
                return true;
            }

            return true;
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            CheckedListBox checkList = null;
            switch (radioButton.Name)
            {
                case "radioButtonDrop":
                    if (radioButton.Checked)
                    {
                        checkListDrop.Enabled = true;
                        checkListMixUp.Enabled = false;
                        checkList = checkListMixUp;
                    }
                    break;

                case "radioButtonMixUp":
                    if (radioButton.Checked)
                    {
                        checkListDrop.Enabled = false;
                        checkListMixUp.Enabled = true;
                        checkList = checkListDrop;
                    }
                    break;
            }
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                checkList.SetItemChecked(i, false);
            }
        }

        public void GetMixStepToCheckListBox()
        {
            actionStepRepository.FunStepToCheckList(DeviceID, "DropStep", checkListDrop);
            actionStepRepository.FunStepToCheckList(DeviceID, "MixStep", checkListMixUp);
        }

        /// <summary>
        /// 验证工艺序号输入是否正确
        /// </summary>
        /// <param name="stepOrder"></param>
        /// <returns></returns>
        private string ComputeDropOrder(int stepOrder)
        {
            int computerOrder = 0;
            int cishu = 0;
            foreach (FormulaMixF item in MixFList)
            {
                if (item.StepOrder == stepOrder)
                {
                    cishu++;
                }

                if (Convert.ToInt32(item.StepOrder) > computerOrder)
                {
                    computerOrder = item.StepOrder;
                }
            }
            computerOrder++;

            if (cishu > 1)
            {
                return NewuGlobal.GetRes("000142") + stepOrder + NewuGlobal.GetRes("000322"); //"已存在，不允许再次输入！"
            }
            if (stepOrder > computerOrder)
            {
                return NewuGlobal.GetRes("000304") + (stepOrder - 1) + NewuGlobal.GetRes("000289") + stepOrder + NewuGlobal.GetRes("000142");//"步工艺步骤未录入，不允许录入第"
            }
            else
            {
                return "";
            }
        }

        private void CheckListMixUp_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkListMixUp.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkListMixUp.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkListMixUp.SetItemChecked(i, false);
                    }
                }
            }
        }
    }
}