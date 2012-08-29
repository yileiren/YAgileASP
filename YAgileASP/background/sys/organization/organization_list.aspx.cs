using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YSystem.Organization;

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

                    //获取用户
                    this.bindUserInfos();
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
                    //获取父机构信息
                    if (this.hidParentId.Value == "-1")
                    {
                        this.spanParentName.InnerText = "顶级机构";
                        this.returnButton.Disabled = true;
                        this.hidReturnId.Value = "";
                    }
                    else
                    {
                        OrganizationInfo org = orgOper.getOrganization(Convert.ToInt32(this.hidParentId.Value));
                        this.spanParentName.InnerText = org.name;
                        this.hidReturnId.Value = org.parentId.ToString();
                    }

                    //获取组织机构列表
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

        /// <summary>
        /// 绑定用户。
        /// 作者：董帅 创建时间：2012-8-23 21:40:38
        /// </summary>
        public void bindUserInfos()
        {
            try
            {
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //获取数据库操作对象
                OrgOperater orgOper = OrgOperater.createOrgOperater(configFile, "SQLServer");
                if (orgOper != null)
                {
                    List<UserInfo> users = orgOper.getUserByOrganizationId(Convert.ToInt32(this.hidParentId.Value));
                    if (users != null)
                    {
                        this.userList.DataSource = users;
                        this.userList.DataBind();
                    }
                    else
                    {
                        YMessageBox.show(this, "获取用户数据失败！错误信息[" + orgOper.errorMessage + "]");
                    }
                }
                else
                {
                    YMessageBox.show(this, "获取数据库操作对象失败！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "运行错误！错误信息[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butDeleteItems_Click(object sender, EventArgs e)
        {
            try
            {
                string s = Request["chkOrg"];
                string[] orgIds = new string[0];
                string[] userIds = new string[0];
                if(!string.IsNullOrEmpty(s))
                {
                    orgIds = s.Split(','); //要删除的机构id
                }

                s = Request["chkUser"];
                if(!string.IsNullOrEmpty(s))
                {
                    userIds = s.Split(','); //要删除的用户id
                }

                if ((orgIds.Length + userIds.Length) > 0)
                {
                    //获取配置文件路径。
                    string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                    //创建数据库操作对象。
                    OrgOperater orgOper = OrgOperater.createOrgOperater(configFile, "SQLServer");
                    if (orgOper != null)
                    {

                        //删除机构和用户
                        int[] orgIntIds = new int[orgIds.Length];
                        for (int i = 0; i < orgIds.Length; i++)
                        {
                            orgIntIds[i] = Convert.ToInt32(orgIds[i]);
                        }

                        int[] userIntIds = new int[userIds.Length];
                        for (int i = 0; i < userIds.Length; i++)
                        {
                            userIntIds[i] = Convert.ToInt32(userIds[i]);
                        }

                        if (orgOper.deleteOrganizationAndUser(orgIntIds, userIntIds))
                        {
                            this.Response.Redirect("organization_list.aspx?parentId=" + this.hidParentId.Value);
                            //YMessageBox.showAndResponseScript(this, "删除数据成功！", "", "window.parent.menuButtonOnClick('组织机构管理','icon-organization','sys/organization/organization_list.aspx?parentId=" + this.hidParentId.Value + "')");
                        }
                        else
                        {
                            YMessageBox.show(this, "删除数据失败！错误信息[" + orgOper.errorMessage + "]");
                        }
                    }
                    else
                    {
                        YMessageBox.show(this, "获取数据库实例失败！");
                    }
                }
                else
                {
                    YMessageBox.show(this, "没有选择要删除的角色！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "系统运行异常！异常信息[" + ex.Message + "]");
            }
        }
    }
}