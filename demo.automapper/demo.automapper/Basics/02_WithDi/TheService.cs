using System;
using Castle.Core.Logging;

namespace demo.ioc._02_WithDi
{
    public class TheService
    {
        public void Work()
        {
            
        } 
    }

    public class TheController
    {
        private TheService _theService;

        // Inversion of Control - who controls the 'creation'
        // Injecting the Dependency - type: Constructor
        // Q: Who 'creates' the instance of 'TheService'?
        public TheController(TheService theService)
        {
            _theService = theService;
        }

        public void DoStuff()
        {
            Logger.Log();
            _theService.Work();
        }


        // DI via Property (optional)
        private ILogger _logger;

        public ILogger Logger
        {
            get { return _logger ?? new NullLogger(); }
        }

        // DI via Method
        public void SetTheService(TheService service)
        {
            _theService = service;
        }
    }

    public class NullLogger : ILogger
    {
        public void Log()
        {
            throw new NotImplementedException();
        }
    }
    public interface ILogger
    {
        void Log();
    }
}