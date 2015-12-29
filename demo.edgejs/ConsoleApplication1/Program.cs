using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            new demo.roslyn.TryIt().Go();
            new demo.edgejs.TryIt().Go();
            Console.ReadKey();
        }
    }


}
