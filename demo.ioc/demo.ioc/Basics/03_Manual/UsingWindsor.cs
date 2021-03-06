﻿using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using NUnit.Framework;

namespace demo.ioc._03_Manual
{
    public class UsingWindsor
    {
        [Test]
        public void WithHelp()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<DatabaseSettings>().ImplementedBy<DatabaseSettings>(),
                Component.For<Repository>()
                    .ImplementedBy<Repository>()
                    .LifestyleSingleton(),
                Component.For<SomeService>().ImplementedBy<SomeService>(),
                Component.For<IAnotherService>().ImplementedBy<AnotherService>(),
                Component.For<ValidationService>().ImplementedBy<ValidationService>(),
                Component.For<TheRealWorker>()
                    .ImplementedBy<TheRealWorker>()
                    .LifestyleTransient()
                );

            var w = container.Resolve<TheRealWorker>();
            var w2 = container.Resolve<TheRealWorker>();

            Assert.That(w==w2, Is.False);

            Assert.That(w, Is.Not.Null);
        }

        [Test]
        public void FromXml()
        {
            var container = new WindsorContainer(new XmlInterpreter("castle.xml"));

            var w = container.Resolve<TheRealWorker>();
            var w2 = container.Resolve<TheRealWorker>();

            Assert.That(w == w2, Is.False);

            Assert.That(w, Is.Not.Null);
        }


        [Test]
        public void FromXmlAndCode()
        {
            var container = new WindsorContainer(new XmlInterpreter("castle.partial.xml"));
            container.Register(
                Component.For<TheRealWorker>()
                    .ImplementedBy<TheRealWorker>()
                    .LifestyleTransient()
            );

            var w = container.Resolve<TheRealWorker>();
            var w2 = container.Resolve<TheRealWorker>();

            Assert.That(w == w2, Is.False);

            Assert.That(w, Is.Not.Null);
        }

        [Test]
        public void RegistrationErrors()
        {
            var c = new WindsorContainer();
            c.Register(
                Component.For<DatabaseSettings>().ImplementedBy<DatabaseSettings>(),
                Component.For<Repository>().ImplementedBy<Repository>(),
                Component.For<SomeService>().ImplementedBy<SomeService>(),
                Component.For<AnotherService>().ImplementedBy<AnotherService>(),
                Component.For<TheRealWorker>().ImplementedBy<TheRealWorker>()
                );

            Assert.That(()=>c.Resolve<TheRealWorker>(), Throws.InstanceOf<HandlerException>());
        }
    }

    public class Weirdness
    {
        interface A
        {

        }

        interface B : A
        {

        }

        class Yo : B
        {

        }

        [Test]
        public void ABC()
        {
            var c = new WindsorContainer();
            c.Register(Component.For<B>().ImplementedBy<Yo>());
            var x = c.ResolveAll<A>();
            var y = c.ResolveAll<B>();

            Assert.That(x, Is.Empty);
            Assert.That(y, Is.Not.Empty);
        }
    }
}
