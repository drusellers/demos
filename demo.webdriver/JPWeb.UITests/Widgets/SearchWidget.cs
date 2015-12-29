using demo.webdriver.BasicElements;
using demo.webdriver.Browsers;
using demo.webdriver.Pages;
using OpenQA.Selenium;

namespace JPWeb.UITests.Widgets
{
    public class SearchWidget : IAppWidget
    {
        private readonly IBrowserTestingSession _testingSession;

        public SearchWidget(IBrowserTestingSession testingSession)
        {
            _testingSession = testingSession;
        }

        public void Type(string input)
        {
            _testingSession.Browser.WaitFor(By.Name("Ntt"));


            _testingSession.GetDriver<TextBox>(By.Name("Ntt"))
                .EnterText(input);
        }

        public void Submit()
        {
            _testingSession.GetDriver<Button>(By.CssSelector(".search-button")).Click();
            _testingSession.Browser.WaitFor(By.CssSelector(".search-nav"));
        }
    }
}