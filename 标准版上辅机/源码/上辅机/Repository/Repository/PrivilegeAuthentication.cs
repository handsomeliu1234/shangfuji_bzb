using Repository.DAL;
using Repository.GlobalConfig;
using Repository.Model;
using System;
using System.Data;
using Dapper;
using System.Collections.Generic;

namespace Repository.Repository
{
    /// <summary>
    /// 窗体内部权限验证：验证按钮
    /// </summary>
    public class PrivilegeAuthentication : BaseDAL<SYS_Menu>
    {
        /// 验证控窗体中该控件权限是否添加,该方法针对单个控件
        /// </summary>
        /// <param name="formMenuId"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static bool Authentication(string formMenuId, string controlName)
        {
            try
            {
                using (IDbConnection connection = ConnectionXF)
                {
                    string strSql = "SELECT b.* FROM [TB_Privilege] a join [SYS_Menu] b on a.MenuID = b.MenuID and a.RoleID = @RoleID and a.Enable='1' and b.ParentMenuID = @ParentMenuID and b.ControlName = @ControlName";
                    List<SYS_Menu> sYS_Menus = connection.Query<SYS_Menu>(strSql, new
                    {
                        ParentMenuID = formMenuId,
                        ControlName = controlName,
                        RoleID = NewuGlobal.TB_UserInfo.RoleID
                    }).AsList();

                    if (sYS_Menus.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                NewuGlobal.LogCat("PrivilegeAuthentication").Error(ex.ToString());
                return false;
            }
        }
    }
}