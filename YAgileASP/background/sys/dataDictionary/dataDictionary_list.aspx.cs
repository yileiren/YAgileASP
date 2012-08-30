using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YLR.YMessage;
using YLR.YSystem.DataDictionary;

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

        /// <summary>
        /// 删除数据
        /// 作者：董帅 创建时间：2012-8-28 22:17:10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butDeleteItems_Click(object sender, EventArgs e)
        {
            try
            {
                string s = Request["chkDic"];
                string[] dicIds = new string[0];
                if (!string.IsNullOrEmpty(s))
                {
                    dicIds = s.Split(','); //要删除的字典id
                }

                if (dicIds.Length > 0)
                {
                    //获取配置文件路径。
                    string configFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataBaseConfig.xml";

                    //创建数据库操作对象。
                    DataDicOperater dicOper = DataDicOperater.createDataDicOperater(configFile, "SQLServer");
                    if (dicOper != null)
                    {

                        //删除字典项
                        int[] dicIntIds = new int[dicIds.Length];
                        for (int i = 0; i < dicIds.Length; i++)
                        {
                            dicIntIds[i] = Convert.ToInt32(dicIds[i]);
                        }

                        if (dicOper.deleteDataDictionarys(dicIntIds))
                        {
                            this.Response.Redirect("dataDictionary_list.aspx?parentId=" + this.hidParentId.Value);
                            //YMessageBox.showAndResponseScript(this, "删除数据成功！", "", "window.location.href='dataDictionary_list.aspx?parentId=" + this.hidParentId.Value + "'");
                        }
                        else
                        {
                            YMessageBox.show(this, "删除数据失败！错误信息[" + dicOper.errorMessage + "]");
                        }
                    }
                    else
                    {
                        YMessageBox.show(this, "获取数据库实例失败！");
                    }
                }
                else
                {
                    YMessageBox.show(this, "没有选择要删除的字典！");
                }
            }
            catch (Exception ex)
            {
                YMessageBox.show(this, "系统运行异常！异常信息[" + ex.Message + "]");
            }
        }
    }
}