
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data
{
    public class Mx_Attribute: Base_Attribute
    {        
        /// <summary>
        /// 面享数据有一个爬取地址 获取地址
        /// </summary>
        [DataType(DataType.Url),MaxLength(200)]
        public string GetUrl { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [RegularExpression(@"^\+?((\d{2,4}(-)?)|(\(\d{2,4})))*(\d{0,16})*$"), NotMapped]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 移动手机号码
        /// </summary>
        [RegularExpression(@"^\+?\d{0,4}?[1][3-8]\d{9}*$"), NotMapped]
        public string MobileNumber { get; set; }
    }
}
