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
    public partial class MixPart : UserControl
    {
        public MixPart()
        {
            InitializeComponent();

            PicShuanUp.BackColor = Color.Transparent;
            PicShuanDown.BackColor = Color.Transparent;
            PicInDoorOpen.BackColor = Color.Transparent;
            PicInDoorClose.BackColor = Color.Transparent;

            picRawRun.BackColor = Color.Transparent;
            picRawStop.BackColor = Color.Transparent;

            picOutDoorClose.BackColor = Color.Transparent;
            picOutDoorOpen.BackColor = Color.Transparent;

            PicMixBg.Controls.Add(PicShuanUp);
            PicMixBg.Controls.Add(PicShuanDown);
            PicMixBg.Controls.Add(PicInDoorOpen);
            PicMixBg.Controls.Add(PicInDoorClose);
            PicMixBg.Controls.Add(picRawRun);
            PicMixBg.Controls.Add(picRawStop);

            PicMixBg.Controls.Add(picOutDoorOpen);
            PicMixBg.Controls.Add(picOutDoorClose);
        }

        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(121, 251);
        }

        public enum ShuanLocation
        {
            high,
            middle,
            low
        }

        [Browsable(true)]
        public enum NewuPicStyle
        {
            Stop,
            Run
        }

        private bool _NewuInDoorState = false;

        /// <summary>
        /// 投料门状态
        /// </summary>
        public bool NewuInDoorState
        {
            get { return _NewuInDoorState; }
            set
            {
                //if (_NewuInDoorState == value) return;

                _NewuInDoorState = value;

                if (_NewuInDoorState == true)
                {
                    PicInDoorOpen.Visible = true;
                    PicInDoorClose.Visible = false;
                }
                else
                {
                    PicInDoorOpen.Visible = false;
                    PicInDoorClose.Visible = true;
                }
            }
        }

        private bool _NewuOutDoorClose = true;

        /// <summary>
        /// 卸料门状态
        /// </summary>
        public bool NewuOutDoorClose
        {
            get { return _NewuOutDoorClose; }
            set
            {
                //if (_NewuOutDoorClose == value) return;
                _NewuOutDoorClose = value;

                if (_NewuOutDoorClose == true)
                {
                    picOutDoorOpen.Visible = false;
                    picOutDoorClose.Visible = true;
                }
                else
                {
                    picOutDoorOpen.Visible = true;
                    picOutDoorClose.Visible = false;
                }
            }
        }

        public bool _NewuDownShuanClose = true;

        /// <summary>
        /// 下定栓位置
        /// </summary>
        private bool NewuDownShuanClose
        {
            get { return _NewuDownShuanClose; }
            set
            {
                //if (_NewuDownShuanClose == value) return;
                _NewuDownShuanClose = value;
                PicShuanDown.Visible = _NewuDownShuanClose;
            }
        }

        private ShuanLocation _NewuShuanLocation = ShuanLocation.low;

        /// <summary>
        /// 上顶栓位置
        /// </summary>
        public ShuanLocation NewuShuanLocation
        {
            get { return _NewuShuanLocation; }
            set
            {
                //if (_NewuShuanLocation == value) return;
                _NewuShuanLocation = value;
                switch (_NewuShuanLocation)
                {
                    case ShuanLocation.high:
                        PicShuanUp.Location = new Point(PicShuanUp.Location.X, 15);
                        break;

                    case ShuanLocation.middle:
                        PicShuanUp.Location = new Point(PicShuanUp.Location.X, 51);
                        break;

                    case ShuanLocation.low:
                        PicShuanUp.Location = new Point(PicShuanUp.Location.X, 90);
                        break;

                    default:
                        break;
                }
            }
        }

        private NewuPicStyle _picStyle = NewuPicStyle.Stop;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public NewuPicStyle NewuMixRun
        {
            get { return _picStyle; }
            set
            {
                //if (_picStyle == value) return;
                _picStyle = value;

                switch (_picStyle)
                {
                    case NewuPicStyle.Stop:
                        picRawStop.Visible = true;
                        picRawRun.Visible = false;

                        break;

                    case NewuPicStyle.Run:
                        picRawStop.Visible = false;
                        picRawRun.Visible = true;

                        break;

                    default:
                        picRawStop.Visible = false;
                        picRawRun.Visible = true;

                        break;
                }
            }
        }

        /// <summary>
        /// 设置运行状态   面向对象化  下同
        /// </summary>
        /// <param name="run"></param>
        public void SetMixRun(bool run)
        {
            if (run)
            {
                this.NewuMixRun = NewuCommon.MixPart.NewuPicStyle.Run;
            }
            else
            {
                this.NewuMixRun = NewuCommon.MixPart.NewuPicStyle.Stop;
            }
        }

        /// <summary>
        /// 设置进料门开
        /// </summary>
        /// <param name="open"></param>
        /// <param name="close"></param>
        public void SetMixInDoor(bool open, bool close)
        {
            if (open)
            {
                this.NewuInDoorState = true;
            }
            else if (close)
            {
                this.NewuInDoorState = false;
            }
        }

        /// <summary>
        /// 设置卸料口开
        /// </summary>
        /// <param name="open"></param>
        /// <param name="close"></param>
        public void SetMixOutDoor(bool open, bool close)
        {
            if (open)
            {
                this.NewuOutDoorClose = false;
            }
            else if (close)//关
            {
                this.NewuOutDoorClose = true;
            }
        }

        /// <summary>
        /// 设置上顶栓 位置
        /// </summary>
        /// <param name="high"></param>
        /// <param name="mid"></param>
        /// <param name="low"></param>
        public void SetMixShuanLocation(bool high, bool mid, bool low)
        {
            //上顶栓高、中、低位
            if (high)
            {
                this.NewuShuanLocation = NewuCommon.MixPart.ShuanLocation.high;
            }
            else if (mid)
            {
                this.NewuShuanLocation = NewuCommon.MixPart.ShuanLocation.middle;
            }
            else if (low)
            {
                this.NewuShuanLocation = NewuCommon.MixPart.ShuanLocation.low;
            }
        }
    }
}