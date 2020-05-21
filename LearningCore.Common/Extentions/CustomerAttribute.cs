using LearningCore.Common.Helpers;
using LearningCore.Common.SharedModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningCore.Common.Extentions
{
    /// <summary>
    /// 自定义属性验证
    /// </summary>
    public class CustomerAttribute: RegularExpressionAttribute
    {
        /// <summary>
        /// 根据正则表达式自定义验证规则
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <param name="errormsg">错误描述</param>
        public CustomerAttribute(string pattern, string errormsg) : base(pattern)
        {
            ErrorMessage = errormsg;
        }
        /// <summary>
        /// 常用规则
        /// </summary>
        /// <param name="type"></param>
        /// <param name="errormsg"></param>
        public CustomerAttribute(FrequentRegularEnum type, string errormsg) :base(StringHelper.GetFrequentRegularStr(type))
        {
            ErrorMessage = errormsg;
        }
    }
}
