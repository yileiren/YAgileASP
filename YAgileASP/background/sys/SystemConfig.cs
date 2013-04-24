using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YAgileASP.background.sys
{
    /// <summary>
    /// 系统管理配置信息。
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// 数据库配置文件名称。
        /// </summary>
        static public string databaseConfigFileName = "DataBaseConfig.config";

        /// <summary>
        /// 数据库配置文件节点名称。
        /// </summary>
        static public string databaseConfigNodeName = "SQLServer";

        /// <summary>
        /// 配置文件使用的解密密钥。
        /// </summary>
        static public string configFileKey = "YLRPro@YAgileASP";
    }
}