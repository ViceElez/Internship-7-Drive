using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;

namespace Drive.Presentation.Actions.AccountSettings
{
    public class AccountSettingActions
    {
        public static void AccountSettingsMenu(User loggedUser)
        {
            string[] accountSettingsOptions = new string[] { "Promjena emaila", "Promjena lozinke", "Povratak" };
            var menu = new DriveMenu(accountSettingsOptions);
            int selectedOption = menu.Run();
            switch (selectedOption)
            {
                case 0:
                    ChangeEmail.ChangeAccountEmail(loggedUser);
                    break;
                case 1:
                    ChangePassword.ChangeAccountPassword(loggedUser);
                    break;
                case 2:
                    DriveMenuActions.DriveMenu(loggedUser);
                    break;
            }
        }
    }
}
