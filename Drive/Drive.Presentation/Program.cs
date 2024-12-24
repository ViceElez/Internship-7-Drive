

namespace Drive.Presentation
{
    public class Program
    {
        static void Main(string[] args)
        {
            string decorativeLine = new string('-', 30);
            Console.WriteLine(decorativeLine);
            Console.WriteLine("\tDOBRODOSLI");
            Console.WriteLine(decorativeLine);
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey();
            Drive.Presentation.Actions.Menus.MainMenuActions.MainMenu();
        }
    }
}