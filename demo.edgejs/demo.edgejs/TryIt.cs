using System;
using System.Threading.Tasks;

namespace demo.edgejs
{
    /// <summary>
    /// https://github.com/tjanczuk/edge#how-to-nodejs-hello-world
    /// </summary>
    public class TryIt
    {
        public static async Task<bool> Start()
        {
            var func = EdgeJs.Edge.Func(@"
return function(data, callback)
{
    callback(null, {'Address':123});
}
");
            dynamic o = await func(new Payload
            {
                Name = ".net"
            });

            Console.WriteLine(o.Address);

            return true;
        }

        public void Go()
        {
            Task.Run<bool>((Func<Task<bool>>)Start).Wait();
        }
    }

    public class Payload
    {
        public string Name { get; set; }
    }
}