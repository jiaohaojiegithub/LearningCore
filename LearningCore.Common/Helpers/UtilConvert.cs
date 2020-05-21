using LearningCore.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace LearningCore.Common.Helpers
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public static class UtilConvert
    {
        public static int ToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        public static int ToInt(this object thisValue, int errorValue)
        {
            int reval;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static long ToLong(this object s)
        {
            if (s == null || s == DBNull.Value)
                return 0L;

            long.TryParse(s.ToString(), out long result);
            return result;
        }

        public static double ToMoney(this object thisValue)
        {
            double reval;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }

        public static double ToMoney(this object thisValue, double errorValue)
        {
            double reval;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static string ToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }

        public static string ToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }

        public static decimal ToDecimal(this object thisValue)
        {
            decimal reval;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }

        public static decimal ToDecimal(this object thisValue, decimal errorValue)
        {
            decimal reval;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static DateTime ToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }

        public static DateTime ToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static bool ToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        public static byte ToByte(this object s)
        {
            if (s == null || s == DBNull.Value)
                return 0;

            byte.TryParse(s.ToString(), out byte result);
            return result;
        }

        #region ==字节转换==
        /// <summary>
        /// 转换为16进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes, bool lowerCase = true)
        {
            if (bytes == null)
                return null;

            var result = new StringBuilder();
            var format = lowerCase ? "x2" : "X2";
            for (var i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString(format));
            }

            return result.ToString();
        }

        /// <summary>
        /// 16进制转字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string s)
        {
            if (s.IsNullOrEmpty())
                return null;
            var bytes = new byte[s.Length / 2];

            for (int x = 0; x < s.Length / 2; x++)
            {
                int i = (Convert.ToInt32(s.Substring(x * 2, 2), 16));
                bytes[x] = (byte)i;
            }

            return bytes;
        }

        /// <summary>
        /// 转换为Base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            if (bytes == null)
                return null;

            return Convert.ToBase64String(bytes);
        }

        #endregion

        #region ==Object转换==
        //public static TResult ObjConvert<TInput, TResult>(this TInput obj)
        //{
        //    if (obj == null)
        //        return default(TResult);
        //    object result;
        //    var resultT = typeof(TResult);
        //    try
        //    {
        //        foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        //        {
        //            //var val = prop.GetValue(obj, null);
        //            //var name = prop.Name;
        //            if(prop.PropertyType.FullName!=typeof(Nullable).FullName)
        //                prop.SetValue(result,obj)
        //        }
        //    } catch 
        //    {
        //        result = default(TResult);
        //    }
        //    return (TResult)result;
        //}

        /// <summary>
        /// 通过反射的方式来实现对象映射
        /// </summary>
        /// <typeparam name="TModel">映射输出模型</typeparam>
        /// <typeparam name="TModelDto">映射源模型</typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TModel Trans<TModel, TModelDto>(TModelDto dto)
              where TModel : class
              where TModelDto : class
        {
            TModel model = Activator.CreateInstance(typeof(TModel)) as TModel;
            //获取TModel的属性集合
            PropertyInfo[] modlePropertys = typeof(TModel).GetProperties();
            //获取TModelDto的属性集合
            Type type = dto.GetType();
            PropertyInfo[] propertys = type.GetProperties();
            foreach (var property in propertys)
            {
                foreach (var mproperty in modlePropertys)
                {
                    //如果属性名称一致，则将该属性值赋值到TModel实例中
                    //这里可以用Attribute来实现成员的自定义映射
                    if (property.Name.Equals(mproperty.Name))
                    {
                        mproperty.SetValue(model, property.GetValue(dto));
                        break;
                    }
                }
            }

            //获取TModel的字段集合
            FieldInfo[] modelfieldInfos = typeof(TModel).GetFields();
            //获取TModelDto的字段集合
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var field in fieldInfos)
            {
                foreach (var mfield in modelfieldInfos)
                {
                    //如果字段名称一致，则将该字段值赋值到TModel实例中
                    if (field.Name.Equals(mfield.Name))
                    {
                        mfield.SetValue(model, field.GetValue(dto));
                        break;
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 通过序列化的方式，对象类型可以转换成json字符串，然后再由json字符串转换成所需的对象
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TModelDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TModel TransByDeserialize<TModel, TModelDto>(TModelDto dto)
             where TModel : class
             where TModelDto : class
        {
            //return JsonConvert.DeserializeObject<TModel>(JsonConvert.SerializeObject(dto));
            return JsonSerializer.Deserialize<TModel>(JsonSerializer.Serialize(dto));
        }
        #endregion
    }

    #region 数据类型转换
    /// <summary>
    /// 使用Expression表达式的方式来解决，将所需实例对象new、赋值的过程先写入表达式，然后生成lambda表达式，最后编译该表达式生成委托，invoke即可
    /// </summary>
    public static class ExpressionAndSeesionMethod

    {
        public static Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public static TModel Trans<TModel, TModelDto>(TModelDto dto)
        {
            Type modelType = typeof(TModel);
            Type modelDtoType = typeof(TModelDto);

            //如果_dictionary中不存在该key，则存进去
            string key = $"{modelDtoType.Name}-->{modelType.Name}";
            if (!_dictionary.ContainsKey(key))
            {
                //创建一个lambda参数x，定义的对象为TModelDto
                ParameterExpression parameterExpression = Expression.Parameter(modelDtoType, "x");
                //开始生成lambda表达式
                List<MemberBinding> list = new List<MemberBinding>();
                foreach (var item in modelType.GetProperties())
                {
                    //为x参数表达式生成一个属性值
                    MemberExpression property = Expression.Property(parameterExpression, modelDtoType.GetProperty(item.Name));
                    //将该属性初始化 eg:No=x.No
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    list.Add(memberBinding);
                }

                foreach (var item in typeof(TModel).GetFields())
                {
                    //为x参数表达式生成一个字段值
                    MemberExpression field = Expression.Field(parameterExpression, modelDtoType.GetField(item.Name));
                    //将该字段初始化
                    MemberBinding memberBinding = Expression.Bind(item, field);
                    list.Add(memberBinding);
                }
                //调用构造函数，初始化一个TModel eg: new{No=x.No...}
                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(modelType), list);
                //创建lambda表达式  eg: x=>new{ No=x.No...}
                Expression<Func<TModelDto, TModel>> lambda = Expression.Lambda<Func<TModelDto, TModel>>(memberInitExpression, parameterExpression);
                //将lambda表达式生成委托
                Func<TModelDto, TModel> func = lambda.Compile();
                _dictionary[key] = func;
            }
            return ((Func<TModelDto, TModel>)_dictionary[key]).Invoke(dto);
        }
    }

    /// <summary>
    ///泛型委托基于泛型类之上 【效率较高】
    ///泛型静态类在确定参数类型的时候会调用其静态函数
    ///在执行委托时，泛型委托会内置查找相应的委托来执行
    /// </summary>
    public static class ExpressionAndFuncMethod<TModel, TModelDto>
       where TModel : class
       where TModelDto : class
    {
        static ExpressionAndFuncMethod()
        {
            ExpressionMapper();
        }

        public static Func<TModelDto, TModel> _func = null;

        public static void ExpressionMapper()
        {
            Type modelType = typeof(TModel);
            Type modelDtoType = typeof(TModelDto);

            //创建一个lambda参数x，定义的对象为TModelDto
            ParameterExpression parameterExpression = Expression.Parameter(modelDtoType, "x");
            //开始生成lambda表达式
            List<MemberBinding> list = new List<MemberBinding>();
            foreach (var item in modelType.GetProperties())
            {
                //为x参数表达式生成一个属性值
                MemberExpression property = Expression.Property(parameterExpression, modelDtoType.GetProperty(item.Name));
                //将该属性初始化 eg:No=x.No
                MemberBinding memberBinding = Expression.Bind(item, property);
                list.Add(memberBinding);
            }

            foreach (var item in typeof(TModel).GetFields())
            {
                //为x参数表达式生成一个字段值
                MemberExpression field = Expression.Field(parameterExpression, modelDtoType.GetField(item.Name));
                //将该字段初始化
                MemberBinding memberBinding = Expression.Bind(item, field);
                list.Add(memberBinding);
            }
            //调用构造函数，初始化一个TModel eg: new{No=x.No...}
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(modelType), list);
            //创建lambda表达式  eg: x=>new{ No=x.No...}
            Expression<Func<TModelDto, TModel>> lambda = Expression.Lambda<Func<TModelDto, TModel>>(memberInitExpression, parameterExpression);
            //将lambda表达式生成委托
            _func = lambda.Compile();
        }

        public static TModel Trans(TModelDto dto)
        {
            if (_func != null)
                return _func(dto);
            return default(TModel);
        }
    }
    #endregion
}
