using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YOrganization;
using System.Web.Security;
using YLR.YMessage;

namespace YAgileASP.background
{
    public partial class mainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    UserInfo user = (UserInfo)Session["UserInfo"];
                    if (user != null)
                    {
                        this.userName.InnerText = user.name;
                        this.logName.InnerText = user.logName;
                    }
                    else
                    {
                        YMessageBox.showAndRedirect(this, "用户登陆超时，请重新登陆！", "sys/login.aspx");
                    }
                }
                catch (Exception ex)
                {
                    YMessageBox.showAndRedirect(this, "系统运行异常！异常信息[" + ex.Message + "]","sys/login.aspx");
                }
            }
        }

        /// <summary>
        /// 退出系统单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void logOut_onClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}