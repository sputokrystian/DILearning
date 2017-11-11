using System;

namespace DILearning.Exceptions
{
    public class TypeRepositoryEmptyException : Exception
    {
        public TypeRepositoryEmptyException()
            : base("Repository of types is empty!")
        {
        }
    }
}
