using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("View_FormulaTechParamF")]
    public class View_FormulaTechParamF
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string MaterialID
        {
            get; set;
        }

        public string MaterialCode
        {
            get; set;
        }

        public decimal TechParamVal
        {
            get; set;
        }

        public string TechParamID
        {
            get; set;
        }

        public string FormulaTechParamID
        {
            get; set;
        }

        public int TechParamOrder
        {
            get; set;
        }
    }
}