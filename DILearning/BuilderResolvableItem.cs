using System;

using DILearning.Exceptions;
using DILearning.Interfaces;

namespace DILearning
{
    internal class BuilderResolvableItem : IBuilderResolvableItem
    {

        internal BuilderResolvableItem(Type inType)
        {
            this.InType = inType;
            this.AsType = this.InType;
        }

        internal Type InType { get; set; }

        internal Type AsType { get; set; }

        public void As<T>()
        {
            var asType = typeof(T);
            if (!asType.IsAssignableFrom(this.InType))
            {
                throw new NotAssignableException();
            }

            this.AsType = asType;
        }
    }
}
