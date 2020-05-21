using LearningCore.Common.SharedModel;
using LearningCore.Common.SharedModel.Enums;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace LearningCore.Common.Helpers
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        private static readonly char[] _constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 生成随机字符串，默认32位
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string GenerateRandom(int length = 32)
        {
            var newRandom = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(_constant[rd.Next(_constant.Length)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 生成随机字符串，只包含数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int length = 6)
        {
            var newRandom = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(_constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 获取常用的正则数据 不合格数据替换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFrequentReplaceStr(FrequentRegularEnum type) => type switch
        {
            FrequentRegularEnum.WindowsFileName=>@"[/\\\\:*?<>|]",
            FrequentRegularEnum.SQLInject => @"\b(and|exec|insert|select|drop|grant|alter|delete|update|count|chr|mid|master|truncate|char|declare|or)\b|(\*|;|\+|'|%)",//sql注入
            _ => ""
        };
        /// <summary>
        /// 获取常用的正则数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFrequentRegularStr(FrequentRegularEnum type) => type switch
        {
            FrequentRegularEnum.UserName => @"^[A-Za-z0-9\u4e00-\u9fa5]+$",//只能由数字,汉字,字母组成
            FrequentRegularEnum.EmailUrl=> @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",//电子邮箱
            FrequentRegularEnum.域名=> @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?",//域名
            FrequentRegularEnum.InternetURL => @"[a-zA-z]+://[^\s]* 或 ^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$",//网络地址
            FrequentRegularEnum.MobileNumber=> @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$",//手机号码
            FrequentRegularEnum.PhoneNumber => @"^(13[0-9]^(\(\d{3,4}-)|\d{3.4}-)?\d{7,8}$",//电话号码
            FrequentRegularEnum.InlandPhoneNumber => @"\d{3}-\d{8}|\d{4}-\d{7}",//国内电话号码
            FrequentRegularEnum.IdentityCard => @"^\d{15}|\d{18}$",//身份证号码
            FrequentRegularEnum.QQNumber => @"[1-9][0-9]{4,}",//QQ号码
            FrequentRegularEnum.中国邮政编码 => @"[1-9]\d{5}(?!\d)",//中国邮政编码
            FrequentRegularEnum.IPAddress => @"((?:(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d))",//IP地址
            _ => ""
        };
        /// <summary>
        /// 正则表达式替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="replaceStr"></param>
        /// <returns></returns>
        public static string ReplaceStrRegular(string str, string pattern, string replaceStr)
        {
            return Regex.Replace(str, pattern, replaceStr);
        }
    }
}
