using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class TB_FormulaOperateLog
    {
        public string MaterialID
        {
            get; set;
        }

        public string MaterialCode
        {
            get; set;
        }

        public string FormulaWeight
        {
            get; set;
        }

        public string FormulaMixStep
        {
            get; set;
        }

        public string FormulaTechParam
        {
            get; set;
        }

        public string FormulaMixStepF
        {
            get; set;
        }

        public string FormulaTechParamF
        {
            get; set;
        }

        public string Version
        {
            get; set;
        }

        public DateTime SaveTime
        {
            get; set;
        }
    }
}