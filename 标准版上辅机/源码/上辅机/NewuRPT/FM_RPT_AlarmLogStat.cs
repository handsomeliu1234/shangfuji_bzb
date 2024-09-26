using NewuCommon;
using Repository.GlobalConfig;
using Repository.Repository;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewuRPT
{
    public partial class FM_RPT_AlarmLogStat : Form
    {
        public FM_RPT_AlarmLogStat()
        {
            InitializeComponent();
        }

        public FM_RPT_AlarmLogStat(string dtStart, string dtEnd)
        {
            InitializeComponent();
            ColStruct[] cols = new ColStruct[]{
                new ColStruct("AlarmInfo","报警信息"),
                new ColStruct("AlarmCount","报警次数"),
            };
            dgvAlarmLogStat.AddCols(cols);
            dgvAlarmLogStat.AllowUserToAddRows = false;
            SetLanguage();
            InitData(dtStart, dtEnd);
        }

        private void SetLanguage()
        {
            Text = NewuGlobal.GetRes("000366");
            dgvAlarmLogStat.Columns[0].HeaderText = NewuGlobal.GetRes("000365");
            dgvAlarmLogStat.Columns[1].HeaderText = NewuGlobal.GetRes("000377");
        }

        private void InitData(string dtStart, string dtEnd)
        {
            RPT_AlarmlogRepository alarmLog = new RPT_AlarmlogRepository();
            List<AlarmStates> alarmStates = alarmLog.GetAlarmLogStat(dtStart, dtEnd);
            if (alarmStates.Count > 0)
            {
                dgvAlarmLogStat.DataSource = alarmStates;
            }
            else
            {
                return;
            }
        }
    }
}