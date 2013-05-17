using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLR.YAdoNet;
using System.Data;

namespace YLR.YSystem.DataDictionary
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
        /// <param name="key">解密密码。</param>
        /// <returns>成功返回操作对象，否则返回null。</returns>
        public static DataDicOperater createDataDicOperater(string configFilePath, string nodeName,string key)
        {
            DataDicOperater dicOper = null;

            //获取数据库实例。
            YDataBase orgDb = YDataBaseConfigFile.createDataBase(configFilePath, nodeName, key);

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
                        YParameters par = new YParameters();
                        par.add("@dicName", dataDic.name);
                        par.add("@dicValue", dataDic.value);
                        par.add("@dicCode", dataDic.code);
                        par.add("@dicOrder", dataDic.order);
                        par.add("@dicParentId", dataDic.parentId);
                        if (dataDic.parentId == -1)
                        {
                            switch (this._dicDataBase.databaseType)
                            {
                                case DataBaseType.MSSQL:
                                case DataBaseType.SQL2000:
                                case DataBaseType.SQL2005:
                                case DataBaseType.SQL2008:
                                    {
                                        sql = "INSERT INTO SYS_DATADICTIONARY (NAME,VALUE,CODE,[ORDER]) VALUES (@dicName,@dicValue,@dicCode,@dicOrder) SELECT SCOPE_IDENTITY() AS id";
                                        break;
                                    }
                                case DataBaseType.SQLite:
                                    {
                                        sql = "INSERT INTO SYS_DATADICTIONARY (NAME,VALUE,CODE,[ORDER]) VALUES (@dicName,@dicValue,@dicCode,@dicOrder);SELECT LAST_INSERT_ROWID() AS id;";
                                        break;
                                    }
                                default:
                                    {
                                        sql = "INSERT INTO SYS_DATADICTIONARY (NAME,VALUE,CODE,[ORDER]) VALUES (@dicName,@dicValue,@dicCode,@dicOrder) SELECT SCOPE_IDENTITY() AS id";
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            switch (this._dicDataBase.databaseType)
                            {
                                case DataBaseType.MSSQL:
                                case DataBaseType.SQL2000:
                                case DataBaseType.SQL2005:
                                case DataBaseType.SQL2008:
                                    {
                                        sql = "INSERT INTO SYS_DATADICTIONARY (NAME,PARENTID,VALUE,CODE,[ORDER]) VALUES (@dicName,@dicParentId,@dicValue,@dicCode,@dicOrder) SELECT SCOPE_IDENTITY() AS id";
                                        break;
                                    }
                                case DataBaseType.SQLite:
                                    {
                                        sql = "INSERT INTO SYS_DATADICTIONARY (NAME,PARENTID,VALUE,CODE,[ORDER]) VALUES (@dicName,@dicParentId,@dicValue,@dicCode,@dicOrder);SELECT LAST_INSERT_ROWID() AS id;";
                                        break;
                                    }
                                default:
                                    {
                                        sql = "INSERT INTO SYS_DATADICTIONARY (NAME,PARENTID,VALUE,CODE,[ORDER]) VALUES (@dicName,@dicParentId,@dicValue,@dicCode,@dicOrder) SELECT SCOPE_IDENTITY() AS id";
                                        break;
                                    }
                            }
                        }

                        DataTable retDt = this._dicDataBase.executeSqlReturnDt(sql,par);
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
                        YParameters par = new YParameters();
                        par.add("@id", id);
                        string sql = "SELECT * FROM SYS_DATADICTIONARY WHERE ID = @id";

                        //获取数据
                        DataTable dt = this._dicDataBase.executeSqlReturnDt(sql,par);
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
                        YParameters par = new YParameters();
                        par.add("@parentId", pId);
                        if (pId == -1)
                        {
                            sql = "SELECT * FROM SYS_DATADICTIONARY WHERE PARENTID IS NULL ORDER BY [ORDER] ASC";
                        }
                        else
                        {
                            sql = "SELECT * FROM SYS_DATADICTIONARY WHERE PARENTID = @parentId ORDER BY [ORDER] ASC";
                        }
                        //获取数据
                        DataTable dt = this._dicDataBase.executeSqlReturnDt(sql,par);
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
                        YParameters par = new YParameters();
                        par.add("@dicId", dic.id);
                        par.add("@dicName", dic.name);
                        par.add("@dicValue", dic.value);
                        par.add("@dicCode", dic.code);
                        par.add("@dicOrder", dic.order);
                        par.add("@dicParentId", dic.parentId);
                        if (dic.parentId == -1)
                        {
                            //顶级菜单
                            sql = "UPDATE SYS_DATADICTIONARY SET NAME = @dicName,VALUE = @dicValue,CODE = @dicCode,[ORDER] = @dicOrder WHERE ID = @dicId";
                        }
                        else
                        {
                            sql = "UPDATE SYS_DATADICTIONARY SET NAME = @dicName,PARENTID = @dicParentId,VALUE = @dicValue,CODE = @dicCode,[ORDER] = @dicOrder WHERE ID = @dicId";
                        }

                        int retCount = this._dicDataBase.executeSqlWithOutDs(sql,par);
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

        /// <summary>
        /// 删除指定的字典项，删除时，连同子字典项一并删除。
        /// 作者：董帅 创建时间：2012-8-28 22:20:05
        /// </summary>
        /// <param name="dicIds">字典项id</param>
        /// <returns>成功返回true，否则返回false。</returns>
        public bool deleteDataDictionarys(int[] dicIds)
        {
            bool bRet = true;
            try
            {
                //连接数据库
                if (this._dicDataBase.connectDataBase())
                {
                    this._dicDataBase.beginTransaction(); //开启事务

                    //删除字典项
                    foreach (int i in dicIds)
                    {
                        if (this.deleteChildDataDictionarys(i))
                        {
                            //删除当前机构
                            string sql = "DELETE FROM SYS_DATADICTIONARY WHERE ID = @id";
                            YParameters par = new YParameters();
                            par.add("@id",i);
                            if (this._dicDataBase.executeSqlWithOutDs(sql,par) != 1)
                            {
                                bRet = false;
                                break;
                            }
                        }
                        else
                        {
                            bRet = false;
                            break;
                        }
                    }
                }
                else
                {
                    this._errorMessage = "连接数据库出错！错误信息[" + this._dicDataBase.errorText + "]";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bRet)
                {
                    //提交
                    this._dicDataBase.commitTransaction();
                }
                else
                {
                    //回滚
                    this._dicDataBase.rollbackTransaction();
                }
                this._dicDataBase.disconnectDataBase();
            }

            return bRet;
        }

        /// <summary>
        /// 删除指定字典项。调用前需要连接数据库，调用后关闭。
        /// 作者：董帅 创建时间：2012-8-28 22:21:50
        /// </summary>
        /// <param name="dicId">字典项id</param>
        /// <returns>成功返回true，否则返回false。</returns>
        private bool deleteChildDataDictionarys(int dicId)
        {
            bool bRet = true;

            try
            {
                //获取下字典项
                List<DataDictionaryInfo> cDics = null;

                //sql语句，获取所有字典项
                string sql = "";
                YParameters par = new YParameters();
                par.add("@parentId",dicId);
                if (dicId == -1)
                {
                    sql = "SELECT * FROM SYS_DATADICTIONARY WHERE PARENTID IS NULL ORDER BY [ORDER] ASC";
                }
                else
                {
                    sql = "SELECT * FROM SYS_DATADICTIONARY WHERE PARENTID = @parentId ORDER BY [ORDER] ASC";
                }
                //获取数据
                DataTable dt = this._dicDataBase.executeSqlReturnDt(sql,par);
                if (dt != null)
                {
                    cDics = new List<DataDictionaryInfo>();
                    foreach (DataRow r in dt.Rows)
                    {
                        DataDictionaryInfo d = this.getDataDictionaryFormDataRow(r);
                        if (d != null)
                        {
                            cDics.Add(d);
                        }
                    }
                }
                else
                {
                    this._errorMessage = "获取数据失败！错误信息：[" + this._dicDataBase.errorText + "]";
                }

                if (cDics != null)
                {
                    for (int j = 0; j < cDics.Count; j++)
                    {
                        //删除下级字典项
                        if (this.deleteChildDataDictionarys(cDics[j].id))
                        {
                            //删除当前字典项
                            sql = "DELETE FROM SYS_DATADICTIONARY WHERE ID = @dicId";
                            YParameters par2 = new YParameters();
                            par2.add("@dicId", cDics[j].id);
                            if (this._dicDataBase.executeSqlWithOutDs(sql, par2) != 1)
                            {
                                bRet = false;
                                break;
                            }
                        }
                        else
                        {
                            bRet = false;
                            break;
                        }
                    }
                }
                else
                {
                    bRet = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRet;
        }
    }
}
