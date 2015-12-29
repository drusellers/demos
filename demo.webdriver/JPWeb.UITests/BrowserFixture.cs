using demo.webdriver.Browsers;
using NUnit.Framework;

namespace JPWeb.UITests
{
    public class BrowserFixture
    {
        private bool _open;
        //spin up the container
        protected IBrowserTestingSession Session { get; private set; }

        protected void LeaveTheBrowserOpen()
        {
            _open = true;
        }
        
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            //AppFactory.CreateInstance<BrowserApplication>() as IApplication(Container)
            //TestFactory.CreateInstance(Chrome) as ITestingSession
            // session(browser, application)

            var factory = new BrowserTestingSessionFactory();
            Session = factory.CreateSession<TestMarker>();   
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if(!_open)
                Session.Dispose();
        }
    }
}