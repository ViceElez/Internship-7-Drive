using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Presentation.Actions.File
{
    public class FileActions
    {
        public static void CreateFile(User loggedUser)
        {
            Console.Write("Upisite ime file-a:");
            var fileName=Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Ime file-a nemoze biti prazno.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime file-a:");
                        fileName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces kreiranja file-a je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                    break;

            }
            Console.WriteLine($"Zelite li kreirati novi file pod nazivom:{fileName}");
            var answer = Console.ReadLine().Trim().ToLower();
            do
            {
                if (answer == "da")
                {
                    Drive.Domain.Repositories.FileRepository.AddFile(loggedUser, fileName);
                    Console.WriteLine("File uspjesno kreirana.");
                    Console.ReadKey();
                    MyDiskMenuActions.MyDiskMenu(loggedUser);
                }
                else if (answer == "ne")
                {
                    Console.WriteLine("Proces kreiranja file-a je prekinut.");
                    Console.ReadKey();
                    MyDiskMenuActions.MyDiskMenu(loggedUser);
                }
                else
                {
                    Console.WriteLine("Unesite da ili ne.");
                    answer = Console.ReadLine().Trim().ToLower();
                }

            } while (answer != "da" && answer != "ne");
        }
    }
}
