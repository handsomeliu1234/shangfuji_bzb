using MultiLanguage;
using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuTB.TB
{
    public partial class FM_TB_Department : Form, ILanguageChanged
    {
        private readonly TB_DepartmentRepository departmentRepository = new TB_DepartmentRepository();
        private List<TB_Department> list;
        private TB_Department tB_Department;

        public FM_TB_Department()
        {
            InitializeComponent();
        }

        private void FM_TB_Department_Load(object sender, EventArgs e)
        {
            SetLanguage();

            list = departmentRepository.GetList("");
            cmb_ParentDepartmentID.DataSource = list;
            cmb_ParentDepartmentID.ValueMember = "DepartmentID";
            cmb_ParentDepartmentID.DisplayMember = "DepartmentName";

            GetData();
        }

        public void GetData()
        {
            treeListView1.Nodes.Clear();

            list = departmentRepository.GetList("");
            if (list != null)
            {
                treeListView1.BeginUpdate();
                LoaderMen(null, "", list);
                treeListView1.EndUpdate();
            }
        }

        private void LoaderMen(CommonTools.Node node, string pid, List<TB_Department> list)
        {
            foreach (TB_Department item in list)
            {
                if (item.ParentDepartmentID == pid)
                {
                    object[] obj = new object[5];

                    obj[0] = item.DepartmentName;
                    obj[1] = item.DepartmentCode;
                    obj[2] = item.DepartmentRemark;
                    obj[3] = item.DepartmentID;

                    CommonTools.Node P = new CommonTools.Node(obj)
                    {
                        Tag = item
                    };
                    if (pid == "")
                    {
                        treeListView1.Nodes.Add(P);
                        LoaderMen(P, item.DepartmentID, list);
                    }
                    else
                    {
                        node.Nodes.Add(P);
                        LoaderMen(P, item.DepartmentID, list);
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            CommonTools.Node node = treeListView1.FocusedNode;
            List<string> listID = new List<string>();

            if (node != null)
            {
                node["DepartmentName"].ToString();
                listID.Add(node["DepartmentID"].ToString());
                GetParentID(node, listID);
            }
            else
            {
                MessageBox.Show(NewuGlobal.GetRes("000208"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DialogResult diaResult = MessageBox.Show(NewuGlobal.GetRes("000175"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (diaResult == DialogResult.Cancel)
                return;

            string delStr = "";
            for (int i = 0; i < listID.Count; i++)
            {
                delStr += "'" + listID[i] + "',";
            }
            if (delStr != "")
            {
                delStr = delStr.Substring(0, delStr.Length - 1);

                bool isDel = departmentRepository.DeleteList(delStr);
                if (isDel)
                {
                    MessageBox.Show(NewuGlobal.GetRes("000173"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                }
                else
                {
                    MessageBox.Show(NewuGlobal.GetRes("000174"), NewuGlobal.GetRes("000578"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void GetParentID(CommonTools.Node node, List<string> list)
        {
            CommonTools.NodeCollection collectNode = node.Nodes;

            for (int i = 0; i < collectNode.Count; i++)
            {
                CommonTools.Node tempNode = collectNode[i];
                list.Add(tempNode["DepartmentID"].ToString());

                GetParentID(tempNode, list);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ExcuteData(new TB_Department(), NodeLevel.Top, true);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (tB_Department != null)
            {
                //父节点
                if (string.IsNullOrEmpty(tB_Department.ParentDepartmentID))
                    ExcuteData(tB_Department, NodeLevel.Top, false);
                else
                    ExcuteData(tB_Department, NodeLevel.Child, false);
            }
        }

        private void BtnAddChild_Click(object sender, EventArgs e)
        {
            ExcuteData(new TB_Department(), NodeLevel.Child, true);
        }

        private void ExcuteData(TB_Department department, NodeLevel nodeLevel, bool flag)
        {
            try
            {
                department.DepartmentName = txt_DepartmentName.Text;
                department.DepartmentCode = txt_DepartmentCode.Text;
                department.DepartmentJaneSpell = txt_DepartmentJaneSpell.Text;
                department.DepartmentRemark = txt_DepartmentRemark.Text;
                if (nodeLevel == NodeLevel.Top)
                    department.ParentDepartmentID = "";
                else
                {
                    if (cmb_ParentDepartmentID.SelectedIndex >= 0)
                        department.ParentDepartmentID = cmb_ParentDepartmentID.SelectedValue.ToString();
                    else
                        department.ParentDepartmentID = "";
                }

                department.SaveTime = DateTime.Now;

                if (!DataVerification(department, nodeLevel))
                    return;

                bool result;
                if (flag)
                    result = departmentRepository.Add(department);
                else
                    result = departmentRepository.Update(department);

                if (result)
                {
                    hint.Text = NewuGlobal.GetRes("000171");
                    GetData();
                }
                else
                    hint.Text = NewuGlobal.GetRes("000172");
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_TB_Department").Error(ex.ToString());
            }
        }

        private bool DataVerification(TB_Department department, NodeLevel nodeLevel)
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
            if (department.ParentDepartmentID == "" && nodeLevel == NodeLevel.Child)
            {
                MessageBox.Show(NewuGlobal.GetRes("000037") + NewuGlobal.GetRes("000162"));
                return false;
            }
            return true;
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            label1.Text = NewuGlobal.GetRes("000503") + ":";
            label2.Text = NewuGlobal.GetRes("000504") + ":";
            label3.Text = NewuGlobal.GetRes("000505") + ":";
            label4.Text = NewuGlobal.GetRes("000506") + ":";
            label5.Text = NewuGlobal.GetRes("000507") + ":";

            hint.Text = NewuGlobal.GetRes("000170");
            btnAdd.Text = NewuGlobal.GetRes("000496");
            btnAddChild.Text = NewuGlobal.GetRes("000497");
            btnEdit.Text = NewuGlobal.GetRes("000101");
            btnDel.Text = NewuGlobal.GetRes("000102");
            btnClose.Text = NewuGlobal.GetRes("000103");
            treeListView1.Columns[0].Caption = NewuGlobal.GetRes("000503");
            treeListView1.Columns[1].Caption = NewuGlobal.GetRes("000504");
            treeListView1.Columns[2].Caption = NewuGlobal.GetRes("000507");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnAddChild.Size = btnAdd.Size = new Size(111, 30);
                btnAddChild.Location = new Point(1190, 27);
                btnEdit.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
                btnEdit.Location = new Point(1319, 27);
                btnDel.Location = new Point(1415, 27);
                btnClose.Location = new Point(1508, 27);
            }
            else
            {
                btnEdit.Padding = btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
                btnAdd.Size = new Size(140, 30);
                btnAddChild.Size = new Size(140, 30);
                btnAddChild.Location = new Point(155 + 1052, 27);
                btnEdit.Location = new Point(281 + 1072, 27);
                btnDel.Location = new Point(362 + 1072, 27);
                btnClose.Location = new Point(443 + 1072, 27);
            }
        }

        private void TreeListView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                hint.Text = NewuGlobal.GetRes("000170");
                CommonTools.Node focusedNode = treeListView1.FocusedNode;
                if (focusedNode != null)
                {
                    tB_Department = focusedNode.Tag as TB_Department;
                    txt_DepartmentName.Text = tB_Department.DepartmentName;
                    txt_DepartmentCode.Text = tB_Department.DepartmentCode;
                    txt_DepartmentJaneSpell.Text = tB_Department.DepartmentJaneSpell;
                    if (!string.IsNullOrEmpty(tB_Department.ParentDepartmentID))
                        cmb_ParentDepartmentID.SelectedValue = tB_Department.ParentDepartmentID;
                    else
                        cmb_ParentDepartmentID.SelectedIndex = -1;

                    txt_DepartmentRemark.Text = tB_Department.DepartmentRemark;
                }
            }
            catch (Exception ex)
            {

                NewuGlobal.LogCat("FM_TB_Department").Error(ex.ToString());
            }

        }
    }
}