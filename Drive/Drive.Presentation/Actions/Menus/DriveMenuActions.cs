namespace Drive.Presentation.Actions.Menus
{
    public class DriveMenuActions
    {
        public static void DriveMenu()
        {
            string[] driveMenuOptions = new string[] { "Moj disk", "Dijeljeno sa mnom", "Postavke profila", "Odjava iz profila" };
            var menu = new DriveMenu(driveMenuOptions);
            int selectedOption = menu.Run();

            //nek se napise ime usera koi je ulogiran
            switch (selectedOption)
            {
                case 0:
                    Console.WriteLine("Moj disk");
                    DriveMenuActions.DriveMenu();
                    break;
                case 1:
                    Console.WriteLine("Dijeljeno sa mnom");
                    DriveMenuActions.DriveMenu();
                    break;
                case 2:
                    Console.WriteLine("Postavke profila");
                    DriveMenuActions.DriveMenu();
                    break;
                case 3:
                    MainMenuActions.MainMenu();
                    break;
            }
        }
    }
}
