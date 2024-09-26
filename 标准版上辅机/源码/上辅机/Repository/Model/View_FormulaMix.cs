using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("View_FormulaMix")]
    public class View_FormulaMix
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string FormulaMixID { get; set; }

        public string MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string DeviceID { get; set; }
        public string DeviceCode { get; set; }
        public string DevicePartID { get; set; }
        public string DevicePartCode { get; set; }
        public int StepOrder { get; set; }
        public int StepCode { get; set; }
        public string StepDesc { get; set; }
        public string ActionControlCode { get; set; }
        public decimal StepTime { get; set; }
        public decimal StepTemp { get; set; }
        public decimal StepPower { get; set; }
        public decimal StepEnergy { get; set; }
        public decimal StepPress { get; set; }
        public decimal StepSpeed { get; set; }
        public decimal KeepTime { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public int ActionControlValue { get; set; }
        public string ActionControlNameCN { get; set; }
        public string ActionControlNameEN { get; set; }
    }
}