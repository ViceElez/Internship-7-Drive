using Drive.Domain.Repositories;
using Drive.Presentation.Actions.Menus;
namespace Drive.Presentation.Actions.SignIn
{
    public class SignInAction
    {
        public static void UserSignInAction()
        {
            Console.Write("Upisite mail za registraciju:");
            var registrationEmail = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(registrationEmail))
                {
                    Console.WriteLine("Email nemoze biti prazan.");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        registrationEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (!Helper.InputValidation.IsValid(registrationEmail))
                {
                    Console.WriteLine("Neipsravan format email-a.");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email:");
                        registrationEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (Drive.Domain.Repositories.UserRepository.EmailExists(registrationEmail))
                {
                    Console.WriteLine("Vec postoji racun s tim emailom");
                    var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForEmail)
                    {
                        Console.Write("Unesite email za registraciju:");
                        registrationEmail = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }

                }
                else
                    break;
            }


            Console.Write("Upisite lozinku za registraciju:");
            var registrationPassword = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(registrationPassword))
                {
                    Console.WriteLine("Lozinka nemoze biti prazna.");
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite lozinku:");
                        registrationPassword = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else
                {
                    break;
                }
            }

            Console.Write("Upisite ponovno lozinku za registraciju:");
            var registrationPasswordRepeat = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(registrationPasswordRepeat))
                {
                    Console.WriteLine("Lozinka nemoze biti prazna.");
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite ponovno lozinku:");
                        registrationPasswordRepeat = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (registrationPassword != registrationPasswordRepeat)
                {
                    Console.WriteLine("Lozinke se ne podudaraju.");
                    var confirmForPassword = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForPassword)
                    {
                        Console.Write("Unesite ponovno lozinku:");
                        registrationPasswordRepeat = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else
                {
                    break;
                }
            }

            var generatedCaptcha=Drive.Domain.Repositories.UserRepository.GenerateCaptcha();
            Console.WriteLine($"{generatedCaptcha}");
            Console.Write("Upisite captcha kod:");
            var enteredCaptcha = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(enteredCaptcha))
                {
                    Console.WriteLine("Captcha nemoze biti prazna.");
                    var confirmForCaptcha = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForCaptcha)
                    {
                        Console.Write("Unesite captcha kod:");
                        enteredCaptcha = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else if (!Drive.Domain.Repositories.UserRepository.CheckCaptcha(generatedCaptcha,enteredCaptcha))
                {
                    Console.WriteLine("Captcha kodovi se ne podudaraju.");
                    var confirmForCaptcha = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForCaptcha)
                    {
                        Console.Write("Unesite captcha kod:");
                        enteredCaptcha = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces registracije je prekinut.");
                        Console.ReadKey();
                        MainMenuActions.MainMenu();
                    }
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Uspjesno registrirani!");
            Console.ReadKey();
            Drive.Domain.Repositories.UserRepository.AddUser(registrationEmail, registrationPassword);
            var registeredUser = Drive.Domain.Repositories.UserRepository.GetUserByEmail(registrationEmail);
            DriveMenuActions.DriveMenu(registeredUser);



        }
    }
}
