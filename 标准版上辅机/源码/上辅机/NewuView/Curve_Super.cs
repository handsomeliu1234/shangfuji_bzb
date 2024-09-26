using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace NewuView
{
    public struct SeriseParam
    {
        public SeriseParam(int No, Color color, int width, string field, int Loc)
        {
            this.No = No;
            this.color = color;
            this.width = width;
            this.field = field;
            showLegend = false;
            this.Loc = Loc;
        }

        public int No;
        public Color color;
        public int width;
        public string field;
        public bool showLegend;
        public int Loc;// 使用1👈 还是👉2
    }
    public class MixSerise
    {
        public SeriseParam[] ssr = new SeriseParam[9];

        public MixSerise()
        {
            ssr[0] = new SeriseParam(0, Color.Black, 0, "time", 1);
            ssr[1] = new SeriseParam(1, Color.Red, 3, "temp", 1);
            ssr[2] = new SeriseParam(2, Color.Green, 2, "Power", 2);
            ssr[3] = new SeriseParam(3, Color.Blue, 2, "Press", 1);
            ssr[4] = new SeriseParam(4, Color.DarkOrchid, 2, "Speed", 1);
            ssr[5] = new SeriseParam(5, Color.LawnGreen, 1, "Energy", 1);
            ssr[6] = new SeriseParam(6, Color.Magenta, 2, "Bolt", 2);
            ssr[7] = new SeriseParam(7, Color.Navy, 1, "Spannung", 2);
            ssr[8] = new SeriseParam(8, Color.DimGray , 6, "auto", 1);
        }
        public SeriseParam getMixSerise(int num, bool showLegend)
        {
            if (num<0 || num >= 9) return ssr[1];
            ssr[num].showLegend = false;
            return ssr[num];
        }
        public SeriseParam getMixSerise(int num)
        {
            if (num < 0 || num >= 9) return ssr[1];
            return ssr[num];
        }
    }
}
