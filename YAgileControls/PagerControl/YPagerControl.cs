using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace YLR.YAgileControls.PagerControl
{
    [ToolboxBitmap(typeof(YPagerControl), "YPagerControl.png")]
    [DefaultProperty("PageCount")]
    [ToolboxData("<{0}:YPagerControl runat=server></{0}:YPagerControl>")]
    public class YPagerControl : WebControl, IPostBackEventHandler
    {
        //页面改变事件。
        public event EventHandler PageChanged = null;

        /// <summary>
        /// 总页数。
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue(1)]
        [Localizable(true)]
        public int PageCount
        {
            get
            {
                return (ViewState["PageCount"] == null ) ? 1 : (int)ViewState["PageCount"];
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

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            //显示首页按钮
            output.Write("<input type=\"button\" value=\"首&nbsp;&nbsp;页\" " + ((this.PageNum == 1) ? "disabled=\"disabled\"" : "") + " style=\"height:25px;border:solid 2px #dcdcdc;padding:2px 4px 2px 4px;background-color:#EEF5FD;\" onclick=" + Page.ClientScript.GetPostBackClientHyperlink(this, "First") + " onmouseover=\"this.style.borderColor='#75cd02'\" onmouseout=\"this.style.borderColor='#dcdcdc'\" />");

            //显示上一页按钮
            output.Write("<input type=\"button\" value=\"上一页\" " + ((this.PageNum == 1) ? "disabled=\"disabled\"" : "") + " style=\"height:25px;border:solid 2px #dcdcdc;padding:2px 4px 2px 4px;background-color:#EEF5FD;\" onclick=" + Page.ClientScript.GetPostBackClientHyperlink(this, "PageUp") + " onmouseover=\"this.style.borderColor='#75cd02'\" onmouseout=\"this.style.borderColor='#dcdcdc'\" />");

            //显示数据总页数和当前页
            output.AddAttribute(HtmlTextWriterAttribute.Style, "height:25px;padding:2px 4px 2px 4px;font-size:12px");
            output.RenderBeginTag(HtmlTextWriterTag.Span);
            output.Write("第" + this.PageNum + "页,共" + this.PageCount.ToString() + "页,每页" + this.DataCount + "条数据");
            output.RenderEndTag();

            //显示下一页按钮
            output.Write("<input type=\"button\" value=\"下一页\" " + ((this.PageNum == this.PageCount) ? "disabled=\"disabled\"" : "") + " style=\"height:25px;border:solid 2px #dcdcdc;padding:2px 4px 2px 4px;background-color:#EEF5FD;\" onclick=" + Page.ClientScript.GetPostBackClientHyperlink(this, "PageDown") + " onmouseover=\"this.style.borderColor='#75cd02'\" onmouseout=\"this.style.borderColor='#dcdcdc'\" />");

            //显示尾页按钮
            output.Write("<input type=\"button\" value=\"尾&nbsp;&nbsp;页\" " + ((this.PageNum == this.PageCount) ? "disabled=\"disabled\"" : "") + " style=\"height:25px;border:solid 2px #dcdcdc;padding:2px 4px 2px 4px;background-color:#EEF5FD;\" onclick=" + Page.ClientScript.GetPostBackClientHyperlink(this, "Last") + " onmouseover=\"this.style.borderColor='#75cd02'\" onmouseout=\"this.style.borderColor='#dcdcdc'\" />");
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "First")
            {
                this.PageNum = 1;
            }
            else if (eventArgument == "PageUp")
            {
                this.PageNum--;
            }
            else if (eventArgument == "PageDown")
            {
                this.PageNum++;
            }
            else
            {
                this.PageNum = this.PageCount;
            }

            this.OnPageChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 分页控件页面改变事件，在此进行数据处理。
        /// </summary>
        /// <param name="e"></param>
        public void OnPageChanged(EventArgs e)
        {
            if (this.PageChanged != null)
            {
                PageChanged(this, e);
            }
        }
    }
}
