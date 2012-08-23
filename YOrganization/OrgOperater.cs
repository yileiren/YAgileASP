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

                        sql = string.Format("INSERT INTO org_organization (name,[ORDER]) VALUES ('{0}',{1}) SELECT SCOPE_IDENTITY() AS id"
                            , org.name
                            , org.order);
                    }
                    else
                    {
                        sql = string.Format("INSERT INTO org_organization (name,parentId,[ORDER]) VALUES ('{0}',{1},{2}) SELECT SCOPE_IDENTITY() AS id"
                            , org.name
                            , org.parentId
                            , org.order);
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
        /// 根据DataRow获取组织机构对象。
        /// </summary>
        /// <param name="r">组织机构数据</param>
        /// <returns>组织机构，失败返回null。</returns>
        private OrganizationInfo getOrganizationFormDataRow(DataRow r)
        {
            if (r != null)
            {
                //分组对象
                OrganizationInfo org = new OrganizationInfo();

                //菜单id不能为null，否则返回失败。
                if (!r.IsNull("ID"))
                {
                    org.id = Convert.ToInt32(r["ID"]);
                }
                else
                {
                    return null;
                }

                if (!r.IsNull("NAME"))
                {
                    org.name = r["NAME"].ToString();
                }

                if (!r.IsNull("PARENTID"))
                {
                    org.parentId = Convert.ToInt32(r["PARENTID"]);
                }
                else
                {
                    org.parentId = -1;
                }

                if (!r.IsNull("CREATETIME"))
                {
                    org.createTime = Convert.ToDateTime(r["CREATETIME"]);
                }

                if (!r.IsNull("ISDELETE"))
                {
                    if (r["ISDELETE"].ToString() == "N")
                    {
                        org.isDelete = false;
                    }
                    else
                    {
                        org.isDelete = true;
                    }
                }

                if (!r.IsNull("ORDER"))
                {
                    org.order = Convert.ToInt32(r["ORDER"]);
                }
                else
                {
                    org.order = 0;
                }

                return org;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取组织机构。
        /// 作者：董帅 创建时间：2012-8-22 22:22:47
        /// </summary>
        /// <param name="id">机构id。</param>
        /// <returns>机构，失败返回null。</returns>
        public OrganizationInfo getOrganization(int id)
        {
            OrganizationInfo orgInfo = null;

            try
            {
                if (this._orgDataBase != null)
                {
                    //连接数据库
                    if (this._orgDataBase.connectDataBase())
                    {

                        //sql语句，获取所有权限
                        string sql = "SELECT * FROM ORG_ORGANIZATION WHERE ID = " + id.ToString();
                        
                        //获取数据
                        DataTable dt = this._orgDataBase.executeSqlReturnDt(sql);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            orgInfo = this.getOrganizationFormDataRow(dt.Rows[0]);
                        }
                        else
                        {
                            if(dt != null)
                            {
                                this._errorMessage = "获取数据失败！";
                            }
                            else
                            {
                                this._errorMessage = "获取数据失败！错误信息：[" + this._orgDataBase.errorText + "]";
                            }
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

            return orgInfo;
        }

        /// <summary>
        /// 修改指定组织机构的内容，通过组织机构id匹配。
        /// </summary>
        /// <param name="org">要修改的组织机构。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changeOrganization(OrganizationInfo org)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._orgDataBase != null)
                {
                    //连接数据库
                    if (this._orgDataBase.connectDataBase())
                    {
                        //sql语句
                        string sql = "";
                        if (org.parentId == -1)
                        {
                            //顶级菜单
                            sql = string.Format("UPDATE ORG_ORGANIZATION SET NAME = '{0}',[ORDER] = {1} WHERE ID = {2}"
                                , org.name
                                , org.order
                                , org.id);
                        }
                        else
                        {
                            sql = string.Format("UPDATE ORG_ORGANIZATION SET NAME = '{0}',PARENTID = {1},[ORDER] = {2} WHERE ID = {3}"
                                , org.name
                                , org.parentId
                                , org.order
                                , org.id);
                        }

                        int retCount = this._orgDataBase.executeSqlWithOutDs(sql);
                        if (retCount == 1)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "更新数据失败！";
                            if (retCount != 1)
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

            return bRet;
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
                            sql = "SELECT * FROM ORG_ORGANIZATION WHERE PARENTID IS NULL ORDER BY [ORDER] ASC";
                        }
                        else
                        {
                            sql = "SELECT * FROM ORG_ORGANIZATION WHERE PARENTID = " + pId.ToString() + " ORDER BY [ORDER] ASC";
                        }
                        //获取数据
                        DataTable dt = this._orgDataBase.executeSqlReturnDt(sql);
                        if (dt != null)
                        {
                            orgs = new List<OrganizationInfo>();
                            foreach (DataRow r in dt.Rows)
                            {
                                OrganizationInfo o = this.getOrganizationFormDataRow(r);
                                if (o != null)
                                {
                                    orgs.Add(o);
                                }
                            }
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
        /// 创建新用户。
        /// 作者：董帅 创建时间：2012-8-23 13:23:45
        /// </summary>
        /// <param name="user">新用户。</param>
        /// <returns>成功返回用户id，失败返回-1。</returns>
        public int createNewUser(UserInfo user)
        {
            int userId = -1; //创建的组织机构id。

            try
            {
                if (user == null)
                {
                    this._errorMessage = "不能插入空用户！";
                }
                else if (string.IsNullOrEmpty(user.name) || user.name.Length > 20)
                {
                    this._errorMessage = "用户名称不合法！";
                }
                else if (string.IsNullOrEmpty(user.logName) || user.logName.Length > 20)
                {
                    this._errorMessage = "用户登陆名称不合法！";
                }
                else if (string.IsNullOrEmpty(user.logPassword) || user.logPassword.Length > 40)
                {
                    this._errorMessage = "用户登陆密码不合法！";
                }
                else
                {
                    //新增数据
                    string sql = "";
                    if (user.organizationId > 0)
                    {
                        sql = string.Format("INSERT INTO ORG_USER (LOGNAME,LOGPASSWORD,NAME,ORGANIZATIONID,[ORDER]) VALUES ('{0}','{1}','{2}',{3},{4}) SELECT SCOPE_IDENTITY() AS id"
                            , user.logName
                            , user.logPassword
                            , user.name
                            , user.organizationId
                            , user.order);
                        
                    }
                    else
                    {
                        //顶级用户
                        sql = string.Format("INSERT INTO ORG_USER (LOGNAME,LOGPASSWORD,NAME,[ORDER]) VALUES ('{0}','{1}','{2}',{3}) SELECT SCOPE_IDENTITY() AS id"
                            , user.logName
                            , user.logPassword
                            , user.name
                            , user.order);
                    }

                    //存入数据库
                    if (this._orgDataBase.connectDataBase())
                    {
                        DataTable retDt = this._orgDataBase.executeSqlReturnDt(sql);
                        if (retDt != null && retDt.Rows.Count > 0)
                        {
                            //获取组织机构id
                            userId = Convert.ToInt32(retDt.Rows[0]["id"]);
                        }
                        else
                        {
                            this._errorMessage = "创建用户失败！";
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

            return userId;
        }

        /// <summary>
        /// 获取指定id的用户。
        /// 作者：董帅 创建时间：2012-8-23 23:03:58
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>成功返回用户，否则返回null。</returns>
        public UserInfo getUser(int id)
        {
            UserInfo user = null;
            try
            {
                //构建SQL语句
                string sql = string.Format("SELECT TOP(1) * FROM ORG_USER WHERE ID = {0}", id);

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

                            //用户登录名
                            if (!dt.Rows[0].IsNull("LOGNAME"))
                            {
                                user.logName = Convert.ToString(dt.Rows[0]["LOGNAME"]);
                            }

                            //用户登录密码
                            if (!dt.Rows[0].IsNull("LOGPASSWORD"))
                            {
                                user.logPassword = Convert.ToString(dt.Rows[0]["LOGPASSWORD"]);
                            }

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

                            //序号
                            if (!dt.Rows[0].IsNull("ORDER"))
                            {
                                user.order = Convert.ToInt32(dt.Rows[0]["ORDER"]);
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
                    this._errorMessage = "连接数据库出错！错误信息[" + this._orgDataBase.errorText + "]";
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
        /// 根据DataRow获取用户对象。
        /// 作者：董帅 创建时间：2012-8-23 21:34:04
        /// </summary>
        /// <param name="r">用户数据</param>
        /// <returns>用户，失败返回null。</returns>
        private UserInfo getUserFormDataRow(DataRow r)
        {
            if (r != null)
            {
                //分组对象
                UserInfo user = new UserInfo();

                //菜单id不能为null，否则返回失败。
                if (!r.IsNull("ID"))
                {
                    user.id = Convert.ToInt32(r["ID"]);
                }
                else
                {
                    return null;
                }

                if (!r.IsNull("LOGNAME"))
                {
                    user.logName = r["LOGNAME"].ToString();
                }

                if (!r.IsNull("LOGPASSWORD"))
                {
                    user.logPassword = r["LOGPASSWORD"].ToString();
                }

                if (!r.IsNull("NAME"))
                {
                    user.name = r["NAME"].ToString();
                }

                if (!r.IsNull("ORGANIZATIONID"))
                {
                    user.organizationId = Convert.ToInt32(r["ORGANIZATIONID"]);
                }
                else
                {
                    user.organizationId = -1;
                }

                if (!r.IsNull("ISDELETE"))
                {
                    if (r["ISDELETE"].ToString() == "N")
                    {
                        user.isDelete = false;
                    }
                    else
                    {
                        user.isDelete = true;
                    }
                }

                if (!r.IsNull("ORDER"))
                {
                    user.order = Convert.ToInt32(r["ORDER"]);
                }
                else
                {
                    user.order = 0;
                }

                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过指定的组织机构id获取该机构下的所有用户。
        /// 作者：董帅 创建时间：2012-8-23 21:27:07
        /// </summary>
        /// <param name="orgId">机构id</param>
        /// <returns>用户列表，失败返回null。</returns>
        public List<UserInfo> getUserByOrganizationId(int orgId)
        {
            List<UserInfo> users = null;

            try
            {
                if (this._orgDataBase != null)
                {
                    //连接数据库
                    if (this._orgDataBase.connectDataBase())
                    {

                        //sql语句
                        string sql = "";
                        if (orgId == -1)
                        {
                            sql = "SELECT * FROM ORG_USER WHERE ORGANIZATIONID IS NULL ORDER BY [ORDER] ASC";
                        }
                        else
                        {
                            sql = "SELECT * FROM ORG_USER WHERE ORGANIZATIONID = " + orgId.ToString() + " ORDER BY [ORDER] ASC";
                        }
                        //获取数据
                        DataTable dt = this._orgDataBase.executeSqlReturnDt(sql);
                        if (dt != null)
                        {
                            users = new List<UserInfo>();
                            foreach (DataRow r in dt.Rows)
                            {
                                UserInfo u = this.getUserFormDataRow(r);
                                if (u != null)
                                {
                                    users.Add(u);
                                }
                            }
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

            return users;
        }

        /// <summary>
        /// 修改指定用户的内容，通过用户id匹配。
        /// </summary>
        /// <param name="user">要修改的用户。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changeUser(UserInfo user)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._orgDataBase != null)
                {
                    //连接数据库
                    if (this._orgDataBase.connectDataBase())
                    {
                        //sql语句
                        string sql = "";
                        if (user.organizationId == -1)
                        {
                            //顶级菜单
                            sql = string.Format("UPDATE ORG_USER SET LOGNAME = '{0}', NAME = '{1}',[ORDER] = {2} WHERE ID = {3}"
                                , user.logName
                                , user.name
                                , user.order
                                , user.id);
                        }
                        else
                        {
                            sql = string.Format("UPDATE ORG_USER SET LOGNAME = '{0}',NAME = '{1}',ORGANIZATIONID = {2},[ORDER] = {3} WHERE ID = {4}"
                                , user.logName
                                , user.name
                                , user.organizationId
                                , user.order
                                , user.id);
                        }

                        int retCount = this._orgDataBase.executeSqlWithOutDs(sql);
                        if (retCount == 1)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "更新数据失败！";
                            if (retCount != 1)
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

            return bRet;
        }

        /// <summary>
        /// 判断用户是否存在。
        /// </summary>
        /// <param name="logName">登陆名</param>
        /// <returns>存在返回true，否则返回false。</returns>
        public bool existUser(string logName)
        {
            bool bRet = false;
            try
            {
                //构建SQL语句
                string sql = string.Format("SELECT TOP(1) * FROM ORG_USER WHERE LOGNAME = '{0}'", logName);

                //连接数据库
                if (this._orgDataBase.connectDataBase())
                {
                    //获取用户
                    DataTable dt = this._orgDataBase.executeSqlReturnDt(sql);

                    //构建用户
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        bRet = true;
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
                    this._errorMessage = "连接数据库出错！错误信息[" + this._orgDataBase.errorText + "]";
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

            return bRet;
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
