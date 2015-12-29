using Castle.Windsor;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace demo.webdriver.Browsers.Firefox
{
    public class FirefoxBrowserInstance : BaseBrowserInstance
    {
        private readonly RemoteWebDriver _webDriver;

        public FirefoxBrowserInstance(IWindsorContainer container) : base(container)
        {
            _webDriver = new FirefoxDriver();
        }


        public override void Dispose()
        {
//            _webDriver.Quit();
//            _webDriver.Close();
            _webDriver.Dispose();
        }

        public override RemoteWebDriver WebDriver
        {
            get { return _webDriver; }
        }

        public override SupportedBrowserType BrowserType
        {
            get { return SupportedBrowserType.Firefox; }
        }
    }
}