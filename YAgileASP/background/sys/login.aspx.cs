using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YAdoNet;
using YLR.YMessage;
using YLR.YCrypto;
using System.Web.Security;
using YLR.YSystem.Organization;

namespace YAgileASP.background.sys
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
            }
        }

        protected void butLogin_Click(object sender, EventArgs e)
        { 
            //用户登陆
            try
            {
                //校验数据
                if (string.IsNullOrEmpty(this.txtUserName.Value))
                {
                    //用户名不能为空
                    YMessageBox.show(this, "用户名不能为空，请输入用户名后重新登录！");
                    return;
                }

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //获取数据库实例。
                YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");
                
                if (orgDb != null)
                {
                    //获取用户
                    OrgOperater orgOper = new OrgOperater();
                    orgOper.orgDataBase = orgDb;
                    
                    //用户密码二次加密
                    MD5Encrypt md5Encrypt = new MD5Encrypt();
                    UserInfo logUser = orgOper.getUser(this.txtUserName.Value, md5Encrypt.GetMD5(this.passUserPassword.Value));

                    if (logUser != null && logUser.id > 0)
                    {
                        //验证成功，跳转主页
                        FormsAuthentication.RedirectFromLoginPage(logUser.id.ToString(), false);
                        Session.Add("UserInfo", logUser);
                    }
                    else
                    { 
                        //验证失败
                        YMessageBox.show(this,"用户验证失败，请核对用户名和密码后重新登录！");
                    }
                }
            }
            catch(Exception ex)
            {
                YMessageBox.show(this,"用户登陆异常！异常消息[" + ex.Message + "]");
            }
        }
    }
}