using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DBUtility;
using NewuBLL;
namespace NewuReport
{
    public partial class FM_DataCompare : Form
    {
        DbHelperSQL sqlClass = new DbHelperSQL(ConnType.NewuAutomation);
        private List<DataD> list = new List<DataD>();
        string[] type = new string[] { "日产量", "月产量", "月同比" };

        public FM_DataCompare()
        {
            InitializeComponent();


        }
        // 获取生产数据
        private void button1_Click(object sender, EventArgs e)
        {

           
        }


        private void FM_DataCompare_Load(object sender, EventArgs e)
        {
            InitChart();
        }
       
        
        }

    }
    class DataD
    {
        public int Count { get; set; }
        public DateTime dt { get; set; }
    }
}
