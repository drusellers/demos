using System.Collections.Generic;
using System.Linq;
using demo.webdriver.Browsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace demo.webdriver.BasicElements
{
    public class SelectBox
    {
        private readonly By _selector;
        private readonly IBrowserTestingSession _testingSession;

        public SelectBox(By selector, IBrowserTestingSession testingSession)
        {
            _selector = selector;
            _testingSession = testingSession;
        }

        public void SelectByValue(string value)
        {
            var el = _testingSession.Browser.FindElement(_selector);
            var se = new SelectElement(el);
            se.SelectByValue(value);
        }

        public void SelectByDisplay(string displayValue)
        {
            var el = _testingSession.Browser.FindElement(_selector);
            var se = new SelectElement(el);
            se.SelectByText(displayValue);
        }

        public string DisplayValue()
        {
            var el = _testingSession.Browser.FindElement(_selector);
            var se = new SelectElement(el);
            return se.SelectedOption.Text;
        }

        public IList<string> GetOptionDisplays()
        {
            var el = _testingSession.Browser.FindElement(_selector);
            var se = new SelectElement(el);
            return se.Options.Select(o=>o.Text).ToList();
        }
    }
}