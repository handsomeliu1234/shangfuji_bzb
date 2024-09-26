using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_Weight")]
    public class RPT_Weight
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string WeightID
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string DeviceCode
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string DevicePartCode
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string OrderID
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string MaterialCode
        {
            get; set;
        }

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
        public string VersionNo
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Lot
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public int PlanQty
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
        public decimal AllowError
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public decimal ActWeight
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public decimal ActError
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public int WeightOrder
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

        /// <summary>
        ///
        /// </summary>
        public string WorkGroup
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string WorkOrder
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string WorkerUserCode
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime SaveTime
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Reserve1
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Reserve2
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Reserve3
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Reserve4
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Reserve5
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string MaterialRate
        {
            get; set;
        }

        public int VersionID
        {
            get; set;
        }

        public int Is_Read
        {
            get; set;
        }

        public DateTime ReadTime
        {
            get; set;
        }
    }
}