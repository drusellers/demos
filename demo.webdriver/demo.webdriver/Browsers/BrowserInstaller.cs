using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace demo.webdriver.Browsers
{
    public class BrowserInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBrowserInstance>().UsingFactoryMethod((k, cxt) =>
                {
                    return k.Resolve<IBrowserTestingSession>().Browser;
                }));

        }
    }
}