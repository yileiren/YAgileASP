using System;
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
                        string sql = "SELECT * FROM SYS_MENUS  ORDER BY PARENTID ASC,[ORDER] ASC ";

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
                    this._errorMessage = "为设置数据库实例！";
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
    }
}
