using System;
using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using demo.ioc.Advanced._04_FeatureToggles;
using NUnit.Framework;

namespace demo.ioc.Advanced._03_Installers
{
    public class MEEPAssemblyAttribute : Attribute
    {
        
    }




    public class Installers
    {
        [Test]
        public void InstallIt()
        {
            var c = new WindsorContainer();
            c.Install(FromAssembly.Containing<JPWebAssemblyMarker>());


            // MAG Kernel - provides memory, io, cpu
            //database - persistance
            //endeca - search
            //logging - logging
            
            

            //orders - 
            //purchase orders (domain elements, services, migrations, css / js / html)


            var cfg = c.Resolve<MyConfig>();
            Assert.IsNotNull(cfg);
            
        }

        //in the past i've forgotten to register an implementation of an interface.
        // smoke test - i'd build it as an extension method 

        [Test]
        public void Xa()
        {
            //all that set up junk is dried up in the AppContext

            //App.Resolve<SomeServiec>().AddOrder();
            //App.Resolve<IRepository>().Find<Orders>().Count().ShouldBe(1);
        }

        [Test]
        public void X()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(basePath, "*.dll");
            foreach (var a in assemblies)
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(a);
                var mea = assembly.GetCustomAttribute<MEEPAssemblyAttribute>();
                if (mea != null)
                {
                    Assembly.LoadFrom(a);
                }
            }

        }
    }

    public class ConfigInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer c, IConfigurationStore store)
        {
            c.Register(Classes.FromThisAssembly()
               .Where(t=>t.Name.EndsWith("Config"))
               .LifestyleSingleton());
        }
    }


    public class MyConfig
    {
        
    }
}