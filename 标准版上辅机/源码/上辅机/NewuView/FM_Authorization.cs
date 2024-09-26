using Repository.GlobalConfig;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace NewuView
{
    public partial class FM_Authorization : Form
    {
        public FM_Authorization()
        {
            InitializeComponent();
        }

        private void FM_Authorization_Load(object sender, EventArgs e)
        {
            SetControlTextLanguage();
            intput1.Text = GenerateRandomNumber(5);
            intput2.Text = GenerateRandomNumber2(5);
        }


        private void SetControlTextLanguage()
        {
            label2.Text = NewuGlobal.GetRes("000846");
            label1.Text = NewuGlobal.GetRes("000847");
            label4.Text = NewuGlobal.GetRes("000848");
            label5.Text = NewuGlobal.GetRes("000170") + ":" + NewuGlobal.GetRes("000849");

            button1.Text = NewuGlobal.GetRes("000850");
        }

        #region

        private static char[] constant =
      {
        '0','1','2','3','4','5','6','7','8','9',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };

        public static string GenerateRandomNumber(int Length)
        {
            StringBuilder newRandom = new StringBuilder(36);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(36)]);
            }
            return newRandom.ToString();
        }

        private static char[] constant2 =
      {
        'A','B','C','D','E','0','1','2','3','4','F','G','H','I','J','K','L','M','5','6','7','8','9','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
      };

        public static string GenerateRandomNumber2(int Length)
        {
            StringBuilder newRandom = new StringBuilder(36);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant2[rd.Next(36)]);
            }
            return newRandom.ToString();
        }

        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            byte[] result = Encoding.Default.GetBytes(this.intput1.Text.Trim() + this.intput2.Text.Trim());    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string ans = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
            // 判定该授权码是否 合格
            string ans30 = "";
            string ansForEver = "";
            for (int i = 0; i < ans.Length; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    ansForEver += ans[i];
                }
                if ((i + 1) % 4 == 0)
                {
                    ans30 += ans[i];
                }
            }
            if (textBox1.Text == ans30)
            {
                Authorization.UpDataParam(30);
                MessageBox.Show("验证成功，感谢您支持正版,本次有效期一个月！");
                this.Close();
            }
            else if (textBox1.Text == ansForEver)
            {
                Authorization.UpDataParam(36500);
                MessageBox.Show("验证成功，感谢您支持正版,有效期永久！");
                this.Close();
            }
            else
            {
                MessageBox.Show("授权码有误，请仔细核对检查！");
            }
        }
    }
}