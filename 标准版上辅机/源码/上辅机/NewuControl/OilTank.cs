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
    public partial class OilTank : UserControl
    {
        public OilTank()
        {
            InitializeComponent();
        }

        private void OilTank_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            pictureBox1.Controls.Add(label1);
        }

        public void NewuSetLiaoWei(bool _high, bool _low)
        {

            if (_high == true)
            {
                lblHigh.BackColor = Color.Lime;
            }
            else
            {
                lblHigh.BackColor = Color.DimGray;
            }
            if (_low == true)
            {
                lblLow.BackColor = Color.Lime;
            }
            else
            {
                lblLow.BackColor = Color.DimGray;
            }
        }

        private void OilTank_SizeChanged(object sender, EventArgs e)
        {
         

            //换行物料名称
            string _BinText = label1.Text.Replace("\r\n", "");
            label1.Text = "";
            for (int i = 0; i < _BinText.Length; i++)
            {
                label1.Text += _BinText.Substring(i, 1);
                // label1.Width >= this.Width -10显示全所有的物料名称
                if (label1.Width >= this.Width -10)
                {
                    label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);
                    label1.Text = label1.Text + "\r\n" + _BinText.Substring(i, 1);
                }

            }

            double y=(double) (this.Height - label1.Height) / 1.5;

            label1.Location = new Point((this.Width - label1.Width) / 2, Convert.ToInt32(y));
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string NewuLabText
        {
            get { return label1.Text; }
            set
            {
                if (label1.Text == value) return;

                label1.Text = value;
                OilTank_SizeChanged(null, null);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color NewuLabForeColor
        {
            get { return label1.ForeColor; }
            set
            {
                label1.ForeColor = value;
                
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font NewuLabFont
        {
            get { return label1.Font; }
            set
            {
                label1.Font = value;

            }
        }
    }
}
