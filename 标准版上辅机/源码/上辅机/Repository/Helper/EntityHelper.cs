using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helper
{
    public static class EntityHelper
    {
        public static string GetTableName(Type type)
        {
            TableAttribute table = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (table != null)
            {
                return table.Name;
            }
            else
            {
                return type.Name;
            }

        }

        public static PropertyInfo GetSingleKey<T>()
        {
            Type type = typeof(T);
            //ExplicitKeyAttribute key = type.GetCustomAttribute(typeof(ExplicitKeyAttribute)) as ExplicitKeyAttribute;
            string singlekey = "";
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                ExplicitKeyAttribute key = propInfo.GetCustomAttribute(typeof(ExplicitKeyAttribute)) as ExplicitKeyAttribute;
                if (key != null)
                {
                    singlekey = propInfo.Name;
                    return propInfo;
                }
            }
            return null;
        }
    }
}
