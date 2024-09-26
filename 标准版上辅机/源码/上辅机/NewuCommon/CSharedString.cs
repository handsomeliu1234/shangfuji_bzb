using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NewuCommon
{
    public class CSharedString
    {
        [DllImport("winmm.dll")]
        private static extern long timeGetTime();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern int GetLastError();

        private const int ERROR_ALREADY_EXISTS = 183;

        private const int FILE_MAP_COPY = 0x0001;
        private const int FILE_MAP_WRITE = 0x0002;
        private const int FILE_MAP_READ = 0x0004;
        private const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;

        private const int PAGE_READONLY = 0x02;
        private const int PAGE_READWRITE = 0x04;
        private const int PAGE_WRITECOPY = 0x08;
        private const int PAGE_EXECUTE = 0x10;
        private const int PAGE_EXECUTE_READ = 0x20;
        private const int PAGE_EXECUTE_READWRITE = 0x40;

        private const int SEC_COMMIT = 0x8000000;
        private const int SEC_IMAGE = 0x1000000;
        private const int SEC_NOCACHE = 0x10000000;
        private const int SEC_RESERVE = 0x4000000;

        private const int INVALID_HANDLE_VALUE = -1;

        private IntPtr m_hSharedMemoryFile = IntPtr.Zero;
        private IntPtr m_pwData = IntPtr.Zero;
        private bool m_bAlreadyExist = false;
        private bool m_bInit = false;
        private long m_MemSize = 0;
        private const long CStopWSize = 50240;

        private int _timerOut = 0;

        public int TimerOut
        {
            get
            {
                return _timerOut;
            }
            set
            {
                _timerOut = value;
            }
        }

        public CSharedString(string memName)
        {
            _timerOut = 5000;
            CreateMem(memName);
        }

        public CSharedString()
        {
            _timerOut = 5000;
            CreateMem();
        }

        public CSharedString(int TimerOut)
        {
            _timerOut = TimerOut;
            CreateMem();
        }

        public CSharedString(int TimerOut, string memNam)
        {
            _timerOut = TimerOut;
            CreateMem(memNam);
        }

        ~CSharedString()
        {
            Destroy();
        }

        public bool CreateMem(string memName)
        {
            return Create(memName);
        }

        public bool CreateMem()
        {
            return Create("newUcomm");
        }

        private bool Create(string memName)
        {
            int i = this.Create(memName, CStopWSize);
            return i == 0 ? true : false;
        }

        /// <summary>
        /// 初始化共享内存
        /// </summary>
        /// <param name="strName">共享内存名称</param>
        /// <param name="lngSize">共享内存大小</param>
        /// <returns></returns>
        private int Create(string strName, long lngSize)
        {
            if (lngSize <= 0 || lngSize > 0x00800000)
                lngSize = 0x00800000;
            m_MemSize = lngSize;
            if (strName.Length > 0)
            {
                //创建内存共享体(INVALID_HANDLE_VALUE)-1,0,4,0,8388608,"newUcomm"
                m_hSharedMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)lngSize, strName);
                if (m_hSharedMemoryFile == IntPtr.Zero)
                {
                    m_bAlreadyExist = false;
                    m_bInit = false;
                    return 2; //创建共享体失败
                }
                else
                {
                    if (GetLastError() == ERROR_ALREADY_EXISTS)  //已经创建
                    {
                        m_bAlreadyExist = true;
                    }
                    else                                         //新创建
                    {
                        m_bAlreadyExist = false;
                    }
                }
                //---------------------------------------
                //创建内存映射
                m_pwData = MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_WRITE, 0, 0, (uint)lngSize);
                if (m_pwData == IntPtr.Zero)
                {
                    m_bInit = false;
                    CloseHandle(m_hSharedMemoryFile);
                    return 3; //创建内存映射失败
                }
                else
                {
                    m_bInit = true;
                    if (m_bAlreadyExist == false)
                    {
                        //初始化
                    }
                }
                //----------------------------------------
            }
            else
            {
                return 1; //参数错误
            }

            return 0;     //创建成功
        }

        /// <summary>
        /// 关闭共享内存
        /// </summary>
        private void Destroy()
        {
            if (m_bInit)
            {
                UnmapViewOfFile(m_pwData);
                CloseHandle(m_hSharedMemoryFile);
            }
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="bytData">数据</param>
        /// <param name="lngAddr">起始地址</param>
        /// <param name="lngSize">个数</param>
        /// <returns></returns>
        private int Read(ref byte[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > m_MemSize)
                return 2; //超出数据区

            if (m_bInit)
            {
                Marshal.Copy((IntPtr)(m_pwData.ToInt64() + lngAddr - 1), bytData, 0, lngSize);
            }
            else
            {
                return 1; //共享内存未初始化
            }
            return 0;     //读成功
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="bytData">数据</param>
        /// <param name="lngAddr">起始地址</param>
        /// <param name="lngSize">个数</param>
        /// <returns></returns>
        private int Write(ref byte[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > m_MemSize)
                return 2; //超出数据区
            if (m_bInit)
            {
                Marshal.Copy(bytData, 0, (IntPtr)(m_pwData.ToInt64() + lngAddr - 1), lngSize);
            }
            else
            {
                return 1; //共享内存未初始化
            }
            return 0;     //写成功
        }

        public int GetInt(int start, int size)
        {
            string r = GetStr(start, size);
            if (string.IsNullOrEmpty(r))
            {
                return 0;
            }
            if (r.IndexOf('\0') >= 0)
            {
                return 0;
            }
            else
            {
                //解决异常 判定不全为数字 返回0
                for (int i = 0; i < size; i++)
                {
                    if (r[i] < '0' || r[i] > '9')
                        return 0;
                }
                return int.Parse(r);
            }
        }

        public long GetLong(int start, int size)
        {
            string r = GetStr(start, size);
            if (string.IsNullOrEmpty(r))
            {
                return 0;
            }
            if (r.IndexOf('\0') >= 0)
            {
                return 0;
            }
            else
            {
                return long.Parse(r);
            }
        }

        public double GetDouble(int start, int size)
        {
            return (double)GetLong(start, size);
        }

        public bool Getbool(int start)
        {
            int r = GetInt(start, 1);
            bool b = Convert.ToBoolean(r);
            return b;
        }

        public int GetHex(int start, int size)
        {
            string r = GetStr(start, size);
            if (string.IsNullOrEmpty(r))
            {
                return 0;
            }
            if (r.IndexOf('\0') >= 0)
            {
                return 0;
            }
            int d = 0;
            try
            {
                d = Convert.ToInt32(r, 16);
            }
            catch
            {
                return -1;
            }
            return d;
        }

        /// <summary>
        /// 获取共享内存字符串
        /// </summary>
        /// <param name="start">开始地址</param>
        /// <param name="size">长度</param>
        /// <returns></returns>
        public string GetStr(int start, int size)
        {
            if (Validation(start, size) == false)
                return "";

            byte[] bb = new byte[size];

            string returnStr = "";

            int i = Read(ref bb, start, size);

            if (i == 0)
            {
                returnStr = Encoding.UTF8.GetString(bb);
            }
            return returnStr;
        }

        public void SetStr(int start, string strVal)
        {
            if (Validation(start, strVal.Length) == false)
                return;

            //将"xxxxxxxx"n个字符的字符串数据转化为长度为n的byte整型数组(占n个字节)
            byte[] bb = Encoding.Default.GetBytes(strVal);

            int i = Write(ref bb, start, strVal.Length);
            if (i != 0)
            {
                MessageBox.Show("写入失败！");
            }
        }

        /// <summary>
        /// 等待写入PLC,成功为True,超时为False
        /// </summary>
        /// <param name="memAddr">写入标志位内存地址</param>
        /// <param name="size">长度</param>
        /// <param name="waitVal">标志位值</param>
        /// <returns>成功为True，超时为False</returns>
        public bool SleepFlag(int memAddr, int size, string waitVal)
        {
            bool resultBool = false;
            long st = timeGetTime();
            try
            {
                //在_TimerOut时间内不停地getStr(memAddr, size),判断标志位是否复位
                while (timeGetTime() - st < _timerOut)
                {
                    if (GetStr(memAddr, size) == waitVal)
                    {
                        resultBool = true;
                        break;
                    }
                    Application.DoEvents();
                }
            }
            catch
            {
                resultBool = false;
            }

            return resultBool;
        }

        public bool SleepFlag(int memAddr, int size, int num)
        {
            bool resultBool = false;
            long st = timeGetTime();
            try
            {
                //在_TimerOut时间内不停地getStr(memAddr, size),判断标志位是否复位
                while (timeGetTime() - st < num)
                {
                    if (GetStr(memAddr, size) == "0")
                    {
                        resultBool = true;
                        break;
                    }
                    Application.DoEvents();
                }
            }
            catch
            {
                resultBool = false;
            }

            return resultBool;
        }

        public void Sleep(int millisecond)
        {
            long st = timeGetTime();

            while (timeGetTime() - st < millisecond)
            {
                Application.DoEvents();
            }
        }

        private bool Validation(int start, int size)
        {
            if (start <= 0 || start >= CStopWSize || (start + size) >= CStopWSize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 读取具体数据类型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addr"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool Read<T>(int addr, int size, out T value)
        {
            byte[] temp = new byte[size];
            if (m_bInit)  //初始化成功
            {
                Marshal.Copy((IntPtr)(m_pwData.ToInt64() + addr - 1), temp, 0, size);
                if (typeof(T).Equals(typeof(int)))  //整型
                {
                    value = (T)(object)BitConverter.ToInt32(temp, 0);
                }
                else if (typeof(T).Equals(typeof(float))) //单精度浮点型
                {
                    value = (T)(object)BitConverter.ToSingle(temp, 0);
                }
                else if (typeof(T).Equals(typeof(double)))  //双精度浮点型
                {
                    value = (T)(object)BitConverter.ToDouble(temp, 0);
                }
                else if (typeof(T).Equals(typeof(bool))) //布尔型
                {
                    value = (T)(object)BitConverter.ToBoolean(temp, 0);
                }
                else
                {
                    value = default(T);
                    return false; //无匹配数据类型
                }
            }
            else
            {
                value = default(T);
                return false; //共享内存未初始化
            }
            return true;     //读成功
        }

        /// <summary>
        /// 写入具体数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addr"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Write<T>(int addr, int size, T value)
        {
            if (m_bInit)  //初始化成功
            {
                byte[] temp = null;
                long y = m_pwData.ToInt64();
                if (typeof(T).Equals(typeof(int)))  //整型
                {
                    Marshal.WriteInt32((IntPtr)(m_pwData.ToInt64() + addr - 1), (int)(object)value);
                }
                else if (typeof(T).Equals(typeof(float))) //单精度浮点型
                {
                    temp = BitConverter.GetBytes((float)(object)value);
                    Marshal.Copy(temp, 0, (IntPtr)(m_pwData.ToInt64() + addr - 1), temp.Length);
                }
                else if (typeof(T).Equals(typeof(double)))  //双精度浮点型
                {
                    temp = BitConverter.GetBytes((double)(object)value);
                    Marshal.Copy(temp, 0, (IntPtr)(m_pwData.ToInt64() + addr - 1), temp.Length);
                }
                else if (typeof(T).Equals(typeof(bool))) //布尔型
                {
                    temp = BitConverter.GetBytes((bool)(object)value);
                    Marshal.Copy(temp, 0, (IntPtr)(m_pwData.ToInt64() + addr - 1), temp.Length);
                }
                else
                {
                    return false; //无匹配数据类型
                }
            }
            else
            {
                return false; //共享内存未初始化
            }
            return true;
        }
    }
}