using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class XfLog : UserControl
    {
        public delegate void AlarmAppendDelegate(Color color, string text);
        public XfLog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 追加显示的文本
        /// </summary>
        /// <param name="color">文本颜色</param>
        /// <param name="text">显示的文本</param>
        public void LogAppend(Color color, string text)
        {
            if (richTextBox1.Lines.Length > 20)
            {
                richTextBox1.Clear();
            }
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(text.Trim() + "\n");
        }

        /// <summary>
        /// 显示错误日志,红色
        /// </summary>
        /// <param name="text"></param>
        public void LogError(string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Red, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary>
        /// 显示警告
        /// </summary>
        /// <param name="text"></param>
        public void LogWarning(string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Violet, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="text"></param>
        public void LogMessage(string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Black, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        public void LogHistory(string time, string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Black, time + " _ "+ text);
        }

        public void SetTitle(string str)
        {
            this.label1.Text = str;
        }
    }
}
