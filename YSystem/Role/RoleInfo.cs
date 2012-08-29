using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YSystem.Role
{
    /// <summary>
    /// 角色信息类。
    /// 作者：董帅 创建时间：2012-8-15 22:54:38
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// 角色id。
        /// </summary>
        protected int _id = -1;

        /// <summary>
        /// 角色id。
        /// </summary>
        public int id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// 角色名称。
        /// </summary>
        protected string _name = "";

        /// <summary>
        /// 角色名称。
        /// </summary>
        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// 说明。
        /// </summary>
        protected string _explain = "";

        /// <summary>
        /// 说明。
        /// </summary>
        public string explain
        {
            get { return this._explain; }
            set { this._explain = value; }
        }
    }
}
