using Castle.Windsor;

namespace demo.webdriver.Browsers.Firefox
{
    public class FirefoxBrowserTestingSession : BaseBrowserTestingSession
    {
        
        public FirefoxBrowserTestingSession(IWindsorContainer container) : base(container)
        {
        }

        protected override IBrowserInstance GetInstance()
        {
            var instance = new FirefoxBrowserInstance(Container);
            return instance;
        }

    }
}