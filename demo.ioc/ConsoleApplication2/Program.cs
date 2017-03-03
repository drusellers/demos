using System;
using Castle.Core.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace ConsoleApplication2
{
    class Program
    {
        interface IClient
        {

        }

        interface IWebOperationClient : IClient
        {

        }

        class BizRateClient : IWebOperationClient
        {

        }

        class Op : IClient
        {

        }

        interface IMom
        {

        }

        class RandomSurveyClient : IMom
        {
            public RandomSurveyClient(IWebOperationClient[] webOperationClients)
            {
                Console.WriteLine("L:{0}", webOperationClients.Length);
            }
        }
        static void Main(string[] args)
        {
            var c = new WindsorContainer();
            c.Kernel.Resolver.AddSubResolver(new CollectionResolver(c.Kernel, true));

            c.Register(
                Component.For<IClient>().ImplementedBy<RandomSurveyClient>(),
                Component.For<IMom>()
                 //.ServiceOverrides(ServiceOverride.ForKey("webOperationClients").Eq("IWebOperationClient"))
                .ImplementedBy<RandomSurveyClient>(),
                Component.For<IWebOperationClient>()
               .Named("IWebOperationClient")
                    .ImplementedBy<BizRateClient>());

            var x = c.ResolveAll<IMom>();
            var y = c.ResolveAll<IWebOperationClient>();

            Console.WriteLine(x.Length);
            Console.WriteLine(y.Length);
            Console.ReadLine();

        }
    }
}
