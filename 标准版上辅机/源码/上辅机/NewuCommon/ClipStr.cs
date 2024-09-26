using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
namespace NewuCommon
{
    public class ClipStr
    {

        IDataObject iData = Clipboard.GetDataObject();

        //粘贴
        public string  GetData()
        {
            string s = "";
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                s = (String)iData.GetData(DataFormats.Text);
            }
            return s;
        }

        //复制
        public void Setdata(string str)
        {

            if (str != "")
            {
                Clipboard.SetDataObject(str);
            }
        }


    }
}
