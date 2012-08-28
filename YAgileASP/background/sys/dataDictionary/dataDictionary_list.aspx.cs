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
    public partial class dataDictionary : System.Web.UI.Page
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

                    this.bindDicInfos();
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "程序运行出错！错误信息[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 绑定字典项。
        /// 作者：董帅 创建时间：2012-8-28 21:46:08
        /// </summary>
        public void bindDicInfos()
        {
            try
            {
                //获取配置文件路径。
                string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                //获取数据库操作对象
                DataDicOperater dicOper = DataDicOperater.createDataDicOperater(configFile, "SQLServer");
                if (dicOper != null)
                {
                    //获取父字典项
                    if (this.hidParentId.Value == "-1")
                    {
                        this.spanParentName.InnerText = "顶级字典";
                        this.returnButton.Disabled = true;
                        this.hidReturnId.Value = "";
                    }
                    else
                    {
                        DataDictionaryInfo org = dicOper.getDataDictionary(Convert.ToInt32(this.hidParentId.Value));
                        this.spanParentName.InnerText = org.name;
                        this.hidReturnId.Value = org.parentId.ToString();
                    }

                    //获取组织机构列表
                    List<DataDictionaryInfo> dics = dicOper.getDataDictionaryByParentId(Convert.ToInt32(this.hidParentId.Value));
                    if (dics != null)
                    {
                        this.dicList.DataSource = dics;
                        this.dicList.DataBind();
                    }
                    else
                    {
                        YMessageBox.show(this, "获取字典数据失败！错误信息[" + dicOper.errorMessage + "]");
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
    }
}