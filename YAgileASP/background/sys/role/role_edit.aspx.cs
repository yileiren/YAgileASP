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
    public partial class role_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    //获取id，没有id表示新增，否则为修改
                    string roleId = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        this.hidRoleId.Value = roleId;
                        //获取配置文件路径。
                        string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                        //创建数据库操作对象。
                        RoleOperater roleOper = RoleOperater.createRoleOperater(configFile, "SQLServer");
                        if (roleOper != null)
                        {
                            RoleInfo role = roleOper.getRole(Convert.ToInt32(roleId));
                            if(role != null)
                            {
                                this.txtRoleName.Value = role.name;
                                this.txtRoleExplain.Value = role.explain;
                            }
                        }
                        else
                        {
                            YMessageBox.show(this, "创建数据库对象失败！");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    YMessageBox.show(this, "错误信息：[" + ex.Message + "]");
                }
            }
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
                    if (string.IsNullOrEmpty(this.hidRoleId.Value))
                    {
                        //新增
                        int retId = roleOper.createNewRole(role);
                        if (retId > 0)
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('角色管理','icon-role','sys/role/role_list.aspx')");
                        }
                        else
                        {
                            YMessageBox.show(this, "创建角色失败！错误信息：[" + roleOper.errorMessage + "]");
                            return;
                        }
                    }
                    else
                    {
                        //修改
                        role.id = Convert.ToInt32(this.hidRoleId.Value);
                        bool bRet = roleOper.changeRole(role);
                        if (bRet)
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('角色管理','icon-role','sys/role/role_list.aspx')");
                        }
                        else
                        {
                            YMessageBox.show(this, "修改角色失败！错误信息：[" + roleOper.errorMessage + "]");
                            return;
                        }
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