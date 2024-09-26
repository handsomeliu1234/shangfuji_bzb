using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newu;


namespace NewuSys
{
    public partial class T : Form
    {
        NewuBLL.SYS_FactoryBLL FactoryBll = new NewuBLL.SYS_FactoryBLL();
        NewuModel.SYS_FactoryMDL FactoryMode = null;

        public T()
        {
            InitializeComponent();
       
        }

        private void T_Load(object sender, EventArgs e)
        {
            List<NewuModel.SYS_FactoryMDL> FactoryList = FactoryBll.GetModelList("");

            if (FactoryList.Count > 0)
            {
                FactoryMode = FactoryList[0];

                textBox1.Text = FactoryMode.FactoryName;
                textBox2.Text = FactoryMode.FactoryCode;
                textBox3.Text = FactoryMode.FactoryAddress;
                textBox4.Text = FactoryMode.FactoryPhone;


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

//        private void button1_Click(object sender, EventArgs e)
//        {
          
//               T_Edit fm = new T_Edit();
//                fm.Owner = this;
//                fm.ShowDialog();
            
//        }
    }
}
