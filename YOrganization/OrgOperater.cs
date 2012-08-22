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
        /// 创建组织机构数据库操作对象。
        /// 作者：董帅 创建时间：2012-8-22 13:17:48
        /// </summary>
        /// <param name="configFilePath">配置文件路径。</param>
        /// <param name="nodeName">节点名。</param>
        /// <returns>成功返回操作对象，否则返回null。</returns>
        public static OrgOperater createOrgOperater(string configFilePath, string nodeName)
        {
            OrgOperater orgOper = null;

            //获取数据库实例。
            YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFilePath, nodeName);

            if (orgDb != null)
            {
                orgOper = new OrgOperater();
                orgOper.orgDataBase = orgDb;
            }
            else
            {
                Exception ex = new Exception("创建数据库实例失败！");
                throw ex;
            }

            return orgOper;
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

                        sql = string.Format("INSERT INTO org_organization (name) VALUES ('{0}') SELECT SCOPE_IDENTITY() AS id"
                            , org.name);
                    }
                    else
                    {
                        sql = string.Format("INSERT INTO org_organization (name,parentId) VALUES ('{0}',{1}) SELECT SCOPE_IDENTITY() AS id"
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
        /// 通过指定的父id获取组织机构列表。
        /// 作者：董帅 创建时间：2012-8-22 12:57:13
        /// </summary>
        /// <param name="pId">父id，如果是顶级机构，父id为-1</param>
        /// <returns>成功返回组织机构列表，否则返回null。</returns>
        public List<OrganizationInfo> getOrganizationByParentId(int pId)
        {
            List<OrganizationInfo> orgs = null;

            try
            {
                if (this._orgDataBase != null)
                {
                    //连接数据库
                    if (this._orgDataBase.connectDataBase())
                    {

                        //sql语句，获取所有权限
                        string sql = "";
                        if (pId == -1)
                        {
                            sql = "SELECT * FROM ORG_ORGANIZATION WHERE PARENTID IS NULL";
                        }
                        else
                        {
                            sql = "SELECT * FROM ORG_ORGANIZATION WHERE PARENTID = " + pId.ToString(); ; sql = "SELECT * FROM ORG_ORGANIZATION WHERE PARENTID IS NULL ";
                        }
                        //获取数据
                        DataTable dt = this._orgDataBase.executeSqlReturnDt(sql);
                        if (dt != null)
                        {
                            //role = this.getRoleFormDataRow(dt.Rows[0]);
                        }
                        else
                        {
                            this._errorMessage = "获取数据失败！错误信息：[" + this._orgDataBase.errorText + "]";
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._orgDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._orgDataBase.disconnectDataBase();
            }

            return orgs;
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
                string sql = string.Format("SELECT TOP(1) * FROM ORG_USER WHERE LOGNAME = '{0}' AND LOGPASSWORD = '{1}'",logName,logPassword);

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

        /// <summary>
        /// 修改用户密码。
        /// </summary>
        /// <param name="user">要修改的用户信息。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changePassword(UserInfo user)
        {
            bool retVal = false;
            try
            {
                //更新语句
                string sql = string.Format("UPDATE ORG_USER SET LOGPASSWORD = '{0}' WHERE ID = {1}", user.logPassword, user.id);

                //连接数据库
                if (this._orgDataBase.connectDataBase())
                {
                    //更新数据
                    int iRet = this._orgDataBase.executeSqlWithOutDs(sql);
                    if (iRet > 0)
                    {
                        retVal = true;
                    }
                    else
                    {
                        this._errorMessage = "更新数据出错！";

                        if (iRet < 0)
                        {
                            //执行出错
                            this._errorMessage += "[" + this.orgDataBase.errorText + "]";
                        }
                    }
                }
                else
                {
                    this._errorMessage = "连接数据库出错！错误信息[" + this._orgDataBase.errorText + "]";
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

            return retVal;
        }
    }
}
