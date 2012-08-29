using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YAdoNet;
using YLR.YMessage;
using YLR.YSystem.Role;

namespace YAgileASP.background.sys.role
{
    public partial class chouseMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //获取菜单
                try
                {
                    //获取id，没有id表示新增，否则为修改
                    string roleId = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        this.hidRoleId.Value = roleId;
                    }
                    //获取配置文件路径。
                    string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                    //获取数据库实例。
                    YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");

                    if (orgDb != null)
                    {
                        RoleOperater roleOper = new RoleOperater();
                        roleOper.roleDataBase = orgDb;

                        List<RoleMenuInfo> menus = roleOper.getChouseMenus(Convert.ToInt32(this.hidRoleId.Value));
                        this.menuGroup.DataSource = menus;
                        this.menuGroup.DataBind();
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

        protected void menuGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rp = e.Item.FindControl("menuButton") as Repeater; //子菜单数据空间。
                RoleMenuInfo pMenu = (RoleMenuInfo)(e.Item.DataItem); //插入的菜单对象


                rp.DataSource = pMenu.childMenus;
                rp.DataBind();
            }
        }

        protected void butChouse_Click(object sender, EventArgs e)
        {
            try
            {
                string strIds = Request["chkItem"];
                string[] ids = new string[0];
                if (!string.IsNullOrEmpty(strIds))
                {
                    ids = strIds.Split(',');
                }

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //创建数据库操作对象。
                RoleOperater roleOper = RoleOperater.createRoleOperater(configFile, "SQLServer");
                if (roleOper != null)
                {

                    //删除数据
                    int[] intIds = new int[ids.Length];
                    for (int i = 0; i < intIds.Length; i++)
                    {
                        intIds[i] = Convert.ToInt32(ids[i]);
                    }

                    if (roleOper.chouseRoleMenus(Convert.ToInt32(this.hidRoleId.Value),intIds))
                    {
                        YMessageBox.showAndResponseScript(this, "选择菜单成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('角色管理','icon-role','sys/role/role_list.aspx')");
                    }
                    else
                    {
                        YMessageBox.show(this, "选择菜单失败！错误信息[" + roleOper.errorMessage + "]");
                    }
                }
                else
                {
                    YMessageBox.show(this, "获取数据库实例失败！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "系统运行异常！异常信息[" + ex.Message + "]");
            }
        }
    }
}