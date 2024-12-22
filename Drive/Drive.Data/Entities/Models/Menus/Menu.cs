using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Data.Entities.Models.Menus
{
    public class Menu
    {
        private int SelectedOption;
        private string[] Options;
        private string Title;

        public Menu(string title, string[] options)
        {
            Title = title;
            Options = options;
        }

        private void PrintMenu()
        {
            Console.WriteLine(Title);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                if (i == SelectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{i + 1}. {currentOption}");
                }
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                PrintMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedOption--;
                    if (SelectedOption == -1)
                    {
                        SelectedOption = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedOption++;
                    if (SelectedOption == Options.Length)
                    {
                        SelectedOption = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedOption;
        }
    }
}
