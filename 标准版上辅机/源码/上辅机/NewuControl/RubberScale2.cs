using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class RubberScale2 : RubberScale
    {
        bool _sensorState = false;
        public RubberScale2()
        {
            InitializeComponent();
        }
        public void setScaleSensor(bool _sensorState)
        {
            if (this._sensorState == _sensorState) return;
            this._sensorState = _sensorState;
            this.Sensor.Visible = _sensorState;
        }
        /// <summary>
        /// 胶料称，传送皮带的动作和光电信号显示
        /// </summary>
        /// <param name="_scaleState">电机动作</param>
        /// <param name="_sensorState">光电开关</param>
        public void setScaleState(bool _scaleState, bool _sensorState)
        {
            base.setScaleState(_scaleState);
            this.setScaleSensor(_sensorState);
        }

        private void RubberScale2_Resize(object sender, EventArgs e)
        {
            this.Sensor.Size = new Size(this.Size.Width/5,this.Size.Height/5);
        }
    }
}
