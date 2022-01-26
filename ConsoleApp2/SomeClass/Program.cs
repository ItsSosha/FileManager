using System;

namespace CommandsLib
{
    public class Command
    {
        public string PrintToConsole(string name)
        {
            string str = $"Hello, {name}!";
            // Console.WriteLine(str);
            return str;
        }
    }
}
