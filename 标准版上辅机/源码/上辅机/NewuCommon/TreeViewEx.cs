using System;
using System.Collections;
using System.Windows.Forms;

namespace NewuCommon
{
    public class TreeViewEx : TreeView
    {
        #region CheckBox选择功能

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (this.CheckBoxes == true)
            {
                this.CheckChildren(e.Node);
                this.CheckParent(e.Node);
                this.CheckPingJi(e.Node);
            }
        }

        private void CheckChildren(TreeNode node)
        {
            bool isCheck = node.Checked;

            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    n.Checked = isCheck;
                    this.CheckChildren(n);
                }
            }
        }

        private void CheckParent(TreeNode node)
        {
            try
            {
                if (node.Checked == true)
                {
                    if (node.Parent != null)
                    {
                        TreeNode _node = node.Parent;
                        _node.Checked = true;
                        CheckParent(_node);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void CheckPingJi(TreeNode node)
        {
            try
            {
                if (node.Checked == false)
                {
                    if (node.Parent != null)
                    {
                        bool isAllUneck = false;
                        TreeNode parent = node.Parent;

                        TreeNodeCollection _nodes = parent.Nodes;

                        foreach (TreeNode child in _nodes)
                        {
                            if (child.Checked == true)
                            {
                                isAllUneck = true;
                                break;
                            }
                        }
                        if (isAllUneck == false)
                        {
                            //parent.Checked = false;
                            CheckPingJi(parent);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion CheckBox选择功能

        #region 获取节点名称和Check的状态列表

        public struct NodeCheck
        {
            public NodeCheck(bool _ischeck, string _nodename)
            {
                IsCheck = _ischeck;
                NodeName = _nodename;
            }

            public bool IsCheck;
            public string NodeName;
        }

        public ArrayList GetNodeNameArr()
        {
            ArrayList arr = new ArrayList();

            foreach (TreeNode item in this.Nodes)
            {
                NodeCheck _nodeCheck = new NodeCheck
                {
                    NodeName = item.Name,
                    IsCheck = item.Checked
                };

                arr.Add(_nodeCheck);
                GetChildChecked(item, arr);
            }
            return arr;
        }

        private void GetChildChecked(TreeNode node, ArrayList arr)
        {
            foreach (TreeNode item in node.Nodes)
            {
                NodeCheck _nodeCheck = new NodeCheck
                {
                    NodeName = item.Name,
                    IsCheck = item.Checked
                };

                arr.Add(_nodeCheck);
                GetChildChecked(item, arr);
            }
        }

        #endregion 获取节点名称和Check的状态列表

        #region 查找节点

        private TreeNode FindChildNode(TreeNode tnParent, string nodeName)
        {
            if (tnParent == null)
                return null;
            if (tnParent.Name == nodeName)
                return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                if (tn.Name == nodeName)
                {
                    tnRet = tn;
                    break;
                }
                else
                {
                    tnRet = FindChildNode(tn, nodeName);
                    if (tnRet != null)
                        break;
                }
            }
            return tnRet;
        }

        public TreeNode FindNode(string nodeName)
        {
            TreeNode tnRet = null;
            foreach (TreeNode tn in this.Nodes)
            {
                if (tn.Name == nodeName)
                {
                    tnRet = tn;
                    break;
                }
                else
                {
                    tnRet = FindChildNode(tn, nodeName);
                    if (tnRet != null)
                        break;
                }
            }

            return tnRet;
        }

        #endregion 查找节点

        #region 清空Check

        private void ForEachChildNode(TreeNode tnParent)
        {
            if (tnParent == null)
                return;
            tnParent.Checked = false;

            foreach (TreeNode tn in tnParent.Nodes)
            {
                tn.Checked = false;
                ForEachChildNode(tn);
            }
        }

        public void ClearCheck()
        {
            foreach (TreeNode tn in this.Nodes)
            {
                tn.Checked = false;
                ForEachChildNode(tn);
            }
        }

        #endregion 清空Check
    }
}