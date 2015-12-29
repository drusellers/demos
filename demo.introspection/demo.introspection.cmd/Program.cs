using System;

namespace demo.introspection.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var pu = new PageUpdater();
            Console.WriteLine(pu.Get());

            Console.ReadKey();
        }
    }
}
