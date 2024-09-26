using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.GlobalConfig
{
    public class MixerState
    {
        public MixerState()
        {
            isRun = false;
            runOrderID = null;
        }

        public bool isRun { get; set; }
        public string runOrderID { get; set; }
    }
}