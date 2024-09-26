using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewuControl
{
    public class PaintClass
    {
        public static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            //把圆角矩形分成八段直线、弧的组合，依次加到路径中
            GraphicsPath graphicsPath = new GraphicsPath();

            //上直线
            graphicsPath.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right, rectangle.Y));

            //右边竖线
            graphicsPath.AddLine(new Point(rectangle.Right, rectangle.Y), new Point(rectangle.Right, rectangle.Bottom));

            //下边直线
            graphicsPath.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));

            //左下角圆弧
            graphicsPath.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            //左上角圆弧
            graphicsPath.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);

            return graphicsPath;
        }

        public static GraphicsPath GetCircle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            //把圆角矩形分成八段直线、弧的组合，依次加到路径中
            GraphicsPath graphicsPath = new GraphicsPath();

            //上直线
            //graphicsPath.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));

            //右上角圆弧
            graphicsPath.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);

            //右边竖线
            //graphicsPath.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));

            //右下角圆弧
            graphicsPath.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);

            //下边直线
            //graphicsPath.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));

            //左下角圆弧
            graphicsPath.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            //左边竖线
            //graphicsPath.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.Right, rectangle.Y + r));

            //左上角圆弧
            graphicsPath.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);

            return graphicsPath;
        }
    }
}