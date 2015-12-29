using System;
using System.Collections.Generic;
using demo.webdriver.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace demo.webdriver.Browsers
{
    public interface IBrowserInstance : IDisposable
    {
        RemoteWebDriver WebDriver { get; }
        SupportedBrowserType BrowserType { get; }

        string GetLocation();
        void NavigateTo(string url);
        T NavigateTo<T>() where T : IAppPage;
        T NavigateTo<T>(object args) where T : IAppPage;

        string GetPageSource();
        void TakeScreenshot(string location);
        

        bool IsElementPresent(By selector);
        bool IsElementDisabled(By selector);
        bool IsTextPresent(string textToFind);
        bool IsElementVisible(By selector);

        IWebElement FindElement(By selector);
        IEnumerable<IWebElement> FindElements(By selector);
        void WaitFor(Func<IBrowserInstance, bool> condition);
        void WaitFor(By selector);
    }
}