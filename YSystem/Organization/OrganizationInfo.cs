using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YSystem.Organization
{
    /// <summary>
    /// 组织机构实体类。
    /// </summary>
    public class OrganizationInfo
    {
        /// <summary>
        /// 组织机构id，-1表示错误机构。
        /// </summary>
        protected int _id = -1;

        /// <summary>
        /// 组织机构id。
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
        /// 组织机构名称。
        /// </summary>
        protected string _name = "";

        /// <summary>
        /// 组织机构名称。
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
        /// 组织机构父id，-1表示顶级机构。
        /// </summary>
        protected int _parentId = -1;

        /// <summary>
        /// 组织机构父id，-1表示顶级机构。
        /// </summary>
        public int parentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        /// <summary>
        /// 创建时间，默认值是当前值。
        /// </summary>
        protected DateTime _createTime = DateTime.Now;

        /// <summary>
        /// 创建时间，默认值是当前值。
        /// </summary>
        public DateTime createTime
        {
            get
            {
                return this._createTime;
            }
            set
            {
                this._createTime = value;
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

        /// <summary>
        /// 序号。
        /// </summary>
        protected int _order = 0;

        /// <summary>
        /// 序号。
        /// </summary>
        public int order
        {
            get { return this._order; }
            set { this._order = value; }
        }
    }
}
