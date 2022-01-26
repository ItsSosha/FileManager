using System;

namespace Loggers
{
    public class Logger : ILogger
    {
        public void Log(bool equalty, int testNumber)
        {
            if (equalty)
            {
                Console.Write($"Test {testNumber}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Passed");
            }
            else
            {
                Console.Write($"Test {testNumber}: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed");
            }
            Console.ResetColor();
        }
    }
}
