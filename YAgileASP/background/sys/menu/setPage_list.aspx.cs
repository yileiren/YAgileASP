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

        /// <summary>
        /// 删除数据
        /// 作者：董帅 创建时间：2013-5-4 23:07:57
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butDeleteItems_Click(object sender, EventArgs e)
        {
            try
            {
                string s = Request["chkPage"];
                string[] dicIds = new string[0];
                if (!string.IsNullOrEmpty(s))
                {
                    dicIds = s.Split(','); //要删除的字典id
                }

                if (dicIds.Length > 0)
                {
                    //获取配置文件路径。
                    string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + SystemConfig.databaseConfigFileName;

                    //获取数据库实例。
                    YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, SystemConfig.databaseConfigNodeName, SystemConfig.configFileKey);

                    if (orgDb != null)
                    {
                        MenuOperater menuOper = new MenuOperater();
                        menuOper.menuDataBase = orgDb;

                        //删除字典项
                        int[] dicIntIds = new int[dicIds.Length];
                        for (int i = 0; i < dicIds.Length; i++)
                        {
                            dicIntIds[i] = Convert.ToInt32(dicIds[i]);
                        }

                        if (menuOper.deletePages(dicIntIds))
                        {
                            this.Response.Redirect("setPage_list.aspx?menuId=" + this.hidMenuId.Value);
                        }
                        else
                        {
                            YMessageBox.show(this, "删除数据失败！错误信息[" + menuOper.errorMessage + "]");
                        }
                    }
                    else
                    {
                        YMessageBox.show(this, "获取数据库实例失败！");
                    }
                }
                else
                {
                    YMessageBox.show(this, "没有选择要删除的页面！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "系统运行异常！异常信息[" + ex.Message + "]");
            }
        }
    }
}