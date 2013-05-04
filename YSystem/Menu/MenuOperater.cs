using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;
using System.Data;

namespace YLR.YSystem.Menu
{
    /// <summary>
    /// 菜单操作类库封装。
    /// </summary>
    public class MenuOperater
    {
        /// <summary>
        /// 菜单数据库实例。
        /// </summary>
        protected YDataBase _menuDataBase = null;

        /// <summary>
        /// 菜单数据库实例。
        /// </summary>
        public YDataBase menuDataBase
        {
            set
            {
                this._menuDataBase = value;
            }
            get
            {
                return this._menuDataBase;
            }
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
        /// 根据DataRow获取菜单对象。
        /// </summary>
        /// <param name="r">菜单数据</param>
        /// <returns>菜单，失败返回null。</returns>
        private MenuInfo getMenuFormDataRow(DataRow r)
        {
            if (r != null)
            {
                //分组对象
                MenuInfo m = new MenuInfo();
                
                //菜单id不能为null，否则返回失败。
                if (!r.IsNull("ID"))
                {
                    m.id = Convert.ToInt32(r["ID"]);
                }
                else
                {
                    return null;
                }

                if (!r.IsNull("NAME"))
                {
                    m.name = r["NAME"].ToString();
                }

                if (!r.IsNull("URL"))
                {
                    m.url = r["URL"].ToString();
                }

                if (!r.IsNull("PARENTID"))
                {
                    m.parentID = Convert.ToInt32(r["PARENTID"]);
                }

                if (!r.IsNull("ICON"))
                {
                    m.icon = r["ICON"].ToString();
                }

                if (!r.IsNull("DESKTOPICON"))
                {
                    m.desktopIcon = r["DESKTOPICON"].ToString();
                }

                if (!r.IsNull("ORDER"))
                {
                    m.order = Convert.ToInt32(r["ORDER"]);
                }

                return m;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据用户id，获取主页菜单。
        /// 作者：董帅 创建时间：2012-8-27 23:24:28
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>主页菜单。</returns>
        public List<MenuInfo> getMainPageMunus(int userId)
        {
            List<MenuInfo> menus = new List<MenuInfo>();

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {

                        //sql语句，获取所有菜单
                        string sql = "";
                        YParameters par = new YParameters();
                        par.add("@userId", userId);
                        if (userId == 1)
                        {
                            sql = "SELECT * FROM SYS_MENUS ORDER BY PARENTID ASC,[ORDER] ASC";
                        }
                        else
                        {
                            sql = @"SELECT DISTINCT SYS_MENUS.* 
                                    FROM SYS_MENUS,AUT_USER_ROLE,AUT_ROLE_MENU 
                                    WHERE AUT_USER_ROLE.USERID = @userId 
	                                AND AUT_USER_ROLE.ROLEID = AUT_ROLE_MENU.ROLEID
	                                AND AUT_ROLE_MENU.MENUID = SYS_MENUS.ID
                                    ORDER BY PARENTID ASC,[ORDER] ASC";
                        }

                        //获取数据
                        DataTable dt = this._menuDataBase.executeSqlReturnDt(sql,par);
                        if (dt != null)
                        {
                            //获取分组
                            DataRow[] groupMenus = dt.Select("PARENTID is null", "PARENTID ASC,[ORDER] ASC");

                            //获取子菜单
                            foreach(DataRow row in groupMenus)
                            {
                                MenuInfo pMenu = this.getMenuFormDataRow(row); //获取父菜单

                                if (pMenu != null)
                                {
                                    //获取子菜单
                                    DataRow[] childMenus = dt.Select("PARENTID = " + row["ID"], "PARENTID ASC,[ORDER] ASC");
                                    foreach (DataRow cRow in childMenus)
                                    {
                                        MenuInfo cMenu = this.getMenuFormDataRow(cRow);

                                        if (cMenu != null)
                                        {
                                            pMenu.childMenus.Add(cMenu);
                                        }
                                    }

                                    menus.Add(pMenu);
                                }
                            }

                            return menus;
                        }

                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return menus;
        }

        /// <summary>
        /// 通过父id获取菜单。
        /// 作者：董帅 创建时间：2012-8-6 12:57:40
        /// </summary>
        /// <param name="parentId">父id，为-1时表示获取顶层菜单。。</param>
        /// <returns>菜单，失败返回null。</returns>
        public List<MenuInfo> getMenuByParentId(int parentId)
        {
            List<MenuInfo> menus = null;
            try
            {
                menus = new List<MenuInfo>();

                //连接数据库
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {

                        menus = this.getMenuByParentId(parentId, this._menuDataBase);

                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._menuDataBase.errorText + "]";
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
                //断开数据库连接。
                this.menuDataBase.disconnectDataBase();
            }

            return menus;
        }

        /// <summary>
        /// 通过父id获取菜单。
        /// 作者：董帅 创建时间：2012-8-6 12:57:40
        /// </summary>
        /// <param name="parentId">父id，为-1时表示获取顶层菜单。。</param>
        /// <param name="db">数据连接。</param>
        /// <returns>菜单，失败返回null。</returns>
        private List<MenuInfo> getMenuByParentId(int parentId,YDataBase db)
        {
            List<MenuInfo> menus = null;
            try
            {
                menus = new List<MenuInfo>();

                //连接数据库
                if (db != null)
                {

                    //构建sql语句。
                    string sql = "";
                    YParameters par = new YParameters();
                    par.add("@parentId", parentId);
                    if (parentId == -1)
                    {
                        sql = "SELECT * FROM SYS_MENUS WHERE PARENTID IS NULL OR PARENTID = -1 ORDER BY [ORDER] ASC";
                    }
                    else
                    {
                        sql = "SELECT * FROM SYS_MENUS WHERE PARENTID = @parentId ORDER BY [ORDER] ASC";
                    }

                    //获取数据
                    DataTable dt = db.executeSqlReturnDt(sql, par);
                    if (dt != null)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            MenuInfo m = this.getMenuFormDataRow(row);

                            if (m != null)
                            {
                                menus.Add(m);
                            }
                        }

                        return menus;
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

            return menus;
        }

        /// <summary>
        /// 创建一个菜单。
        /// </summary>
        /// <param name="menu">要创建的菜单，名称不能为空。</param>
        /// <returns>成功返回创建的菜单id，失败返回-1。</returns>
        public int createNewMenu(MenuInfo menu)
        {
            int menuId = -1; //创建的组织机构id。

            try
            {
                if (menu == null)
                {
                    //不能插入空组织机构
                    this._errorMessage = "不能插入空菜单！";
                }
                else if (string.IsNullOrEmpty(menu.name) || menu.name.Length > 20)
                {
                    //组织机构名称不合法。
                    this._errorMessage = "菜单名称不合法！";
                }
                else if (menu.url.Length > 200)
                {
                    this._errorMessage = "菜单URL不合法！";
                }
                else if (menu.icon.Length > 20)
                {
                    this._errorMessage = "菜单图标不合法！";
                }
                else if (menu.desktopIcon.Length > 100)
                {
                    this._errorMessage = "菜单桌面图标不合法！";
                }
                else
                {
                    //新增数据
                    string sql = "";
                    YParameters par = new YParameters();
                    par.add("@name",menu.name);
                    par.add("@url", menu.url);
                    par.add("@parentID", menu.parentID);
                    par.add("@icon", menu.icon);
                    par.add("@desktopIcon", menu.desktopIcon);
                    par.add("@order", menu.order);
                    if (menu.parentID == -1)
                    {

                        sql = "INSERT INTO SYS_MENUS (NAME,ICON,[ORDER]) VALUES (@name,@icon,@order) SELECT SCOPE_IDENTITY() AS id";
                    }
                    else
                    {
                        sql = "INSERT INTO SYS_MENUS (NAME,URL,PARENTID,ICON,DESKTOPICON,[ORDER]) VALUES (@name,@url,@parentID,@icon,@desktopIcon,@order) SELECT SCOPE_IDENTITY() AS id";
                    }

                    //存入数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        DataTable retDt = this._menuDataBase.executeSqlReturnDt(sql,par);
                        if (retDt != null && retDt.Rows.Count > 0)
                        {
                            //获取组织机构id
                            menuId = Convert.ToInt32(retDt.Rows[0]["id"]);
                        }
                        else
                        {
                            this._errorMessage = "创建菜单失败！";
                            if (retDt == null)
                            {
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return menuId;
        }

        /// <summary>
        /// 获取指定id的菜单。
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns>菜单，失败返回null</returns>
        public MenuInfo getMenu(int id)
        {
            MenuInfo menu = null;

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {

                        //sql语句，获取所有菜单
                        YParameters par = new YParameters();
                        par.add("@menuId", id);
                        string sql = "SELECT * FROM SYS_MENUS WHERE id = @menuId ORDER BY PARENTID ASC,[ORDER] ASC";

                        //获取数据
                        DataTable dt = this._menuDataBase.executeSqlReturnDt(sql,par);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            //获取分组
                             menu = this.getMenuFormDataRow(dt.Rows[0]); //获取父菜单
                        }

                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return menu;
        }

        /// <summary>
        /// 修改指定菜单的内容，通过菜单id匹配。
        /// </summary>
        /// <param name="menu">要修改的菜单。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changeMenu(MenuInfo menu)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        //sql语句
                        string sql = "";
                        YParameters par = new YParameters();
                        par.add("@id",menu.id);
                        par.add("@name", menu.name);
                        par.add("@url", menu.url);
                        par.add("@parentID", menu.parentID);
                        par.add("@icon", menu.icon);
                        par.add("@desktopIcon", menu.desktopIcon);
                        par.add("@order", menu.order);
                        if (menu.parentID == -1)
                        {
                            //顶级菜单
                            sql = "UPDATE SYS_MENUS SET NAME = @name,ICON = @icon,[ORDER] = @order WHERE ID = @id";
                        }
                        else
                        {
                            sql = "UPDATE SYS_MENUS SET NAME = @name,URL = @url,PARENTID = @parentID,ICON = @icon,DESKTOPICON = @desktopIcon,[ORDER] = @order WHERE ID = @id";
                        }

                        int retCount = this._menuDataBase.executeSqlWithOutDs(sql,par);
                        if (retCount == 1)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "更新数据失败！";
                            if (retCount != 1)
                            {
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 删除分组。
        /// </summary>
        /// <param name="ids">分组id</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool deleteGroup(int[] ids)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        this._menuDataBase.beginTransaction();

                        int i = 0;
                        for (i = 0; i < ids.Length; i++)
                        {
                            //删除关联页面
                            if (this.deleteItemByParentId(ids[i], this.menuDataBase))
                            {
                                //sql语句
                                YParameters par = new YParameters();
                                par.add("@id", ids[i]);
                                string sql = "DELETE SYS_MENUS WHERE ID = @id";

                                int retCount = this._menuDataBase.executeSqlWithOutDs(sql, par);
                                if (retCount <= 0)
                                {
                                    this._errorMessage = "删除数据失败！";
                                    this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                                    break;
                                }
                            }
                            else
                            {
                                this._errorMessage = "删除数据失败！";
                                break;
                            }
                        }

                        //成功
                        if (i >= ids.Length)
                        {
                            bRet = true;
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                //提交或回滚事务。
                if (bRet)
                {
                    this._menuDataBase.commitTransaction();
                }
                else
                {
                    this._menuDataBase.rollbackTransaction();
                }

                this._menuDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 批量删除菜单项。
        /// 作者：董帅 创建时间：2012-8-14 14:49:28
        /// </summary>
        /// <param name="ids">要删除的分组id。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool deleteItem(int[] ids)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        this._menuDataBase.beginTransaction();
                        if(this.deleteItem(ids,this.menuDataBase))
                        {
                            bRet = true;
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                //提交或回滚事务。
                if (bRet)
                {
                    this._menuDataBase.commitTransaction();
                }
                else
                {
                    this._menuDataBase.rollbackTransaction();
                }

                this._menuDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 批量删除菜单项。
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <param name="db">数据库连接。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        private bool deleteItem(int[] ids, YDataBase db)
        {
            bool bRet = false; //返回值

            try
            {
                if (db != null)
                {
                    int i = 0;
                    for (i = 0; i < ids.Length; i++)
                    {
                        //删除关联页面
                        if (this.deletePagesByMenuId(ids[i], db))
                        {
                            //sql语句
                            YParameters par = new YParameters();
                            par.add("@id", ids[i]);
                            string sql = "DELETE SYS_MENUS WHERE ID = @id";

                            int retCount = this._menuDataBase.executeSqlWithOutDs(sql, par);
                            if (retCount <= 0)
                            {
                                this._errorMessage = "删除数据失败！";
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                                break;
                            }
                        }
                        else
                        {
                            this._errorMessage = "删除数据失败！";
                            break;
                        }
                    }

                    //成功
                    if (i >= ids.Length)
                    {
                        bRet = true;
                    }
                }
                else
                {
                    this._errorMessage = "未创建数据库实例。";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            return bRet;
        }

        /// <summary>
        /// 批量删除菜单项。
        /// </summary>
        /// <param name="parentId">分组id</param>
        /// <param name="db">数据库连接。</param>
        /// <returns>成功法返回true，否则返回false。</returns>
        private bool deleteItemByParentId(int parentId,YDataBase db)
        {
            bool bRet = false; //返回值

            try
            {
                if (db != null)
                {
                    //获取子菜单
                    List<MenuInfo> menus = this.getMenuByParentId(parentId,db);

                    int i = 0;
                    for (i = 0; i < menus.Count; i++)
                    {
                        //删除关联页面
                        if (this.deletePagesByMenuId(menus[i].id, db))
                        {
                            //sql语句
                            YParameters par = new YParameters();
                            par.add("@id", menus[i].id);
                            string sql = "DELETE SYS_MENUS WHERE ID = @id";

                            int retCount = this._menuDataBase.executeSqlWithOutDs(sql, par);
                            if (retCount <= 0)
                            {
                                this._errorMessage = "删除数据失败！";
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                                break;
                            }
                        }
                        else
                        {
                            this._errorMessage = "删除数据失败！";
                            break;
                        }
                    }
                    //成功
                    if (i >= menus.Count)
                    {
                        bRet = true;
                    }
                }
                else
                {
                    this._errorMessage = "未创建数据库实例。";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            return bRet;
        }

        /// <summary>
        /// 创建一个页面。
        /// </summary>
        /// <param name="page">要创建的页面。</param>
        /// <returns>成功返回创建的页面id，失败返回-1。</returns>
        public int createNewPage(PageInfo page)
        {
            int pageId = -1; //创建的组织机构id。

            try
            {
                if (page == null)
                {
                    //不能插入空组织机构
                    this._errorMessage = "不能插入空菜单！";
                }
                else if (string.IsNullOrEmpty(page.filePath) || page.filePath.Length > 500)
                {
                    //组织机构名称不合法。
                    this._errorMessage = "页面路径不合法！";
                }
                else
                {
                    //新增数据
                    string sql = "INSERT INTO SYS_MENU_PAGE (DETAIL,PATH,MENUID) VALUES (@detail,@filePath,@menuId) SELECT SCOPE_IDENTITY() AS id";
                    YParameters par = new YParameters();
                    par.add("@filePath", page.filePath);
                    par.add("@detail", page.detail);
                    par.add("@menuId", page.menuId);

                    //存入数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        DataTable retDt = this._menuDataBase.executeSqlReturnDt(sql, par);
                        if (retDt != null && retDt.Rows.Count > 0)
                        {
                            //获取组织机构id
                            pageId = Convert.ToInt32(retDt.Rows[0]["id"]);
                        }
                        else
                        {
                            this._errorMessage = "创建页面失败！";
                            if (retDt == null)
                            {
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return pageId;
        }

        /// <summary>
        /// 根据DataRow获取页面对象。
        /// </summary>
        /// <param name="r">页面数据</param>
        /// <returns>页面，失败返回null。</returns>
        private PageInfo getPageFormDataRow(DataRow r)
        {
            if (r != null)
            {
                //分组对象
                PageInfo p = new PageInfo();

                //菜单id不能为null，否则返回失败。
                if (!r.IsNull("ID"))
                {
                    p.id = Convert.ToInt32(r["ID"]);
                }
                else
                {
                    return null;
                }

                if (!r.IsNull("DETAIL"))
                {
                    p.detail = r["DETAIL"].ToString();
                }

                if (!r.IsNull("PATH"))
                {
                    p.filePath = r["PATH"].ToString();
                }

                if (!r.IsNull("MENUID"))
                {
                    p.menuId = Convert.ToInt32(r["MENUID"]);
                }

                return p;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定id的页面信息。
        /// 作者：董帅 创建时间：2013-5-4 22:54:40
        /// </summary>
        /// <param name="id">页面id。</param>
        /// <returns>页面信息。</returns>
        public PageInfo getPage(int id)
        {
            PageInfo page = null;

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {

                        //sql语句，获取所有菜单
                        YParameters par = new YParameters();
                        par.add("@id", id);
                        string sql = "SELECT * FROM SYS_MENU_PAGE WHERE id = @id";

                        //获取数据
                        DataTable dt = this._menuDataBase.executeSqlReturnDt(sql, par);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            //获取分组
                            page = this.getPageFormDataRow(dt.Rows[0]); //获取父菜单
                        }

                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return page;
        }

        /// <summary>
        /// 获取指定菜单的关联页面。
        /// 作者：董帅 创建时间：2013-5-4 22:33:00
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns>关联页面列表，出错返回null。</returns>
        public List<PageInfo> getPageByMenuId(int menuId)
        {
            List<PageInfo> pages = null;
            try
            {
                pages = new List<PageInfo>();

                //连接数据库
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {

                        //构建sql语句。
                        string sql = "SELECT * FROM SYS_MENU_PAGE WHERE MENUID = @menuId";
                        YParameters par = new YParameters();
                        par.add("@menuId", menuId);

                        //获取数据
                        DataTable dt = this._menuDataBase.executeSqlReturnDt(sql, par);
                        if (dt != null)
                        {

                            foreach (DataRow row in dt.Rows)
                            {
                                PageInfo p = this.getPageFormDataRow(row);

                                if (p != null)
                                {
                                    pages.Add(p);
                                }
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._menuDataBase.errorText + "]";
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
                //断开数据库连接。
                this.menuDataBase.disconnectDataBase();
            }

            return pages;
        }

        /// <summary>
        /// 修改指定页面的内容，通过页面id匹配。
        /// </summary>
        /// <param name="page">要修改的页面。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changePage(PageInfo page)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        //sql语句
                        string sql = "UPDATE SYS_MENU_PAGE SET DETAIL = @detail,PATH = @filePath WHERE ID = @id";
                        YParameters par = new YParameters();
                        par.add("@id", page.id);
                        par.add("@detail", page.detail);
                        par.add("@filePath", page.filePath);

                        int retCount = this._menuDataBase.executeSqlWithOutDs(sql, par);
                        if (retCount == 1)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "更新数据失败！";
                            if (retCount != 1)
                            {
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                this._menuDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 批量删除菜单。
        /// 作者：董帅 创建时间：2013-5-4 23:10:41
        /// </summary>
        /// <param name="ids">要删除的菜单id。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool deletePages(int[] ids)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._menuDataBase != null)
                {
                    //连接数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        this._menuDataBase.beginTransaction();
                        int i = 0;
                        for (i = 0; i < ids.Length; i++)
                        {
                            //sql语句
                            YParameters par = new YParameters();
                            par.add("@id", ids[i]);
                            string sql = "DELETE SYS_MENU_PAGE WHERE ID = @id";

                            int retCount = this._menuDataBase.executeSqlWithOutDs(sql, par);
                            if (retCount <= 0)
                            {
                                this._errorMessage = "删除数据失败！";
                                this._errorMessage += "错误信息[" + this._menuDataBase.errorText + "]";
                                break;
                            }
                        }

                        //成功
                        if (i >= ids.Length)
                        {
                            bRet = true;
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._menuDataBase.errorText + "]";
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
                //提交或回滚事务。
                if (bRet)
                {
                    this._menuDataBase.commitTransaction();
                }
                else
                {
                    this._menuDataBase.rollbackTransaction();
                }

                this._menuDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 通过指定的菜单id删除菜单。
        /// 作者：董帅 创建时间：2013-5-4 23:17:47
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="db">使用的数据库。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        private bool deletePagesByMenuId(int menuId,YDataBase db)
        {
            bool retValue = false;

            try
            {
                if (db != null)
                {
                    //sql语句
                    YParameters par = new YParameters();
                    par.add("@menuId", menuId);
                    string sql = "DELETE SYS_MENU_PAGE WHERE MENUID = @menuId";

                    int retCount = db.executeSqlWithOutDs(sql, par);
                    if (retCount >= 0)
                    {
                        retValue = true;
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

            return retValue;
        }
    }
}
