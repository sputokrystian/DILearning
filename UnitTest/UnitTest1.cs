using DILearning.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DILearning;

namespace UnitTest
{
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void RegisteringWithoutAs()
        {
            var builder = new ContainerBuilder();

            builder.Register<ClassA>();
            var resolver = builder.Build();

            var resolvedInstance = resolver.Resolve<ClassA>();
            var exceptedInstance = new ClassA();

            Assert.AreEqual(exceptedInstance.GetType(), resolvedInstance.GetType());
        }

        [TestMethod]
        public void RegisteringSubclass()
        {
            var builder = new ContainerBuilder();

            builder.Register<ClassB>().As<ClassA>();
            var resolver = builder.Build();

            var resolvedInstance = resolver.Resolve<ClassA>();
            var exceptedInstance = new ClassB();

            Assert.AreEqual(exceptedInstance.GetType(), resolvedInstance.GetType());
        }

        [TestMethod]
        public void RegisteringInterface()
        {
            var builder = new ContainerBuilder();

            builder.Register<ClassB>().As<IInterfaceForClassB>();
            var resolver = builder.Build();

            var resolvedInstance = resolver.Resolve<IInterfaceForClassB>();
            var exceptedInstance = (new ClassB()) as IInterfaceForClassB;

            Assert.AreEqual(exceptedInstance.GetType(), resolvedInstance.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(NotAssignableException))]
        public void RegisteringNotAssignableClass()
        {
            var builder = new ContainerBuilder();

            builder.Register<ClassA>().As<ClassB>();
            builder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(TypeAlreadyRegisteredException))]
        public void RegisteringTheSameTypeTwice()
        {
            var builder = new ContainerBuilder();

            builder.Register<ClassB>().As<ClassA>();
            builder.Register<ClassB>().As<ClassA>();
            builder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(CannotResolveTypeException))]
        public void ResolvingUnregisteredType()
        {
            var builder = new ContainerBuilder();
            builder.Build().Resolve<ClassA>();
        }
    }
}
