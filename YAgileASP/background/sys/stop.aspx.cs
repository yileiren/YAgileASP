﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YAgileASP.background.sys
{
    public partial class stop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string waringText = Request.QueryString["waringText"];
                if (waringText != null)
                {
                    this.waringText.InnerText = waringText;
                }
            }
        }
    }
}