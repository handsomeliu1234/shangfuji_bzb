namespace NewuCommon
{
    public class MemAddrMananger
    {
        private CSharedString _readMem = null;

        public CSharedString ReadMem
        {
            get
            {
                return _readMem;
            }
            set
            {
                _readMem = value;
            }
        }

        private CSharedString _writeMem = null;

        public CSharedString WriteMem
        {
            get
            {
                return _writeMem;
            }
            set
            {
                _writeMem = value;
            }
        }

        private CSharedString _flagMem = null;

        public CSharedString FlagMem
        {
            get
            {
                return _flagMem;
            }
            set
            {
                _flagMem = value;
            }
        }

        private CSharedString _backMem = null;

        public CSharedString BackMem
        {
            get
            {
                return _backMem;
            }
            set
            {
                _backMem = value;
            }
        }

        public MemAddrMananger()
        {
            _readMem = new CSharedString("newUcomm");
            _writeMem = new CSharedString("newUcommW");
            _flagMem = new CSharedString("newUcommF");
            _backMem = new CSharedString("newUcommB");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addr">同步地址</param>
        /// <param name="value">同步值</param>
        /// <param name="flagValue">同步标志值</param>
        /// <param name="size">同步标志内存大小</param>
        /// <param name="waitValue">需要的同步标志复位值</param>
        /// <returns></returns>
        public bool Sync(int addr, string value, string flagValue = "1", int size = 1, string waitValue = "0")
        {
            _writeMem.SetStr(addr, value);
            _flagMem.SetStr(addr, flagValue);
            return _flagMem.SleepFlag(addr, size, waitValue);
        }

        /// <summary>
        /// 读取具体数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addr"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Read<T>(int addr, int size, out T value)
        {
            return _readMem.Read<T>(addr, size, out value);
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
            _writeMem.Write<T>(addr, size, value);
            _flagMem.SetStr(addr, "1");
            return _flagMem.SleepFlag(addr, 1, "0");
        }

        public int GetSharedMemIntValue(int addr, int size)
        {
            //return _readMem.getInt(addr, size);
            return _readMem.GetHex(addr, size);
        }

        public string GetSharedMemStringValue(int addr, int size)
        {
            return _readMem.GetStr(addr, size);
        }

        public void WriteReadMen(int addr, string value)
        {
            _readMem.SetStr(addr, value);
        }

        public bool WriteReadMenSelf(int addr, string value, string flagValue = "1", int size = 1, string waitValue = "0")
        {
            _readMem.SetStr(addr, value);
            return true;
        }
    }
}