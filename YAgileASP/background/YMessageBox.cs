using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YMessage
{
    /// <summary>
    /// 弹出提示窗口封装。
    /// </summary>
    public class YMessageBox
    {
        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.Replace("\\","\\\\").Replace("'","\\'").Replace("\"","\\\"") + "');</script>");
        }

        /// <summary>
        /// 显示消息提示框，并执行脚本。
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息。</param>
        /// <param name="beginScript">提示信息前执行的脚本。</param>
        /// <param name="endScript">提示信息后执行的脚本</param>
        public static void showAndResponseScript(System.Web.UI.Page page, string msg, string beginScript,string endScript)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + beginScript + ";alert('" + msg.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"") + "');" + endScript + "</script>");
        }

        /// <summary>
        /// 使用EasyLayout布局的页面显示消息提示框，并执行脚本，处理消息重复加载的bug。
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息。</param>
        /// <param name="beginScript">提示信息前执行的脚本。</param>
        /// <param name="endScript">提示信息后执行的脚本</param>
        public static void easyLayoutShowAndResponseScript(System.Web.UI.Page page, string msg, string beginScript, string endScript)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", @"<script language='javascript' defer>
                                                                                    if(window.__yltlClientScriptRegistKey == null 
                                                                                        || window.__yltlClientScriptRegistKey == undefined 
                                                                                        || window.__yltlClientScriptRegistKey !='somekey') 
                                                                                    { 
                                                                                        window.__yltlClientScriptRegistKey = 'somekey';
                                                                                    }
                                                                                    else if(window.__yltlClientScriptRegistKey =='somekey')
                                                                                    {
                                                                                        " + beginScript + @";
                                                                                        alert('" + msg.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"") + @"');
                                                                                        " + endScript + @"
                                                                                    }
                                                                                  </script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void showConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"") + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void showAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\""));
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(),"message", Builder.ToString());

        }
        public static void showConfirmAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("return confirm(('{0}');", msg.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\""));
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

        }
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void responseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script);
        }
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void responseScript(string script)
        {
            System.Web.HttpContext.Current.Response.Write("<script language='javascript' defer>" + script + "</script>");
        }
        /// <summary>
        /// 获得焦点
        /// </summary>
        /// <param name="ctrl">控件名（this.TextBox）</param>
        /// <param name="page">this.page</param>
        public static void setFocus(System.Web.UI.Control ctrl, System.Web.UI.Page page)
        {
            string s = "<SCRIPT language='javascript' defer>document.getElementById('" + ctrl.ID + "').focus() </SCRIPT>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "focus", s);

        }
    }
}
