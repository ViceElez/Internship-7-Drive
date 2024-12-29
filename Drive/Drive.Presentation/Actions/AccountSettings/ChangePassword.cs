using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;

namespace Drive.Presentation.Actions.AccountSettings
{
    public class ChangePassword
    {
        public static void ChangeAccountPassword(User loggedUser)
        {
            Console.Write("Unesite novu sifru:");
            var newPassword = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(newPassword))
                {
                    Console.WriteLine("Lozinka nemoze biti prazna.");
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite lozinku:");
                        newPassword = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        AccountSettingActions.AccountSettingsMenu(loggedUser);
                    }
                }
                else
                {
                    break;
                }
            }

            Console.Write("Unesite ponovo lozinku:");
            var newPasswordRepeat = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(newPasswordRepeat))
                {
                    Console.WriteLine("Lozinka nemoze biti prazna.");
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite ponovno lozinku:");
                        newPasswordRepeat = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        AccountSettingActions.AccountSettingsMenu(loggedUser);
                    }
                }
                else if (newPassword != newPasswordRepeat)
                {
                    Console.WriteLine("Lozinke se ne podudaraju.");
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite ponovno lozinku:");
                        newPasswordRepeat = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        AccountSettingActions.AccountSettingsMenu(loggedUser);
                    }
                }
                else
                {
                    break;
                }
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Domain.Repositories.UserRepository.ChangeAccountPassword(newPassword, loggedUser);
                Console.WriteLine("Lozinka uspjesno promjenjena.");
                Console.ReadKey();
                AccountSettingActions.AccountSettingsMenu(loggedUser);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                AccountSettingActions.AccountSettingsMenu(loggedUser);
            }
        }
    }
}
