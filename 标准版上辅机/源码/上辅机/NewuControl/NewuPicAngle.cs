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
    public partial class NewuPicAngle : UserControl
    {
        public NewuPicAngle()
        {
            InitializeComponent();

            picPrivate.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;

            this.Controls.Add(picPrivate);
            this.Controls.Add(pictureBox1);
        }

        private void StartPaint()
        {
            if (NewuImgFore != null && NewuImgBg != null)
            {
                object obj = null;

                if (NewuPicTypeStyle == NewuPicType.Background)
                {
                    obj = NewuImgBg.Clone();
                }
                else
                {
                    obj = NewuImgFore.Clone();
                }

                Image objImg = (Image)obj;

                if (objImg != null)
                {
                    this.picPrivate.Image = objImg;
                }

                //todo:2018.6.25  关工指导  尝试解决内存溢出问题
                //Image tempImg = RotateImg(objImg, _angle);
                //if (tempImg != null)
                //{
                //    this.picPrivate.Image = tempImg;
                //}
            }
        }

        private Image RotateImg(Image b, int angle)
        {
            try
            {
                angle = angle % 360;

                //弧度转换

                double radian = angle * Math.PI / 180.0;

                double cos = Math.Cos(radian);

                double sin = Math.Sin(radian);

                //原图的宽和高

                int w = b.Width;

                int h = b.Height;

                int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));

                int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

                //目标位图

                Bitmap dsImage = new Bitmap(W, H);

                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //计算偏移量

                Point Offset = new Point((W - w) / 2, (H - h) / 2);

                //构造图像显示区域：让图像的中心与窗口的中心点一致

                Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);

                Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

                g.TranslateTransform(center.X, center.Y);

                g.RotateTransform(360 - angle);

                //恢复图像在水平和垂直方向的平移

                g.TranslateTransform(-center.X, -center.Y);

                g.DrawImage(b, rect);

                //重至绘图的所有变换

                g.ResetTransform();

                g.Save();

                g.Dispose();

                //保存旋转后的图片

                b.Dispose();

                //dsImage.Save("FocusPoint.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                return dsImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int _angle;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int NewuAngle
        {
            get
            {
                return _angle;
            }

            set
            {
                //if (_angle == value) return;

                if (_angle >= 360)
                {
                    _angle = 0;
                }
                else
                {
                    _angle = value;
                }

                StartPaint();
            }
        }

        private Image _NewuImgFore;

        public Image NewuImgFore
        {
            get
            {
                return _NewuImgFore;
            }
            set
            {
                _NewuImgFore = value;
                StartPaint();
            }
        }

        private Image _NewuImgBg;

        public Image NewuImgBg
        {
            get
            {
                return _NewuImgBg;
            }
            set
            {
                _NewuImgBg = value;
                StartPaint();
            }
        }

        private NewuPicType _NewuPicTypeStyle = NewuPicType.Background;

        public NewuPicType NewuPicTypeStyle
        {
            get
            {
                return _NewuPicTypeStyle;
            }
            set
            {
                //if (_NewuPicTypeStyle == value) return;

                _NewuPicTypeStyle = value;
                if (_NewuPicTypeStyle == NewuPicType.Background)
                {
                    if (NewuImgBg != null)
                        picPrivate.Image = NewuImgBg;
                }
                else
                {
                    if (NewuImgFore != null)
                        picPrivate.Image = NewuImgFore;
                }

                StartPaint();
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            StartPaint();
        }

        private bool tag = false;

        public void setImageTag(bool tag)
        {
            if (this.tag == tag)
                return;
            this.tag = tag;
            if (tag)
            {
                this.NewuPicTypeStyle = NewuPicType.Background;
            }
            else
            {
                this.NewuPicTypeStyle = NewuPicType.Foreground;
            }
        }
    }
}