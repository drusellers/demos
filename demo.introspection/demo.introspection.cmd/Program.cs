using System;

namespace demo.introspection.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var pu = new PageUpdater();
            var resp = pu.Get(115818626);
            Console.WriteLine(resp.body.view.value);

            pu.Put(115818626, resp.version.number, "<p>UPDATED2</p>");
            Console.ReadKey();
        }
    }
}
