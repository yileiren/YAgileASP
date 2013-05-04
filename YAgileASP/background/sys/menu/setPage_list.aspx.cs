using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YAdoNet;
using YLR.YSystem.Menu;
using YLR.YMessage;

namespace YAgileASP.background.sys.menu
{
    public partial class setPage_list : System.Web.UI.Page
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
                    this.bindData();
                }
            }
        }

        /// <summary>
        /// 绑定数据。
        /// </summary>
        private void bindData()
        {
            //获取菜单
            try
            {
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + SystemConfig.databaseConfigFileName;

                //获取数据库实例。
                YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, SystemConfig.databaseConfigNodeName, SystemConfig.configFileKey);

                if (orgDb != null)
                {
                    MenuOperater menuOper = new MenuOperater();
                    menuOper.menuDataBase = orgDb;

                    //绑定分组
                    List<PageInfo> pages = menuOper.getPageByMenuId(Convert.ToInt32(this.hidMenuId.Value));
                    if (pages != null)
                    {
                        this.pageList.DataSource = pages;
                        this.pageList.DataBind();
                    }
                    else
                    {
                        YMessageBox.show(this, "获取数据失败！");
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