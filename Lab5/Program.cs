using System;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; Console.InputEncoding = System.Text.Encoding.UTF8;
                var menu = Menu.Create();
                menu.ShowMenu();
            }
        }
    }
}
