using System;

namespace deme.fooidity_examples
{
    public class RedConsoleWriter : ConsoleWriter
    {

        public void Write(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }
    }
}