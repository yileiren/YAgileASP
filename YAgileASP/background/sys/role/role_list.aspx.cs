using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YRole;
using YLR.YMessage;

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
    }
}