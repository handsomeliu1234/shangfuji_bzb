using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewuCommon
{
    public static class StaticCom
    {
        public static string GetDataGridViewID(DataGridView dgv, int ColIndex)
        {
            if (ColIndex >= dgv.Columns.Count)
            {
                return "";
            }
            if (dgv.Rows.Count == 0)
            {
                return "";
            }
            if (dgv.CurrentCell == null)
            {
                return "";
            }

            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                return dgv[ColIndex, rowIndex].Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public static string GetDataGridViewID(DataGridView dgv, string ColumnName)
        {
            if (dgv.Columns.Contains(ColumnName) == false)
            {
                return "";
            }

            if (dgv.Rows.Count == 0)
            {
                return "";
            }
            if (dgv.CurrentCell == null)
            {
                return "";
            }

            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                return dgv.Rows[rowIndex].Cells[ColumnName].Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public static void AddFmToContainer(Control _control, Form fm)
        {
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.WindowState = FormWindowState.Normal;
            fm.Dock = DockStyle.Fill;
            fm.Parent = _control;
        }
    }
}