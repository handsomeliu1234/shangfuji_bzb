using MultiLanguage;
using Repository.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NewuView
{
    public partial class FM_InternalSoftwarSettings : Form, ILanguageChanged
    {
        public FM_InternalSoftwarSettings()
        {
            InitializeComponent();
            InitView();
        }

        private void FM_InternalSoftwarSettings_Load(object sender, EventArgs e)
        {
            SetControlLanguageText();
        }

        public void LanguageChanged(SupportLanguageType language)
        {
            SetControlLanguageText();
        }

        private void SetControlLanguageText()
        {
            this.btClose.Text = NewuGlobal.GetRes("000103");     //关闭
            this.btSave.Text = NewuGlobal.GetRes("000108");     //保存
            this.tabPage1.Text = NewuGlobal.GetRes("000837");            //软件版本设置
            this.label1.Text = NewuGlobal.GetRes("000835") + ":";    //软件版本设置
            this.label2.Text = NewuGlobal.GetRes("000836") + ":";    //Svn版本
            this.tabPage2.Text = NewuGlobal.GetRes("000823");        //数据库清理配置
            this.label4.Text = NewuGlobal.GetRes("000188") + ":";    //是否启用
            this.label3.Text = NewuGlobal.GetRes("000822") + ":";    //删除年数
            this.btnDataBaseConfig.Text= NewuGlobal.GetRes("000108");  //保存
            this.button2.Text= NewuGlobal.GetRes("000103");     //保存
            this.label5.Text= NewuGlobal.GetRes("000838");      //盘符
            this.tabPage3.Text = NewuGlobal.GetRes("000839");        //导出文件盘符
            this.Text = NewuGlobal.GetRes("000840");
        }

        private void InitView()
        {
            try
            {
                txtSoftwareVer.Text = GetMiddleValue(NewuGlobal.SoftConfig.SoftwareVersion, "V", "-");
                txtSvnVer.Text = NewuGlobal.SoftConfig.SoftwareVersion.Split('n')[1];
                cbEnable.Checked = NewuGlobal.SoftConfig.DBCleanEnable;
                txtYears.Text = NewuGlobal.SoftConfig.DBCleanYear.ToString();
                txtDisk.Text = NewuGlobal.SoftConfig.ExportPath;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_InternalSoftwarSettings").Error(ex.ToString());
            }
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtSvnVer.Text)) &&(!string.IsNullOrEmpty(txtSoftwareVer.Text)))
                {
                    NewuGlobal.SoftConfig.SoftwareVersion = "V" + txtSoftwareVer.Text + "-Svn" + txtSvnVer.Text;
                }
                else
                {
                    MessageBox.Show(NewuGlobal.GetRes("000162"));    //输入的东西不能为空
                    return;
                }
                MessageBox.Show(NewuGlobal.GetRes("000171"));//保存成功
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"));//保存失败
                NewuGlobal.LogCat("FM_InternalSoftwarSettings").Error(ex.ToString());
            }
        }

        public string GetMiddleValue(string str,string sta,string end)
        {
            Regex rg = new Regex("(?<=(" + sta + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }

        private void btnDataBaseConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtYears.Text))
                {
                    NewuGlobal.SoftConfig.DBCleanEnable = cbEnable.Checked;
                    NewuGlobal.SoftConfig.DBCleanYear = int.Parse(txtYears.Text);
                }
                else
                {
                    MessageBox.Show(NewuGlobal.GetRes("000162"));    //保留年数为空
                    return;
                }
                MessageBox.Show(NewuGlobal.GetRes("000171"));//保存成功
            }
            catch (Exception ex)
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"));//保存失败
                NewuGlobal.LogCat("FM_InternalSoftwarSettings").Error(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void checkTxtIsDigit(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        public void checkTxtIsLetter(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !char.IsLetter(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void btnExportDisk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDisk.Text.Length == 1)
                {
                    NewuGlobal.SoftConfig.ExportPath = txtDisk.Text;
                    MessageBox.Show(NewuGlobal.GetRes("000171"));//保存成功
                }
                else
                {
                    MessageBox.Show(NewuGlobal.GetRes("000841"));   //只能输入一个字符
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"));//保存失败
                NewuGlobal.LogCat("FM_InternalSoftwarSettings").Error(ex.ToString());
            }
            
        }

        private void txtYears_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkTxtIsDigit(e);
        }

        private void txtSvnVer_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkTxtIsDigit(e);
        }

        private void txtDisk_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkTxtIsLetter(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}