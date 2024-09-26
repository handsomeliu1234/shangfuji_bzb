using NewuCommon;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;

namespace Repository.GlobalConfig
{
    public class SaveAlarmUtil
    {
        // 使用一个新线程 监控报警信息
        /**
         *  1.查询 TB_Alarm表 中数据
         *  2. 循环遍历  监控点
         *  3. flag 标记变量走起
         *  4. 插入数据库数据
         *  5. 生成String  显示在四个界面  母炼主辅监控  终炼主监控  胶料界面
         *  6. 信号点消失  变化标记  减少信息提示 ，  记录解除报警信息
         */

        public event Action<string> MonitorAlarm;

        private readonly TB_AlarmRepository alarmRepository = new TB_AlarmRepository();
        private readonly RPT_AlarmlogRepository alarmlogRepository = new RPT_AlarmlogRepository();
        private List<TB_Alarm> tB_Alarms;
        private string alarmString = "";
        private int allNum = 0;
        private static SaveAlarmUtil monitorAlarm;

        private bool[] flag = null;

        private System.Threading.Timer timer1;

        private SaveAlarmUtil()
        {
            tB_Alarms = alarmRepository.GetModelList(" IsDisplay = 1 ");
            flag = new bool[tB_Alarms.Count];
            timer1 = new System.Threading.Timer(ThreadCallback, null, 1000, 1000);
            PrepareData(true);
        }

        public static SaveAlarmUtil GetInstance()
        {
            if (monitorAlarm == null)
            {
                monitorAlarm = new SaveAlarmUtil();
            }
            return monitorAlarm;
        }

        /// <summary>
        /// 更新报警信息使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadCallback(object sender)
        {
            if (NewuGlobal.MemDB.Getbool(AddressConst.UpDateAlarm))
            {
                tB_Alarms = null;
                NewuGlobal.MemDB.SetStr(AddressConst.UpDateAlarm, "0");
            }
            MoniAlarm();
        }

        public void MoniAlarm()
        {
            if (tB_Alarms == null)
            {
                tB_Alarms = alarmRepository.GetModelList(" IsDisplay = 1 ");
                flag = new bool[tB_Alarms.Count];
            }

            bool alarmChange = false;
            int cnt = 0;
            foreach (TB_Alarm item in tB_Alarms)
            {
                //todo: xxx为暂时未定义  顾不记录
                if (item.AlarmInfo == "xxx" || item.IsDisplay == 0)
                {
                    cnt++;
                    continue;
                }

                if (NewuGlobal.MemDB.Getbool(item.MemoryAddr) && !flag[cnt])
                {
                    //第一次get到该报警点
                    flag[cnt] = true;

                    //插入生成报警记录
                    alarmlogRepository.SaveAlarmLogMix(item, true);

                    alarmChange = true;
                }

                if (!NewuGlobal.MemDB.Getbool(item.MemoryAddr) && flag[cnt])
                {
                    flag[cnt] = false;
                    //插入结束报警记录
                    alarmlogRepository.SaveAlarmLogMix(item, false);
                    alarmChange = true;
                }
                cnt++;
            }
            allNum = cnt;
            PrepareData(alarmChange);
        }

        //准备报警数据  向主界面报警条展示
        // 切换数据的时候 也调用该方法
        public void PrepareData(bool alarmChange)
        {
            if (alarmChange)
            {
                alarmString = "";
                for (int i = 0; i < allNum; i++)
                {
                    if (flag[i])
                    {
                        if (NewuGlobal.SoftConfig.Language.Equals("English"))   // < !--English-- >  < !--Chinese-- >
                            alarmString += tB_Alarms[i].AlarmInfo + " ";  //定下来 Res5 存放英文报警
                        else
                            alarmString += tB_Alarms[i].AlarmInfo + "_En ";
                    }
                }
                if (!string.IsNullOrEmpty(alarmString))
                {
                    NewuGlobal.AlarmInfo = alarmString;
                }
                else
                {
                    NewuGlobal.AlarmInfo = NewuGlobal.GetRes("000714");
                }
                MonitorAlarm?.Invoke(NewuGlobal.AlarmInfo);
            }
        }
    }
}