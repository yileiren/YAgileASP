using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YAgileControls.PagerControl;

namespace WebTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void YPagerControl1_PageChanged(object sender, EventArgs e)
        {
            this.Label1.Text = ((YPagerControl)sender).PageNum.ToString();
        }
    }
}