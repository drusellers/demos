using System;
using Castle.Core.Logging;
using demo.webdriver.Browsers;

namespace demo.webdriver
{
    public class ApplicationContext
    {
        private static Uri _root = new Uri("http://www.jpcycles.com");
 
        private readonly IBrowserTestingSessionFactory _testingSessionFactory;
        private readonly IDrivers _drivers;
        private readonly ILogger _logger;
        // "system" the application under test

        public ApplicationContext(IBrowserTestingSessionFactory testingSessionFactory, IDrivers drivers, ILogger logger)
        {
            _testingSessionFactory = testingSessionFactory;
            _drivers = drivers;
            _logger = logger;
        }
    }
}