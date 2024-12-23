
namespace Drive.Presentation.Actions.Menus;

public class DriveMenu
{
    private int SelectedOption;
    private string[] Options;

    public DriveMenu(string[] options)
    {
        Options = options;
    }

    private void PrintMenu()
    {
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
            }
            Console.WriteLine($"{Options[i]}");
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
