using System;
using System.Diagnostics;
using Autofac;
using Autofac.Features.Metadata;
using Castle.MicroKernel.Registration;
using NUnit.Framework;

namespace demo.ioc._04_Tooling
{
    public class OptionalItems
    {
        [Test]
        public void X()
        {
            var cb = new ContainerBuilder();
            cb.RegisterGeneric(typeof(NotImplementedVendorService<>))
                .As(typeof(IVendorService<>));

            cb.RegisterType<Tuckers>()
                .As(typeof(IVendorService<TuckerRocky>));

            var c1 = cb.Build();

            var meta = c1.Resolve<IVendorService<HelmetHouse>>();

        }
    }

    public interface IVendor
    {

    }
    public struct TuckerRocky : IVendor
    {

    }

    public struct HelmetHouse : IVendor
    {

    }
    public interface IVendorService<TVendor> where TVendor : IVendor
    {

    }

    public class NotImplementedVendorService<TVendor> : IVendorService<TVendor> where TVendor : IVendor
    {

    }

    public class Tuckers : IVendorService<TuckerRocky>
    {

    }
}
