using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class MixerShow : FormulaMix
    {
        public string PlcStepSpeed
        {
            get; set;
        }

        public string PlcStepPress
        {
            get; set;
        }

        public string PlcStepTemp
        {
            get; set;
        }

        public string PlcStepTime
        {
            get; set;
        }

        public string PlcStepEnergy
        {
            get; set;
        }

        public string PlcSteppower
        {
            get; set;
        }
    }
}