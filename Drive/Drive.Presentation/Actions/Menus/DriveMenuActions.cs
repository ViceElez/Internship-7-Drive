using Drive.Data.Entities.Models.Users;

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
                    MyDiskMenuActions.MyDiskMenu(loggedUser, null);
                    break;
                case 1:
                    SharedWithMeMenuActions.SharedDiskMenu(loggedUser, null);
                    break;
                case 2:
                    AccountSettings.AccountSettingActions.AccountSettingsMenu(loggedUser);
                    break;
                case 3:
                    MainMenuActions.MainMenu();
                    break;
            }
        }
    }
}
