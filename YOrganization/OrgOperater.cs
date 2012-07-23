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
                        sql = string.Format("INSERT INTO org_organization (name,parentId) VALUES ('%s',%d) SELECT SCOPE_IDENTITY() AS id"
                            , org.name
                            , org.parentId);
                    }

                    //存入数据库
                    if (this._orgDataBase.connectDataBase())
                    {
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
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._orgDataBase.errorText + "]";
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

        /// <summary>
        /// 根据登录名和密码获取用户信息，如果存在多个用户则返回id小的。
        /// </summary>
        /// <param name="logName">登陆名</param>
        /// <param name="logPassword">密码</param>
        /// <returns>成功返回用户，否则返回null。</returns>
        public UserInfo getUser(string logName, string logPassword)
        {
            UserInfo user = null;
            try
            {
                //构建SQL语句
                string sql = string.Format("SELECT TOP(1) * FROM ORG_USER WHILE LOGNAME = '%s' AND LOGPASSWORD = '%s'",logName,logPassword);

                //连接数据库
                if (this._orgDataBase.connectDataBase())
                {
                    //获取用户
                    DataTable dt = this._orgDataBase.executeSqlReturnDt(sql);

                    //构建用户
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        user = new UserInfo();
                        //获取用户id
                        if (!dt.Rows[0].IsNull("ID"))
                        {
                            user.id = Convert.ToInt32(dt.Rows[0]["ID"]);

                            //获取用户登录名和密码
                            user.logName = logName;
                            user.logPassword = logPassword;

                            //用户姓名
                            if (!dt.Rows[0].IsNull("NAME"))
                            {
                                user.name = Convert.ToString(dt.Rows[0]["NAME"]);
                            }

                            //用户所属组织机构
                            if (!dt.Rows[0].IsNull("ORGANIZATIONID"))
                            {
                                user.organizationId = Convert.ToInt32(dt.Rows[0]["ORGANIZATIONID"]);
                            }

                            //是否删除
                            if (!dt.Rows[0].IsNull("ISDELETE"))
                            {
                                if ("Y" == dt.Rows[0]["ISDELETE"].ToString())
                                {
                                    user.isDelete = true;
                                }
                                else
                                {
                                    user.isDelete = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "获取用户信息出错！";

                        if (dt == null)
                        {
                            this._errorMessage += "错误信息[" + this._orgDataBase.errorText + "]";
                        }
                    }
                }
                else
                {
                    this._errorMessage = "连接数据库出错！错误信息[" + this._orgDataBase.errorText +"]";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this._orgDataBase.disconnectDataBase();
            }

            return user;
        }
    }
}
