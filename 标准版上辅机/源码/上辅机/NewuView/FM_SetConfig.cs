using MultiLanguage;
using Repository.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using WindowsCaptureVideo;

namespace NewuView
{
    public partial class FM_SetConfig : Form, ILanguageChanged
    {
        private CaptureVideoFFpeng captureVideo;

        public FM_SetConfig()
        {
            InitializeComponent();
            InitView();
        }

        private void FM_SetConfig_Load(object sender, EventArgs e)
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
            this.Text = NewuGlobal.GetRes("");

            this.groupBox1.Text = NewuGlobal.GetRes("000053");
            this.groupBox2.Text = NewuGlobal.GetRes("000799");//扫描枪设置

            this.labPLCIP.Text = "PLC IP:";
            this.label3.Text = NewuGlobal.GetRes("000056") + ":";

            this.label1.Text = NewuGlobal.GetRes("000057") + NewuGlobal.GetRes("000808") + ":";
            this.label8.Text = NewuGlobal.GetRes("000058") + NewuGlobal.GetRes("000808") + ":";
            this.label4.Text = NewuGlobal.GetRes("000059") + NewuGlobal.GetRes("000808") + ":";
            this.label2.Text = NewuGlobal.GetRes("000075") + NewuGlobal.GetRes("000808") + ":";
            this.label5.Text = NewuGlobal.GetRes("000736") + ":";
            this.label6.Text = NewuGlobal.GetRes("000062") + NewuGlobal.GetRes("000808") + ":";
            this.label7.Text = NewuGlobal.GetRes("000526") + ":";

            this.btnAlone.Text = NewuGlobal.GetRes("000797"); //单机版
            this.btnOnline.Text = NewuGlobal.GetRes("000798"); //网络版
            this.btnSvaeVersion.Text = NewuGlobal.GetRes("000108"); //保存
            this.button2.Text = NewuGlobal.GetRes("000103");       //关闭
            this.tabPage1.Text = NewuGlobal.GetRes("000799");
            this.tabPage2.Text = NewuGlobal.GetRes("000800"); //版本设置

            this.btnRewrite.Text = NewuGlobal.GetRes("000876"); //强制重写
            this.btnRewriteClose.Text = NewuGlobal.GetRes("000103");     //关闭
            this.label9.Text = NewuGlobal.GetRes("000877"); //tips
            this.tabPage3.Text = NewuGlobal.GetRes("000875");  //历史回放重写
        }

        private void InitView()
        {
            cbLocalIP.DropDownStyle = ComboBoxStyle.DropDownList;
            tbPLCIP.Text = NewuGlobal.SoftConfig.PLC_IP;
            List<string> ipArray = GetLocalIPAddress();
            if (!ipArray.Contains(NewuGlobal.SoftConfig.PCIP))
            {
                ipArray.Add(NewuGlobal.SoftConfig.PCIP);
            }
            cbLocalIP.DataSource = ipArray;
            cbLocalIP.Text = NewuGlobal.SoftConfig.PCIP;
            cbAutoCheck.Checked = NewuGlobal.SoftConfig.AutoScaleCheck;

            cbRubScanner.Checked = NewuGlobal.SoftConfig.RubScanner;
            cbDrugScanner.Checked = NewuGlobal.SoftConfig.DrugScanner;
            cbOilScanner.Checked = NewuGlobal.SoftConfig.OilScanner;
            cbZnOScanner.Checked = NewuGlobal.SoftConfig.ZnOScanner;
            cb_formularContinue.Checked = NewuGlobal.SoftConfig.IsContinue;
            cbCarbonScanner.Checked = NewuGlobal.SoftConfig.CarBonScanner;
            if (NewuGlobal.SoftConfig.VersionID == "1")
            {
                btnAlone.Checked = true;
                btnOnline.Checked = false;
            }
            else
            {
                btnAlone.Checked = false;
                btnOnline.Checked = true;
            }
        }

