using Castle.Windsor;
using OpenQA.Selenium.PhantomJS;

namespace demo.webdriver.Browsers.Phantom
{
    public class PhantomBrowserTestingSession : BaseBrowserTestingSession
    {
        private PhantomJSDriverService _svc;

        public PhantomBrowserTestingSession(IWindsorContainer container) : base(container)
        {
        }

        public override void Start()
        {
            _svc = PhantomJSDriverService.CreateDefaultService();
            _svc.Start();
        }

        protected override IBrowserInstance GetInstance()
        {
            return new PhantomBrowserInstance(_svc.ServiceUrl, Container);
        }

        public override void Dispose()
        {
            base.Dispose();
            _svc.Dispose();
        }
    }
}