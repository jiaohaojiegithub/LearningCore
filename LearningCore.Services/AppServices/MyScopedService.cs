using System;

namespace LearningCore.Services
{
    public class MyScopedService : IMyScopedService
    {
        public int MyProperty { get ; set ; }

        public bool IsMyProperty() => MyProperty >= 1000;
    }
}
