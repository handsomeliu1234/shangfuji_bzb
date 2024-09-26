using Dapper;
using Dapper.Contrib.Extensions;
using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Repository
{
    public class ScaleCheckSetRepository : BaseDAL<ScaleCheckSet>
    {
        public ScaleCheckSetRepository()
        {
        }

        public List<ScaleCheckSet> GetList()
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string query = string.Format("Select * from [dbo].[ScaleCheckSet]");
                    var list = dbConnection.Query<ScaleCheckSet>(query).ToList();
                    if (list != null && list.Count() != 0)
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ScaleCheckSetRepository").Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public List<ScaleCheckSet> GetByDevice(string device)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionXF)
                {
                    string query = string.Format("Select * from [dbo].[ScaleCheckSet] where DeviceCode = @Device order by CheckScaleNo");
                    var list = dbConnection.Query<ScaleCheckSet>(query, new
                    {
                        Device = device
                    }).ToList();
                    if (list != null && list.Count() != 0)
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("ScaleCheckSetRepository").Error(ex.ToString());
                return null;
            }
        }
    }
}