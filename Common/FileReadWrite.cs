using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BusinessModel;

namespace Common
{
    public class FileReadWrite
    {

        /// <summary>
        /// 更新数据库配置文件内容
        /// </summary>
        /// <param name="database">数据库实体对象</param>
        public static void UpdateDBXmlFile(DataBase database)
        {
            // 取得消息配置文件路径
            string strDatabaseFileName = "C:\\DB\\DatabaseConfig.xml";

            // 读取数据库配置文件
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(strDatabaseFileName);

            try
            {
                // 取得数据库节点的内容
                XmlNode xmlNode = xmlDocument.SelectSingleNode("/POWERMIS/Database");

                if (xmlNode != null)
                {
                    // 循环更改配置文件相关内容
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        if (child.LocalName == "item")
                        {
                            switch (child.Attributes["id"].Value)
                            {
                                case "SERVER":
                                    child.Attributes["value"].Value = database.ServerIP;
                                    break;
                                case "DATABASE":
                                    child.Attributes["value"].Value = database.DataBaseName;
                                    break;
                                case "USERCODE":
                                    child.Attributes["value"].Value = database.LoginID;
                                    break;
                                case "PASSWORD":
                                    child.Attributes["value"].Value = database.Password;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                // 保存更改的XML配置文件
                xmlDocument.Save(strDatabaseFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 读取数据库配置文件内容
        /// </summary>
        /// <returns>数据库对象</returns>
        public static DataBase ReadDatabaseXml()
        {
            DataBase database = new DataBase();

            // 取得消息配置文件路径

            string strDatabaseFileName = "C:\\DB\\DatabaseConfig.xml";
            
            try
            {
                // 读取数据库配置文件
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(strDatabaseFileName);

                // 将读取的内容为消息标题集合赋值
                XmlNode xmlNode = xmlDocument.SelectSingleNode("/POWERMIS/Database");
                if (xmlNode != null)
                {
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        if (child.LocalName == "item")
                        {
                            switch (child.Attributes["id"].Value)
                            {
                                case "SERVER":
                                    database.ServerIP = child.Attributes["value"].Value;
                                    break;
                                case "DATABASE":
                                    database.DataBaseName = child.Attributes["value"].Value;
                                    break;
                                case "USERCODE":
                                    database.LoginID = child.Attributes["value"].Value;
                                    break;
                                case "PASSWORD":
                                    database.Password = child.Attributes["value"].Value;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                return database;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
