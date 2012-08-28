using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YDataDictionary;

namespace YAgileASP.background.sys.dataDictionary
{
    public partial class dataDictionary_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

                //获取id
                string strId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(strId))
                {
                    
                }
            }
        }

        /// <summary>
        /// 保存数据。
        /// 作者：董帅 创建时间：2012-8-28 17:28:54
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataDictionaryInfo dataDicInfo = new DataDictionaryInfo();

                dataDicInfo.name = this.txtDicName.Value;
                if (string.IsNullOrEmpty(dataDicInfo.name) || dataDicInfo.name.Length > 20)
                {
                    YMessageBox.show(this, "名称不合法！");
                    return;
                }

                dataDicInfo.value = Convert.ToInt32(this.txtDicValue.Value);
                dataDicInfo.code = this.txtDicCode.Value;
                dataDicInfo.order = Convert.ToInt32(this.txtDicOrder.Value);
                dataDicInfo.parentId = Convert.ToInt32(this.hidParentId.Value);

                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //创建操作对象
                DataDicOperater dataDicOper = DataDicOperater.createDataDicOperater(configFile, "SQLServer");
                if (dataDicOper != null)
                {
                    if (string.IsNullOrEmpty(this.hidDicId.Value))
                    {
                        //新增
                        if (dataDicOper.createNewDataDictionary(dataDicInfo) > 0)
                        {
                            YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('数据字典','icon-dictionary','sys/dataDictionary/dataDictionary_list.aspx?parentId=" + this.hidParentId.Value + "')");
                        }
                        else
                        {
                            YMessageBox.show(this, "创建机构失败！错误信息：[" + dataDicOper.errorMessage + "]");
                            return;
                        }
                    }
                    else
                    {
                        //修改
                        //user.id = Convert.ToInt32(this.hidUserId.Value);
                        //if (orgOper.changeUser(user))
                        //{
                        //    bool bRet = true;
                        //    if (!string.IsNullOrEmpty(user.logPassword))
                        //    {
                        //        //修改密码
                        //        bRet = orgOper.changePassword(user);

                        //    }
                        //    if (bRet)
                        //    {
                        //        YMessageBox.showAndResponseScript(this, "保存成功！", "window.parent.closePopupsWindow('#popups');", "window.parent.menuButtonOnClick('组织机构管理','icon-organization','sys/organization/organization_list.aspx?parentId=" + this.hidOrgId.Value + "')");
                        //    }
                        //    else
                        //    {
                        //        YMessageBox.show(this, "修改密码失败！错误信息：[" + orgOper.errorMessage + "]");
                        //    }
                        //}
                        //else
                        //{
                        //    YMessageBox.show(this, "修改用户失败！错误信息：[" + orgOper.errorMessage + "]");
                        //    return;
                        //}
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
                YMessageBox.show(this, "程序异常！错误信息[" + ex.Message + "]");
            }
        }
    }
}