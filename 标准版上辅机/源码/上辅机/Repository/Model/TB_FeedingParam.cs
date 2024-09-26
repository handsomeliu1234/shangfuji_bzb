using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("TB_FeedingParam")]
    public class TB_FeedingParam
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string FeedingID { get; set; }

        public string DeviceID { get; set; }
        public string TypeCodeID { get; set; }

        public int BinNo { get; set; }

        public decimal? Big_FreqKuai { get; set; }

        public decimal? Big_FreqZhong { get; set; }

        public decimal? Big_FreqMan { get; set; }

        public decimal? Small_FreqKuai { get; set; }

        public decimal? Small_FreqZhong { get; set; }

        public decimal? Small_FreqMan { get; set; }

        public decimal? Big_FeedKuai { get; set; }

        public decimal? Big_FeedMan { get; set; }

        public decimal? Small_FeedKuai { get; set; }

        public decimal? Small_FeedMan { get; set; }

        public decimal? Sys_FeedKuaiTi { get; set; }

        public decimal? Sys_FeedZhongTi { get; set; }

        public decimal? Sys_FeedManTi { get; set; }

        public string SaveUserID { get; set; }

        public DateTime SaveTime { get; set; }

        public string Reserve1 { get; set; }

        public string Reserve2 { get; set; }

        public string Reserve3 { get; set; }

        public string Reserve4 { get; set; }

        public string Reserve5 { get; set; }
    }
}