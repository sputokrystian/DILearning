using System;

namespace DILearning.Exceptions
{
    public class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException()
            : base("Type already registered!")
        {
        }
    }
}
