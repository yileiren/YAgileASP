using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMenu;
using YLR.YMessage;
using YLR.YAdoNet;

namespace YAgileASP.background.sys.menu
{
    public partial class menu_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx')");
                        }
                        else
                        {
                            YMessageBox.show(this, "创建菜单出错！错误信息[" + menuOper.errorMessage + "]");
                        }
                    }
                    else
                    {
                        //修改菜单
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