﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NewuView
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Mix.FM_JL());
            Application.Run();
        }
    }
}