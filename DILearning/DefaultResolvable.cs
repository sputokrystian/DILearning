using System;
using System.Collections.Generic;
using System.Linq;

using DILearning.Exceptions;
using DILearning.Interfaces;

namespace DILearning
{

    internal class DefaultResolvable : IResolvable
    {
        internal IDictionary<Type, Type> TypeContainer { get; set; }
        internal bool ResolveImplicit { get; set; }

        public T Resolve<T>()
        {
            if (TypeContainer == null)
            {
                throw new TypeRepositoryEmptyException();
            }

            var desiredType = typeof(T);
            var outputPair = TypeContainer.FirstOrDefault(pair => pair.Key == desiredType);
            if (IsPairValuesNull(outputPair))
            {
                if (!this.ResolveImplicit)
                {
                    throw new CannotResolveTypeException();
                }


                outputPair = this.TypeContainer.FirstOrDefault(pair => desiredType.IsAssignableFrom(pair.Value));
                if (IsPairValuesNull(outputPair))
                {
                    throw new CannotResolveTypeException();
                }
            }


            var outputType = outputPair.Value;
            if (!desiredType.IsAssignableFrom(outputType))
            {
                throw new CannotResolveTypeException();
            }

            // tworzymy instancje klasy
            return (T)Activator.CreateInstance(outputType);
        }

        private bool IsPairValuesNull(KeyValuePair<Type,Type> outputPair)
        {
            return (outputPair.Key == null || outputPair.Value == null);
        }


    }
}
