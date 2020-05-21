using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCore.Common.SharedModel.Enums
{
    public enum QuestionsTypeEnum:byte
    {
        未定义 = 0,
        /// <summary>
        /// 单选题
        /// </summary>
        单选题 = 1,
        /// <summary>
        /// 多选题
        /// </summary>
        多选题 = 2,
        /// <summary>
        /// 简答题
        /// </summary>
        简答题 = 3,
        /// <summary>
        /// 判断题
        /// </summary>
        判断题 = 4,
        /// <summary>
        /// 填空题
        /// </summary>
        填空题 = 5
    }
}
