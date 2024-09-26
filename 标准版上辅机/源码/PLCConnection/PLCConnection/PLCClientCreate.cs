using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCConnection
{
    /*Bit  	=> 	bool
      Byte => byte
      word 	=> 	ushort
      DWord => uint
      Int 	=> 	short
      DInt => int
      Real 	=> 	float
      LReal => double
      String 	=> 	string
    */

    public class PLCClientCreate
    {
        private static Plc plc;
        private string PLCIP = "192.168.0.1";

        public Plc InitSimens()
        {
            if (plc == null)
            {
                plc = new Plc(CpuType.S71500, PLCIP, 0, 1);
                plc.Open();
            }
            return plc;
        }

        public ushort ReadDBW(string flag, string address)
        {
            try
            {
                ushort plcValue = 0;
                if (plc != null && plc.IsConnected)
                    plcValue = (ushort)plc.Read(address);

                return plcValue;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public uint ReadDBD(string flag, string address)
        {
            try
            {
                uint plcValue = 0;
                if (plc != null && plc.IsConnected)
                    plcValue = (uint)plc.Read(address);

                return plcValue;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool ReadDBX(string flag, string address)
        {
            try
            {
                bool plcValue = false;
                if (plc != null && plc.IsConnected)
                    plcValue = (bool)plc.Read(address);

                return plcValue;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Write(string flag, string address, string addressValue)
        {
            try
            {
                if (plc != null && plc.IsConnected)
                {
                    switch (flag)
                    {
                        case "dbw":
                            plc.Write(address, Convert.ToInt16(addressValue));
                            break;

                        case "dbd":
                            break;

                        case "dbx":

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}