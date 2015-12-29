using System;
using Castle.Windsor;
using OpenQA.Selenium.Remote;

namespace demo.webdriver.Browsers.Chrome
{
    public class ChromeBrowserInstance : BaseBrowserInstance
    {
        private readonly RemoteWebDriver _webDriver;

        public ChromeBrowserInstance(Uri url, IWindsorContainer container) : base(container)
        {
            _webDriver = new RemoteWebDriver(url, DesiredCapabilities.Chrome());
            _webDriver.FileDetector = new LocalFileDetector();
            //_webDriver.Manage().Window.Size;
        }

        public override void Dispose()
        {
            _webDriver.Close();
            _webDriver.Quit();
            _webDriver.Dispose();
        }

        public override RemoteWebDriver WebDriver
        {
            get { return _webDriver; }
        }

        public override SupportedBrowserType BrowserType
        {
            get { return SupportedBrowserType.Chrome; }
        }
    }
}