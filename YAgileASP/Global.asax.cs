using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using YLR.YSystem.Organization;
using YLR.YMessage;
using YLR.YSystem.Menu;
using YAgileASP.background.sys;
using YLR.YSystem.Role;
using System.IO;

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
            if("/" != Request.ApplicationPath.ToString())
            {
                reqFile = reqFile.Replace(Request.ApplicationPath.ToString(), "");
            }

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
                    if (user.id == 1)
                    {
                        return;
                    }

                    
                    

                    //查询页面是否与菜单关联。
                    string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + SystemConfig.databaseConfigFileName;

                    MenuOperater oper = MenuOperater.createMenuOperater(configFile,SystemConfig.databaseConfigNodeName, SystemConfig.configFileKey);
                    if (oper != null)
                    {
                        if (oper.pageExists(reqFile))
                        {
                            //页面关联，需要进行权限验证。
                            RoleOperater roleOper = RoleOperater.createRoleOperater(configFile, SystemConfig.databaseConfigNodeName, SystemConfig.configFileKey);
                            if (roleOper != null)
                            {
                                if (!roleOper.pageAllowRequest(user.id, reqFile))
                                {
                                    //页面不允许访问。
                                    Response.Redirect("~/background/sys/stop.aspx?waringText=你正试图访问未经授权的页面！", true);
                                }
                            }
                            else
                            {
                                //访问权限数据库初始化出错。
                                Response.Redirect("~/background/sys/stop.aspx?waringText=访问权限数据库初始失败！", true);
                            }
                        }
                    }
                    else
                    { 
                        //页面数据库初始化失败。
                        Response.Redirect("~/background/sys/stop.aspx?waringText=页面关联数据库初始失败！", true);
                    }

                    
                }
                else
                {
                    //登陆超时。
                    //Response.Write("用户登陆超时！");
                    Response.Redirect("~/background/sys/login.aspx",true);
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