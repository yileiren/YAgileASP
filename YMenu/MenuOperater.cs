using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;

namespace YLR.YMenu
{
    /// <summary>
    /// 菜单操作类库封装。
    /// </summary>
    public class MenuOperater
    {
        /// <summary>
        /// 菜单数据库实例。
        /// </summary>
        protected YDataBase _menuDataBase = null;

        /// <summary>
        /// 菜单数据库实例。
        /// </summary>
        public YDataBase menuDataBase
        {
            set
            {
                this._menuDataBase = value;
            }
            get
            {
                return this._menuDataBase;
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
    }
}
