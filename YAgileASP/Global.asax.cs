using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using YLR.YSystem.Organization;
using YLR.YMessage;

namespace YAgileASP
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string reqFile = Request.Path.ToString();
            //判断请求是否在background里。
            if (reqFile.IndexOf("/background") == 0 && reqFile.IndexOf("/background/sys/login.aspx") != 0)
            {
                string strNoFilterFiles = System.Configuration.ConfigurationManager.AppSettings["noFilterFiles"].ToString();
                string[] noFilterFiles = null;
                if (!string.IsNullOrEmpty(strNoFilterFiles))
                {
                    noFilterFiles = strNoFilterFiles.Split(',');
                }

                if (noFilterFiles != null && noFilterFiles.Length > 0)
                {
                    //判断请求文件是否不在过滤范围之内。
                    string extName = reqFile.Substring(reqFile.LastIndexOf("."));
                    for (int i = 0; i < noFilterFiles.Length; i++)
                    {
                        if (extName == noFilterFiles[i])
                        {
                            return;
                        }
                    }
                }

                HttpApplication app = (HttpApplication)sender;
                UserInfo user = (UserInfo)app.Session["UserInfo"];
                if (null != user)
                {
                }
                else
                {
                    //登陆超时。
                    Response.Write("用户登陆超时！");
                    Response.Redirect("sys/login.aspx");
                }

            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}