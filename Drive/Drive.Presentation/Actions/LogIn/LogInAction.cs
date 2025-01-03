using Drive.Domain.Repositories;
using Drive.Presentation.Actions.Menus;


namespace Drive.Presentation.Actions.LogIn
{
    public class LogInAction
    {
        public static void UserLogInAction()
        {
            Console.Write("Upisite email za login:");
            var loginEmail = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(loginEmail))
                {
                    Console.WriteLine("Email nemoze biti prazan.");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        loginEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces logina je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (!Helper.InputValidation.IsValid(loginEmail))
                {
                    Console.WriteLine("Neipsravan format email-a.");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        loginEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces logina je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (!Drive.Domain.Repositories.UserRepository.EmailExists(loginEmail))
                {
                    Console.WriteLine("Ne postoji racun s tim emailom");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email za login:");
                        loginEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces logina je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }

                }
                else
                    break;
            }

            Console.Write("Upisite lozinku:");
            var loginPassword = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(loginPassword))
                {
                    Console.WriteLine("Lozinka nemoze biti prazna.");
                    Console.WriteLine("Proces logiranja zakljucan 30 sekundi.");
                    Thread.Sleep(30000);
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite lozinku:");
                        loginPassword = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces logina je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (!Drive.Domain.Repositories.UserRepository.ConfirmPassword(loginEmail, loginPassword))
                {
                    Console.WriteLine("Pogresna lozinka");
                    Console.WriteLine("Proces logiranja zakljucan 30 sekundi.");
                    Thread.Sleep(30000);
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite lozinku:");
                        loginPassword = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces logina je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else
                    break;
            }
            Console.WriteLine("Uspjesno logirani.");
            Console.ReadKey();
            var loggedUser = UserRepository.GetUserByEmail(loginEmail);
            DriveMenuActions.DriveMenu(loggedUser);
        }
    }
}
