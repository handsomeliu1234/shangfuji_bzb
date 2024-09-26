using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Runtime.InteropServices;

namespace NewuCommon
{
    public static class APIClass
    {

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);
    }
}
