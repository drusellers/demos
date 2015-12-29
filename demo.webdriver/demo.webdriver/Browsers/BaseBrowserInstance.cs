using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using Castle.Windsor;
using demo.webdriver.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace demo.webdriver.Browsers
{
    public abstract class BaseBrowserInstance : IBrowserInstance
    {
        private IWindsorContainer _container;
        public abstract void Dispose();
        public abstract RemoteWebDriver WebDriver { get; }
        public abstract SupportedBrowserType BrowserType { get; }

        protected BaseBrowserInstance(IWindsorContainer container)
        {
            _container = container;
        }

        public bool IsElementVisible(By selector)
        {
            try
            {
                return FindElement(selector).Displayed;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        
        }

        public bool IsTextPresent(string textToFind)
        {
            return WebDriver.IsTextPresent(textToFind);
        }

        public string GetLocation()
        {
            return WebDriver.Url ?? String.Empty;
        }

        public void NavigateTo(string url)
        {
            try
            {
                WebDriver.Navigate().GoToUrl(url);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Unable to navigate to url '{0}'. Exception:", url);
                Debug.WriteLine(e);
            }
        }

        public TPage NavigateTo<TPage>() where TPage : IAppPage
        {
            var page = _container.Resolve<TPage>();
            page.Navigate();
            return page;
        }

        public TPage NavigateTo<TPage>(object args) where TPage : IAppPage
        {
            var page = _container.Resolve<TPage>(args);
            page.Navigate();
            return page;
        }

        public string GetPageSource()
        {
            try
            {
                return WebDriver.PageSource;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Unable to fetch page source. Exception:");
                Debug.WriteLine(e);
                return string.Empty;
            }
        }

        public void TakeScreenshot(string location)
        {
            var screenshotResponse = ((ITakesScreenshot)WebDriver).GetScreenshot();
            screenshotResponse.SaveAsFile(location, ImageFormat.Jpeg);
        }

        public bool IsElementPresent(By selector)
        {
            return WebDriver.IsElementPresent(selector);
        }

        public bool IsElementDisabled(By selector)
        {
            return WebDriver.IsElementDisabled(selector);
        }

        public IWebElement FindElement(By selector)
        {
            return FindElements(selector).FirstOrDefault();
        }

        public IEnumerable<IWebElement> FindElements(By selector)
        {
            //return WebDriver.FindElements(selector);

            var w = new DefaultWait<IWebDriver>(WebDriver);

            w.Timeout = TimeSpan.FromSeconds(2);
            w.PollingInterval = TimeSpan.FromMilliseconds(100);
            w.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            
            //waits for true or a non-null value
            return w.Until((b) =>
            {
                return b.FindElements(selector);
            });
        }

        public void WaitFor(By selector)
        {
            WaitFor(b =>
            {
                var el = b.FindElement(selector);
                return el != null && el.Displayed;
            });
        }

        public void WaitFor(Func<IBrowserInstance, bool> condition)
        {
            var w = new DefaultWait<IBrowserInstance>(this);
            w.Timeout = TimeSpan.FromSeconds(2);
            w.PollingInterval = TimeSpan.FromMilliseconds(100);

            //waiting for a true or non-null value
            w.Until(condition);
        }
    }
}