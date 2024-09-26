using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class FormulaWeighF
    {
        public string FormulaWeighID
        {
            get; set;
        }

        public string MaterialID
        {
            get; set;
        }

        public string MaterialCode
        {
            get; set;
        }

        public string DevicePartID
        {
            get; set;
        }

        public string DevicePartCode
        {
            get; set;
        }
        public string MixPartID
        {
            get; set;
        }

        public string WeighMaterialID
        {
            get; set;
        }

        public string WeighMaterialCode
        {
            get; set;
        }

        public string DeviceID
        {
            get; set;
        }

        public string DeviceCode
        {
            get; set;
        }

        public decimal WeighSetVal
        {
            get; set;
        }

        public decimal AllowError
        {
            get; set;
        }

        public int WeighOrder
        {
            get; set;
        }

        public int DropOrder
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

        public int Rubber
        {
            get; set;
        }

        public int Scanner
        {
            get; set;
        }
    }
}