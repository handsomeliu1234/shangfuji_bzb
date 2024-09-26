using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Repository.GlobalConfig;

namespace Newu.Report
{
    public partial class FM_RPT_RunHistory : Form
    {
        private NewuCommon.CSharedString share = NewuGlobal.MemDB;
        private List<NewuModel.RPT_RunHistoryMDL> list = new List<NewuModel.RPT_RunHistoryMDL>();
        private int timing = 1;
        private DateTime savetime;
        private DateTime lastSavetiem;
        private NewuBLL.PM_OrderTranBLL bllOrder = new NewuBLL.PM_OrderTranBLL();
        private string last = "";
        private string current = "";
        private string path = "E:\\Access";//"E:\\Access"

        public FM_RPT_RunHistory()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                getData();
                if (timing >= 10)
                {
                    saveData();
                }
                else
                {
                    timing++;
                }
                lastSavetiem = savetime;
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                MessageBox.Show("保存失败:" + ex.ToString());
            }
        }

        private void getData()
        {
            DataSet ds = bllOrder.getOrder();//得到生产数据和SaveTime
            NewuModel.RPT_RunHistoryMDL model = new NewuModel.RPT_RunHistoryMDL();
            DataRowCollection row = ds.Tables[0].Rows;
            model.WorkerName = row[0]["StartUserCode"].ToString();
            model.MemData = "100:01010101/1000:00001111";
            model.SaveTime = DateTime.Now;
            savetime = DateTime.Parse(row[0]["SaveTime"].ToString());   //OrderTran表的SaveTime用于检查数据库和表是否存在

            current = share.getStr(280, 4) + row[0]["OrderID"].ToString() + row[0]["DeviceCode"].ToString() + row[0]["MaterialCode"].ToString() + row[0]["VersionNo"].ToString();
            current += row[0]["Lot"].ToString() + row[0]["SetBatch"].ToString();
            if (current != last || savetime.ToString("yyyyMMdd") != lastSavetiem.ToString("yyyyMMdd"))
            {
                if (share.getStr(280, 4).ToString() != "\0\0\0\0")
                {
                    model.FactOrder = int.Parse(share.getStr(280, 4));
                }
                model.OrderID = row[0]["OrderID"].ToString();
                model.DeviceCode = row[0]["DeviceCode"].ToString();
                model.MateriCode = row[0]["MaterialCode"].ToString();
                model.VersionNo = row[0]["VersionNo"].ToString();
                model.Lot = row[0]["Lot"].ToString();
                if (row[0]["SetBatch"].ToString() != "")
                {
                    model.PlanQty = int.Parse(row[0]["SetBatch"].ToString());
                }
            }
            last = current;
            if (savetime.ToString("yyyyMMdd") != lastSavetiem.ToString("yyyyMMdd") && lastSavetiem.ToString() != "0001/1/1 0:00:00")
            {
                saveData();
            }
            list.Add(model);
        }

        private void saveData()
        {
            // 路径不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (lastSavetiem.ToString() == "0001/1/1 0:00:00") { lastSavetiem = savetime; }
            NewuBLL.RPT_RunHistoryBLL bllHistory = new NewuBLL.RPT_RunHistoryBLL(path, lastSavetiem);
            bllHistory.addList(list);

            timing = 1;
            list = new List<NewuModel.RPT_RunHistoryMDL>();
        }
    }
}