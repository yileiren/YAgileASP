using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using YLR.YMessage;
using YLR.YAdoNet;
using YLR.YSystem.Organization;
using YLR.YSystem.Menu;

namespace YAgileASP.background
{
    public partial class mainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //获取用户信息。
                UserInfo user = (UserInfo)Session["UserInfo"];
                try
                {
                    if (user != null)
                    {
                        this.userName.InnerText = user.name;
                        this.logName.InnerText = user.logName;
                    }
                    else
                    {
                        YMessageBox.showAndRedirect(this, "用户登陆超时，请重新登陆！", "sys/login.aspx");
                    }


                }
                catch (Exception ex)
                {
                    YMessageBox.showAndRedirect(this, "系统运行异常！异常信息[" + ex.Message + "]","sys/login.aspx");
                }

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

                        List<MenuInfo> menus = menuOper.getMainPageMunus(user.id);
                        this.menuGroup.DataSource = menus;
                        this.menuGroup.DataBind();
                    }
                    else
                    {
                        YMessageBox.show(this,"获取数据库实例失败！");
                    }
                }
                catch (Exception ex)
                {
                    YMessageBox.showAndRedirect(this, "系统运行异常！异常信息[" + ex.Message + "]", "sys/login.aspx");
                }
            }
        }

        /// <summary>
        /// 退出系统单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void logOut_onClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void menuGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rp = e.Item.FindControl("menuButton") as Repeater; //子菜单数据空间。
                MenuInfo pMenu = (MenuInfo)(e.Item.DataItem); //插入的菜单对象


                rp.DataSource = pMenu.childMenus;
                rp.DataBind();
            }
        }
    }
}