using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;
using System.Data;
using YLR.YMenu;

namespace YLR.YRole
{
    /// <summary>
    /// 角色数据库操作类。
    /// 作者：董帅 创建时间：2012-8-15 23:00:55
    /// </summary>
    public class RoleOperater
    {
        /// <summary>
        /// 角色数据库实例。
        /// </summary>
        protected YDataBase _roleDataBase = null;

        /// <summary>
        /// 角色数据库实例。
        /// </summary>
        public YDataBase roleDataBase
        {
            get { return this._roleDataBase; }
            set { this._roleDataBase = value; }
        }

        /// <summary>
        /// 错误信息。
        /// </summary>
        protected string _errorMessage = "";

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string errorMessage
        {
            get
            {
                return this._errorMessage;
            }
        }

        /// <summary>
        /// 创建角色数据库操作对象。
        /// 作者：董帅 创建时间：2012-8-15 23:20:13
        /// </summary>
        /// <param name="configFilePath">配置文件路径。</param>
        /// <param name="nodeName">节点名。</param>
        /// <returns>成功返回操作对象，否则返回null。</returns>
        public static RoleOperater createRoleOperater(string configFilePath,string nodeName)
        {
            RoleOperater roleOper = null;

            //获取数据库实例。
            YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFilePath, nodeName);

            if (orgDb != null)
            {
                roleOper = new RoleOperater();
                roleOper.roleDataBase = orgDb;
            }
            else
            {
                Exception ex = new Exception("创建数据库实例失败！");
                throw ex;
            }

            return roleOper;
        }

