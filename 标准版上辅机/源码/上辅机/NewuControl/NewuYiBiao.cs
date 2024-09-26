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
    public partial class NewuYiBiao : UserControl
    {
        public NewuYiBiao()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(110, 44);
        }


        /// <summary>
        /// 仪表名称
        /// </summary>
        public string NewuYiBiaoNameTxt
        {
            get { return btnScaleName.Text; }
            set { btnScaleName.Text = value; }
        }


        /// <summary>
        /// 仪表数值
        /// </summary>
        public string NewuYiBiaoValue
        {
            get { return txtScaleValue.Text; }
            set { txtScaleValue.Text = value; }
        }


        /// <summary>
        /// 仪表单位
        /// </summary>
        public string NewuYiBiaoUnit
        {
            get { return labUnit.Text; }
            set
            {
                //if (labUnit.Text == value) return;

                labUnit.Text = value;

                int w = labUnit.Width;
                int x = this.Width - w;

                labUnit.Location = new Point(x,labUnit.Location.Y);
            }
        }


        private string _NewuScaleToolTip = "";
        /// <summary>
        /// 磅秤提示
        /// </summary>
        public string NewuScaleToolTip
        {
            get { return _NewuScaleToolTip; }
            set
            {
                //if (_NewuScaleToolTip == value) return;
                _NewuScaleToolTip = value;
                toolTip1.SetToolTip(txtScaleValue, _NewuScaleToolTip);
            }
        }

        public void SetTitle(string str)
        {
            this.btnScaleName.Text = str;
        }


    }
    
}
