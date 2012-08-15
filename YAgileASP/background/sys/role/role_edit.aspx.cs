using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YRole;

namespace YAgileASP.background.sys.role
{
    public partial class role_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存菜单。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            { 
                //创建角色对象
                RoleInfo role = new RoleInfo();

                role.name = this.txtRoleName.Value;
                if (string.IsNullOrEmpty(role.name) || role.name.Length > 20)
                {
                    YMessageBox.show(this, "角色名称不合法！");
                    return;
                }

                role.explain = this.txtRoleExplain.Value;
                if (role.explain.Length > 50)
                {
                    YMessageBox.show(this, "角色说明不合法！");
                    return;
                }

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //创建数据库操作对象。
                RoleOperater roleOper = RoleOperater.createRoleOperater(configFile, "SQLServer");
                if (roleOper != null)
                {
                    int retId = roleOper.createNewRole(role);
                    if (retId > 0)
                    {
                        YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('系统菜单','icon-menu','sys/role/role_list.aspx')");
                    }
                    else
                    {
                        YMessageBox.show(this, "创建角色失败！错误信息：[" + roleOper.errorMessage + "]");
                        return;
                    }
                }
                else
                {
                    YMessageBox.show(this, "创建数据库对象失败！");
                    return;
                }
            }
            catch(Exception ex)
            {
                YMessageBox.show(this,"保存出错！错误信息：[" + ex.Message + "]");
            }
        }
    }
}