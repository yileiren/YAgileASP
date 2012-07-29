using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YOrganization;
using YLR.YCrypto;

namespace YAgileASP.background.sys
{
    public partial class changePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butChange_onClick(object sender, EventArgs e)
        {
            try
            {
                UserInfo user = (UserInfo)Session["UserInfo"];
                if (user != null)
                {
                    MD5Encrypt md5Encrypt = new MD5Encrypt();
                    if (user.logPassword == md5Encrypt.GetMD5(this.pswOldPsw.Value))
                    {
                        user.logPassword = md5Encrypt.GetMD5(this.pswNewPsw1.Value);
                        OrganizationDataBase orgDB = new OrganizationDataBase();
                        if (orgDB.changePassword(user))
                        {
                            Session["User"] = user;
                            YMessageBox.showMessageBox(this.UpdatePanel1, "提示", "密码修改成功！", YMessageType.Info, "", "$('.easyui-linkbutton').linkbutton({});");
                            //this.message.InnerHtml = YMessageBox.createMessageBox("提示", "密码修改成功！", YMessageType.Info);
                        }
                        else
                        {
                            //提示错误信息
                            YMessageBox.showMessageBox(this.UpdatePanel1, "提示", "修改密码失败！", YMessageType.Info, "", "$('.easyui-linkbutton').linkbutton({});");
                            //this.message.InnerHtml = YMessageBox.createMessageBox("提示", "原密码输入错误，请重新输入！", YMessageType.Info);
                        }
                    }
                    else
                    {
                        //提示错误信息
                        YMessageBox.showMessageBox(this.UpdatePanel1, "提示", "原密码输入错误，请重新输入！", YMessageType.Info, "", "$('.easyui-linkbutton').linkbutton({});");
                        //this.message.InnerHtml = YMessageBox.createMessageBox("提示","原密码输入错误，请重新输入！",YMessageType.Info);
                    }
                }
                else
                {
                    Exception ex = new Exception("用户登陆超时，请重新登陆！");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                YMessageBox.showMessageBox(this.UpdatePanel1, "提示", ex.Message, YMessageType.Info, "", "$('.easyui-linkbutton').linkbutton({});");
                //this.message.InnerHtml = YMessageBox.createMessageBox("提示", ex.Message, YMessageType.Info);
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}