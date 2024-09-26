using System;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("RPT_BarcodeRecord")]
    public class RPT_BarcodeRecord
    {
        public string BarcordID { get; set; }        //发送ID
        public string OrderID { get; set; }        //发送ID
        public string DeviceCode { get; set; }      //机台名
        public string MaterialCode { get; set; }    //配方名
        public string TypeCodeName { get; set; }   //物料类型
        public string WeighMaterialCode { get; set; }  //物料名
        public DateTime SaveTime { get; set; }         //保存时间
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public string Reserve6 { get; set; }
        public string Reserve7 { get; set; }
    }
}