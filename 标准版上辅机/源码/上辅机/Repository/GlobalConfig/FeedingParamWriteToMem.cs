using NewuCommon;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.GlobalConfig
{
    public class FeedingParamWriteToMem
    {
        private readonly SYS_TypeCodeRepository typeCodeRepository = new SYS_TypeCodeRepository();

        //委托
        public delegate void doWriteProgress(int total, int current);

        //事件
        public event doWriteProgress OnWriteProgress;

        public FeedingParamWriteToMem()
        {
        }

        private string Format(params decimal[] menStr)   //注意不定参数的用法
        {
            StringBuilder dataStr = new StringBuilder();
            foreach (decimal str in menStr)
            {
                dataStr.Append(str.ToString("0000"));
            }
            return dataStr.ToString();
        }

        /// <summary>
        /// 全部下发时调用
        /// </summary>
        /// <param name="feedingParam"></param>
        /// <returns></returns>
        public bool WriteToMem(List<TB_FeedingParam> feedingParam)
        {
            bool isok = false;
            int i = 0;
            foreach (var f in feedingParam)
            {
                OnWriteProgress(feedingParam.Count, ++i);

                isok = WriteToMemS(f);
                if (isok == false)
                    break;
            }
            return isok;
        }

        /// <summary>
        /// 发送配方时调用
        /// </summary>
        /// <param name="feedingParam"></param>
        /// <returns></returns>
        public bool WriteToMemTransfer(List<TB_FeedingParam> feedingParam)
        {
            bool isok = WriteAllToMemS(feedingParam);
            return isok;
        }

        /// <summary>
        /// 全部下发时调用
        /// </summary>
        /// <param name="typeCodeID"></param>
        /// <returns></returns>
        public bool WriteAllToMemS(List<TB_FeedingParam> feedingParams)
        {
            try
            {
                List<TB_FeedingParam> carbonBin = new List<TB_FeedingParam>();
                List<TB_FeedingParam> znoBin = new List<TB_FeedingParam>();
                List<TB_FeedingParam> wCarbonBin = new List<TB_FeedingParam>();
                string carbon = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T炭黑);
                string carbonTypeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(carbon);
                string zno = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T粉料);
                string zonTypeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(zno);
                string wCarbon = typeCodeRepository.GetTypeCodeNameByEnum(TypeCodeEnum.T白炭黑);
                string wCarbonTypeCodeID = NewuGlobal.GetTypeCodeIDByCodeName(wCarbon);

                int addrStart = 9000;

                foreach (var item in feedingParams)
                {
                    if (!string.IsNullOrEmpty(carbonTypeCodeID) && item.TypeCodeID.Equals(carbonTypeCodeID))
                        carbonBin.Add(item);
                    else if (!string.IsNullOrEmpty(zonTypeCodeID) && item.TypeCodeID.Equals(zonTypeCodeID))
                        znoBin.Add(item);
                    else if (!string.IsNullOrEmpty(zonTypeCodeID) && item.TypeCodeID.Equals(wCarbonTypeCodeID))
                        wCarbonBin.Add(item);
                }

                bool flag = false;

                if (carbonBin.Count > 0)
                {
                    string dataStr = "";
                    foreach (var item in carbonBin)
                    {
                        dataStr += (item.Big_FreqKuai?.ToString("0000") + item.Big_FreqZhong?.ToString("0000"));
                    }
                    flag = NewuGlobal.MemMgr.Sync(addrStart, dataStr);
                }

                if (znoBin.Count > 0)
                {
                    string zDataStr = "";
                    foreach (var item in znoBin)
                    {
                        zDataStr += item.Big_FreqKuai?.ToString("0000") + item.Big_FreqZhong?.ToString("0000");
                    }
                    flag = NewuGlobal.MemMgr.Sync(addrStart + 100, zDataStr);
                }

                if (wCarbonBin.Count > 0)
                {
                    string wDataStr = "";
                    foreach (var item in wCarbonBin)
                    {
                        wDataStr += item.Big_FreqKuai?.ToString("0000") + item.Big_FreqZhong?.ToString("0000");
                    }
                    flag = NewuGlobal.MemMgr.Sync(addrStart + 200, wDataStr);
                }

                return flag;
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("FeedingParamWriteToMem").Error(ex.ToString());
                return false;
            }
        }

        public int GetAddAddr(string typeCodeID)
        {
            if (NewuGlobal.GetTypeCodeIDByCodeName("Zno") == typeCodeID)
            {
                return 100;
            }
            return 0;
        }

        /// <summary>
        /// 单个下发调用
        /// </summary>
        /// <param name="feedingParam"></param>
        /// <returns></returns>
        public bool WriteToMemS(TB_FeedingParam feedingParam)
        {
            int addaddr = GetAddAddr(feedingParam.TypeCodeID);
            int i = feedingParam.BinNo;
            if (i < 0 || i > 12)
            {
                NewuGlobal.LogCat("FeedingParamWriteToMem").Info("加料参数中储斗编号数据超出范围");
            }
            int addrSt = 9000 + (i - 1) * 8 + addaddr;
            string memStr1 = Format(feedingParam.Big_FreqKuai.Value);
            bool isOk = NewuGlobal.MemMgr.Sync(addrSt, memStr1);

            string memStr2 = Format(feedingParam.Big_FreqZhong.Value);
            bool isOk2 = NewuGlobal.MemMgr.Sync(addrSt + 4, memStr2);

            if (isOk == false)
                return false;

            if (isOk2 == false)
                return false;
            return true;
        }
    }
}