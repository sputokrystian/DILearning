using System;

namespace DILearning.Exceptions
{
    public class CannotResolveTypeException : Exception
    {
        public CannotResolveTypeException(): base("Cannot resolve desired type!"){}
    }
}
