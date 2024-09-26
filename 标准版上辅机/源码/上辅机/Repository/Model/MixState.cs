using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class MixState
    {
        /// <summary>
        /// 物料是否进入密炼机
        /// </summary>
        public bool IsInMix
        {
            get; set;
        }

        /// <summary>
        /// 炼胶开始
        /// </summary>
        public bool IsInReady
        {
            get; set;
        }

        public DateTime MixST
        {
            get; set;
        }

        public DateTime MixED
        {
            get; set;
        }

        /// <summary>
        /// 投料间隔时间
        /// </summary>
        public int DropCycle
        {
            get; set;
        }

        /// <summary>
        /// 车数，相当于280
        /// </summary>
        public int RealBatch
        {
            get; set;
        }

        /// <summary>
        /// 最近一次OrderCode
        /// </summary>
        public string LastOrderCode
        {
            get; set;
        }

        private string _curveid = "";
        /// <summary>
        /// 曲线数据ID
        /// </summary>
        public string CurveID
        {
            get 
            {
                return _curveid;
            }
            set 
            {
                _curveid = value;
            }
        }

        /// <summary>
        /// 上顶栓高低位数据
        /// </summary>
        public string Ram_state
        {
            get; set;
        }

        /// <summary>
        /// 加料门状态数据
        /// </summary>
        public string DOOR_CHARGE
        {
            get; set;
        }

        /// <summary>
        /// 卸料门状态数据
        /// </summary>
        public string DOOR_DISCHARGE
        {
            get; set;
        }

        /// <summary>
        /// 电压
        /// </summary>
        public string Voltage
        {
            get; set;
        }

        /// <summary>
        /// 自动手动
        /// </summary>
        public string AutoRun
        {
            get; set;
        }
    }
}