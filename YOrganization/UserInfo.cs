using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YOrganization
{
    /// <summary>
    /// 组织机构用户实体类。
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户id。
        /// </summary>
        protected int _id = -1;

        /// <summary>
        /// 用户id。
        /// </summary>
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// 登陆名。
        /// </summary>
        protected string _logName = "";

        /// <summary>
        /// 登陆名。
        /// </summary>
        public string logName
        {
            get
            {
                return this._logName;
            }
            set
            {
                this._logName = value;
            }
        }

        /// <summary>
        /// 用户登陆密码。
        /// </summary>
        protected string _logPassword = "";

        /// <summary>
        /// 用户登陆密码。
        /// </summary>
        public string logPassword
        {
            get
            {
                return this._logPassword;
            }
            set
            {
                this._logPassword = value;
            }
        }

        /// <summary>
        /// 用户姓名。
        /// </summary>
        protected string _name = "";

        /// <summary>
        /// 用户姓名。
        /// </summary>
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// 用户所属机构id。
        /// </summary>
        protected int _organizationId = -1;

        /// <summary>
        /// 用户所属机构id。
        /// </summary>
        public int organizationId
        {
            get
            {
                return this._organizationId;
            }
            set
            {
                this._organizationId = value;
            }
        }

        /// <summary>
        /// 是否删除。
        /// </summary>
        protected bool _isDelete = false;

        /// <summary>
        /// 是否删除。
        /// </summary>
        public bool isDelete
        {
            get
            {
                return this._isDelete;
            }
            set
            {
                this._isDelete = value;
            }
        }
    }
}
