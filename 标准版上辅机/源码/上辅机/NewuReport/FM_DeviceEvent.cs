using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NewuBLL;
using NewuCommon;
using Repository.GlobalConfig;

namespace Newu.Report
{
    public partial class FM_DeviceEvent : Form
    {
        public FM_DeviceEvent()
        {
            InitializeComponent();
        }

        private NewuBLL.RPT_DeviceEventBLL DeviceEventBLL = new NewuBLL.RPT_DeviceEventBLL();
        private NewuModel.RPT_DeviceEventMDL DeviceEventModle = new NewuModel.RPT_DeviceEventMDL();
        private NewuBLL.PM_OrderTranBLL OrderTranBll = new NewuBLL.PM_OrderTranBLL();
        private NewuModel.PM_OrderTranMDL OrderTranModel = new NewuModel.PM_OrderTranMDL();

        private string DeviceCode = NewuGlobal.SoftConfig.DeviceCode;
        private bool IsExists = false;
        private bool Islock = false;
        private bool Moni = false;
        private string DeviceEventID = "";
        private string Endtime = "";

        private void FM_RPT_DeviceEvent_Load(object sender, EventArgs e)
        {
            //IsExists = DeviceEventBLL.IsExists(DeviceCode);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Getdata(1);
        }

        private void Getdata(int flag)
        {
            OrderTranModel = OrderTranBll.GetModelToDeviceCode(DeviceCode, NewuGlobal.GetDevicePartCode(DevicePartType.MixUp));
            IsExists = DeviceEventBLL.QueryExists(DeviceCode, OrderTranModel.Savetime, out DeviceEventID, out Endtime, out Moni);
            Islock = Moni;
            if (Islock && flag == 1)
            {
                if (!IsExists)
                {
                    bool Istrue = InsertData(EventType.DeviceRun.ToString());
                    if (Istrue) { Islock = false; }
                }
                else
                {
                    bool Istrue = Update(EventType.DeviceRun.ToString());
                    if (Istrue) { Islock = false; }
                }
            }
            else if (!Islock && flag == 0)
            {
                if (!IsExists)
                {
                    bool Istrue = InsertData(EventType.DeviceStop.ToString());
                    if (Istrue) { Islock = true; }
                }
                else
                {
                    bool Istrue = Update(EventType.DeviceStop.ToString());
                    if (Istrue) { Islock = true; }
                }
            }
        }

        private bool InsertData(string Type)
        {
            InsertorderData(Type);

            bool Add = DeviceEventBLL.Add(DeviceEventModle, OrderTranModel.Savetime.Year.ToString());
            if (Add)
            {
                MessageBox.Show("操作成功！");
                return true;
            }
            else
            {
                MessageBox.Show("插入数据失败！");
                return false;
            }
        }

        private bool Update(string Type)
        {
            //更新停机的数据，新增一笔运行的数据。
            IsExists = DeviceEventBLL.QueryExists(DeviceCode, OrderTranModel.Savetime, out DeviceEventID, out Endtime, out Moni);
            bool Update = false;
            if (Endtime == "")
            {
                Update = DeviceEventBLL.UpdatetoDeviceEventID(DeviceEventID, DateTime.Now, OrderTranModel.Savetime);
            }
            InsertorderData(Type);
            bool Add = DeviceEventBLL.Add(DeviceEventModle, OrderTranModel.Savetime.Year.ToString());
            if (Add)
            {
                MessageBox.Show("操作成功！");
                return true;
            }
            else
            {
                MessageBox.Show("数据错误！");
                return false;
            }
        }

        private void InsertorderData(string Type)
        {
            DeviceEventModle.EventType = Type;
            DeviceEventModle.StartTime = DateTime.Now;
            DeviceEventModle.InitTime = DateTime.Now;
            DeviceEventModle.PmMode = PmMode.Automatic.ToString();
            DeviceEventModle.DeviceCode = NewuGlobal.SoftConfig.DeviceCode;

            if (OrderTranModel == null) return;

            if (OrderTranModel.MaterialCode != "") DeviceEventModle.MaterialCode = OrderTranModel.MaterialCode;
            if (OrderTranModel.VersionNo != "") DeviceEventModle.VersionNo = OrderTranModel.VersionNo;
            if (OrderTranModel.OrderID != "") DeviceEventModle.OrderID = OrderTranModel.OrderID;
            if (OrderTranModel.Lot != "") DeviceEventModle.Lot = OrderTranModel.Lot;
            if (OrderTranModel.SetBatch.ToString() != "") DeviceEventModle.PlanQty = OrderTranModel.SetBatch;
            if (OrderTranModel.WorkGroup != "") DeviceEventModle.WorkGroup = OrderTranModel.WorkGroup;
            if (OrderTranModel.WorkOrder != "") DeviceEventModle.WorkOrder = OrderTranModel.WorkOrder;
            if (OrderTranModel.StartUserCode != "") DeviceEventModle.WorkerUserCode = OrderTranModel.StartUserCode;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Getdata(0);
        }
    }
}