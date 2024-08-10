using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleApp.ConsoleControl
{
    class Output
    {
        public Output() { }
        public static void Print(string str = "")
        {
            Console.Write(str);
        }
        public static void PrintL(string str = "")
        {
            Console.WriteLine(str);
        }
        public static void Blue(string str = "")
        {
            Console.Write(str);
        }
    }
}
