using System;
using System.Diagnostics;
using System.Drawing;
using Castle.Core.Internal;
using Castle.Windsor;
using OpenQA.Selenium;

namespace demo.webdriver.Browsers
{
    public abstract class BaseBrowserTestingSession : IBrowserTestingSession
    {
        public static readonly TimeSpan DefaultImplicitWaittime = TimeSpan.FromSeconds(30);
        private readonly Lazy<IBrowserInstance> _instance;
        private readonly IWindsorContainer _container;

        protected BaseBrowserTestingSession(IWindsorContainer container)
        {
            _container = container;
            _instance = new Lazy<IBrowserInstance>(GetInstance);
        }

        public virtual void Dispose()
        {
            _instance.Value.Dispose();
        }

        public virtual void Start()
        {
            if (IsRunningOnBuildServer())
            {
                MouseDriver.MoveToBottomCorner();
            }

            HandleBrowserSize();
            HandleDefaults();
        }

        private void HandleDefaults()
        {
            try
            {
                //TODO: put into configuration model?
                Browser.WebDriver
                    .Manage()
                    .Timeouts()
                    .ImplicitlyWait(DefaultImplicitWaittime)
                    .SetScriptTimeout(DefaultImplicitWaittime);

            }
            catch (Exception e)
            {
                //logging
                Debug.Fail("Problem configuring RemoteWebDriver");
            }
        }

        private void HandleBrowserSize()
        {

            var size = getWindowSize();

            if (size.HasValue)
            {
                //logging
                Browser.WebDriver.Manage().Window.Size = size.Value;
            }
            else
            {
                //logging
                Browser.WebDriver.Manage().Window.Maximize();
            }

            var resizedSize = Browser.WebDriver.Manage().Window.Size;
            //logging

        }

        public IBrowserInstance Browser
        {
            get { return _instance.Value; }
        }
        public IWindsorContainer Container { get { return _container;
            
        } }

        public bool IsRunningOnBuildServer()
        {
            var str = Environment.GetEnvironmentVariable("ISTEAMCITY");
            return str.IsNullOrEmpty();
        }

        private Size? getWindowSize()
        {
            try
            {
                var width = int.Parse(Environment.GetEnvironmentVariable("BROWSER_WIDTH"));
                var height = int.Parse(Environment.GetEnvironmentVariable("BROWSER_HEIGHT"));

                return new Size(width, height);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected abstract IBrowserInstance GetInstance();

        public T GetDriver<T>(By selector)
        {
            return _container.Resolve<T>(new
            {
                selector
            });
        }
    }
}