        /// <summary>
        /// 创建一个角色。
        /// </summary>
        /// <param name="role">要创建的角色，名称不能为空。</param>
        /// <returns>成功返回创建的角色id，失败返回-1。</returns>
        public int createNewRole(RoleInfo role)
        {
            int roleId = -1; //创建的组织机构id。

            try
            {
                if (role == null)
                {
                    this._errorMessage = "不能插入空角色！";
                }
                else if (string.IsNullOrEmpty(role.name) || role.name.Length > 20)
                {
                    this._errorMessage = "角色名称不合法！";
                }
                else if (string.IsNullOrEmpty(role.explain) || role.explain.Length > 50)
                {
                    this._errorMessage = "角色说明不合法！";
                }
                else
                {
                    //新增数据
                    string sql = string.Format("INSERT INTO AUT_ROLE (NAME,EXPLAIN) VALUES ('{0}','{1}') SELECT SCOPE_IDENTITY() AS id"
                                , role.name
                                , role.explain);

                    //存入数据库
                    if (this._roleDataBase.connectDataBase())
                    {
                        DataTable retDt = this._roleDataBase.executeSqlReturnDt(sql);
                        if (retDt != null && retDt.Rows.Count > 0)
                        {
                            //获取组织机构id
                            roleId = Convert.ToInt32(retDt.Rows[0]["id"]);
                        }
                        else
                        {
                            this._errorMessage = "创建角色失败！";
                            if (retDt == null)
                            {
                                this._errorMessage += "错误信息[" + this._roleDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._roleDataBase.errorText + "]";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //断开数据库连接。
                this._roleDataBase.disconnectDataBase();
            }

            return roleId;
        }

        /// <summary>
        /// 根据DataRow获取权限对象。
        /// </summary>
        /// <param name="r">权限数据</param>
        /// <returns>权限，失败返回null。</returns>
        private RoleInfo getRoleFormDataRow(DataRow r)
        {
            if (r != null)
            {
                //分组对象
                RoleInfo role = new RoleInfo();

                //菜单id不能为null，否则返回失败。
                if (!r.IsNull("ID"))
                {
                    role.id = Convert.ToInt32(r["ID"]);
                }
                else
                {
                    return null;
                }

                if (!r.IsNull("NAME"))
                {
                    role.name = r["NAME"].ToString();
                }

                if (!r.IsNull("EXPLAIN"))
                {
                    role.explain = r["EXPLAIN"].ToString();
                }

                return role;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有的权限。
        /// 作者：董帅 创建时间：2012-8-16 21:45:33
        /// </summary>
        /// <returns>权限，如果失败返回null。</returns>
        public List<RoleInfo> getRoles()
        {
            List<RoleInfo> roles = null;

            try
            {
                if (this._roleDataBase != null)
                {
                    //连接数据库
                    if (this._roleDataBase.connectDataBase())
                    {

                        //sql语句，获取所有权限
                        string sql = "SELECT * FROM AUT_ROLE";

                        //获取数据
                        DataTable dt = this._roleDataBase.executeSqlReturnDt(sql);
                        if (dt != null)
                        {
                            roles = new List<RoleInfo>();
                            foreach (DataRow row in dt.Rows)
                            {
                                RoleInfo role = this.getRoleFormDataRow(row);
                                if (role != null)
                                {
                                    roles.Add(role);
                                }
                            }
                        }
                        else
                        {
                            this._errorMessage = "获取数据失败！错误信息：[" + this._roleDataBase.errorText + "]";
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._roleDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._roleDataBase.disconnectDataBase();
            }

            return roles;
        }

        /// <summary>
        /// 通过指定的权限id获取权限。
        /// </summary>
        /// <param name="id">权限id</param>
        /// <returns>权限</returns>
        public RoleInfo getRole(int id)
        {
            RoleInfo role = null;

            try
            {
                if (this._roleDataBase != null)
                {
                    //连接数据库
                    if (this._roleDataBase.connectDataBase())
                    {

                        //sql语句，获取所有权限
                        string sql = "SELECT * FROM AUT_ROLE WHERE ID = " + id.ToString(); ;

                        //获取数据
                        DataTable dt = this._roleDataBase.executeSqlReturnDt(sql);
                        if (dt != null)
                        {
                            role = this.getRoleFormDataRow(dt.Rows[0]);
                        }
                        else
                        {
                            this._errorMessage = "获取数据失败！错误信息：[" + this._roleDataBase.errorText + "]";
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._roleDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._roleDataBase.disconnectDataBase();
            }

            return role;
        }

        /// <summary>
        /// 修改权限。
        /// 作者：董帅 创建时间：2012-8-16 22:16:27
        /// </summary>
        /// <param name="role">要修改的权限，使用id作为唯一标识。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changeRole(RoleInfo role)
        {
            bool bRet = false;

            try
            {
                if (this._roleDataBase != null)
                {
                    //连接数据库
                    if (this._roleDataBase.connectDataBase())
                    {
                        //sql语句
                        string sql = string.Format("UPDATE AUT_ROLE SET NAME = '{0}',EXPLAIN = '{1}' WHERE ID = {2}"
                                        , role.name
                                        , role.explain
                                        , role.id);

                        int retCount = this._roleDataBase.executeSqlWithOutDs(sql);
                        if (retCount == 1)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "更新数据失败！";
                            if (retCount != 1)
                            {
                                this._errorMessage += "错误信息[" + this._roleDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._roleDataBase.errorText + "]";
                    }

                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._roleDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 删除角色。
        /// 作者：董帅 创建时间：2012-8-16 22:33:20
        /// </summary>
        /// <param name="ids">要删除的角色id。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool deleteRoles(int[] ids)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._roleDataBase != null)
                {
                    //连接数据库
                    if (this._roleDataBase.connectDataBase())
                    {
                        //组件id字符串
                        string strIds = "";
                        for (int i = 0; i < ids.Length; i++)
                        {
                            if (i == 0)
                            {
                                strIds += ids[i].ToString();
                            }
                            else
                            {
                                strIds += "," + ids[i].ToString();
                            }
                        }
                        //sql语句
                        string sql = string.Format("DELETE AUT_ROLE WHERE ID IN ({0})", strIds);

                        int retCount = this._roleDataBase.executeSqlWithOutDs(sql);
                        if (retCount > 0)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "删除数据失败！";
                            this._errorMessage += "错误信息[" + this._roleDataBase.errorText + "]";
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._roleDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._roleDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 根据角色id获取菜单，并标记选中的菜单。
        /// 作者：董帅 创建时间：2012-8-20 23:12:49
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>角色菜单，失败返回null</returns>
        public List<RoleMenuInfo> getChouseMenus(int roleId)
        {
            List<RoleMenuInfo> menus = new List<RoleMenuInfo>();

            try
            {
                if (this._roleDataBase != null)
                {
                    //连接数据库
                    if (this._roleDataBase.connectDataBase())
                    {

                        //sql语句，获取所有菜单
                        string sql = "SELECT * FROM SYS_MENUS WHERE PARENTID IS NULL AND ID <> 1 OR ID <> 1 AND PARENTID <> 1 ORDER BY PARENTID ASC,[ORDER] ASC";
                        string chouseMenuSql = "SELECT * FROM AUT_ROLE_MENU WHERE ROLEID = " + roleId.ToString();
                        //获取数据
                        DataTable dt = this._roleDataBase.executeSqlReturnDt(sql);
                        DataTable chouseMenuDt = this._roleDataBase.executeSqlReturnDt(chouseMenuSql);
                        if (dt != null && chouseMenuDt != null)
                        {
                            //获取分组
                            DataRow[] groupMenus = dt.Select("PARENTID is null", "PARENTID ASC,[ORDER] ASC");

                            //获取子菜单
                            foreach (DataRow row in groupMenus)
                            {
                                RoleMenuInfo pMenu = new RoleMenuInfo(); //获取父菜单

                                //设置数据
                                //菜单id不能为null，否则返回失败。
                                if (!row.IsNull("ID"))
                                {
                                    pMenu.id = Convert.ToInt32(row["ID"]);
                                }
                                else
                                {
                                    return null;
                                }

                                if (!row.IsNull("NAME"))
                                {
                                    pMenu.name = row["NAME"].ToString();
                                }

                                if (!row.IsNull("URL"))
                                {
                                    pMenu.url = row["URL"].ToString();
                                }

                                if (!row.IsNull("PARENTID"))
                                {
                                    pMenu.parentID = Convert.ToInt32(row["PARENTID"]);
                                }

                                if (!row.IsNull("ICON"))
                                {
                                    pMenu.icon = row["ICON"].ToString();
                                }

                                if (!row.IsNull("DESKTOPICON"))
                                {
                                    pMenu.desktopIcon = row["DESKTOPICON"].ToString();
                                }

                                if (!row.IsNull("ORDER"))
                                {
                                    pMenu.order = Convert.ToInt32(row["ORDER"]);
                                }

                                if(chouseMenuDt.Select("MENUID = " + pMenu.id.ToString()).Length > 0)
                                {
                                    pMenu.choused = true;
                                }

                                //获取子菜单
                                DataRow[] childMenus = dt.Select("PARENTID = " + row["ID"], "PARENTID ASC,[ORDER] ASC");
                                foreach (DataRow cRow in childMenus)
                                {
                                    RoleMenuInfo cMenu = new RoleMenuInfo();

                                    //设置数据
                                    //菜单id不能为null，否则返回失败。
                                    if (!cRow.IsNull("ID"))
                                    {
                                        cMenu.id = Convert.ToInt32(cRow["ID"]);
                                    }
                                    else
                                    {
                                        return null;
                                    }

                                    if (!cRow.IsNull("NAME"))
                                    {
                                        cMenu.name = cRow["NAME"].ToString();
                                    }

                                    if (!cRow.IsNull("URL"))
                                    {
                                        cMenu.url = cRow["URL"].ToString();
                                    }

                                    if (!cRow.IsNull("PARENTID"))
                                    {
                                        cMenu.parentID = Convert.ToInt32(cRow["PARENTID"]);
                                    }

                                    if (!cRow.IsNull("ICON"))
                                    {
                                        cMenu.icon = cRow["ICON"].ToString();
                                    }

                                    if (!cRow.IsNull("DESKTOPICON"))
                                    {
                                        cMenu.desktopIcon = cRow["DESKTOPICON"].ToString();
                                    }

                                    if (!cRow.IsNull("ORDER"))
                                    {
                                        cMenu.order = Convert.ToInt32(cRow["ORDER"]);
                                    }

                                    if (chouseMenuDt.Select("MENUID = " + cMenu.id.ToString()).Length > 0)
                                    {
                                        cMenu.choused = true;
                                    }

                                    if (cMenu != null)
                                    {
                                        pMenu.childMenus.Add(cMenu);
                                    }
                                }

                                menus.Add(pMenu);
                            }

                            return menus;
                        }

                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._roleDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._roleDataBase.disconnectDataBase();
            }

            return menus;
        }

        /// <summary>
        /// 选择权限菜单，将制定id保存到数据库。
        /// 作者：董帅 创建时间：2012-8-20 23:42:03
        /// </summary>
        /// <param name="roleId">角色id。</param>
        /// <param name="menuIds">选中的菜单id。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool chouseRoleMenus(int roleId,int[] menuIds)
        {
            bool bRet = false;

            try
            {
                if (this._roleDataBase != null)
                {
                    //连接数据库
                    if (this._roleDataBase.connectDataBase())
                    {
                        this._roleDataBase.beginTransaction();

                        string deleteSql = "DELETE AUT_ROLE_MENU WHERE ROLEID = " + roleId.ToString();

                        if (this._roleDataBase.executeSqlWithOutDs(deleteSql) >= 0)
                        {

                            int count = 0;
                            foreach (int mId in menuIds)
                            {
                                //sql语句
                                string sql = string.Format("INSERT INTO AUT_ROLE_MENU (ROLEID,MENUID) VALUES ({0},{1})", roleId, mId);
                                int retCount = this._roleDataBase.executeSqlWithOutDs(sql);

                                if (retCount == 1)
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (count == menuIds.Length)
                            {
                                bRet = true;
                            }
                            else
                            {
                                this._errorMessage = "存储数据失败！";
                                this._errorMessage += "错误信息[" + this._roleDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._roleDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                if(bRet)
                {
                    this._roleDataBase.commitTransaction();
                }
                else
                {
                    this._roleDataBase.rollbackTransaction();
                }

                this._roleDataBase.disconnectDataBase();
            }

            return bRet;
        }
    }
}
