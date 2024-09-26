using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace NewuCommon
{
    public partial class ScaleFinal : UserControl
    {
        public ScaleFinal()
        {
            InitializeComponent();

            toolTip1.SetToolTip(picManual, "手动运行");
            toolTip1.SetToolTip(picAlarm, "报警");
            toolTip1.SetToolTip(picAuto, "自动模式");
            toolTip1.SetToolTip(picRun, " 开始运行");

            picManual.BackColor = Color.Transparent;
            picAlarm.BackColor = Color.Transparent;
            picAuto.BackColor = Color.Transparent;
            picRun.BackColor = Color.Transparent;

            panel1.Controls.Add(picManual);
            panel1.Controls.Add(picAlarm);
            panel1.Controls.Add(picAuto);
            panel1.Controls.Add(picRun);
        }

        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(216, 124);
        }

        private bool _NewuScaleAuto = true;

        /// <summary>
        /// 磅秤自动模式或手动模式
        /// </summary>
        public bool NewuScaleAuto
        {
            get
            {
                return _NewuScaleAuto;
            }
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

        private bool _NewuScaleOverAlarm = false;

        /// <summary>
        /// 磅秤是否超差报警
        /// </summary>
        public bool NewuScaleOverAlarm
        {
            get
            {
                return _NewuScaleOverAlarm;
            }
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
            get
            {
                return _NewuScaleStartRun;
            }
            set
            {
                //if (_NewuScaleStartRun == value) return;
                _NewuScaleStartRun = value;

                picRun.Visible = value;
            }
        }

        private bool _isChengOk1 = false;

        [Browsable(true)]
        [Description(" 称好信号"), Category("Newu")]
        public bool IsChengOK1
        {
            get
            {
                return _isChengOk1;
            }
            set
            {
                _isChengOk1 = value;
                if (_isChengOk1 == true)
                {
                    txtScaleValue.BackColor = Color.GreenYellow;
                    txtScaleValue.ForeColor = Color.Black;
                }
                else
                {
                    txtScaleValue.BackColor = Color.Black;
                    txtScaleValue.ForeColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 磅秤名称
        /// </summary>
        public string NewuScaleNameTxt
        {
            get
            {
                return btnScaleName.Text;
            }
            set
            {
                btnScaleName.Text = value;
            }
        }

        /// <summary>
        /// 磅秤数值
        /// </summary>
        public string NewuScaleValue
        {
            get
            {
                return txtScaleValue.Text;
            }
            set
            {
                txtScaleValue.Text = value;
            }
        }

        /// <summary>
        /// 磅秤车数
        /// </summary>
        public string NewuScaleBatch
        {
            get
            {
                return labScaleBatch.Text;
            }
            set
            {
                labScaleBatch.Text = value;
            }
        }

        /// <summary>
        /// 仪表单位
        /// </summary>
        public string NewuYiBiaoUnit
        {
            get
            {
                return labUnit.Text;
            }
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
            get
            {
                return _NewuScaleToolTip;
            }
            set
            {
                //if (_NewuScaleToolTip == value) return;
                _NewuScaleToolTip = value;
                toolTip1.SetToolTip(txtScaleValue, _NewuScaleToolTip);
            }
        }

        public void SetTitle(string str)
        {
            this.btnScaleName.Text = str;
        }
    }
}