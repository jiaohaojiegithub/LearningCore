using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data
{
    public class TodoItem
    {
        [Key,Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(10)]
        [Display(Name ="名称")]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
