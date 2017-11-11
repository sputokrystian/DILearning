using System;
using System.Collections.Generic;
using System.Linq;

using DILearning.Exceptions;
using DILearning.Interfaces;
namespace DILearning
{
    public class ContainerBuilder
    {
        private readonly List<BuilderResolvableItem> typeContainer;
        public bool ResolveImplicit { get; set; }

        public ContainerBuilder()
        {
            this.typeContainer = new List<BuilderResolvableItem>();
        }

        public IBuilderResolvableItem Register<T>()
        {
            var builderItem = new BuilderResolvableItem(typeof(T));
            this.typeContainer.Add(builderItem);
            return builderItem;
        }

        public IResolvable Build()
        {

            var resolvable = new DefaultResolvable
            {
                TypeContainer = new Dictionary<Type, Type>(),
                ResolveImplicit = this.ResolveImplicit
            };

            foreach (var builderResolvableItem in this.typeContainer)
            {
                if (builderResolvableItem.InType.IsInterface || builderResolvableItem.InType.IsAbstract)
                {
                    throw new NotAssignableException();
                }

                if (resolvable.TypeContainer.Keys.Any(type => type == builderResolvableItem.AsType))
                {
                    throw new TypeAlreadyRegisteredException();
                }

                var pair = new KeyValuePair<Type, Type>(
                    builderResolvableItem.AsType, builderResolvableItem.InType);
                resolvable.TypeContainer.Add(pair);
            }

            return resolvable;
        }

    }
}
