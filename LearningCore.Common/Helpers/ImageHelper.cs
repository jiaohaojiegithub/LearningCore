using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningCore.Common.Helpers
{
    public class ImageHelper
    {
        #region 图片打包下载
        /// <summary>
        /// 下载图片到本地，压缩
        /// </summary>
        /// <param name="urlList">图片列表</param>
        /// <param name="curDirName">要压缩文档的路径</param>
        /// <param name="curFileName">压缩后生成文档保存路径</param>
        /// <returns></returns>
        public static bool ImagePackZip(List<string> urlList, string curDirName, string curFileName)
        {
            return CommonException(() =>
            {
                //1.新建文件夹  
                if (!Directory.Exists(curDirName))
                    Directory.CreateDirectory(curDirName);

                //2.下载文件到服务器临时目录
                foreach (var url in urlList)
                {
                    DownPicToLocal(url, curDirName + "\\");
                    Thread.Sleep(60);//加个延时，避免上一张图还没下载完就执行下一张图的下载操作
                }

                //3.压缩文件夹
                if (!File.Exists(curFileName))
                    ZipFile.CreateFromDirectory(curDirName, curFileName); //压缩

                //异步删除压缩前，下载的临时文件
                Task.Run(() =>
                {
                    if (Directory.Exists(curDirName))
                        Directory.Delete(curDirName, true);
                });
                return true;
            });
        }
        /// <summary>
        /// 下载压缩包
        /// </summary>
        /// <param name="targetfile">目标临时文件地址</param>
        ///// <param name="filename">文件名</param>
        //public static bool DownePackZip(string targetfile, string filename)
        //{
        //    return CommonException(() =>
        //    {
        //        FileInfo fileInfo = new FileInfo(targetfile);
        //        HttpResponse rs = System.Web.HttpContext.Current.Response;
        //        rs.Clear();
        //        rs.ClearContent();
        //        rs.ClearHeaders();
        //        rs.AddHeader("Content-Disposition", "attachment;filename=" + $"{filename}");
        //        rs.AddHeader("Content-Length", fileInfo.Length.ToString());
        //        rs.AddHeader("Content-Transfer-Encoding", "binary");
        //        rs.AddHeader("Pragma", "public");//这两句解决https的cache缓存默认不给权限的问题
        //        rs.AddHeader("Cache-Control", "max-age=0");
        //        rs.ContentType = "application/octet-stream";
        //        rs.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //        rs.WriteFile(fileInfo.FullName);
        //        rs.Flush();
        //        rs.End();
        //        return true;
        //    });
        //}

        /// <summary>
        /// 下载一张图片到本地
        /// </summary>
        /// <param name="url"></param>
        public static bool DownPicToLocal(string url, string localpath)
        {
            return CommonException(() =>
            {
                string fileprefix = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                var filename = $"{fileprefix}.jpg";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 60000;
                WebResponse response = request.GetResponse();
                using (Stream reader = response.GetResponseStream())
                {
                    FileStream writer = new FileStream(localpath + filename, FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] buff = new byte[512];
                    int c = 0; //实际读取的字节数
                    while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, c);
                    }
                    writer.Close();
                    writer.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                response.Close();
                response.Dispose();

                return true;
            });
        }
        /// <summary>
        /// 公用捕获异常
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private static bool CommonException(Func<bool> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
