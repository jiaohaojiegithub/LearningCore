using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data
{
    /// <summary>
    /// 面享_题目类别
    /// </summary>
    public partial class Mx_QuestionCategory : EntityBase<long>,ISort
    {
        /// <summary>
        /// 类别名
        /// </summary>
        [MaxLength(50)]
        public string CategoryName { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 父级名
        /// </summary>
        [MaxLength(50)]
        public string ParentName { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public ushort Hierarchy { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get ; set; }
        /// <summary>
        /// 对应多条题目列表
        /// </summary>
        [InverseProperty("Mx_QuestionCategory")]//数据批注 反向导航
        public ICollection<Mx_Question> Mx_Questions { get; set; }
    }
}
