using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_MixStepF")]
    public class RPT_MixStepF
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string MixStepID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string DevicePartCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string VersionNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Lot { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PlanQty { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int FactOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int StepOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ActionControlName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal SetTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal ActTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal SetTemp { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal ActTemp { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal SetPower { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal ActPower { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal SetEnergy { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal ActEnergy { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal SetPress { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal ActPress { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal SetSpeed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal ActSpeed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal KeepTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string WorkGroup { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string WorkerUserCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime SaveTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Reserve1 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Reserve2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Reserve3 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Reserve4 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Reserve5 { get; set; }
        public int VersionID { get; set; }
        public int Is_Read { get; set; }
        public DateTime ReadTime { get; set; }
    }
}