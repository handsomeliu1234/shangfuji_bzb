using Repository.GlobalConfig;
using System.Threading.Tasks;

namespace NewuView.Mix
{
    /**
     * 考虑到 测试通讯方式较为繁琐，后续可能继续优化测试方式  抽出在改类中。
     * 以方便优化简化日后开发时间
     * 使用方式：
     * 1. 获取该类的实例。PLC_Connection_State plc_state = PLC_Connection_State.GetInstance();
     * 2. 使用该类实例 开启异步轮训 测试  plc_state.StartRun();
     * 3. 该类的ConnectTionState变量  存储的为是否成功
     *    给予该变量 UI显示即可  tb_PLCState.Text = plc_state.ConnectTionState ? "连接成功" : "连接失败";
     *               Coders --wings 2018.9.10
     */

    internal class PLC_Connection_State
    {
        #region 单例

        //在Singleton第一次被调用时会执行instance的初始化
        private static PLC_Connection_State _instance = null;

        private static readonly object padlock = new object();

        private static PLC_Connection_State Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new PLC_Connection_State();
                    }
                    return _instance;
                }
            }
        }

        public static PLC_Connection_State GetInstance()
        {
            return Instance;
        }

        #endregion 单例

        private bool _connecTionState = false;

        public bool ConnectTionState
        {
            get
            {
                return _connecTionState;
            }
            set
            {
                this._connecTionState = value;
            }
        }

        //默认间隔时间2000ms；
        private int _delayTime = 2000;

        //地址表中的通讯地址点
        private int _aim = AddressConst.PLCCommunicationStatus;

        private int _write = AddressConst.PLCCommunicationStatus;
        private bool _isRun = false;

        private int Aim
        {
            get
            {
                return _aim;
            }
            set
            {
                this._aim = value;
            }
        }

        private int Write
        {
            get
            {
                return _write;
            }
            set
            {
                this._write = value;
            }
        }

        private int DelayTime
        {
            get
            {
                return _delayTime;
            }
            set
            {
                this._delayTime = value;
            }
        }

        public void SetValue(int delayTime, int aim, int write)
        {
            DelayTime = delayTime;
            Aim = aim;
            Write = write;
        }

        //供外调用，开启判断连接
        public async void StartRun()
        {
            if (_isRun)
                return;
            _isRun = true;
            await Task.Run(() => PPP());
        }

        public async void StartRun(int delayTime, int aim, int write)
        {
            if (_isRun)
                return;
            _isRun = true;
            SetValue(delayTime, aim, write);
            await Task.Run(() => PPP());
        }

        private async void PPP()
        {
            while (true)
            {
                PLCConnectState(Aim, Write);
                await Task.Delay(DelayTime);
            }
        }

        /// <summary>
        /// 向通讯程序中 AIm点 不停的写数，判断是否通讯正常
        /// </summary>
        /// <param name="aim">通讯程序测试点</param>
        /// <param name="write">地址表中是否连接成功的点，历史回放使用 </param>
        private void PLCConnectState(int aim, int write)
        {
            NewuGlobal.MemW.SetStr(aim, "0001");
            NewuGlobal.MemF.SetStr(aim, "1");
            if (NewuGlobal.MemF.SleepFlag(aim, 1, 5000))
            {
                ConnectTionState = true;
            }
            else
            {
                ConnectTionState = false;
            }
        }
    }
}