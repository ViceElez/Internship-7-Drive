﻿using Drive.Data.Entities.Models.Users;

namespace Drive.Presentation.Actions.Menus
{
    public class DriveMenuActions
    {
        public static void DriveMenu(User loggedUser)
        {
            Console.WriteLine($"\tDobrodosli: {loggedUser.Email}");
            string[] driveMenuOptions = new string[] { "Moj disk", "Dijeljeno sa mnom", "Postavke profila", "Odjava iz profila" };
            var menu = new DriveMenu(driveMenuOptions);
            int selectedOption = menu.Run();

            switch (selectedOption)
            {
                case 0:
                    Console.WriteLine("Moj disk");
                    break;
                case 1:
                    Console.WriteLine("Dijeljeno sa mnom");
                    break;
                case 2:
                    Console.WriteLine("Postavke profila");
                    break;
                case 3:
                    MainMenuActions.MainMenu();
                    break;
            }
        }
    }
}
