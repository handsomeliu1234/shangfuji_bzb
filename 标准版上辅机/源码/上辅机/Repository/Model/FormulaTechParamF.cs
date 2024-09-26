using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("FormulaTechParamF")]
    public class FormulaTechParamF
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string FormulaTechParamID
        {
            get; set;
        }

        public string MaterialID
        {
            get; set;
        }

        public string TechParamID
        {
            get; set;
        }

        public decimal TechParamVal
        {
            get; set;
        }

        public string Reserve1
        {
            get; set;
        }

        public string Reserve2
        {
            get; set;
        }

        public string Reserve3
        {
            get; set;
        }

        public string Reserve4
        {
            get; set;
        }

        public string Reserve5
        {
            get; set;
        }
    }
}