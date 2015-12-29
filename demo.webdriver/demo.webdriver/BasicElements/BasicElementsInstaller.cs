using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace demo.webdriver.BasicElements
{
    public class BasicElementsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<SelectBox>().LifestyleTransient());
            container.Register(Component.For<Button>().LifestyleTransient());
            container.Register(Component.For<TextBox>().LifestyleTransient());
        }
    }
}