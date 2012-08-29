using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YAdoNet;
using YLR.YSystem.Menu;

namespace YAgileASP.background.sys.menu
{
    public partial class menu_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                { 
                    //判断编辑菜单类型，group是分组，item是菜单，默认是分组
                    string pageType = Request.QueryString["pageType"];
                    if (string.IsNullOrEmpty(pageType) || pageType == "group")
                    {
                        this.txtMenuURL.Disabled = true;
                        this.txtMenuDesktopICON.Disabled = true;
                    }

                    //获取父id
                    string parentId = Request.QueryString["parentId"];
                    if (!string.IsNullOrEmpty(parentId))
                    {
                        this.hidParentId.Value = parentId;
                    }

                    //获取id，没有id表示新增，否则为修改
                    string menuId = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(menuId))
                    {
                        //修改，获取数据
                        //获取配置文件路径。
                        string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                        //获取数据库实例。
                        YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");

                        if (orgDb != null)
                        {
                            MenuOperater menuOper = new MenuOperater();
                            menuOper.menuDataBase = orgDb;

                            MenuInfo menu = menuOper.getMenu(Convert.ToInt32(menuId));
                            if (menu != null)
                            {
                                this.hidMenuId.Value = menu.id.ToString();
                                this.txtMenuName.Value = menu.name;
                                this.txtMenuURL.Value = menu.url;
                                this.txtMenuICON.Value = menu.icon;
                                this.txtMenuDesktopICON.Value = menu.desktopIcon;
                                this.txtMenuOrder.Value = menu.order.ToString();
                            }
                            else
                            {
                                YMessageBox.show(this,"未找到指定的菜单！");
                            }
                        }
                        else
                        {
                            YMessageBox.show(this, "获取数据库实例失败！");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                YMessageBox.show(this,ex.Message);
            }
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                //创建菜单对象
                MenuInfo menu = new MenuInfo();

                //菜单名称
                if (string.IsNullOrEmpty(this.txtMenuName.Value))
                {
                    YMessageBox.show(this, "菜单名称不能为空！");
                    return;
                }
                else
                {
                    menu.name = this.txtMenuName.Value;
                }

                menu.url = this.txtMenuURL.Value;
                menu.icon = this.txtMenuICON.Value;
                menu.desktopIcon = this.txtMenuDesktopICON.Value;
                menu.order = Convert.ToInt32(this.txtMenuOrder.Value);
                
                //父菜单
                if (string.IsNullOrEmpty(this.hidParentId.Value))
                {
                    menu.parentID = -1;
                }
                else
                {
                    menu.parentID = Convert.ToInt32(this.hidParentId.Value);
                }

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //获取数据库实例。
                YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");

                if (orgDb != null)
                {
                    MenuOperater menuOper = new MenuOperater();
                    menuOper.menuDataBase = orgDb;

                    if (string.IsNullOrEmpty(this.hidMenuId.Value))
                    {
                        //新增菜单
                        int iRet = menuOper.createNewMenu(menu);
                        if (iRet > 0)
                        {
                            if (this.txtMenuURL.Disabled)
                            {
                                //分组
                                YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx?id=" + iRet.ToString() + "')");
                            }
                            else
                            {
                                //菜单
                                YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx?id=" + this.hidParentId.Value + "')");
                            }
                        }
                        else
                        {
                            YMessageBox.show(this, "创建菜单出错！错误信息[" + menuOper.errorMessage + "]");
                        }
                    }
                    else
                    {
                        //修改菜单
                        menu.id = Convert.ToInt32(this.hidMenuId.Value);
                        bool bRet = menuOper.changeMenu(menu);
                        if (bRet)
                        {
                            if (this.txtMenuURL.Disabled)
                            {
                                //分组
                                YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx?id=" + menu.id.ToString() + "')");
                            }
                            else
                            {
                                //菜单
                                YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx?id=" + menu.parentID.ToString() + "')");
                            }
                        }
                        else
                        {
                            YMessageBox.show(this, "修改菜单出错！错误信息[" + menuOper.errorMessage + "]");
                        }
                    }
                }
                else
                {
                    YMessageBox.show(this, "获取数据库实例失败！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "保存数据出错！错误信息[" + ex.Message + "]");
            }
        }
    }
}