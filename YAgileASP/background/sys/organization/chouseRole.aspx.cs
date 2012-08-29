using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YSystem.Role;

namespace YAgileASP.background.sys.organization
{
    public partial class chouseRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //获取用户id
                try
                {
                    //获取用户id
                    string userId = Request.QueryString["userId"];
                    if (!string.IsNullOrEmpty(userId))
                    {
                        this.hidUserId.Value = userId;
                    }
                    else
                    {
                        YMessageBox.show(this,"获取用户id失败！");
                        return;
                    }

                    this.bindData();
                }
                catch (Exception ex)
                {
                    YMessageBox.showAndRedirect(this, "系统运行异常！异常信息[" + ex.Message + "]", "sys/login.aspx");
                }
                
            }
        }

        /// <summary>
        /// 绑定数据。
        /// 作者：董帅 创建时间：2012-8-27 17:24:18
        /// </summary>
        private void bindData()
        {
            try
            {
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //创建数据库操作对象。
                RoleOperater roleOper = RoleOperater.createRoleOperater(configFile, "SQLServer");
                if (roleOper != null)
                {
                    List<UserRoleInfo> roles = roleOper.getChouseRoles(Convert.ToInt32(this.hidUserId.Value));
                    if (roles != null)
                    {
                        this.rolesRepeater.DataSource = roles;
                        this.rolesRepeater.DataBind();
                    }
                    else
                    {
                        YMessageBox.show(this, "查询出错！错误信息：[" + roleOper.errorMessage + "]");
                    }
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "查询出错！错误信息：[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 选择角色保存方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    if (roleOper.chouseUserRoles(Convert.ToInt32(this.hidUserId.Value), intIds))
                    {
                        YMessageBox.showAndResponseScript(this, "选择角色成功！", "window.parent.closePopupsWindow('#popups');", "");
                    }
                    else
                    {
                        YMessageBox.show(this, "选择角色失败！错误信息[" + roleOper.errorMessage + "]");
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