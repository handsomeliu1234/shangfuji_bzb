using System.Runtime.InteropServices;

namespace NewuCommon
{
    /// <summary>
    /// 以毫秒计算起始时间，Guan 2017.05.14 Create
    /// </summary>
    public class CStopWatch
    {
        [DllImport("winmm.dll")]
        private static extern long timeGetTime();

        private long m_StartTime;

        public CStopWatch()
        {
            m_StartTime = timeGetTime();
        }

        /// <summary>
        /// 获取间隔时间，单位毫秒
        /// </summary>
        /// <returns></returns>
        public long Elapsed()
        {
            long r = timeGetTime() - m_StartTime;
            return r;
        }

        /// <summary>
        /// 重置开始时间
        /// </summary>
        public void Reset()
        {
            m_StartTime = timeGetTime();
        }

        /// <summary>
        /// 暂停时间
        /// </summary>
        /// <param name="millisecond"></param>
        public void Sleep(int millisecond)
        {
            long st = timeGetTime();

            while (timeGetTime() - st <= millisecond)
            {
                System.Threading.Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}