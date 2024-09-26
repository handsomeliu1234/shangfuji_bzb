using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_CheckScale : Form
    {
        public FM_RPT_CheckScale()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 开始校秤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_RPT_CheckScale_Load(object sender, EventArgs e)
        {
            GetData();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetData()
        {

        }
    }
}
