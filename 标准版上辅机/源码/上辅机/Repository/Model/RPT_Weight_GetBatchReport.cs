using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class RPT_Weight_GetBatchReport
    {
        /// <summary>
        ///
        /// </summary>
        public string TypeCodeName
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string SetMaterialCode
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public int SetBinNo
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public decimal SetWeight
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string ActWeight
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public int FactOrder
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public int DropOrder
        {
            get; set;
        }
    }
}