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
    public partial class organization_edit : System.Web.UI.Page
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
                        YMessageBox.show(this, "未设置父菜单id失败！");
                    }

                    //获取id
                    string strId = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(strId))
                    {
                        this.hidOrgId.Value = strId;

                        
                        //获取配置文件路径。
                        string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                        //创建操作对象
                        OrgOperater orgOper = OrgOperater.createOrgOperater(configFile, "SQLServer");
                        if (orgOper != null)
                        {
                            //获取机构信息
                            OrganizationInfo org = orgOper.getOrganization(Convert.ToInt32(strId));
                            if (org != null)
                            {
                                this.txtOrgName.Value = org.name;
                                this.txtOrgOrder.Value = org.order.ToString();
                            }
                            else
                            {
                                YMessageBox.show(this, "获取机构信息失败！错误信息[" + orgOper.errorMessage + "]");
                                return;
                            }
                        }
                        else
                        {
                            YMessageBox.show(this, "创建数据库操作对象失败！");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "程序运行出错！错误信息[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                OrganizationInfo orgInfo = new OrganizationInfo();
                
                orgInfo.name = this.txtOrgName.Value;
                if (string.IsNullOrEmpty(orgInfo.name) || orgInfo.name.Length > 50)
                {
                    YMessageBox.show(this, "机构名称不合法！");
                    return;
                }

                orgInfo.order = Convert.ToInt32(this.txtOrgOrder.Value);

                orgInfo.parentId = Convert.ToInt32(this.hidParentId.Value);

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //创建操作对象
                OrgOperater orgOper = OrgOperater.createOrgOperater(configFile, "SQLServer");
                if (orgOper != null)
                {
                    if (string.IsNullOrEmpty(this.hidOrgId.Value))
                    {
                        //新增
                        if (orgOper.createNewOrganization(orgInfo) > 0)
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('组织机构管理','icon-organization','sys/organization/organization_list.aspx?parentId=" + this.hidParentId.Value + "')");
                        }
                        else
                        {
                            YMessageBox.show(this, "创建机构失败！错误信息：[" + orgOper.errorMessage + "]");
                            return;
                        }
                    }
                    else
                    {
                        //修改
                        orgInfo.id = Convert.ToInt32(this.hidOrgId.Value);
                        if (orgOper.changeOrganization(orgInfo))
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('组织机构管理','icon-organization','sys/organization/organization_list.aspx?parentId=" + this.hidParentId.Value + "')");
                        }
                        else
                        {
                            YMessageBox.show(this, "修改机构失败！错误信息：[" + orgOper.errorMessage + "]");
                            return;
                        }
                    }
                }
                else
                {
                    YMessageBox.show(this, "创建数据库操作对象失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this,"程序异常！错误信息[" + ex.Message + "]");
            }
        }
    }
}