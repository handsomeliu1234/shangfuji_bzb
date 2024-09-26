using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_TypeCode")]
    public class SYS_TypeCode
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string TypeCodeID { get; set; }

        public string TypeCodeName { get; set; }
        public string TypeCodeDesc { get; set; }
        public string TypeCodeSpell { get; set; }
        public string ParentTypeCodeID { get; set; }
        public string ParentTypeCodeDataSet { get; set; }
        public DateTime SaveTime { get; set; }
        public int Enable { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }

    public enum TypeCodeEnum
    {
        T炭黑,
        T粉料,
        T白炭黑,
        T油料,
        T胶料,
        T药品,
        T塑解剂,
        T硅烷,
        T母炼配方,
        T终炼配方,
        T小药配方,
        T原材料类型,
        T配方类型
    }
}