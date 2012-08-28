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

        /// <summary>
        /// 根据DataRow获取数据字典项对象。
        /// </summary>
        /// <param name="r">字典项数据</param>
        /// <returns>字典项，失败返回null。</returns>
        private DataDictionaryInfo getDataDictionaryFormDataRow(DataRow r)
        {
            if (r != null)
            {
                //分组对象
                DataDictionaryInfo dic = new DataDictionaryInfo();

                //菜单id不能为null，否则返回失败。
                if (!r.IsNull("ID"))
                {
                    dic.id = Convert.ToInt32(r["ID"]);
                }
                else
                {
                    return null;
                }

                if (!r.IsNull("NAME"))
                {
                    dic.name = r["NAME"].ToString();
                }

                if (!r.IsNull("PARENTID"))
                {
                    dic.parentId = Convert.ToInt32(r["PARENTID"]);
                }
                else
                {
                    dic.parentId = -1;
                }

                if (!r.IsNull("VALUE"))
                {
                    dic.value = Convert.ToInt32(r["VALUE"]);
                }

                if (!r.IsNull("CODE"))
                {
                    dic.code = r["CODE"].ToString();
                }

                if (!r.IsNull("ORDER"))
                {
                    dic.order = Convert.ToInt32(r["ORDER"]);
                }
                else
                {
                    dic.order = 0;
                }

                return dic;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据字典id，获取字典项。
        /// 作者：董帅 创建时间：2012-8-28 21:39:05
        /// </summary>
        /// <param name="id">字典id。</param>
        /// <returns>字典，失败返回null。</returns>
        public DataDictionaryInfo getDataDictionary(int id)
        {
            DataDictionaryInfo dicInfo = null;

            try
            {
                if (this._dicDataBase != null)
                {
                    //连接数据库
                    if (this._dicDataBase.connectDataBase())
                    {

                        //sql语句，获取字典项
                        string sql = "SELECT * FROM SYS_DATADICTIONARY WHERE ID = " + id.ToString();

                        //获取数据
                        DataTable dt = this._dicDataBase.executeSqlReturnDt(sql);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            dicInfo = this.getDataDictionaryFormDataRow(dt.Rows[0]);
                        }
                        else
                        {
                            if (dt != null)
                            {
                                this._errorMessage = "获取数据失败！";
                            }
                            else
                            {
                                this._errorMessage = "获取数据失败！错误信息：[" + this._dicDataBase.errorText + "]";
                            }
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._dicDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._dicDataBase.disconnectDataBase();
            }

            return dicInfo;
        }

        /// <summary>
        /// 通过指定的父id获取字典项列表。
        /// 作者：董帅 创建时间：2012-8-28 21:27:19
        /// </summary>
        /// <param name="pId">父id，如果是顶级字典，父id为-1</param>
        /// <returns>成功返回字典项列表，否则返回null。</returns>
        public List<DataDictionaryInfo> getDataDictionaryByParentId(int pId)
        {
            List<DataDictionaryInfo> dics = null;

            try
            {
                if (this._dicDataBase != null)
                {
                    //连接数据库
                    if (this._dicDataBase.connectDataBase())
                    {

                        //sql语句，获取所有字典
                        string sql = "";
                        if (pId == -1)
                        {
                            sql = "SELECT * FROM SYS_DATADICTIONARY WHERE PARENTID IS NULL ORDER BY [ORDER] ASC";
                        }
                        else
                        {
                            sql = "SELECT * FROM SYS_DATADICTIONARY WHERE PARENTID = " + pId.ToString() + " ORDER BY [ORDER] ASC";
                        }
                        //获取数据
                        DataTable dt = this._dicDataBase.executeSqlReturnDt(sql);
                        if (dt != null)
                        {
                            dics = new List<DataDictionaryInfo>();
                            foreach (DataRow r in dt.Rows)
                            {
                                DataDictionaryInfo d = this.getDataDictionaryFormDataRow(r);
                                if (d != null)
                                {
                                    dics.Add(d);
                                }
                            }
                        }
                        else
                        {
                            this._errorMessage = "获取数据失败！错误信息：[" + this._dicDataBase.errorText + "]";
                        }
                    }
                    else
                    {
                        this._errorMessage = "连接数据库失败！错误信息：[" + this._dicDataBase.errorText + "]";
                    }
                }
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._dicDataBase.disconnectDataBase();
            }

            return dics;
        }

        /// <summary>
        /// 修改指定字典项内容，通过字典项id匹配。
        /// </summary>
        /// <param name="dic">要修改的字典项。</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool changeDataDictionary(DataDictionaryInfo dic)
        {
            bool bRet = false; //返回值

            try
            {
                if (this._dicDataBase != null)
                {
                    //连接数据库
                    if (this._dicDataBase.connectDataBase())
                    {
                        //sql语句
                        string sql = "";
                        if (dic.parentId == -1)
                        {
                            //顶级菜单
                            sql = string.Format("UPDATE SYS_DATADICTIONARY SET NAME = '{0}',VALUE = {1},CODE = '{2}',[ORDER] = {3} WHERE ID = {4}"
                                , dic.name
                                , dic.value
                                , dic.code
                                , dic.order
                                , dic.id);
                        }
                        else
                        {
                            sql = string.Format("UPDATE SYS_DATADICTIONARY SET NAME = '{0}',PARENTID = {1},VALUE = {2},CODE = '{3}',[ORDER] = {4} WHERE ID = {5}"
                                , dic.name
                                , dic.parentId
                                , dic.value
                                , dic.code
                                , dic.order
                                , dic.id);
                        }

                        int retCount = this._dicDataBase.executeSqlWithOutDs(sql);
                        if (retCount == 1)
                        {
                            bRet = true;
                        }
                        else
                        {
                            this._errorMessage = "更新数据失败！";
                            if (retCount != 1)
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
                else
                {
                    this._errorMessage = "未设置数据库实例！";
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            finally
            {
                this._dicDataBase.disconnectDataBase();
            }

            return bRet;
        }
    }
}
