using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningCore.Common.Helpers
{
    /// <summary>
    /// 配置文件读取
    /// </summary>
    public class FileConfigHelper
    {
        /// <summary>
        /// json配置文件读取
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="basePath"></param>
        /// <returns>FileConfigHelper.GetJsonConfig("MyTestResource.json").GetSection("ConnectionString:TestDb").Value</returns>
        public static IConfigurationRoot GetJsonConfig(
         string configFileName = "appsettings.json",
         string basePath = "")
        {
            basePath = string.IsNullOrWhiteSpace(basePath) ? AppContext.BaseDirectory : basePath;//Directory.GetCurrentDirectory()
            var builder = new ConfigurationBuilder().
             SetBasePath(basePath).
             AddJsonFile(configFileName, true, true);
            return builder.Build();
        }
        /// <summary>
        /// xml配置文件读取
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="basePath"></param>
        /// <returns>FileConfigHelper.GetXmlConfig("XMLResource.xml").GetSection("ConnectionString:TestDb:value").Value</returns>
        public static IConfigurationRoot GetXmlConfig(
         string configFileName = "appsettings.xml",
         string basePath = "")
        {
            basePath = string.IsNullOrWhiteSpace(basePath) ? AppContext.BaseDirectory : basePath;//Directory.GetCurrentDirectory()
            var builder = new ConfigurationBuilder().
             //SetBasePath(basePath).
             AddXmlFile(b =>
             {
                 b.Path = configFileName;
                 b.FileProvider = new PhysicalFileProvider(basePath);
             });
            return builder.Build();
        }
        //读取sql文件字符
        public static List<string> GetSqlFileData(string configFileName = "appsettings.sql",
         string basePath = "")
        {
            var result = new List<string>();
            string s = string.Empty;
            basePath = string.IsNullOrEmpty(basePath) ? AppContext.BaseDirectory : basePath;
            string path = Path.Combine(basePath, configFileName);
            if (!File.Exists(path))
                s = "不存在相应的目录";
            else
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string line = string.Empty;
                    char spaceChar = ' ';
                    string newLIne = "\r\n", semicolon = ";";
                    string sprit = "/", whiffletree = "-";
                    string sql = string.Empty;
                    do
                    {
                        line = streamReader.ReadLine();
                        // 文件结束
                        if (line == null) break;
                        // 跳过注释行
                        if (line.StartsWith(sprit) || line.StartsWith(whiffletree)) continue;
                        // 去除右边空格
                        line = line.TrimEnd(spaceChar);
                        sql += line;
                        // 以分号(;)结尾，则执行SQL
                        if (sql.EndsWith(semicolon))
                        {
                            result.Add(sql);
                            sql = string.Empty;
                        }
                        else
                        {
                            // 添加换行符
                            if (sql.Length > 0) sql += newLIne;
                        }
                    } while (true);
                   
                }
            }
            return result;
        }
    }
}
