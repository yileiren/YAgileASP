using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YAdoNet;
using YLR.YMenu;
using YLR.YMessage;

namespace YAgileASP.background.sys.menu
{
    public partial class menu_list : System.Web.UI.Page
    {
        protected string groupTitle = ""; //分组子菜单标题
        protected string groupIcon = ""; //分组图标
 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.bindData();
            }
        }

        private void bindData()
        {
            //获取菜单
            try
            {
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //获取数据库实例。
                YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");

                if (orgDb != null)
                {
                    MenuOperater menuOper = new MenuOperater();
                    menuOper.menuDataBase = orgDb;

                    //绑定分组
                    List<MenuInfo> menus = menuOper.getMenuByParentId(-1);
                    if (menus != null)
                    {
                        this.menuGroups.DataSource = menus;
                        this.menuGroups.DataBind();
                    }
                    else
                    {
                        return;
                    }

                    //设置选中的分组id
                    if (string.IsNullOrEmpty(this.selectGroupId.Value) && menus.Count > 0)
                    {
                        this.selectGroupId.Value = menus[0].id.ToString();

                        //设置标题和图标
                        this.groupTitle = menus[0].name;
                        this.groupIcon = menus[0].icon;
                    }

                    //获取子菜单
                    if (menus.Count > 0)
                    {
                        List<MenuInfo> childMenus = menuOper.getMenuByParentId(Convert.ToInt32(this.selectGroupId.Value));

                        if (childMenus != null)
                        {
                            this.childs.DataSource = childMenus;
                            this.childs.DataBind();
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
                YMessageBox.showAndRedirect(this, "系统运行异常！异常信息[" + ex.Message + "]", "sys/login.aspx");
            }
        }
    }
}