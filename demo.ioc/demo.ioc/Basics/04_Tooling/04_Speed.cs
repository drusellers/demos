using System;
using System.Diagnostics;
using Autofac;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace demo.ioc._04_Tooling
{
    public class Speed
    {
        [Test]
        public void SupposedToBeHard()
        {
            var c = new WindsorContainer();

            c.Register(Component.For(typeof(ISomethingHard<>))
                .ImplementedBy(typeof(SomethingHard<>))
                .LifestyleTransient());

            var t = new Stopwatch();
            t.Start();
            for (int i = 0; i < 1000000; i++)
            {
                Activator.CreateInstance<SomethingHard<string>>();
            }
            t.Stop();
            Console.WriteLine(t.Elapsed.TotalMilliseconds);


            var x = c.Resolve<ISomethingHard<string>>();

            var t2 = new Stopwatch();
            t2.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var x2 = c.Resolve<ISomethingHard<string>>();
            }
            t2.Stop();

            Console.WriteLine(t2.Elapsed.TotalMilliseconds);

            var cb = new ContainerBuilder();
            cb.RegisterType<SomethingHard<string>>()
                .As<ISomethingHard<string>>()
                .InstancePerDependency();
            var c1 = cb.Build();

            var t3 = new Stopwatch();
            t3.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var x2 = c1.Resolve<ISomethingHard<string>>();
            }
            t3.Stop();

            Console.WriteLine(t3.Elapsed.TotalMilliseconds);

            var mk = new Castle.MicroKernel.DefaultKernel();
            mk.Register(Component.For(typeof(ISomethingHard<>))
                .ImplementedBy(typeof(SomethingHard<>))
                .LifestyleTransient());

            mk.Resolve<ISomethingHard<string>>();

            var t4 = new Stopwatch();
            t4.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var x2 = mk.Resolve<ISomethingHard<string>>();
            }
            t4.Stop();

            Console.WriteLine(t4.Elapsed.TotalMilliseconds);
        }        
    }


    public interface ISomethingHard<T>
    {

    }

    public class SomethingHard<T> : ISomethingHard<T>
    {

    }
}