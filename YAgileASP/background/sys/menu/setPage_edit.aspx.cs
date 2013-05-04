using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YSystem.Menu;
using YLR.YAdoNet;

namespace YAgileASP.background.sys.menu
{
    public partial class setPage_edit : System.Web.UI.Page
    {
        protected string menuId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.menuId = Request.QueryString["menuId"];
                if (!string.IsNullOrEmpty(this.menuId))
                {
                    this.hidMenuId.Value = menuId;
                }
            }
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                //创建菜单对象
                PageInfo page = new PageInfo();

                //菜单名称
                if (string.IsNullOrEmpty(this.txtFilePath.Value))
                {
                    YMessageBox.show(this, "路径不能为空！");
                    return;
                }
                else
                {
                    page.filePath = this.txtFilePath.Value;
                }

                page.detail = this.txtFileDetail.Value;
                page.menuId = Convert.ToInt32(this.hidMenuId.Value);

                //父菜单
                if (string.IsNullOrEmpty(this.hidPageId.Value))
                {
                    page.id = -1;
                }
                else
                {
                    page.id = Convert.ToInt32(this.hidPageId.Value);
                }

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + SystemConfig.databaseConfigFileName;

                //获取数据库实例。
                YDataBase menuDb = YDataBaseConfigFile.createDataBase(configFile, SystemConfig.databaseConfigNodeName, SystemConfig.configFileKey);

                if (menuDb != null)
                {
                    MenuOperater menuOper = new MenuOperater();
                    menuOper.menuDataBase = menuDb;

                    if (string.IsNullOrEmpty(this.hidPageId.Value))
                    {
                        //新增菜单
                        int iRet = menuOper.createNewPage(page);
                        if (iRet > 0)
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "", "window.location.href='setPage_list.aspx?menuId=" + this.hidMenuId.Value+ "';");
                        }
                        else
                        {
                            YMessageBox.show(this, "创建页面出错！错误信息[" + menuOper.errorMessage + "]");
                        }
                    }
                    else
                    {
                        //修改菜单
                        //menu.id = Convert.ToInt32(this.hidMenuId.Value);
                        //bool bRet = menuOper.changeMenu(menu);
                        //if (bRet)
                        //{
                        //    if (this.txtMenuURL.Disabled)
                        //    {
                        //        //分组
                        //        YMessageBox.showAndResponseScript(this, "保存成功！", "", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx?id=" + menu.id.ToString() + "');window.parent.closePopupsWindow('#popups');");
                        //    }
                        //    else
                        //    {
                        //        //菜单
                        //        YMessageBox.showAndResponseScript(this, "保存成功！", "", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/menu/menu_list.aspx?id=" + menu.parentID.ToString() + "');window.parent.closePopupsWindow('#popups');");
                        //    }
                        //}
                        //else
                        //{
                        //    YMessageBox.show(this, "修改菜单出错！错误信息[" + menuOper.errorMessage + "]");
                        //}
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