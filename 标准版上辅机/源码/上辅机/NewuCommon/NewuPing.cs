using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace NewuCommon
{
    public static class NewuPing
    {
        public static PingReply Ping(string ipAddr,int timerOut)
        {
            //Ping 实例对象;
            Ping pingSender = new Ping();
       

            //调用同步send方法发送数据，结果存入reply对象;
            PingReply reply = pingSender.Send(ipAddr, timerOut);

            #if DEBUG
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("主机地址::" + reply.Address);
                Console.WriteLine("往返时间::" + reply.RoundtripTime);
                Console.WriteLine("生存时间TTL::" + reply.Options.Ttl);
                Console.WriteLine("缓冲区大小::" + reply.Buffer.Length);
                Console.WriteLine("数据包是否分段::" + reply.Options.DontFragment);
            }
            #endif

            return reply;
        }

        /// <summary>
        /// 网络是否能ping成功
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <returns></returns>
        public static bool IsSuccess(string ipAddr)
        {
            if (Ping(ipAddr, 300).Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
