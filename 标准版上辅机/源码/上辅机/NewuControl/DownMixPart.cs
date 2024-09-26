using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuCommon
{
    public class DownMixPart : UserControl
    {
        public DownMixPart()
        {
            InitializeComponent();
            picRawRun.BackColor = Color.Transparent;
            picRawStop.BackColor = Color.Transparent;

            picOutDoorClose.BackColor = Color.Transparent;
            picOutDoorOpen.BackColor = Color.Transparent;

            PicMixBg.Controls.Add(picRawRun);
            PicMixBg.Controls.Add(picRawStop);

            PicMixBg.Controls.Add(picOutDoorOpen);
            PicMixBg.Controls.Add(picOutDoorClose);
        }

        [Browsable(true)]
        public enum NewuPicStyle
        {
            Stop,
            Run
        }

        private bool _NewuOutDoorClose = true;
        private PictureBox PicMixBg;
        private PictureBox picRawStop;
        private PictureBox picRawRun;
        private PictureBox picOutDoorOpen;
        private PictureBox picOutDoorClose;

        /// <summary>
        /// 卸料门状态
        /// </summary>
        public bool NewuOutDoorClose
        {
            get
            {
                return _NewuOutDoorClose;
            }
            set
            {
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

        private NewuPicStyle _picStyle = NewuPicStyle.Stop;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public NewuPicStyle NewuMixRun
        {
            get
            {
                return _picStyle;
            }
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
                this.NewuMixRun = NewuPicStyle.Run;
            }
            else
            {
                this.NewuMixRun = NewuPicStyle.Stop;
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownMixPart));
            this.PicMixBg = new System.Windows.Forms.PictureBox();
            this.picRawStop = new System.Windows.Forms.PictureBox();
            this.picRawRun = new System.Windows.Forms.PictureBox();
            this.picOutDoorOpen = new System.Windows.Forms.PictureBox();
            this.picOutDoorClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicMixBg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRawStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRawRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOutDoorOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOutDoorClose)).BeginInit();
            this.SuspendLayout();
            // 
            // PicMixBg
            // 
            this.PicMixBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicMixBg.Image = ((System.Drawing.Image)(resources.GetObject("PicMixBg.Image")));
            this.PicMixBg.Location = new System.Drawing.Point(0, 0);
            this.PicMixBg.Name = "PicMixBg";
            this.PicMixBg.Size = new System.Drawing.Size(121, 66);
            this.PicMixBg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicMixBg.TabIndex = 0;
            this.PicMixBg.TabStop = false;
            // 
            // picRawStop
            // 
            this.picRawStop.Image = ((System.Drawing.Image)(resources.GetObject("picRawStop.Image")));
            this.picRawStop.Location = new System.Drawing.Point(31, 5);
            this.picRawStop.Name = "picRawStop";
            this.picRawStop.Size = new System.Drawing.Size(56, 23);
            this.picRawStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRawStop.TabIndex = 9;
            this.picRawStop.TabStop = false;
            // 
            // picRawRun
            // 
            this.picRawRun.Image = ((System.Drawing.Image)(resources.GetObject("picRawRun.Image")));
            this.picRawRun.Location = new System.Drawing.Point(31, 5);
            this.picRawRun.Name = "picRawRun";
            this.picRawRun.Size = new System.Drawing.Size(56, 23);
            this.picRawRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRawRun.TabIndex = 10;
            this.picRawRun.TabStop = false;
            this.picRawRun.Visible = false;
            // 
            // picOutDoorOpen
            // 
            this.picOutDoorOpen.Image = ((System.Drawing.Image)(resources.GetObject("picOutDoorOpen.Image")));
            this.picOutDoorOpen.Location = new System.Drawing.Point(28, 38);
            this.picOutDoorOpen.Name = "picOutDoorOpen";
            this.picOutDoorOpen.Size = new System.Drawing.Size(6, 20);
            this.picOutDoorOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOutDoorOpen.TabIndex = 12;
            this.picOutDoorOpen.TabStop = false;
            this.picOutDoorOpen.Visible = false;
            // 
            // picOutDoorClose
            // 
            this.picOutDoorClose.Image = ((System.Drawing.Image)(resources.GetObject("picOutDoorClose.Image")));
            this.picOutDoorClose.Location = new System.Drawing.Point(29, 38);
            this.picOutDoorClose.Name = "picOutDoorClose";
            this.picOutDoorClose.Size = new System.Drawing.Size(25, 6);
            this.picOutDoorClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOutDoorClose.TabIndex = 11;
            this.picOutDoorClose.TabStop = false;
            // 
            // DownMixPart
            // 
            this.Controls.Add(this.picOutDoorOpen);
            this.Controls.Add(this.picOutDoorClose);
            this.Controls.Add(this.picRawRun);
            this.Controls.Add(this.picRawStop);
            this.Controls.Add(this.PicMixBg);
            this.Name = "DownMixPart";
            this.Size = new System.Drawing.Size(121, 66);
            ((System.ComponentModel.ISupportInitialize)(this.PicMixBg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRawStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRawRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOutDoorOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOutDoorClose)).EndInit();
            this.ResumeLayout(false);

        }
    }
}