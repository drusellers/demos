using Castle.Windsor;
using demo.webdriver.BasicElements;
using demo.webdriver.Browsers.Chrome;
using demo.webdriver.Browsers.Firefox;
using demo.webdriver.Browsers.Ie;
using demo.webdriver.Pages;
using Castle.MicroKernel.Registration;
using demo.webdriver.Browsers.Phantom;

namespace demo.webdriver.Browsers
{
    public class BrowserTestingSessionFactory : IBrowserTestingSessionFactory
    {
        private IWindsorContainer _container;

        //logging
        private IBrowserTestingSession _testingSession;

        public BrowserTestingSessionFactory()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<IWindsorContainer>().Instance(_container));


            
            _container.Install(
                new BrowserInstaller(),
                new BasicElementsInstaller()
                );
            //new up logging    
        }



        public IBrowserTestingSession CreateSession<TMarker>(SupportedBrowserType browserType = SupportedBrowserType.Chrome)
        {
            var session = CreateTestSession(browserType);
            session.Start();
            _container.Register(Classes.FromAssemblyContaining<TMarker>().BasedOn<IAppPage>());
            _container.Register(Classes.FromAssemblyContaining<TMarker>().BasedOn<IAppWidget>());

            _container.Register(Component.For<IBrowserTestingSession>().Instance(session));
            return session;
        }
 




        private IBrowserTestingSession CreateTestSession(SupportedBrowserType browserType)
        {
            switch (browserType)
            {
                case SupportedBrowserType.Firefox:
                    return new FirefoxBrowserTestingSession(_container);
                case SupportedBrowserType.Ie:
                    return new IeBrowserTestingSession(_container);
                case SupportedBrowserType.Phantom:
                    return new PhantomBrowserTestingSession(_container);
                default:
                    return new ChromeBrowserTestingSession(_container);
            }
        }
    }
}