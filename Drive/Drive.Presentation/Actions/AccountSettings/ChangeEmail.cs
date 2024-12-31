using Drive.Data.Entities.Models.Users;

namespace Drive.Presentation.Actions.AccountSettings
{
    public class ChangeEmail
    {
        public static void ChangeAccountEmail(User loggedUser)
        {
            Console.Clear();
            Console.WriteLine($"Trenutni email:{loggedUser.Email}");
            Console.Write("Unesite novi email: ");
            var newEmail = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(newEmail))
                {
                    Console.WriteLine("Email nemoze biti prazan.");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        newEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        AccountSettingActions.AccountSettingsMenu(loggedUser);
                    }
                }
                else if (!Helper.InputValidation.IsValid(newEmail))
                {
                    Console.WriteLine("Neipsravan format email-a.");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        newEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        AccountSettingActions.AccountSettingsMenu(loggedUser);
                    }
                }
                else if (Drive.Domain.Repositories.UserRepository.EmailExists(newEmail))
                {
                    Console.WriteLine("Vec postoji racun s tim emailom");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        newEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        AccountSettingActions.AccountSettingsMenu(loggedUser);
                    }

                }
                else
                    break;
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                loggedUser=Drive.Domain.Repositories.UserRepository.ChangeAccountEmail(newEmail, loggedUser);
                Console.WriteLine("Email uspjesno promijenjen!");
                Console.ReadKey();
                AccountSettingActions.AccountSettingsMenu(loggedUser);
            }
            else
            {
                Console.WriteLine("Proces promjene emaila je prekinut.");
                Console.ReadKey();
                AccountSettingActions.AccountSettingsMenu(loggedUser);
            }
        }
    }
}
