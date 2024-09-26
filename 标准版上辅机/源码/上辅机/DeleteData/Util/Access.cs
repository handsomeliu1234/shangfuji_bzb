using ADOX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace DeleteData
{
    public class Access
    {
        /// <summary>
        /// 动态创建Access数据库和表
        /// </summary>
        /// <param name="dirPath">文件目录路径</param>
        /// <param name="filePath">数据库路径</param>
        /// <param name="tbName">表名</param>
        /// <param name="colums">表字段列表</param>
        /// <returns></returns>
        public static bool CreateAccessTable(string dirPath, string filePath, string tbName)
        {
            try
            {
                //目录与子目录不存在则创建
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                //数据库文件不存在则创建
                Catalog catalog = new Catalog();
                if (!File.Exists(filePath))
                {
                    catalog.Create(ConnectionString);
                }
                ADODB.Connection cn = new ADODB.Connection();
                OleDbConnection accessConnection = new OleDbConnection(ConnectionString);
                cn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath, null, null, -1);
                catalog.ActiveConnection = cn;
                accessConnection.Open();
                //判断数据库中是否存在表
                if (!GetTables(accessConnection, tbName))
                {
                    Table table = new Table
                    {
                        Name = tbName
                    };
                    Column column = new Column
                    {
                        ParentCatalog = catalog,
                        Name = "RecordId",
                        Type = DataTypeEnum.adInteger,
                        DefinedSize = 9
                    };

                    column.Properties["AutoIncrement"].Value = true;
                    table.Columns.Append(column, DataTypeEnum.adInteger, 9);
                    table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                    table.Columns.Append("DatabaseType", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("TableName", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("StartTime", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("EndTime", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("TableDataSum", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("IntervalCount", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("ConsumTime", DataTypeEnum.adVarWChar, 50);
                    table.Columns.Append("SaveTime", DataTypeEnum.adVarWChar, 50);

                    catalog.Tables.Append(table);
                }
                cn.Close();
                accessConnection.Close();
                return true;
            }
            catch (Exception )
            {
                //NewuGlobal.LogCat("Access").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 向Access数据库中插入数据
        /// </summary>
        /// <param name="data">要插入的数据</param>
        /// <param name="tableName">表名</param>
        /// <param name="accessConnection">数据库连接</param>
        /// <returns></returns>
        public static bool AddDataToAccess(Dictionary<string, object> data, string tableName, OleDbConnection accessConnection)
        {
            accessConnection.Open();
            if (data.Count > 0)
            {
                string fields = "";
                string values = "";
                foreach (var item in data)
                {
                    fields += item.Key.ToString() + ',';
                    values += string.Format("'{0}'", item.Value.ToString()) + ',';
                }
                fields = fields.Remove(fields.Length - 1, 1);
                values = values.Remove(values.Length - 1, 1);
                string sql = string.Format("insert into {0} ({1}) values ({2})", tableName, fields, values);
                OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
                cmd.ExecuteNonQuery();
            }
            accessConnection.Close();
            return true;
        }

        /// <summary>
        /// 判断数据库中是否存在某张表
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static bool GetTables(OleDbConnection conn, string tableName)
        {
            int result = 0;
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            if (schemaTable != null)
            {
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string col_name = schemaTable.Rows[i]["TABLE_NAME"].ToString();
                    if (col_name == tableName)
                    {
                        result++;
                    }
                }
            }
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + PathStr + ";Jet OLEDB:Engine Type=5";
            }
        }

        public static string PathStr
        {
            get;
            set;
        }

        public static string directoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\logs";   //存放删除数据库数据的日志
    }
}