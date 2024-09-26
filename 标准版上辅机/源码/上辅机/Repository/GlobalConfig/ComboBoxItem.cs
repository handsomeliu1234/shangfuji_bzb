using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.GlobalConfig
{
    public struct Item<TValue, TDisplayName>
    {
        private TValue _value;
        private TDisplayName _displayname;

        public Item(TValue value, TDisplayName displayName)
        {
            this._value = value;
            this._displayname = displayName;
        }

        public TValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public TDisplayName DisplayName
        {
            get
            {
                return _displayname;
            }
            set
            {
                _displayname = value;
            }
        }

        public override string ToString()
        {
            return _displayname.ToString();
        }
    }

    public static class EnableListR
    {
        public static List<Ruby> GetList()
        {
            List<Ruby> list = new List<Ruby>();
            Ruby ruby;
            Ruby ruby1;
            Ruby ruby2;
            Ruby ruby3;

            if (NewuGlobal.SupportLanguage.Equals("1"))
            {
                ruby = new Ruby
                {
                    Name = "否",
                    Value = 0
                };
                ruby1 = new Ruby
                {
                    Name = "下层",
                    Value = 1
                };
                ruby2 = new Ruby
                {
                    Name = "上层",
                    Value = 2
                };
                ruby3 = new Ruby
                {
                    Name = "双层",
                    Value = 3
                };
            }
            else
            {
                ruby = new Ruby
                {
                    Name = "No",
                    Value = 0
                };
                ruby1 = new Ruby
                {
                    Name = "Under",
                    Value = 1
                };
                ruby2 = new Ruby
                {
                    Name = "Upper",
                    Value = 2
                };
                ruby3 = new Ruby
                {
                    Name = "Both",
                    Value = 3
                };
            }

            list.Add(ruby);
            list.Add(ruby1);
            list.Add(ruby2);
            list.Add(ruby3);
            return list;
        }

        public class Ruby
        {
            public string Name
            {
                get; set;
            }

            public int Value
            {
                get; set;
            }
        }
    }

    public static class EnableList
    {
        public static DataTable GetList()
        {
            try
            {
                DataTable dt = new DataTable("EanbledList");

                dt.Columns.Add("names", Type.GetType("System.String"));
                dt.Columns.Add("values", Type.GetType("System.Int32"));
                DataRow row = dt.NewRow();
                DataRow row1 = dt.NewRow();

                if (NewuGlobal.SupportLanguage.Equals("1"))
                {
                    row["names"] = "是";
                    row["values"] = "1";
                    row1["names"] = "否";
                    row1["values"] = "0";
                }
                else
                {
                    row["names"] = "Yes";
                    row["values"] = "1";
                    row1["names"] = "No";
                    row1["values"] = "0";

                }
                dt.Rows.Add(row);

                dt.Rows.Add(row1);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class CreateTable
    {
        public DataTable GetTable(string[] columnName, object[,] values)
        {
            Type[] types = new Type[columnName.Length];
            for (int i = 0; i < types.Length; i++)
            {
                types[i] = typeof(string);
            }

            return GetTable(columnName, types, values);
        }

        public DataTable GetTable(string[] columnName, Type[] types, object[,] values)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < columnName.Length; i++)
            {
                dt.Columns.Add(columnName[i], types[i]);
            }

            for (int i = 0; i <= values.GetUpperBound(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columnName.Length; j++)
                {
                    dr[columnName[j]] = values[i, j];
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}