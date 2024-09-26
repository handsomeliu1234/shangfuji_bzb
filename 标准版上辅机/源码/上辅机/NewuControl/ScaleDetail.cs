using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace NewuCommon
{
    public partial class ScaleDetail : UserControl
    {
        const int thisW = 161;
        int thisH = 141;
        public ScaleDetail()
        {
            InitializeComponent();

            toolTip1.SetToolTip(picManual, "手动运行");
            toolTip1.SetToolTip(picAlarm, "报警");
            toolTip1.SetToolTip(picAuto, "自动模式");
            toolTip1.SetToolTip(picRun, "开始运行");
            


            picManual.BackColor = Color.Transparent;
            picAlarm.BackColor = Color.Transparent;
            picAuto.BackColor = Color.Transparent;

            panel1.Controls.Add(picManual);
            panel1.Controls.Add(picAlarm);
            panel1.Controls.Add(picAuto);
        }



        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(thisW, thisH);
        }


        private bool _NewuScaleAuto = true;

        /// <summary>
        /// 磅秤自动模式或手动模式
        /// </summary>
        public Boolean NewuScaleAuto
        {
            get { return _NewuScaleAuto; }
            set
            {
                //if (_NewuScaleAuto == value) return;

                _NewuScaleAuto = value;

                if (_NewuScaleAuto == true)
                {
                    picAuto.Visible = true;
                    picManual.Visible = false;
                }
                else
                {
                    picAuto.Visible = false;
                    picManual.Visible = true;
                }

            }
        }


        private bool _NewuScaleOverAlarm=false;

        /// <summary>
        /// 磅秤是否超差报警
        /// </summary>
        public bool NewuScaleOverAlarm
        {
            get { return _NewuScaleOverAlarm; }
            set
            {
                //if (_NewuScaleOverAlarm == value) return;

                _NewuScaleOverAlarm = value;

                picAlarm.Visible = value;

            }
        }



        private bool _NewuScaleStartRun = false;

        /// <summary>
        /// 磅秤开始称量
        /// </summary>
        public bool NewuScaleStartRun
        {
            get { return _NewuScaleStartRun; }
            set
            {
                //if (_NewuScaleStartRun == value) return;
                _NewuScaleStartRun = value;

                picRun.Visible = value;

            }
        }


        /// <summary>
        /// 磅秤名称
        /// </summary>
        public string NewuScaleNameTxt
        {
            get { return btnScaleName.Text; }
            set { btnScaleName.Text = value; }
        }


        /// <summary>
        /// 磅秤数值
        /// </summary>
        public string NewuScaleValue
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

                labUnit.Location = new Point(x, labUnit.Location.Y);
            }
        }


        private string _NewuScaleToolTip = "";
        /// <summary>
        /// 磅秤提示
        /// </summary>
        public string NewuScaleToolTip
        {
            get { return _NewuScaleToolTip; }
            set
            {
                //if (_NewuScaleToolTip == value) return;
                _NewuScaleToolTip = value;
                toolTip1.SetToolTip(txtScaleValue, _NewuScaleToolTip);
            }
        }



        private string _NewuDisplayMsg = "";
        /// <summary>
        /// 显示信息
        /// </summary>
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string NewuDisplayMsg
        {
            get { return _NewuDisplayMsg; }
            set
            {
                //if (_NewuDisplayMsg == value) return;

                _NewuDisplayMsg = value;
                labDisplay.Text = _NewuDisplayMsg;

                if (_NewuDisplayMsg == "")
                {
                    int y =  txtScaleValue.Location.Y + txtScaleValue.Height;
                    panel1.Location = new Point(panel1.Location.X, y+1);
                }
                else
                {
                    int y = labDisplay.Location.Y + labDisplay.Height;


                    panel1.Location = new Point(panel1.Location.X, y+2);
                }

                thisH = panel1.Location.Y + panel1.Height + 2;
                OnResize(null);

            }
        }

        public void showPlcDate(double val, string msg, bool isAuto, bool isOver, bool isRun)
        {

            NewuScaleValue = val.ToString();
            NewuDisplayMsg = msg;
            NewuScaleAuto = isAuto;
            NewuScaleOverAlarm = isOver;
            NewuScaleStartRun = isRun;
        }

    }
}
