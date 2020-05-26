using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCore.Services
{
    public interface IMyScopedService
    {
        public int MyProperty { get; set; }
        bool IsMyProperty();
    }
}
