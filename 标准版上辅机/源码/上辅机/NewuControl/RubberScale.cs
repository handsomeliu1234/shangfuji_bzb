using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NewuCommon
{
    public partial class RubberScale : UserControl
    {
        public RubberScale()
        {
            InitializeComponent();
        }
        bool ok = false;


        [Browsable(true)]
        public enum NewuPicStyle
        {
            RunnberScaleStop,
            RunnberScaleRun,
            RubberConveyorStop,
            RubberConveyorRun,

        }

        private NewuPicStyle _picStyle= NewuPicStyle.RunnberScaleStop;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public NewuPicStyle NewuPicDisplayStyle
        {
            get { return _picStyle; }
            set
            {
                //if (_picStyle == value) return;

                _picStyle = value;
                string picStr = "";
                switch (_picStyle)
                {
                    case NewuPicStyle.RunnberScaleStop:
                        picStr = "RubberScale.Fore";
                        break;
                    case NewuPicStyle.RunnberScaleRun:
                        picStr = "RubberScale.Run";
                        break;
                    case NewuPicStyle.RubberConveyorStop:
                        picStr = "RubberConveyor.Fore";
                        break;
                    case NewuPicStyle.RubberConveyorRun:
                        picStr = "RubberConveyor.Run";
                        break;
                    default:
                        picStr = "RubberScale.Fore";
                        break;
                }



                this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject(picStr)));
            }

        }

        public void setScaleState(bool ok)
        {
            if (this.ok == ok) return;
            this.ok = ok;
            if (ok)
            {
                  this.NewuPicDisplayStyle = NewuCommon.RubberScale.NewuPicStyle.RunnberScaleRun;
            }
            else
            {
                 this.NewuPicDisplayStyle = NewuCommon.RubberScale.NewuPicStyle.RubberConveyorStop;
            }
        }
    

    }

}
