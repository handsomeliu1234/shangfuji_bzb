using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class ProccessBar2 : UserControl
    {
        public ProccessBar2()
        {
            InitializeComponent();
        }
        //设置粗秤百分比
        private decimal _percent;
        public decimal Percent
        {
            get
            { return _percent; }
            set
            { _percent = value; }
        }

        //设置设定重量
        private decimal _setvalue;
        public decimal Setvalue
        {
            get { return _setvalue; }
            set
            { _setvalue = value; }
        }

        //允许误差
        private decimal _allowerror;
        public decimal AllowError
        {
            get { return _allowerror; }
            set
            { _allowerror = value; }
        }


        private decimal _val;

        public decimal Val
        {

            get
            { return _val; }
            set
            {
                _val = value;
                decimal perValue = _percent * _setvalue; decimal topValue = _setvalue + AllowError;
                decimal minValue = _setvalue - AllowError;

                progress1.Mini = 0;
                progress1.Max = Convert.ToInt32(perValue);
                progress2.Mini = Convert.ToInt32(perValue);
                progress2.Max = Convert.ToInt32(_setvalue);
                progress3.Mini = Convert.ToInt32(_setvalue);
                progress3.Max = Convert.ToInt32(_setvalue + 2 * perValue);
                progress4.Mini = Convert.ToInt32(perValue);
                progress4.Max = Convert.ToInt32(minValue);
                progress5.Mini = Convert.ToInt32(minValue);
                progress5.Max = Convert.ToInt32(_setvalue);
                progress6.Mini = Convert.ToInt32(_setvalue);
                progress6.Max = Convert.ToInt32(topValue);
                progress7.Mini = Convert.ToInt32(topValue);
                progress7.Max = Convert.ToInt32(topValue + AllowError);

                if (_val <= perValue)
                {
                    progress1.Val = Convert.ToInt32(_val);
                    progress2.Val = 0;
                    progress3.Val = 0;
                    progress4.Val = 0;
                    progress5.Val = 0;
                    progress6.Val = 0;
                    progress7.Val = 0;

                }
                else if (_val > perValue && _val <= minValue)
                {
                    progress1.Val = Convert.ToInt32(perValue);
                    progress2.Val = Convert.ToInt32(_val);
                    progress4.Val = Convert.ToInt32(_val);
                    progress3.Val = 0;
                    progress5.Val = 0;
                    progress6.Val = 0;
                    progress7.Val = 0;

                }
                else if (_val > minValue && _val <= _setvalue)
                {
                    progress1.Val = Convert.ToInt32(perValue);
                    progress2.Val = Convert.ToInt32(_val);
                    progress4.Val = Convert.ToInt32(minValue);
                    progress5.Val = Convert.ToInt32(_val);
                    progress3.Val = 0;
                    progress6.Val = 0;
                    progress7.Val = 0;
                }
                else if (_val > Setvalue && _val <= topValue)
                {
                    progress1.Val = Convert.ToInt32(perValue);
                    progress2.Val = Convert.ToInt32(Setvalue);
                    progress3.Val = Convert.ToInt32(_val);
                    progress4.Val = Convert.ToInt32(minValue);
                    progress5.Val = Convert.ToInt32(Setvalue);
                    progress6.Val = Convert.ToInt32(_val);
                    progress7.Val = 0;
                }
                else if (_val > topValue && _val < topValue + AllowError) /////
                {
                    progress1.Val = Convert.ToInt32(perValue);
                    progress2.Val = Convert.ToInt32(Setvalue);
                    progress3.Val = Convert.ToInt32(_val);
                    progress4.Val = Convert.ToInt32(minValue);
                    progress5.Val = Convert.ToInt32(Setvalue);
                    progress6.Val = Convert.ToInt32(topValue);
                    progress7.Val = Convert.ToInt32(_val);
                }

                if (_val > minValue && _val <= topValue)
                {

                    progress1.ProgressBarColor = Color.Lime;
                    progress2.ProgressBarColor = Color.Lime;
                    progress3.ProgressBarColor = Color.Lime;
                    progress4.ProgressBarColor = Color.Lime;
                    progress5.ProgressBarColor = Color.Lime;
                    progress6.ProgressBarColor = Color.Lime;
                    progress7.ProgressBarColor = Color.Lime;




                }
                else if (_val > topValue)
                {

                    progress1.ProgressBarColor = Color.Red;
                    progress2.ProgressBarColor = Color.Red;
                    progress3.ProgressBarColor = Color.Red;
                    progress4.ProgressBarColor = Color.Red;
                    progress5.ProgressBarColor = Color.Red;
                    progress6.ProgressBarColor = Color.Red;
                    progress7.ProgressBarColor = Color.Red;



                }
                else
                {
                    progress1.ProgressBarColor = Color.Blue;
                    progress2.ProgressBarColor = Color.Blue;
                    progress3.ProgressBarColor = Color.Blue;
                    progress4.ProgressBarColor = Color.Blue;
                    progress5.ProgressBarColor = Color.Blue;
                    progress6.ProgressBarColor = Color.Blue;
                    progress7.ProgressBarColor = Color.Blue;
                }
            }
        }


    }
}
