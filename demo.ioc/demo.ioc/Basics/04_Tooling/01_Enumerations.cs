using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using NUnit.Framework;

namespace demo.ioc._04_Tooling
{
    public class Enumerations
    {
        [Test]
        public void Enum()
        {
            var c = new WindsorContainer();
            
            c.Register(Component.For<IUrlStrategy>().ImplementedBy<JPUrlStrategy>());
            c.Register(Component.For<IUrlStrategy>().ImplementedBy<MSSUrlStrategy>());

            c.Register(Component.For<Needy>().ImplementedBy<Needy>());

            var x = c.ResolveAll<IUrlStrategy>();
            Assert.That(x.Length, Is.EqualTo(2));
        }

        [Test]
        public void EnumerabelCtor()
        {
            var c = new WindsorContainer();

            //Teach Windsor how to resolve arrays/enumerable/collection etc
            c.Kernel.Resolver.AddSubResolver(new CollectionResolver(c.Kernel, true));
            
            c.Register(Component.For<IUrlStrategy>().ImplementedBy<JPUrlStrategy>());
            c.Register(Component.For<IUrlStrategy>().ImplementedBy<MSSUrlStrategy>());
            c.Register(Component.For<Needy>().ImplementedBy<Needy>());

            var x = c.Resolve<Needy>();
        }
    }

    public class Needy
    {
        private IEnumerable<IUrlStrategy> _is;

        public Needy(IEnumerable<IUrlStrategy> @is)
        {
            _is = @is;
        }
    }
    public interface IUrlStrategy
    {
    }

    public class JPUrlStrategy : IUrlStrategy
    {
    }

    public class MSSUrlStrategy : IUrlStrategy
    {
    }
}