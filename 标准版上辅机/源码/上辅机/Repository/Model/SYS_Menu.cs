using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [Dapper.Contrib.Extensions.Table("SYS_Menu")]
    public class SYS_Menu
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string MenuID { get; set; }

        public string Caption { get; set; }
        public string ControlType { get; set; }
        public string ParentMenuID { get; set; }
        public string ASSEMBLY { get; set; }
        public string NameSpaceAndClass { get; set; }
        public int ShowDialog { get; set; }
        public string ContainerForm { get; set; }
        public string ControlName { get; set; }
        public string ControlText { get; set; }
        public string ToolTip { get; set; }
        public int AutoShow { get; set; }
        public DateTime SaveTime { get; set; }
        public string MenuOrder { get; set; }
        public byte[] MenuLogo { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
    }
}