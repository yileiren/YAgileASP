using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YSystem.Menu
{
    /// <summary>
    /// 菜单关联页面信息实体类。
    /// 作者：董帅 创建时间：2013-5-4 21:38:07
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 页面信息id
        /// </summary>
        protected int _id = -1;

        /// <summary>
        /// 页面信息id
        /// </summary>
        public int id
        {
            set { this._id = value; }
            get { return this._id; }
        }

        /// <summary>
        /// 文件路径。
        /// </summary>
        protected string _filePath = "";

        /// <summary>
        /// 文件路径。
        /// </summary>
        public string filePath
        {
            set { this._filePath = value; }
            get { return this._filePath; }
        }

        /// <summary>
        /// 详细信息。
        /// </summary>
        protected string _detail = "";

        /// <summary>
        /// 详细信息。
        /// </summary>
        public string detail
        {
            get { return this._detail; }
            set { this._detail = value; }
        }

        /// <summary>
        /// 所属菜单id。
        /// </summary>
        protected int _menuId = -1;

        /// <summary>
        /// 所属菜单id。
        /// </summary>
        public int menuId
        {
            get { return this._menuId; }
            set { this._menuId = value; }
        }
    }
}
