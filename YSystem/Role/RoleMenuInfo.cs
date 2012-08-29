using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YSystem.Menu;

namespace YLR.YSystem.Role
{
    /// <summary>
    /// 权限菜单，继承菜单属性，扩展权限特有属性。
    /// 作者：董帅 创建时间：2012-8-20 23:06:50
    /// </summary>
    public class RoleMenuInfo : MenuInfo
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

        /// <summary>
        /// 子菜单。
        /// </summary>
        protected new List<RoleMenuInfo> _childMenus = new List<RoleMenuInfo>();

        /// <summary>
        /// 子菜单。
        /// </summary>
        public new List<RoleMenuInfo> childMenus
        {
            set { this._childMenus = value; }
            get { return this._childMenus; }
        }
    }
}
