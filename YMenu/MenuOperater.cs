﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;
using System.Data;

namespace YLR.YMenu
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
        /// 获取主页菜单。
        /// </summary>
        /// <returns>主页菜单。</returns>
        public List<MenuInfo> getMainPageMunus()
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
                        string sql = "SELECT * FROM SYS_MENUS ORDER BY PARENTID ASC,[ORDER] ASC";

                        //获取数据
                        DataTable dt = this._menuDataBase.executeSqlReturnDt(sql);
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

                        //构建sql语句。
                        string sql = "";
                        if (parentId == -1)
                        {
                            sql = "SELECT * FROM SYS_MENUS WHERE PARENTID IS NULL OR PARENTID = -1 ORDER BY [ORDER] ASC";
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM SYS_MENUS WHERE PARENTID = {0} ORDER BY [ORDER] ASC", parentId);
                        }

                        //获取数据
                        DataTable dt = this._menuDataBase.executeSqlReturnDt(sql);
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
                else if (string.IsNullOrEmpty(menu.name) || menu.name.Length > 50)
                {
                    //组织机构名称不合法。
                    this._errorMessage = "菜单名称不合法！";
                }
                else
                {
                    //新增数据
                    string sql = "";
                    if (menu.parentID == -1)
                    {

                        sql = string.Format("INSERT INTO SYS_MENUS (NAME,ICON,[ORDER]) VALUES ('{0}','{1}',{2}) SELECT SCOPE_IDENTITY() AS id"
                            , menu.name
                            , menu.icon
                            ,menu.order);
                    }
                    else
                    {
                        sql = string.Format("INSERT INTO SYS_MENUS (NAME,URL,PARENTID,ICON,DESKTOPICON,[ORDER]) VALUES ('{0}','{1}',{2},'{3}','{4}',{5}) SELECT SCOPE_IDENTITY() AS id"
                            , menu.name
                            , menu.url
                            , menu.parentID
                            , menu.icon
                            , menu.desktopIcon
                            , menu.order);
                    }

                    //存入数据库
                    if (this._menuDataBase.connectDataBase())
                    {
                        DataTable retDt = this._menuDataBase.executeSqlReturnDt(sql);
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
    }
}
