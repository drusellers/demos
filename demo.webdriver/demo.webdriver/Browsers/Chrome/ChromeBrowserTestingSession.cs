using System.Net;
using Castle.Windsor;
using OpenQA.Selenium.Chrome;

namespace demo.webdriver.Browsers.Chrome
{
    public class ChromeBrowserTestingSession : BaseBrowserTestingSession
    {
        private ChromeDriverService _svc;

        public ChromeBrowserTestingSession(IWindsorContainer container) : base(container)
        {
        }

        public override void Start()
        {
            _svc = ChromeDriverService.CreateDefaultService();
            _svc.Start();
        }

        protected override IBrowserInstance GetInstance()
        {
            return new ChromeBrowserInstance(_svc.ServiceUrl, Container);
        }

        public override void Dispose()
        {
            base.Dispose();
            _svc.Dispose();

//            var req = HttpWebRequest.Create(_svc.ServiceUrl + "/shutdown");
//            using (var resp = req.GetResponse())
//            {
//                
//            }
//           

        }
    }
}