        private void BtClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            LoadConfig softConfig = new LoadConfig();
            try
            {
                if (NewuCommon.FunClass.IsIPAddress(tbPLCIP.Text.Trim()))
                {
                    softConfig.SetPlcIP(tbPLCIP.Text.Trim());
                }
                if (NewuCommon.FunClass.IsIPAddress(cbLocalIP.Text.Trim()))
                {
                    softConfig.SetPCIP(cbLocalIP.Text.Trim());
                }

                NewuGlobal.SoftConfig.SetAutoScaleCheck(cbAutoCheck.Checked);

                bool flag = false;

                softConfig.SetScannerState(cbRubScanner.Checked, "RubUSE");
                if (cbRubScanner.Checked)
                {  //注意通讯程序中必须配置为1位  此处写1 因为配置四位的话读前两位有可能读到00  lzc测试配置四位有时会是0.
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.RubberEnable, "0001");
                }
                else
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.RubberEnable, "0000");
                }

                softConfig.SetScannerState(cbDrugScanner.Checked, "DrugUSE");
                if (cbDrugScanner.Checked)
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.DrugEnable, "0001");
                }
                else
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.DrugEnable, "0000");
                }

                softConfig.SetScannerState(cbOilScanner.Checked, "OilUSE");
                if (cbOilScanner.Checked)
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.OilEnable, "0001");
                }
                else
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.OilEnable, "0000");
                }

                softConfig.SetScannerState(cbZnOScanner.Checked, "ZnoUSE");
                if (cbZnOScanner.Checked)
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.ZnoEnable, "0001");
                }
                else
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.ZnoEnable, "0000");
                }

                softConfig.SetScannerState(cbCarbonScanner.Checked, "CarbonUSE");
                if (cbCarbonScanner.Checked)
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.CarbonEnable, "0001");
                }
                else
                {
                    flag = NewuGlobal.MemMgr.Sync((int)MixerDigitalMiningScanner.CarbonEnable, "0000");
                }

                softConfig.SetIsContinue(cb_formularContinue.Checked);
                if (cb_formularContinue.Checked)
                    ContinuedOrder.GetInstance().StartContinueOrder();
                else
                    ContinuedOrder.GetInstance().StopContinueOrder();

                NewuGlobal.SoftConfig = new LoadConfig();
                MessageBox.Show(NewuGlobal.GetRes("000171"));//保存成功
            }
            catch (Exception ex)
            {
                MessageBox.Show(NewuGlobal.GetRes("000172"));//保存失败
                NewuGlobal.LogCat("FM_SetConfig").Error(ex.ToString());
            }
        }

        private List<string> GetLocalIPAddress()
        {
            try
            {
                List<string> IPArray = new List<string>();
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();//获取本机所有网卡对象
                foreach (NetworkInterface adapter in adapters)
                {
                    if (adapter.Description.Contains("PCI") || adapter.Description.Contains("Wireless"))//枚举条件：描述中包含"PCI"
                    {
                        IPInterfaceProperties ipProperties = adapter.GetIPProperties();//获取IP配置
                        UnicastIPAddressInformationCollection ipCollection = ipProperties.UnicastAddresses;//获取单播地址集
                        foreach (UnicastIPAddressInformation ip in ipCollection)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)//只要ipv4的
                            {
                                IPArray.Add(ip.Address.ToString());
                            }
                        }
                    }
                }
                return IPArray;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SetConfig").Error(ex.ToString());
                return null;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSaveVersion_Click(object sender, EventArgs e)
        {
            if (btnAlone.Checked == true)
            {
                NewuGlobal.VersionFlag = 1;
                NewuGlobal.SoftConfig.VersionID = "1";
            }
            else
            {
                NewuGlobal.VersionFlag = 2;
                NewuGlobal.SoftConfig.VersionID = "2";
            }

            //更新主界面机台版本显示
            IVersionSet fMain = NewuGlobal.FmMain as IVersionSet;
            if (fMain != null)
            {
                fMain.VersionSet();
            }
            MessageBox.Show(NewuGlobal.GetRes("000171"));
            this.Close();
        }

        private void btnRewriteClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //todo:20240718 增加历史回放重写功能
        private void btnRewrite_Click(object sender, EventArgs e)
        {
            try
            {
                NewuGlobal.CaptureVideo.ReStart();
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FM_SetConfig").Error(ex.ToString());
            }
        }
    }
}