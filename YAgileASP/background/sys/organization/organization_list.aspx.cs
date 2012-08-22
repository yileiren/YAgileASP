using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;

namespace YAgileASP.background.sys.organization
{
    public partial class organization_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    //获取父id
                    string parentId = Request.QueryString["parentId"];
                    if (!string.IsNullOrEmpty(parentId))
                    {
                        this.hidParentId.Value = parentId;
                    }
                    else
                    {
                        this.hidParentId.Value = "-1";
                    }
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "程序运行出错！错误信息[" + ex.Message + "]");
            }
        }
    }
}