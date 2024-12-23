using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        return;
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
                        return;
                    }
                }
                else if (false)//samo u domain napravi za provejeru
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
                        return;
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
                        return;
                    }
                }
                else
                    break;
            }

            //provjeri u domainu
            //ako je sve ok onda se prikaze menu
            Console.WriteLine("Uspjesno logirani.");
            Console.ReadKey();
            Drive.Presentation.Actions.Menus.DriveMenuActions.DriveMenu();
        }
    }
}
