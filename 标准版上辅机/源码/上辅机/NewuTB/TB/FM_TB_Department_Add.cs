using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_Department_Add : Form
    {
        private string departmentID = "";
        private TB_Department department;
        private readonly TB_DepartmentRepository departmentRepository = new TB_DepartmentRepository();

        private CommonTools.Node FoucesNode
        {
            get; set;
        }

        private NodeLevel AddNodeLevel
        {
            get; set;
        }

        public FM_TB_Department_Add(CommonTools.Node node, NodeLevel level)
        {
            InitializeComponent();

            FoucesNode = node;
            AddNodeLevel = level;
        }

        public FM_TB_Department_Add(string _DepartmentID)
        {
            InitializeComponent();
            departmentID = _DepartmentID;
            AddNodeLevel = NodeLevel.Child;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (department == null)
            {
                department = new TB_Department();
            }

            department.DepartmentName = txt_DepartmentName.Text;
            department.DepartmentCode = txt_DepartmentCode.Text;
            department.DepartmentJaneSpell = txt_DepartmentJaneSpell.Text;
            department.DepartmentRemark = txt_DepartmentRemark.Text;
            if (cmb_ParentDepartmentID.SelectedIndex >= 0)

                department.ParentDepartmentID = cmb_ParentDepartmentID.SelectedValue.ToString();
            else
                department.ParentDepartmentID = "";

            department.SaveTime = DateTime.Now;

            if (DataVerification() == false)
            {
                return;
            }

            bool isAccess;
            if (string.IsNullOrEmpty(department.DepartmentID))
            {
                isAccess = departmentRepository.Add(department);
            }
            else
            {
                isAccess = departmentRepository.Update(department);
            }

            if (isAccess == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000495") + NewuGlobal.GetRes("000171"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(department.DepartmentID))
                {
                    ClearControl();
                }
                RefreshGrid();
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000495") + NewuGlobal.GetRes("000172"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_TB_Department fm = obj as FM_TB_Department;
                fm.GetData();
            }
        }

        private void ClearControl()
        {
            txt_DepartmentName.Text = "";
            txt_DepartmentCode.Text = "";
            txt_DepartmentJaneSpell.Text = "";
            cmb_ParentDepartmentID.SelectedIndex = -1;
        }

        private bool DataVerification()
        {
            if (department.DepartmentName == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000503") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (department.DepartmentCode == "")
            {
                MessageBox.Show(NewuGlobal.GetRes("000504") + NewuGlobal.GetRes("000162"));
                return false;
            }
            if (department.ParentDepartmentID == "" && cmb_ParentDepartmentID.Enabled == true)
            {
                MessageBox.Show(NewuGlobal.GetRes("000037") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }

        private void FM_TB_Department_Add_Load(object sender, EventArgs e)
        {
            SetLanguage();
            List<TB_Department> list = departmentRepository.GetList("");
            cmb_ParentDepartmentID.DataSource = list;
            cmb_ParentDepartmentID.ValueMember = "DepartmentID";
            cmb_ParentDepartmentID.DisplayMember = "DepartmentName";

            if (departmentID != "")
            {
                department = departmentRepository.GetModel(departmentID);

                txt_DepartmentName.Text = department.DepartmentName;
                txt_DepartmentCode.Text = department.DepartmentCode;
                txt_DepartmentJaneSpell.Text = department.DepartmentJaneSpell;
                cmb_ParentDepartmentID.SelectedValue = department.ParentDepartmentID;
                txt_DepartmentRemark.Text = department.DepartmentRemark;
            }

            switch (AddNodeLevel)
            {
                case NodeLevel.Top:
                    cmb_ParentDepartmentID.SelectedIndex = -1;
                    break;

                case NodeLevel.Child:
                    if (FoucesNode != null)
                    {
                        cmb_ParentDepartmentID.SelectedValue = FoucesNode["DepartmentID"].ToString();
                    }
                    break;

                default:
                    break;
            }
        }

        private void SetLanguage()
        {
            btnSave.Text = NewuGlobal.GetRes("000108");
            btnClose.Text = NewuGlobal.GetRes("000103");
            groupBox1.Text = NewuGlobal.GetRes("000502");
            label1.Text = NewuGlobal.GetRes("000503") + "：";
            label2.Text = NewuGlobal.GetRes("000504") + "：";
            label3.Text = NewuGlobal.GetRes("000505") + "：";
            label4.Text = NewuGlobal.GetRes("000506") + "：";
            label5.Text = NewuGlobal.GetRes("000507") + "：";
            label6.Text = label7.Text = label8.Text = NewuGlobal.GetRes("000162");
            if (!NewuGlobal.SupportLanguage.Equals("1"))
                btnClose.Padding = new Padding(0, 0, 0, 0);
        }
    }
}