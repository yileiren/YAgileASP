using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YSystem.Role
{
    /// <summary>
    /// 用户角色信息，记录用户被选则的角色。
    /// 作者：董帅 创建时间：2012-8-27 22:27:52
    /// </summary>
    public class UserRoleInfo : RoleInfo
    {
        /// <summary>
        /// 是否选择。
        /// </summary>
        protected bool _choused = false;

        /// <summary>
        /// 是否选择。
        /// </summary>
        public bool choused
        {
            get { return this._choused; }
            set { this._choused = value; }
        }
    }
}
