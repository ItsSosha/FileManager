using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestFramework;
using CommandsLib;

namespace PresentationLayer
{
    [TestClass]
    class UnitTests
    {
        public Command ConsoleCommand = new();

        [TestMethod]
        public void PrintToConsoleTest()
        {
            string expected = "Hello, Mark!";
            string actual = ConsoleCommand.PrintToConsole("Mark");

            TestFramework.IsEqual(expected, actual);
        }
    }
}
