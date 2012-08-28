using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;
using System.Data;

namespace YLR.YDataDictionary
{
    /// <summary>
    /// 数据字典业务类封装。
    /// 作者：董帅 创建时间：2012-8-28 17:41:18
    /// </summary>
    public class DataDicOperater
    {
        /// <summary>
        /// 数据字典数据库。
        /// </summary>
        protected YDataBase _dicDataBase = null;

        /// <summary>
        /// 数据字典数据库。
        /// </summary>
        public YDataBase orgDataBase
        {
            get
            {
                return this._dicDataBase;
            }
            set
            {
                this._dicDataBase = value;
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
        /// 创建数据字典数据库操作对象。
        /// 作者：董帅 创建时间：2012-8-28 17:43:19
        /// </summary>
        /// <param name="configFilePath">配置文件路径。</param>
        /// <param name="nodeName">节点名。</param>
        /// <returns>成功返回操作对象，否则返回null。</returns>
        public static DataDicOperater createDataDicOperater(string configFilePath, string nodeName)
        {
            DataDicOperater dicOper = null;

            //获取数据库实例。
            YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFilePath, nodeName);

            if (orgDb != null)
            {
                dicOper = new DataDicOperater();
                dicOper.orgDataBase = orgDb;
            }
            else
            {
                Exception ex = new Exception("创建数据库实例失败！");
                throw ex;
            }

            return dicOper;
        }

        /// <summary>
        /// 创建新字典项。
        /// </summary>
        /// <param name="dataDic">新字典项，新字典项名称不能为""，切长度不能大于50。</param>
        /// <returns>成功返回组织机构id，失败返回-1。</returns>
        public int createNewDataDictionary(DataDictionaryInfo dataDic)
        {
            int dataDicId = -1; //创建的组织机构id。

            try
            {
                if (dataDic == null)
                {
                    this._errorMessage = "不能插入空字典项！";
                }
                else if (string.IsNullOrEmpty(dataDic.name) || dataDic.name.Length > 50)
                {
                    this._errorMessage = "字典项名称不合法！";
                }
                else
                {
                    //存入数据库
                    if (this._dicDataBase.connectDataBase())
                    {
                        //新增数据
                        string sql = "";
                        if (dataDic.parentId == -1)
                        {

                            sql = string.Format("INSERT INTO SYS_DATADICTIONARY (NAME,VALUE,CODE,[ORDER]) VALUES ('{0}',{1},'{2}',{3}) SELECT SCOPE_IDENTITY() AS id"
                                , dataDic.name
                                , dataDic.value
                                , dataDic.code
                                , dataDic.order);
                        }
                        else
                        {
                            sql = string.Format("INSERT INTO SYS_DATADICTIONARY (NAME,PARENTID,VALUE,CODE,[ORDER]) VALUES ('{0}',{1},{2},'{3}',{4}) SELECT SCOPE_IDENTITY() AS id"
                                , dataDic.name
                                , dataDic.parentId
                                , dataDic.value
                                , dataDic.code
                                , dataDic.order);
                        }

                        DataTable retDt = this._dicDataBase.executeSqlReturnDt(sql);
                        if (retDt != null && retDt.Rows.Count > 0)
                        {
                            //获取组织机构id
                            dataDicId = Convert.ToInt32(retDt.Rows[0]["id"]);
                        }
                        else
                        {
                            this._errorMessage = "创建字典项失败！";
                            if (retDt == null)
                            {
                                this._errorMessage += "错误信息[" + this._dicDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库出错！错误信息[" + this._dicDataBase.errorText + "]";
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
                this._dicDataBase.disconnectDataBase();
            }

            return dataDicId;
        }
    }
}
