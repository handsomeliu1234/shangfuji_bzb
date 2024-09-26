using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class Formula
    {
        /// <summary>
        /// 订单来源，0本机，1 mes
        /// </summary>
        public int OrderSource
        {
            get; set;
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId
        {
            get; set;
        }

        /// <summary>
        /// 计划车数
        /// </summary>
        public int PlanNum
        {
            get; set;
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo
        {
            get; set;
        }

        /// <summary>
        /// 计划下发时间
        /// </summary>
        public DateTime PlanAddTime
        {
            get; set;
        }

        /// <summary>
        /// 称量部分
        /// </summary>
        public List<View_FormulaWeigh> FormulaWeights
        {
            get; set;
        }

        /// <summary>
        /// 密炼部分
        /// </summary>
        public List<View_FormulaMix> FormulaMixs
        {
            get; set;
        }

        /// <summary>
        /// 配方参数
        /// </summary>
        public List<View_FormulaTechParam> FormulaTechParam
        {
            get; set;
        }
    }
}