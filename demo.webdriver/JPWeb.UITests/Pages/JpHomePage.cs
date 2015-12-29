using demo.webdriver.Browsers;
using demo.webdriver.Pages;
using JPWeb.UITests.Widgets;
using OpenQA.Selenium;

namespace JPWeb.UITests.Pages
{
    public class JpHomePage : IAppPage
    {
        private readonly IBrowserTestingSession _testingSession;
        private readonly YmmWidget _widget;
        private readonly SearchWidget _searchWidget;

        public JpHomePage(IBrowserTestingSession testingSession)
        {
            _testingSession = testingSession;
            _widget = _testingSession.GetDriver<YmmWidget>(By.ClassName("fitment-drop-down"));
            _searchWidget = _testingSession.GetDriver<SearchWidget>(By.ClassName("abc"));
        }

        public void Navigate()
        {
            //_testingSession.Browser.NavigateTo<HomeController>(c => c.HomePage(22));
            _testingSession.Browser.NavigateTo("http://www.jpcycles.com/");
        }

        public void SelectAMotorcycle(string year, string make, string model)
        {
            _widget.SelectYear(year);
            _widget.SelectMake(make);
            _widget.SelectModel(model);
            _widget.Submit();
        }

        public void Search(string searchString)
        {
            _searchWidget.Type(searchString);
            _searchWidget.Submit();
        }
    }
}