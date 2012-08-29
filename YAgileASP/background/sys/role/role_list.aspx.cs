using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YSystem.Role;

namespace YAgileASP.background.sys.role
{
    public partial class role_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.bindData();
            }
        }

        /// <summary>
        /// 绑定数据。
        /// 作者：董帅 创建时间：2012-8-16 21:54:54
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
                    List<RoleInfo> roles = roleOper.getRoles();
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

        protected void butDeleteRoles_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ids = Request["chkItem"].Split(',');

                if (ids.Length > 0)
                {
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

                        if (roleOper.deleteRoles(intIds))
                        {
                            this.Response.Redirect("role_list.aspx");
                            //YMessageBox.showAndResponseScript(this, "删除数据成功！", "", "window.parent.menuButtonOnClick('角色管理','icon-role','sys/role/role_list.aspx')");
                        }
                        else
                        {
                            YMessageBox.show(this, "删除数据失败！请确认是否有用户设置了该角色！");
                        }
                    }
                    else
                    {
                        YMessageBox.show(this, "获取数据库实例失败！");
                    }
                }
                else
                {
                    YMessageBox.show(this, "没有选择要删除的角色！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "系统运行异常！异常信息[" + ex.Message + "]");
            }
        }
    }
}