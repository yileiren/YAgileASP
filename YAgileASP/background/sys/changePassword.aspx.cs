using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YCrypto;
using YLR.YMessage;
using YLR.YAdoNet;
using YLR.YSystem.Organization;

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
                        OrgOperater orgDB = new OrgOperater();

                        //获取配置文件路径。
                        string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                        //获取数据库实例。
                        YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");

                        if (orgDb != null)
                        {
                            //更新数据
                            OrgOperater orgOper = new OrgOperater();
                            orgOper.orgDataBase = orgDb;

                            bool bRet = orgOper.changePassword(user);

                            if (bRet)
                            {
                                //更新成功。
                                YMessageBox.showAndResponseScript(this, "修改用户密码成功！", "window.parent.closePopupsWindow('#popups');","");
                            }
                            else
                            {
                                //更新出错。
                                YMessageBox.show(this, "修改用户密码出错！错误信息[" + orgOper.errorMessage + "]");
                            }
                        }
                    }
                    else
                    {
                        //原密码验证出错。
                        YMessageBox.show(this,"原密码验证错误！");
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
                YMessageBox.show(this,"修改用户密码出错！错误信息[" + ex.Message + "]");
            }
        }
    }
}