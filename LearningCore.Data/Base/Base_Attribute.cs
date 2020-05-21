using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data
{
    /// <summary>
    /// 基础的属性表
    /// </summary>
    public partial class Base_Attribute:EntityBase<long>,IDescription
    {
        /// <summary>
        /// 鉴别器映射
        /// </summary>
        [NotMapped]
        public string Discriminator { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        [MaxLength(50)]
        public string AttributeName { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        [MaxLength(50)]
        public string AttributeValue { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        [MaxLength(50)]
        public string AttributeType { get; set; }
        [MaxLength(100)]
        public string Description { get; set ; }
    }
}
