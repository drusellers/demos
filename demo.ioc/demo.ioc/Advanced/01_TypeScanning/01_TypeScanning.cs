using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using demo.ioc._04_Tooling;
using NUnit.Framework;

namespace demo.ioc.Advanced._01_TypeScanning
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
                .Where(t=>
                    // Ioc.Registration - <module/installer> - which type as which interface
                    t.Name.EndsWith("Config"))
                .WithServiceAllInterfaces());


            var x = c.ResolveAll<IUrlStrategy>();
            Assert.AreEqual(2, x.Count());
        }  
    }
}