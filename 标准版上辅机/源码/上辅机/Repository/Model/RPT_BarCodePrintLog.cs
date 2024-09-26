
namespace Repository.Model
{
    /// <summary>
    /// 喷码机喷印条码实体
    /// </summary>
    [Dapper.Contrib.Extensions.Table("RPT_BarCodePrintLog")]
    public class RPT_BarCodePrintLog
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string PrintID { get; set; }
        public string OrderID { get; set; }
        public string DeviceCode { get; set; }
        public string FormulaCode { get; set; }
        public string VersionNo { get; set; }
        public int RealBatch { get; set; }
        public string PrintBarCodeContent { get; set; }
        public string SaveTime { get; set; }
        public string SaveUser { get; set; }
    }
}
