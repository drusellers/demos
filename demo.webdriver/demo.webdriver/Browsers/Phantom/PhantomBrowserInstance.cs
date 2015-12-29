using System;
using Castle.Windsor;
using OpenQA.Selenium.Remote;

namespace demo.webdriver.Browsers.Phantom
{
    public class PhantomBrowserInstance : BaseBrowserInstance
    {
        private readonly RemoteWebDriver _webDriver;

        public PhantomBrowserInstance(Uri url, IWindsorContainer container) : base(container)
        {
            _webDriver = new RemoteWebDriver(url, DesiredCapabilities.PhantomJS());
            _webDriver.FileDetector = new LocalFileDetector();
        }
        public override void Dispose()
        {
            _webDriver.Quit();
            _webDriver.Close();
            _webDriver.Dispose();
        }

        public override RemoteWebDriver WebDriver
        {
            get { return _webDriver; }
        }

        public override SupportedBrowserType BrowserType
        {
            get { return SupportedBrowserType.Phantom; }
        }
    }
}