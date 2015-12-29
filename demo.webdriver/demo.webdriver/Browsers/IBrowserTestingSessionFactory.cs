namespace demo.webdriver.Browsers
{
    public interface IBrowserTestingSessionFactory
    {
        IBrowserTestingSession CreateSession<TMarker>(SupportedBrowserType browserType = SupportedBrowserType.Chrome);
    }
}