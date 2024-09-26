using System;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_AutoCheckScale")]
    public class RPT_AutoCheckScale
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string ID { get; set; }
        public string DeviceCode { get; set; }
        public int CheckScaleNo { get; set; }
        public string ScaleName { get; set; }
        public string ScaleWeight { get; set; }
        public string SetError { get; set; }
        public string RealWeight { get; set; }
        public bool Result { get; set; }
        public DateTime SaveTime { get; set; }
        public string SaveUser { get; set; }
    }
}
