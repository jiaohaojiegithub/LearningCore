using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data
{
    [Table("Blog")]
    public partial class Blog:EntityBase<long>
    {
        [Required]
        [Column(TypeName = "varchar(200)"),MaxLength(100)]
        public string Url { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        [MaxLength(10)]
        public string FamilyName { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        [ConcurrencyCheck,MaxLength(10)]//并发标记控制
        public string LastName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Timestamp/rowversion 是一个属性，在每次插入或更新行时，数据库会自动为其生成新值。 此属性也被视为并发标记，这确保了在你查询行后，如果正在更新的行发生了更改，则会出现异常。
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
