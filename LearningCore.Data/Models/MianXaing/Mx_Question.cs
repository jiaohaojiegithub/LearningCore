
using LearningCore.Common.SharedModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data
{
    /// <summary>
    /// 面享_题库
    /// </summary>
    public partial class Mx_Question:EntityBase<long>
    {
        /// <summary>
        /// 题目
        /// </summary>
        [MaxLength(500)]
        public string Question { get; set; }
        /// <summary>
        /// 题目类型
        /// </summary>
        [MaxLength(50)]
        public QuestionsTypeEnum QuestionType { get; set; }
        /// <summary>
        /// 题目分类
        /// </summary>
        [MaxLength(50)]
        public string QuestionCate { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        [MaxLength(500)]
        public string Answer { get; set; }
        /// <summary>
        /// 选项
        /// </summary>
        [MaxLength(50)]
        public string Options { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [MaxLength(100)]
        public string Tags { get; set; }
       
        public long Mx_QuestionCategoryId { get; set; }
        [ForeignKey("Mx_QuestionCategoryId")]//数据批注 外键
        public Mx_QuestionCategory Mx_QuestionCategory { get; set; }


    }
}
