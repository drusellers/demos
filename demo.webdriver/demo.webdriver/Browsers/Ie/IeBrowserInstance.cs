using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Castle.Windsor;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace demo.webdriver.Browsers.Ie
{
    public class IeBrowserInstance : BaseBrowserInstance
    {
        private readonly RemoteWebDriver _webDriver;

        public IeBrowserInstance(IWindsorContainer container) : base(container)
        {
            _webDriver = new InternetExplorerDriver();
        }

        public override void Dispose()
        {
            var reset = new ManualResetEvent(false);
            try
            {
                var disposeTask = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _webDriver.Close();
                        _webDriver.Dispose();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                    finally
                    {
                        reset.Set();
                    }
                });

                var timedOut = !reset.WaitOne(TimeSpan.FromSeconds(30));
                if (timedOut)
                {
                    foreach (var p in Process.GetProcessesByName("IEDriverServer"))
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public override RemoteWebDriver WebDriver
        {
            get { return _webDriver; }
        }

        public override SupportedBrowserType BrowserType
        {
            get { return SupportedBrowserType.Ie; }
        }
    }
}