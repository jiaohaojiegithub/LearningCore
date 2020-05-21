using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningCore.Data
{
    /// <summary>
    /// 面享_岗位
    /// </summary>
    public partial class Mx_Jobs:EntityBase<long>,IDescription
    {
        [MaxLength(50)]
        public string JobName { get; set; }
        [MaxLength(20)]
        public string JobType { get; set; }
        [MaxLength(100)]
        public string Description { get ; set ; }
    }
}
