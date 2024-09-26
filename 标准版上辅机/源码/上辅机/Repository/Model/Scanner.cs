using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class Scanner
    {
        private EndPoint scannerIP;

        public EndPoint ScannerIP
        {
            get { return scannerIP; }
            set { scannerIP = value; }
        }

        private int? binNo;

        public int? BinNo
        {
            get { return binNo; }
            set { binNo = value; }
        }

        private string materialBarCode;

        public string MaterialBarCode
        {
            get { return materialBarCode; }
            set { materialBarCode = value; }
        }

        public Scanner(EndPoint scannerIP, int? binNo, string materialBarCode)
        {
            this.scannerIP = scannerIP;
            this.binNo = binNo;
            this.materialBarCode = materialBarCode;
        }
    }
}