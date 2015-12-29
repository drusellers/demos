using System;
using System.Diagnostics;
using Autofac;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NUnit.Framework;

namespace demo.ioc._04_Tooling
{
    public class Installers
    {
        [Test]
        public void Manual()
        {
            var c = new WindsorContainer();
            c.Install(new AnInstaller());

            var x = c.Resolve<Needy>();
        }

        [Test]
        public void TypeScan()
        {
            var c = new WindsorContainer();

            c.Install(FromAssembly.Containing<AnInstaller>());

            var x = c.Resolve<Needy>();
        } 


    }

    public class AnInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer c, IConfigurationStore store)
        {
            //Teach Windsor how to resolve arrays/enumerable/collection etc
            c.Kernel.Resolver.AddSubResolver(new CollectionResolver(c.Kernel, true));

            c.Register(Component.For<IUrlStrategy>().ImplementedBy<JPUrlStrategy>());
            c.Register(Component.For<IUrlStrategy>().ImplementedBy<MSSUrlStrategy>());
            c.Register(Component.For<Needy>().ImplementedBy<Needy>());
        }
    }
}