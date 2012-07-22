using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;
using System.Data;

namespace YLR.YOrganization
{
    /// <summary>
    /// 组织机构操作操作类。
    /// </summary>
    public class OrgOperater
    {
        /// <summary>
        /// 组织机构数据库。
        /// </summary>
        protected YDataBase _orgDataBase = null;

        /// <summary>
        /// 组织机构数据库。
        /// </summary>
        public YDataBase orgDataBase
        {
            get
            {
                return this._orgDataBase;
            }
            set
            {
                this._orgDataBase = value;
            }
        }

        /// <summary>
        /// 错误信息。
        /// </summary>
        protected string _errorMessage = "";

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string errorMessage
        {
            get
            {
                return this._errorMessage;
            }
        }

        /// <summary>
        /// 创建新组织机构。
        /// </summary>
        /// <param name="org">新组织机构，组织机构名称不能为""，切长度不能大于50。</param>
        /// <returns>成功返回组织机构id，失败返回-1。</returns>
        public int createNewOrganization(OrganizationInfo org)
        {
            int orgId = -1; //创建的组织机构id。

            try
            {
                if (org == null)
                {
                    //不能插入空组织机构
                    this._errorMessage = "不能插入空组织机构！";
                }
                else if (string.IsNullOrEmpty(org.name) || org.name.Length > 50)
                {
                    //组织机构名称不合法。
                    this._errorMessage = "组织机构名称不合法！";
                }
                else
                {
                    //新增数据
                    string sql = "";
                    if (org.parentId == -1)
                    {

                        sql = string.Format("INSERT INTO org_organization (name) VALUES (%s) SELECT SCOPE_IDENTITY() AS id"
                            , org.name);
                    }
                    else
                    {
                        sql = string.Format("INSERT INTO org_organization (name,parentId) VALUES (%s,%d) SELECT SCOPE_IDENTITY() AS id"
                            , org.name
                            , org.parentId);
                    }

                    //存入数据库
                    this._orgDataBase.connectDataBase();
                    DataTable retDt = this._orgDataBase.executeSqlReturnDt(sql);
                    if (retDt != null && retDt.Rows.Count > 0)
                    {
                        //获取组织机构id
                        orgId = Convert.ToInt32(retDt.Rows[0]["id"]);
                    }
                    else
                    {
                        this._errorMessage = "创建组织机构失败！";
                        if (retDt == null)
                        {
                            this._errorMessage += "错误信息[" + this._orgDataBase.errorText + "]";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //断开数据库连接。
                this._orgDataBase.disconnectDataBase();
            }

            return orgId;
        }
    }
}
