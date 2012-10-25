using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YLR.YAgileControls
{
    [DefaultProperty("PageCount")]
    [ToolboxData("<{0}:YPagerControl runat=server></{0}:YPagerControl>")]
    public class YPagerControl : WebControl
    {
        /// <summary>
        /// 总页数。
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int PageCount
        {
            get
            {
                return (ViewState["PageCount"] == null ) ? 0 : (int)ViewState["PageCount"];
            }

            set
            {
                ViewState["PageCount"] = value;
            }
        }

        /// <summary>
        /// 当前显示的页号，从1开始。
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue(1)]
        [Localizable(true)]
        public int PageNum
        {
            get
            {
                return (ViewState["PageNum"] == null) ? 1 : (int)ViewState["PageNum"];
            }

            set
            {
                ViewState["PageNum"] = value;
            }
        }

        /// <summary>
        /// 每页显示的数据数量，默认是20。
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue(20)]
        [Localizable(true)]
        public int DataCount
        {
            get
            {
                return (ViewState["DataCount"] == null) ? 20 : (int)ViewState["DataCount"];
            }

            set
            {
                ViewState["DataCount"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write("数据共" + this.PageCount.ToString() + "页&nbsp;&nbsp;当前第" + this.PageNum + "页&nbsp;&nbsp;");
        }
    }
}
