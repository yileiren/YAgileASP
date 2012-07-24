using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YAdoNet;
using YLR.YMessage;

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
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFile, "SQLServer");

            }
            catch(Exception ex)
            {
                YMessageBox.show(this,"用户登陆异常！异常消息[" + ex.Message + "]");
            }
        }
    }
}