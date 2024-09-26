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
    public partial class CarbonBin : UserControl
    {
        public CarbonBin()
        {
            InitializeComponent();

            toolTip1.SetToolTip(label1, label1.Text);

            label1.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.Controls.Add(label1);
        }

        private NewuLiaoWei _liaowei;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public NewuLiaoWei NewuLiaoWei
        {
            get
            {
                return _liaowei;
            }

            set
            {
                //if (_liaowei == value) return;
                int locationXIndex = 0;
                switch (value)
                {
                    case NewuLiaoWei.none:
                        locationXIndex = 0;
                        break;

                    case NewuLiaoWei.Low:
                        locationXIndex = 1;
                        break;

                    case NewuLiaoWei.Mid:
                        locationXIndex = 2;
                        break;

                    case NewuLiaoWei.High:
                        locationXIndex = 3;
                        break;
                        //case NewuLiaoWei.SupperHigh:
                        //    locationXIndex = 4;
                        //    break;
                }

                _liaowei = value;

                pictureBox1.Location = new Point(-panel1.Width * locationXIndex, 0);
                int startX = 1 + (-pictureBox1.Location.X);
                label1.Location = new Point(startX, pictureBox1.Location.Y + 15);
            }
        }

        private int tempLocation = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public void NewuSet料位(bool _supperHigh, bool _high, bool _mid, bool _low)
        {
            int locationXIndex = 0;
            if (_supperHigh == true)
            {
                locationXIndex = 4;
            }
            else if (_high == true)
            {
                locationXIndex = 3;
            }
            else if (_mid == true)
            {
                locationXIndex = 2;
            }
            else if (_low == true)
            {
                locationXIndex = 1;
            }
            else
            {
                locationXIndex = 0;
            }
            //if (tempLocation == locationXIndex) return;
            tempLocation = locationXIndex;

            pictureBox1.Location = new Point(-panel1.Width * locationXIndex, 0);
            int startX = 1 + (-pictureBox1.Location.X);
            label1.Location = new Point(startX, pictureBox1.Location.Y + 30);
        }

        public void NewuSet料位(int num)
        {
            int locationXIndex = 0;
            if (num / 1000 == 1)
            {
                locationXIndex = 4;
            }
            else if (num / 100 == 1)
            {
                locationXIndex = 3;
            }
            else if (num / 10 == 1)
            {
                locationXIndex = 2;
            }
            else if (num % 10 == 1)
            {
                locationXIndex = 1;
            }
            else
            {
                locationXIndex = 0;
            }
            if (tempLocation == locationXIndex)
                return;
            tempLocation = locationXIndex;

            pictureBox1.Location = new Point(-panel1.Width * locationXIndex, 0);
            int startX = 1 + (-pictureBox1.Location.X);
            label1.Location = new Point(startX, pictureBox1.Location.Y + 30);
        }

        private void CarbonBin_SizeChanged(object sender, EventArgs e)
        {
            //先确定pannel的位置
            panel1.Width = this.Width - 30;
            panel1.Height = this.Height;
            panel1.Location = new Point(15, 0);

            // 再确定picturebox的位置
            pictureBox1.Width = panel1.Width * 5;
            pictureBox1.Height = panel1.Height;
            pictureBox1.Location = new Point(0, 0);

            //换行物料名称
            string _BinText = label1.Text.Replace("\r\n", "");
            label1.Text = "";
            for (int i = 0; i < _BinText.Length; i++)
            {
                label1.Text += _BinText.Substring(i, 1);

                if (label1.Width > panel1.Width)
                {
                    label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);
                    label1.Text = label1.Text + "\r\n" + _BinText.Substring(i, 1);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string NewuLabText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
                toolTip1.SetToolTip(label1, label1.Text);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font NewuLabFont
        {
            get
            {
                return label1.Font;
            }
            set
            {
                label1.Font = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color NewuLabForeColor
        {
            get
            {
                return label1.ForeColor;
            }
            set
            {
                label1.ForeColor = value;
            }
        }
    }
}