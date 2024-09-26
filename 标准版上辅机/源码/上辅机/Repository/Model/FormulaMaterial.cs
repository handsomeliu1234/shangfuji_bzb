using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("FormulaMaterial")]
    public class FormulaMaterial
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string MaterialID { get; set; }

        public string MaterialCode { get; set; }
        public string VersionNo { get; set; }
        public string DeviceID { get; set; }
        public string DeviceCode { get; set; }
        public string MaterialDesc { get; set; }
        public string TypeCodeID { get; set; }
        public string MaterialFrom { get; set; }
        public string BarCode { get; set; }
        public string SaveRealName { get; set; }
        public string UseToMaterialID { get; set; }
        public int Enable { get; set; }
        public string SaveUserID { get; set; }
        public DateTime SaveTime { get; set; }

        //恒温温度
        public string Reserve1 { get; set; }

        //恒温字符
        public string Reserve2 { get; set; }

        //配方的间隔时间
        public string Reserve3 { get; set; }

        public string Reserve4 { get; set; }

        //是否使用供胶机
        public string Reserve5 { get; set; }
    }
}