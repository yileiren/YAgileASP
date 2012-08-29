using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLR.YSystem.DataDictionary
{
    /// <summary>
    /// 数据字典实体类。
    /// 作者：董帅 创建时间：2012-8-28 17:33:34
    /// </summary>
    public class DataDictionaryInfo
    {
        /// <summary>
        /// id
        /// </summary>
        protected int _id = 0;

        /// <summary>
        /// id
        /// </summary>
        public int id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// 父id，为-1表示顶级字典。
        /// </summary>
        protected int _paretnId = -1;

        /// <summary>
        /// 父id，为-1表示顶级字典。
        /// </summary>
        public int parentId
        {
            get { return this._paretnId; }
            set { this._paretnId = value; }
        }

        /// <summary>
        /// 名称。
        /// </summary>
        protected string _name = "";

        /// <summary>
        /// 名称。
        /// </summary>
        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// 字典对应的数值，默认是0。
        /// </summary>
        protected int _value = 0;

        /// <summary>
        /// 字典对应的数值，默认是0。
        /// </summary>
        public int value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        /// <summary>
        /// 字典对应的代码。
        /// </summary>
        protected string _code = "";

        /// <summary>
        /// 字典对应的代码。
        /// </summary>
        public string code
        {
            get { return this._code; }
            set { this._code = value; }
        }

        /// <summary>
        /// 字典的排序序号。
        /// </summary>
        protected int _order = 0;

        /// <summary>
        /// 字典的排序序号。
        /// </summary>
        public int order
        {
            get { return this._order; }
            set { this._order = value; }
        }
    }
}
