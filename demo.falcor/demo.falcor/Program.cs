using System;
using System.Linq;
using System.Threading.Tasks;
using Falcor;
using Falcor.Server;
using Falcor.Server.Owin;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
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
            app.UseGraphQL("/graphql");
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
            //http://localhost:9000/helloWorldModel.json?paths=[["message"]]&method=get
            Get["message"] = async _ =>
            {
                var result = await Task.FromResult(Path("message").Atom("Hello World"));
                return Complete(result);
            };

            //http://localhost:9000/helloWorldModel.json?paths=[["order", {"from":0, "to":2}]]&method=get
            Get["order[{ranges:eventIds}]"] = async _ =>
            {
                var o = (NumberRange)_.eventIds;
                var r = o.Select(i => Path(new StringKey("order"), new NumberKey(i)).Atom("BOOM"));

                var result = await Task.FromResult(r);
                return Complete(result);
            };

            //http://localhost:9000/helloWorldModel.json?paths=[["items", {"from":0, "to":2}, ["cost","itemId"]]]&method=get
            Get["items[{ranges:itemIds}]['cost', 'description', 'itemId']"] = async _ =>
            {
                NumberRange a = _.itemIds;

                var f = Path("items").Atom("");
                var result = await Task.FromResult(f);
                return Complete(result);
            };
            // Define additional routes for your virtual model here
        }
    }
}
