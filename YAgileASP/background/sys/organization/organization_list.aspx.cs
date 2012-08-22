using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YOrganization;

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

                    //获取组织机构
                    this.bindOrgInfos();
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "程序运行出错！错误信息[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 绑定组织机构。
        /// 作者：董帅 创建时间：2012-8-22 13:45:41
        /// </summary>
        public void bindOrgInfos()
        {
            try
            {
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //获取数据库操作对象
                OrgOperater orgOper = OrgOperater.createOrgOperater(configFile,"SQLServer");
                if(orgOper != null)
                {
                    List<OrganizationInfo> orgs = orgOper.getOrganizationByParentId(Convert.ToInt32(this.hidParentId.Value));
                    if(orgs != null)
                    {
                        this.orgList.DataSource = orgs;
                        this.orgList.DataBind();
                    }
                    else
                    {
                        YMessageBox.show(this,"获取组织机构数据失败！错误信息[" + orgOper.errorMessage + "]");
                    }
                }
                else
                {
                    YMessageBox.show(this,"获取数据库操作对象失败！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this,"运行错误！错误信息[" + ex.Message + "]");
            }
        }
    }
}