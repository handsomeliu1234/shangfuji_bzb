using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GlobalConfig
{
    public class FormulaTranEventArgs : EventArgs
    {
        public string MemoryAddrs { get; set; }
        public string Message { get; set; }
        public string Desccription { get; set; }
        public int BinNo { get; set; }
        public bool TransToBin;
        public bool TransToScale;
    }
}