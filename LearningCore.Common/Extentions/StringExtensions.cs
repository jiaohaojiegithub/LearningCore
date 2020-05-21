using LearningCore.Common.Helpers;
using LearningCore.Common.SharedModel;
using LearningCore.Common.SharedModel.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LearningCore.Common.Extentions
{
    public static class StringExtensions
    {
       
        /// <summary>
        /// 判断字符串是否不为Null、空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NotNull(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 与字符串进行比较，忽略大小写
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string s, string value)
        {
            return s.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 首字母转小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string str = s.First().ToString().ToLower() + s.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母转大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string str = s.First().ToString().ToUpper() + s.Substring(1);
            return str;
        }

        /// <summary>
        /// 转为Base64，UTF-8格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToBase64(this string s)
        {
            return s.ToBase64(Encoding.UTF8);
        }

        /// <summary>
        /// 转为Base64
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToBase64(this string s, Encoding encoding)
        {
            if (s.IsNullOrEmpty())
                return string.Empty;

            var bytes = encoding.GetBytes(s);
            return bytes.ToBase64();
        }
        /// <summary>
        /// 判断字符串是否为Null、空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 判断字符串 是 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static byte[] ToBytesFromBase64String(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public static byte[] ToBytesFromHexString(this string hexString)
        {
            if (hexString.IsNullOrWhiteSpace())
            {
                return null;
            }

            string str = hexString.ToLower();
            Regex r = new Regex(@"^(0x)?([0-9a-f]{2})+");
            if (!r.IsMatch(str))
            {
                throw new InvalidOperationException("数字格式错误");
            }

            var spidx = str.IndexOf('x');
            spidx = spidx < 0 ? 0 : spidx + 1;
            str = str.Substring(spidx);

            byte[] bytes = new byte[str.Length / 2];

            for (int i = 0; i < str.Length; i += 2)
            {
                bytes[i / 2] = Byte.Parse(str.Substring(i, 2), NumberStyles.HexNumber);
            }

            return bytes;
        }

        public static object ToEnumObject(this string stringOrNumber, Type @enum)
        {
            if (!@enum.IsEnum) throw new ArgumentException($"{@enum.FullName}不是枚举类型。");

            object enumObject;
            var exception = new InvalidOperationException($"待转换的值不在{@enum.FullName}的定义中。");

            switch (Enum.GetUnderlyingType(@enum))
            {
                #region 有符号整数

                case var t when t == typeof(sbyte):
                    {
                        enumObject = sbyte.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<sbyte>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;
                case var t when t == typeof(short):
                    {
                        enumObject = short.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<short>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;
                case var t when t == typeof(int):
                    {
                        enumObject = int.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<int>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;
                case var t when t == typeof(long):
                    {
                        enumObject = long.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<long>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;

                #endregion

                #region 无符号整数

                case var t when t == typeof(byte):
                    {
                        enumObject = byte.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<byte>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;
                case var t when t == typeof(ushort):
                    {
                        enumObject = ushort.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<ushort>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;
                case var t when t == typeof(uint):
                    {
                        enumObject = uint.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<uint>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;
                case var t when t == typeof(ulong):
                    {
                        enumObject = ulong.TryParse(stringOrNumber, out var rei)
                            ? Enum.GetValues(@enum).Cast<ulong>().Any(en => en == rei)
                                ?
                                Enum.ToObject(@enum, rei)
                                :
                                throw exception
                            : Enum.Parse(@enum, stringOrNumber);
                    }
                    break;

                #endregion

                default:
                    throw new InvalidOperationException($"无法将值转换为{@enum.FullName}");
            }
            return enumObject;
        }

        public static T ToEnum<T>(this string stringOrNumber)
            where T : Enum
        {
            return (T)stringOrNumber.ToEnumObject(typeof(T));
        }
        #region 正则表达式
        /// <summary>
        /// 根据正则判断是否是有效的数据
        /// </summary>
        /// <param name="s"></param>
        /// <param name="strtype"></param>
        /// <returns></returns>
        public static bool IsValid(this string s, FrequentRegularEnum pattern)
        {
            if (!s.IsNullOrWhiteSpace())
            {
                return Regex.IsMatch(s, StringHelper.GetFrequentRegularStr(pattern));
            }
            return false;
        }
        /// <summary>
        /// 根据正则判断是否是有效的数据
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsValid(this string s, string pattern)
        {
            if (!s.IsNullOrWhiteSpace())
            {
                return Regex.IsMatch(s, pattern);
            }
            return false;
        }
        /// <summary>
        /// 判断字符是否有SQL注入
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool containsSqlInjection(this string s)
        {
            // "/response|group_concat|cmd|sysdate|xor|declare|db_name|char| and| or|truncate| asc| desc|drop |table|count|from|select|insert|update|delete|union|into|load_file|outfile/"
            return Regex.IsMatch(s.ToLower(), StringHelper.GetFrequentReplaceStr(FrequentRegularEnum.SQLInject));
        }
        #endregion
    }
}
