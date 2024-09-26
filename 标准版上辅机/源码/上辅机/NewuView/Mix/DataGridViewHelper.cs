using Repository.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewuCommon
{
    public class DataGridViewHelper
    {
        public DataGridViewHelper(DataGridView gridview)
        {
            gridview.CellPainting += new DataGridViewCellPaintingEventHandler(Gridview_CellPainting);
        }

        private int top = 0;
        private int left = 0;
        private int height = 0;
        private int width = 0;

        public void Gridview_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 重绘datagridview表头

            try
            {
                DataGridView dgv = (DataGridView)(sender);
                if (e.RowIndex != -1)
                    return;

                foreach (TopHeader item in Headers)
                {
                    if (e.ColumnIndex >= item.Index && e.ColumnIndex < item.Index + item.Span)
                    {
                        if (e.ColumnIndex == item.Index)
                        {
                            top = e.CellBounds.Top;
                            left = e.CellBounds.Left;
                            height = e.CellBounds.Height;
                        }

                        int dgvWidth = 0;//dgv宽度
                        for (int i = item.Index; i < item.Span + item.Index; i++)
                        {
                            dgvWidth += dgv.Columns[i].Width;
                        }

                        Rectangle rect = new Rectangle(left, top, dgvWidth, e.CellBounds.Height);

                        using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor)) //Cell背景颜色
                        {
                            //抹去原来的cell背景
                            e.Graphics.FillRectangle(backColorBrush, rect);
                        }

                        using (Pen gridLinePen = new Pen(dgv.GridColor)) //画笔颜色
                        {
                            e.Graphics.DrawLine(gridLinePen, left, top, left + dgvWidth, top);
                            e.Graphics.DrawLine(gridLinePen, left, top + height / 2, left + dgvWidth, top + height / 2);
                            e.Graphics.DrawLine(gridLinePen, left, top + height - 1, left + dgvWidth, top + height - 1); //自定义区域下部横线

                            width = 0;
                            e.Graphics.DrawLine(gridLinePen, left - 1, top, left - 1, top + height);
                            for (int i = item.Index; i < item.Span + item.Index; i++)
                            {
                                if (i == 1 || i == 2)
                                    width += dgv.Columns[i].Width - 1; //分隔区域首列
                                else
                                    width += dgv.Columns[i].Width;
                                if (i == (item.Span + item.Index - 2))
                                    e.Graphics.DrawLine(gridLinePen, left + width, top + height / 2, left + width, top + height);
                                else
                                    e.Graphics.DrawLine(gridLinePen, left + width, top, left + width, top + height);
                            }

                            SizeF sf = e.Graphics.MeasureString(item.Text, e.CellStyle.Font);
                            float lstr = (dgvWidth - sf.Width) / 2;
                            float rstr = (height / 2 - sf.Height) / 2;
                            //画出文本框
                            if (item.Text != "")
                            {
                                string headerTitle = NewuGlobal.GetRes(item.Text);
                                e.Graphics.DrawString(headerTitle, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), left + lstr, top + rstr, StringFormat.GenericDefault);
                            }

                            int columnWidth = 0;
                            width = 0;
                            for (int i = item.Index; i < item.Span + item.Index; i++)
                            {
                                string columnValue = dgv.Columns[i].HeaderText;
                                width = dgv.Columns[i].Width;
                                sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                                lstr = (width - sf.Width) / 2;
                                rstr = (height / 2 - sf.Height) / 2;

                                if (columnValue != "")
                                {
                                    e.Graphics.DrawString(columnValue, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), left + columnWidth + lstr, top + height / 2 + rstr, StringFormat.GenericDefault);
                                }
                                columnWidth += dgv.Columns[i].Width;
                            }
                        }
                        e.Handled = true;
                    }
                }
            }
            catch (Exception)
            {
            }

            #endregion 重绘datagridview表头
        }

        private List<TopHeader> _headers = new List<TopHeader>();

        public List<TopHeader> Headers
        {
            get
            {
                return _headers;
            }
        }

        public struct TopHeader
        {
            public TopHeader(int index, int span, string text)
            {
                this.Index = index;
                this.Span = span;
                this.Text = text;
            }

            public int Index;
            public int Span;
            public string Text;
        }
    }
}