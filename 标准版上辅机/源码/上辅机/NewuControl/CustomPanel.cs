using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NewuControl
{
    public partial class CustomPanel : UserControl
    {
        public CustomPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//忽略窗口信息，减少闪烁
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//绘制到缓存区，减少闪烁
            SetStyle(ControlStyles.UserPaint, true);//控件由其自身，而不是操作系统绘制
            SetStyle(ControlStyles.ResizeRedraw, true);//控件调整大小时重绘
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);//支持透明背景
        }

        private Color bgColor = Color.LightGray;

        [DefaultValue(typeof(Color), "LightGray"), Description("控件背景色1")]
        public Color BgColor
        {
            get
            {
                return bgColor;
            }
            set
            {
                bgColor = value;
                Invalidate();//引发重绘，不会立即执行
            }
        }

        private Color bgColor2 = Color.Transparent;

        [DefaultValue(typeof(Color), "Transparent"), Description("控件背景色2")]
        public Color BgColor2
        {
            get
            {
                return bgColor2;
            }
            set
            {
                bgColor2 = value;
                Invalidate();//引发重绘，不会立即执行
            }
        }

        private Color borderColor = Color.Gray;

        [DefaultValue(typeof(Color), "LightGray"), Description("控件边框颜色")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                Invalidate();//引发重绘，不会立即执行
            }
        }

        private int borderWidth = 0;

        [DefaultValue(typeof(int), "0"), Description("控件边框粗细")]
        public int BorderWidth
        {
            get
            {
                return borderWidth;
            }
            set
            {
                borderWidth = value;
                Invalidate();//引发重绘，不会立即执行
            }
        }

        private int radius = 5;

        [DefaultValue(typeof(int), "5"), Description("控件边框圆角半径")]
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                Invalidate();
            }
        }

        private LinearGradientMode gradientMode = LinearGradientMode.Vertical;

        [DefaultValue(typeof(LinearGradientMode), "Vertical"), Description("控件背景涩渐变模式")]
        public LinearGradientMode GradientMode
        {
            get
            {
                return gradientMode;
            }
            set
            {
                gradientMode = value;
                Invalidate();
            }
        }

        private Rectangle r;//绘制区域

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnAutoSizeChanged(e);
            r = this.ClientRectangle;//获取当前绘制区域
            this.Region = new Region(r);
            r.Width -= 1;
            r.Height -= 1;
        }

        //重写描绘
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //绘制对象
            Graphics graphics = e.Graphics;

            //呈现质量
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            //内部填充矩形的路径
            Rectangle rect;
            //生成外部矩形的路径
            path1 = PaintClass.GetRoundRectangle(r, radius);

            if (this.borderWidth > 0)
            {
                //填充外部矩形----边框
                graphics.FillPath(new SolidBrush(BorderColor), path1);

                //定义内部矩形
                rect = new Rectangle(r.X + borderWidth, r.Y + borderWidth, r.Width - 2 * borderWidth, r.Height - 2 * borderWidth);

                //生成内部矩形圆角路径
                path2 = PaintClass.GetRoundRectangle(rect, radius - 1);
            }
            else //无边框时的内部矩形
            {
                path2 = path1;
                rect = r;
            }

            if (this.BgColor2 != Color.Transparent)
            {
                //线性渐变画刷
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, BgColor, BgColor2, GradientMode);
                graphics.FillPath(linearGradientBrush, path2);//填充圆角矩形内部
            }
            else
            {
                SolidBrush solidBrush = new SolidBrush(BgColor);
                graphics.FillPath(solidBrush, path2);// 填充圆角矩形内部
            }
        }
    }
}