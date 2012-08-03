using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YMenu
{
    /// <summary>
    /// 菜单实体类。
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 菜单id。
        /// </summary>
        protected int _id = -1;

        /// <summary>
        /// 菜单id。
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
        /// 菜单名称。
        /// </summary>
        protected string _name = "";

        /// <summary>
        /// 菜单名称。
        /// </summary>
        public string name
        {
            set
            {
                this._name = value;
            }
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// 菜单关联的url。
        /// </summary>
        protected string _url = "";

        /// <summary>
        /// 菜单关联的url。
        /// </summary>
        public string url
        {
            set
            {
                this._url = value;
            }
            get
            {
                return this._url;
            }
        }

        /// <summary>
        /// 父菜单id，为-1标识顶级菜单。
        /// </summary>
        protected int _parentID = -1;

        /// <summary>
        /// 父菜单id，为-1标识顶级菜单。
        /// </summary>
        public int parentID
        {
            set
            {
                this._parentID = value;
            }
            get
            {
                return this._parentID;
            }
        }

        /// <summary>
        /// 菜单图片。
        /// </summary>
        protected string _icon = "";

        /// <summary>
        /// 菜单图片。
        /// </summary>
        public string icon
        {
            set
            {
                this._icon = value;
            }
            get
            {
                return this._icon;
            }
        }

        /// <summary>
        /// 桌面图标。
        /// </summary>
        protected string _desktopIcon = "";


        /// <summary>
        /// 桌面图标。
        /// </summary>
        public string desktopIcon
        {
            set
            {
                this._desktopIcon = value;
            }
            get
            {
                return this._desktopIcon;
            }
        }

        /// <summary>
        /// 排序序号。
        /// </summary>
        protected int _order = 0;

        /// <summary>
        /// 排序序号。
        /// </summary>
        public int order
        {
            set
            {
                this._order = value;
            }
            get
            {
                return this._order;
            }
        }

        /// <summary>
        /// 子菜单。
        /// </summary>
        protected List<MenuInfo> _childMenus = new List<MenuInfo>();

        /// <summary>
        /// 子菜单。
        /// </summary>
        public List<MenuInfo> childMenus
        {
            get
            {
                return this._childMenus;
            }
            set
            {
                this._childMenus = value;
            }
        }
    }
}
