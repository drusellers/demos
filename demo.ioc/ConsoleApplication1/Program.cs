using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = AppContext.Build();


            app.RunAsConsole();
            app.RunAsService();
            app.RunAsMessageQueuue();
            app.RunAsWeb(); 
        }
    }

    public class AppContext
    {
        private IWindsorContainer _container;

        private AppContext(WindsorContainer windsorContainer)
        {
            _container = windsorContainer;
        }

        public static AppContext Build(params Assembly[] extensionAssemblies)
        {
            var c = new WindsorContainer();
            return new AppContext(c);
          
        }
    }
}
