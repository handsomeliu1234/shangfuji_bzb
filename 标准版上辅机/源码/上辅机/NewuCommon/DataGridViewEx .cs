using Newu;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NewuCommon
{
    public struct ColStruct
    {
        public ColStruct(string _Field, string _colHeader, ColumnType _colType, bool _colVisible)
        {
            ColType = _colType;
            ColHeader = _colHeader;
            ColVisible = _colVisible;
            ColField = _Field;
            ColName = _Field;
            CommboxStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        }

        public ColStruct(string _Field, string _colHeader, DataGridViewComboBoxDisplayStyle _commboxStyle, bool _colVisible)
        {
            ColType = ColumnType.cmb;
            ColHeader = _colHeader;
            ColVisible = _colVisible;
            ColField = _Field;
            ColName = _Field;
            CommboxStyle = _commboxStyle;
        }

        public ColStruct(string _Field, string _colHeader)
        {
            ColType = ColumnType.txt;
            ColHeader = _colHeader;
            ColVisible = true;
            ColField = _Field;
            ColName = _Field;
            CommboxStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        }

        public ColumnType ColType;
        public string ColHeader;
        public bool ColVisible;
        public string ColField;
        public string ColName;
        public DataGridViewComboBoxDisplayStyle CommboxStyle;
    }

    public class DataGridViewEx : DataGridView
    {
        public bool VisibleOrderNumber
        {
            get;
            set;
        }

        private SolidBrush solidBrush;

        public DataGridViewEx()
        {
            solidBrush = new SolidBrush(this.RowHeadersDefaultCellStyle.ForeColor);
            BackgroundColor = SystemColors.Control;
            BorderStyle = BorderStyle.FixedSingle;
            this.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                if (VisibleOrderNumber == true)
                {
                    e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, solidBrush, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 5);
                }
                base.OnRowPostPaint(e);
            }
            catch (Exception)
            {
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            if (this.CurrentCell != null)
            {
                this.BeginEdit(true);
            }
            base.OnCellContentClick(e);
        }

        public void AutoColumn()
        {
            int width = 0;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                this.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += this.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > this.Size.Width)
            {
                this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void ChangeColumnColor(int st, int ed, Color color)
        {
            try
            {
                for (int i = st; i <= ed; i++)
                {
                    this.Columns[i].DefaultCellStyle.BackColor = color;
                }
            }
            catch (Exception)
            {
            }
        }

        public void ClearRow()
        {
            try
            {
                DataTable dt = (DataTable)this.DataSource;
                if (dt != null)
                {
                    dt.Rows.Clear();
                    this.DataSource = dt;
                }
                else
                {
                    this.Rows.Clear();
                }
            }
            catch (Exception)
            {
            }
        }

        public void AddCols(ColStruct[] Col)
        {
            this.DataSource = null;
            this.Columns.Clear();

            for (int i = 0; i < Col.Length; i++)
            {
                switch (Col[i].ColType)
                {
                    case ColumnType.txt:
                        DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();
                        this.Columns.Add(txtColumn);
                        break;

                    case ColumnType.cmb:
                        DataGridViewComboBoxColumn cmbColumn = new DataGridViewComboBoxColumn
                        {
                            DisplayStyle = Col[i].CommboxStyle
                        };
                        this.Columns.Add(cmbColumn);
                        break;

                    case ColumnType.chk:
                        DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                        this.Columns.Add(chkColumn);
                        break;

                    case ColumnType.btn:
                        DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                        this.Columns.Add(btnColumn);
                        break;

                    case ColumnType.img:
                        DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                        this.Columns.Add(imgColumn);
                        break;

                    default:
                        DataGridViewTextBoxColumn dftColumn = new DataGridViewTextBoxColumn();
                        this.Columns.Add(dftColumn);
                        break;
                }

                int index = this.Columns.Count - 1;
                this.Columns[index].HeaderText = Col[i].ColHeader;
                this.Columns[index].Visible = Col[i].ColVisible;
                this.Columns[index].DataPropertyName = Col[i].ColField;
                this.Columns[index].Name = Col[i].ColField;
                this.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;  //不排序
                this.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //居中显示
                this.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;        //铺满
            }

            this.AutoGenerateColumns = false;
        }

        public DataGridViewComboBoxColumn GetComboBoxColumn(string columnName)
        {
            return (DataGridViewComboBoxColumn)this.Columns[columnName];
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}