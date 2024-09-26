using MultiLanguage;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_Menu : Form, ILanguageChanged
    {
        private readonly SYS_MenuRepository menuRepository = new SYS_MenuRepository();
        private readonly TB_PrivilegeRepository privilegeRepository = new TB_PrivilegeRepository();

        public FM_SYS_Menu()
        {
            InitializeComponent();
        }

        private void FM_SYS_Menu_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            SetLanguage();
          
        }

        private void SetLanguage()
        {
            btnAdd.Text = NewuGlobal.LanguagResourceManager.GetString("000060");
            btnAddChild.Text = NewuGlobal.LanguagResourceManager.GetString("000061");
            btnEdit.Text = NewuGlobal.LanguagResourceManager.GetString("000101");
            btnDel.Text = NewuGlobal.LanguagResourceManager.GetString("000102");
            btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");

            treeListView1.Columns[0].Caption = NewuGlobal.LanguagResourceManager.GetString("000737");
            treeListView1.Columns[1].Caption = NewuGlobal.LanguagResourceManager.GetString("000738");
            treeListView1.Columns[2].Caption = NewuGlobal.LanguagResourceManager.GetString("000739");
            treeListView1.Columns[3].Caption = NewuGlobal.LanguagResourceManager.GetString("000740");
            treeListView1.Columns[5].Caption = NewuGlobal.LanguagResourceManager.GetString("000741");
            treeListView1.Columns[6].Caption = NewuGlobal.LanguagResourceManager.GetString("000742");
            treeListView1.Columns[7].Caption = NewuGlobal.LanguagResourceManager.GetString("000743");
            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                btnAddChild.Size = new Size(111, 30);
                btnEdit.Location = new Point(285, 8);
                btnDel.Location = new Point(380, 8);
                btnClose.Location = new Point(475, 8);
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 7, 0);
            }
            else
            {
                btnAddChild.Size = new Size(145, 30);
                btnEdit.Location = new Point(305, 8);
                btnDel.Location = new Point(395, 8);
                btnClose.Location = new Point(485, 8);
                btnDel.Padding = btnClose.Padding = new Padding(0, 0, 0, 0);
            }

            CommonTools.Node node = treeListView1.FocusedNode;
            int index = -1;
            if (node != null)
            {
                index = GetTopNode(node).NodeIndex;
            }
            List<SYS_Menu> sYS_Menus = menuRepository.GetList(0, "", "menuOrder");
            treeListView1.Nodes.Clear();

            if (sYS_Menus != null)
            {
                treeListView1.BeginUpdate();
                LoaderMen(null, "", sYS_Menus);   //载入菜单项，添加到treeList
                treeListView1.EndUpdate();
                if (index > -1)
                {
                    treeListView1.Nodes[index].ExpandAll();
                }
            }
        }

        private CommonTools.Node GetTopNode(CommonTools.Node p)
        {
            if (p.Parent != null)
            {
                return GetTopNode(p.Parent);
            }
            else
            {
                return p;
            }
        }

        private void LoaderMen(CommonTools.Node node, string pid, List<SYS_Menu> list)
        {
            try
            {
                foreach (var item in list)
                {
                    if (item.ParentMenuID.Equals(pid))
                    {
                        object[] obj = new object[8];
                        if (!string.IsNullOrEmpty(item.Reserve1))
                        {
                            //item.Text 看情况使用
                            item.Caption = NewuGlobal.LanguagResourceManager.GetString(item.Reserve1);
                        }
                        obj[0] = item.Caption;
                        obj[1] = item.ControlType == "1" ? NewuGlobal.LanguagResourceManager.GetString("000094") : NewuGlobal.LanguagResourceManager.GetString("000095");
                        obj[2] = item.ShowDialog == 1 ? NewuGlobal.LanguagResourceManager.GetString("000096") : NewuGlobal.LanguagResourceManager.GetString("000097");
                        obj[3] = item.ControlText;
                        obj[4] = item.MenuID;
                        if (item.AutoShow == 0)
                        {
                            obj[5] = NewuGlobal.LanguagResourceManager.GetString("000107"); //否
                        }
                        else
                        {
                            //obj[5] = "是";
                            obj[5] = NewuGlobal.LanguagResourceManager.GetString("000106"); //是
                        }
                        obj[6] = item.MenuOrder;
                        obj[7] = item.Reserve1;
                        CommonTools.Node P = new CommonTools.Node(obj)
                        {
                            Tag = item
                        };
                        if (string.IsNullOrEmpty(pid))
                        {
                            treeListView1.Nodes.Add(P);
                            LoaderMen(P, item.MenuID, list);
                        }
                        else
                        {
                            node.Nodes.Add(P);
                            LoaderMen(P, item.MenuID, list);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Menu").Error(ex.ToString());
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CommonTools.Node node = null;
                FM_SYS_Menu_Add fm = new FM_SYS_Menu_Add(node, FM_SYS_Menu_Add.NodeLevel.Top)
                {
                    Owner = this
                };
                fm.ShowDialog();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Menu").Error(ex.ToString());
            }
        }

        private void BtnAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                CommonTools.Node node = treeListView1.FocusedNode;
                if (node == null)
                {
                    MessageBox.Show("请选择有效的节点！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FM_SYS_Menu_Add fm = new FM_SYS_Menu_Add(treeListView1.FocusedNode, FM_SYS_Menu_Add.NodeLevel.Child)
                {
                    Owner = this
                };
                fm.ShowDialog();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Menu").Error(ex.ToString());
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                CommonTools.Node node = treeListView1.FocusedNode;

                if (node != null)
                {
                    //根据menuID查询到ParentID判断是子节点还是父节点
                    SYS_Menu sYS_Menu = menuRepository.GetModel(node["MenuID"].ToString());
                    FM_SYS_Menu_Add fm;
                    if (!string.IsNullOrEmpty(sYS_Menu.ParentMenuID))
                    {
                        fm = new FM_SYS_Menu_Add(node["MenuID"].ToString(), FM_SYS_Menu_Add.NodeLevel.Child)
                        {
                            Owner = this
                        };
                    }
                    else
                    {
                        fm = new FM_SYS_Menu_Add(node["MenuID"].ToString(), FM_SYS_Menu_Add.NodeLevel.Top)
                        {
                            Owner = this
                        };
                    }

                    fm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Menu").Error(ex.ToString());
            }
        }

        private void GetParentID(CommonTools.Node node, List<string> list)
        {
            CommonTools.NodeCollection collectNode = node.Nodes;

            for (int i = 0; i < collectNode.Count; i++)
            {
                CommonTools.Node tempNode = collectNode[i];
                list.Add(tempNode["MenuID"].ToString());

                GetParentID(tempNode, list);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetLanguage();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonTools.Node node = treeListView1.FocusedNode;
                List<string> listID = new List<string>();  //MenuID列表
                string nodeCaption;
                if (node != null)
                {
                    nodeCaption = node["Caption"].ToString();
                    listID.Add(node["MenuID"].ToString());
                    GetParentID(node, listID);
                    treeListView1.FocusedNode = null;
                }
                else
                {
                    MessageBox.Show("请选择要删除的节点！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult diaResult = MessageBox.Show("确认删除节点[ " + nodeCaption + " ] 及其子节点吗？", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (diaResult == DialogResult.Cancel)
                    return;

                string delStr = "";
                for (int i = 0; i < listID.Count; i++)
                {
                    delStr += "'" + listID[i] + "',";
                }
                if (delStr != "")
                {
                    delStr = delStr.Substring(0, delStr.Length - 1); //MenuID
                    ///先删除权限管理中有相关权限人员的权限(存在问题:没有打开分配权限保存一下导致整个删除失败)
                    ///先做一个查询:看看是否打开过权限界面分配权限,根据返回结果进行对应操作
                    bool IsExist;
                    bool isDel = false;
                    List<TB_Privilege> tB_Privileges = privilegeRepository.GetModelList("MenuID in (" + delStr + ")");
                    if (tB_Privileges != null && tB_Privileges.Count > 0)
                        IsExist = privilegeRepository.DeleteList(delStr);
                    else
                        IsExist = true;

                    if (IsExist)
                    {
                        isDel = menuRepository.DeleteList(delStr);
                    }

                    if (isDel)
                    {
                        MessageBox.Show("节点 [" + nodeCaption + " ]及其子节点删除成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("节点 [" + nodeCaption + " ]删除失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Menu").Error(ex.ToString());
            }
        }
    }
}