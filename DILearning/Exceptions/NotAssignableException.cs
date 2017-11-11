using System;

namespace DILearning.Exceptions
{
    public class NotAssignableException : Exception
    {
        public NotAssignableException() : base("Ouptut type is not assignable from source type!") {}
    }
}
