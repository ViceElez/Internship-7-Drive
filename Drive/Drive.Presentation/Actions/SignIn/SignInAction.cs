
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
                        return;
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
                        return;
                    }
                }
                else if (false)//samo u domain napravi za provejeru
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
                        return;
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
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Upisite ponovno lozinku za registraciju:");
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
                        return;
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
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Uspjesno registrirani!");
            Console.ReadKey();

            //spremi u bazu

            Drive.Presentation.Actions.Menus.DriveMenuActions.DriveMenu();



        }
    }
}
