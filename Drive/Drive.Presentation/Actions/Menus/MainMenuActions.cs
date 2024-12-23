namespace Drive.Presentation.Actions.Menus
{
    public class MainMenuActions
    {
        public static void MainMenu()
        {
            string[] options = new string[] { "Prijavi se", "Registriraj se", "Izlaz" };
            var menu = new DriveMenu(options);
            int selectedOption = menu.Run();

            switch (selectedOption)
            {
                case 0:
                    Drive.Presentation.Actions.Menus.DriveMenuActions.DriveMenu();
                    break;
                case 1:
                    Console.WriteLine("Signin");
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }


        }
    }
}
