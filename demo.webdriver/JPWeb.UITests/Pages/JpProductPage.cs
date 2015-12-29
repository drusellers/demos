using demo.webdriver.BasicElements;
using demo.webdriver.Browsers;
using demo.webdriver.Pages;
using OpenQA.Selenium;

namespace JPWeb.UITests.Pages
{
    public class JpProductPage : IAppPage
    {
        private IBrowserTestingSession _testingSession;
        private string _sku;

        public JpProductPage(IBrowserTestingSession testingSession, string sku)
        {
            _testingSession = testingSession;
            _sku = sku;
        }

        public void Navigate()
        {
            //_testingSession.Browser.NavigateTo<HomeController>(c => c.HomePage(22));
            _testingSession.Browser.NavigateTo("http://www.jpcycles.com/product/"+_sku);
        }

        public void SetQty(int qty)
        {
            _testingSession.Browser.WaitFor(By.Name("quantity"));
            _testingSession.Browser.FindElement(By.Name("quantity")).SendKeys(qty.ToString());
        }

        public void AddToCart()
        {
            _testingSession.GetDriver<Button>(By.Id("addToCartButton")).Click();
        }
    }
}