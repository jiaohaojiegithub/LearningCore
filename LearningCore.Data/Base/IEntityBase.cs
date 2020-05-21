using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCore.Data
{
    interface IEntityBase
    {
        DateTime ModifiedTime { get; set; }
        DateTime CreatedTime { get; set; }
        bool IsDeleted { get; set; }
    }
}
