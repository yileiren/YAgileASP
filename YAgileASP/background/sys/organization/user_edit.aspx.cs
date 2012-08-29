using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YCrypto;
using YLR.YSystem.Organization;

namespace YAgileASP.background.sys.organization
{
    public partial class user_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //获取父id
                string orgId = Request.QueryString["orgId"];
                if (!string.IsNullOrEmpty(orgId))
                {
                    this.hidOrgId.Value = orgId;
                }
                else
                {
                    YMessageBox.show(this, "未设置组织机构id！");
                }

                //获取id
                string strId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(strId))
                {
                    this.hidUserId.Value = strId;

                    this.txtUserLogName.Disabled = true;
                    //获取配置文件路径。
                    string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                    //创建操作对象
                    OrgOperater orgOper = OrgOperater.createOrgOperater(configFile, "SQLServer");
                    if (orgOper != null)
                    {
                        ////获取用户信息
                        UserInfo user = orgOper.getUser(Convert.ToInt32(strId));
                        if (user != null)
                        {
                            this.txtUserLogName.Value = user.logName;
                            this.txtUserName.Value = user.name;
                            this.txtUserOrder.Value = user.order.ToString();
                        }
                        else
                        {
                            YMessageBox.show(this, "获取机构信息失败！错误信息[" + orgOper.errorMessage + "]");
                            return;
                        }
                    }
                    else
                    {
                        YMessageBox.show(this, "创建数据库操作对象失败！");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo user = new UserInfo();

                user.logName = this.txtUserLogName.Value;
                if (string.IsNullOrEmpty(user.logName) || user.logName.Length > 20)
                {
                    YMessageBox.show(this, "用户名不合法！");
                    return;
                }

                user.logPassword = this.txtUserLogPassword1.Value;
                if (string.IsNullOrEmpty(user.logPassword) || user.logPassword.Length > 40)
                {
                    //新增时报错
                    if (string.IsNullOrEmpty(this.hidUserId.Value))
                    {
                        YMessageBox.show(this, "用户登陆密码不合法！");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(user.logPassword))
                {
                    //用户密码二次加密
                    MD5Encrypt md5Encrypt = new MD5Encrypt();
                    user.logPassword = md5Encrypt.GetMD5(user.logPassword);
                }

                user.name = this.txtUserName.Value;
                if (string.IsNullOrEmpty(user.name) || user.name.Length > 20)
                {
                    YMessageBox.show(this, "姓名不合法！");
                    return;
                }

                user.order = Convert.ToInt32(this.txtUserOrder.Value);

                user.organizationId = Convert.ToInt32(this.hidOrgId.Value);

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //创建操作对象
                OrgOperater orgOper = OrgOperater.createOrgOperater(configFile, "SQLServer");
                if (orgOper != null)
                {
                    if (string.IsNullOrEmpty(this.hidUserId.Value))
                    {
                        //判断用户是否存在
                        if (orgOper.existUser(user.logName))
                        {
                            YMessageBox.show(this, "用户名已存在，请更换用户名后重试！");
                            return;
                        }

                        //新增
                        if (orgOper.createNewUser(user) > 0)
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('组织机构管理','icon-organization','sys/organization/organization_list.aspx?parentId=" + this.hidOrgId.Value + "')");
                        }
                        else
                        {
                            YMessageBox.show(this, "创建机构失败！错误信息：[" + orgOper.errorMessage + "]");
                            return;
                        }
                    }
                    else
                    {
                        //修改
                        user.id = Convert.ToInt32(this.hidUserId.Value);
                        if (orgOper.changeUser(user))
                        {
                            bool bRet = true;
                            if (!string.IsNullOrEmpty(user.logPassword))
                            { 
                                //修改密码
                                bRet = orgOper.changePassword(user);

                            }
                            if (bRet)
                            {
                                YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('组织机构管理','icon-organization','sys/organization/organization_list.aspx?parentId=" + this.hidOrgId.Value + "')");
                            }
                            else
                            {
                                YMessageBox.show(this, "修改密码失败！错误信息：[" + orgOper.errorMessage + "]");
                            }
                        }
                        else
                        {
                            YMessageBox.show(this, "修改用户失败！错误信息：[" + orgOper.errorMessage + "]");
                            return;
                        }
                    }
                }
                else
                {
                    YMessageBox.show(this, "创建数据库操作对象失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "程序异常！错误信息[" + ex.Message + "]");
            }
        }
    }
}