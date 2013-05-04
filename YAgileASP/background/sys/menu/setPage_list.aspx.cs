using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YAgileASP.background.sys.menu
{
    public partial class setPage_list : System.Web.UI.Page
    {
        protected string menuId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.menuId = Request.QueryString["menuId"];
                if (!string.IsNullOrEmpty(this.menuId))
                {
                    this.hidMenuId.Value = menuId;
                }
            }
        }
    }
}