using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningCore.Data
{
    public class EntityBase<TKey> : IEntityBase
    {
        public EntityBase()
        {
            IsDeleted = false;
            ModifiedTime = DateTime.Now;
            CreatedTime = DateTime.Now;
        }
        [Required]
        public virtual TKey Id { get; set; }
        /// <summary>
        /// 模型中排除的数据
        /// </summary>
        [NotMapped]
        public virtual TKey CreatedUserId { get; set; }
        [NotMapped]
        public string CreatedUserName { get ; set ; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),DataType(DataType.Date),Required]
        public DateTime CreatedTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed), DataType(DataType.Date),Required]
        public DateTime ModifiedTime { get; set ; }
    }
}
