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
    public partial class NewuYiBiao2 : UserControl
    {
        public NewuYiBiao2()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(104, 63);
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



        /// <summary>
        /// 仪表数值2
        /// </summary>
        public string NewuYiBiaoValue2
        {
            get { return txtScaleValue2.Text; }
            set { txtScaleValue2.Text = value; }
        }


        /// <summary>
        /// 仪表单位2
        /// </summary>
        public string NewuYiBiaoUnit2
        {
            get { return labUnit2.Text; }
            set
            {
                if (labUnit2.Text == value) return;
                labUnit2.Text = value;

                int w = labUnit2.Width;
                int x = this.Width - w;

                labUnit2.Location = new Point(x, labUnit2.Location.Y);
            }
        }

        public void SetTitle(string str)
        {
            this.btnScaleName.Text = str;
        }

    }


     
}
