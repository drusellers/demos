using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace demo.ioc._04_Tooling
{
    public class TypeScanning
    {

        [Test]
        public void WithOutScanning()
        {
            var c = new WindsorContainer();

            c.Register(Component.For<IUrlStrategy>().ImplementedBy<JPUrlStrategy>());
            c.Register(Component.For<IUrlStrategy>().ImplementedBy<MSSUrlStrategy>());


            var x = c.ResolveAll<IUrlStrategy>();
            Assert.AreEqual(2, x.Count());
        }







        [Test]
        public void WithScanning()
        {
            var c = new WindsorContainer();




            c.Register(Classes.FromThisAssembly()
                .BasedOn<IUrlStrategy>()
                .WithServiceAllInterfaces());




            var x = c.ResolveAll<IUrlStrategy>();
            Assert.AreEqual(2, x.Count());
        }  
    }
}