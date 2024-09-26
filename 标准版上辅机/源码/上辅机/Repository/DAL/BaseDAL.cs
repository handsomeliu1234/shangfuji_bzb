using Dapper;
using Dapper.Contrib.Extensions;
using Repository.GlobalConfig;
using Repository.Helper;
using Repository.Model;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Repository.DAL
{
    /// <summary>
    /// 数据库访问基类
    /// </summary>
    /// <typeparam name="T">实体类类型</typeparam>
    public partial class BaseDAL<T> where T : class
    {
        /// <summary>
        /// 对象的表名
        /// </summary>
        public string TableName
        {
            get; set;
        }

        /// <summary>
        /// 主键属性对象
        /// </summary>
        public PropertyInfo PrimaryKey
        {
            get; set;
        }

        public BaseDAL()
        {
            this.TableName = EntityHelper.GetTableName(typeof(T));
            this.PrimaryKey = EntityHelper.GetSingleKey<T>();
        }

        /// <summary>
        /// 主数据库连接
        /// </summary>
        protected static IDbConnection ConnectionXF
        {
            get
            {
                var connection = new System.Data.SqlClient.SqlConnection(new DbHelperSQL().ConnectionString);
                connection.Open();
                return connection;
            }
        }

        /// <summary>
        /// 报表数据数据库
        /// </summary>
        protected IDbConnection ConnectionXFData
        {
            get
            {
                var connection = new System.Data.SqlClient.SqlConnection(new DbHelperSQL(ConnType.NewuSoftData).ConnectionString);
                connection.Open();
                return connection;
            }
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                return dbConnection.GetAll<T>();
            }
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        public T FindByID(object id)
        {
            using (IDbConnection dbConnection = ConnectionXF)
            {
                return dbConnection.Get<T>(id);
            }
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Insert(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                dbConnection.Insert(info);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Insert(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                result = dbConnection.Insert(list) > 0;
            }
            return result;
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Update(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                result = dbConnection.Update(info);
            }
            return result;
        }

        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Update(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                result = dbConnection.Update(list);
            }
            return result;
        }

        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Delete(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                result = dbConnection.Delete(info);
            }
            return result;
        }

        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                result = dbConnection.Delete(list);
            }
            return result;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        public bool Delete(object id)
        {
            try
            {
                bool result = false;
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string query = string.Format("DELETE FROM {0} WHERE {1} = @id", TableName, PrimaryKey.Name);
                    var parameters = new DynamicParameters();
                    parameters.Add("@id", id);

                    result = dbConnection.Execute(query, parameters) > 0;
                }
                return result;
            }
            catch (System.Exception ex)
            {
                NewuGlobal.LogCat("BaseDAL").Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            bool result = false;
            using (IDbConnection dbConnection = ConnectionXF)
            {
                result = dbConnection.DeleteAll<T>();
            }
            return result;
        }
    }
}