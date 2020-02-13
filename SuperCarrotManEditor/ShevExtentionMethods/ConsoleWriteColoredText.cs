using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shev.ExtentionMethods
{
    public static class shevConsole
    {
        public static void WriteColored(string text,ConsoleColor Color)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.Write(text);
            Console.ForegroundColor = currentColor;
        }
        public static void WriteColoredLine(string text, ConsoleColor Color)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.WriteLine(text);
            Console.ForegroundColor = currentColor;
        }
    }
}
