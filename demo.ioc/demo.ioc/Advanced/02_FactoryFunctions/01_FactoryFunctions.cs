using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using demo.ioc.Advanced._03_Installers;
using demo.ioc._04_Tooling;
using NUnit.Framework;

namespace demo.ioc.Advanced._02_FactoryFunctions
{
    public class FactoryFunctions
    {

        [Test]
        public void FactoryFactory()
        {
            var c = new WindsorContainer();

            c.Register(Component.For<IUrlStrategy>().UsingFactoryMethod((kernel, creationContext) =>
            {
                return new JPUrlStrategy();
            }));


            var x = c.ResolveAll<IUrlStrategy>();
            Assert.AreEqual(2, x.Count());
        }
    }
}