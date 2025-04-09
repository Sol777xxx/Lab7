using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; Console.InputEncoding = System.Text.Encoding.UTF8;
                var menu = Menu.Create(); // 🔹 Весь «двіж» усередині Menu
                menu.ShowMenu();
            }
        }
    }
}
