using MultiLanguage;
using Newu;
using Repository.GlobalConfig;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NewuSys
{
    public partial class FM_SYS_Menu_Add : Form
    {
        private SYS_Menu menuModel = new SYS_Menu();
        private readonly SYS_MenuRepository menuRepository = new SYS_MenuRepository();
        private readonly TB_RoleRepository roleRepository = new TB_RoleRepository();

        private string menuID = "";
        private string pathName = "";

        private CommonTools.Node FoucesNode
        {
            get; set;
        }

        private NodeLevel AddNodeLevel
        {
            get; set;
        }

        public enum NodeLevel
        {
            Top,
            Child
        }

        public FM_SYS_Menu_Add(CommonTools.Node node, NodeLevel level)
        {
            InitializeComponent();

            FoucesNode = node;
            AddNodeLevel = level;
        }

        public FM_SYS_Menu_Add(string _MenuID, NodeLevel level)
        {
            InitializeComponent();
            menuID = _MenuID;
            AddNodeLevel = level;
        }

        private void FM_SYS_Menu_Add_Load(object sender, EventArgs e)
        {
            SetToolTip();

            #region combobox初始化

            CreateTable CTable = new CreateTable();

            string[] cols = new string[] { "names", "values" };

            string[,] vals = new string[,] { { "菜单", "1" }, { "按钮", "0" } };

            cmb_ControlType.DataSource = CTable.GetTable(cols, vals);
            cmb_ControlType.DisplayMember = "names";
            cmb_ControlType.ValueMember = "values";

            vals = new string[,] { { "对话框", "1" }, { "最大化窗口", "0" }, { "正常化居中窗口", "2" } };
            cmb_ShowDialog.DataSource = CTable.GetTable(cols, vals);
            cmb_ShowDialog.DisplayMember = "names";
            cmb_ShowDialog.ValueMember = "values";

            vals = new string[,] {{"否", "0" },
                                  {"是", "1" }};

            cbAutoRunShow.DataSource = CTable.GetTable(cols, vals);
            cbAutoRunShow.DisplayMember = "names";
            cbAutoRunShow.ValueMember = "values";

            vals = new string[,] {{"否", "0" },
                                  {"是", "1" }};

            cmb_IsShow.DataSource = CTable.GetTable(cols, vals);
            cmb_IsShow.DisplayMember = "names";
            cmb_IsShow.ValueMember = "values";

            cmb_ParentMenuID.DataSource = menuRepository.GetList("");
            cmb_ParentMenuID.DisplayMember = "Caption";
            cmb_ParentMenuID.ValueMember = "MenuID";
            cmb_ParentMenuID.SelectedIndex = -1;

            List<TB_Role> tB_Roles = roleRepository.GetList("");
            tB_Roles.Add(new TB_Role { RoleID = "", RoleName = "" });
            cb_Role.DataSource = tB_Roles;
            cb_Role.DisplayMember = "RoleName";
            cb_Role.ValueMember = "RoleID";
            cb_Role.SelectedIndex = -1;

            #endregion combobox初始化

            splitContainer1.Panel1.BackColor = NewuColor.PanelBg;

            if (menuID != "")
            {
                menuModel = menuRepository.GetModel(menuID);
                txt_Caption.Text = menuModel.Caption;
                cmb_ControlType.SelectedValue = menuModel.ControlType;
                cmb_ParentMenuID.SelectedValue = menuModel.ParentMenuID;
                txt_ASSEMBLY.Text = menuModel.ASSEMBLY;
                txt_NameSpaceAndClass.Text = menuModel.NameSpaceAndClass;

                cmb_ShowDialog.SelectedValue = menuModel.ShowDialog.ToString();
                txt_ContainerForm.Text = menuModel.ContainerForm;
                txt_ControlName.Text = menuModel.ControlName;
                txt_ControlText.Text = menuModel.ControlText;
                txt_ToolTip.Text = menuModel.ToolTip;
                txt_MenuOrder.Text = menuModel.MenuOrder;
                tbResourceId.Text = menuModel.Reserve1;

                if (menuModel.Reserve3 != null)
                    cb_Role.SelectedValue = menuModel.Reserve3;
                if (menuModel.Reserve2 == null || menuModel.Reserve2 == "")
                {
                    cmb_IsShow.SelectedValue = "1";
                }
                else
                {
                    cmb_IsShow.SelectedValue = menuModel.Reserve2.ToString();
                }

                if (menuModel.AutoShow.ToString() == null)
                {
                    cbAutoRunShow.Text = "";
                }
                else
                {
                    cbAutoRunShow.SelectedValue = menuModel.AutoShow;
                }

                if (menuModel.MenuLogo != null)
                {
                    MemoryStream buf = new MemoryStream(menuModel.MenuLogo);
                    Image menuLogo = Image.FromStream(buf, true);
                    picMenuLogo.Image = menuLogo;
                    picMenuLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (!string.IsNullOrEmpty(menuModel.Reserve3))
                    cb_Role.SelectedValue = menuModel.Reserve3;
            }

            switch (AddNodeLevel)
            {
                case NodeLevel.Top:
                    cmb_ParentMenuID.SelectedIndex = -1;
                    break;

                case NodeLevel.Child:
                    if (FoucesNode != null)
                    {
                        cmb_ParentMenuID.SelectedValue = FoucesNode["MenuID"].ToString();
                    }
                    break;

                default:
                    break;
            }
            SetControlLanguageText();
        }



        private void SetControlLanguageText()
        {
            this.btnClose.Text = NewuGlobal.LanguagResourceManager.GetString("000103");     //关闭
            this.btnSave.Text = NewuGlobal.LanguagResourceManager.GetString("000108");      //保存
            this.Text = NewuGlobal.LanguagResourceManager.GetString("000015");

            this.btnMenuLogo.Text = NewuGlobal.LanguagResourceManager.GetString("000873");//选择图片
            this.btnDelMenuLogo.Text = NewuGlobal.LanguagResourceManager.GetString("000874");//删除图片

            this.groupBox2.Text = NewuGlobal.LanguagResourceManager.GetString("000872");   //菜单主要数据
            this.groupBox3.Text = NewuGlobal.LanguagResourceManager.GetString("000872");   //菜单详细数据

            this.label1.Text = NewuGlobal.LanguagResourceManager.GetString("000856") + ":";    //标题名称
            this.label2.Text = NewuGlobal.LanguagResourceManager.GetString("000857") + ":";    //控件类型
            this.label3.Text = NewuGlobal.LanguagResourceManager.GetString("000855") + ":";    //父菜单名称
            this.label4.Text = NewuGlobal.LanguagResourceManager.GetString("000868") + ":";    //显示排序
            this.label5.Text = NewuGlobal.LanguagResourceManager.GetString("000859") + ":";    //*不能为空
            this.label6.Text = NewuGlobal.LanguagResourceManager.GetString("000865") + ":";    //菜单logo
            this.label7.Text = NewuGlobal.LanguagResourceManager.GetString("000858") + ":";    //自动运行
            this.label8.Text = NewuGlobal.LanguagResourceManager.GetString("000870") + ":";    //资源ID
            this.label9.Text = NewuGlobal.LanguagResourceManager.GetString("000871") + ":";    //是否显示

            this.label17.Text = NewuGlobal.LanguagResourceManager.GetString("000860") + ":"; //控件名称
            this.label15.Text = NewuGlobal.LanguagResourceManager.GetString("000867") + ":"; //显示模式
            this.label11.Text = NewuGlobal.LanguagResourceManager.GetString("000861") + ":"; //程序集
            this.label10.Text = NewuGlobal.LanguagResourceManager.GetString("000862") + ":"; //命名空间
            this.label19.Text = NewuGlobal.LanguagResourceManager.GetString("000863") + ":"; //传递参数/功能描述
            this.label14.Text = NewuGlobal.LanguagResourceManager.GetString("000864") + ":"; //容器窗体
            this.label21.Text = NewuGlobal.LanguagResourceManager.GetString("000869") + ":"; //工具提示
            this.lb_Role.Text = NewuGlobal.LanguagResourceManager.GetString("000866") + ":";
        }

        private void SetToolTip()
        {
            toolTip1.SetToolTip(txt_ASSEMBLY, "程序集：Newu.SysSet");
            toolTip1.SetToolTip(txt_ControlName, "控件名称：btnSave 或 Newu.SysSet.FM_SYS_MENU");
            toolTip1.SetToolTip(txt_ControlText, "控件文本：btnSave的文本是\"保存\"，或者菜单功能描述文本");
            toolTip1.SetToolTip(txt_ToolTip, "当鼠标悬停时显示的文本");
            toolTip1.SetToolTip(txt_NameSpaceAndClass, "Form所在的命名控件+类型");
            toolTip1.SetToolTip(txt_ContainerForm, "Form的父及容器全名");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (menuModel == null)
                {
                    menuModel = new SYS_Menu();
                }
                menuModel.Caption = txt_Caption.Text.Trim();

                if (cmb_ControlType.SelectedValue != null)
                {
                    menuModel.ControlType = cmb_ControlType.SelectedValue.ToString();
                }
                else
                {
                    menuModel.ControlType = "";
                }

                menuModel.AutoShow = int.Parse(cbAutoRunShow.SelectedValue.ToString());

                if (cmb_IsShow.SelectedValue == null)
                {
                    menuModel.Reserve2 = "0";
                }
                else
                {
                    menuModel.Reserve2 = cmb_IsShow.SelectedValue.ToString();
                }

                if (cmb_ParentMenuID.SelectedValue != null)
                {
                    menuModel.ParentMenuID = cmb_ParentMenuID.SelectedValue.ToString();
                }
                else
                {
                    menuModel.ParentMenuID = "";
                }

                menuModel.ASSEMBLY = txt_ASSEMBLY.Text.Trim();
                menuModel.NameSpaceAndClass = txt_NameSpaceAndClass.Text.Trim();
                if (cmb_ShowDialog.SelectedValue != null)
                {
                    menuModel.ShowDialog = Convert.ToInt32(cmb_ShowDialog.SelectedValue.ToString());
                }

                menuModel.ContainerForm = txt_ContainerForm.Text.Trim();
                menuModel.ControlName = txt_ControlName.Text.Trim();
                menuModel.ControlText = txt_ControlText.Text.Trim();
                menuModel.ToolTip = txt_ToolTip.Text.Trim();
                menuModel.SaveTime = DateTime.Now;
                menuModel.MenuOrder = txt_MenuOrder.Text.Trim();
                if (pathName != "")
                {
                    FileStream fs = new FileStream(pathName, FileMode.Open, FileAccess.Read);
                    byte[] buffByte = new byte[fs.Length];
                    fs.Read(buffByte, 0, (int)fs.Length);
                    fs.Close();
                    menuModel.MenuLogo = buffByte;
                }
                if (picMenuLogo.Image == null)
                {
                    menuModel.MenuLogo = null;
                }

                menuModel.Reserve1 = tbResourceId.Text.Trim();
                if (cb_Role.SelectedValue == null)
                    menuModel.Reserve3 = "";
                else
                    menuModel.Reserve3 = cb_Role.SelectedValue.ToString();

                if (DataVerification() == false)
                {
                    return;
                }

                bool isAccess;
                if (string.IsNullOrEmpty(menuModel.MenuID))
                {
                    isAccess = menuRepository.Add(menuModel);
                }
                else
                {
                    isAccess = menuRepository.Update(menuModel);
                }

                if (isAccess == true)
                {
                    MessageBox.Show("菜单信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (string.IsNullOrEmpty(menuModel.MenuID))
                    {
                        ClearControl();
                    }
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("菜单信息保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SYS_Menu_Add").Error(ex.ToString());
            }
        }

        private void ClearControl()
        {
            txt_Caption.Text = "";
            cmb_ControlType.SelectedIndex = -1;
            txt_ASSEMBLY.Text = "";
            txt_NameSpaceAndClass.Text = "";
            cmb_ShowDialog.SelectedIndex = -1;
            cbAutoRunShow.SelectedIndex = -1;
            txt_ContainerForm.Text = "";
            txt_ControlName.Text = "";
            txt_ControlText.Text = "";
            txt_ToolTip.Text = "";
        }

        private void RefreshGrid()
        {
            object obj = this.Owner;

            if (obj != null)
            {
                FM_SYS_Menu fm = obj as FM_SYS_Menu;
                fm.GetData();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool DataVerification()
        {
            if (menuModel.Caption == "")
            {
                MessageBox.Show("控件名称 不能为空！");
                return false;
            }
            if (menuModel.ControlType == "")
            {
                MessageBox.Show("控件类型 不能为空！");
                return false;
            }

            return true;
        }

        private void BtnMenuLogo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openfiledlg = new OpenFileDialog
                {
                    CheckPathExists = true
                };
                if (openfiledlg.ShowDialog() == DialogResult.OK)
                {
                    pathName = openfiledlg.FileName;
                    Image img = Image.FromFile(pathName);
                    picMenuLogo.Image = img;
                    picMenuLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("内存不足"))
                {
                    MessageBox.Show("请选择图片格式进行上传");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BtnDelMenuLogo_Click(object sender, EventArgs e)
        {
            pathName = "";
            picMenuLogo.Image = null;
        }
    }
}