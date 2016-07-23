using Autofac;
using Autofac.Features.Metadata;
using NUnit.Framework;

namespace demo.ioc.Advanced._05_Metadata
{
    public class Metadata
    {
        [Test]
        public void MetadataPlay()
        {
            var cb = new ContainerBuilder();

            cb.RegisterType<O>()
                .WithMetadata("a","b");

            var c = cb.Build();


            var o = c.Resolve<Meta<O>>();
            Assert.AreEqual("b", o.Metadata["a"]);
        }


        class O
        {
            
        }
    }
}