using Repository.GlobalConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewuView
{
    public partial class FM_Monitor_Alarm : Form
    {
        public List<NewuModel.TB_AlarmMDL> alarmList = new List<NewuModel.TB_AlarmMDL>();

        private Timer timer1 = new Timer();
        private NewuCommon.CSharedString SS = NewuGlobal.MemDB;

        public FM_Monitor_Alarm()
        {
            InitializeComponent();
        }

        private void FM_Monitor_Alarm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string alarmMsg = "";
            foreach (NewuModel.TB_AlarmMDL item in alarmList)
            {
                if (SS.getbool(item.MemoryAddr))
                {
                    alarmMsg += DateTime.Now.ToString("HH:mm:ss") + "：" + item.AlarmInfo + "\r\n";
                }
            }
            txtAlarmMsg.Text = alarmMsg;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}