using System.Collections.Generic;
using BaseProduct.BizznessCode.Services.DogNames;
using Bottles;
using Bottles.Diagnostics;
using StructureMap.Configuration.DSL;

namespace BaseProduct.BizznessCode
{
    public class DemoRegistry : Registry
    {
        public DemoRegistry()
        {
            For<IActivator>().Add<SampleActivator>();

            Scan(s =>
            {
                s.AssemblyContainingType<DemoRegistry>();

                s.WithDefaultConventions();
            });
            
            //TODO: Demo Activator
            
            /*
             * MAGIC SAUCE IS BELOW
             * 
             */
            Scan(scanner =>
            {
                PackageRegistry.PackageAssemblies.Each(scanner.Assembly);
                scanner.LookForRegistries();
            });
        }
    }

    public class SampleActivator : IActivator
    {
        
        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            packages.Each(p => log.Trace("H:" + p.Name));
        }
    }
}