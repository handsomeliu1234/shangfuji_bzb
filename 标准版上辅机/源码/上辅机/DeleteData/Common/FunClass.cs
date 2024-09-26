using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DeleteData
{
  

    public  class FunClass
    {
        private static FunClass _FunClass = null;
        private static object FunClass_Lock = new object();
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static FunClass GetInstance()
        {
            if (_FunClass == null)
            {
                lock (FunClass_Lock)
                {
                    if (_FunClass == null)
                    {
                        _FunClass = new FunClass();
                    }

                }
            }
            return _FunClass;
        }

        public static string CurrentPath 
        {
            get { return Application.StartupPath.ToString(); }
        }


        /// <summary>
        /// 将内存地址的字符串转换为double
        /// </summary>
        /// <param name="memStr">字符串</param>
        /// <param name="decimalInt">小数位</param>
        /// <returns></returns>
        public static double GetMemStrDec(string memStr, int decimalInt)
        {

            if (decimalInt < 0) return 0;

            int tempVal = FunClass.vVal(memStr);
            double carbonVal = (double)tempVal / (double)Math.Pow(10, decimalInt);


            return carbonVal;
        }



        private static void WriteFile()
        {
            string _Path = "";

            FileStream stream = File.Open(_Path, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_Path, true))
            {

                file.WriteLine("");
           
            }

        }


        private static void ReadFile()
        {

            string _test="";
            string _Path = "";
            //按行读取为字符串数组
            string[] lines = System.IO.File.ReadAllLines(_Path);
            for (int i = 0; i <= lines.GetUpperBound(0); i++)
            {
                if (i == 0) { _test = lines[i]; }
            }

        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int vCint(double d)
        {
            try
            {
                string s = string.Format("{0:N0}", d);
                return Convert.ToInt32(s);
            }
            catch
            {
                return 0;
            }
        }
        public static double vDbl(object obj)
        {

            try
            {
                if (obj != null)
                {

                    return Convert.ToDouble(obj.ToString());
                }
                else
                {
                    return 0;
                }

            }
            catch
            {
                return 0;
            }


        }
        public static decimal vDecimal(object obj)
        {

            try
            {

                return Convert.ToDecimal(obj.ToString());

            }
            catch
            {
                return 0;
            }


        }

        /// <summary>
        /// 判断输入的是否为小数或整数，返回0则为空或字符，返回2则为整数
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int isEmptyOrNumber(string txt)
        {

            int veri = 0;
            Regex regDig = new Regex(@"^\d+\.\d+$");
            Regex regNum = new Regex(@"^\d+$");
            if (txt.Trim() == "" || txt.Length < 0)
            {
                veri = 0;
            }
            else if (regDig.IsMatch(txt))
            {
                // 是小数
                veri = 1;
            }
            else if (regNum.IsMatch(txt))
            {
                // 是整数
                veri = 2;
            }
            return veri;
        }

        /// <summary>
        /// 判断输入的是否为数字，不是则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int vVal(object obj)
        {

            try
            {
                if (obj.ToString().Contains("."))
                {
                    double d = Convert.ToDouble(obj.ToString());
                    return vCint(d);
                }
                else
                {
                    return Convert.ToInt32(obj.ToString());
                }
            }
            catch
            {
                return 0;
            }


        }

        /// <summary>
        /// 获取空字符
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetEmptyChr(int size)
        {
            byte[] dd = new byte[size];

            return System.Text.Encoding.Default.GetString(dd);
        }


        public static string vStr(object obj)
        {
            string r = "";
            if (obj != null)
            {
                r = obj.ToString();
            }
            return r;
        }

        public static bool IsIPAddress(string ipAddress)
        {
            Regex validipregex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            return (ipAddress != "" && validipregex.IsMatch(ipAddress.Trim())) ? true : false;
        }

        //public void WriteLogFile(string input, string logFolderAndFileName)
        //{

        //    try
        //    {
        //        string fileName = DateTime.Now.ToString("yyyyMMdd");
        //        string fileName2 = DateTime.Now.ToString("HH:mm:ss");
        //        /**/
        //        ///指定日志文件的目录
        //        string fname = "";
        //        if (logFolderAndFileName == "")
        //        {
        //            //fname =Directory.GetCurrentDirectory() + "\\" + fileName + "_LogFile.txt";
        //            Directory.CreateDirectory(Application.StartupPath + "\\Log");
        //            fname = Application.StartupPath + "\\Log\\" + fileName + "_LogFile.txt";
        //        }
        //        else
        //        {
        //            //fname = logPath;
        //            string[] li = new string[1] { "\\" };
        //            string[] folder = logFolderAndFileName.Split(li, StringSplitOptions.RemoveEmptyEntries);
        //            if (!Directory.Exists(Application.StartupPath + "\\" + folder[0]))
        //            {
        //                Directory.CreateDirectory(Application.StartupPath + "\\" + folder[0]);
        //            }
        //            fname = Application.StartupPath + "\\" + logFolderAndFileName;
        //        }


        //        /**/
        //        ///定义文件信息对象

        //        FileInfo finfo = new FileInfo(fname);

        //        if (!finfo.Exists)
        //        {
        //            try
        //            {
        //                FileStream fs;
        //                fs = File.Create(fname);
        //                fs.Close();
        //                finfo = new FileInfo(fname);
        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //        }

        //        /**/
        //        ///判断文件是否存在以及是否大于2K
        //        if (finfo.Length > 1024 * 1024 * 10)
        //        {
        //            /**/
        //            ///文件超过10MB则重命名
        //            File.Move(fname, CurrentPath + fileName + fileName2 + "_LogFile.txt");
        //            /**/
        //            ///删除该文件
        //            //finfo.Delete();
        //        }
        //        //finfo.AppendText();
        //        /**/
        //        ///创建只写文件流

        //        using (FileStream fs = finfo.OpenWrite())
        //        {
        //            /**/
        //            ///根据上面创建的文件流创建写数据流
        //            StreamWriter w = new StreamWriter(fs);

        //            /**/
        //            ///设置写数据流的起始位置为文件流的末尾
        //            w.BaseStream.Seek(0, SeekOrigin.End);


        //            string time1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " ";
        //            time1 += DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString("D4");

        //            /**/
        //            ///写入日志内容并换行
        //            string log = "\n\r" + time1 + "：" + input + "\n\r";
        //            w.Write(log);




        //            /**/
        //            ///清空缓冲区内容，并把缓冲区内容写入基础流
        //            w.Flush();

        //            /**/
        //            ///关闭写数据流
        //            w.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
           

            
        //}


        /**/
        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input"></param>
        //public  void WriteLogFile(string input)
        //{

        //    WriteLogFile(input, "");
        //}

    }
}
