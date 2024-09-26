using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class LuoXuan : UserControl
    {
        public LuoXuan()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            NewuPicDisplayType = NewuPicType.Foreground;
        }

        private void LuoXuan_SizeChanged(object sender, EventArgs e)
        {
        }

        private NewuPicType _picType = NewuPicType.Foreground;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public NewuPicType NewuPicDisplayType
        {
            get
            {
                return _picType;
            }
            set
            {
                _picType = value;

                if (NewuPicDisplayType == NewuPicType.Foreground)
                {
                    this.picStop.Visible = true;

                    this.picRun.Visible = false;
                    this.picStopRight.Visible = false;
                    this.picRunRight.Visible = false;
                }
                else if (NewuPicDisplayType == NewuPicType.Background)
                {
                    this.picRun.Visible = true;

                    this.picStop.Visible = false;
                    this.picStopRight.Visible = false;
                    this.picRunRight.Visible = false;
                }

                if (NewuPicDisplayType == NewuPicType.ForegroundRight)
                {
                    this.picStopRight.Visible = true;

                    this.picRunRight.Visible = false;
                    this.picStop.Visible = false;
                    this.picRun.Visible = false;
                }
                else if (NewuPicDisplayType == NewuPicType.BackgroundRight)
                {
                    this.picRunRight.Visible = true;

                    this.picStopRight.Visible = false;
                    this.picStop.Visible = false;
                    this.picRun.Visible = false;
                }
            }
        }

        private int _NewuRunState = 0;

        public int NewuRunState
        {
            get
            {
                return _NewuRunState;
            }
            set
            {
                _NewuRunState = value;
                if (_NewuRunState == 0)
                {
                    this.picRun.Visible = false;
                    this.picRunRight.Visible = false;
                    this.picRun.Visible = false;

                    this.picStop.Visible = true;
                }
                else if (_NewuRunState == 1)
                {
                    this.picStop.Visible = false;
                    this.picStopRight.Visible = false;
                    this.picRunRight.Visible = false;

                    this.picRun.Visible = true;
                }
                else if (_NewuRunState == 2)
                {
                    this.picStop.Visible = false;
                    this.picRunRight.Visible = false;
                    this.picRun.Visible = false;

                    this.picStopRight.Visible = true;
                }
                else if (_NewuRunState == 3)
                {
                    this.picStop.Visible = false;
                    this.picStopRight.Visible = false;
                    this.picRun.Visible = false;

                    this.picRunRight.Visible = true;
                }
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // 在这里添加绘制代码
        //    base.OnPaint(e);
        //}
    }
}