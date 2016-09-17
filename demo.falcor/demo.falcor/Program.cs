using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Falcor;
using Falcor.Server;
using Falcor.Server.Owin;
using Falcor.Server.Routing;
using Microsoft.Owin.Hosting;
using Owin;

namespace demo.falcor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                var x = FalcorRouterConfiguration.PathParser.ParseMany("[[\"bob\", \"0..2\"]]");
                var y = FalcorRouterConfiguration.RouteParser.Parse("message.[0..2]");

                Console.WriteLine($"Hosted at: {baseAddress}");
                Console.ReadKey();
            }

            Console.ReadLine();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFalcor("/helloWorldModel.json", routerFactory: config =>
            {

                return new HelloWorldRouter();
            });

        }
    }

    public class HelloWorldRouter : FalcorRouter
    {
        public HelloWorldRouter()
        {
            // Route to match a JSON path, in this case the 'message' member
            // of the root JSON node of the virtual JSON Graph
            Get["message"] = async _ =>
            {
                var result = await Task.FromResult(Path("message").Atom("Hello World"));
                return Complete(result);
            };
            Get["order[{integers:eventIds}]"] = async _ =>
            {
                var o = (NumericSet)_.eventIds;
                var r = o.Select(i => Path(new StringKey("order"), new NumberKey(i)).Atom("BOOM"));

                var result = await Task.FromResult(r);
                return Complete(result);
            };
            // Define additional routes for your virtual model here
        }
    }
}
