using System;
using System.Configuration;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using demo.ioc._04_Tooling;
using NUnit.Framework;

namespace demo.ioc.Advanced._04_FeatureToggles
{
    public class Toggles
    {

        [Test]
        public void FactoryFactory()
        {
            var c = new WindsorContainer();

            c.Register(Component.For<IUrlStrategy>().UsingFactoryMethod((k, cxt) =>
            {
                if (ConfigurationManager.AppSettings["UrlClass"] != null)
                {
                    return (IUrlStrategy)Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["UrlClass"]));
                }

                return new JPUrlStrategy();
            }));


            var x = c.ResolveAll<IUrlStrategy>();
            Assert.AreEqual(2, x.Count());
        }

        /*
         * MEEP.Web
         * - an assembly that has MEEP.MssCustomizations
         * - an assembly that has MEEP.JpCustomizations
         * - 
         * */


        public void LoadAssemblies(params Type[] assemblies)
        {
            var c = new WindsorContainer();
            foreach (var aa in assemblies.Select(a=>a.Assembly).Distinct())
            {

            }
        }
    }

    public interface JPWebAssemblyMarker
    {
        
    }
}