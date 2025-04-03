using BLL;
using System;
public class Menu
{
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Готель =====");
            Console.WriteLine("1. Клієнт");
            Console.WriteLine("2. Адмін");
            Console.WriteLine("0. Вийти");
            Console.Write("Оберіть опцію: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ClientMenu(); break;
                case "2": Admin(); break;
                case "0": return;
                default:
                    Console.WriteLine("Невідома команда, спробуйте ще раз.");
                    Console.ReadLine();
                    break;
            }
        }
    }
    public void ClientMenu() { }
    public void Admin() { }
}
