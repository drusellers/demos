using System;

namespace deme.fooidity_examples
{
    public class BlueConsoleWriter : ConsoleWriter
    {
        public void Write(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }
    }
}